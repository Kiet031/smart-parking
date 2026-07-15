namespace SmartParking
{
    partial class AdminForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.tabControlAdmin = new System.Windows.Forms.TabControl();
            this.tabCamera = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.grpCamRear = new System.Windows.Forms.GroupBox();
            this.txtRearPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRearUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRearPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRearIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpCamFront = new System.Windows.Forms.GroupBox();
            this.txtFrontPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFrontUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrontPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrontIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.btnResetDb = new System.Windows.Forms.Button();
            this.grpCrudActions = new System.Windows.Forms.GroupBox();
            this.txtVal4 = new System.Windows.Forms.TextBox();
            this.lblVal4 = new System.Windows.Forms.Label();
            this.txtVal3 = new System.Windows.Forms.TextBox();
            this.lblVal3 = new System.Windows.Forms.Label();
            this.txtVal2 = new System.Windows.Forms.TextBox();
            this.lblVal2 = new System.Windows.Forms.Label();
            this.txtVal1 = new System.Windows.Forms.TextBox();
            this.lblVal1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvAdminData = new System.Windows.Forms.DataGridView();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tabControlAdmin.SuspendLayout();
            this.tabCamera.SuspendLayout();
            this.grpCamRear.SuspendLayout();
            this.grpCamFront.SuspendLayout();
            this.tabData.SuspendLayout();
            this.grpCrudActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(984, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(860, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(112, 36);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "ĐĂNG XUẤT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(15, 15);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(517, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "BẢNG ĐIỀU KHIỂN QUẢN TRỊ - ADMIN DASHBOARD";
            // 
            // tabControlAdmin
            // 
            this.tabControlAdmin.Controls.Add(this.tabCamera);
            this.tabControlAdmin.Controls.Add(this.tabData);
            this.tabControlAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tabControlAdmin.Location = new System.Drawing.Point(0, 60);
            this.tabControlAdmin.Name = "tabControlAdmin";
            this.tabControlAdmin.SelectedIndex = 0;
            this.tabControlAdmin.Size = new System.Drawing.Size(984, 601);
            this.tabControlAdmin.TabIndex = 2;
            // 
            // tabCamera
            // 
            this.tabCamera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabCamera.Controls.Add(this.btnSaveConfig);
            this.tabCamera.Controls.Add(this.grpCamRear);
            this.tabCamera.Controls.Add(this.grpCamFront);
            this.tabCamera.Location = new System.Drawing.Point(4, 30);
            this.tabCamera.Name = "tabCamera";
            this.tabCamera.Padding = new System.Windows.Forms.Padding(20);
            this.tabCamera.Size = new System.Drawing.Size(976, 567);
            this.tabCamera.TabIndex = 0;
            this.tabCamera.Text = "Cấu Hình Camera Hikvision";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSaveConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveConfig.FlatAppearance.BorderSize = 0;
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSaveConfig.ForeColor = System.Drawing.Color.White;
            this.btnSaveConfig.Location = new System.Drawing.Point(360, 460);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(250, 60);
            this.btnSaveConfig.TabIndex = 2;
            this.btnSaveConfig.Text = "LƯU CẤU HÌNH RTSP";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.BtnSaveConfig_Click);
            // 
            // grpCamRear
            // 
            this.grpCamRear.BackColor = System.Drawing.Color.White;
            this.grpCamRear.Controls.Add(this.txtRearPass);
            this.grpCamRear.Controls.Add(this.label5);
            this.grpCamRear.Controls.Add(this.txtRearUser);
            this.grpCamRear.Controls.Add(this.label6);
            this.grpCamRear.Controls.Add(this.txtRearPort);
            this.grpCamRear.Controls.Add(this.label7);
            this.grpCamRear.Controls.Add(this.txtRearIP);
            this.grpCamRear.Controls.Add(this.label8);
            this.grpCamRear.Location = new System.Drawing.Point(500, 30);
            this.grpCamRear.Name = "grpCamRear";
            this.grpCamRear.Size = new System.Drawing.Size(440, 390);
            this.grpCamRear.TabIndex = 1;
            this.grpCamRear.TabStop = false;
            this.grpCamRear.Text = "CAMERA SAU (RTSP - LÀN RA)";
            // 
            // txtRearPass
            // 
            this.txtRearPass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRearPass.Location = new System.Drawing.Point(26, 310);
            this.txtRearPass.Name = "txtRearPass";
            this.txtRearPass.Size = new System.Drawing.Size(380, 29);
            this.txtRearPass.TabIndex = 7;
            this.txtRearPass.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label5.Location = new System.Drawing.Point(22, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Mật khẩu:";
            // 
            // txtRearUser
            // 
            this.txtRearUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRearUser.Location = new System.Drawing.Point(26, 230);
            this.txtRearUser.Name = "txtRearUser";
            this.txtRearUser.Size = new System.Drawing.Size(380, 29);
            this.txtRearUser.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label6.Location = new System.Drawing.Point(22, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Tên đăng nhập:";
            // 
            // txtRearPort
            // 
            this.txtRearPort.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRearPort.Location = new System.Drawing.Point(26, 150);
            this.txtRearPort.Name = "txtRearPort";
            this.txtRearPort.Size = new System.Drawing.Size(380, 29);
            this.txtRearPort.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label7.Location = new System.Drawing.Point(22, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cổng (Port):";
            // 
            // txtRearIP
            // 
            this.txtRearIP.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRearIP.Location = new System.Drawing.Point(26, 70);
            this.txtRearIP.Name = "txtRearIP";
            this.txtRearIP.Size = new System.Drawing.Size(380, 29);
            this.txtRearIP.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label8.Location = new System.Drawing.Point(22, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Địa chỉ IP: *";
            // 
            // grpCamFront
            // 
            this.grpCamFront.BackColor = System.Drawing.Color.White;
            this.grpCamFront.Controls.Add(this.txtFrontPass);
            this.grpCamFront.Controls.Add(this.label4);
            this.grpCamFront.Controls.Add(this.txtFrontUser);
            this.grpCamFront.Controls.Add(this.label3);
            this.grpCamFront.Controls.Add(this.txtFrontPort);
            this.grpCamFront.Controls.Add(this.label2);
            this.grpCamFront.Controls.Add(this.txtFrontIP);
            this.grpCamFront.Controls.Add(this.label1);
            this.grpCamFront.Location = new System.Drawing.Point(30, 30);
            this.grpCamFront.Name = "grpCamFront";
            this.grpCamFront.Size = new System.Drawing.Size(440, 390);
            this.grpCamFront.TabIndex = 0;
            this.grpCamFront.TabStop = false;
            this.grpCamFront.Text = "CAMERA TRƯỚC (RTSP - LÀN VÀO)";
            // 
            // txtFrontPass
            // 
            this.txtFrontPass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFrontPass.Location = new System.Drawing.Point(26, 310);
            this.txtFrontPass.Name = "txtFrontPass";
            this.txtFrontPass.Size = new System.Drawing.Size(380, 29);
            this.txtFrontPass.TabIndex = 7;
            this.txtFrontPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label4.Location = new System.Drawing.Point(22, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mật khẩu:";
            // 
            // txtFrontUser
            // 
            this.txtFrontUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFrontUser.Location = new System.Drawing.Point(26, 230);
            this.txtFrontUser.Name = "txtFrontUser";
            this.txtFrontUser.Size = new System.Drawing.Size(380, 29);
            this.txtFrontUser.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label3.Location = new System.Drawing.Point(22, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên đăng nhập:";
            // 
            // txtFrontPort
            // 
            this.txtFrontPort.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFrontPort.Location = new System.Drawing.Point(26, 150);
            this.txtFrontPort.Name = "txtFrontPort";
            this.txtFrontPort.Size = new System.Drawing.Size(380, 29);
            this.txtFrontPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label2.Location = new System.Drawing.Point(22, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cổng (Port):";
            // 
            // txtFrontIP
            // 
            this.txtFrontIP.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFrontIP.Location = new System.Drawing.Point(26, 70);
            this.txtFrontIP.Name = "txtFrontIP";
            this.txtFrontIP.Size = new System.Drawing.Size(380, 29);
            this.txtFrontIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label1.Location = new System.Drawing.Point(22, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Địa chỉ IP: *";
            // 
            // tabData
            // 
            this.tabData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabData.Controls.Add(this.btnResetDb);
            this.tabData.Controls.Add(this.grpCrudActions);
            this.tabData.Controls.Add(this.dgvAdminData);
            this.tabData.Controls.Add(this.cbTables);
            this.tabData.Controls.Add(this.label9);
            this.tabData.Location = new System.Drawing.Point(4, 30);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(15);
            this.tabData.Size = new System.Drawing.Size(976, 567);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "Quản Trị Cơ Sở Dữ Liệu (CRUD)";
            // 
            // btnResetDb
            // 
            this.btnResetDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnResetDb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetDb.FlatAppearance.BorderSize = 0;
            this.btnResetDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetDb.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnResetDb.ForeColor = System.Drawing.Color.White;
            this.btnResetDb.Location = new System.Drawing.Point(746, 12);
            this.btnResetDb.Name = "btnResetDb";
            this.btnResetDb.Size = new System.Drawing.Size(215, 36);
            this.btnResetDb.TabIndex = 4;
            this.btnResetDb.Text = "DỌN DẸP BÃI XE (RESET)";
            this.btnResetDb.UseVisualStyleBackColor = false;
            this.btnResetDb.Click += new System.EventHandler(this.BtnResetDb_Click);
            // 
            // grpCrudActions
            // 
            this.grpCrudActions.BackColor = System.Drawing.Color.White;
            this.grpCrudActions.Controls.Add(this.txtVal4);
            this.grpCrudActions.Controls.Add(this.lblVal4);
            this.grpCrudActions.Controls.Add(this.txtVal3);
            this.grpCrudActions.Controls.Add(this.lblVal3);
            this.grpCrudActions.Controls.Add(this.txtVal2);
            this.grpCrudActions.Controls.Add(this.lblVal2);
            this.grpCrudActions.Controls.Add(this.txtVal1);
            this.grpCrudActions.Controls.Add(this.lblVal1);
            this.grpCrudActions.Controls.Add(this.btnDelete);
            this.grpCrudActions.Controls.Add(this.btnEdit);
            this.grpCrudActions.Controls.Add(this.btnAdd);
            this.grpCrudActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpCrudActions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpCrudActions.Location = new System.Drawing.Point(15, 382);
            this.grpCrudActions.Name = "grpCrudActions";
            this.grpCrudActions.Size = new System.Drawing.Size(946, 170);
            this.grpCrudActions.TabIndex = 3;
            this.grpCrudActions.TabStop = false;
            this.grpCrudActions.Text = "BIỂU MẪU ĐIỀU CHỈNH THÔNG TIN";
            // 
            // txtVal4
            // 
            this.txtVal4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVal4.Location = new System.Drawing.Point(620, 120);
            this.txtVal4.Name = "txtVal4";
            this.txtVal4.Size = new System.Drawing.Size(300, 27);
            this.txtVal4.TabIndex = 11;
            // 
            // lblVal4
            // 
            this.lblVal4.AutoSize = true;
            this.lblVal4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVal4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVal4.Location = new System.Drawing.Point(616, 98);
            this.lblVal4.Name = "lblVal4";
            this.lblVal4.Size = new System.Drawing.Size(52, 19);
            this.lblVal4.TabIndex = 10;
            this.lblVal4.Text = "Nhãn 4";
            // 
            // txtVal3
            // 
            this.txtVal3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVal3.Location = new System.Drawing.Point(620, 55);
            this.txtVal3.Name = "txtVal3";
            this.txtVal3.Size = new System.Drawing.Size(300, 27);
            this.txtVal3.TabIndex = 9;
            // 
            // lblVal3
            // 
            this.lblVal3.AutoSize = true;
            this.lblVal3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVal3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVal3.Location = new System.Drawing.Point(616, 33);
            this.lblVal3.Name = "lblVal3";
            this.lblVal3.Size = new System.Drawing.Size(52, 19);
            this.lblVal3.TabIndex = 8;
            this.lblVal3.Text = "Nhãn 3";
            // 
            // txtVal2
            // 
            this.txtVal2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVal2.Location = new System.Drawing.Point(280, 55);
            this.txtVal2.Name = "txtVal2";
            this.txtVal2.Size = new System.Drawing.Size(300, 27);
            this.txtVal2.TabIndex = 7;
            // 
            // lblVal2
            // 
            this.lblVal2.AutoSize = true;
            this.lblVal2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVal2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVal2.Location = new System.Drawing.Point(276, 33);
            this.lblVal2.Name = "lblVal2";
            this.lblVal2.Size = new System.Drawing.Size(52, 19);
            this.lblVal2.TabIndex = 6;
            this.lblVal2.Text = "Nhãn 2";
            // 
            // txtVal1
            // 
            this.txtVal1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVal1.Location = new System.Drawing.Point(20, 55);
            this.txtVal1.Name = "txtVal1";
            this.txtVal1.Size = new System.Drawing.Size(230, 27);
            this.txtVal1.TabIndex = 5;
            // 
            // lblVal1
            // 
            this.lblVal1.AutoSize = true;
            this.lblVal1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVal1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVal1.Location = new System.Drawing.Point(16, 33);
            this.lblVal1.Name = "lblVal1";
            this.lblVal1.Size = new System.Drawing.Size(52, 19);
            this.lblVal1.TabIndex = 4;
            this.lblVal1.Text = "Nhãn 1";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(400, 110);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "XÓA BỎ";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(210, 110);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "CẬP NHẬT";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 110);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "THÊM MỚI";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // dgvAdminData
            // 
            this.dgvAdminData.AllowUserToAddRows = false;
            this.dgvAdminData.AllowUserToDeleteRows = false;
            this.dgvAdminData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdminData.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdminData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminData.Location = new System.Drawing.Point(15, 60);
            this.dgvAdminData.MultiSelect = false;
            this.dgvAdminData.Name = "dgvAdminData";
            this.dgvAdminData.ReadOnly = true;
            this.dgvAdminData.RowHeadersVisible = false;
            this.dgvAdminData.RowTemplate.Height = 25;
            this.dgvAdminData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdminData.Size = new System.Drawing.Size(946, 310);
            this.dgvAdminData.TabIndex = 2;
            this.dgvAdminData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAdminData_CellClick);
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(180, 15);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(240, 29);
            this.cbTables.TabIndex = 1;
            this.cbTables.SelectedIndexChanged += new System.EventHandler(this.CbTables_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Chọn bảng hiển thị:";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tabControlAdmin);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Parking - Bảng Quản Trị Hệ Thống";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminForm_FormClosing);
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControlAdmin.ResumeLayout(false);
            this.tabCamera.ResumeLayout(false);
            this.grpCamRear.ResumeLayout(false);
            this.grpCamRear.PerformLayout();
            this.grpCamFront.ResumeLayout(false);
            this.grpCamFront.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            this.grpCrudActions.ResumeLayout(false);
            this.grpCrudActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabCamera;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.GroupBox grpCamFront;
        private System.Windows.Forms.TextBox txtFrontPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFrontUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrontPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrontIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpCamRear;
        private System.Windows.Forms.TextBox txtRearPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRearUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRearPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRearIP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvAdminData;
        private System.Windows.Forms.GroupBox grpCrudActions;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtVal4;
        private System.Windows.Forms.Label lblVal4;
        private System.Windows.Forms.TextBox txtVal3;
        private System.Windows.Forms.Label lblVal3;
        private System.Windows.Forms.TextBox txtVal2;
        private System.Windows.Forms.Label lblVal2;
        private System.Windows.Forms.TextBox txtVal1;
        private System.Windows.Forms.Label lblVal1;
        private System.Windows.Forms.Button btnResetDb;
    }
}
