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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.KyLuat = new MetroFramework.Controls.MetroTile();
            this.KhenThuong = new MetroFramework.Controls.MetroTile();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.DanhSach = new MetroFramework.Controls.MetroTile();
            this.dangVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
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
            this.panel2.Controls.Add(this.tableLayoutPanel4);
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 572);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.KyLuat, 0, 1);
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
            // KyLuat
            // 
            this.KyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KyLuat.Location = new System.Drawing.Point(3, 54);
            this.KyLuat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.KyLuat.Name = "KyLuat";
            this.KyLuat.Size = new System.Drawing.Size(146, 42);
            this.KyLuat.TabIndex = 1;
            this.KyLuat.Text = "Kỷ Luật";
            this.KyLuat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KyLuat.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KyLuat.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KyLuat.Click += new System.EventHandler(this.KyLuat_Click);
            // 
            // KhenThuong
            // 
            this.KhenThuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KhenThuong.Location = new System.Drawing.Point(3, 9);
            this.KhenThuong.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.KhenThuong.Name = "KhenThuong";
            this.KhenThuong.Size = new System.Drawing.Size(146, 38);
            this.KhenThuong.TabIndex = 0;
            this.KhenThuong.Text = "Khen Thưởng";
            this.KhenThuong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KhenThuong.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.KhenThuong.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KhenThuong.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.KhenThuong.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.KhenThuong.Click += new System.EventHandler(this.KhenThuong_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 578);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1043, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.DanhSach, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(152, 100);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // DanhSach
            // 
            this.DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DanhSach.Location = new System.Drawing.Point(3, 4);
            this.DanhSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DanhSach.Name = "DanhSach";
            this.DanhSach.Size = new System.Drawing.Size(146, 42);
            this.DanhSach.TabIndex = 2;
            this.DanhSach.Text = "Danh Sách Khen Thưởng, Kỷ Luật";
            this.DanhSach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DanhSach.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.DanhSach.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.DanhSach.Click += new System.EventHandler(this.DanhSach_Click);
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
            this.Controls.Add(this.panel1);
            this.Name = "UserControlThiDuaKhenThuong";
            this.Size = new System.Drawing.Size(1043, 578);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dangVienBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MetroFramework.Controls.MetroTile KyLuat;
        private MetroFramework.Controls.MetroTile KhenThuong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.BindingSource dangVienBindingSource;
        private MetroFramework.Controls.MetroTile DanhSach;
    }
}
