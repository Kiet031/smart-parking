using System.Drawing;
using System.Windows.Forms;

namespace SmartParking
{
    partial class GuardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Top Dashboard
            this.pnlTopDashboard = new System.Windows.Forms.Panel();
            this.tblTopDashboard = new System.Windows.Forms.TableLayoutPanel();

            // Sub-Panel A (Monthly Member Demographics)
            this.pnlSubPanelA = new System.Windows.Forms.Panel();
            this.pbSubPortrait = new System.Windows.Forms.PictureBox();
            this.lblMemberId = new System.Windows.Forms.Label();
            this.txtMemberId = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.lblVehicleInfo = new System.Windows.Forms.Label();
            this.txtVehicleInfo = new System.Windows.Forms.TextBox();
            this.lblLicensePlate = new System.Windows.Forms.Label();
            this.txtLicensePlate = new System.Windows.Forms.TextBox();

            // Sub-Panel B (Time & Analytics)
            this.pnlSubPanelB = new System.Windows.Forms.Panel();
            this.lblEntryTime = new System.Windows.Forms.Label();
            this.txtEntryTime = new System.Windows.Forms.TextBox();
            this.lblExitTime = new System.Windows.Forms.Label();
            this.txtExitTime = new System.Windows.Forms.TextBox();
            this.lblFee = new System.Windows.Forms.Label();
            this.txtFeeToPay = new System.Windows.Forms.TextBox();

            // Sub-Panel C (Status Broadcast Banner)
            this.pnlSubPanelC = new System.Windows.Forms.Panel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatusText = new System.Windows.Forms.Label();

            this.btnAllowExit = new System.Windows.Forms.Button();
            this.btnWarning = new System.Windows.Forms.Button();
            this.lblHotkeyInfo = new System.Windows.Forms.Label();

            // Right Audio Panel
            this.pnlRightAudio = new System.Windows.Forms.Panel();
            this.btnEnableAudio = new System.Windows.Forms.Button();
            this.btnDisableAudio = new System.Windows.Forms.Button();

            // Bottom Ribbon Panel
            this.pnlBottomRibbon = new System.Windows.Forms.Panel();
            this.btnRegisterMonthly = new System.Windows.Forms.Button();
            this.lblSearchPlate = new System.Windows.Forms.Label();
            this.txtSearchPlate = new System.Windows.Forms.TextBox();
            this.btnSearchPlate = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            // Central 2x2 Workspace
            this.tblCentralMatrix = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFrontExit = new System.Windows.Forms.Panel();
            this.lblFrontExitTitle = new System.Windows.Forms.Label();
            this.pbLiveFront = new System.Windows.Forms.PictureBox();

            this.pnlFrontEntry = new System.Windows.Forms.Panel();
            this.lblFrontEntryTitle = new System.Windows.Forms.Label();
            this.pbEntryFront = new System.Windows.Forms.PictureBox();

            this.pnlRearEntry = new System.Windows.Forms.Panel();
            this.lblRearEntryTitle = new System.Windows.Forms.Label();
            this.pbEntryRear = new System.Windows.Forms.PictureBox();

            this.pnlRearExit = new System.Windows.Forms.Panel();
            this.lblRearExitTitle = new System.Windows.Forms.Label();
            this.pbLiveRear = new System.Windows.Forms.PictureBox();

            // Hidden Panel to keep simulation references compiled and working
            this.pnlHiddenTest = new System.Windows.Forms.Panel();
            this.grpSimulator = new System.Windows.Forms.GroupBox();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.btnConnectCom = new System.Windows.Forms.Button();
            this.txtSimCardId = new System.Windows.Forms.TextBox();
            this.btnSimSwipe = new System.Windows.Forms.Button();
            this.txtSimPlate = new System.Windows.Forms.TextBox();
            this.btnSetPlate = new System.Windows.Forms.Button();
            this.btnMismatchExit = new System.Windows.Forms.Button();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();

