using System.Drawing;
using System.Windows.Forms;

namespace SmartParking
{
    partial class SubscriptionRegistrationForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.cbRoleGroup = new System.Windows.Forms.ComboBox();
            this.lblRoleGroup = new System.Windows.Forms.Label();
            this.txtLicensePlate = new System.Windows.Forms.TextBox();
            this.lblLicensePlate = new System.Windows.Forms.Label();
            this.txtVehicleInfo = new System.Windows.Forms.TextBox();
            this.lblVehicleInfo = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.txtCardId = new System.Windows.Forms.TextBox();
            this.lblCardId = new System.Windows.Forms.Label();
            this.gbImageSystem = new System.Windows.Forms.GroupBox();
            this.btnCaptureVehicle = new System.Windows.Forms.Button();
            this.btnChooseVehicle = new System.Windows.Forms.Button();
            this.btnCapturePortrait = new System.Windows.Forms.Button();
            this.btnChoosePortrait = new System.Windows.Forms.Button();
            this.lblVehiclePreview = new System.Windows.Forms.Label();
            this.lblPortraitPreview = new System.Windows.Forms.Label();
            this.pbVehiclePreview = new System.Windows.Forms.PictureBox();
            this.pbPortraitPreview = new System.Windows.Forms.PictureBox();
            this.cbCameraSelect = new System.Windows.Forms.ComboBox();
            this.lblCameraSelect = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbInfo.SuspendLayout();
            this.gbImageSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVehiclePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortraitPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(920, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐĂNG KÝ THÀNH VIÊN VÉ THÁNG MỚI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.cbRoleGroup);
            this.gbInfo.Controls.Add(this.lblRoleGroup);
            this.gbInfo.Controls.Add(this.txtLicensePlate);
            this.gbInfo.Controls.Add(this.lblLicensePlate);
            this.gbInfo.Controls.Add(this.txtVehicleInfo);
            this.gbInfo.Controls.Add(this.lblVehicleInfo);
            this.gbInfo.Controls.Add(this.dtpBirthDate);
            this.gbInfo.Controls.Add(this.lblBirthDate);
            this.gbInfo.Controls.Add(this.txtFullName);
            this.gbInfo.Controls.Add(this.lblFullName);
            this.gbInfo.Controls.Add(this.txtUserCode);
            this.gbInfo.Controls.Add(this.lblUserCode);
            this.gbInfo.Controls.Add(this.txtCardId);
            this.gbInfo.Controls.Add(this.lblCardId);
            this.gbInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.gbInfo.Location = new System.Drawing.Point(16, 63);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(430, 437);
            this.gbInfo.TabIndex = 1;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Thông Tin Thành Viên";
            // 
            // cbRoleGroup
            // 
            this.cbRoleGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoleGroup.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cbRoleGroup.FormattingEnabled = true;
            this.cbRoleGroup.Items.AddRange(new object[] {
            "Sinh viên",
            "Giảng viên",
            "Nhân viên"});
            this.cbRoleGroup.Location = new System.Drawing.Point(165, 382);
            this.cbRoleGroup.Name = "cbRoleGroup";
            this.cbRoleGroup.Size = new System.Drawing.Size(245, 29);
            this.cbRoleGroup.TabIndex = 13;
            // 
            // lblRoleGroup
            // 
            this.lblRoleGroup.AutoSize = true;
            this.lblRoleGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRoleGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblRoleGroup.Location = new System.Drawing.Point(15, 385);
            this.lblRoleGroup.Name = "lblRoleGroup";
            this.lblRoleGroup.Size = new System.Drawing.Size(127, 21);
            this.lblRoleGroup.TabIndex = 12;
            this.lblRoleGroup.Text = "Nhóm đối tượng";
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLicensePlate.Location = new System.Drawing.Point(165, 332);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Size = new System.Drawing.Size(245, 29);
            this.txtLicensePlate.TabIndex = 11;
            this.txtLicensePlate.TextChanged += new System.EventHandler(this.TxtLicensePlate_TextChanged);
            // 
            // lblLicensePlate
            // 
            this.lblLicensePlate.AutoSize = true;
            this.lblLicensePlate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLicensePlate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLicensePlate.Location = new System.Drawing.Point(15, 335);
            this.lblLicensePlate.Name = "lblLicensePlate";
            this.lblLicensePlate.Size = new System.Drawing.Size(89, 21);
            this.lblLicensePlate.TabIndex = 10;
            this.lblLicensePlate.Text = "Biển số xe";
            // 
            // txtVehicleInfo
            // 
            this.txtVehicleInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtVehicleInfo.Location = new System.Drawing.Point(165, 282);
            this.txtVehicleInfo.Name = "txtVehicleInfo";
            this.txtVehicleInfo.Size = new System.Drawing.Size(245, 29);
            this.txtVehicleInfo.TabIndex = 9;
            // 
            // lblVehicleInfo
            // 
            this.lblVehicleInfo.AutoSize = true;
            this.lblVehicleInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblVehicleInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblVehicleInfo.Location = new System.Drawing.Point(15, 285);
            this.lblVehicleInfo.Name = "lblVehicleInfo";
            this.lblVehicleInfo.Size = new System.Drawing.Size(84, 21);
            this.lblVehicleInfo.TabIndex = 8;
            this.lblVehicleInfo.Text = "Loại xe/Màu";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.CustomFormat = "dd/MM/yyyy";
            this.dtpBirthDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthDate.Location = new System.Drawing.Point(165, 232);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(245, 29);
            this.dtpBirthDate.TabIndex = 7;
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBirthDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBirthDate.Location = new System.Drawing.Point(15, 238);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(87, 21);
            this.lblBirthDate.TabIndex = 6;
            this.lblBirthDate.Text = "Ngày sinh";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFullName.Location = new System.Drawing.Point(165, 182);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(245, 29);
            this.txtFullName.TabIndex = 5;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblFullName.Location = new System.Drawing.Point(15, 185);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(83, 21);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "Họ và tên";
            // 
            // txtUserCode
            // 
            this.txtUserCode.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUserCode.Location = new System.Drawing.Point(165, 132);
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(245, 29);
            this.txtUserCode.TabIndex = 3;
            // 
            // lblUserCode
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblUserCode.Location = new System.Drawing.Point(15, 135);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(139, 21);
            this.lblUserCode.TabIndex = 2;
            this.lblUserCode.Text = "Mã số định danh";
            // 
            // txtCardId
            // 
            this.txtCardId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCardId.Location = new System.Drawing.Point(165, 82);
            this.txtCardId.Name = "txtCardId";
            this.txtCardId.Size = new System.Drawing.Size(245, 29);
            this.txtCardId.TabIndex = 1;
            // 
            // lblCardId
            // 
            this.lblCardId.AutoSize = true;
            this.lblCardId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCardId.Location = new System.Drawing.Point(15, 85);
            this.lblCardId.Name = "lblCardId";
            this.lblCardId.Size = new System.Drawing.Size(106, 21);
            this.lblCardId.TabIndex = 0;
            this.lblCardId.Text = "Mã thẻ RFID";
            // 
            // gbImageSystem
            // 
            this.gbImageSystem.Controls.Add(this.btnCaptureVehicle);
            this.gbImageSystem.Controls.Add(this.btnChooseVehicle);
            this.gbImageSystem.Controls.Add(this.btnCapturePortrait);
            this.gbImageSystem.Controls.Add(this.btnChoosePortrait);
            this.gbImageSystem.Controls.Add(this.lblVehiclePreview);
            this.gbImageSystem.Controls.Add(this.lblPortraitPreview);
            this.gbImageSystem.Controls.Add(this.pbVehiclePreview);
            this.gbImageSystem.Controls.Add(this.pbPortraitPreview);
            this.gbImageSystem.Controls.Add(this.cbCameraSelect);
            this.gbImageSystem.Controls.Add(this.lblCameraSelect);
            this.gbImageSystem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbImageSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.gbImageSystem.Location = new System.Drawing.Point(461, 63);
            this.gbImageSystem.Name = "gbImageSystem";
            this.gbImageSystem.Size = new System.Drawing.Size(443, 437);
            this.gbImageSystem.TabIndex = 2;
            this.gbImageSystem.TabStop = false;
            this.gbImageSystem.Text = "Hình Ảnh Đăng Ký";
            // 
            // btnCaptureVehicle
            // 
            this.btnCaptureVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnCaptureVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureVehicle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCaptureVehicle.ForeColor = System.Drawing.Color.White;
            this.btnCaptureVehicle.Location = new System.Drawing.Point(327, 381);
            this.btnCaptureVehicle.Name = "btnCaptureVehicle";
            this.btnCaptureVehicle.Size = new System.Drawing.Size(99, 32);
            this.btnCaptureVehicle.TabIndex = 9;
            this.btnCaptureVehicle.Text = "Chụp từ Cam";
            this.btnCaptureVehicle.UseVisualStyleBackColor = false;
            this.btnCaptureVehicle.Click += new System.EventHandler(this.BtnCaptureVehicle_Click);
            // 
            // btnChooseVehicle
            // 
            this.btnChooseVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChooseVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseVehicle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnChooseVehicle.ForeColor = System.Drawing.Color.White;
            this.btnChooseVehicle.Location = new System.Drawing.Point(226, 381);
            this.btnChooseVehicle.Name = "btnChooseVehicle";
            this.btnChooseVehicle.Size = new System.Drawing.Size(95, 32);
            this.btnChooseVehicle.TabIndex = 8;
            this.btnChooseVehicle.Text = "Chọn file...";
            this.btnChooseVehicle.UseVisualStyleBackColor = false;
            this.btnChooseVehicle.Click += new System.EventHandler(this.BtnChooseVehicle_Click);
            // 
            // btnCapturePortrait
            // 
            this.btnCapturePortrait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnCapturePortrait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapturePortrait.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCapturePortrait.ForeColor = System.Drawing.Color.White;
            this.btnCapturePortrait.Location = new System.Drawing.Point(118, 381);
            this.btnCapturePortrait.Name = "btnCapturePortrait";
            this.btnCapturePortrait.Size = new System.Drawing.Size(99, 32);
            this.btnCapturePortrait.TabIndex = 7;
            this.btnCapturePortrait.Text = "Chụp từ Cam";
            this.btnCapturePortrait.UseVisualStyleBackColor = false;
            this.btnCapturePortrait.Click += new System.EventHandler(this.BtnCapturePortrait_Click);
            // 
            // btnChoosePortrait
            // 
            this.btnChoosePortrait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChoosePortrait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoosePortrait.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnChoosePortrait.ForeColor = System.Drawing.Color.White;
            this.btnChoosePortrait.Location = new System.Drawing.Point(17, 381);
            this.btnChoosePortrait.Name = "btnChoosePortrait";
            this.btnChoosePortrait.Size = new System.Drawing.Size(95, 32);
            this.btnChoosePortrait.TabIndex = 6;
            this.btnChoosePortrait.Text = "Chọn file...";
            this.btnChoosePortrait.UseVisualStyleBackColor = false;
            this.btnChoosePortrait.Click += new System.EventHandler(this.BtnChoosePortrait_Click);
            // 
            // lblVehiclePreview
            // 
            this.lblVehiclePreview.AutoSize = true;
            this.lblVehiclePreview.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVehiclePreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblVehiclePreview.Location = new System.Drawing.Point(222, 142);
            this.lblVehiclePreview.Name = "lblVehiclePreview";
            this.lblVehiclePreview.Size = new System.Drawing.Size(96, 20);
            this.lblVehiclePreview.TabIndex = 5;
            this.lblVehiclePreview.Text = "Ảnh Đăng Ký";
            // 
            // lblPortraitPreview
            // 
            this.lblPortraitPreview.AutoSize = true;
            this.lblPortraitPreview.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPortraitPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPortraitPreview.Location = new System.Drawing.Point(13, 142);
            this.lblPortraitPreview.Name = "lblPortraitPreview";
            this.lblPortraitPreview.Size = new System.Drawing.Size(107, 20);
            this.lblPortraitPreview.TabIndex = 4;
            this.lblPortraitPreview.Text = "Ảnh Chân Dung";
            // 
            // pbVehiclePreview
            // 
            this.pbVehiclePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pbVehiclePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVehiclePreview.Location = new System.Drawing.Point(226, 165);
            this.pbVehiclePreview.Name = "pbVehiclePreview";
            this.pbVehiclePreview.Size = new System.Drawing.Size(200, 200);
            this.pbVehiclePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVehiclePreview.TabIndex = 3;
            this.pbVehiclePreview.TabStop = false;
            // 
            // pbPortraitPreview
            // 
            this.pbPortraitPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pbPortraitPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPortraitPreview.Location = new System.Drawing.Point(17, 165);
            this.pbPortraitPreview.Name = "pbPortraitPreview";
            this.pbPortraitPreview.Size = new System.Drawing.Size(200, 200);
            this.pbPortraitPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPortraitPreview.TabIndex = 2;
            this.pbPortraitPreview.TabStop = false;
            // 
            // cbCameraSelect
            // 
            this.cbCameraSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameraSelect.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cbCameraSelect.FormattingEnabled = true;
            this.cbCameraSelect.Items.AddRange(new object[] {
            "Làn vào - Camera trước",
            "Làn vào - Camera sau",
            "Làn ra - Camera trước",
            "Làn ra - Camera sau"});
            this.cbCameraSelect.Location = new System.Drawing.Point(17, 85);
            this.cbCameraSelect.Name = "cbCameraSelect";
            this.cbCameraSelect.Size = new System.Drawing.Size(409, 29);
            this.cbCameraSelect.TabIndex = 1;
            // 
            // lblCameraSelect
            // 
            this.lblCameraSelect.AutoSize = true;
            this.lblCameraSelect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCameraSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCameraSelect.Location = new System.Drawing.Point(13, 56);
            this.lblCameraSelect.Name = "lblCameraSelect";
            this.lblCameraSelect.Size = new System.Drawing.Size(126, 21);
            this.lblCameraSelect.TabIndex = 0;
            this.lblCameraSelect.Text = "Chọn nguồn Cam";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(232, 513);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 48);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "LƯU ĐĂNG KÝ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(489, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 48);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "HỦY BỎ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SubscriptionRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 580);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbImageSystem);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubscriptionRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đăng ký thành viên vé tháng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubscriptionRegistrationForm_FormClosing);
            this.Load += new System.EventHandler(this.SubscriptionRegistrationForm_Load);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.gbImageSystem.ResumeLayout(false);
            this.gbImageSystem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVehiclePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortraitPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox txtCardId;
        private System.Windows.Forms.Label lblCardId;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.TextBox txtLicensePlate;
        private System.Windows.Forms.Label lblLicensePlate;
        private System.Windows.Forms.TextBox txtVehicleInfo;
        private System.Windows.Forms.Label lblVehicleInfo;
        private System.Windows.Forms.ComboBox cbRoleGroup;
        private System.Windows.Forms.Label lblRoleGroup;
        private System.Windows.Forms.GroupBox gbImageSystem;
        private System.Windows.Forms.ComboBox cbCameraSelect;
        private System.Windows.Forms.Label lblCameraSelect;
        private System.Windows.Forms.PictureBox pbVehiclePreview;
        private System.Windows.Forms.PictureBox pbPortraitPreview;
        private System.Windows.Forms.Label lblVehiclePreview;
        private System.Windows.Forms.Label lblPortraitPreview;
        private System.Windows.Forms.Button btnCaptureVehicle;
        private System.Windows.Forms.Button btnChooseVehicle;
        private System.Windows.Forms.Button btnCapturePortrait;
        private System.Windows.Forms.Button btnChoosePortrait;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
