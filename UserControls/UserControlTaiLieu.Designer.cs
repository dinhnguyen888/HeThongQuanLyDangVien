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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewTaiLieu = new System.Windows.Forms.TreeView();
            this.panelTreeView = new System.Windows.Forms.Panel();
            this.lblTreeView = new MetroFramework.Controls.MetroLabel();
            this.panelList = new System.Windows.Forms.Panel();
            this.dataGridViewTaiLieu = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnXoa = new MetroFramework.Controls.MetroButton();
            this.btnSua = new MetroFramework.Controls.MetroButton();
            this.btnThem = new MetroFramework.Controls.MetroButton();
            this.btnRefresh = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelTreeView.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaiLieu)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.metroTile1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(969, 554);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTile1.Location = new System.Drawing.Point(3, 3);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(963, 34);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Quản lý Tài liệu";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelList);
            this.splitContainer1.Size = new System.Drawing.Size(963, 508);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // panelTreeView
            // 
            this.panelTreeView.Controls.Add(this.treeViewTaiLieu);
            this.panelTreeView.Controls.Add(this.lblTreeView);
            this.panelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTreeView.Location = new System.Drawing.Point(0, 0);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Padding = new System.Windows.Forms.Padding(10);
            this.panelTreeView.Size = new System.Drawing.Size(250, 508);
            this.panelTreeView.TabIndex = 0;
            // 
            // lblTreeView
            // 
            this.lblTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeView.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblTreeView.Location = new System.Drawing.Point(10, 10);
            this.lblTreeView.Name = "lblTreeView";
            this.lblTreeView.Size = new System.Drawing.Size(230, 23);
            this.lblTreeView.TabIndex = 1;
            this.lblTreeView.Text = "Thư mục Server:";
            // 
            // treeViewTaiLieu
            // 
            this.treeViewTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTaiLieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.treeViewTaiLieu.Location = new System.Drawing.Point(10, 33);
            this.treeViewTaiLieu.Name = "treeViewTaiLieu";
            this.treeViewTaiLieu.Size = new System.Drawing.Size(230, 465);
            this.treeViewTaiLieu.TabIndex = 0;
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.dataGridViewTaiLieu);
            this.panelList.Controls.Add(this.panelButtons);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Name = "panelList";
            this.panelList.Padding = new System.Windows.Forms.Padding(10);
            this.panelList.Size = new System.Drawing.Size(709, 508);
            this.panelList.TabIndex = 0;
            // 
            // dataGridViewTaiLieu
            // 
            this.dataGridViewTaiLieu.AllowUserToAddRows = false;
            this.dataGridViewTaiLieu.AllowUserToDeleteRows = false;
            this.dataGridViewTaiLieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTaiLieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTaiLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTaiLieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dataGridViewTaiLieu.Location = new System.Drawing.Point(10, 10);
            this.dataGridViewTaiLieu.MultiSelect = false;
            this.dataGridViewTaiLieu.Name = "dataGridViewTaiLieu";
            this.dataGridViewTaiLieu.ReadOnly = true;
            this.dataGridViewTaiLieu.RowHeadersWidth = 51;
            this.dataGridViewTaiLieu.RowTemplate.Height = 30;
            this.dataGridViewTaiLieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTaiLieu.Size = new System.Drawing.Size(689, 438);
            this.dataGridViewTaiLieu.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnSua);
            this.panelButtons.Controls.Add(this.btnThem);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(10, 448);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(689, 50);
            this.panelButtons.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(330, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
         
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(224, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
          
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(118, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
         
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(12, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
           
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // UserControlTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlTaiLieu";
            this.Size = new System.Drawing.Size(969, 554);
            this.Load += new System.EventHandler(this.UserControlTaiLieu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelTreeView.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaiLieu)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelTreeView;
        private System.Windows.Forms.TreeView treeViewTaiLieu;
        private MetroFramework.Controls.MetroLabel lblTreeView;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.DataGridView dataGridViewTaiLieu;
        private System.Windows.Forms.Panel panelButtons;
        private MetroFramework.Controls.MetroButton btnThem;
        private MetroFramework.Controls.MetroButton btnSua;
        private MetroFramework.Controls.MetroButton btnXoa;
        private MetroFramework.Controls.MetroButton btnRefresh;
    }
}
