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
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.TheoDoiChuyenChinhThuc = new MetroFramework.Controls.MetroTile();
            this.QuanLyDangVienDuBi = new MetroFramework.Controls.MetroTile();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mainTableLayoutPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.topPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.contentPanel, 0, 1);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(907, 567);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Info;
            this.topPanel.Controls.Add(this.tableLayoutPanelButtons);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel.Location = new System.Drawing.Point(3, 3);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(901, 94);
            this.topPanel.TabIndex = 0;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.ColumnCount = 2;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Controls.Add(this.QuanLyDangVienDuBi, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.TheoDoiChuyenChinhThuc, 1, 0);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(901, 94);
            this.tableLayoutPanelButtons.TabIndex = 0;
            // 
            // QuanLyDangVienDuBi
            // 
            this.QuanLyDangVienDuBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuanLyDangVienDuBi.Location = new System.Drawing.Point(3, 4);
            this.QuanLyDangVienDuBi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.QuanLyDangVienDuBi.Name = "QuanLyDangVienDuBi";
            this.QuanLyDangVienDuBi.Size = new System.Drawing.Size(444, 86);
            this.QuanLyDangVienDuBi.TabIndex = 0;
            this.QuanLyDangVienDuBi.Text = "Quản Lý Đảng Viên Dự Bị";
            this.QuanLyDangVienDuBi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QuanLyDangVienDuBi.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.QuanLyDangVienDuBi.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.QuanLyDangVienDuBi.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.QuanLyDangVienDuBi.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.QuanLyDangVienDuBi.Click += new System.EventHandler(this.QuanLyDangVienDuBi_Click);
            // 
            // TheoDoiChuyenChinhThuc
            // 
            this.TheoDoiChuyenChinhThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheoDoiChuyenChinhThuc.Location = new System.Drawing.Point(453, 4);
            this.TheoDoiChuyenChinhThuc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TheoDoiChuyenChinhThuc.Name = "TheoDoiChuyenChinhThuc";
            this.TheoDoiChuyenChinhThuc.Size = new System.Drawing.Size(445, 86);
            this.TheoDoiChuyenChinhThuc.TabIndex = 1;
            this.TheoDoiChuyenChinhThuc.Text = "Theo dõi chuyển chính thức";
            this.TheoDoiChuyenChinhThuc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TheoDoiChuyenChinhThuc.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.TheoDoiChuyenChinhThuc.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TheoDoiChuyenChinhThuc.Click += new System.EventHandler(this.TheoDoiChuyenChinhThuc_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(3, 103);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(901, 461);
            this.contentPanel.TabIndex = 1;
            // 
            // UserControlCongTacPhatTrienDang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "UserControlCongTacPhatTrienDang";
            this.Size = new System.Drawing.Size(907, 567);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private MetroFramework.Controls.MetroTile QuanLyDangVienDuBi;
        private MetroFramework.Controls.MetroTile TheoDoiChuyenChinhThuc;
        private System.Windows.Forms.Panel contentPanel;
    }
}
