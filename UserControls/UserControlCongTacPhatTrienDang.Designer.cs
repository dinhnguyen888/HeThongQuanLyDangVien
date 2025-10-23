namespace QuanLyDangVien
{
    partial class UserControlCongTacPhatTrienDang
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
            this.Panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TheoDoiChuyenChinhThuc = new MetroFramework.Controls.MetroTile();
            this.QuanLyDangVienDuBi = new MetroFramework.Controls.MetroTile();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(272, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(635, 567);
            this.Panel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TheoDoiChuyenChinhThuc, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.QuanLyDangVienDuBi, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(272, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TheoDoiChuyenChinhThuc
            // 
            this.TheoDoiChuyenChinhThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheoDoiChuyenChinhThuc.Location = new System.Drawing.Point(3, 54);
            this.TheoDoiChuyenChinhThuc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TheoDoiChuyenChinhThuc.Name = "TheoDoiChuyenChinhThuc";
            this.TheoDoiChuyenChinhThuc.Size = new System.Drawing.Size(266, 42);
            this.TheoDoiChuyenChinhThuc.TabIndex = 1;
            this.TheoDoiChuyenChinhThuc.Text = "Theo dõi chuyển chính thức";
            this.TheoDoiChuyenChinhThuc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TheoDoiChuyenChinhThuc.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.TheoDoiChuyenChinhThuc.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TheoDoiChuyenChinhThuc.Click += new System.EventHandler(this.TheoDoiChuyenChinhThuc_Click);
            // 
            // QuanLyDangVienDuBi
            // 
            this.QuanLyDangVienDuBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuanLyDangVienDuBi.Location = new System.Drawing.Point(3, 9);
            this.QuanLyDangVienDuBi.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.QuanLyDangVienDuBi.Name = "QuanLyDangVienDuBi";
            this.QuanLyDangVienDuBi.Size = new System.Drawing.Size(266, 38);
            this.QuanLyDangVienDuBi.TabIndex = 0;
            this.QuanLyDangVienDuBi.Text = "Quản Lý Đảng Viên Dự Bị";
            this.QuanLyDangVienDuBi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QuanLyDangVienDuBi.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.QuanLyDangVienDuBi.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QuanLyDangVienDuBi.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.QuanLyDangVienDuBi.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.QuanLyDangVienDuBi.Click += new System.EventHandler(this.QuanLyDangVienDuBi_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 567);
            this.panel1.TabIndex = 0;
            // 
            // UserControlCongTacPhatTrienDang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.panel1);
            this.Name = "UserControlCongTacPhatTrienDang";
            this.Size = new System.Drawing.Size(907, 567);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroTile TheoDoiChuyenChinhThuc;
        private MetroFramework.Controls.MetroTile QuanLyDangVienDuBi;
        private System.Windows.Forms.Panel panel1;
    }
}