            this.pnlTopDashboard.SuspendLayout();
            this.tblTopDashboard.SuspendLayout();
            this.pnlSubPanelA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubPortrait)).BeginInit();
            this.pnlSubPanelB.SuspendLayout();
            this.pnlSubPanelC.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlRightAudio.SuspendLayout();
            this.pnlBottomRibbon.SuspendLayout();
            this.tblCentralMatrix.SuspendLayout();
            this.pnlFrontExit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveFront)).BeginInit();
            this.pnlFrontEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryFront)).BeginInit();
            this.pnlRearEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryRear)).BeginInit();
            this.pnlRearExit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveRear)).BeginInit();
            this.pnlHiddenTest.SuspendLayout();
            this.grpSimulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlTopDashboard
            // 
            this.pnlTopDashboard.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlTopDashboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTopDashboard.Controls.Add(this.tblTopDashboard);
            this.pnlTopDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlTopDashboard.Name = "pnlTopDashboard";
            this.pnlTopDashboard.Size = new System.Drawing.Size(1264, 190); // TĂNG CHIỀU CAO lên 190 (Cũ là 140)
            this.pnlTopDashboard.TabIndex = 1;
            // 
            // tblTopDashboard
            // 
            this.tblTopDashboard.ColumnCount = 3; // CHUYỂN VỀ 3 CỘT THEO PHÁC THẢO VẼ
            this.tblTopDashboard.ColumnStyles.Clear();
            // Thiết lập tỷ lệ vàng mới: Ô Vé Tháng (38%) | Ô Thời gian (26%) | Ô Gộp lớn điều khiển (36%)
            this.tblTopDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tblTopDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tblTopDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblTopDashboard.Controls.Add(this.pnlSubPanelA, 0, 0);
            this.tblTopDashboard.Controls.Add(this.pnlSubPanelB, 1, 0);
            this.tblTopDashboard.Controls.Add(this.pnlSubPanelC, 2, 0); // Đưa Panel C làm ô chứa gộp chính
            this.tblTopDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTopDashboard.Location = new System.Drawing.Point(0, 0);
            this.tblTopDashboard.Name = "tblTopDashboard";
            this.tblTopDashboard.RowCount = 1;
            this.tblTopDashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTopDashboard.Size = new System.Drawing.Size(1262, 188);
            this.tblTopDashboard.TabIndex = 0;

            // 
            // pnlSubPanelA (Khu vực thông tin người đăng ký vé tháng)
            // 
            this.pnlSubPanelA.BackColor = System.Drawing.Color.White;
            this.pnlSubPanelA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSubPanelA.Controls.Add(this.pbSubPortrait);
            this.pnlSubPanelA.Controls.Add(this.lblMemberId);
            this.pnlSubPanelA.Controls.Add(this.txtMemberId);
            this.pnlSubPanelA.Controls.Add(this.lblUserCode); // THÊM MỚI
            this.pnlSubPanelA.Controls.Add(this.txtUserCode); // THÊM MỚI
            this.pnlSubPanelA.Controls.Add(this.lblFullName);
            this.pnlSubPanelA.Controls.Add(this.txtFullName);
            this.pnlSubPanelA.Controls.Add(this.lblVehicleInfo);
            this.pnlSubPanelA.Controls.Add(this.txtVehicleInfo);
            this.pnlSubPanelA.Controls.Add(this.lblLicensePlate);
            this.pnlSubPanelA.Controls.Add(this.txtLicensePlate);
            this.pnlSubPanelA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubPanelA.Location = new System.Drawing.Point(3, 3);
            this.pnlSubPanelA.Name = "pnlSubPanelA";
            this.pnlSubPanelA.Size = new System.Drawing.Size(474, 182);
            this.pnlSubPanelA.TabIndex = 0;
            // 
            // pbSubPortrait
            // 
            this.pbSubPortrait.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pbSubPortrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSubPortrait.Location = new System.Drawing.Point(12, 12);
            this.pbSubPortrait.Name = "pbSubPortrait";
            this.pbSubPortrait.Size = new System.Drawing.Size(115, 155);
            this.pbSubPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSubPortrait.TabIndex = 0;
            this.pbSubPortrait.TabStop = false;
            // 
            // lblMemberId
            // 
            this.lblMemberId.AutoSize = true;
            this.lblMemberId.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMemberId.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMemberId.Location = new System.Drawing.Point(140, 13);
            this.lblMemberId.Name = "lblMemberId";
            this.lblMemberId.Size = new System.Drawing.Size(71, 25);
            this.lblMemberId.TabIndex = 1;
            this.lblMemberId.Text = "Mã TV:";
            // 
            // txtMemberId
            // 
            this.txtMemberId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemberId.BackColor = System.Drawing.Color.White;
            this.txtMemberId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberId.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMemberId.Location = new System.Drawing.Point(245, 10); // Đẩy X sang 245 chống chạm chữ
            this.txtMemberId.Name = "txtMemberId";
            this.txtMemberId.PlaceholderText = "Thông tin vé tháng";
            this.txtMemberId.ReadOnly = true;
            this.txtMemberId.Size = new System.Drawing.Size(215, 32);
            this.txtMemberId.TabIndex = 2;
            // 
            // lblUserCode (NHÃN MSV/MGV MỚI)
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserCode.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUserCode.Location = new System.Drawing.Point(140, 46);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(102, 25);
            this.lblUserCode.Text = "MSV/MGV:";
            // 
            // txtUserCode (Ô HIỂN THỊ MSV/MGV MỚI)
            // 
            this.txtUserCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserCode.BackColor = System.Drawing.Color.White;
            this.txtUserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUserCode.Location = new System.Drawing.Point(245, 43);
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.PlaceholderText = "Thông tin vé tháng";
            this.txtUserCode.ReadOnly = true;
            this.txtUserCode.Size = new System.Drawing.Size(215, 32);
            this.txtUserCode.TabIndex = 3;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFullName.Location = new System.Drawing.Point(140, 79);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(77, 25);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "Họ Tên:";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.BackColor = System.Drawing.Color.White;
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtFullName.Location = new System.Drawing.Point(245, 76);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.PlaceholderText = "Thông tin vé tháng";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(215, 32);
            this.txtFullName.TabIndex = 5;
            // 
            // lblVehicleInfo
            // 
            this.lblVehicleInfo.AutoSize = true;
            this.lblVehicleInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVehicleInfo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVehicleInfo.Location = new System.Drawing.Point(140, 112);
            this.lblVehicleInfo.Name = "lblVehicleInfo";
            this.lblVehicleInfo.Size = new System.Drawing.Size(69, 25);
            this.lblVehicleInfo.TabIndex = 6;
            this.lblVehicleInfo.Text = "Mô tả:";
            // 
            // txtVehicleInfo
            // 
            this.txtVehicleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehicleInfo.BackColor = System.Drawing.Color.White;
            this.txtVehicleInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVehicleInfo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVehicleInfo.Location = new System.Drawing.Point(245, 109);
            this.txtVehicleInfo.Name = "txtVehicleInfo";
            this.txtVehicleInfo.PlaceholderText = "Thông tin vé tháng";
            this.txtVehicleInfo.ReadOnly = true;
            this.txtVehicleInfo.Size = new System.Drawing.Size(215, 32);
            this.txtVehicleInfo.TabIndex = 7;
            // 
            // lblLicensePlate
            // 
            this.lblLicensePlate.AutoSize = true;
            this.lblLicensePlate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLicensePlate.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblLicensePlate.Location = new System.Drawing.Point(140, 145);
            this.lblLicensePlate.Name = "lblLicensePlate";
            this.lblLicensePlate.Size = new System.Drawing.Size(79, 25);
            this.lblLicensePlate.TabIndex = 8;
            this.lblLicensePlate.Text = "Biển số:";
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicensePlate.BackColor = System.Drawing.Color.White;
            this.txtLicensePlate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicensePlate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtLicensePlate.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.txtLicensePlate.Location = new System.Drawing.Point(245, 142);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.PlaceholderText = "Thông tin vé tháng";
            this.txtLicensePlate.ReadOnly = true;
            this.txtLicensePlate.Size = new System.Drawing.Size(215, 34);
            this.txtLicensePlate.TabIndex = 9;

            // 
            // pnlSubPanelB (Khu vực hiển thị thời gian và chi phí bãi xe)
            // 
            this.pnlSubPanelB.BackColor = System.Drawing.Color.White;
            this.pnlSubPanelB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSubPanelB.Controls.Add(this.lblEntryTime);
            this.pnlSubPanelB.Controls.Add(this.txtEntryTime);
            this.pnlSubPanelB.Controls.Add(this.lblExitTime);
            this.pnlSubPanelB.Controls.Add(this.txtExitTime);
            this.pnlSubPanelB.Controls.Add(this.lblFee);
            this.pnlSubPanelB.Controls.Add(this.txtFeeToPay);
            this.pnlSubPanelB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubPanelB.Location = new System.Drawing.Point(483, 3);
            this.pnlSubPanelB.Name = "pnlSubPanelB";
            this.pnlSubPanelB.Size = new System.Drawing.Size(322, 182);
            this.pnlSubPanelB.TabIndex = 1;
            // 
            // lblEntryTime
            // 
            this.lblEntryTime.AutoSize = true;
            this.lblEntryTime.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEntryTime.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEntryTime.Location = new System.Drawing.Point(12, 18);
            this.lblEntryTime.Name = "lblEntryTime";
            this.lblEntryTime.Size = new System.Drawing.Size(89, 25);
            this.lblEntryTime.TabIndex = 0;
            this.lblEntryTime.Text = "Giờ Vào:";
            // 
            // txtEntryTime
            // 
            this.txtEntryTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntryTime.BackColor = System.Drawing.Color.White;
            this.txtEntryTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEntryTime.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEntryTime.Location = new System.Drawing.Point(112, 14);
            this.txtEntryTime.Name = "txtEntryTime";
            this.txtEntryTime.ReadOnly = true;
            this.txtEntryTime.Size = new System.Drawing.Size(196, 32);
            this.txtEntryTime.TabIndex = 1;
            // 
            // lblExitTime
            // 
            this.lblExitTime.AutoSize = true;
            this.lblExitTime.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblExitTime.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblExitTime.Location = new System.Drawing.Point(12, 68);
            this.lblExitTime.Name = "lblExitTime";
            this.lblExitTime.Size = new System.Drawing.Size(77, 25);
            this.lblExitTime.TabIndex = 2;
            this.lblExitTime.Text = "Giờ Ra:";
            // 
            // txtExitTime
            // 
            this.txtExitTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExitTime.BackColor = System.Drawing.Color.White;
            this.txtExitTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExitTime.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtExitTime.Location = new System.Drawing.Point(112, 64);
            this.txtExitTime.Name = "txtExitTime";
            this.txtExitTime.ReadOnly = true;
            this.txtExitTime.Size = new System.Drawing.Size(196, 32);
            this.txtExitTime.TabIndex = 3;
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFee.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblFee.Location = new System.Drawing.Point(12, 122);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(89, 28);
            this.lblFee.TabIndex = 4;
            this.lblFee.Text = "Phí Thu:";
            // 
            // txtFeeToPay
            // 
            this.txtFeeToPay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFeeToPay.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.txtFeeToPay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFeeToPay.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.txtFeeToPay.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.txtFeeToPay.Location = new System.Drawing.Point(112, 110);
            this.txtFeeToPay.Name = "txtFeeToPay";
            this.txtFeeToPay.ReadOnly = true;
            this.txtFeeToPay.Size = new System.Drawing.Size(196, 56);
            this.txtFeeToPay.TabIndex = 5;
            this.txtFeeToPay.Text = "--";
            this.txtFeeToPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // pnlSubPanelC (Ô lớn gộp cả Thông báo trạng thái và Nút bấm Barie)
            // 
            this.pnlSubPanelC.BackColor = System.Drawing.Color.White;
            this.pnlSubPanelC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSubPanelC.Controls.Add(this.pnlStatus); // Nửa trên chứa băng rôn thông báo màu xanh/xám
            this.pnlSubPanelC.Controls.Add(this.btnWarning); // Nút Đóng Barie xếp bên trái
            this.pnlSubPanelC.Controls.Add(this.btnAllowExit); // Nút Mở Barie xếp bên phải
            this.pnlSubPanelC.Controls.Add(this.lblHotkeyInfo); // Dòng chữ hotkey nhỏ dưới đáy
            this.pnlSubPanelC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubPanelC.Location = new System.Drawing.Point(811, 3);
            this.pnlSubPanelC.Name = "pnlSubPanelC";
            this.pnlSubPanelC.Size = new System.Drawing.Size(448, 182);
            this.pnlSubPanelC.TabIndex = 2;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.pnlStatus.Controls.Add(this.lblStatusText);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(5);
            this.pnlStatus.Size = new System.Drawing.Size(446, 95); // Chiếm chiều dọc nửa trên ô gộp
            this.pnlStatus.TabIndex = 0;
            // 
            // lblStatusText
            // 
            this.lblStatusText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblStatusText.ForeColor = System.Drawing.Color.White;
            this.lblStatusText.Location = new System.Drawing.Point(5, 5);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(436, 85);
            this.lblStatusText.TabIndex = 0;
            this.lblStatusText.Text = "MỜI QUẸT THẺ";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWarning (ESC Đóng Barie - Xếp Trái theo phác thảo vẽ)
            // 
            this.btnWarning.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnWarning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWarning.FlatAppearance.BorderSize = 0;
            this.btnWarning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarning.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnWarning.ForeColor = System.Drawing.Color.White;
            this.btnWarning.Location = new System.Drawing.Point(12, 110);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.Size = new System.Drawing.Size(205, 48);
            this.btnWarning.TabIndex = 1;
            this.btnWarning.Text = "ĐÓNG BARIE\n(Esc)";
            this.btnWarning.UseVisualStyleBackColor = false;
            this.btnWarning.Click += new System.EventHandler(this.BtnWarning_Click);
            // 
            // btnAllowExit (ENTER Mở Barie - Xếp Phải theo phác thảo vẽ)
            // 
            this.btnAllowExit.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnAllowExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllowExit.FlatAppearance.BorderSize = 0;
            this.btnAllowExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllowExit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAllowExit.ForeColor = System.Drawing.Color.White;
            this.btnAllowExit.Location = new System.Drawing.Point(227, 110);
            this.btnAllowExit.Name = "btnAllowExit";
            this.btnAllowExit.Size = new System.Drawing.Size(205, 48);
            this.btnAllowExit.TabIndex = 0;
            this.btnAllowExit.Text = "MỞ BARIE\n(Enter)";
            this.btnAllowExit.UseVisualStyleBackColor = false;
            this.btnAllowExit.Click += new System.EventHandler(this.BtnAllowExit_Click);
            // 
            // lblHotkeyInfo
            // 
            this.lblHotkeyInfo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblHotkeyInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHotkeyInfo.Location = new System.Drawing.Point(12, 161);
            this.lblHotkeyInfo.Name = "lblHotkeyInfo";
            this.lblHotkeyInfo.Size = new System.Drawing.Size(420, 18);
            this.lblHotkeyInfo.TabIndex = 2;
            this.lblHotkeyInfo.Text = "Phím tắt hệ thống - Enter: Mở Barie | Esc: Đóng Barie";
            this.lblHotkeyInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // pnlRightAudio (Audio Notification Anchors)
            // 
            this.pnlRightAudio.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlRightAudio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRightAudio.Controls.Add(this.btnDisableAudio);
            this.pnlRightAudio.Controls.Add(this.btnEnableAudio);
            this.pnlRightAudio.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightAudio.Location = new System.Drawing.Point(1184, 190);
            this.pnlRightAudio.Name = "pnlRightAudio";
            this.pnlRightAudio.Size = new System.Drawing.Size(80, 471);
            this.pnlRightAudio.TabIndex = 2;
            // 
            // btnEnableAudio
            // 
            this.btnEnableAudio.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnEnableAudio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnableAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnableAudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnableAudio.ForeColor = System.Drawing.Color.White;
            this.btnEnableAudio.Location = new System.Drawing.Point(10, 40);
            this.btnEnableAudio.Name = "btnEnableAudio";
            this.btnEnableAudio.Size = new System.Drawing.Size(60, 60);
            this.btnEnableAudio.TabIndex = 0;
            this.btnEnableAudio.Text = "Bật Loa\n🔊";
            this.btnEnableAudio.UseVisualStyleBackColor = false;
            this.btnEnableAudio.Click += new System.EventHandler(this.BtnEnableAudio_Click);
            // 
            // btnDisableAudio
            // 
            this.btnDisableAudio.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnDisableAudio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisableAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableAudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDisableAudio.ForeColor = System.Drawing.Color.White;
            this.btnDisableAudio.Location = new System.Drawing.Point(10, 120);
            this.btnDisableAudio.Name = "btnDisableAudio";
            this.btnDisableAudio.Size = new System.Drawing.Size(60, 60);
            this.btnDisableAudio.TabIndex = 1;
            this.btnDisableAudio.Text = "Tắt Loa\n🔇";
            this.btnDisableAudio.UseVisualStyleBackColor = false;
            this.btnDisableAudio.Click += new System.EventHandler(this.BtnDisableAudio_Click);

            // 
            // pnlBottomRibbon (Layout Utility Ribbon)
            // 
            this.pnlBottomRibbon.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.pnlBottomRibbon.Controls.Add(this.btnRegisterMonthly);
            this.pnlBottomRibbon.Controls.Add(this.lblSearchPlate);
            this.pnlBottomRibbon.Controls.Add(this.txtSearchPlate);
            this.pnlBottomRibbon.Controls.Add(this.btnSearchPlate);
            this.pnlBottomRibbon.Controls.Add(this.btnLogout); // ĐÃ XÓA DÒNG LBLCOMSTATUS TẠI ĐÂY
            this.pnlBottomRibbon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomRibbon.Location = new System.Drawing.Point(0, 661);
            this.pnlBottomRibbon.Name = "pnlBottomRibbon";
            this.pnlBottomRibbon.Size = new System.Drawing.Size(1264, 60);
            this.pnlBottomRibbon.TabIndex = 3;
            // 
            // btnRegisterMonthly
            // 
            this.btnRegisterMonthly.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnRegisterMonthly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegisterMonthly.FlatAppearance.BorderSize = 0;
            this.btnRegisterMonthly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterMonthly.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegisterMonthly.ForeColor = System.Drawing.Color.White;
            this.btnRegisterMonthly.Location = new System.Drawing.Point(15, 12);
            this.btnRegisterMonthly.Name = "btnRegisterMonthly";
            this.btnRegisterMonthly.Size = new System.Drawing.Size(180, 36);
            this.btnRegisterMonthly.TabIndex = 0;
            this.btnRegisterMonthly.Text = "Đăng ký thẻ tháng";
            this.btnRegisterMonthly.UseVisualStyleBackColor = false;
            this.btnRegisterMonthly.Click += new System.EventHandler(this.BtnRegisterMonthly_Click);
            // 
            // lblSearchPlate
            // 
            this.lblSearchPlate.AutoSize = true;
            this.lblSearchPlate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearchPlate.ForeColor = System.Drawing.Color.White;
            this.lblSearchPlate.Location = new System.Drawing.Point(400, 20);
            this.lblSearchPlate.Name = "lblSearchPlate";
            this.lblSearchPlate.Size = new System.Drawing.Size(95, 19);
            this.lblSearchPlate.TabIndex = 1;
            this.lblSearchPlate.Text = "Tìm kiếm xe:";
            // 
            // txtSearchPlate
            // 
            this.txtSearchPlate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearchPlate.Location = new System.Drawing.Point(500, 16);
            this.txtSearchPlate.Name = "txtSearchPlate";
            this.txtSearchPlate.Size = new System.Drawing.Size(200, 27);
            this.txtSearchPlate.TabIndex = 2;
            this.txtSearchPlate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchPlate_KeyDown);
            // 
            // btnSearchPlate
            // 
            this.btnSearchPlate.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnSearchPlate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchPlate.FlatAppearance.BorderSize = 0;
            this.btnSearchPlate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPlate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearchPlate.ForeColor = System.Drawing.Color.White;
            this.btnSearchPlate.Location = new System.Drawing.Point(710, 15);
            this.btnSearchPlate.Name = "btnSearchPlate";
            this.btnSearchPlate.Size = new System.Drawing.Size(120, 28);
            this.btnSearchPlate.TabIndex = 3;
            this.btnSearchPlate.Text = "🔍 Tìm kiếm";
            this.btnSearchPlate.UseVisualStyleBackColor = false;
            this.btnSearchPlate.Click += new System.EventHandler(this.BtnSearchPlate_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1140, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(110, 36);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);

            // 
            // tblCentralMatrix (Central Workspace Streams Matrix 2x2 Grid)
            // 
            this.tblCentralMatrix.ColumnCount = 2;
            this.tblCentralMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCentralMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCentralMatrix.Controls.Add(this.pnlFrontExit, 0, 0);
            this.tblCentralMatrix.Controls.Add(this.pnlFrontEntry, 1, 0);
            this.tblCentralMatrix.Controls.Add(this.pnlRearEntry, 0, 1);
            this.tblCentralMatrix.Controls.Add(this.pnlRearExit, 1, 1);
            this.tblCentralMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCentralMatrix.Location = new System.Drawing.Point(0, 190);
            this.tblCentralMatrix.Name = "tblCentralMatrix";
            this.tblCentralMatrix.RowCount = 2;
            this.tblCentralMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCentralMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCentralMatrix.Size = new System.Drawing.Size(1184, 471);
            this.tblCentralMatrix.TabIndex = 4;
            // 
            // pnlFrontExit (Top-Left)
            // 
            this.pnlFrontExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFrontExit.Controls.Add(this.pbLiveFront);
            this.pnlFrontExit.Controls.Add(this.lblFrontExitTitle);
            this.pnlFrontExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFrontExit.Location = new System.Drawing.Point(3, 3);
            this.pnlFrontExit.Name = "pnlFrontExit";
            this.pnlFrontExit.Size = new System.Drawing.Size(586, 229);
            this.pnlFrontExit.TabIndex = 0;
            // 
            // lblFrontExitTitle
            // 
            this.lblFrontExitTitle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblFrontExitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFrontExitTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFrontExitTitle.ForeColor = System.Drawing.Color.White;
            this.lblFrontExitTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFrontExitTitle.Name = "lblFrontExitTitle";
            this.lblFrontExitTitle.Size = new System.Drawing.Size(584, 25);
            this.lblFrontExitTitle.TabIndex = 0;
            this.lblFrontExitTitle.Text = "Camera trước (Làn ra)";
            this.lblFrontExitTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLiveFront
            // 
            this.pbLiveFront.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.pbLiveFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLiveFront.Location = new System.Drawing.Point(0, 25);
            this.pbLiveFront.Name = "pbLiveFront";
            this.pbLiveFront.Size = new System.Drawing.Size(584, 202);
            this.pbLiveFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLiveFront.TabIndex = 1;
            this.pbLiveFront.TabStop = false;
            // 
            // pnlFrontEntry (Top-Right)
            // 
            this.pnlFrontEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFrontEntry.Controls.Add(this.pbEntryFront);
            this.pnlFrontEntry.Controls.Add(this.lblFrontEntryTitle);
            this.pnlFrontEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFrontEntry.Location = new System.Drawing.Point(595, 3);
            this.pnlFrontEntry.Name = "pnlFrontEntry";
            this.pnlFrontEntry.Size = new System.Drawing.Size(586, 229);
            this.pnlFrontEntry.TabIndex = 1;
            // 
            // lblFrontEntryTitle
            // 
            this.lblFrontEntryTitle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblFrontEntryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFrontEntryTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFrontEntryTitle.ForeColor = System.Drawing.Color.White;
            this.lblFrontEntryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFrontEntryTitle.Name = "lblFrontEntryTitle";
            this.lblFrontEntryTitle.Size = new System.Drawing.Size(584, 25);
            this.lblFrontEntryTitle.TabIndex = 0;
            this.lblFrontEntryTitle.Text = "Camera trước (Làn vào)";
            this.lblFrontEntryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbEntryFront
            // 
            this.pbEntryFront.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.pbEntryFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbEntryFront.Location = new System.Drawing.Point(0, 25);
            this.pbEntryFront.Name = "pbEntryFront";
            this.pbEntryFront.Size = new System.Drawing.Size(584, 202);
            this.pbEntryFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEntryFront.TabIndex = 1;
            this.pbEntryFront.TabStop = false;
            // 
            // pnlRearEntry (Bottom-Left)
            // 
            this.pnlRearEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRearEntry.Controls.Add(this.pbEntryRear);
            this.pnlRearEntry.Controls.Add(this.lblRearEntryTitle);
            this.pnlRearEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRearEntry.Location = new System.Drawing.Point(3, 238);
            this.pnlRearEntry.Name = "pnlRearEntry";
            this.pnlRearEntry.Size = new System.Drawing.Size(586, 230);
            this.pnlRearEntry.TabIndex = 2;
            // 
            // lblRearEntryTitle
            // 
            this.lblRearEntryTitle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRearEntryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRearEntryTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRearEntryTitle.ForeColor = System.Drawing.Color.White;
            this.lblRearEntryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRearEntryTitle.Name = "lblRearEntryTitle";
            this.lblRearEntryTitle.Size = new System.Drawing.Size(584, 25);
            this.lblRearEntryTitle.TabIndex = 0;
            this.lblRearEntryTitle.Text = "Camera sau (Làn vào)";
            this.lblRearEntryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbEntryRear
            // 
            this.pbEntryRear.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.pbEntryRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbEntryRear.Location = new System.Drawing.Point(0, 25);
            this.pbEntryRear.Name = "pbEntryRear";
            this.pbEntryRear.Size = new System.Drawing.Size(584, 203);
            this.pbEntryRear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEntryRear.TabIndex = 1;
            this.pbEntryRear.TabStop = false;
            // 
            // pnlRearExit (Bottom-Right)
            // 
            this.pnlRearExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRearExit.Controls.Add(this.pbLiveRear);
            this.pnlRearExit.Controls.Add(this.lblRearExitTitle);
            this.pnlRearExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRearExit.Location = new System.Drawing.Point(595, 238);
            this.pnlRearExit.Name = "pnlRearExit";
            this.pnlRearExit.Size = new System.Drawing.Size(586, 230);
            this.pnlRearExit.TabIndex = 3;
            // 
            // lblRearExitTitle
            // 
            this.lblRearExitTitle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRearExitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRearExitTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRearExitTitle.ForeColor = System.Drawing.Color.White;
            this.lblRearExitTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRearExitTitle.Name = "lblRearExitTitle";
            this.lblRearExitTitle.Size = new System.Drawing.Size(584, 25);
            this.lblRearExitTitle.TabIndex = 0;
            this.lblRearExitTitle.Text = "Camera sau (Làn ra)";
            this.lblRearExitTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLiveRear
            // 
            this.pbLiveRear.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.pbLiveRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLiveRear.Location = new System.Drawing.Point(0, 25);
            this.pbLiveRear.Name = "pbLiveRear";
            this.pbLiveRear.Size = new System.Drawing.Size(584, 203);
            this.pbLiveRear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLiveRear.TabIndex = 1;
            this.pbLiveRear.TabStop = false;

            // 
            // pnlHiddenTest (Background references)
            // 
            this.pnlHiddenTest.Controls.Add(this.grpSimulator);
            this.pnlHiddenTest.Controls.Add(this.dgvLogs);
            this.pnlHiddenTest.Location = new System.Drawing.Point(12, 1000); // Placed way outside form bounds
            this.pnlHiddenTest.Name = "pnlHiddenTest";
            this.pnlHiddenTest.Size = new System.Drawing.Size(300, 300);
            this.pnlHiddenTest.TabIndex = 5;
            this.pnlHiddenTest.Visible = false;
            // 
            // grpSimulator
            // 
            this.grpSimulator.Controls.Add(this.btnConnectCom);
            this.grpSimulator.Controls.Add(this.cbComPort);
            this.grpSimulator.Controls.Add(this.label4);
            this.grpSimulator.Controls.Add(this.btnMismatchExit);
            this.grpSimulator.Controls.Add(this.btnSetPlate);
            this.grpSimulator.Controls.Add(this.txtSimPlate);
            this.grpSimulator.Controls.Add(this.label3);
            this.grpSimulator.Controls.Add(this.btnSimSwipe);
            this.grpSimulator.Controls.Add(this.txtSimCardId);
            this.grpSimulator.Controls.Add(this.label2);
            this.grpSimulator.Controls.Add(this.label1);
            this.grpSimulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSimulator.Location = new System.Drawing.Point(0, 0);
            this.grpSimulator.Name = "grpSimulator";
            this.grpSimulator.Size = new System.Drawing.Size(300, 200);
            this.grpSimulator.TabIndex = 0;
            this.grpSimulator.TabStop = false;
            this.grpSimulator.Text = "BẢNG GIẢ LẬP THIẾT BỊ (TEST)";
            // 
            // btnConnectCom
            // 
            this.btnConnectCom.Location = new System.Drawing.Point(0, 0);
            this.btnConnectCom.Name = "btnConnectCom";
            this.btnConnectCom.Size = new System.Drawing.Size(10, 10);
            this.btnConnectCom.Click += new System.EventHandler(this.BtnConnectCom_Click);
            // 
            // cbComPort
            // 
            this.cbComPort.Location = new System.Drawing.Point(0, 0);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(10, 10);
            this.cbComPort.DropDown += new System.EventHandler(this.CbComPort_DropDown);
            // 
            // txtSimCardId
            // 
            this.txtSimCardId.Location = new System.Drawing.Point(0, 0);
            this.txtSimCardId.Name = "txtSimCardId";
            this.txtSimCardId.Size = new System.Drawing.Size(10, 10);
            this.txtSimCardId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSimCardId_KeyDown);
            // 
            // btnSimSwipe
            // 
            this.btnSimSwipe.Location = new System.Drawing.Point(0, 0);
            this.btnSimSwipe.Name = "btnSimSwipe";
            this.btnSimSwipe.Size = new System.Drawing.Size(10, 10);
            this.btnSimSwipe.Click += new System.EventHandler(this.BtnSimSwipe_Click);
            // 
            // txtSimPlate
            // 
            this.txtSimPlate.Location = new System.Drawing.Point(0, 0);
            this.txtSimPlate.Name = "txtSimPlate";
            this.txtSimPlate.Size = new System.Drawing.Size(10, 10);
            // 
            // btnSetPlate
            // 
            this.btnSetPlate.Location = new System.Drawing.Point(0, 0);
            this.btnSetPlate.Name = "btnSetPlate";
            this.btnSetPlate.Size = new System.Drawing.Size(10, 10);
            this.btnSetPlate.Click += new System.EventHandler(this.BtnSetPlate_Click);
            // 
            // btnMismatchExit
            // 
            this.btnMismatchExit.Location = new System.Drawing.Point(0, 0);
            this.btnMismatchExit.Name = "btnMismatchExit";
            this.btnMismatchExit.Size = new System.Drawing.Size(10, 10);
            this.btnMismatchExit.Click += new System.EventHandler(this.BtnMismatchExit_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Size = new System.Drawing.Size(10, 10);
            this.label1.Name = "label1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Size = new System.Drawing.Size(10, 10);
            this.label2.Name = "label2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Size = new System.Drawing.Size(10, 10);
            this.label3.Name = "label3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Size = new System.Drawing.Size(10, 10);
            this.label4.Name = "label4";
            // 
            // dgvLogs
            // 
            this.dgvLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvLogs.Size = new System.Drawing.Size(10, 10);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLogs_CellClick);

            // 
            // GuardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 721);
            this.Controls.Add(this.tblCentralMatrix);
            this.Controls.Add(this.pnlRightAudio);
            this.Controls.Add(this.pnlBottomRibbon);
            this.Controls.Add(this.pnlTopDashboard);
            this.Controls.Add(this.pnlHiddenTest);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.KeyPreview = true;
            this.Name = "GuardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Bãi xe Thông minh - Guard Desk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GuardForm_FormClosing);
            this.Load += new System.EventHandler(this.GuardForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GuardForm_KeyDown);

            this.pnlTopDashboard.ResumeLayout(false);
            this.tblTopDashboard.ResumeLayout(false);
            this.pnlSubPanelA.ResumeLayout(false);
            this.pnlSubPanelA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubPortrait)).EndInit();
            this.pnlSubPanelB.ResumeLayout(false);
            this.pnlSubPanelB.PerformLayout();
            this.pnlSubPanelC.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.pnlRightAudio.ResumeLayout(false);
            this.pnlBottomRibbon.ResumeLayout(false);
            this.pnlBottomRibbon.PerformLayout();
            this.tblCentralMatrix.ResumeLayout(false);
            this.pnlFrontExit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveFront)).EndInit();
            this.pnlFrontEntry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryFront)).EndInit();
            this.pnlRearEntry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryRear)).EndInit();
            this.pnlRearExit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveRear)).EndInit();
            this.pnlHiddenTest.ResumeLayout(false);
            this.grpSimulator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        // Top Dashboard
        private System.Windows.Forms.Panel pnlTopDashboard;
        private System.Windows.Forms.TableLayoutPanel tblTopDashboard;

        // Sub-Panel A
        private System.Windows.Forms.Panel pnlSubPanelA;
        private System.Windows.Forms.PictureBox pbSubPortrait;
        private System.Windows.Forms.Label lblMemberId;
        private System.Windows.Forms.TextBox txtMemberId;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblVehicleInfo;
        private System.Windows.Forms.TextBox txtVehicleInfo;
        private System.Windows.Forms.Label lblLicensePlate;
        private System.Windows.Forms.TextBox txtLicensePlate;

        // Sub-Panel B
        private System.Windows.Forms.Panel pnlSubPanelB;
        private System.Windows.Forms.Label lblEntryTime;
        private System.Windows.Forms.TextBox txtEntryTime;
        private System.Windows.Forms.Label lblExitTime;
        private System.Windows.Forms.TextBox txtExitTime;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.TextBox txtFeeToPay;

        // Sub-Panel C
        private System.Windows.Forms.Panel pnlSubPanelC;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Button btnAllowExit;
        private System.Windows.Forms.Button btnWarning;
        private System.Windows.Forms.Label lblHotkeyInfo;

        // Central Streams Matrix
        private System.Windows.Forms.TableLayoutPanel tblCentralMatrix;
        private System.Windows.Forms.Panel pnlFrontExit;
        private System.Windows.Forms.Label lblFrontExitTitle;
        private System.Windows.Forms.PictureBox pbLiveFront;

        private System.Windows.Forms.Panel pnlFrontEntry;
        private System.Windows.Forms.Label lblFrontEntryTitle;
        private System.Windows.Forms.PictureBox pbEntryFront;

        private System.Windows.Forms.Panel pnlRearEntry;
        private System.Windows.Forms.Label lblRearEntryTitle;
        private System.Windows.Forms.PictureBox pbEntryRear;

        private System.Windows.Forms.Panel pnlRearExit;
        private System.Windows.Forms.Label lblRearExitTitle;
        private System.Windows.Forms.PictureBox pbLiveRear;

        // Right Audio Anchors
        private System.Windows.Forms.Panel pnlRightAudio;
        private System.Windows.Forms.Button btnEnableAudio;
        private System.Windows.Forms.Button btnDisableAudio;

        // Bottom Ribbon
        private System.Windows.Forms.Panel pnlBottomRibbon;
        private System.Windows.Forms.Button btnRegisterMonthly;
        private System.Windows.Forms.Label lblSearchPlate;
        private System.Windows.Forms.TextBox txtSearchPlate;
        private System.Windows.Forms.Button btnSearchPlate;
        private System.Windows.Forms.Button btnLogout;

        // Hidden controls for simulator legacy
        private System.Windows.Forms.Panel pnlHiddenTest;
        private System.Windows.Forms.GroupBox grpSimulator;
        private System.Windows.Forms.Button btnSimSwipe;
        private System.Windows.Forms.TextBox txtSimCardId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnSetPlate;
        private System.Windows.Forms.TextBox txtSimPlate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMismatchExit;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnectCom;
    }
}
