using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Npgsql;

namespace SmartParking
{
    public partial class SubscriptionRegistrationForm : Form
    {
        private readonly CameraService _cameraService;

        public SubscriptionRegistrationForm(CameraService cameraService)
        {
            InitializeComponent();
            _cameraService = cameraService;
        }

        private void SubscriptionRegistrationForm_Load(object sender, EventArgs e)
        {
            cbRoleGroup.SelectedIndex = 0; // Default: Sinh viên
            cbCameraSelect.SelectedIndex = 0; // Default: Làn vào - Camera trước

            if (this.Owner is GuardForm guardForm)
            {
                guardForm.IsRegistrationActive = true;
                guardForm.RfidReader.CardSwiped += RfidReader_CardSwiped;
            }
        }

        private void SubscriptionRegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner is GuardForm guardForm)
            {
                guardForm.IsRegistrationActive = false;
                guardForm.RfidReader.CardSwiped -= RfidReader_CardSwiped;
            }
        }

        private void RfidReader_CardSwiped(string cardId)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(RfidReader_CardSwiped), cardId);
                return;
            }
            txtCardId.Text = cardId;
        }

        private void TxtLicensePlate_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = txtLicensePlate.SelectionStart;
            txtLicensePlate.Text = txtLicensePlate.Text.ToUpper();
            txtLicensePlate.SelectionStart = selectionStart;
        }

        private void BtnChoosePortrait_Click(object sender, EventArgs e)
        {
            ChooseFile(pbPortraitPreview);
        }

        private void BtnChooseVehicle_Click(object sender, EventArgs e)
        {
            ChooseFile(pbVehiclePreview);
        }

        private void ChooseFile(PictureBox pb)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Ảnh (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|Tất cả tệp (*.*)|*.*";
                openFileDialog.Title = "Chọn tệp ảnh đăng ký";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var img = Image.FromFile(openFileDialog.FileName);
                        var oldImg = pb.Image;
                        pb.Image = img;
                        oldImg?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể mở file ảnh: {ex.Message}", "Lỗi Tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnCapturePortrait_Click(object sender, EventArgs e)
        {
            CaptureFrame(pbPortraitPreview);
        }

        private void BtnCaptureVehicle_Click(object sender, EventArgs e)
        {
            CaptureFrame(pbVehiclePreview);
        }

        private void CaptureFrame(PictureBox pb)
        {
            Bitmap? bmp = null;
            try
            {
                switch (cbCameraSelect.SelectedIndex)
                {
                    case 0:
                        bmp = _cameraService.CaptureEntryFront();
                        break;
                    case 1:
                        bmp = _cameraService.CaptureEntryRear();
                        break;
                    case 2:
                        bmp = _cameraService.CaptureExitFront();
                        break;
                    case 3:
                        bmp = _cameraService.CaptureExitRear();
                        break;
                    default:
                        bmp = _cameraService.CaptureEntryFront();
                        break;
                }

                if (bmp != null)
                {
                    var oldImg = pb.Image;
                    pb.Image = bmp;
                    oldImg?.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chụp ảnh từ camera: {ex.Message}", "Lỗi Snapshot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string cardId = txtCardId.Text.Trim();
            string userCode = txtUserCode.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string licensePlate = txtLicensePlate.Text.Trim();
            string vehicleInfo = txtVehicleInfo.Text.Trim();
            DateTime birthDate = dtpBirthDate.Value;

            // 1. Validation
            if (string.IsNullOrEmpty(cardId))
            {
                MessageBox.Show("Mã thẻ RFID không được để trống!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCardId.Focus();
                return;
            }
            if (string.IsNullOrEmpty(userCode))
            {
                MessageBox.Show("Mã số định danh (Member ID) không được để trống!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Họ và tên không được để trống!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(licensePlate))
            {
                MessageBox.Show("Biển số xe không được để trống!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLicensePlate.Focus();
                return;
            }
            if (pbPortraitPreview.Image == null)
            {
                MessageBox.Show("Vui lòng cung cấp Ảnh Chân Dung!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pbVehiclePreview.Image == null)
            {
                MessageBox.Show("Vui lòng cung cấp Ảnh Đăng Ký Xe!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Map role group to card_type_id
            // Options: "Sinh viên" (index 0), "Giảng viên" (index 1), "Nhân viên" (index 2)
            string cardTypeId = "1";
            if (cbRoleGroup.SelectedIndex == 1) cardTypeId = "2";
            else if (cbRoleGroup.SelectedIndex == 2) cardTypeId = "3";

            DateTime expiryDate = DateTime.Today.AddYears(1);

            // 2. Compress and save images
            string portraitPath = "";
            string vehiclePath = "";
            try
            {
                portraitPath = FileStorageManager.SaveCompressedSubscriptionImage(pbPortraitPreview.Image, licensePlate, "portrait");
                vehiclePath = FileStorageManager.SaveCompressedSubscriptionImage(pbVehiclePreview.Image, licensePlate, "vehicle");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu nén ảnh thành viên: {ex.Message}", "Lỗi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. PostgreSQL Database Transaction
            try
            {
                using (var connection = new NpgsqlConnection(DatabaseHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // A. Check if the card is already in use by subscription_users
                            string checkSubQuery = "SELECT COUNT(*) FROM subscription_users WHERE card_id = @CardId";
                            using (var cmd = new NpgsqlCommand(checkSubQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CardId", cardId);
                                long existsCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
                                if (existsCount > 0)
                                {
                                    MessageBox.Show("Thẻ RFID này đã được gán cho một thành viên vé tháng khác!", "Lỗi Trùng Thẻ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            // B. Check if card exists in rfid_cards
                            string checkCardQuery = "SELECT COUNT(*) FROM rfid_cards WHERE card_id = @CardId";
                            bool cardExists = false;
                            using (var cmd = new NpgsqlCommand(checkCardQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CardId", cardId);
                                long existsCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
                                cardExists = (existsCount > 0);
                            }

                            if (cardExists)
                            {
                                // Update card status and type to monthly type
                                string updateCardQuery = @"
                                    UPDATE rfid_cards 
                                    SET card_type_id = @CardTypeId, status = 'Active', registration_date = CURRENT_DATE, expiry_date = @ExpiryDate 
                                    WHERE card_id = @CardId";
                                using (var cmd = new NpgsqlCommand(updateCardQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@CardId", cardId);
                                    cmd.Parameters.AddWithValue("@CardTypeId", cardTypeId);
                                    cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Insert card
                                string insertCardQuery = @"
                                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) 
                                    VALUES (@CardId, @CardTypeId, 'Active', CURRENT_DATE, @ExpiryDate)";
                                using (var cmd = new NpgsqlCommand(insertCardQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@CardId", cardId);
                                    cmd.Parameters.AddWithValue("@CardTypeId", cardTypeId);
                                    cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // C. Insert subscription user
                            string insertUserQuery = @"
                                INSERT INTO subscription_users (card_id, user_code, full_name, birth_date, member_image_path, vehicle_info, license_plate, vehicle_image_path) 
                                VALUES (@CardId, @UserCode, @FullName, @BirthDate, @MemberImagePath, @VehicleInfo, @LicensePlate, @VehicleImagePath)";
                            using (var cmd = new NpgsqlCommand(insertUserQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CardId", cardId);
                                cmd.Parameters.AddWithValue("@UserCode", userCode);
                                cmd.Parameters.AddWithValue("@FullName", fullName);
                                cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                                cmd.Parameters.AddWithValue("@MemberImagePath", portraitPath);
                                cmd.Parameters.AddWithValue("@VehicleInfo", string.IsNullOrEmpty(vehicleInfo) ? DBNull.Value : (object)vehicleInfo);
                                cmd.Parameters.AddWithValue("@LicensePlate", licensePlate);
                                cmd.Parameters.AddWithValue("@VehicleImagePath", vehiclePath);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show($"Đăng ký thành viên vé tháng thành công!\nBiển số: {licensePlate}\nThẻ RFID: {cardId}", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Lỗi thực thi dữ liệu: " + ex.Message, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đăng ký thất bại: {ex.Message}", "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
