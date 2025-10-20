namespace QuanLyDangVien
{
    partial class UserControlHoSoDonVi
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.SinhHoatChiBo = new MetroFramework.Controls.MetroTile();
            this.DanhSachChiBo = new MetroFramework.Controls.MetroTile();
            this.Panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dangVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dangVienBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1206, 657);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 651);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.SinhHoatChiBo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.DanhSachChiBo, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(176, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // SinhHoatChiBo
            // 
            this.SinhHoatChiBo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SinhHoatChiBo.Location = new System.Drawing.Point(3, 54);
            this.SinhHoatChiBo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SinhHoatChiBo.Name = "SinhHoatChiBo";
            this.SinhHoatChiBo.Size = new System.Drawing.Size(170, 42);
            this.SinhHoatChiBo.TabIndex = 1;
            this.SinhHoatChiBo.Text = "Sinh hoạt Chi bộ";
            this.SinhHoatChiBo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SinhHoatChiBo.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.SinhHoatChiBo.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.SinhHoatChiBo.Click += new System.EventHandler(this.SinhHoatChiBo_Click);
            // 
            // DanhSachChiBo
            // 
            this.DanhSachChiBo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DanhSachChiBo.Location = new System.Drawing.Point(3, 9);
            this.DanhSachChiBo.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.DanhSachChiBo.Name = "DanhSachChiBo";
            this.DanhSachChiBo.Size = new System.Drawing.Size(170, 38);
            this.DanhSachChiBo.TabIndex = 0;
            this.DanhSachChiBo.Text = "Danh sách Chi bộ";
            this.DanhSachChiBo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DanhSachChiBo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DanhSachChiBo.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DanhSachChiBo.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.DanhSachChiBo.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.DanhSachChiBo.Click += new System.EventHandler(this.DanhSachChiBo_Click);
            // 
            // Panel
            // 
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(185, 3);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(1018, 651);
            this.Panel.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 651);
            this.panel1.TabIndex = 3;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(176, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dangVienBindingSource
            // 
            this.dangVienBindingSource.DataSource = typeof(QuanLyDangVien.Models.DangVien);
            // 
            // UserControlHoSoDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlHoSoDonVi";
            this.Size = new System.Drawing.Size(1206, 657);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dangVienBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource dangVienBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
       
       
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MetroFramework.Controls.MetroTile SinhHoatChiBo;
        private MetroFramework.Controls.MetroTile DanhSachChiBo;
        private System.Windows.Forms.Panel Panel;
    }
}
