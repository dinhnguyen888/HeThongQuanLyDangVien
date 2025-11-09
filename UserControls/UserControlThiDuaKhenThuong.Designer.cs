namespace QuanLyDangVien
{
    partial class UserControlThiDuaKhenThuong
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
            this.components = new System.ComponentModel.Container();
            this.Panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.DanhSach = new MetroFramework.Controls.MetroTile();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.KyLuatToChuc = new MetroFramework.Controls.MetroTile();
            this.KyLuat = new MetroFramework.Controls.MetroTile();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.KhenThuongDonVi = new MetroFramework.Controls.MetroTile();
            this.KhenThuong = new MetroFramework.Controls.MetroTile();
            this.dangVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dangVienBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(161, 3);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(879, 572);
            this.Panel.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.15152F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.84849F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Panel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1043, 578);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.tableLayoutPanel5);
            this.panel2.Controls.Add(this.tableLayoutPanel4);
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 572);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.DanhSach, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 200);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(152, 100);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // DanhSach
            // 
            this.DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DanhSach.Location = new System.Drawing.Point(3, 9);
            this.DanhSach.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.DanhSach.Name = "DanhSach";
            this.DanhSach.Size = new System.Drawing.Size(146, 38);
            this.DanhSach.TabIndex = 2;
            this.DanhSach.Text = "Danh Sách";
            this.DanhSach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DanhSach.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.DanhSach.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.DanhSach.Click += new System.EventHandler(this.DanhSach_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.KyLuatToChuc, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.KyLuat, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(152, 100);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // KyLuatToChuc
            // 
            this.KyLuatToChuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KyLuatToChuc.Location = new System.Drawing.Point(3, 54);
            this.KyLuatToChuc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.KyLuatToChuc.Name = "KyLuatToChuc";
            this.KyLuatToChuc.Size = new System.Drawing.Size(146, 42);
            this.KyLuatToChuc.TabIndex = 1;
            this.KyLuatToChuc.Text = "Kỷ Luật Đơn Vị";
            this.KyLuatToChuc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KyLuatToChuc.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KyLuatToChuc.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KyLuatToChuc.Click += new System.EventHandler(this.KyLuatToChuc_Click);
            // 
            // KyLuat
            // 
            this.KyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KyLuat.Location = new System.Drawing.Point(3, 9);
            this.KyLuat.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.KyLuat.Name = "KyLuat";
            this.KyLuat.Size = new System.Drawing.Size(146, 38);
            this.KyLuat.TabIndex = 0;
            this.KyLuat.Text = "Kỷ Luật Cá Nhân";
            this.KyLuat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KyLuat.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KyLuat.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KyLuat.Click += new System.EventHandler(this.KyLuat_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.KhenThuongDonVi, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.KhenThuong, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(152, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // KhenThuongDonVi
            // 
            this.KhenThuongDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KhenThuongDonVi.Location = new System.Drawing.Point(3, 54);
            this.KhenThuongDonVi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.KhenThuongDonVi.Name = "KhenThuongDonVi";
            this.KhenThuongDonVi.Size = new System.Drawing.Size(146, 42);
            this.KhenThuongDonVi.TabIndex = 1;
            this.KhenThuongDonVi.Text = "Khen Thưởng Đơn vị";
            this.KhenThuongDonVi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KhenThuongDonVi.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KhenThuongDonVi.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KhenThuongDonVi.Click += new System.EventHandler(this.KhenThuongDonVi_Click);
            // 
            // KhenThuong
            // 
            this.KhenThuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KhenThuong.Location = new System.Drawing.Point(3, 9);
            this.KhenThuong.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.KhenThuong.Name = "KhenThuong";
            this.KhenThuong.Size = new System.Drawing.Size(146, 38);
            this.KhenThuong.TabIndex = 0;
            this.KhenThuong.Text = "Khen Thưởng Cá nhân";
            this.KhenThuong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KhenThuong.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.KhenThuong.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KhenThuong.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KhenThuong.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KhenThuong.Click += new System.EventHandler(this.KhenThuong_Click);
            // 
            // dangVienBindingSource
            // 
            this.dangVienBindingSource.DataSource = typeof(QuanLyDangVien.Models.DangVien);
            // 
            // UserControlThiDuaKhenThuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlThiDuaKhenThuong";
            this.Size = new System.Drawing.Size(1043, 578);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dangVienBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MetroFramework.Controls.MetroTile KyLuatToChuc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MetroFramework.Controls.MetroTile KhenThuongDonVi;
        private MetroFramework.Controls.MetroTile KyLuat;
        private MetroFramework.Controls.MetroTile KhenThuong;
        private System.Windows.Forms.BindingSource dangVienBindingSource;
        private MetroFramework.Controls.MetroTile DanhSach;
    }
}
