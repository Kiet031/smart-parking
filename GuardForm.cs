using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SmartParking
{
    public partial class GuardForm : Form
    {
        private readonly Form _loginForm;
        private bool _isLoggingOut = false;

        #region Biến toàn cục & Khai báo Dịch vụ
        private readonly CameraService _cameraService = new CameraService();
        private readonly RFIDReader _rfidReader = new RFIDReader();
        public RFIDReader RfidReader => _rfidReader;
        public bool IsRegistrationActive = false;

        // Bản ghi xe hiện tại đang so sánh ở cổng ra
        private ParkingRecord? _currentParkingRecord = null;

        // Ảnh snapshot lúc ra cổng
        private Bitmap? _exitFrontSnap = null;
        private Bitmap? _exitRearSnap = null;

        // Bộ nhớ đệm giả lập Keyboard Wedge
        private readonly StringBuilder _kbdBuffer = new StringBuilder();
        private DateTime _lastKeyPressTime = DateTime.MinValue;

        // Các bộ hẹn giờ cho giao diện
        private readonly System.Windows.Forms.Timer _resetTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _flashTimer = new System.Windows.Forms.Timer();
        private bool _isFlashRed = false;

        // Trạng thái loa phát thanh
        private bool _isAudioEnabled = true;

        // Bộ nhớ đệm giữ thông tin giao dịch xe vào khi đang đóng băng chờ bảo vệ xác nhận
        private string? _pendingEntryCardId = null;
        private Bitmap? _entryFrontSnap = null;
        private Bitmap? _entryRearSnap = null;
        #endregion

        public class SubscriptionUser
        {
            public string MemberId { get; set; } = string.Empty;
            public string UserCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string VehicleInfo { get; set; } = string.Empty;
            public string LicensePlate { get; set; } = string.Empty;
            public string MemberImagePath { get; set; } = string.Empty;
            public string VehicleImagePath { get; set; } = string.Empty;
        }

        public GuardForm(Form loginForm)
        {
            InitializeComponent();
            _loginForm = loginForm;
            SetupTimers();

            this.KeyPreview = true;
        }

        private void SetupTimers()
        {
            _resetTimer.Interval = 3000;
            _resetTimer.Tick += (s, e) =>
            {
                _resetTimer.Stop();
                ResetDisplayForNext();
            };

            _flashTimer.Interval = 500;
            _flashTimer.Tick += (s, e) =>
            {
                _isFlashRed = !_isFlashRed;
                pnlStatus.BackColor = _isFlashRed ? Color.Red : Color.White;
                lblStatusText.ForeColor = _isFlashRed ? Color.White : Color.Red;
            };
        }

        #region Xử lý Sự kiện Form
        private void GuardForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            // Lấy link cấu hình RTSP từ Database Settings
            string camEntryFrontUrl = DatabaseHelper.GetCameraUrl("CamEntryFrontUrl");
            string camEntryRearUrl = DatabaseHelper.GetCameraUrl("CamEntryRearUrl");
            string camExitFrontUrl = DatabaseHelper.GetCameraUrl("CamExitFrontUrl");
            string camExitRearUrl = DatabaseHelper.GetCameraUrl("CamExitRearUrl");

            // Ghi nhật ký khởi động camera để xem cấu hình
            Console.WriteLine($"Đang tải Camera Vào Trước RTSP: {camEntryFrontUrl}");
            Console.WriteLine($"Đang tải Camera Vào Sau RTSP: {camEntryRearUrl}");
            Console.WriteLine($"Đang tải Camera Ra Trước RTSP: {camExitFrontUrl}");
            Console.WriteLine($"Đang tải Camera Ra Sau RTSP: {camExitRearUrl}");

            // =========================================================================
            // GIẢI PHÁP SỬA LỖI: TỰ ĐỘNG KHẢO SÁT LƯỚI VÀ HOÁN ĐỔI CỘT HIỂN THỊ HÀNG DƯỚI
            // =========================================================================
            Control containerVaoSau = pbEntryRear;
            while (containerVaoSau.Parent != null && !(containerVaoSau.Parent is TableLayoutPanel) && containerVaoSau.Parent != this)
            {
                containerVaoSau = containerVaoSau.Parent; // Leo ngược lên để tìm GroupBox/Panel bọc ngoài ô Làn Vào
            }

            Control containerRaSau = pbLiveRear;
            while (containerRaSau.Parent != null && !(containerRaSau.Parent is TableLayoutPanel) && containerRaSau.Parent != this)
            {
                containerRaSau = containerRaSau.Parent; // Leo ngược lên để tìm GroupBox/Panel bọc ngoài ô Làn Ra
            }

            // Nếu hệ thống sử dụng TableLayoutPanel để chia lưới 2x2
            if (containerVaoSau.Parent is TableLayoutPanel tableLayout)
            {
                int colVao = tableLayout.GetColumn(containerVaoSau);
                int colRa = tableLayout.GetColumn(containerRaSau);

                // Thực hiện tráo đổi cột trực tiếp trên lưới Layout mẫu để đẩy Làn Ra sang Trái, Làn Vào sang Phải
                tableLayout.SetColumn(containerVaoSau, colRa);
                tableLayout.SetColumn(containerRaSau, colVao);
            }
            else
            {
                // Phương án dự phòng (Fallback) nếu các Panel đặt tự do không dùng lưới TableLayout
                Point tempLoc = containerVaoSau.Location;
                containerVaoSau.Location = containerRaSau.Location;
                containerRaSau.Location = tempLoc;
            }
            // =========================================================================

            // Khởi động luồng Camera Live Stream
            _cameraService.Start(pbEntryFront, pbEntryRear, pbLiveFront, pbLiveRear);

            // Nạp danh sách các cổng COM
            LoadAvailableComPorts();

            // Đăng ký sự kiện RFID Reader
            _rfidReader.CardSwiped += RFIDReader_CardSwiped;
            _rfidReader.ErrorOccurred += RFIDReader_ErrorOccurred;

            // Nạp danh sách xe đang trong bãi
            RefreshGrid();

            // Thiết lập trạng thái ban đầu của các nút bấm
            btnAllowExit.Enabled = false;
            btnWarning.Enabled = false;

            dgvLogs.Focus();
        }

        private void GuardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cameraService.Stop();
            _rfidReader.Stop();

            _exitFrontSnap?.Dispose();
            _exitRearSnap?.Dispose();

            if (!_isLoggingOut)
            {
                Application.Exit();
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            _isLoggingOut = true;
            this.Close();
            _loginForm.Show();
        }
        #endregion

        #region Lắng nghe thiết bị (RFID & Keyboard Wedge)
        private void RFIDReader_CardSwiped(string cardId)
        {
            if (IsRegistrationActive) return;
            ProcessCardSwipe(cardId);
        }

        private void RFIDReader_ErrorOccurred(string errorMessage)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                ExceptionManager.ShowWarning("LỖI ĐẦU ĐỌC THẺ. Vui lòng kiểm tra lại cổng kết nối thiết bị.", "Lỗi RFID COM");
            }));
        }

        private void GuardForm_KeyDown(object? sender, KeyEventArgs e)
        {
            // Nếu bảo vệ đang gõ trực tiếp trong các ô văn bản, cho phép ô tự nhận chữ
            if (txtSimCardId.Focused || txtSimPlate.Focused || txtSearchPlate.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSimCardId.Focused) BtnSimSwipe_Click(this, EventArgs.Empty);
                    else if (txtSimPlate.Focused) BtnSetPlate_Click(this, EventArgs.Empty);
                    else if (txtSearchPlate.Focused) BtnSearchPlate_Click(this, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                return;
            }

            // Quản lý thời gian giãn cách giữa các phím của đầu đọc Keyboard Wedge
            if ((DateTime.Now - _lastKeyPressTime).TotalMilliseconds > 600)
            {
                _kbdBuffer.Clear();
            }
            _lastKeyPressTime = DateTime.Now;

            // CHỈ GOM KÝ TỰ CHỮ VÀ SỐ (Bỏ qua hoàn toàn phím Enter)
            if (e.KeyCode != Keys.Enter)
            {
                char c = (char)e.KeyValue;
                if (char.IsLetterOrDigit(c))
                {
                    _kbdBuffer.Append(c);
                }
            }
        }
        #endregion

        #region Xử lý phím Enter & Esc từ reader RFID và hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Nếu phím gõ xuống là Enter và bảo vệ không phải đang nhập ở các ô văn bản
            if (keyData == Keys.Enter && !txtSimCardId.Focused && !txtSimPlate.Focused && !txtSearchPlate.Focused)
            {
                // TH 1: Bộ đệm đang thu thập mã thẻ từ thiết bị quét RFID (Keyboard Wedge)
                if (_kbdBuffer.Length >= 4)
                {
                    string cardId = _kbdBuffer.ToString();
                    _kbdBuffer.Clear();
                    ProcessCardSwipe(cardId);
                    return true;
                }

                // TH 2: Nút cho phép xe ra đang bật và bảo vệ bấm Enter để đồng ý cho xe ra
                if (btnAllowExit.Enabled && _currentParkingRecord != null)
                {
                    BtnAllowExit_Click(this, EventArgs.Empty);
                    return true;
                }

                return true;
            }

            // Phím ESC để kích hoạt đóng Barie / Cảnh báo lỗi
            if (keyData == Keys.Escape)
            {
                if (btnWarning.Enabled && _currentParkingRecord != null)
                {
                    BtnWarning_Click(this, EventArgs.Empty);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Đọc cơ sở dữ liệu thành viên
        private SubscriptionUser? GetSubscriptionUser(string cardId)
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(DatabaseHelper.GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        SELECT member_id, full_name, vehicle_info, license_plate, member_image_path, user_code, vehicle_image_path
                        FROM subscription_users
                        WHERE card_id = @CardID;
                    ";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CardID", cardId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new SubscriptionUser
                                {
                                    MemberId = reader.GetString(0),
                                    FullName = reader.GetString(1),
                                    VehicleInfo = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    LicensePlate = reader.GetString(3),
                                    MemberImagePath = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    UserCode = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    VehicleImagePath = reader.IsDBNull(6) ? "" : reader.GetString(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting subscription user: {ex.Message}");
            }
            return null;
        }

        private decimal GetCardDefaultFee(string cardId)
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(DatabaseHelper.GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        SELECT ct.default_fee 
                        FROM rfid_cards rc
                        JOIN card_types ct ON rc.card_type_id = ct.card_type_id
                        WHERE rc.card_id = @CardID;
                    ";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CardID", cardId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting card default fee: {ex.Message}");
            }
            return 5000;
        }
        #endregion

        #region Luồng Nghiệp Vụ Bãi Xe 2 Bước Tinh Gọn & Dự Phòng (Fallback)
        private void ProcessCardSwipe(string cardId)
        {
            cardId = cardId.Trim().ToUpper();
            if (string.IsNullOrEmpty(cardId)) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ProcessCardSwipe), cardId);
                return;
            }

            StopAlarm();
            _resetTimer.Stop();

            _kbdBuffer.Clear();
            txtSimCardId.Clear();

            // 1. Kiểm tra loại tài khoản (Thẻ tháng hay Thẻ lượt)
            SubscriptionUser? subUser = GetSubscriptionUser(cardId);
            if (subUser != null)
            {
                txtMemberId.Text = subUser.MemberId;
                txtUserCode.Text = subUser.UserCode;
                txtFullName.Text = subUser.FullName;
                txtVehicleInfo.Text = subUser.VehicleInfo;
                txtLicensePlate.Text = subUser.LicensePlate;

                pbSubPortrait.Image?.Dispose();
                pbSubPortrait.Image = FileStorageManager.LoadCompressedImage(subUser.MemberImagePath);
            }
            else
            {
                txtMemberId.Clear();
                txtFullName.Clear();
                txtUserCode.Clear();
                txtVehicleInfo.Clear();
                txtLicensePlate.Clear();
                pbSubPortrait.Image?.Dispose();
                pbSubPortrait.Image = null;
            }

            // 2. Cập nhật trạng thái quẹt thẻ
            lblStatusText.Text = $"ĐÃ ĐỌC THẺ: [{cardId}] - ĐANG XỬ LÝ...";
            lblStatusText.ForeColor = Color.White;
            pnlStatus.BackColor = Color.FromArgb(71, 85, 105);
            lblStatusText.Refresh();

            bool isInside = DatabaseHelper.IsCardInParking(cardId);

            if (!isInside)
            {
                #region LUỒNG XE VÀO - CHỤP ẢNH, ĐÓNG BĂNG MÀN HÌNH CHỜ DUYỆT
                _pendingEntryCardId = cardId;
                _currentParkingRecord = null; // Bảo đảm luồng ra đang trống

                // 1. Hạ cờ yêu cầu CameraService ngắt Live Stream làn vào
                _cameraService.IsEntryFrontLive = false;
                _cameraService.IsEntryRearLive = false;

                // 2. Kích hoạt chụp ảnh snapshot siêu tốc từ RAM
                _entryFrontSnap?.Dispose();
                _entryRearSnap?.Dispose();
                _entryFrontSnap = _cameraService.CaptureEntryFront();
                _entryRearSnap = _cameraService.CaptureEntryRear();

                // 3. Ép PictureBox hiển thị cứng bức ảnh vừa chụp lên màn hình
                pbEntryFront.Image = (Image)_entryFrontSnap.Clone();
                pbEntryRear.Image = (Image)_entryRearSnap.Clone();

                // 4. Cập nhật bảng phân tích Analytics Row
                txtEntryTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtExitTime.Text = "--";
                txtFeeToPay.Text = "--";

                // 5. Mở khóa cụm điều khiển và hiển thị băng rôn chờ lệnh bảo vệ
                btnAllowExit.Enabled = true;
                btnWarning.Enabled = true;

                lblStatusText.Text = $"XE VÀO: ĐÃ CHỤP ẢNH THẺ [{cardId}] - CHỜ BẢO VỆ XÁC NHẬN (ENTER)";
                lblStatusText.ForeColor = Color.Black;
                pnlStatus.BackColor = Color.FromArgb(251, 191, 36); // Băng rôn màu vàng Amber cảnh báo

                if (_isAudioEnabled) System.Media.SystemSounds.Asterisk.Play();
                #endregion
            }
            else
            {
                #region LUỒNG XE RA - ĐÓNG BĂNG 4 CAM, ĐỔI CHIẾU SONG SONG ẢNH QUÁ KHỨ/HIỆN TẠI
                _currentParkingRecord = DatabaseHelper.GetActiveParking(cardId);
                _pendingEntryCardId = null; // Bảo đảm luồng vào đang trống

                if (_currentParkingRecord != null)
                {
                    // 1. Hạ cờ Đóng băng TOÀN BỘ 4 Camera hệ thống
                    _cameraService.IsEntryFrontLive = false;
                    _cameraService.IsEntryRearLive = false;
                    _cameraService.IsExitFrontLive = false;
                    _cameraService.IsExitRearLive = false;

                    System.Threading.Thread.Sleep(150);
                    Application.DoEvents();

                    // 2. Chụp ảnh snapshot thực tế tại làn ra lúc này
                    _exitFrontSnap?.Dispose();
                    _exitRearSnap?.Dispose();
                    _exitFrontSnap = _cameraService.CaptureExitFront();
                    _exitRearSnap = _cameraService.CaptureExitRear();

                    // 3. HIỂN THỊ SONG SONG ĐỐI CHIẾU:
                    // Ô LÀN RA (Phải): Hiện ảnh chụp hiện tại
                    pbLiveFront.Image = (Image)_exitFrontSnap.Clone();
                    pbLiveRear.Image = (Image)_exitRearSnap.Clone();

                    // Ô LÀN VÀO (Trái): Nạp bức ảnh lịch sử đã lưu trong DB lên để đối chiếu biển số
                    LoadImageToPictureBox(pbEntryFront, _currentParkingRecord.EntryFrontImage);
                    LoadImageToPictureBox(pbEntryRear, _currentParkingRecord.EntryRearImage);

                    // 4. Tính toán giá tiền
                    txtEntryTime.Text = _currentParkingRecord.EntryTime.ToString("yyyy-MM-dd HH:mm:ss");
                    txtExitTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    decimal fee = GetCardDefaultFee(cardId);
                    txtFeeToPay.Text = fee == 0 ? "0 VND" : $"{fee:N0} VND";

                    // 5. Bật nút chờ lệnh duyệt ra
                    btnAllowExit.Enabled = true;
                    btnWarning.Enabled = true;

                    lblStatusText.Text = $"XE RA: ĐỐI CHIẾU ẢNH VÀO/RA MÃ THẺ [{cardId}] - BẤM ENTER ĐỂ CHO RA";
                    lblStatusText.ForeColor = Color.Black;
                    pnlStatus.BackColor = Color.FromArgb(251, 191, 36);

                    if (_isAudioEnabled) System.Media.SystemSounds.Asterisk.Play();
                }
                else
                {
                    lblStatusText.Text = "LỖI: THẺ CÓ TRONG BÃI NHƯNG KHÔNG TÌM THẤY DỮ LIỆU!";
                    lblStatusText.ForeColor = Color.White;
                    pnlStatus.BackColor = Color.Red;
                    _resetTimer.Start();
                }
                #endregion
            }
        }

        private void BtnAllowExit_Click(object sender, EventArgs e)
        {
            // TÌNH HUỐNG 1: BẢO VỆ XÁC NHẬN CHO XE VÀO HỢP LỆ
            if (_pendingEntryCardId != null && _entryFrontSnap != null && _entryRearSnap != null)
            {
                string cardId = _pendingEntryCardId;
                string frontPath = ""; string rearPath = "";
                DateTime entryTime = DateTime.Now;

                try
                {
                    SubscriptionUser? subUser = GetSubscriptionUser(cardId);
                    bool isMonthly = (subUser != null);
                    string plate = (isMonthly && subUser != null) ? subUser.LicensePlate : "";

                    // Lưu file ảnh nén
                    frontPath = FileStorageManager.SaveCompressedTransactionImage(_entryFrontSnap, isMonthly, plate, "Vao", "front", entryTime);
                    rearPath = FileStorageManager.SaveCompressedTransactionImage(_entryRearSnap, isMonthly, plate, "Vao", "rear", entryTime);

                    // Chèn bản ghi xe vào bãi
                    DatabaseHelper.InsertActiveParking(cardId, frontPath, rearPath);

                    lblStatusText.Text = $"HỢP LỆ! XE [{cardId}] VÀO BÃI THÀNH CÔNG (ĐÃ MỞ BARIE)";
                    lblStatusText.ForeColor = Color.White;
                    pnlStatus.BackColor = Color.FromArgb(22, 163, 74); // Màu xanh thành công

                    // GIẢI PHÓNG VÙNG NHỚ VÀ KHÔI PHỤC LIVE STREAM LÀN VÀO MƯỢT MÀ TRỞ LẠI
                    _entryFrontSnap?.Dispose(); _entryFrontSnap = null;
                    _entryRearSnap?.Dispose(); _entryRearSnap = null;
                    _pendingEntryCardId = null;
                    _cameraService.IsEntryFrontLive = true;
                    _cameraService.IsEntryRearLive = true;

                    btnAllowExit.Enabled = false;
                    btnWarning.Enabled = false;

                    if (_isAudioEnabled) System.Media.SystemSounds.Beep.Play();
                    RefreshGrid();
                    _resetTimer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thực hiện transaction xe vào: {ex.Message}");
                }
                return;
            }

            // TÌNH HUỐNG 2: BẢO VỆ XÁC NHẬN CHO XE RA HỢP LỆ
            if (_currentParkingRecord != null)
            {
                string cardId = _currentParkingRecord.CardID;
                string exitFrontPath = "[NO_IMAGE_FALLBACK]"; string exitRearPath = "[NO_IMAGE_FALLBACK]";
                DateTime exitTime = DateTime.Now;

                try
                {
                    if (_exitFrontSnap != null && _exitRearSnap != null)
                    {
                        SubscriptionUser? subUser = GetSubscriptionUser(cardId);
                        bool isMonthly = (subUser != null);
                        string plate = (isMonthly && subUser != null) ? subUser.LicensePlate : "";

                        exitFrontPath = FileStorageManager.SaveCompressedTransactionImage(_exitFrontSnap, isMonthly, plate, "Ra", "front", exitTime);
                        exitRearPath = FileStorageManager.SaveCompressedTransactionImage(_exitRearSnap, isMonthly, plate, "Ra", "rear", exitTime);
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Lỗi lưu ảnh xe ra: {ex.Message}"); }

                try
                {
                    DatabaseHelper.CompleteExitTransaction(cardId, exitFrontPath, exitRearPath, "Cổng số 1");

                    lblStatusText.Text = $"HỢP LỆ! ĐÃ XÓA XE [{cardId}] KHỎI BÃI (MỞ BARIE CHO XE RA)";
                    lblStatusText.ForeColor = Color.White;
                    pnlStatus.BackColor = Color.FromArgb(22, 163, 74);

                    // GIẢI PHÓNG VÙNG NHỚ VÀ KHÔI PHỤC TOÀN BỘ LIVE STREAM 4 CAM TRỞ LẠI BÌNH THƯỜNG
                    _exitFrontSnap?.Dispose(); _exitFrontSnap = null;
                    _exitRearSnap?.Dispose(); _exitRearSnap = null;
                    _currentParkingRecord = null;

                    _cameraService.IsEntryFrontLive = true;
                    _cameraService.IsEntryRearLive = true;
                    _cameraService.IsExitFrontLive = true;
                    _cameraService.IsExitRearLive = true;

                    btnAllowExit.Enabled = false;
                    btnWarning.Enabled = false;

                    if (_isAudioEnabled) System.Media.SystemSounds.Beep.Play();
                    RefreshGrid();
                    _resetTimer.Start();
                }
                catch (Exception ex) { ExceptionManager.HandleException(ex, "Thực hiện giao dịch cho xe ra"); }
                return;
            }
        }

        private void BtnWarning_Click(object sender, EventArgs e)
        {
            StartAlarm();

            if (_pendingEntryCardId != null)
            {
                lblStatusText.Text = $"CẢNH BÁO: HỦY LƯỢT VÀO - KHÓA XE THẺ [{_pendingEntryCardId}]";
                _entryFrontSnap?.Dispose(); _entryFrontSnap = null;
                _entryRearSnap?.Dispose(); _entryRearSnap = null;
                _pendingEntryCardId = null;
            }
            else if (_currentParkingRecord != null)
            {
                lblStatusText.Text = $"CẢNH BÁO: SAI BIỂN SỐ / KHÔNG KHỚP - KHÓA XE THẺ [{_currentParkingRecord.CardID}]";
                _exitFrontSnap?.Dispose(); _exitFrontSnap = null;
                _exitRearSnap?.Dispose(); _exitRearSnap = null;
                _currentParkingRecord = null;
            }

            // Giải phóng lệnh đóng băng, mở luồng Stream hoạt động bình thường
            _cameraService.IsEntryFrontLive = true;
            _cameraService.IsEntryRearLive = true;
            _cameraService.IsExitFrontLive = true;
            _cameraService.IsExitRearLive = true;

            btnAllowExit.Enabled = false;
            btnWarning.Enabled = false;
            RefreshGrid();
        }

        private void ResetDisplayForNext()
        {
            _cameraService.IsEntryFrontLive = true;
            _cameraService.IsEntryRearLive = true;
            _cameraService.IsExitFrontLive = true;
            _cameraService.IsExitRearLive = true;

            pbEntryFront.Image?.Dispose(); pbEntryFront.Image = null;
            pbEntryRear.Image?.Dispose(); pbEntryRear.Image = null;

            // Clear Demographics Panel
            txtMemberId.Clear();
            txtFullName.Clear();
            txtUserCode.Clear();
            txtVehicleInfo.Clear();
            txtLicensePlate.Clear();
            pbSubPortrait.Image?.Dispose();
            pbSubPortrait.Image = null;

            // Clear Analytics Panel
            txtEntryTime.Clear();
            txtExitTime.Clear();
            txtFeeToPay.Text = "--";

            _cameraService.IsVehiclePresent = true;

            lblStatusText.Text = "MỜI QUẸT THẺ RFID";
            lblStatusText.ForeColor = Color.White;
            pnlStatus.BackColor = Color.FromArgb(100, 116, 139);

            btnAllowExit.Enabled = false;
            btnWarning.Enabled = false;

            _kbdBuffer.Clear();
            txtSimCardId.Clear();
            txtSimPlate.Clear();

            dgvLogs.Focus();
        }

        private void StartAlarm()
        {
            _flashTimer.Start();
        }

        private void StopAlarm()
        {
            _flashTimer.Stop();
            lblStatusText.ForeColor = Color.White;
        }
        #endregion

        #region Giả Lập & Thiết Bị (RFID COM, Camera)
        private void LoadAvailableComPorts()
        {
            cbComPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbComPort.Items.Add(port);
            }

            if (cbComPort.Items.Count > 0)
            {
                cbComPort.SelectedIndex = 0;
            }
        }

        private void CbComPort_DropDown(object sender, EventArgs e)
        {
            LoadAvailableComPorts();
        }

        private void BtnConnectCom_Click(object sender, EventArgs e)
        {
            if (_rfidReader.IsOpen)
            {
                _rfidReader.Stop();
                btnConnectCom.Text = "Kết nối";
                btnConnectCom.BackColor = Color.FromArgb(71, 85, 95);
            }
            else
            {
                if (cbComPort.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn cổng COM trước khi kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string selectedPort = cbComPort.SelectedItem.ToString()!;
                if (_rfidReader.Start(selectedPort, 9600))
                {
                    btnConnectCom.Text = "Ngắt kết nối";
                    btnConnectCom.BackColor = Color.FromArgb(220, 38, 38);
                }
            }
        }

        private void BtnSimSwipe_Click(object sender, EventArgs e)
        {
            string cardId = txtSimCardId.Text.Trim();
            if (string.IsNullOrEmpty(cardId))
            {
                MessageBox.Show("Vui lòng nhập mã thẻ giả lập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _rfidReader.SimulateSwipe(cardId);
        }

        private void TxtSimCardId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSimSwipe_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BtnSetPlate_Click(object sender, EventArgs e)
        {
            string plate = txtSimPlate.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(plate))
            {
                _cameraService.CurrentFrontPlate = plate;
                _cameraService.CurrentRearPlate = plate;
                _cameraService.IsVehiclePresent = true;
            }
        }

        private void BtnMismatchExit_Click(object sender, EventArgs e)
        {
            string cardId = txtSimCardId.Text.Trim();
            if (string.IsNullOrEmpty(cardId))
            {
                MessageBox.Show("Vui lòng nhập mã thẻ giả lập để quẹt ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!DatabaseHelper.IsCardInParking(cardId))
            {
                MessageBox.Show("Thẻ này chưa có trong bãi, không thể giả lập quẹt ra không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _cameraService.CurrentFrontPlate = "29K-987.65";
            _cameraService.CurrentRearPlate = "29K-987.65";
            _cameraService.IsVehiclePresent = true;

            _rfidReader.SimulateSwipe(cardId);
        }
        #endregion

        #region Tiện ích Hiển thị
        private void RefreshGrid()
        {
            try
            {
                dgvLogs.DataSource = DatabaseHelper.GetRecentLogs(50);
            }
            catch
            {
                // Bỏ qua
            }
        }

        private void DgvLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLogs.Rows[e.RowIndex];
                string? cardId = row.Cells["Mã Thẻ"].Value?.ToString();
                if (!string.IsNullOrEmpty(cardId))
                {
                    txtSimCardId.Text = cardId;
                }
            }
        }

        private void LoadImageToPictureBox(PictureBox pb, string path)
        {
            try
            {
                // CHUẨN HÓA ĐƯỜNG DẪN: Nếu là đường dẫn tương đối, ép nó đi từ thư mục bin thực thi của App
                string fullPath = path;
                if (!Path.IsPathRooted(path))
                {
                    fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                }

                if (File.Exists(fullPath))
                {
                    // Giải nén file nhị phân trực tiếp ra RAM rồi hiển thị lên PictureBox
                    Image? decompressedImg = FileStorageManager.LoadCompressedImage(fullPath);
                    if (decompressedImg != null)
                    {
                        pb.Image?.Dispose();
                        pb.Image = decompressedImg;
                    }
                }
                else
                {
                    pb.Image?.Dispose();
                    pb.Image = null;
                }
            }
            catch
            {
                pb.Image?.Dispose();
                pb.Image = null;
            }
        }
        #endregion

        #region Event Handlers cho giao diện mới
        private void BtnEnableAudio_Click(object sender, EventArgs e)
        {
            _isAudioEnabled = true;
            btnEnableAudio.BackColor = Color.FromArgb(22, 163, 74);
            btnDisableAudio.BackColor = Color.FromArgb(100, 116, 139);
            lblStatusText.Text = "ĐÃ BẬT LOA PHÁT THANH THÔNG BÁO";
        }

        private void BtnDisableAudio_Click(object sender, EventArgs e)
        {
            _isAudioEnabled = false;
            btnEnableAudio.BackColor = Color.FromArgb(100, 116, 139);
            btnDisableAudio.BackColor = Color.FromArgb(220, 38, 38);
            lblStatusText.Text = "ĐÃ TẮT LOA PHÁT THANH THÔNG BÁO";
        }

        private void BtnRegisterMonthly_Click(object sender, EventArgs e)
        {
            using (var regForm = new SubscriptionRegistrationForm(_cameraService))
            {
                regForm.ShowDialog(this);
            }
        }

        private void BtnSearchPlate_Click(object sender, EventArgs e)
        {
            string plate = txtSearchPlate.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(plate))
            {
                _cameraService.CurrentFrontPlate = plate;
                _cameraService.CurrentRearPlate = plate;
                _cameraService.IsVehiclePresent = true;
                lblStatusText.Text = $"ĐÃ GIẢ LẬP BIỂN SỐ CHO CAMERA: {plate}";
                txtSearchPlate.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập biển số xe cần giả lập / tìm kiếm!", "Tìm kiếm thông tin xe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtSearchPlate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearchPlate_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion
    }
}
