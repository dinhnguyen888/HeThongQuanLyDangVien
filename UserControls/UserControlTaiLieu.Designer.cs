namespace QuanLyDangVien.UserControls
{
    partial class UserControlTaiLieu
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
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelTreeView = new System.Windows.Forms.Panel();
            this.lblTreeView = new MetroFramework.Controls.MetroLabel();
            this.treeViewTaiLieu = new System.Windows.Forms.TreeView();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.lblPreviewMessage = new System.Windows.Forms.Label();
            this.panelPdfViewer = new System.Windows.Forms.Panel();
            this.lblPreview = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelTreeView.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.metroTile1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainerMain, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1200, 700);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTile1.Location = new System.Drawing.Point(3, 3);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(1194, 34);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Quản lý Tài liệu";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 43);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelTreeView);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelPreview);
            this.splitContainerMain.Panel2.Controls.Add(this.panelButtons);
            this.splitContainerMain.Size = new System.Drawing.Size(1194, 654);
            this.splitContainerMain.SplitterDistance = 300;
            this.splitContainerMain.TabIndex = 1;
            // 
            // panelTreeView
            // 
            this.panelTreeView.Controls.Add(this.treeViewTaiLieu);
            this.panelTreeView.Controls.Add(this.lblTreeView);
            this.panelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTreeView.Location = new System.Drawing.Point(0, 0);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Padding = new System.Windows.Forms.Padding(10);
            this.panelTreeView.Size = new System.Drawing.Size(300, 654);
            this.panelTreeView.TabIndex = 0;
            // 
            // lblTreeView
            // 
            this.lblTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeView.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblTreeView.Location = new System.Drawing.Point(10, 10);
            this.lblTreeView.Name = "lblTreeView";
            this.lblTreeView.Size = new System.Drawing.Size(280, 23);
            this.lblTreeView.TabIndex = 1;
            this.lblTreeView.Text = "Tài liệu";
            // 
            // treeViewTaiLieu
            // 
            this.treeViewTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTaiLieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.treeViewTaiLieu.Location = new System.Drawing.Point(10, 33);
            this.treeViewTaiLieu.Name = "treeViewTaiLieu";
            this.treeViewTaiLieu.Size = new System.Drawing.Size(280, 611);
            this.treeViewTaiLieu.TabIndex = 0;
            // 
            // panelPreview
            // 
            this.panelPreview.Controls.Add(this.lblPreviewMessage);
            this.panelPreview.Controls.Add(this.panelPdfViewer);
            this.panelPreview.Controls.Add(this.lblPreview);
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panelPreview.Size = new System.Drawing.Size(890, 604);
            this.panelPreview.TabIndex = 0;
            // 
            // lblPreview
            // 
            this.lblPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPreview.Location = new System.Drawing.Point(10, 10);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(870, 25);
            this.lblPreview.TabIndex = 0;
            this.lblPreview.Text = "Xem trước";
            // 
            // panelPdfViewer
            // 
            this.panelPdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPdfViewer.Location = new System.Drawing.Point(10, 35);
            this.panelPdfViewer.Name = "panelPdfViewer";
            this.panelPdfViewer.Padding = new System.Windows.Forms.Padding(0);
            this.panelPdfViewer.Size = new System.Drawing.Size(870, 569);
            this.panelPdfViewer.TabIndex = 1;
            // 
            // lblPreviewMessage
            // 
            this.lblPreviewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviewMessage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPreviewMessage.ForeColor = System.Drawing.Color.Gray;
            this.lblPreviewMessage.Location = new System.Drawing.Point(10, 35);
            this.lblPreviewMessage.Name = "lblPreviewMessage";
            this.lblPreviewMessage.Size = new System.Drawing.Size(870, 569);
            this.lblPreviewMessage.TabIndex = 2;
            this.lblPreviewMessage.Text = "Chọn file để xem trước";
            this.lblPreviewMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPreviewMessage.Visible = true;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnSua);
            this.panelButtons.Controls.Add(this.btnThem);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 604);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(10);
            this.panelButtons.Size = new System.Drawing.Size(890, 50);
            this.panelButtons.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(330, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(17)))), ((int)(((byte)(65)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(224, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(118, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
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
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(12, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // UserControlTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlTaiLieu";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Load += new System.EventHandler(this.UserControlTaiLieu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelTreeView.ResumeLayout(false);
            this.panelPreview.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelTreeView;
        private MetroFramework.Controls.MetroLabel lblTreeView;
        private System.Windows.Forms.TreeView treeViewTaiLieu;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Panel panelPdfViewer;
        private System.Windows.Forms.Label lblPreviewMessage;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnRefresh;
    }
}
