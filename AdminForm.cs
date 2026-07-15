using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartParking
{
    public partial class AdminForm : Form
    {
        private readonly Form _loginForm;
        private bool _isLoggingOut = false;

        // Tracks selected row primary key metadata for auto-increment & composite partitions
        private object? _selectedRowId = null;
        private object? _originalExitTime = null;
        private object? _originalKey = null;

        // Sidebar Navigation controls
        private Panel? pnlSidebar;
        private Button? btnNavCamera;
        private Button? btnNavData;
        private Button? btnNavRetention;

        // Search, Pagination & RFID Controls
        private TextBox? txtSearch;
        private Label lblCamType = new Label();
        private ComboBox cbCamType = new ComboBox();
        private Label lblCustomSuffix = new Label();
        private TextBox txtCustomSuffix = new TextBox();
        private Label? lblPageInfo;

        // Programmatic 4-camera settings controls
        private GroupBox? grpCamEntryFront;
        private GroupBox? grpCamEntryRear;
        private GroupBox? grpCamExitFront;
        private GroupBox? grpCamExitRear;

        private TextBox txtEntryFrontIP = new TextBox();
        private TextBox txtEntryFrontPort = new TextBox();
        private TextBox txtEntryFrontUser = new TextBox();
        private TextBox txtEntryFrontPass = new TextBox();

        private TextBox txtEntryRearIP = new TextBox();
        private TextBox txtEntryRearPort = new TextBox();
        private TextBox txtEntryRearUser = new TextBox();
        private TextBox txtEntryRearPass = new TextBox();

        private TextBox txtExitFrontIP = new TextBox();
        private TextBox txtExitFrontPort = new TextBox();
        private TextBox txtExitFrontUser = new TextBox();
        private TextBox txtExitFrontPass = new TextBox();

        private TextBox txtExitRearIP = new TextBox();
        private TextBox txtExitRearPort = new TextBox();
        private TextBox txtExitRearUser = new TextBox();
        private TextBox txtExitRearPass = new TextBox();

        private Label lblVal5 = new Label();
        private Label lblVal6 = new Label();
        private Label lblVal7 = new Label();
        private Label lblVal8 = new Label();
        private TextBox txtVal5 = new TextBox();
        private TextBox txtVal6 = new TextBox();
        private TextBox txtVal7 = new TextBox();
        private TextBox txtVal8 = new TextBox();

        private Label lblMemberRole = new Label();
        private ComboBox cbMemberRole = new ComboBox();
        private Label lblVehicleImg = new Label();
        private TextBox txtVehicleImg = new TextBox();
        private int _currentPage = 1;
        private const int PageSize = 15;
        private int _totalPages = 1;
        private int _totalRows = 0;
        private readonly RFIDReader _rfidReader = new RFIDReader();

        public AdminForm(Form loginForm)
        {
            InitializeComponent();
            _loginForm = loginForm;
        }

        #region Xử lý Sự kiện Tải Form & Đóng Form
        private void AdminForm_Load(object sender, EventArgs e)
        {
            // Tải cấu hình camera đã lưu trong Database
            LoadCameraSettings();

            // Cấu hình danh mục bảng điều khiển CRUD với toàn bộ 7 bảng
            cbTables.Items.Add("Users");
            cbTables.Items.Add("ActiveParking");
            cbTables.Items.Add("CardTypes");
            cbTables.Items.Add("RFIDCards");
            cbTables.Items.Add("SubscriptionUsers");
            cbTables.Items.Add("ParkingHistory");
            cbTables.Items.Add("Settings");

            cbTables.SelectedIndex = 0; // Mặc định bảng Users

            // Cấu hình tab Lưu trữ / Retention động
            SetupRetentionTab();

            // Cấu hình Hệ thống Sidebar & Giao diện Responsive
            SetupResponsiveLayout();

            // Đăng ký sự kiện quẹt thẻ tự động điền (RFID Auto-fill)
            try
            {
                _rfidReader.CardSwiped += RFIDReader_CardSwiped;
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    _rfidReader.Start(ports[0], 9600);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing RFID port in Admin: {ex.Message}");
            }
        }

        private void SetupResponsiveLayout()
        {
            // 1. Ẩn thanh tab bar ngang của tabControlAdmin
            tabControlAdmin.Appearance = TabAppearance.FlatButtons;
            tabControlAdmin.ItemSize = new Size(0, 1);
            tabControlAdmin.SizeMode = TabSizeMode.Fixed;

            // 2. Định vị lại tiêu đề header gốc
            lblHeaderTitle.Location = new Point(15, 15);

            // 3. Tạo Sidebar bên trái cố định
            pnlSidebar = new Panel();
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 260;
            pnlSidebar.BackColor = Color.FromArgb(30, 41, 59); // Slate-800
            pnlSidebar.Padding = new Padding(0, 10, 0, 0);

            // FlowLayoutPanel xếp chồng các nút chức năng
            FlowLayoutPanel flowSidebar = new FlowLayoutPanel();
            flowSidebar.Dock = DockStyle.Fill;
            flowSidebar.FlowDirection = FlowDirection.TopDown;
            flowSidebar.WrapContents = false;
            flowSidebar.Padding = new Padding(0);
            pnlSidebar.Controls.Add(flowSidebar);

            // Tạo các nút điều hướng sidebar
            btnNavCamera = CreateSidebarButton("📷  Cấu Hình Camera", 0);
            btnNavData = CreateSidebarButton("🗄️  Quản Trị Dữ Liệu", 1);
            btnNavRetention = CreateSidebarButton("💾  Quản Lý Lưu Trữ", 2);

            flowSidebar.Controls.Add(btnNavCamera);
            flowSidebar.Controls.Add(btnNavData);
            flowSidebar.Controls.Add(btnNavRetention);

            // Thêm Sidebar vào Form và thiết lập Z-order chuẩn
            this.Controls.Add(pnlSidebar);

            // Sắp xếp từ ngoài vào trong để WinForms chia khung Docking chính xác
            pnlHeader.BringToFront();       // Đưa thanh tiêu đề lên trước (chiếm trọn chiều ngang Top)
            pnlSidebar.BringToFront();      // Đưa sidebar lên trước (chiếm chiều dọc bên trái phần còn lại)
            tabControlAdmin.BringToFront(); // Đưa tab nội dung lên TRÊN CÙNG để tự động điền đầy vùng trống (Fill)

            // Làm nổi bật nút đầu tiên
            HighlightNavButton(0);

            // 4. Cấu hình responsive cho các Tab nội dung
            ConfigureCameraTabResponsive();
            ConfigureDataTabResponsive();
        }

        private Button CreateSidebarButton(string text, int tabIndex)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Width = 260;
            btn.Height = 55;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.ForeColor = Color.FromArgb(226, 232, 240); // Slate-200
            btn.BackColor = Color.Transparent;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105); // Slate-600
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Margin = new Padding(0, 5, 0, 5);

            btn.Click += (s, e) =>
            {
                tabControlAdmin.SelectedIndex = tabIndex;
                HighlightNavButton(tabIndex);
            };

            return btn;
        }

        private void HighlightNavButton(int activeIndex)
        {
            Color activeColor = Color.FromArgb(37, 99, 235); // Blue-600
            Color inactiveColor = Color.Transparent;

            if (btnNavCamera != null) btnNavCamera.BackColor = activeIndex == 0 ? activeColor : inactiveColor;
            if (btnNavData != null) btnNavData.BackColor = activeIndex == 1 ? activeColor : inactiveColor;
            if (btnNavRetention != null) btnNavRetention.BackColor = activeIndex == 2 ? activeColor : inactiveColor;
        }

        private void UpdateCameraLayout(Panel pnlBottomCamera)
        {
            int totalWidth = lblCamType.Width + 10 + cbCamType.Width;
            if (txtCustomSuffix.Visible)
            {
                totalWidth += 30 + lblCustomSuffix.Width + 10 + txtCustomSuffix.Width;
            }

            int startX = (pnlBottomCamera.Width - totalWidth) / 2;
            int y = 20;

            lblCamType.Location = new Point(startX, y + 4);
            cbCamType.Location = new Point(lblCamType.Right + 10, y);

            if (txtCustomSuffix.Visible)
            {
                lblCustomSuffix.Location = new Point(cbCamType.Right + 30, y + 4);
                txtCustomSuffix.Location = new Point(lblCustomSuffix.Right + 10, y);
            }

            btnSaveConfig.Location = new Point((pnlBottomCamera.Width - btnSaveConfig.Width) / 2, 70);
        }

        private GroupBox CreateCameraGroupBox(string title, TextBox txtIP, TextBox txtPort, TextBox txtUser, TextBox txtPass)
        {
            GroupBox grp = new GroupBox();
            grp.Text = title;
            grp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grp.ForeColor = Color.FromArgb(15, 23, 42); // Slate-900
            grp.BackColor = Color.White;
            grp.Dock = DockStyle.Fill;
            grp.Margin = new Padding(8);

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 4;
            layout.RowCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35f));

            Label lblIP = new Label { Text = "IP:", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(71, 85, 105), Margin = new Padding(2, 2, 2, 0) };
            Label lblPort = new Label { Text = "Cổng:", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(71, 85, 105), Margin = new Padding(2, 2, 2, 0) };
            Label lblUser = new Label { Text = "User:", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(71, 85, 105), Margin = new Padding(2, 2, 2, 0) };
            Label lblPass = new Label { Text = "Pass:", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(71, 85, 105), Margin = new Padding(2, 2, 2, 0) };

            txtIP.Font = new Font("Segoe UI", 9.5F);
            txtIP.Dock = DockStyle.Fill;
            txtPort.Font = new Font("Segoe UI", 9.5F);
            txtPort.Dock = DockStyle.Fill;
            txtUser.Font = new Font("Segoe UI", 9.5F);
            txtUser.Dock = DockStyle.Fill;
            txtPass.Font = new Font("Segoe UI", 9.5F);
            txtPass.Dock = DockStyle.Fill;
            txtPass.UseSystemPasswordChar = true;

            layout.Controls.Add(lblIP, 0, 0);
            layout.Controls.Add(lblPort, 1, 0);
            layout.Controls.Add(lblUser, 2, 0);
            layout.Controls.Add(lblPass, 3, 0);

            layout.Controls.Add(txtIP, 0, 1);
            layout.Controls.Add(txtPort, 1, 1);
            layout.Controls.Add(txtUser, 2, 1);
            layout.Controls.Add(txtPass, 3, 1);

            grp.Controls.Add(layout);
            return grp;
        }

        private void ConfigureCameraTabResponsive()
        {
            // Bố cục lưới 2x2 cho 4 camera
            TableLayoutPanel tblCamera = new TableLayoutPanel();
            tblCamera.ColumnCount = 2;
            tblCamera.RowCount = 2;
            tblCamera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tblCamera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tblCamera.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            tblCamera.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            tblCamera.Dock = DockStyle.Top;
            tblCamera.Height = 350;
            tblCamera.BackColor = Color.Transparent;

            // Đưa groupbox camera vào khung 2x2
            grpCamEntryFront = CreateCameraGroupBox("Camera Trước (Làn vào)", txtEntryFrontIP, txtEntryFrontPort, txtEntryFrontUser, txtEntryFrontPass);
            grpCamEntryRear = CreateCameraGroupBox("Camera Sau (Làn vào)", txtEntryRearIP, txtEntryRearPort, txtEntryRearUser, txtEntryRearPass);
            grpCamExitFront = CreateCameraGroupBox("Camera Trước (Làn ra)", txtExitFrontIP, txtExitFrontPort, txtExitFrontUser, txtExitFrontPass);
            grpCamExitRear = CreateCameraGroupBox("Camera Sau (Làn ra)", txtExitRearIP, txtExitRearPort, txtExitRearUser, txtExitRearPass);

            tblCamera.Controls.Add(grpCamEntryFront, 0, 0);
            tblCamera.Controls.Add(grpCamEntryRear, 1, 0);
            tblCamera.Controls.Add(grpCamExitFront, 0, 1);
            tblCamera.Controls.Add(grpCamExitRear, 1, 1);

            // Căn giữa nút SaveConfig
            Panel pnlBottomCamera = new Panel();
            pnlBottomCamera.Dock = DockStyle.Fill;
            pnlBottomCamera.BackColor = Color.Transparent;

            lblCamType.Text = "Hãng Camera:";
            lblCamType.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCamType.ForeColor = Color.FromArgb(15, 23, 42);
            lblCamType.AutoSize = true;

            cbCamType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCamType.Font = new Font("Segoe UI", 11F);
            cbCamType.Width = 200;
            cbCamType.Items.Clear();
            cbCamType.Items.AddRange(new string[]
            {
                "Hikvision / HiLook",
                "Tapo (TP-Link)",
                "Dahua / Imou",
                "Ezviz",
                "Tùy chỉnh (Custom URL)"
            });

            lblCustomSuffix.Text = "Suffix Tùy Chỉnh:";
            lblCustomSuffix.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCustomSuffix.ForeColor = Color.FromArgb(15, 23, 42);
            lblCustomSuffix.AutoSize = true;
            lblCustomSuffix.Visible = false;

            txtCustomSuffix.Font = new Font("Segoe UI", 11F);
            txtCustomSuffix.Width = 200;
            txtCustomSuffix.PlaceholderText = "Ví dụ: /live/ch0";
            txtCustomSuffix.Visible = false;

            lblCamType.Parent = pnlBottomCamera;
            cbCamType.Parent = pnlBottomCamera;
            lblCustomSuffix.Parent = pnlBottomCamera;
            txtCustomSuffix.Parent = pnlBottomCamera;

            cbCamType.SelectedIndexChanged += (s, e) =>
            {
                bool isCustom = cbCamType.SelectedItem?.ToString() == "Tùy chỉnh (Custom URL)";
                lblCustomSuffix.Visible = isCustom;
                txtCustomSuffix.Visible = isCustom;
                UpdateCameraLayout(pnlBottomCamera);
            };

            pnlBottomCamera.SizeChanged += (s, e) =>
            {
                UpdateCameraLayout(pnlBottomCamera);
            };

            btnSaveConfig.Parent = pnlBottomCamera;
            btnSaveConfig.Anchor = AnchorStyles.None;

            tabCamera.Controls.Clear();
            tabCamera.Controls.Add(pnlBottomCamera);
            tabCamera.Controls.Add(tblCamera);

            if (cbCamType.SelectedIndex == -1)
            {
                cbCamType.SelectedIndex = 0;
            }
        }

        private void ConfigureDataTabResponsive()
        {
            // 1. Panel điều khiển lọc dữ liệu hàng đầu (Giữ nguyên khung chứa)
            Panel pnlTopControls = new Panel();
            pnlTopControls.Dock = DockStyle.Top;
            pnlTopControls.Height = 55;
            pnlTopControls.BackColor = Color.Transparent;

            // 2. GIẢI PHÁP MỚI: Tạo FlowLayoutPanel để tự xếp hàng các điều khiển từ trái qua phải
            FlowLayoutPanel flowLeftControls = new FlowLayoutPanel();
            flowLeftControls.Dock = DockStyle.Fill;
            flowLeftControls.FlowDirection = FlowDirection.LeftToRight;
            flowLeftControls.WrapContents = false; // Không cho phép tự xuống dòng
            flowLeftControls.Padding = new Padding(15, 13, 0, 0); // Căn lề trên để các ô nằm chính giữa thanh thanh panel
            pnlTopControls.Controls.Add(flowLeftControls);

            // 3. Đặt nút Reset DB cố định ở góc bên phải panel chính
            btnResetDb.Parent = pnlTopControls;
            btnResetDb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnResetDb.Location = new Point(tabData.Width - btnResetDb.Width - 30, 10);
            btnResetDb.BringToFront(); // Đảm bảo nút nằm trên cùng không bị che

            // 4. Thêm các thành phần vào FlowLayoutPanel (WinForms sẽ tự căn khoảng cách không lo đè nhau)
            label9.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label9.Margin = new Padding(0, 4, 8, 0); // Khoảng cách xung quanh nhãn (Top, Left, Right, Bottom)
            flowLeftControls.Controls.Add(label9);

            cbTables.Font = new Font("Segoe UI", 11F);
            cbTables.Margin = new Padding(0, 0, 30, 0); // Đẩy khoảng cách bên phải ra 30px để thoáng giao diện
            flowLeftControls.Controls.Add(cbTables);

            // Bổ sung các điều khiển tìm kiếm vào thanh luồng (Flow)
            Label lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSearch.Margin = new Padding(0, 4, 8, 0);
            flowLeftControls.Controls.Add(lblSearch);

            txtSearch = new TextBox();
            txtSearch.Name = "txtSearch";
            txtSearch.Width = 180;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Margin = new Padding(0, 0, 10, 0);
            flowLeftControls.Controls.Add(txtSearch);

            Button btnSearch = new Button();
            btnSearch.Text = "TÌM KIẾM";
            btnSearch.Width = 120;
            btnSearch.Height = 40;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.BackColor = Color.FromArgb(79, 70, 229); // Indigo-600
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Margin = new Padding(0, 0, 0, 0);
            flowLeftControls.Controls.Add(btnSearch);

            // Đăng ký sự kiện tìm kiếm (Giữ nguyên logic nghiệp vụ của bạn)
            btnSearch.Click += (s, ev) => { _currentPage = 1; RefreshGrid(); };
            txtSearch.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    _currentPage = 1;
                    RefreshGrid();
                    ev.SuppressKeyPress = true;
                }
            };

            // ====================================================================================
            // THAY THẾ ĐOẠN CẤU HÌNH PANEL PHÂN TRANG (PAGINATION) BẰNG ĐOẠN GIẢI PHÁP MỚI DƯỚI ĐÂY:
            // ====================================================================================

            // 1. Khởi tạo pnlPagination dưới dạng TableLayoutPanel để chia lưới căn giữa tuyệt đối
            TableLayoutPanel pnlPagination = new TableLayoutPanel();
            pnlPagination.Dock = DockStyle.Bottom;
            pnlPagination.Height = 52; // Tăng lên 52px giúp không gian rộng rãi, hoàn toàn không bị lưới che khuất
            pnlPagination.BackColor = Color.FromArgb(241, 245, 249); // Slate-100

            // Thiết lập lưới 3 cột: Cột trái (50%), Cột giữa chứa nút (Tự động co giãn), Cột phải (50%)
            pnlPagination.ColumnCount = 3;
            pnlPagination.RowCount = 1;
            pnlPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            pnlPagination.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            pnlPagination.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            // 2. Tạo một FlowLayoutPanel nội bộ để xếp hàng ngang cụm nút bấm ngay chính giữa lưới
            FlowLayoutPanel flowPaginationInner = new FlowLayoutPanel();
            flowPaginationInner.FlowDirection = FlowDirection.LeftToRight;
            flowPaginationInner.WrapContents = false;
            flowPaginationInner.AutoSize = true;
            flowPaginationInner.BackColor = Color.Transparent;
            flowPaginationInner.Anchor = AnchorStyles.None; // Ép cụm nội dung căn giữa cả dọc lẫn ngang trong ô lưới
            flowPaginationInner.Margin = new Padding(0);

            // Nút Trước
            Button btnPrev = new Button();
            btnPrev.Text = "◀ TRƯỚC";
            btnPrev.Width = 120;
            btnPrev.Height = 40; // Đặt chiều cao 32px cho vừa vặn thanh hiển thị
            btnPrev.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPrev.BackColor = Color.FromArgb(71, 85, 105); // Slate-600
            btnPrev.ForeColor = Color.White;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.Margin = new Padding(0, 0, 10, 0); // Khoảng cách bên phải nút Trước là 10px
            btnPrev.Click += (s, ev) => { if (_currentPage > 1) { _currentPage--; RefreshGrid(); } };

            // Nút Sau
            Button btnNext = new Button();
            btnNext.Text = "SAU ▶";
            btnNext.Width = 120;
            btnNext.Height = 40;
            btnNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNext.BackColor = Color.FromArgb(71, 85, 105); // Slate-600
            btnNext.ForeColor = Color.White;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Cursor = Cursors.Hand;
            btnNext.Margin = new Padding(0, 0, 20, 0); // Tạo khoảng cách 20px tới nhãn thông tin trang
            btnNext.Click += (s, ev) => { if (_currentPage < _totalPages) { _currentPage++; RefreshGrid(); } };

            // Nhãn thông tin Trang
            lblPageInfo = new Label();
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Text = "Trang 1 / 1 (Tổng số dòng: 0)";
            lblPageInfo.AutoSize = true;
            lblPageInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPageInfo.ForeColor = Color.FromArgb(15, 23, 42); // Slate-900
            lblPageInfo.Margin = new Padding(0, 5, 0, 0); // Đẩy nhẹ lề trên 5px để chữ thẳng hàng hoàn hảo với nút bấm

            // Thêm các thành phần vào container luồng nội bộ
            flowPaginationInner.Controls.Add(btnPrev);
            flowPaginationInner.Controls.Add(btnNext);
            flowPaginationInner.Controls.Add(lblPageInfo);

            // Đưa cụm điều hướng vào ô thứ 2 (Cột giữa) của TableLayoutPanel phân trang
            pnlPagination.Controls.Add(flowPaginationInner, 1, 0);

            // ====================================================================================

            // ====================================================================================
            // BIỂU MẪU CRUD RESPONSIVE NÂNG CẤP LƯỚI 3 TẦNG (HỖ TRỢ THÊM Ô CHỌN SELECT BOX)
            // ====================================================================================

            grpCrudActions.Dock = DockStyle.Bottom;
            grpCrudActions.Height = 350; // Tăng chiều cao lên 310px để chứa đủ 3 tầng ô dữ liệu thoải mái
            grpCrudActions.Controls.Clear();

            // 1. Tạo TableLayoutPanel chia lưới nhập liệu
            TableLayoutPanel tblCrudInputs = new TableLayoutPanel();
            tblCrudInputs.Dock = DockStyle.Top;
            tblCrudInputs.Height = 240; // Nới không gian chiều cao lưới lên 240px
            tblCrudInputs.Padding = new Padding(10, 5, 10, 0);
            tblCrudInputs.BackColor = Color.Transparent;

            // Thiết lập 4 cột bằng nhau (Mỗi cột chiếm 25% chiều rộng)
            tblCrudInputs.ColumnCount = 4;
            tblCrudInputs.ColumnStyles.Clear();
            tblCrudInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tblCrudInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tblCrudInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tblCrudInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            // Cấu hình 6 hàng: Gồm 3 cặp (Nhãn AutoSize + Ô nhập Cố định 44px)
            tblCrudInputs.RowCount = 6;
            tblCrudInputs.RowStyles.Clear();
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Hàng 0: Nhãn tầng 1
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f)); // Hàng 1: Ô nhập tầng 1
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Hàng 2: Nhãn tầng 2
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f)); // Hàng 3: Ô nhập tầng 2
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Hàng 4: Nhãn tầng 3
            tblCrudInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f)); // Hàng 5: Ô nhập tầng 3

            // Vòng lặp định dạng đồng bộ các ô nhập dữ liệu text truyền thống
            Label[] labels = { lblVal1, lblVal2, lblVal3, lblVal4, lblVal5, lblVal6, lblVal7, lblVal8 };
            TextBox[] textBoxes = { txtVal1, txtVal2, txtVal3, txtVal4, txtVal5, txtVal6, txtVal7, txtVal8 };
            Padding cellMargin = new Padding(6, 0, 6, 12);

            for (int i = 0; i < 8; i++)
            {
                labels[i].Parent = tblCrudInputs;
                labels[i].Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                labels[i].ForeColor = Color.FromArgb(71, 85, 105);
                labels[i].AutoSize = true;
                labels[i].Margin = new Padding(6, 4, 6, 2);

                textBoxes[i].Parent = tblCrudInputs;
                textBoxes[i].Font = new Font("Segoe UI", 11F);
                textBoxes[i].Dock = DockStyle.Fill;
                textBoxes[i].Margin = cellMargin;
            }

            // ĐỊNH DẠNG RIÊNG CHO Ô SELECT BOX CHỌN NHÓM ĐỐI TƯỢNG (MỚI)
            lblMemberRole.Parent = tblCrudInputs;
            lblMemberRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMemberRole.ForeColor = Color.FromArgb(71, 85, 105);
            lblMemberRole.AutoSize = true;
            lblMemberRole.Margin = new Padding(6, 4, 6, 2);

            cbMemberRole.Parent = tblCrudInputs;
            cbMemberRole.Font = new Font("Segoe UI", 11F);
            cbMemberRole.Dock = DockStyle.Fill;
            cbMemberRole.Margin = cellMargin;
            cbMemberRole.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cbMemberRole.Items.Count == 0)
            {
                cbMemberRole.Items.AddRange(new string[] { "Student", "Staff", "Lecturer" });
                cbMemberRole.SelectedIndex = 0;
            }

            // ĐỊNH DẠNG Ô NHẬP ĐƯỜNG DẪN ẢNH XE ĐĂNG KÝ VÉ THÁNG
            lblVehicleImg.Parent = tblCrudInputs;
            lblVehicleImg.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblVehicleImg.ForeColor = Color.FromArgb(71, 85, 105);
            lblVehicleImg.AutoSize = true;
            lblVehicleImg.Margin = new Padding(6, 4, 6, 2);

            txtVehicleImg.Parent = tblCrudInputs;
            txtVehicleImg.Font = new Font("Segoe UI", 11F);
            txtVehicleImg.Dock = DockStyle.Fill;
            txtVehicleImg.Margin = cellMargin;

            // Đưa liên kết điều khiển vào Tầng 3 - Cột số 1 (Nằm cạnh ô Chọn nhóm đối tượng)
            tblCrudInputs.Controls.Add(lblVehicleImg, 1, 4);
            tblCrudInputs.Controls.Add(txtVehicleImg, 1, 5);

            // Sắp xếp các control hiện tại vào Tầng 1 và Tầng 2 của lưới
            tblCrudInputs.Controls.Add(lblVal1, 0, 0); tblCrudInputs.Controls.Add(lblVal2, 1, 0);
            tblCrudInputs.Controls.Add(lblVal3, 2, 0); tblCrudInputs.Controls.Add(lblVal4, 3, 0);
            tblCrudInputs.Controls.Add(txtVal1, 0, 1); tblCrudInputs.Controls.Add(txtVal2, 1, 1);
            tblCrudInputs.Controls.Add(txtVal3, 2, 1); tblCrudInputs.Controls.Add(txtVal4, 3, 1);

            tblCrudInputs.Controls.Add(lblVal5, 0, 2); tblCrudInputs.Controls.Add(lblVal6, 1, 2);
            tblCrudInputs.Controls.Add(lblVal7, 2, 2); tblCrudInputs.Controls.Add(lblVal8, 3, 2);
            tblCrudInputs.Controls.Add(txtVal5, 0, 3); tblCrudInputs.Controls.Add(txtVal6, 1, 3);
            tblCrudInputs.Controls.Add(txtVal7, 2, 3); tblCrudInputs.Controls.Add(txtVal8, 3, 3);

            // Sắp xếp Select Box chọn nhóm đối tượng xuống Tầng 3 (Cột 0)
            tblCrudInputs.Controls.Add(lblMemberRole, 0, 4);
            tblCrudInputs.Controls.Add(cbMemberRole, 0, 5);

            // 2. Panel chứa cụm nút bấm hành động (Giữ nguyên kích thước to 140x42px của bạn)
            FlowLayoutPanel flowCrudButtons = new FlowLayoutPanel();
            flowCrudButtons.Dock = DockStyle.Bottom;
            flowCrudButtons.Height = 55;
            flowCrudButtons.FlowDirection = FlowDirection.LeftToRight;
            flowCrudButtons.Padding = new Padding(16, 2, 0, 0);
            flowCrudButtons.BackColor = Color.Transparent;

            btnAdd.Parent = flowCrudButtons; btnEdit.Parent = flowCrudButtons; btnDelete.Parent = flowCrudButtons;
            flowCrudButtons.Controls.Add(btnAdd); flowCrudButtons.Controls.Add(btnEdit); flowCrudButtons.Controls.Add(btnDelete);
            btnAdd.Margin = new Padding(0, 0, 12, 0);
            btnEdit.Margin = new Padding(0, 0, 12, 0);

            grpCrudActions.Controls.Add(tblCrudInputs);
            grpCrudActions.Controls.Add(flowCrudButtons);

            // ====================================================================================
            // CẤU HÌNH TĂNG ĐỘ CAO HÀNG VÀ CHỮ CHO BẢNG DỮ LIỆU (DATAGRIDVIEW)
            // ====================================================================================
            dgvAdminData.Dock = DockStyle.Fill;

            // 1. Tăng chiều cao của các hàng dữ liệu lên 38px (Mặc định hệ thống rất nhỏ chỉ 25px)
            dgvAdminData.RowTemplate.Height = 38;

            // 2. Cấu hình tăng độ cao cho thanh tiêu đề (Header) cột lên 42px để cân xứng
            dgvAdminData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAdminData.ColumnHeadersHeight = 42;

            // 3. Căn chữ nằm chính giữa theo chiều dọc (Vertical Alignment) chống bị lệch lề
            dgvAdminData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAdminData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 4. Tăng nhẹ cỡ chữ hiển thị bên trong bảng để giao diện trông rõ ràng, cao cấp hơn
            dgvAdminData.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dgvAdminData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            // ====================================================================================

            // Đổ dữ liệu và sắp xếp thứ tự hiển thị chuẩn của Tab
            dgvAdminData.Dock = DockStyle.Fill;
            tabData.Controls.Clear();
            tabData.Controls.Add(dgvAdminData);
            tabData.Controls.Add(pnlTopControls);
            tabData.Controls.Add(pnlPagination);
            tabData.Controls.Add(grpCrudActions);

            grpCrudActions.BringToFront();
            pnlPagination.BringToFront();
            pnlTopControls.BringToFront();
            dgvAdminData.BringToFront();
        }

        private void SetupRetentionTab()
        {
            TabPage tabRetention = new TabPage("Quản Lý Lưu Trữ");
            tabRetention.BackColor = Color.FromArgb(248, 250, 252); // Nền xám nhẹ Slate-50 hiện đại
            tabRetention.Padding = new Padding(30);

            // 1. Khởi tạo GroupBox làm khung bo viền màu trắng tinh tế
            GroupBox grpRetention = new GroupBox();
            grpRetention.Text = "CẤU HÌNH LƯU TRỮ & DỌN DẸP TỰ ĐỘNG";
            grpRetention.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            grpRetention.ForeColor = Color.FromArgb(15, 23, 42); // Slate-900
            grpRetention.BackColor = Color.White;
            grpRetention.Size = new Size(580, 270); // Nới rộng nhẹ diện tích khung chứa
            grpRetention.Location = new Point(30, 30);

            // GIẢI PHÁP CHÍNH: Tạo FlowLayoutPanel hướng dọc xếp chồng các hàng tự động
            FlowLayoutPanel flowMain = new FlowLayoutPanel();
            flowMain.Dock = DockStyle.Fill;
            flowMain.FlowDirection = FlowDirection.TopDown;
            flowMain.WrapContents = false; // Không cho phép tự tràn ngang
            flowMain.Padding = new Padding(25, 25, 25, 20); // Đệm khoảng cách lọt lòng đều đặn
            grpRetention.Controls.Add(flowMain);

            // 2. Nhãn chữ hướng dẫn (Bỏ ký tự xuống dòng cứng '\n' để WinForms tự ngắt dòng tự nhiên)
            Label lblRetentionInfo = new Label();
            lblRetentionInfo.Text = "Hệ thống tự động giải phóng các phân vùng cơ sở dữ liệu cũ và tệp tin ảnh vượt quá khoảng thời gian thiết lập dưới đây:";
            lblRetentionInfo.Width = 520;
            lblRetentionInfo.Height = 50; // Diện tích dọc thoải mái cho 2 hàng chữ hiển thị
            lblRetentionInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            lblRetentionInfo.ForeColor = Color.FromArgb(100, 116, 139); // Màu Slate-500 mềm mại
            lblRetentionInfo.Margin = new Padding(0, 0, 0, 18); // Ép khoảng lề dưới 18px để tạo khoảng cách an toàn với hàng nhập liệu
            flowMain.Controls.Add(lblRetentionInfo);

            // 3. Hàng luồng ngang chứa cụm: Tiêu đề ô nhập + Ô chọn số + Chữ đơn vị "tháng"
            FlowLayoutPanel flowRowInput = new FlowLayoutPanel();
            flowRowInput.FlowDirection = FlowDirection.LeftToRight;
            flowRowInput.WrapContents = false;
            flowRowInput.Width = 520;
            flowRowInput.Height = 38;
            flowRowInput.Margin = new Padding(0, 0, 0, 22); // Tạo khoảng cách 22px an toàn với nút bấm hành động ở dưới

            Label lblMonths = new Label();
            lblMonths.Text = "Thời gian lưu trữ tối đa:";
            lblMonths.AutoSize = true;
            lblMonths.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMonths.ForeColor = Color.FromArgb(71, 85, 105); // Slate-600
            lblMonths.Margin = new Padding(0, 4, 8, 0); // Đẩy nhẹ lề trên 4px để chữ nằm thẳng hàng dọc hoàn hảo với ô số

            NumericUpDown numRetention = new NumericUpDown();
            numRetention.Name = "numRetention";
            numRetention.Minimum = 1;
            numRetention.Maximum = 120;
            numRetention.Value = Convert.ToDecimal(DatabaseHelper.GetSetting("HistoryRetentionMonths", "3"));
            numRetention.Size = new Size(90, 29);
            numRetention.Font = new Font("Segoe UI", 11F);
            numRetention.Margin = new Padding(0, 0, 8, 0);

            Label lblUnit = new Label();
            lblUnit.Text = "tháng";
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblUnit.ForeColor = Color.FromArgb(71, 85, 105);
            lblUnit.Margin = new Padding(0, 4, 0, 0); // Thẳng hàng ngang với ô số

            flowRowInput.Controls.Add(lblMonths);
            flowRowInput.Controls.Add(numRetention);
            flowRowInput.Controls.Add(lblUnit);
            flowMain.Controls.Add(flowRowInput);

            // 4. Nút bấm thực thi hành động lưu cấu hình
            Button btnSaveRetention = new Button();
            btnSaveRetention.Text = "LƯU CẤU HÌNH & DỌN DẸP NGAY";
            btnSaveRetention.Size = new Size(340, 45); // Tăng chiều rộng lên 340px để hiển thị đầy đủ chữ không lo bị xén mép
            btnSaveRetention.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSaveRetention.BackColor = Color.FromArgb(22, 163, 74); // Màu xanh lá cây Emerald-600
            btnSaveRetention.ForeColor = Color.White;
            btnSaveRetention.FlatStyle = FlatStyle.Flat;
            btnSaveRetention.FlatAppearance.BorderSize = 0;
            btnSaveRetention.Cursor = Cursors.Hand;
            btnSaveRetention.Margin = new Padding(0, 0, 0, 0);

            // Đăng ký nghiệp vụ lưu dữ liệu của bạn
            btnSaveRetention.Click += (s, ev) =>
            {
                int months = (int)numRetention.Value;
                DatabaseHelper.SaveSetting("HistoryRetentionMonths", months.ToString());

                try
                {
                    DatabaseHelper.CleanupOldPartitions(months);
                    FileStorageManager.CleanupOldTransactions(months);
                    MessageBox.Show($"Cấu hình đã lưu thành công! Đã chạy tiến trình dọn dẹp dữ liệu cũ hơn {months} tháng.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dọn dẹp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            flowMain.Controls.Add(btnSaveRetention);

            // Đưa cấu trúc hoàn chỉnh vào Tab
            tabRetention.Controls.Add(grpRetention);
            tabControlAdmin.TabPages.Add(tabRetention);
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _rfidReader.Stop();
            }
            catch { }

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

        #region Quản lý Cấu hình Camera Hikvision (RTSP)
        private void LoadCameraSettings()
        {
            if (cbCamType.Items.Count == 0)
            {
                cbCamType.Items.AddRange(new string[]
                {
                    "Hikvision / HiLook",
                    "Tapo (TP-Link)",
                    "Dahua / Imou",
                    "Ezviz",
                    "Tùy chỉnh (Custom URL)"
                });
            }

            // Tải dữ liệu từ Database đổ trực tiếp vào 4 cụm điều khiển giao diện mới
            txtEntryFrontIP.Text = DatabaseHelper.GetSetting("CamEntryFrontIP", "111.222.333.444");
            txtEntryFrontPort.Text = DatabaseHelper.GetSetting("CamEntryFrontPort", "554");
            txtEntryFrontUser.Text = DatabaseHelper.GetSetting("CamEntryFrontUser", "admin");
            txtEntryFrontPass.Text = DatabaseHelper.GetSetting("CamEntryFrontPass", "password");

            txtEntryRearIP.Text = DatabaseHelper.GetSetting("CamEntryRearIP", "111.222.333.444");
            txtEntryRearPort.Text = DatabaseHelper.GetSetting("CamEntryRearPort", "554");
            txtEntryRearUser.Text = DatabaseHelper.GetSetting("CamEntryRearUser", "admin");
            txtEntryRearPass.Text = DatabaseHelper.GetSetting("CamEntryRearPass", "password");

            txtExitFrontIP.Text = DatabaseHelper.GetSetting("CamExitFrontIP", "111.222.333.444");
            txtExitFrontPort.Text = DatabaseHelper.GetSetting("CamExitFrontPort", "554");
            txtExitFrontUser.Text = DatabaseHelper.GetSetting("CamExitFrontUser", "admin");
            txtExitFrontPass.Text = DatabaseHelper.GetSetting("CamExitFrontPass", "password");

            txtExitRearIP.Text = DatabaseHelper.GetSetting("CamExitRearIP", "111.222.333.444");
            txtExitRearPort.Text = DatabaseHelper.GetSetting("CamExitRearPort", "554");
            txtExitRearUser.Text = DatabaseHelper.GetSetting("CamExitRearUser", "admin");
            txtExitRearPass.Text = DatabaseHelper.GetSetting("CamExitRearPass", "password");

            // ĐÃ DỌN SẠCH CÁC DÒNG GÁN THỪA TXTFRONTIP / TXTREARIP LỖI THỜI TẠI ĐÂY

            string camType = DatabaseHelper.GetSetting("CamType", "Hikvision / HiLook");
            if (cbCamType.Items.Contains(camType))
            {
                cbCamType.SelectedItem = camType;
            }
            else
            {
                cbCamType.SelectedIndex = 0;
            }

            txtCustomSuffix.Text = DatabaseHelper.GetSetting("CamCustomSuffix", "");
            bool isCustom = cbCamType.SelectedItem?.ToString() == "Tùy chỉnh (Custom URL)";
            lblCustomSuffix.Visible = isCustom;
            txtCustomSuffix.Visible = isCustom;
        }

        private void BtnSaveConfig_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEntryFrontIP.Text) ||
                string.IsNullOrWhiteSpace(txtEntryRearIP.Text) ||
                string.IsNullOrWhiteSpace(txtExitFrontIP.Text) ||
                string.IsNullOrWhiteSpace(txtExitRearIP.Text))
            {
                MessageBox.Show("Địa chỉ IP camera không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Lưu thông số kết nối cơ bản của 4 Camera vào bảng Settings
            DatabaseHelper.SaveSetting("CamEntryFrontIP", txtEntryFrontIP.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryFrontPort", txtEntryFrontPort.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryFrontUser", txtEntryFrontUser.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryFrontPass", txtEntryFrontPass.Text);

            DatabaseHelper.SaveSetting("CamEntryRearIP", txtEntryRearIP.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryRearPort", txtEntryRearPort.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryRearUser", txtEntryRearUser.Text.Trim());
            DatabaseHelper.SaveSetting("CamEntryRearPass", txtEntryRearPass.Text);

            DatabaseHelper.SaveSetting("CamExitFrontIP", txtExitFrontIP.Text.Trim());
            DatabaseHelper.SaveSetting("CamExitFrontPort", txtExitFrontPort.Text.Trim());
            DatabaseHelper.SaveSetting("CamExitFrontUser", txtExitFrontUser.Text.Trim());
            DatabaseHelper.SaveSetting("CamExitFrontPass", txtExitFrontPass.Text);

            DatabaseHelper.SaveSetting("CamExitRearIP", txtExitRearIP.Text.Trim());
            // FIX LỖI 1: Thay thế txtExitRearIP thành txtExitRearPort để lưu đúng cổng 554
            DatabaseHelper.SaveSetting("CamExitRearPort", txtExitRearPort.Text.Trim());
            DatabaseHelper.SaveSetting("CamExitRearUser", txtExitRearUser.Text.Trim());
            DatabaseHelper.SaveSetting("CamExitRearPass", txtExitRearPass.Text);

            string selectedValue = cbCamType.SelectedItem?.ToString() ?? "Hikvision / HiLook";
            DatabaseHelper.SaveSetting("CamType", selectedValue);

            string suffixEntryFront = ""; string suffixEntryRear = "";
            string suffixExitFront = ""; string suffixExitRear = "";

            switch (selectedValue)
            {
                case "Hikvision / HiLook":
                    suffixEntryFront = "/Streaming/Channels/101";
                    suffixEntryRear = "/Streaming/Channels/102";
                    suffixExitFront = "/Streaming/Channels/201";
                    suffixExitRear = "/Streaming/Channels/202";
                    break;
                case "Tapo (TP-Link)":
                    suffixEntryFront = "/stream1";
                    suffixEntryRear = "/stream1";
                    suffixExitFront = "/stream1";
                    suffixExitRear = "/stream1";
                    break;
                case "Dahua / Imou":
                    suffixEntryFront = "/cam/realmonitor?channel=1&subtype=0";
                    suffixEntryRear = "/cam/realmonitor?channel=2&subtype=0";
                    suffixExitFront = "/cam/realmonitor?channel=3&subtype=0";
                    suffixExitRear = "/cam/realmonitor?channel=4&subtype=0";
                    break;
                case "Ezviz":
                    suffixEntryFront = "/h264/ch1/main/av_stream"; suffixEntryRear = "/h264/ch1/main/av_stream";
                    suffixExitFront = "/h264/ch1/main/av_stream"; suffixExitRear = "/h264/ch1/main/av_stream";
                    break;
                case "Tùy chỉnh (Custom URL)":
                    // FIX LỖI 3: Hỗ trợ phân tách dấu phẩy để chạy trộn lẫn nhiều hãng Cam (Ví dụ: Hik,Hik,Hik,Tapo)
                    string customSuffix = txtCustomSuffix.Text.Trim();
                    if (string.IsNullOrEmpty(customSuffix)) customSuffix = "/stream1";
                    DatabaseHelper.SaveSetting("CamCustomSuffix", customSuffix);

                    string[] parts = customSuffix.Split(',');
                    if (parts.Length == 4)
                    {
                        suffixEntryFront = parts[0].Trim();
                        suffixEntryRear = parts[1].Trim();
                        suffixExitFront = parts[2].Trim();
                        suffixExitRear = parts[3].Trim();
                    }
                    else
                    {
                        suffixEntryFront = customSuffix; suffixEntryRear = customSuffix;
                        suffixExitFront = customSuffix; suffixExitRear = customSuffix;
                    }
                    break;
                default:
                    suffixEntryFront = "/Streaming/Channels/101"; suffixEntryRear = "/Streaming/Channels/102";
                    suffixExitFront = "/Streaming/Channels/201"; suffixExitRear = "/Streaming/Channels/202";
                    break;
            }

            // Biên dịch chuỗi kết nối RTSP chuẩn hóa cho hệ thống vận hành
            string rtspEntryFront = "rtsp://" + txtEntryFrontUser.Text.Trim() + ":" + txtEntryFrontPass.Text + "@" + txtEntryFrontIP.Text.Trim() + ":" + txtEntryFrontPort.Text.Trim() + suffixEntryFront;
            string rtspEntryRear = "rtsp://" + txtEntryRearUser.Text.Trim() + ":" + txtEntryRearPass.Text + "@" + txtEntryRearIP.Text.Trim() + ":" + txtEntryRearPort.Text.Trim() + suffixEntryRear;
            string rtspExitFront = "rtsp://" + txtExitFrontUser.Text.Trim() + ":" + txtExitFrontPass.Text + "@" + txtExitFrontIP.Text.Trim() + ":" + txtExitFrontPort.Text.Trim() + suffixExitFront;
            string rtspExitRear = "rtsp://" + txtExitRearUser.Text.Trim() + ":" + txtExitRearPass.Text + "@" + txtExitRearIP.Text.Trim() + ":" + txtExitRearPort.Text.Trim() + suffixExitRear;

            DatabaseHelper.SaveSetting("CamEntryFrontUrl", rtspEntryFront);
            DatabaseHelper.SaveSetting("CamEntryRearUrl", rtspEntryRear);
            DatabaseHelper.SaveSetting("CamExitFrontUrl", rtspExitFront);
            DatabaseHelper.SaveSetting("CamExitRearUrl", rtspExitRear);

            // FIX LỖI 2: Loại bỏ tận gốc 2 dòng lưu đè CamTruocUrl và CamSauUrl thừa cũ,
            // Thực hiện lệnh SQL xóa trực tiếp chúng khỏi cơ sở dữ liệu để dọn sạch bảng hiển thị.
            string deleteLegacyQuery = "DELETE FROM settings WHERE setting_key IN ('CamTruocUrl', 'CamSauUrl')";
            DatabaseHelper.ExecuteNonQuery(deleteLegacyQuery, null);

            MessageBox.Show($"Lưu cấu hình bãi xe thành công!\n\n" +
                            $"Link Cam Vào Trước: {rtspEntryFront}\n" +
                            $"Link Cam Vào Sau: {rtspEntryRear}\n" +
                            $"Link Cam Ra Trước: {rtspExitFront}\n" +
                            $"Link Cam Ra Sau: {rtspExitRear}", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RefreshGrid(); // Làm tươi lại lưới hiển thị
        }
        #endregion

        #region Quản lý Cơ sở dữ liệu (Tab CRUD)
        private string GetDbTableName(string displayTable)
        {
            switch (displayTable)
            {
                case "Users": return "users";
                case "ActiveParking": return "active_parking";
                case "CardTypes": return "card_types";
                case "RFIDCards": return "rfid_cards";
                case "SubscriptionUsers": return "subscription_users";
                case "ParkingHistory": return "parking_history";
                case "Settings": return "settings";
                default: return displayTable.ToLower();
            }
        }

        private void CbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            if (txtSearch != null) txtSearch.Clear();

            string table = cbTables.SelectedItem?.ToString() ?? "";
            ClearCrudInputs();

            // Mặc định bật hiển thị toàn bộ 8 ô, bảng nào thừa ô hệ thống sẽ tự động ẩn đi ở dưới
            Label[] labels = { lblVal1, lblVal2, lblVal3, lblVal4, lblVal5, lblVal6, lblVal7, lblVal8 };
            TextBox[] textBoxes = { txtVal1, txtVal2, txtVal3, txtVal4, txtVal5, txtVal6, txtVal7, txtVal8 };
            for (int i = 0; i < 8; i++) { labels[i].Visible = true; textBoxes[i].Visible = true; textBoxes[i].PlaceholderText = ""; }

            lblMemberRole.Visible = false;
            cbMemberRole.Visible = false;

            lblVehicleImg.Visible = false;
            txtVehicleImg.Visible = false;

            if (table == "Users")
            {
                lblVal1.Text = "Tên đăng nhập: *";
                lblVal2.Text = "Mật khẩu: *";
                lblVal3.Text = "Vai trò: *";

                // Ẩn các ô từ 4 đến 8
                for (int i = 3; i < 8; i++) { labels[i].Visible = false; textBoxes[i].Visible = false; }
            }
            else if (table == "ActiveParking")
            {
                lblVal1.Text = "Mã Thẻ: *";
                lblVal2.Text = "Thời gian vào: *"; txtVal2.PlaceholderText = "dd/MM/yyyy HH:mm:ss hoặc để trống";
                lblVal3.Text = "Đường dẫn ảnh:";
                lblVal4.Text = "Cổng vào:";

                for (int i = 4; i < 8; i++) { labels[i].Visible = false; textBoxes[i].Visible = false; }
            }
            else if (table == "CardTypes")
            {
                lblVal1.Text = "Mã loại thẻ: *";
                lblVal2.Text = "Tên loại thẻ: *";
                lblVal3.Text = "Phí mặc định: *";

                for (int i = 3; i < 8; i++) { labels[i].Visible = false; textBoxes[i].Visible = false; }
            }
            else if (table == "RFIDCards")
            {
                lblVal1.Text = "Mã thẻ RFID: *";
                lblVal2.Text = "Mã loại thẻ: *";
                lblVal3.Text = "Trạng thái: *";
                lblVal4.Text = "Ngày đăng ký: *"; txtVal4.PlaceholderText = "dd/MM/yyyy";
                lblVal5.Text = "Ngày hết hạn: *"; txtVal5.PlaceholderText = "dd/MM/yyyy";

                for (int i = 5; i < 8; i++) { labels[i].Visible = false; textBoxes[i].Visible = false; }
            }
            else if (table == "SubscriptionUsers")
            {
                lblVal1.Text = "Mã thành viên: (Tự sinh)";
                txtVal1.PlaceholderText = "Hệ thống tự cấp mã dạng MB00000x...";

                lblVal2.Text = "Mã thẻ RFID: *";
                lblVal3.Text = "Mã số định danh: *";
                lblVal4.Text = "Họ và tên: *";
                lblVal5.Text = "Ngày sinh: *"; txtVal5.PlaceholderText = "dd/MM/yyyy";

                lblVal6.Text = "Tên tệp ảnh đại diện: *";
                txtVal6.PlaceholderText = "Ví dụ: avatar1.jpg (Không điền đường dẫn)";

                lblVal7.Text = "Thông tin xe (màu, hiệu xe):";
                lblVal8.Text = "Biển số xe: *";

                // KÍCH HOẠT THÊM Ô ẢNH XE ĐĂNG KÝ
                lblVehicleImg.Text = "Tên tệp ảnh xe: *";
                txtVehicleImg.PlaceholderText = "Ví dụ: car1.jpg (Không điền đường dẫn)";
                lblVehicleImg.Visible = true;
                txtVehicleImg.Visible = true;

                lblMemberRole.Text = "Nhóm đối tượng (Role): *";
                lblMemberRole.Visible = true;
                cbMemberRole.Visible = true;
            }
            else if (table == "ParkingHistory")
            {
                lblVal1.Text = "Mã giao dịch: (Tự tăng)"; txtVal1.PlaceholderText = "Không cần nhập khi thêm mới";
                lblVal2.Text = "Mã thẻ RFID: *";
                lblVal3.Text = "Thời gian vào: *"; txtVal3.PlaceholderText = "dd/MM/yyyy HH:mm:ss";
                lblVal4.Text = "Đường dẫn ảnh vào:";
                lblVal5.Text = "Thời gian ra: *"; txtVal5.PlaceholderText = "dd/MM/yyyy HH:mm:ss";
                lblVal6.Text = "Đường dẫn ảnh ra:";
                lblVal7.Text = "Phí đã thu: *";
                lblVal8.Text = "Cổng ra: *";
            }
            else if (table == "Settings")
            {
                lblVal1.Text = "Khóa cấu hình: *";
                lblVal2.Text = "Giá trị cấu hình: *";

                for (int i = 2; i < 8; i++) { labels[i].Visible = false; textBoxes[i].Visible = false; }
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                string table = cbTables.SelectedItem?.ToString() ?? "";
                string dbTable = GetDbTableName(table);
                string searchTerm = txtSearch != null ? txtSearch.Text.Trim() : "";

                var result = DatabaseHelper.GetPaginatedTableData(dbTable, searchTerm, PageSize, (_currentPage - 1) * PageSize);

                dgvAdminData.DataSource = result.Data;
                _totalRows = result.TotalCount;
                _totalPages = (int)Math.Ceiling((double)_totalRows / PageSize);
                if (_totalPages < 1) _totalPages = 1;

                if (_currentPage > _totalPages)
                {
                    _currentPage = _totalPages;
                    result = DatabaseHelper.GetPaginatedTableData(dbTable, searchTerm, PageSize, (_currentPage - 1) * PageSize);
                    dgvAdminData.DataSource = result.Data;
                }

                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages} (Tổng số dòng: {_totalRows})";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RFIDReader_CardSwiped(string cardId)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(RFIDReader_CardSwiped), cardId);
                return;
            }

            string table = cbTables.SelectedItem?.ToString() ?? "";
            if (table == "RFIDCards")
            {
                txtVal1.Text = cardId;
            }
            else if (table == "SubscriptionUsers")
            {
                txtVal2.Text = cardId;
            }
        }

        private void DgvAdminData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAdminData.Rows[e.RowIndex];
                string table = cbTables.SelectedItem?.ToString() ?? "";

                _selectedRowId = null;
                _originalExitTime = null;
                _originalKey = null;

                if (table == "Users")
                {
                    string username = (row.Cells["username"]?.Value ?? row.Cells["Username"]?.Value)?.ToString() ?? "";
                    _originalKey = username;
                    txtVal1.Text = username;
                    txtVal2.Text = (row.Cells["password"]?.Value ?? row.Cells["Password"]?.Value)?.ToString() ?? "";
                    txtVal3.Text = (row.Cells["role"]?.Value ?? row.Cells["Role"]?.Value)?.ToString() ?? "";
                    txtVal4.Text = "";
                }
                else if (table == "ActiveParking")
                {
                    string cardId = (row.Cells["card_id"]?.Value ?? row.Cells["CardID"]?.Value)?.ToString() ?? "";
                    _originalKey = cardId;
                    txtVal1.Text = cardId;
                    txtVal2.Text = (row.Cells["entry_time"]?.Value ?? row.Cells["EntryTime"]?.Value)?.ToString() ?? "";
                    txtVal3.Text = (row.Cells["entry_image_path"]?.Value ?? row.Cells["FrontImagePath"]?.Value)?.ToString() ?? "";
                    txtVal4.Text = (row.Cells["entry_gate"]?.Value ?? row.Cells["RearImagePath"]?.Value)?.ToString() ?? "";
                }
                else if (table == "CardTypes")
                {
                    string id = (row.Cells["card_type_id"]?.Value ?? row.Cells[0].Value)?.ToString() ?? "";
                    _originalKey = id;
                    txtVal1.Text = id;
                    txtVal2.Text = (row.Cells["card_type_name"]?.Value ?? row.Cells[1].Value)?.ToString() ?? "";
                    txtVal3.Text = (row.Cells["default_fee"]?.Value ?? row.Cells[2].Value)?.ToString() ?? "";
                    txtVal4.Text = "";
                }
                else if (table == "RFIDCards")
                {
                    string cardId = (row.Cells["card_id"]?.Value ?? row.Cells[0].Value)?.ToString() ?? "";
                    _originalKey = cardId;
                    txtVal1.Text = cardId;
                    txtVal2.Text = (row.Cells["card_type_id"]?.Value ?? row.Cells[1].Value)?.ToString() ?? "";
                    txtVal3.Text = (row.Cells["status"]?.Value ?? row.Cells[2].Value)?.ToString() ?? "";

                    // Đổ thêm trường ngày đăng ký và hết hạn chuẩn chỉ
                    if (row.Cells["registration_date"]?.Value is DateTime regDate) txtVal4.Text = regDate.ToString("dd/MM/yyyy");
                    else txtVal4.Text = row.Cells["registration_date"]?.Value?.ToString() ?? "";

                    if (row.Cells["expiry_date"]?.Value is DateTime expDate) txtVal5.Text = expDate.ToString("dd/MM/yyyy");
                    else txtVal5.Text = row.Cells["expiry_date"]?.Value?.ToString() ?? "";
                }
                else if (table == "SubscriptionUsers")
                {
                    string memberId = (row.Cells["member_id"]?.Value ?? row.Cells[0].Value)?.ToString() ?? "";
                    _originalKey = memberId;
                    txtVal1.Text = memberId;
                    txtVal2.Text = (row.Cells["card_id"]?.Value ?? row.Cells[1].Value)?.ToString() ?? "";
                    txtVal3.Text = (row.Cells["user_code"]?.Value ?? row.Cells[2].Value)?.ToString() ?? "";
                    txtVal4.Text = (row.Cells["full_name"]?.Value ?? row.Cells[3].Value)?.ToString() ?? "";

                    if (row.Cells["birth_date"]?.Value is DateTime bDate) txtVal5.Text = bDate.ToString("dd/MM/yyyy");
                    else txtVal5.Text = row.Cells["birth_date"]?.Value?.ToString() ?? "";

                    // XỬ LÝ ĐƯỜNG DẪN ẢNH ĐỘNG KHI CLICK CHỌN DÒNG
                    string fullImagePath = (row.Cells["member_image_path"]?.Value ?? row.Cells[5].Value)?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(fullImagePath) && fullImagePath != "[NO_IMAGE_FALLBACK]" && fullImagePath != "[NO_IMAGE]")
                    {
                        // 1. Chỉ hiển thị tên file gọn gàng lên ô gõ dữ liệu
                        txtVal6.Text = "portrait.bin";
                    }
                    else
                    {
                        txtVal6.Text = "";
                    }

                    // 2. Tự chọn mục ComboBox thích hợp dựa trên tiền tố của mã số định danh (user_code)
                    if (txtVal3.Text.StartsWith("SV", StringComparison.OrdinalIgnoreCase))
                        cbMemberRole.SelectedItem = "Student";
                    else if (txtVal3.Text.StartsWith("CB", StringComparison.OrdinalIgnoreCase))
                        cbMemberRole.SelectedItem = "Staff";
                    else
                        cbMemberRole.SelectedItem = "Lecturer";

                    txtVal7.Text = (row.Cells["vehicle_info"]?.Value ?? row.Cells[6].Value)?.ToString() ?? "";
                    txtVal8.Text = (row.Cells["license_plate"]?.Value ?? row.Cells[7].Value)?.ToString() ?? "";

                    // XỬ LÝ ĐƯỜNG DẪN ẢNH XE ĐỘNG KHI CLICK CHỌN DÒNG
                    string fullVehicleImgPath = (row.Cells["vehicle_image_path"]?.Value ?? row.Cells[8].Value)?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(fullVehicleImgPath) && fullVehicleImgPath != "[NO_IMAGE_FALLBACK]")
                    {
                        txtVehicleImg.Text = "vehicle.bin";
                    }
                    else
                    {
                        txtVehicleImg.Text = "";
                    }
                }
                else if (table == "ParkingHistory")
                {
                    _selectedRowId = row.Cells["transaction_id"]?.Value ?? row.Cells[0].Value;
                    _originalExitTime = row.Cells["exit_time"]?.Value ?? row.Cells[4].Value;

                    txtVal1.Text = _selectedRowId?.ToString() ?? "";
                    txtVal2.Text = (row.Cells["card_id"]?.Value ?? row.Cells[1].Value)?.ToString() ?? "";

                    if (row.Cells["entry_time"]?.Value is DateTime enTime) txtVal3.Text = enTime.ToString("dd/MM/yyyy HH:mm:ss");
                    else txtVal3.Text = row.Cells["entry_time"]?.Value?.ToString() ?? "";

                    txtVal4.Text = (row.Cells["entry_image_path"]?.Value ?? row.Cells[3].Value)?.ToString() ?? "";

                    if (_originalExitTime is DateTime exTime) txtVal5.Text = exTime.ToString("dd/MM/yyyy HH:mm:ss");
                    else txtVal5.Text = _originalExitTime?.ToString() ?? "";

                    txtVal6.Text = (row.Cells["exit_image_path"]?.Value ?? row.Cells[5].Value)?.ToString() ?? "";
                    txtVal7.Text = (row.Cells["fee_collected"]?.Value ?? row.Cells[6].Value)?.ToString() ?? "";
                    txtVal8.Text = (row.Cells["exit_gate"]?.Value ?? row.Cells[7].Value)?.ToString() ?? "";
                }
                else if (table == "Settings")
                {
                    string key = (row.Cells["setting_key"]?.Value ?? row.Cells[0].Value)?.ToString() ?? "";
                    _originalKey = key;
                    txtVal1.Text = key;
                    txtVal2.Text = (row.Cells["setting_value"]?.Value ?? row.Cells[1].Value)?.ToString() ?? "";
                    txtVal3.Text = "";
                    txtVal4.Text = "";
                }
            }
        }

        private void ClearCrudInputs()
        {
            txtVal1.Clear(); txtVal2.Clear(); txtVal3.Clear(); txtVal4.Clear();
            txtVal5.Clear(); txtVal6.Clear(); txtVal7.Clear(); txtVal8.Clear();
            txtVehicleImg.Clear();
            if (cbMemberRole.Items.Count > 0) cbMemberRole.SelectedIndex = 0; // THÊM DÒNG NÀY
            _selectedRowId = null;
            _originalExitTime = null;
            _originalKey = null;
        }

        private bool HasReference(string query, string paramName, object paramValue)
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(DatabaseHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue(paramName, paramValue);
                        long count = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking reference integrity: {ex.Message}");
                return false;
            }
        }

        private bool RecordExists(string table, string column, object value)
        {
            string query = $"SELECT COUNT(1) FROM {table} WHERE {column} = @val";
            return HasReference(query, "val", value);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string table = cbTables.SelectedItem?.ToString() ?? "";
            string v1 = txtVal1.Text.Trim();
            string v2 = txtVal2.Text.Trim();
            string v3 = txtVal3.Text.Trim();
            string v4 = txtVal4.Text.Trim();
            string v5 = txtVal5.Text.Trim();
            string v6 = txtVal6.Text.Trim();
            string v7 = txtVal7.Text.Trim();
            string v8 = txtVal8.Text.Trim();

            // =========================================================================
            // CHỈNH SỬA 1: Không bắt buộc nhập ô số 1 (v1) nếu bảng tự sinh ID (SubscriptionUsers & ParkingHistory)
            // =========================================================================
            bool requiresV1 = (table != "SubscriptionUsers" && table != "ParkingHistory");
            if ((requiresV1 && string.IsNullOrEmpty(v1)) || string.IsNullOrEmpty(v2))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc (*)", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // THÊM MẢNG ĐỊNH DẠNG CHUẨN NÀY ĐỂ HỆ THỐNG ĐỐI CHIẾU
            string[] formats = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss" };

            try
            {
                if (table == "Users")
                {
                    if (RecordExists("users", "username", v1))
                    {
                        MessageBox.Show("Tên đăng nhập này đã tồn tại!", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DatabaseHelper.InsertUser(v1, v2, v3);
                }
                else if (table == "ActiveParking")
                {
                    if (!RecordExists("rfid_cards", "card_id", v1))
                    {
                        MessageBox.Show("Mã thẻ RFID này chưa tồn tại trong hệ thống (rfid_cards)!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (RecordExists("active_parking", "card_id", v1))
                    {
                        MessageBox.Show("Thẻ này hiện đang nằm trong bãi xe!", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DateTime entryTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(v2) && !DateTime.TryParseExact(v2, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out entryTime))
                    {
                        MessageBox.Show("Thời gian vào không hợp lệ! Hãy nhập đúng định dạng ngày tháng.", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string gate = string.IsNullOrEmpty(v4) ? "Cổng số 1" : v4;
                    string query = "INSERT INTO active_parking (card_id, entry_time, entry_image_path, entry_gate) VALUES (@v1, @v2, @v3, @v4)";
                    var p = new Dictionary<string, object> {
                        { "v1", v1 },
                        { "v2", entryTime },
                        { "v3", v3 },
                        { "v4", gate }
                    };
                    DatabaseHelper.ExecuteNonQuery(query, p);
                }
                else if (table == "CardTypes")
                {
                    if (RecordExists("card_types", "card_type_id", v1))
                    {
                        MessageBox.Show("Mã loại thẻ này đã tồn tại!", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal fee = 0;
                    if (!string.IsNullOrEmpty(v3) && !decimal.TryParse(v3, out fee))
                    {
                        MessageBox.Show("Phí mặc định phải là số hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string q = "INSERT INTO card_types (card_type_id, card_type_name, default_fee) VALUES (@v1, @v2, @v3)";
                    var p = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 }, { "v3", fee } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "RFIDCards")
                {
                    if (RecordExists("rfid_cards", "card_id", v1))
                    {
                        MessageBox.Show("Mã thẻ RFID này đã tồn tại!", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!RecordExists("card_types", "card_type_id", v2))
                    {
                        MessageBox.Show("Mã loại thẻ chưa tồn tại trong danh mục loại thẻ (card_types)!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime regDate = DateTime.Today;
                    DateTime expiry = DateTime.Today.AddYears(1);
                    DateTime.TryParseExact(v4, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out regDate);
                    DateTime.TryParseExact(v5, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expiry);

                    string q = "INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES (@v1, @v2, @v3, @v4, @v5)";
                    var p = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 }, { "v3", v3 }, { "v4", regDate }, { "v5", expiry } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "SubscriptionUsers")
                {
                    if (!RecordExists("rfid_cards", "card_id", v2))
                    {
                        MessageBox.Show("Mã thẻ RFID này chưa tồn tại trong danh mục thẻ (rfid_cards)!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DateTime birthDate = DateTime.Parse("2000-01-01");
                    DateTime.TryParseExact(v5, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);

                    string finalImgPath = GetOrCreateSubscriptionImagePath(v6, v8, "portrait");
                    string finalVehicleImgPath = GetOrCreateSubscriptionImagePath(txtVehicleImg.Text.Trim(), v8, "vehicle");

                    string q = @"INSERT INTO subscription_users (card_id, user_code, full_name, birth_date, member_image_path, vehicle_info, license_plate, vehicle_image_path) 
                                 VALUES (@v2, @v3, @v4, @v5, @member_image_path, @v7, @v8, @vehicle_image_path)";
                    var p = new Dictionary<string, object> {
                        { "v2", v2 }, { "v3", v3 }, { "v4", v4 },
                        { "v5", birthDate },
                        { "member_image_path", finalImgPath },
                        { "v7", v7 }, { "v8", v8 },
                        { "vehicle_image_path", finalVehicleImgPath }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                // =========================================================================
                else if (table == "ParkingHistory")
                {
                    // Ánh xạ chuẩn: v2=card_id, v3=entry_time, v4=entry_image_path, v5=exit_time, v6=exit_image_path, v7=fee_collected, v8=exit_gate
                    if (string.IsNullOrEmpty(v2))
                    {
                        MessageBox.Show("Vui lòng điền mã thẻ RFID bắt buộc!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime entryTime;
                    DateTime exitTime;
                    decimal fee = 0;

                    // Kiểm tra và bóc tách định dạng ngày tháng từ đúng ô v3 và v5
                    if (!DateTime.TryParseExact(v3, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out entryTime) ||
                        !DateTime.TryParseExact(v5, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out exitTime))
                    {
                        MessageBox.Show("Thời gian vào hoặc thời gian ra không đúng định dạng ngày tháng (dd/MM/yyyy HH:mm:ss)!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!string.IsNullOrEmpty(v7) && !decimal.TryParse(v7, out fee))
                    {
                        MessageBox.Show("Phí thu thực tế phải là số hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Đảm bảo phân vùng lịch sử tồn tại trước khi nạp dữ liệu
                    DatabaseHelper.EnsurePartitionExists(exitTime);

                    string q = @"INSERT INTO parking_history (card_id, entry_time, entry_image_path, exit_time, exit_image_path, fee_collected, exit_gate) 
                                 VALUES (@card_id, @entry_time, @entry_image_path, @exit_time, @exit_image_path, @fee_collected, @exit_gate)";

                    var p = new Dictionary<string, object> {
                        { "card_id", v2 },
                        { "entry_time", entryTime },
                        { "entry_image_path", v4 },
                        { "exit_time", exitTime },
                        { "exit_image_path", v6 },
                        { "fee_collected", fee },
                        { "exit_gate", string.IsNullOrEmpty(v8) ? "Cổng số 1" : v8 }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "Settings")
                {
                    if (RecordExists("settings", "setting_key", v1))
                    {
                        MessageBox.Show("Khóa cấu hình này đã tồn tại!", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string q = "INSERT INTO settings (setting_key, setting_value) VALUES (@v1, @v2)";
                    var p = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }

                MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCrudInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, $"Thêm bản ghi mới vào bảng {table}");
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            string table = cbTables.SelectedItem?.ToString() ?? "";
            string v1 = txtVal1.Text.Trim();
            string v2 = txtVal2.Text.Trim();
            string v3 = txtVal3.Text.Trim();
            string v4 = txtVal4.Text.Trim();
            string v5 = txtVal5.Text.Trim();
            string v6 = txtVal6.Text.Trim();
            string v7 = txtVal7.Text.Trim();
            string v8 = txtVal8.Text.Trim();

            if (string.IsNullOrEmpty(v1) || string.IsNullOrEmpty(v2))
            {
                MessageBox.Show("Vui lòng điền thông tin trước khi cập nhật!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] formats = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss" };

            try
            {
                if (table == "Users")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString()))
                    {
                        MessageBox.Show("Vui lòng chọn một dòng từ bảng dữ liệu trước khi sửa!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string q = "UPDATE users SET username = @new_username, password = @password, role = @role WHERE username = @old_username";
                    var p = new Dictionary<string, object> {
                        { "new_username", v1 },
                        { "password", v2 },
                        { "role", v3 },
                        { "old_username", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "ActiveParking")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString()))
                    {
                        MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DateTime entryTime;
                    if (!DateTime.TryParseExact(v2, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out entryTime))
                    {
                        MessageBox.Show("Thời gian vào không hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (v1 != _originalKey.ToString() && !RecordExists("rfid_cards", "card_id", v1))
                    {
                        MessageBox.Show("Mã thẻ RFID mới chưa tồn tại trong rfid_cards!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string gate = string.IsNullOrEmpty(v4) ? "Cổng số 1" : v4;
                    string q = "UPDATE active_parking SET card_id = @new_card, entry_time = @entry_time, entry_image_path = @entry_image_path, entry_gate = @entry_gate WHERE card_id = @old_card";
                    var p = new Dictionary<string, object> {
                        { "new_card", v1 },
                        { "entry_time", entryTime },
                        { "entry_image_path", v3 },
                        { "entry_gate", gate },
                        { "old_card", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "CardTypes")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString())) return;
                    decimal fee = 0;
                    if (!string.IsNullOrEmpty(v3) && !decimal.TryParse(v3, out fee))
                    {
                        MessageBox.Show("Phí mặc định phải là số hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string q = "UPDATE card_types SET card_type_id = @new_id, card_type_name = @name, default_fee = @fee WHERE card_type_id = @old_id";
                    var p = new Dictionary<string, object> {
                        { "new_id", v1 },
                        { "name", v2 },
                        { "fee", fee },
                        { "old_id", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "RFIDCards")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString())) return;
                    if (!RecordExists("card_types", "card_type_id", v2))
                    {
                        MessageBox.Show("Mã loại thẻ chưa tồn tại!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DateTime expiry;
                    if (!DateTime.TryParseExact(v4, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expiry))
                    {
                        MessageBox.Show("Ngày hết hạn không hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string q = "UPDATE rfid_cards SET card_id = @new_card, card_type_id = @type_id, status = @status, expiry_date = @expiry WHERE card_id = @old_card";
                    var p = new Dictionary<string, object> {
                        { "new_card", v1 },
                        { "type_id", v2 },
                        { "status", v3 },
                        { "expiry", expiry },
                        { "old_card", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "SubscriptionUsers")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString())) return;
                    if (!RecordExists("rfid_cards", "card_id", v2))
                    {
                        MessageBox.Show("Mã thẻ RFID chưa tồn tại!", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DateTime birthDate;
                    if (!DateTime.TryParseExact(v5, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate))
                    {
                        MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // =========================================================================
                    // THÊM MỚI: TỰ ĐỘNG ĐỔI TÊN THƯ MỤC ẢNH TRÊN Ổ CỨNG NẾU ĐỔI BIỂN SỐ XE
                    // =========================================================================
                    string oldPlate = "";
                    try
                    {
                        using (var conn = new Npgsql.NpgsqlConnection(DatabaseHelper.GetConnectionString()))
                        {
                            conn.Open();
                            using (var cmd = new Npgsql.NpgsqlCommand("SELECT license_plate FROM subscription_users WHERE member_id = @id", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", _originalKey.ToString()!);
                                oldPlate = cmd.ExecuteScalar()?.ToString() ?? "";
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Lỗi truy vấn biển số cũ: " + ex.Message); }

                    if (!string.IsNullOrEmpty(oldPlate) && !oldPlate.Equals(v8, StringComparison.OrdinalIgnoreCase))
                    {
                        string oldClean = oldPlate.Replace(":", "-").Replace("/", "-").Trim();
                        string newClean = v8.Replace(":", "-").Replace("/", "-").Trim();

                        string oldFolder = Path.Combine(FileStorageManager.SharedServerPath, "Subscriptions", oldClean);
                        string newFolder = Path.Combine(FileStorageManager.SharedServerPath, "Subscriptions", newClean);

                        // Nếu thư mục cũ tồn tại và thư mục mới chưa bị trùng tên, thực hiện đổi tên folder vật lý
                        if (Directory.Exists(oldFolder) && !Directory.Exists(newFolder))
                        {
                            try
                            {
                                Directory.Move(oldFolder, newFolder);
                            }
                            catch (Exception ex) { Console.WriteLine("Lỗi di chuyển thư mục ảnh: " + ex.Message); }
                        }
                    }
                    // =========================================================================

                    string finalImgPath = GetOrCreateSubscriptionImagePath(v6, v8, "portrait");
                    string finalVehicleImgPath = GetOrCreateSubscriptionImagePath(txtVehicleImg.Text.Trim(), v8, "vehicle");

                    string q = @"UPDATE subscription_users SET member_id = @new_mem, card_id = @card_id, user_code = @ucode, 
                                 full_name = @name, birth_date = @birth, member_image_path = @img, vehicle_info = @vinfo, license_plate = @plate,
                                 vehicle_image_path = @v_img 
                                 WHERE member_id = @old_mem";

                    var p = new Dictionary<string, object> {
                        { "new_mem", v1 }, { "card_id", v2 }, { "ucode", v3 }, { "name", v4 },
                        { "birth", birthDate },
                        { "img", finalImgPath }, { "vinfo", v7 }, { "plate", v8 },
                        { "v_img", finalVehicleImgPath },
                        { "old_mem", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "ParkingHistory")
                {
                    if (_selectedRowId == null || _originalExitTime == null) return;

                    DateTime entryTime;
                    DateTime exitTime;
                    decimal fee;

                    // SỬA ĐỔI: Đối chiếu đúng ô nhập: v3 là entry_time, v5 là exit_time
                    if (!DateTime.TryParseExact(v3, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out entryTime) ||
                        !DateTime.TryParseExact(v5, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out exitTime))
                    {
                        MessageBox.Show("Thời gian vào hoặc ra không hợp lệ (dd/MM/yyyy HH:mm:ss)!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(v7, out fee))
                    {
                        MessageBox.Show("Phí thu phải là số hợp lệ!", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DatabaseHelper.EnsurePartitionExists(exitTime);

                    // Đồng bộ sửa đầy đủ các trường thông tin hình ảnh và cổng ra lịch sử
                    string q = @"UPDATE parking_history 
                                 SET card_id = @card, entry_time = @entry, entry_image_path = @entry_img, 
                                     exit_time = @exit, exit_image_path = @exit_img, fee_collected = @fee, exit_gate = @gate 
                                 WHERE transaction_id = @id AND exit_time = @old_exit";

                    var p = new Dictionary<string, object> {
                        { "card", v2 }, // v2 mới thực sự là mã thẻ RFID
                        { "entry", entryTime },
                        { "entry_img", v4 },
                        { "exit", exitTime },
                        { "exit_img", v6 },
                        { "fee", fee },
                        { "gate", string.IsNullOrEmpty(v8) ? "Cổng số 1" : v8 },
                        { "id", Convert.ToInt64(_selectedRowId) },
                        { "old_exit", Convert.ToDateTime(_originalExitTime) }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "Settings")
                {
                    if (_originalKey == null || string.IsNullOrEmpty(_originalKey.ToString())) return;
                    string q = "UPDATE settings SET setting_key = @new_key, setting_value = @val WHERE setting_key = @old_key";
                    var p = new Dictionary<string, object> {
                        { "new_key", v1 },
                        { "val", v2 },
                        { "old_key", _originalKey.ToString()! }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }

                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCrudInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, $"Cập nhật thông tin bản ghi thuộc bảng {table}");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string table = cbTables.SelectedItem?.ToString() ?? "";
            string v1 = txtVal1.Text.Trim();

            if (string.IsNullOrEmpty(v1))
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Yêu Cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Safety check against foreign key constraint violations in PostgreSQL
            if (table == "CardTypes")
            {
                if (HasReference("SELECT COUNT(1) FROM rfid_cards WHERE card_type_id = @id", "id", v1))
                {
                    MessageBox.Show("Không thể xóa loại thẻ này vì có các thẻ RFID đang liên kết tới nó!", "Lỗi Ràng Buộc Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            else if (table == "RFIDCards")
            {
                if (HasReference("SELECT COUNT(1) FROM active_parking WHERE card_id = @id", "id", v1))
                {
                    MessageBox.Show("Không thể xóa thẻ này vì phương tiện đang sử dụng thẻ này đỗ trong bãi xe!", "Lỗi Ràng Buộc Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (HasReference("SELECT COUNT(1) FROM subscription_users WHERE card_id = @id", "id", v1))
                {
                    MessageBox.Show("Không thể xóa thẻ này vì có thành viên đăng ký đang sử dụng thẻ này!", "Lỗi Ràng Buộc Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa bản ghi '{v1}'?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                if (table == "Users")
                {
                    if (v1.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Không thể xóa tài khoản Admin hệ thống mặc định!", "Bảo vệ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    DatabaseHelper.DeleteUser(v1);
                }
                else if (table == "ActiveParking")
                {
                    DatabaseHelper.DeleteActiveParking(v1);
                }
                else if (table == "CardTypes")
                {
                    string q = "DELETE FROM card_types WHERE card_type_id = @v1";
                    var p = new Dictionary<string, object> { { "v1", v1 } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "RFIDCards")
                {
                    string q = "DELETE FROM rfid_cards WHERE card_id = @v1";
                    var p = new Dictionary<string, object> { { "v1", v1 } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "SubscriptionUsers")
                {
                    string q = "DELETE FROM subscription_users WHERE member_id = @v1";
                    var p = new Dictionary<string, object> { { "v1", v1 } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "ParkingHistory")
                {
                    if (_selectedRowId == null || _originalExitTime == null) return;
                    string q = "DELETE FROM parking_history WHERE transaction_id = @id AND exit_time = @exit";
                    var p = new Dictionary<string, object> {
                        { "id", Convert.ToInt64(_selectedRowId) },
                        { "exit", Convert.ToDateTime(_originalExitTime) }
                    };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }
                else if (table == "Settings")
                {
                    string q = "DELETE FROM settings WHERE setting_key = @v1";
                    var p = new Dictionary<string, object> { { "v1", v1 } };
                    DatabaseHelper.ExecuteNonQuery(q, p);
                }

                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCrudInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, $"Xóa bỏ bản ghi dữ liệu khỏi bảng {table}");
            }
        }

        private void BtnResetDb_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Hành động này sẽ XÓA SẠCH toàn bộ xe đang đỗ trong bãi xe khỏi cơ sở dữ liệu!\nBạn có muốn tiếp tục?", "Cảnh Báo Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.ResetDatabase();
                    MessageBox.Show("Đã reset dữ liệu bãi xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                }
            }
        }
        #endregion

        private string GetOrCreateSubscriptionImagePath(string val, string licensePlate, string imageType)
        {
            if (string.IsNullOrEmpty(val) || val == "[NO_IMAGE_FALLBACK]" || val == "[NO_IMAGE]")
                return "[NO_IMAGE_FALLBACK]";

            string cleanPlate = licensePlate.Replace(":", "-").Replace("/", "-").Trim();
            string expectedPath = $"ImageData/Subscriptions/{cleanPlate}/{imageType}.bin";

            if (val == $"{imageType}.bin")
            {
                return expectedPath;
            }

            string? srcPath = FileStorageManager.LocateSourceFile(val, licensePlate);
            if (srcPath != null)
            {
                try
                {
                    using (Image img = Image.FromFile(srcPath))
                    {
                        return FileStorageManager.SaveCompressedSubscriptionImage(img, licensePlate, imageType);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error compressing {imageType} image: {ex.Message}");
                }
            }

            return expectedPath;
        }
    }
}
