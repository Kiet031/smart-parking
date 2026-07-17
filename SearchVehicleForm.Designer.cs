using System.Drawing;
using System.Windows.Forms;

namespace SmartParking
{
    partial class SearchVehicleForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colCardId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntryTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pnlFilterDropdown = new System.Windows.Forms.Panel();
            this.chkCasual = new System.Windows.Forms.CheckBox();
            this.chkMonthly = new System.Windows.Forms.CheckBox();
            this.chkInYard = new System.Windows.Forms.CheckBox();
            this.chkOutYard = new System.Windows.Forms.CheckBox();
            this.pnlMemberDemographics = new System.Windows.Forms.Panel();
            this.lblDemographicsTitle = new System.Windows.Forms.Label();
            this.lblLicensePlate = new System.Windows.Forms.Label();
            this.lblVehicleInfo = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.lblMemberId = new System.Windows.Forms.Label();
            this.pbMemberPortrait = new System.Windows.Forms.PictureBox();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.btnFilterToggle = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.lblSearchTerm = new System.Windows.Forms.Label();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.tlpCameraGrid = new System.Windows.Forms.TableLayoutPanel();
            this.pnlExitRear = new System.Windows.Forms.Panel();
            this.pbExitRear = new System.Windows.Forms.PictureBox();
            this.lblExitRearTitle = new System.Windows.Forms.Label();
            this.pnlExitFront = new System.Windows.Forms.Panel();
            this.pbExitFront = new System.Windows.Forms.PictureBox();
            this.lblExitFrontTitle = new System.Windows.Forms.Label();
            this.pnlEntryRear = new System.Windows.Forms.Panel();
            this.pbEntryRear = new System.Windows.Forms.PictureBox();
            this.lblEntryRearTitle = new System.Windows.Forms.Label();
            this.pnlEntryFront = new System.Windows.Forms.Panel();
            this.pbEntryFront = new System.Windows.Forms.PictureBox();
            this.lblEntryFrontTitle = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.pnlResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.pnlPagination.SuspendLayout();
            this.pnlFilterDropdown.SuspendLayout();
            this.pnlMemberDemographics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMemberPortrait)).BeginInit();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tlpCameraGrid.SuspendLayout();
            this.pnlExitRear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExitRear)).BeginInit();
            this.pnlExitFront.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExitFront)).BeginInit();
            this.pnlEntryRear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryRear)).BeginInit();
            this.pnlEntryFront.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryFront)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlLeft.Controls.Add(this.pnlResults);
            this.pnlLeft.Controls.Add(this.pnlFilterDropdown);
            this.pnlLeft.Controls.Add(this.pnlMemberDemographics);
            this.pnlLeft.Controls.Add(this.pnlSearchHeader);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(530, 768);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlResults
            // 
            this.pnlResults.Controls.Add(this.dgvResults);
            this.pnlResults.Controls.Add(this.pnlPagination);
            this.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResults.Location = new System.Drawing.Point(0, 310);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(530, 458);
            this.pnlResults.TabIndex = 2;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.ColumnHeadersHeight = 35;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardId,
            this.colCardType,
            this.colEntryTime,
            this.colExitTime,
            this.colPlate,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 32;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(530, 413);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvResults_CellClick);
            // 
            // colCardId
            // 
            this.colCardId.DataPropertyName = "card_id";
            this.colCardId.HeaderText = "Mã thẻ RFID";
            this.colCardId.Name = "colCardId";
            this.colCardId.ReadOnly = true;
            this.colCardId.Width = 90;
            // 
            // colCardType
            // 
            this.colCardType.DataPropertyName = "card_type_name";
            this.colCardType.HeaderText = "Loại vé";
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            this.colCardType.Width = 90;
            // 
            // colEntryTime
            // 
            this.colEntryTime.DataPropertyName = "entry_time";
            this.colEntryTime.HeaderText = "Thời gian vào";
            this.colEntryTime.Name = "colEntryTime";
            this.colEntryTime.ReadOnly = true;
            this.colEntryTime.Width = 110;
            // 
            // colExitTime
            // 
            this.colExitTime.DataPropertyName = "exit_time";
            this.colExitTime.HeaderText = "Thời gian ra";
            this.colExitTime.Name = "colExitTime";
            this.colExitTime.ReadOnly = true;
            this.colExitTime.Width = 110;
            // 
            // colPlate
            // 
            this.colPlate.DataPropertyName = "license_plate";
            this.colPlate.HeaderText = "Biển số";
            this.colPlate.Name = "colPlate";
            this.colPlate.ReadOnly = true;
            this.colPlate.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 50;
            // 
            // pnlPagination
            // 
            this.pnlPagination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlPagination.Controls.Add(this.lblPageInfo);
            this.pnlPagination.Controls.Add(this.btnNext);
            this.pnlPagination.Controls.Add(this.btnPrev);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 413);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(530, 45);
            this.pnlPagination.TabIndex = 1;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPageInfo.Location = new System.Drawing.Point(170, 10);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(190, 25);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Trang 1 / 1 (Tổng: 0)";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(375, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(85, 30);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Sau ▶";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.Location = new System.Drawing.Point(70, 7);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(85, 30);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "◀ Trước";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // pnlFilterDropdown
            // 
            this.pnlFilterDropdown.BackColor = System.Drawing.Color.White;
            this.pnlFilterDropdown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterDropdown.Controls.Add(this.chkOutYard);
            this.pnlFilterDropdown.Controls.Add(this.chkInYard);
            this.pnlFilterDropdown.Controls.Add(this.chkCasual);
            this.pnlFilterDropdown.Controls.Add(this.chkMonthly);
            this.pnlFilterDropdown.Location = new System.Drawing.Point(400, 115);
            this.pnlFilterDropdown.Name = "pnlFilterDropdown";
            this.pnlFilterDropdown.Size = new System.Drawing.Size(115, 68);
            this.pnlFilterDropdown.TabIndex = 3;
            this.pnlFilterDropdown.Visible = false;
            // 
            // chkCasual
            // 
            this.chkCasual.AutoSize = true;
            this.chkCasual.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkCasual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkCasual.Location = new System.Drawing.Point(10, 36);
            this.chkCasual.Name = "chkCasual";
            this.chkCasual.Size = new System.Drawing.Size(73, 21);
            this.chkCasual.TabIndex = 1;
            this.chkCasual.Text = "Thẻ lượt";
            this.chkCasual.UseVisualStyleBackColor = true;
            this.chkCasual.CheckedChanged += new System.EventHandler(this.FilterCheckbox_CheckedChanged);
            // 
            // chkMonthly
            // 
            this.chkMonthly.AutoSize = true;
            this.chkMonthly.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkMonthly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkMonthly.Location = new System.Drawing.Point(10, 10);
            this.chkMonthly.Name = "chkMonthly";
            this.chkMonthly.Size = new System.Drawing.Size(86, 21);
            this.chkMonthly.TabIndex = 0;
            this.chkMonthly.Text = "Thẻ tháng";
            this.chkMonthly.UseVisualStyleBackColor = true;
            this.chkMonthly.CheckedChanged += new System.EventHandler(this.FilterCheckbox_CheckedChanged);
            // 
            // chkInYard
            // 
            this.chkInYard.AutoSize = true;
            this.chkInYard.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkInYard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkInYard.Location = new System.Drawing.Point(10, 62);
            this.chkInYard.Name = "chkInYard";
            this.chkInYard.Size = new System.Drawing.Size(80, 21);
            this.chkInYard.TabIndex = 2;
            this.chkInYard.Text = "Trong bãi";
            this.chkInYard.UseVisualStyleBackColor = true;
            this.chkInYard.CheckedChanged += new System.EventHandler(this.FilterCheckbox_CheckedChanged);
            // 
            // chkOutYard
            // 
            this.chkOutYard.AutoSize = true;
            this.chkOutYard.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkOutYard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkOutYard.Location = new System.Drawing.Point(10, 88);
            this.chkOutYard.Name = "chkOutYard";
            this.chkOutYard.Size = new System.Drawing.Size(120, 21);
            this.chkOutYard.TabIndex = 3;
            this.chkOutYard.Text = "Không trong bãi";
            this.chkOutYard.UseVisualStyleBackColor = true;
            this.chkOutYard.CheckedChanged += new System.EventHandler(this.FilterCheckbox_CheckedChanged);
            // 
            // pnlMemberDemographics
            // 
            this.pnlMemberDemographics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlMemberDemographics.Controls.Add(this.lblDemographicsTitle);
            this.pnlMemberDemographics.Controls.Add(this.lblLicensePlate);
            this.pnlMemberDemographics.Controls.Add(this.lblVehicleInfo);
            this.pnlMemberDemographics.Controls.Add(this.lblFullName);
            this.pnlMemberDemographics.Controls.Add(this.lblUserCode);
            this.pnlMemberDemographics.Controls.Add(this.lblMemberId);
            this.pnlMemberDemographics.Controls.Add(this.pbMemberPortrait);
            this.pnlMemberDemographics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMemberDemographics.Location = new System.Drawing.Point(0, 130);
            this.pnlMemberDemographics.Name = "pnlMemberDemographics";
            this.pnlMemberDemographics.Size = new System.Drawing.Size(530, 180);
            this.pnlMemberDemographics.TabIndex = 1;
            this.pnlMemberDemographics.Visible = false;
            // 
            // lblDemographicsTitle
            // 
            this.lblDemographicsTitle.AutoSize = true;
            this.lblDemographicsTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDemographicsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDemographicsTitle.Location = new System.Drawing.Point(12, 5);
            this.lblDemographicsTitle.Name = "lblDemographicsTitle";
            this.lblDemographicsTitle.Size = new System.Drawing.Size(155, 17);
            this.lblDemographicsTitle.TabIndex = 6;
            this.lblDemographicsTitle.Text = "Đăng Ký Thuê Bao Tháng";
            // 
            // lblLicensePlate
            // 
            this.lblLicensePlate.AutoSize = true;
            this.lblLicensePlate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLicensePlate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLicensePlate.Location = new System.Drawing.Point(145, 148);
            this.lblLicensePlate.Name = "lblLicensePlate";
            this.lblLicensePlate.Size = new System.Drawing.Size(130, 17);
            this.lblLicensePlate.TabIndex = 5;
            this.lblLicensePlate.Text = "Biển số đăng ký: ---";
            // 
            // lblVehicleInfo
            // 
            this.lblVehicleInfo.AutoSize = true;
            this.lblVehicleInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVehicleInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehicleInfo.Location = new System.Drawing.Point(145, 118);
            this.lblVehicleInfo.Name = "lblVehicleInfo";
            this.lblVehicleInfo.Size = new System.Drawing.Size(107, 17);
            this.lblVehicleInfo.TabIndex = 4;
            this.lblVehicleInfo.Text = "Thông tin xe: ---";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblFullName.Location = new System.Drawing.Point(145, 88);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(92, 17);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "Họ và tên: ---";
            // 
            // lblUserCode
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUserCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblUserCode.Location = new System.Drawing.Point(145, 58);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(68, 17);
            this.lblUserCode.TabIndex = 2;
            this.lblUserCode.Text = "Mã số: ---";
            // 
            // lblMemberId
            // 
            this.lblMemberId.AutoSize = true;
            this.lblMemberId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMemberId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMemberId.Location = new System.Drawing.Point(145, 28);
            this.lblMemberId.Name = "lblMemberId";
            this.lblMemberId.Size = new System.Drawing.Size(121, 17);
            this.lblMemberId.TabIndex = 1;
            this.lblMemberId.Text = "Mã thành viên: ---";
            // 
            // pbMemberPortrait
            // 
            this.pbMemberPortrait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pbMemberPortrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMemberPortrait.Location = new System.Drawing.Point(15, 28);
            this.pbMemberPortrait.Name = "pbMemberPortrait";
            this.pbMemberPortrait.Size = new System.Drawing.Size(110, 137);
            this.pbMemberPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMemberPortrait.TabIndex = 0;
            this.pbMemberPortrait.TabStop = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSearchHeader.Controls.Add(this.btnFilterToggle);
            this.pnlSearchHeader.Controls.Add(this.btnSearch);
            this.pnlSearchHeader.Controls.Add(this.cbSearchType);
            this.pnlSearchHeader.Controls.Add(this.lblSearchType);
            this.pnlSearchHeader.Controls.Add(this.txtSearchTerm);
            this.pnlSearchHeader.Controls.Add(this.lblSearchTerm);
            this.pnlSearchHeader.Controls.Add(this.lblSearchTitle);
            this.pnlSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(530, 130);
            this.pnlSearchHeader.TabIndex = 0;
            // 
            // btnFilterToggle
            // 
            this.btnFilterToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnFilterToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterToggle.FlatAppearance.BorderSize = 0;
            this.btnFilterToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterToggle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterToggle.ForeColor = System.Drawing.Color.White;
            this.btnFilterToggle.Location = new System.Drawing.Point(400, 83);
            this.btnFilterToggle.Name = "btnFilterToggle";
            this.btnFilterToggle.Size = new System.Drawing.Size(115, 28);
            this.btnFilterToggle.TabIndex = 5;
            this.btnFilterToggle.Text = "⚙️ Bộ lọc";
            this.btnFilterToggle.UseVisualStyleBackColor = false;
            this.btnFilterToggle.Click += new System.EventHandler(this.BtnFilterToggle_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(270, 83);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cbSearchType
            // 
            this.cbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Location = new System.Drawing.Point(85, 84);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(175, 25);
            this.cbSearchType.TabIndex = 3;
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.CbSearchType_SelectedIndexChanged);
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchType.ForeColor = System.Drawing.Color.White;
            this.lblSearchType.Location = new System.Drawing.Point(12, 87);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(61, 17);
            this.lblSearchType.TabIndex = 2;
            this.lblSearchType.Text = "Tiêu chí:";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchTerm.Location = new System.Drawing.Point(85, 45);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(430, 25);
            this.txtSearchTerm.TabIndex = 1;
            this.txtSearchTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchTerm_KeyDown);
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.AutoSize = true;
            this.lblSearchTerm.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchTerm.ForeColor = System.Drawing.Color.White;
            this.lblSearchTerm.Location = new System.Drawing.Point(12, 48);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(63, 17);
            this.lblSearchTerm.TabIndex = 0;
            this.lblSearchTerm.Text = "Từ khóa:";
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblSearchTitle.Location = new System.Drawing.Point(12, 10);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(325, 21);
            this.lblSearchTitle.TabIndex = 0;
            this.lblSearchTitle.Text = "TÌM KIẾM XE & ĐỐI CHIẾU LỊCH SỬ GIAO DỊCH";
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlRight.Controls.Add(this.tlpCameraGrid);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(530, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(5);
            this.pnlRight.Size = new System.Drawing.Size(750, 768);
            this.pnlRight.TabIndex = 1;
            // 
            // tlpCameraGrid
            // 
            this.tlpCameraGrid.ColumnCount = 2;
            this.tlpCameraGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCameraGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCameraGrid.Controls.Add(this.pnlExitRear, 0, 1);
            this.tlpCameraGrid.Controls.Add(this.pnlExitFront, 0, 0);
            this.tlpCameraGrid.Controls.Add(this.pnlEntryRear, 1, 1);
            this.tlpCameraGrid.Controls.Add(this.pnlEntryFront, 1, 0);
            this.tlpCameraGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCameraGrid.Location = new System.Drawing.Point(5, 5);
            this.tlpCameraGrid.Name = "tlpCameraGrid";
            this.tlpCameraGrid.RowCount = 2;
            this.tlpCameraGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCameraGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCameraGrid.Size = new System.Drawing.Size(740, 758);
            this.tlpCameraGrid.TabIndex = 0;
            // 
            // pnlExitRear
            // 
            this.pnlExitRear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlExitRear.Controls.Add(this.pbExitRear);
            this.pnlExitRear.Controls.Add(this.lblExitRearTitle);
            this.pnlExitRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExitRear.Location = new System.Drawing.Point(3, 382);
            this.pnlExitRear.Name = "pnlExitRear";
            this.pnlExitRear.Padding = new System.Windows.Forms.Padding(3);
            this.pnlExitRear.Size = new System.Drawing.Size(364, 373);
            this.pnlExitRear.TabIndex = 3;
            // 
            // pbExitRear
            // 
            this.pbExitRear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pbExitRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbExitRear.Location = new System.Drawing.Point(3, 28);
            this.pbExitRear.Name = "pbExitRear";
            this.pbExitRear.Size = new System.Drawing.Size(358, 342);
            this.pbExitRear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExitRear.TabIndex = 1;
            this.pbExitRear.TabStop = false;
            // 
            // lblExitRearTitle
            // 
            this.lblExitRearTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblExitRearTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExitRearTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblExitRearTitle.ForeColor = System.Drawing.Color.White;
            this.lblExitRearTitle.Location = new System.Drawing.Point(3, 3);
            this.lblExitRearTitle.Name = "lblExitRearTitle";
            this.lblExitRearTitle.Size = new System.Drawing.Size(358, 25);
            this.lblExitRearTitle.TabIndex = 0;
            this.lblExitRearTitle.Text = "CAMERA SAU - LÀN RA";
            this.lblExitRearTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlExitFront
            // 
            this.pnlExitFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlExitFront.Controls.Add(this.pbExitFront);
            this.pnlExitFront.Controls.Add(this.lblExitFrontTitle);
            this.pnlExitFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExitFront.Location = new System.Drawing.Point(3, 3);
            this.pnlExitFront.Name = "pnlExitFront";
            this.pnlExitFront.Padding = new System.Windows.Forms.Padding(3);
            this.pnlExitFront.Size = new System.Drawing.Size(364, 373);
            this.pnlExitFront.TabIndex = 2;
            // 
            // pbExitFront
            // 
            this.pbExitFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pbExitFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbExitFront.Location = new System.Drawing.Point(3, 28);
            this.pbExitFront.Name = "pbExitFront";
            this.pbExitFront.Size = new System.Drawing.Size(358, 342);
            this.pbExitFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExitFront.TabIndex = 1;
            this.pbExitFront.TabStop = false;
            // 
            // lblExitFrontTitle
            // 
            this.lblExitFrontTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblExitFrontTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExitFrontTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblExitFrontTitle.ForeColor = System.Drawing.Color.White;
            this.lblExitFrontTitle.Location = new System.Drawing.Point(3, 3);
            this.lblExitFrontTitle.Name = "lblExitFrontTitle";
            this.lblExitFrontTitle.Size = new System.Drawing.Size(358, 25);
            this.lblExitFrontTitle.TabIndex = 0;
            this.lblExitFrontTitle.Text = "CAMERA TRƯỚC - LÀN RA";
            this.lblExitFrontTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlEntryRear
            // 
            this.pnlEntryRear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlEntryRear.Controls.Add(this.pbEntryRear);
            this.pnlEntryRear.Controls.Add(this.lblEntryRearTitle);
            this.pnlEntryRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEntryRear.Location = new System.Drawing.Point(373, 382);
            this.pnlEntryRear.Name = "pnlEntryRear";
            this.pnlEntryRear.Padding = new System.Windows.Forms.Padding(3);
            this.pnlEntryRear.Size = new System.Drawing.Size(364, 373);
            this.pnlEntryRear.TabIndex = 1;
            // 
            // pbEntryRear
            // 
            this.pbEntryRear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pbEntryRear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbEntryRear.Location = new System.Drawing.Point(3, 28);
            this.pbEntryRear.Name = "pbEntryRear";
            this.pbEntryRear.Size = new System.Drawing.Size(358, 342);
            this.pbEntryRear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEntryRear.TabIndex = 1;
            this.pbEntryRear.TabStop = false;
            // 
            // lblEntryRearTitle
            // 
            this.lblEntryRearTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEntryRearTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntryRearTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryRearTitle.ForeColor = System.Drawing.Color.White;
            this.lblEntryRearTitle.Location = new System.Drawing.Point(3, 3);
            this.lblEntryRearTitle.Name = "lblEntryRearTitle";
            this.lblEntryRearTitle.Size = new System.Drawing.Size(358, 25);
            this.lblEntryRearTitle.TabIndex = 0;
            this.lblEntryRearTitle.Text = "CAMERA SAU - LÀN VÀO";
            this.lblEntryRearTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlEntryFront
            // 
            this.pnlEntryFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlEntryFront.Controls.Add(this.pbEntryFront);
            this.pnlEntryFront.Controls.Add(this.lblEntryFrontTitle);
            this.pnlEntryFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEntryFront.Location = new System.Drawing.Point(373, 3);
            this.pnlEntryFront.Name = "pnlEntryFront";
            this.pnlEntryFront.Padding = new System.Windows.Forms.Padding(3);
            this.pnlEntryFront.Size = new System.Drawing.Size(364, 373);
            this.pnlEntryFront.TabIndex = 0;
            // 
            // pbEntryFront
            // 
            this.pbEntryFront.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pbEntryFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbEntryFront.Location = new System.Drawing.Point(3, 28);
            this.pbEntryFront.Name = "pbEntryFront";
            this.pbEntryFront.Size = new System.Drawing.Size(358, 342);
            this.pbEntryFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEntryFront.TabIndex = 1;
            this.pbEntryFront.TabStop = false;
            // 
            // lblEntryFrontTitle
            // 
            this.lblEntryFrontTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEntryFrontTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntryFrontTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryFrontTitle.ForeColor = System.Drawing.Color.White;
            this.lblEntryFrontTitle.Location = new System.Drawing.Point(3, 3);
            this.lblEntryFrontTitle.Name = "lblEntryFrontTitle";
            this.lblEntryFrontTitle.Size = new System.Drawing.Size(358, 25);
            this.lblEntryFrontTitle.TabIndex = 0;
            this.lblEntryFrontTitle.Text = "CAMERA TRƯỚC - LÀN VÀO";
            this.lblEntryFrontTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchVehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 768);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchVehicleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm kiếm xe & Đối chiếu lịch sử";
            this.Load += new System.EventHandler(this.SearchVehicleForm_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.pnlPagination.ResumeLayout(false);
            this.pnlFilterDropdown.ResumeLayout(false);
            this.pnlFilterDropdown.PerformLayout();
            this.pnlMemberDemographics.ResumeLayout(false);
            this.pnlMemberDemographics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMemberPortrait)).EndInit();
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.tlpCameraGrid.ResumeLayout(false);
            this.pnlExitRear.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExitRear)).EndInit();
            this.pnlExitFront.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExitFront)).EndInit();
            this.pnlEntryRear.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryRear)).EndInit();
            this.pnlEntryFront.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEntryFront)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Panel pnlLeft;
        private Panel pnlRight;
        private Panel pnlSearchHeader;
        private Label lblSearchTitle;
        private Label lblSearchTerm;
        private TextBox txtSearchTerm;
        private Label lblSearchType;
        private ComboBox cbSearchType;
        private Button btnSearch;
        private Button btnFilterToggle;
        private Panel pnlFilterDropdown;
        private CheckBox chkCasual;
        private CheckBox chkMonthly;
        private CheckBox chkInYard;
        private CheckBox chkOutYard;
        private Panel pnlMemberDemographics;
        private PictureBox pbMemberPortrait;
        private Label lblMemberId;
        private Label lblUserCode;
        private Label lblFullName;
        private Label lblVehicleInfo;
        private Label lblLicensePlate;
        private Panel pnlResults;
        private DataGridView dgvResults;
        private Panel pnlPagination;
        private Button btnPrev;
        private Button btnNext;
        private Label lblPageInfo;
        private TableLayoutPanel tlpCameraGrid;
        private Panel pnlEntryFront;
        private PictureBox pbEntryFront;
        private Label lblEntryFrontTitle;
        private Panel pnlEntryRear;
        private PictureBox pbEntryRear;
        private Label lblEntryRearTitle;
        private Panel pnlExitFront;
        private PictureBox pbExitFront;
        private Label lblExitFrontTitle;
        private Panel pnlExitRear;
        private PictureBox pbExitRear;
        private Label lblExitRearTitle;
        private Label lblDemographicsTitle;
        
        private DataGridViewTextBoxColumn colCardId;
        private DataGridViewTextBoxColumn colCardType;
        private DataGridViewTextBoxColumn colEntryTime;
        private DataGridViewTextBoxColumn colExitTime;
        private DataGridViewTextBoxColumn colPlate;
        private DataGridViewTextBoxColumn colStatus;
    }
}
