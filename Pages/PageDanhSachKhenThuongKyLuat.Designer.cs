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
            this.TimBtn = new MetroFramework.Controls.MetroButton();
            this.TimTb = new MetroFramework.Controls.MetroTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ChucNang = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton4 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton5 = new MetroFramework.Controls.MetroRadioButton();
            this.LocTheo = new MetroFramework.Controls.MetroComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LocTheo);
            this.panel1.Controls.Add(this.metroRadioButton5);
            this.panel1.Controls.Add(this.metroRadioButton4);
            this.panel1.Controls.Add(this.metroRadioButton1);
            this.panel1.Controls.Add(this.TimBtn);
            this.panel1.Controls.Add(this.TimTb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 148);
            this.panel1.TabIndex = 7;
            // 
            // TimBtn
            // 
            this.TimBtn.Location = new System.Drawing.Point(752, 12);
            this.TimBtn.Name = "TimBtn";
            this.TimBtn.Size = new System.Drawing.Size(127, 35);
            this.TimBtn.TabIndex = 6;
            this.TimBtn.Text = "Tìm kiến";
            // 
            // TimTb
            // 
            this.TimTb.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.TimTb.Location = new System.Drawing.Point(6, 12);
            this.TimTb.Name = "TimTb";
            this.TimTb.PromptText = "Tìm Đảng viên đã chuyển sinh hoạt";
            this.TimTb.Size = new System.Drawing.Size(714, 35);
            this.TimTb.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChucNang});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.Location = new System.Drawing.Point(0, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(911, 532);
            this.dataGridView1.TabIndex = 9;
            // 
            // ChucNang
            // 
            this.ChucNang.HeaderText = "Chức Năng";
            this.ChucNang.MinimumWidth = 6;
            this.ChucNang.Name = "ChucNang";
            this.ChucNang.Width = 125;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(911, 30);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 8;
            this.metroTile1.Text = "Danh sách Khen thưởng- kỷ luật";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // metroRadioButton1
            // 
            this.metroRadioButton1.AutoSize = true;
            this.metroRadioButton1.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroRadioButton1.Location = new System.Drawing.Point(133, 53);
            this.metroRadioButton1.Name = "metroRadioButton1";
            this.metroRadioButton1.Size = new System.Drawing.Size(157, 20);
            this.metroRadioButton1.TabIndex = 7;
            this.metroRadioButton1.TabStop = true;
            this.metroRadioButton1.Text = "Sắp xếp theo đơn vị";
            this.metroRadioButton1.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton4
            // 
            this.metroRadioButton4.AutoSize = true;
            this.metroRadioButton4.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroRadioButton4.Location = new System.Drawing.Point(133, 76);
            this.metroRadioButton4.Name = "metroRadioButton4";
            this.metroRadioButton4.Size = new System.Drawing.Size(167, 20);
            this.metroRadioButton4.TabIndex = 10;
            this.metroRadioButton4.TabStop = true;
            this.metroRadioButton4.Text = "Sắp xếp theo cá nhân";
            this.metroRadioButton4.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton5
            // 
            this.metroRadioButton5.AutoSize = true;
            this.metroRadioButton5.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroRadioButton5.Location = new System.Drawing.Point(133, 99);
            this.metroRadioButton5.Name = "metroRadioButton5";
            this.metroRadioButton5.Size = new System.Drawing.Size(145, 20);
            this.metroRadioButton5.TabIndex = 12;
            this.metroRadioButton5.TabStop = true;
            this.metroRadioButton5.Text = "Sắp xếp theo năm";
            this.metroRadioButton5.UseVisualStyleBackColor = true;
            // 
            // LocTheo
            // 
            this.LocTheo.FormattingEnabled = true;
            this.LocTheo.ItemHeight = 24;
            this.LocTheo.Location = new System.Drawing.Point(6, 53);
            this.LocTheo.Name = "LocTheo";
            this.LocTheo.Size = new System.Drawing.Size(121, 30);
            this.LocTheo.TabIndex = 13;
            // 
            // PageDanhSachKhenThuongKyLuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.metroTile1);
            this.Name = "PageDanhSachKhenThuongKyLuat";
            this.Size = new System.Drawing.Size(911, 562);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
        private MetroFramework.Controls.MetroButton TimBtn;
        private MetroFramework.Controls.MetroTextBox TimTb;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn ChucNang;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton5;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton4;
        private MetroFramework.Controls.MetroComboBox LocTheo;
    }
}
