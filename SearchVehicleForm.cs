using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartParking
{
    public partial class SearchVehicleForm : Form
    {
        private int _currentPage = 1;
        private readonly int _pageSize = 10;
        private int _totalRecords = 0;
        private string _initialTerm;
        private DateTimePicker dtpSearchTime = new DateTimePicker();
        private ComboBox cbTimePrecision = new ComboBox();

        public SearchVehicleForm(string initialTerm = "")
        {
            InitializeComponent();
            _initialTerm = initialTerm;

            // =========================================================================
            // 🎛️ KHU VỰC 1: ĐIỀU CHỈNH TỶ LỆ KÍCH THƯỚC CỬA SỔ THEO % MÀN HÌNH
            // =========================================================================
            double FORM_WIDTH_PCT = 0.85;   // Tỷ lệ rộng Form (0.85 = 85% màn hình)
            double FORM_HEIGHT_PCT = 0.85;  // Tỷ lệ cao Form (0.85 = 85% màn hình)
            double LEFT_PANEL_PCT = 0.45;   // Tỷ lệ cột bên trái (0.45 = 45% chiều rộng Form)

            // Tự động lấy vùng làm việc thực tế của màn hình (tránh thanh Taskbar)
            Rectangle screenArea = Screen.PrimaryScreen?.WorkingArea ?? SystemInformation.WorkingArea;

            // Áp đặt kích thước Form thích ứng động
            this.Size = new Size(
                (int)(screenArea.Width * FORM_WIDTH_PCT),
                (int)(screenArea.Height * FORM_HEIGHT_PCT)
            );

            // Áp đặt độ rộng cột bên trái co giãn tỷ lệ thuận theo Form
            this.pnlLeft.Width = (int)(this.ClientSize.Width * LEFT_PANEL_PCT);
        }

        private void SearchVehicleForm_Load(object sender, EventArgs e)
        {
            // Nạp danh sách các tiêu chí tra cứu
            cbSearchType.Items.Clear();
            cbSearchType.Items.AddRange(new object[] {
                "Biển số xe",
                "Thời gian vào",
                "Thời gian ra",
                "Mã thẻ RFID",
                "Mã sinh viên/mã giáo viên",
                "Mã thành viên"
            });
            cbSearchType.SelectedIndex = 0; // Mặc định chọn Biển số xe

            // =========================================================================
            // ⚡ THIẾT LẬP TỶ LỆ VÀNG: BUNG RỘNG LINH KIỆN & THÊM PADDING TRỰC QUAN
            // =========================================================================
            pnlSearchHeader.SuspendLayout();
            pnlSearchHeader.Height = 180; // Chiều cao thoải mái cho 3 tầng chữ lớn
            pnlSearchHeader.Controls.Clear(); // Xóa sạch cấu hình cũ tĩnh trong Designer

            // Cấu hình tiêu đề chính
            lblSearchTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            // Tính toán chiều rộng làm việc thực tế (Trừ đi khoảng đệm an toàn 40px)
            int usableWidth = pnlLeft.Width - 40;
            int labelWidth = (int)(usableWidth * 0.16); // Chiếm 16% rộng hàng
            int inputWidth = (int)(usableWidth * 0.80); // Chiếm 80% rộng hàng

            // 📊 HÀNG TẦNG 2: Nhãn từ khóa + Ô nhập liệu (Đồng bộ cao 42px)
            int row2Height = 42;

            lblSearchTerm.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblSearchTerm.Size = new Size(labelWidth, row2Height);
            lblSearchTerm.Margin = new Padding(0, 8, 10, 0);
            lblSearchTerm.ForeColor = Color.White;

            txtSearchTerm.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
            txtSearchTerm.Size = new Size(inputWidth, row2Height); // Chiếm trọn 100% cột nhập
            txtSearchTerm.Margin = new Padding(0);

            // Phân bổ động khi chọn Thời gian: Lịch chọn (54% cột) + Ô độ chính xác (45% cột) + khoảng cách (1%)
            int dtpWidth = (int)(inputWidth * 0.54);
            int precisionWidth = inputWidth - dtpWidth - 8;

            dtpSearchTime.Font = new Font("Segoe UI", 12.5F, FontStyle.Regular);
            dtpSearchTime.Format = DateTimePickerFormat.Custom;
            dtpSearchTime.CustomFormat = "dd/MM/yyyy";
            dtpSearchTime.Size = new Size(dtpWidth, row2Height);
            dtpSearchTime.Margin = new Padding(0, 0, 8, 0);
            dtpSearchTime.Visible = false;

            cbTimePrecision.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTimePrecision.Font = new Font("Segoe UI", 11.5F, FontStyle.Regular);
            cbTimePrecision.Items.Clear();
            cbTimePrecision.Items.AddRange(new object[] {
                "Chỉ ngày (dd/MM/yyyy)",
                "Ngày + Giờ (dd/MM/yyyy HH)",
                "Ngày + Giờ, Phút (dd/MM/yyyy HH:mm)",
                "Đầy đủ (dd/MM/yyyy HH:mm:ss)"
            });
            cbTimePrecision.SelectedIndex = 0;
            cbTimePrecision.Size = new Size(precisionWidth, row2Height);
            cbTimePrecision.Margin = new Padding(0);
            cbTimePrecision.Visible = false;
            cbTimePrecision.SelectedIndexChanged -= CbTimePrecision_SelectedIndexChanged;
            cbTimePrecision.SelectedIndexChanged += CbTimePrecision_SelectedIndexChanged;

            // 📊 HÀNG TẦNG 3: Tiêu chí & Cụm nút bấm lớn công nghiệp (Được tối ưu động theo % cột nhập)
            int row3Height = 42;

            lblSearchType.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblSearchType.Size = new Size(labelWidth, row3Height);
            lblSearchType.Margin = new Padding(0, 8, 10, 0);
            lblSearchType.ForeColor = Color.White;

            // Tính toán chiều rộng động cho 3 điều khiển (trừ đi 30px tổng khoảng cách ngang giữa các nút)
            int row3UsableWidth = inputWidth - 30;
            int cbSearchTypeWidth = (int)(row3UsableWidth * 0.38);  // Ô tiêu chí chiếm 38% độ rộng hàng
            int btnSearchWidth = (int)(row3UsableWidth * 0.32);     // Nút tìm kiếm chiếm 32%
            int btnFilterWidth = row3UsableWidth - cbSearchTypeWidth - btnSearchWidth; // Nút lọc chiếm 30% còn lại

            // Ô SELECT TIÊU CHÍ (Font to 12.5F giúp ô tự động giãn nở cân bằng)
            cbSearchType.Font = new Font("Segoe UI", 12.5F, FontStyle.Regular);
            cbSearchType.Size = new Size(cbSearchTypeWidth, row3Height);
            cbSearchType.Margin = new Padding(0, 0, 15, 0);

            // NÚT BẤM TÌM KIẾM (Padding lớn bên trong tạo khoảng trống cực kỳ sang trọng cho chữ)
            btnSearch.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnSearch.Size = new Size(btnSearchWidth, row3Height);
            btnSearch.Padding = new Padding(12, 0, 12, 0); // Đệm chữ cách icon và viền
            btnSearch.Margin = new Padding(0, 0, 15, 0);
            btnSearch.Text = "🔍 Tìm kiếm";

            // NÚT BẤM BỘ LỌC
            btnFilterToggle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnFilterToggle.Size = new Size(btnFilterWidth, row3Height);
            btnFilterToggle.Padding = new Padding(12, 0, 12, 0); // Đệm chữ thoáng đãng
            btnFilterToggle.Margin = new Padding(0);
            btnFilterToggle.Text = "⚙️ Bộ lọc";

            // =========================================================================
            // ⚡ DỰNG KHUNG CONTAINER TỰ ĐỘNG CÂN BẰNG KÍCH THƯỚC
            // =========================================================================
            TableLayoutPanel tlpLayout = new TableLayoutPanel();
            tlpLayout.Dock = DockStyle.Fill;
            tlpLayout.Padding = new Padding(15, 12, 15, 12);
            tlpLayout.ColumnCount = 1;
            tlpLayout.RowCount = 3;
            tlpLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tlpLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // Hàng 2 (Từ khóa)
            FlowLayoutPanel flowRow1 = new FlowLayoutPanel();
            flowRow1.Dock = DockStyle.Fill;
            flowRow1.FlowDirection = FlowDirection.LeftToRight;
            flowRow1.WrapContents = false;
            flowRow1.Margin = new Padding(0);
            flowRow1.Controls.Add(lblSearchTerm);
            flowRow1.Controls.Add(txtSearchTerm);
            flowRow1.Controls.Add(dtpSearchTime);
            flowRow1.Controls.Add(cbTimePrecision);

            // Hàng 3 (Tiêu chí + Nút bấm)
            FlowLayoutPanel flowRow2 = new FlowLayoutPanel();
            flowRow2.Dock = DockStyle.Fill;
            flowRow2.FlowDirection = FlowDirection.LeftToRight;
            flowRow2.WrapContents = false;
            flowRow2.Margin = new Padding(0);
            flowRow2.Controls.Add(lblSearchType);
            flowRow2.Controls.Add(cbSearchType);
            flowRow2.Controls.Add(btnSearch);
            flowRow2.Controls.Add(btnFilterToggle);

            tlpLayout.Controls.Add(lblSearchTitle, 0, 0);
            tlpLayout.Controls.Add(flowRow1, 0, 1);
            tlpLayout.Controls.Add(flowRow2, 0, 2);

            pnlSearchHeader.Controls.Add(tlpLayout);
            pnlSearchHeader.ResumeLayout(true);

            // Ép sắp xếp lại các Panel con phía dưới dời xuống dưới hợp lý
            pnlLeft.PerformLayout();
            // =========================================================================

            // Điền từ khóa ban đầu nếu có sẵn từ làn xe
            if (!string.IsNullOrEmpty(_initialTerm))
            {
                txtSearchTerm.Text = _initialTerm;
            }

            ExecuteSearch();
        }

        private void ExecuteSearch()
        {
            string searchType = cbSearchType.SelectedItem?.ToString() ?? "Biển số xe";
            string term = (searchType == "Thời gian vào" || searchType == "Thời gian ra")
                          ? dtpSearchTime.Value.ToString(dtpSearchTime.CustomFormat)
                          : txtSearchTerm.Text.Trim();
            bool filterMonthly = chkMonthly.Checked;
            bool filterCasual = chkCasual.Checked;
            bool filterInYard = chkInYard.Checked;
            bool filterOutYard = chkOutYard.Checked;

            int offset = (_currentPage - 1) * _pageSize;

            // Fetch data from database helper
            DataTable dt = DatabaseHelper.SearchParkingTransactions(
                term,
                searchType,
                filterMonthly,
                filterCasual,
                filterInYard,
                filterOutYard,
                _pageSize,
                offset,
                out _totalRecords
            );

            // =========================================================================
            // KHU VỰC CHỈNH SỬA: QUY HOẠCH LƯỚI TRA CỨU CHUẨN 14F & LỌC SẠCH CỘT THỪA
            // =========================================================================

            // 1. CHẶN TUYỆT ĐỐI việc tự sinh thêm cột thừa từ DataTable đổ lên
            dgvResults.AutoGenerateColumns = false;

            // Bind data to DataGridView
            dgvResults.DataSource = dt;

            try
            {
                // 2. PHÓNG TO CHỮ (CỠ 14F) & TĂNG ĐỘ CAO HÀNG ĐỂ BẢO VỆ DỄ NHÌN
                dgvResults.DefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
                dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

                // Đặt độ cao 45px thoáng đãng chống bị xén chữ
                dgvResults.ColumnHeadersHeight = 45;
                dgvResults.RowTemplate.Height = 45;

                dgvResults.DefaultCellStyle.Padding = new Padding(20, 0, 20, 0);

                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    row.Height = 45;
                }

                // 3. TỰ ĐỘNG THÊM CỘT MSV/MGV ĐỘNG NẾU TRONG THIẾT KẾ DESIGNER CHƯA CÓ
                if (!dgvResults.Columns.Contains("colUserCode"))
                {
                    DataGridViewTextBoxColumn colUserCode = new DataGridViewTextBoxColumn();
                    colUserCode.Name = "colUserCode";
                    colUserCode.DataPropertyName = "user_code"; // Ánh xạ chuẩn trường dữ liệu
                    colUserCode.HeaderText = "MSV/MGV";
                    dgvResults.Columns.Add(colUserCode);
                }

                // 4. DUYỆT QUA TOÀN BỘ CỘT ĐỂ LỌC BỎ CỘT THỪA (CHỈ GIỮ LẠI 5 THÔNG TIN YÊU CẦU)
                foreach (DataGridViewColumn col in dgvResults.Columns)
                {
                    // Lấy tên trường dữ liệu ánh xạ để đối chiếu chính xác
                    string propName = col.DataPropertyName?.ToLower() ?? "";

                    if (propName == "card_id" || propName == "user_code" || propName == "status" || propName == "entry_time" || propName == "exit_time")
                    {
                        col.Visible = true; // Hiện cột hợp lệ

                        // Chuẩn hóa lại tiêu đề tiếng Việt có dấu ngăn nắp
                        if (propName == "card_id") col.HeaderText = "Mã thẻ RFID";
                        else if (propName == "user_code") col.HeaderText = "MSV/MGV";
                        else if (propName == "status") col.HeaderText = "Trạng thái";
                        else if (propName == "entry_time")
                        {
                            col.HeaderText = "Thời gian vào";
                            col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"; // Ép hiển thị kiểu 24h
                        }
                        else if (propName == "exit_time")
                        {
                            col.HeaderText = "Thời gian ra";
                            col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"; // Ép hiển thị kiểu 24h
                        }
                    }
                    else
                    {
                        // Ẩn toàn bộ các cột thừa thãi khác (Loại vé cũ, Biển số cũ, Đường dẫn ảnh...)
                        col.Visible = false;
                    }
                }

                // 5. Bật thanh cuộn và tự động giãn rộng cột theo độ dài thực tế của chữ (Không bao giờ bị xén thành ...)
                dgvResults.ScrollBars = ScrollBars.Both;
                dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception)
            {
                // Bỏ qua lỗi vẽ lưới cục bộ nếu dữ liệu trống
            }
            // =========================================================================

            // Update pagination UI
            UpdatePaginationUI();
        }

        private void UpdatePaginationUI()
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (totalPages < 1) totalPages = 1;

            lblPageInfo.Text = $"Trang {_currentPage} / {totalPages} (Tổng: {_totalRecords})";

            btnPrev.Enabled = (_currentPage > 1);
            btnNext.Enabled = (_currentPage < totalPages);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            ExecuteSearch();
        }

        private void TxtSearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _currentPage = 1;
                ExecuteSearch();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BtnFilterToggle_Click(object sender, EventArgs e)
        {
            // 1. Bật/tắt trạng thái ẩn hiện của ô lọc
            pnlFilterDropdown.Visible = !pnlFilterDropdown.Visible;

            if (pnlFilterDropdown.Visible)
            {
                // 2. Đồng bộ phóng to chữ & kích thước ô chọn cho đẹp với giao diện mới
                pnlFilterDropdown.Size = new Size(180, 150); // Nới rộng nhẹ bảng lựa chọn
                chkMonthly.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                chkCasual.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                chkInYard.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                chkOutYard.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

                chkMonthly.Location = new Point(12, 10);
                chkCasual.Location = new Point(12, 44);
                chkInYard.Location = new Point(12, 78);
                chkOutYard.Location = new Point(12, 112);

                // 3. CHIÊU THỨC QUYẾT ĐỊNH: Dịch chuyển tọa độ tuyệt đối theo nút bấm
                // Lấy tọa độ thực tế của nút Bộ lọc trên màn hình
                Point btnScreenPos = btnFilterToggle.Parent!.PointToScreen(btnFilterToggle.Location);

                // Quy đổi tọa độ đó về tương thích với Panel trái chứa ô chọn
                Point btnRelativePos = pnlLeft.PointToClient(btnScreenPos);

                // Ép ô chọn căn lề phải thẳng hàng với nút, nằm ngay bên dưới nút (cách 2px)
                pnlFilterDropdown.Location = new Point(
                    btnRelativePos.X + btnFilterToggle.Width - pnlFilterDropdown.Width,
                    btnRelativePos.Y + btnFilterToggle.Height + 2
                );

                // Đưa ô chọn lên lớp trên cùng để không bị che khuất[cite: 1]
                pnlFilterDropdown.BringToFront();
            }
        }

        private void FilterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Trigger search reload when checkbox state changes
            _currentPage = 1;
            ExecuteSearch();
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ExecuteSearch();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage < totalPages)
            {
                _currentPage++;
                ExecuteSearch();
            }
        }

        private void CbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cbSearchType.SelectedItem?.ToString() ?? "";

            // Nếu bảo vệ chọn các tiêu chí liên quan đến mốc thời gian xe ra/vào bãi
            if (selectedType == "Thời gian vào" || selectedType == "Thời gian ra")
            {
                txtSearchTerm.Visible = false;    // Ẩn thanh nhập text thường đi
                dtpSearchTime.Visible = true;     // Hiện bộ chọn ngày giờ trực quan
                cbTimePrecision.Visible = true;   // Hiện ô chọn độ chính xác
            }
            else
            {
                // Ngược lại, nếu tìm theo Biển số, Mã thẻ... thì khôi phục lại thanh gõ chữ
                txtSearchTerm.Visible = true;
                dtpSearchTime.Visible = false;
                cbTimePrecision.Visible = false;
                txtSearchTerm.PlaceholderText = "Nhập từ khóa cần tìm kiếm...";
            }
        }

        private void DgvResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow gridRow = dgvResults.Rows[e.RowIndex];
                DataRowView? rowView = gridRow.DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row;

                // 1. Process Monthly Card Demographics Panel
                string memberId = row["member_id"]?.ToString() ?? "";
                bool isMonthly = !string.IsNullOrEmpty(memberId);

                if (isMonthly)
                {
                    pnlMemberDemographics.Visible = true;
                    lblMemberId.Text = $"Mã thành viên: {memberId}";
                    lblUserCode.Text = $"Mã số: {row["user_code"]}";
                    lblFullName.Text = $"Họ và tên: {row["full_name"]}";
                    lblVehicleInfo.Text = $"Thông tin xe: {row["vehicle_info"]}";
                    lblLicensePlate.Text = $"Biển số đăng ký: {row["license_plate"]}";

                    // Load subscriber portrait safely from file path or fallback
                    string licensePlate = row["license_plate"]?.ToString() ?? "";
                    string memberImagePath = row["member_image_path"]?.ToString() ?? "";
                    Image? portrait = null;

                    if (!string.IsNullOrEmpty(memberImagePath))
                    {
                        portrait = FileStorageManager.LoadCompressedImage(memberImagePath);
                    }
                    if (portrait == null && !string.IsNullOrEmpty(licensePlate))
                    {
                        portrait = FileStorageManager.LoadCompressedSubscriptionImage(licensePlate, "portrait");
                    }

                    SafeSetImage(pbMemberPortrait, portrait);
                }
                else
                {
                    pnlMemberDemographics.Visible = false;
                    SafeSetImage(pbMemberPortrait, null);
                }

                // 2. Process Transaction Images Visualizer
                string status = row["status"]?.ToString() ?? "Trong bãi";
                string entryImgPath = row["entry_image_path"]?.ToString() ?? "";
                string exitImgPath = row["exit_image_path"]?.ToString() ?? "";

                // Parse Entry images path (separated by ;)
                string entryFrontFile = "";
                string entryRearFile = "";
                if (!string.IsNullOrEmpty(entryImgPath))
                {
                    string[] parts = entryImgPath.Split(';');
                    entryFrontFile = parts[0];
                    if (parts.Length > 1) entryRearFile = parts[1];
                }

                // Parse Exit images path (separated by ;)
                string exitFrontFile = "";
                string exitRearFile = "";
                if (!string.IsNullOrEmpty(exitImgPath))
                {
                    string[] parts = exitImgPath.Split(';');
                    exitFrontFile = parts[0];
                    if (parts.Length > 1) exitRearFile = parts[1];
                }

                // Load Entry images
                Image? entryFront = FileStorageManager.LoadCompressedImage(entryFrontFile);
                Image? entryRear = FileStorageManager.LoadCompressedImage(entryRearFile);
                SafeSetImage(pbEntryFront, entryFront);
                SafeSetImage(pbEntryRear, entryRear);

                // Check active vs historic transaction rules
                if (status == "Trong bãi")
                {
                    // Exit lane picture boxes must remain completely blank
                    SafeSetImage(pbExitFront, null);
                    SafeSetImage(pbExitRear, null);
                }
                else
                {
                    // Load and decompress Exit images
                    Image? exitFront = FileStorageManager.LoadCompressedImage(exitFrontFile);
                    Image? exitRear = FileStorageManager.LoadCompressedImage(exitRearFile);
                    SafeSetImage(pbExitFront, exitFront);
                    SafeSetImage(pbExitRear, exitRear);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết giao dịch: {ex.Message}", "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SafeSetImage(PictureBox pb, Image? img)
        {
            if (pb.Image != null)
            {
                Image oldImg = pb.Image;
                pb.Image = null;
                oldImg.Dispose(); // Prevent RAM leaks
            }
            pb.Image = img;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Final disposal of all loaded images on close to ensure perfect memory integrity
            SafeSetImage(pbMemberPortrait, null);
            SafeSetImage(pbEntryFront, null);
            SafeSetImage(pbEntryRear, null);
            SafeSetImage(pbExitFront, null);
            SafeSetImage(pbExitRear, null);
        }
        private void CbTimePrecision_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Tự động thay đổi cách hiển thị và cách nhập dựa trên menu chọn của bảo vệ
            switch (cbTimePrecision.SelectedIndex)
            {
                case 0: // Chỉ chọn ngày
                    dtpSearchTime.CustomFormat = "dd/MM/yyyy";
                    dtpSearchTime.ShowUpDown = false; // Hiện thanh lịch thả xuống
                    break;
                case 1: // Ngày + Giờ
                    dtpSearchTime.CustomFormat = "dd/MM/yyyy HH";
                    dtpSearchTime.ShowUpDown = false;  // Hiện nút tăng giảm mũi tên cho dễ bấm giờ
                    break;
                case 2: // Ngày + Giờ + Phút
                    dtpSearchTime.CustomFormat = "dd/MM/yyyy HH:mm";
                    dtpSearchTime.ShowUpDown = false;
                    break;
                case 3: // Đầy đủ
                    dtpSearchTime.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                    dtpSearchTime.ShowUpDown = false;
                    break;
            }
        }
    }
}
