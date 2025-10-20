namespace QuanLyDangVien
{
    partial class UserControlChuyenSinhHoatDang
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
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ChucNang = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TimBtn);
            this.panel1.Controls.Add(this.TimTb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(925, 58);
            this.panel1.TabIndex = 0;
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
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 58);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(925, 30);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 2;
            this.metroTile1.Text = "Lịch sử chuyển sinh hoạt Đảng";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChucNang});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.Location = new System.Drawing.Point(0, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(925, 469);
            this.dataGridView1.TabIndex = 6;
            // 
            // ChucNang
            // 
            this.ChucNang.HeaderText = "Chức Năng";
            this.ChucNang.MinimumWidth = 6;
            this.ChucNang.Name = "ChucNang";
            this.ChucNang.Width = 125;
            // 
            // UserControlChuyenSinhHoatDang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.panel1);
            this.Name = "UserControlChuyenSinhHoatDang";
            this.Size = new System.Drawing.Size(925, 557);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton TimBtn;
        private MetroFramework.Controls.MetroTextBox TimTb;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn ChucNang;
    }
}
