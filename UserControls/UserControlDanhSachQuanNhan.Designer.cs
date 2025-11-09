namespace QuanLyDangVien.UserControls
{
    partial class UserControlDanhSachQuanNhan
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.groupBoxTimKiem = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.cboCapBac = new System.Windows.Forms.ComboBox();
            this.lblCapBac = new System.Windows.Forms.Label();
            this.txtSoCCCD = new System.Windows.Forms.TextBox();
            this.lblSoCCCD = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.cboDonVi = new System.Windows.Forms.ComboBox();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvQuanNhan = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.groupBoxTimKiem.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuanNhan)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.groupBoxTimKiem);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 120);
            this.panelTop.TabIndex = 0;
            // 
            // groupBoxTimKiem
            // 
            this.groupBoxTimKiem.Controls.Add(this.btnLamMoi);
            this.groupBoxTimKiem.Controls.Add(this.btnTimKiem);
            this.groupBoxTimKiem.Controls.Add(this.txtChucVu);
            this.groupBoxTimKiem.Controls.Add(this.lblChucVu);
            this.groupBoxTimKiem.Controls.Add(this.cboCapBac);
            this.groupBoxTimKiem.Controls.Add(this.lblCapBac);
            this.groupBoxTimKiem.Controls.Add(this.txtSoCCCD);
            this.groupBoxTimKiem.Controls.Add(this.lblSoCCCD);
            this.groupBoxTimKiem.Controls.Add(this.txtHoTen);
            this.groupBoxTimKiem.Controls.Add(this.lblHoTen);
            this.groupBoxTimKiem.Controls.Add(this.cboDonVi);
            this.groupBoxTimKiem.Controls.Add(this.lblDonVi);
            this.groupBoxTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTimKiem.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTimKiem.Name = "groupBoxTimKiem";
            this.groupBoxTimKiem.Size = new System.Drawing.Size(1200, 120);
            this.groupBoxTimKiem.TabIndex = 0;
            this.groupBoxTimKiem.TabStop = false;
            this.groupBoxTimKiem.Text = "Tìm kiếm quân nhân";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(800, 80);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(680, 80);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtChucVu
            // 
            this.txtChucVu.Location = new System.Drawing.Point(500, 50);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(150, 20);
            this.txtChucVu.TabIndex = 9;
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Location = new System.Drawing.Point(500, 30);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(47, 13);
            this.lblChucVu.TabIndex = 8;
            this.lblChucVu.Text = "Chức vụ";
            // 
            // cboCapBac
            // 
            this.cboCapBac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapBac.FormattingEnabled = true;
            this.cboCapBac.Items.AddRange(new object[] {
            "Binh nhì",
            "Binh nhất",
            "Hạ sĩ",
            "Trung sĩ",
            "Thượng sĩ",
            "Thiếu úy",
            "Trung úy",
            "Thượng úy",
            "Đại úy"});
            this.cboCapBac.Location = new System.Drawing.Point(320, 50);
            this.cboCapBac.Name = "cboCapBac";
            this.cboCapBac.Size = new System.Drawing.Size(150, 21);
            this.cboCapBac.TabIndex = 7;
            // 
            // lblCapBac
            // 
            this.lblCapBac.AutoSize = true;
            this.lblCapBac.Location = new System.Drawing.Point(320, 30);
            this.lblCapBac.Name = "lblCapBac";
            this.lblCapBac.Size = new System.Drawing.Size(47, 13);
            this.lblCapBac.TabIndex = 6;
            this.lblCapBac.Text = "Cấp bậc";
            // 
            // txtSoCCCD
            // 
            this.txtSoCCCD.Location = new System.Drawing.Point(680, 50);
            this.txtSoCCCD.Name = "txtSoCCCD";
            this.txtSoCCCD.Size = new System.Drawing.Size(150, 20);
            this.txtSoCCCD.TabIndex = 5;
            // 
            // lblSoCCCD
            // 
            this.lblSoCCCD.AutoSize = true;
            this.lblSoCCCD.Location = new System.Drawing.Point(680, 30);
            this.lblSoCCCD.Name = "lblSoCCCD";
            this.lblSoCCCD.Size = new System.Drawing.Size(50, 13);
            this.lblSoCCCD.TabIndex = 4;
            this.lblSoCCCD.Text = "Số CCCD";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(140, 50);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(150, 20);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(140, 30);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(39, 13);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên";
            // 
            // cboDonVi
            // 
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.Location = new System.Drawing.Point(20, 50);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(100, 21);
            this.cboDonVi.TabIndex = 1;
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Location = new System.Drawing.Point(20, 30);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(38, 13);
            this.lblDonVi.TabIndex = 0;
            this.lblDonVi.Text = "Đơn vị";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnIn);
            this.panelBottom.Controls.Add(this.btnXuatExcel);
            this.panelBottom.Controls.Add(this.btnXemChiTiet);
            this.panelBottom.Controls.Add(this.btnXoa);
            this.panelBottom.Controls.Add(this.btnSua);
            this.panelBottom.Controls.Add(this.btnThem);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 550);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 50);
            this.panelBottom.TabIndex = 1;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(1100, 10);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(80, 30);
            this.btnIn.TabIndex = 5;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(1000, 10);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(80, 30);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(290, 10);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(80, 30);
            this.btnXemChiTiet.TabIndex = 3;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(200, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 30);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(20, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 30);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(110, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 30);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // dgvQuanNhan
            // 
            this.dgvQuanNhan.AllowUserToAddRows = false;
            this.dgvQuanNhan.AllowUserToDeleteRows = false;
            this.dgvQuanNhan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuanNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuanNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuanNhan.Location = new System.Drawing.Point(0, 120);
            this.dgvQuanNhan.MultiSelect = false;
            this.dgvQuanNhan.Name = "dgvQuanNhan";
            this.dgvQuanNhan.ReadOnly = true;
            this.dgvQuanNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuanNhan.Size = new System.Drawing.Size(1200, 430);
            this.dgvQuanNhan.TabIndex = 2;
            this.dgvQuanNhan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQuanNhan_CellDoubleClick);
            // 
            // UserControlDanhSachQuanNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvQuanNhan);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "UserControlDanhSachQuanNhan";
            this.Size = new System.Drawing.Size(1200, 600);
            this.panelTop.ResumeLayout(false);
            this.groupBoxTimKiem.ResumeLayout(false);
            this.groupBoxTimKiem.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuanNhan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox groupBoxTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.ComboBox cboCapBac;
        private System.Windows.Forms.Label lblCapBac;
        private System.Windows.Forms.TextBox txtSoCCCD;
        private System.Windows.Forms.Label lblSoCCCD;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.ComboBox cboDonVi;
        private System.Windows.Forms.Label lblDonVi;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvQuanNhan;
    }
}
