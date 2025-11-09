namespace QuanLyDangVien.Pages
{
    partial class PageDanhSachKhenThuongKyLuat
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboLoaiDoiTuong = new MetroFramework.Controls.MetroComboBox();
            this.lblLoaiDoiTuong = new System.Windows.Forms.Label();
            this.lblLocTheoDonVi = new System.Windows.Forms.Label();
            this.radioTheoNam = new MetroFramework.Controls.MetroRadioButton();
            this.cboLocTheo = new MetroFramework.Controls.MetroComboBox();
            this.btnTimKiem = new MetroFramework.Controls.MetroButton();
            this.txtTimKiem = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSuaFile = new MetroFramework.Controls.MetroButton();
            this.btnXoa = new MetroFramework.Controls.MetroButton();
            this.btnSua = new MetroFramework.Controls.MetroButton();
            this.radioKyLuat = new MetroFramework.Controls.MetroRadioButton();
            this.radioKhenThuong = new MetroFramework.Controls.MetroRadioButton();
            this.dgvDanhSach = new System.Windows.Forms.DataGridView();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboLoaiDoiTuong);
            this.panel1.Controls.Add(this.lblLoaiDoiTuong);
            this.panel1.Controls.Add(this.lblLocTheoDonVi);
            this.panel1.Controls.Add(this.radioTheoNam);
            this.panel1.Controls.Add(this.cboLocTheo);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1600, 150);
            this.panel1.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTimKiem.Location = new System.Drawing.Point(13, 13);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PromptText = "Tìm kiếm theo tên, số quyết định, hình thức...";
            this.txtTimKiem.Size = new System.Drawing.Size(1200, 40);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1225, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(150, 40);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            // 
            // cboLocTheo
            // 
            this.cboLocTheo.FormattingEnabled = true;
            this.cboLocTheo.ItemHeight = 24;
            this.cboLocTheo.Location = new System.Drawing.Point(416, 70);
            this.cboLocTheo.Name = "cboLocTheo";
            this.cboLocTheo.Size = new System.Drawing.Size(250, 30);
            this.cboLocTheo.TabIndex = 3;
            this.cboLocTheo.Visible = false;
            // 
            // lblLoaiDoiTuong
            // 
            this.lblLoaiDoiTuong.AutoSize = true;
            this.lblLoaiDoiTuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiDoiTuong.Location = new System.Drawing.Point(13, 73);
            this.lblLoaiDoiTuong.Name = "lblLoaiDoiTuong";
            this.lblLoaiDoiTuong.Size = new System.Drawing.Size(108, 20);
            this.lblLoaiDoiTuong.TabIndex = 4;
            this.lblLoaiDoiTuong.Text = "Loại đối tượng:";
            // 
            // cboLoaiDoiTuong
            // 
            this.cboLoaiDoiTuong.FormattingEnabled = true;
            this.cboLoaiDoiTuong.ItemHeight = 24;
            this.cboLoaiDoiTuong.Location = new System.Drawing.Point(127, 70);
            this.cboLoaiDoiTuong.Name = "cboLoaiDoiTuong";
            this.cboLoaiDoiTuong.Size = new System.Drawing.Size(150, 30);
            this.cboLoaiDoiTuong.TabIndex = 2;
            // 
            // lblLocTheoDonVi
            // 
            this.lblLocTheoDonVi.AutoSize = true;
            this.lblLocTheoDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocTheoDonVi.Location = new System.Drawing.Point(280, 73);
            this.lblLocTheoDonVi.Name = "lblLocTheoDonVi";
            this.lblLocTheoDonVi.Size = new System.Drawing.Size(130, 20);
            this.lblLocTheoDonVi.TabIndex = 6;
            this.lblLocTheoDonVi.Text = "Lọc theo đơn vị:";
            this.lblLocTheoDonVi.Visible = false;
            // 
            // radioTheoNam
            // 
            this.radioTheoNam.AutoSize = true;
            this.radioTheoNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTheoNam.Location = new System.Drawing.Point(690, 70);
            this.radioTheoNam.Name = "radioTheoNam";
            this.radioTheoNam.Size = new System.Drawing.Size(163, 24);
            this.radioTheoNam.TabIndex = 5;
            this.radioTheoNam.TabStop = true;
            this.radioTheoNam.Text = "Sắp xếp theo năm";
            this.radioTheoNam.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSuaFile);
            this.panel2.Controls.Add(this.btnXoa);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.radioKyLuat);
            this.panel2.Controls.Add(this.radioKhenThuong);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 190);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(1600, 70);
            this.panel2.TabIndex = 1;
            // 
            // radioKyLuat
            // 
            this.radioKyLuat.AutoSize = true;
            this.radioKyLuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioKyLuat.Location = new System.Drawing.Point(200, 13);
            this.radioKyLuat.Name = "radioKyLuat";
            this.radioKyLuat.Size = new System.Drawing.Size(88, 24);
            this.radioKyLuat.TabIndex = 1;
            this.radioKyLuat.TabStop = true;
            this.radioKyLuat.Text = "Kỷ luật";
            this.radioKyLuat.UseVisualStyleBackColor = true;
            // 
            // radioKhenThuong
            // 
            this.radioKhenThuong.AutoSize = true;
            this.radioKhenThuong.Checked = true;
            this.radioKhenThuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioKhenThuong.Location = new System.Drawing.Point(13, 13);
            this.radioKhenThuong.Name = "radioKhenThuong";
            this.radioKhenThuong.Size = new System.Drawing.Size(118, 24);
            this.radioKhenThuong.TabIndex = 0;
            this.radioKhenThuong.TabStop = true;
            this.radioKhenThuong.Text = "Khen thưởng";
            this.radioKhenThuong.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(1200, 13);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 40);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            // 
            // btnSuaFile
            // 
            this.btnSuaFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaFile.Location = new System.Drawing.Point(1320, 13);
            this.btnSuaFile.Name = "btnSuaFile";
            this.btnSuaFile.Size = new System.Drawing.Size(120, 40);
            this.btnSuaFile.TabIndex = 3;
            this.btnSuaFile.Text = "Sửa file đính kèm";
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(1460, 13);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 40);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            // 
            // dgvDanhSach
            // 
            this.dgvDanhSach.AllowUserToAddRows = false;
            this.dgvDanhSach.AllowUserToDeleteRows = false;
            this.dgvDanhSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSach.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhSach.Location = new System.Drawing.Point(0, 260);
            this.dgvDanhSach.MultiSelect = false;
            this.dgvDanhSach.Name = "dgvDanhSach";
            this.dgvDanhSach.ReadOnly = true;
            this.dgvDanhSach.RowHeadersWidth = 51;
            this.dgvDanhSach.RowTemplate.Height = 30;
            this.dgvDanhSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSach.Size = new System.Drawing.Size(1600, 640);
            this.dgvDanhSach.TabIndex = 2;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(1600, 40);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 3;
            this.metroTile1.Text = "Danh sách Khen thưởng - Kỷ luật";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Medium;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // PageDanhSachKhenThuongKyLuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDanhSach);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.metroTile1);
            this.Name = "PageDanhSachKhenThuongKyLuat";
            this.Size = new System.Drawing.Size(1600, 900);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroComboBox cboLoaiDoiTuong;
        private System.Windows.Forms.Label lblLoaiDoiTuong;
        private System.Windows.Forms.Label lblLocTheoDonVi;
        private MetroFramework.Controls.MetroRadioButton radioTheoNam;
        private MetroFramework.Controls.MetroComboBox cboLocTheo;
        private MetroFramework.Controls.MetroButton btnTimKiem;
        private MetroFramework.Controls.MetroTextBox txtTimKiem;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton btnSuaFile;
        private MetroFramework.Controls.MetroButton btnXoa;
        private MetroFramework.Controls.MetroButton btnSua;
        private MetroFramework.Controls.MetroRadioButton radioKyLuat;
        private MetroFramework.Controls.MetroRadioButton radioKhenThuong;
        private System.Windows.Forms.DataGridView dgvDanhSach;
        private MetroFramework.Controls.MetroTile metroTile1;
    }
}
