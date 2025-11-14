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
            this.txtChucVu = new MetroFramework.Controls.MetroTextBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.cboCapBac = new MetroFramework.Controls.MetroComboBox();
            this.lblCapBac = new System.Windows.Forms.Label();
            this.txtSoCCCD = new MetroFramework.Controls.MetroTextBox();
            this.lblSoCCCD = new System.Windows.Forms.Label();
            this.txtHoTen = new MetroFramework.Controls.MetroTextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.cboDonVi = new MetroFramework.Controls.MetroComboBox();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
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
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1600, 148);
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
            this.groupBoxTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxTimKiem.Name = "groupBoxTimKiem";
            this.groupBoxTimKiem.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxTimKiem.Size = new System.Drawing.Size(1600, 148);
            this.groupBoxTimKiem.TabIndex = 0;
            this.groupBoxTimKiem.TabStop = false;
            this.groupBoxTimKiem.Text = "Tìm kiếm quân nhân";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(1067, 98);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(133, 43);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(907, 98);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(133, 43);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtChucVu
            // 
            this.txtChucVu.Location = new System.Drawing.Point(667, 62);
            this.txtChucVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(200, 28);
            this.txtChucVu.TabIndex = 9;
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Location = new System.Drawing.Point(667, 37);
            this.lblChucVu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(54, 16);
            this.lblChucVu.TabIndex = 8;
            this.lblChucVu.Text = "Chức vụ";
            // 
            // cboCapBac
            // 
            this.cboCapBac.FormattingEnabled = true;
            this.cboCapBac.ItemHeight = 24;
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
            this.cboCapBac.Location = new System.Drawing.Point(427, 62);
            this.cboCapBac.Margin = new System.Windows.Forms.Padding(4);
            this.cboCapBac.Name = "cboCapBac";
            this.cboCapBac.Size = new System.Drawing.Size(199, 30);
            this.cboCapBac.TabIndex = 7;
            // 
            // lblCapBac
            // 
            this.lblCapBac.AutoSize = true;
            this.lblCapBac.Location = new System.Drawing.Point(427, 37);
            this.lblCapBac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapBac.Name = "lblCapBac";
            this.lblCapBac.Size = new System.Drawing.Size(58, 16);
            this.lblCapBac.TabIndex = 6;
            this.lblCapBac.Text = "Cấp bậc";
            // 
            // txtSoCCCD
            // 
            this.txtSoCCCD.Location = new System.Drawing.Point(907, 62);
            this.txtSoCCCD.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoCCCD.Name = "txtSoCCCD";
            this.txtSoCCCD.Size = new System.Drawing.Size(200, 28);
            this.txtSoCCCD.TabIndex = 5;
            // 
            // lblSoCCCD
            // 
            this.lblSoCCCD.AutoSize = true;
            this.lblSoCCCD.Location = new System.Drawing.Point(907, 37);
            this.lblSoCCCD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoCCCD.Name = "lblSoCCCD";
            this.lblSoCCCD.Size = new System.Drawing.Size(64, 16);
            this.lblSoCCCD.TabIndex = 4;
            this.lblSoCCCD.Text = "Số CCCD";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(187, 62);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(200, 28);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(187, 37);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(46, 16);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên";
            // 
            // cboDonVi
            // 
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.ItemHeight = 24;
            this.cboDonVi.Location = new System.Drawing.Point(27, 62);
            this.cboDonVi.Margin = new System.Windows.Forms.Padding(4);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(132, 30);
            this.cboDonVi.TabIndex = 1;
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Location = new System.Drawing.Point(27, 37);
            this.lblDonVi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(44, 16);
            this.lblDonVi.TabIndex = 0;
            this.lblDonVi.Text = "Đơn vị";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnXuatExcel);
            this.panelBottom.Controls.Add(this.btnXemChiTiet);
            this.panelBottom.Controls.Add(this.btnXoa);
            this.panelBottom.Controls.Add(this.btnSua);
            this.panelBottom.Controls.Add(this.btnThem);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 676);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1600, 62);
            this.panelBottom.TabIndex = 1;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(1482, 12);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(107, 43);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXemChiTiet.FlatAppearance.BorderSize = 0;
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(387, 12);
            this.btnXemChiTiet.Margin = new System.Windows.Forms.Padding(4);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(133, 43);
            this.btnXemChiTiet.TabIndex = 3;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = false;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(267, 12);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(107, 43);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(147, 12);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(107, 43);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(27, 12);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(107, 43);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvQuanNhan
            // 
            this.dgvQuanNhan.AllowUserToAddRows = false;
            this.dgvQuanNhan.AllowUserToDeleteRows = false;
            this.dgvQuanNhan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuanNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuanNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuanNhan.Location = new System.Drawing.Point(0, 148);
            this.dgvQuanNhan.Margin = new System.Windows.Forms.Padding(4);
            this.dgvQuanNhan.MultiSelect = false;
            this.dgvQuanNhan.Name = "dgvQuanNhan";
            this.dgvQuanNhan.ReadOnly = true;
            this.dgvQuanNhan.RowHeadersWidth = 51;
            this.dgvQuanNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuanNhan.Size = new System.Drawing.Size(1600, 528);
            this.dgvQuanNhan.TabIndex = 2;
            this.dgvQuanNhan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQuanNhan_CellDoubleClick);
            // 
            // UserControlDanhSachQuanNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvQuanNhan);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserControlDanhSachQuanNhan";
            this.Size = new System.Drawing.Size(1600, 738);
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
        private MetroFramework.Controls.MetroTextBox txtChucVu;
        private System.Windows.Forms.Label lblChucVu;
        private MetroFramework.Controls.MetroComboBox cboCapBac;
        private System.Windows.Forms.Label lblCapBac;
        private MetroFramework.Controls.MetroTextBox txtSoCCCD;
        private System.Windows.Forms.Label lblSoCCCD;
        private MetroFramework.Controls.MetroTextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private MetroFramework.Controls.MetroComboBox cboDonVi;
        private System.Windows.Forms.Label lblDonVi;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvQuanNhan;
    }
}
