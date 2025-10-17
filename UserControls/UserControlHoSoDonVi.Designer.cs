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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.TimKiemBtn = new MetroFramework.Controls.MetroButton();
            this.TimTb = new MetroFramework.Controls.MetroTextBox();
            this.metroComboBox5 = new MetroFramework.Controls.MetroComboBox();
            this.metroComboBox4 = new MetroFramework.Controls.MetroComboBox();
            this.metroComboBox3 = new MetroFramework.Controls.MetroComboBox();
            this.metroComboBox2 = new MetroFramework.Controls.MetroComboBox();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.DangVienGridView = new System.Windows.Forms.DataGridView();
            this.ChucNang = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DangVienGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.DangVienGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.51499F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.48501F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1122, 567);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroTile1);
            this.metroPanel1.Controls.Add(this.TimKiemBtn);
            this.metroPanel1.Controls.Add(this.TimTb);
            this.metroPanel1.Controls.Add(this.metroComboBox5);
            this.metroPanel1.Controls.Add(this.metroComboBox4);
            this.metroPanel1.Controls.Add(this.metroComboBox3);
            this.metroPanel1.Controls.Add(this.metroComboBox2);
            this.metroPanel1.Controls.Add(this.metroComboBox1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(3, 3);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1116, 132);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(1116, 26);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 32;
            this.metroTile1.Text = "Tìm kiếm";
            this.metroTile1.TileImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            // 
            // TimKiemBtn
            // 
            this.TimKiemBtn.Location = new System.Drawing.Point(671, 81);
            this.TimKiemBtn.Name = "TimKiemBtn";
            this.TimKiemBtn.Size = new System.Drawing.Size(137, 35);
            this.TimKiemBtn.TabIndex = 31;
            this.TimKiemBtn.Text = "Tìm kiếm";
            this.TimKiemBtn.Click += new System.EventHandler(this.TimKiemBtn_Click);
            // 
            // TimTb
            // 
            this.TimTb.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.TimTb.Location = new System.Drawing.Point(2, 40);
            this.TimTb.Name = "TimTb";
            this.TimTb.PromptText = "Nhập từ khóa tìm kiếm";
            this.TimTb.Size = new System.Drawing.Size(436, 35);
            this.TimTb.TabIndex = 30;
            // 
            // metroComboBox5
            // 
            this.metroComboBox5.FontSize = MetroFramework.MetroLinkSize.Tall;
            this.metroComboBox5.FormattingEnabled = true;
            this.metroComboBox5.ItemHeight = 29;
            this.metroComboBox5.Location = new System.Drawing.Point(463, 81);
            this.metroComboBox5.Name = "metroComboBox5";
            this.metroComboBox5.Size = new System.Drawing.Size(180, 35);
            this.metroComboBox5.TabIndex = 29;
            // 
            // metroComboBox4
            // 
            this.metroComboBox4.FontSize = MetroFramework.MetroLinkSize.Tall;
            this.metroComboBox4.FormattingEnabled = true;
            this.metroComboBox4.ItemHeight = 29;
            this.metroComboBox4.Location = new System.Drawing.Point(2, 81);
            this.metroComboBox4.Name = "metroComboBox4";
            this.metroComboBox4.Size = new System.Drawing.Size(436, 35);
            this.metroComboBox4.TabIndex = 28;
            // 
            // metroComboBox3
            // 
            this.metroComboBox3.FontSize = MetroFramework.MetroLinkSize.Tall;
            this.metroComboBox3.FormattingEnabled = true;
            this.metroComboBox3.ItemHeight = 29;
            this.metroComboBox3.Location = new System.Drawing.Point(831, 40);
            this.metroComboBox3.Name = "metroComboBox3";
            this.metroComboBox3.Size = new System.Drawing.Size(213, 35);
            this.metroComboBox3.TabIndex = 27;
            // 
            // metroComboBox2
            // 
            this.metroComboBox2.FontSize = MetroFramework.MetroLinkSize.Tall;
            this.metroComboBox2.FormattingEnabled = true;
            this.metroComboBox2.ItemHeight = 29;
            this.metroComboBox2.Location = new System.Drawing.Point(671, 40);
            this.metroComboBox2.Name = "metroComboBox2";
            this.metroComboBox2.Size = new System.Drawing.Size(137, 35);
            this.metroComboBox2.TabIndex = 26;
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FontSize = MetroFramework.MetroLinkSize.Tall;
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 29;
            this.metroComboBox1.Location = new System.Drawing.Point(463, 40);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(180, 35);
            this.metroComboBox1.TabIndex = 25;
            // 
            // DangVienGridView
            // 
            this.DangVienGridView.AllowUserToOrderColumns = true;
            this.DangVienGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DangVienGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DangVienGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DangVienGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DangVienGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChucNang});
            this.DangVienGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DangVienGridView.Location = new System.Drawing.Point(3, 141);
            this.DangVienGridView.Name = "DangVienGridView";
            this.DangVienGridView.RowHeadersWidth = 51;
            this.DangVienGridView.RowTemplate.Height = 24;
            this.DangVienGridView.Size = new System.Drawing.Size(1116, 423);
            this.DangVienGridView.TabIndex = 7;
            // 
            // ChucNang
            // 
            this.ChucNang.HeaderText = "Chức năng";
            this.ChucNang.Items.AddRange(new object[] {
            "Chi Tiết Đảng Viên",
            "Sửa Thông Tin ",
            "Xóa Đảng Viên"});
            this.ChucNang.MinimumWidth = 6;
            this.ChucNang.Name = "ChucNang";
            // 
            // UserControlHoSoDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlHoSoDonVi";
            this.Size = new System.Drawing.Size(1122, 567);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DangVienGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton TimKiemBtn;
        private MetroFramework.Controls.MetroTextBox TimTb;
        private MetroFramework.Controls.MetroComboBox metroComboBox5;
        private MetroFramework.Controls.MetroComboBox metroComboBox4;
        private MetroFramework.Controls.MetroComboBox metroComboBox3;
        private MetroFramework.Controls.MetroComboBox metroComboBox2;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.DataGridView DangVienGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn ChucNang;
    }
}
