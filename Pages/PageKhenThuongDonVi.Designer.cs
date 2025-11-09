namespace QuanLyDangVien.Pages
{
    partial class PageKhenThuongDonVi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDonVi = new System.Windows.Forms.DataGridView();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTimKiem = new MetroFramework.Controls.MetroButton();
            this.txtTimKiem = new MetroFramework.Controls.MetroTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelHinhThuc = new System.Windows.Forms.Panel();
            this.lblFileDinhKem = new MetroFramework.Controls.MetroLabel();
            this.linkFileDinhKem = new MetroFramework.Controls.MetroLink();
            this.rtxtNoiDung = new System.Windows.Forms.RichTextBox();
            this.lblNoiDung = new MetroFramework.Controls.MetroLabel();
            this.txtCapQuyetDinh = new MetroFramework.Controls.MetroTextBox();
            this.lblCapQuyetDinh = new MetroFramework.Controls.MetroLabel();
            this.txtSoQuyetDinh = new MetroFramework.Controls.MetroTextBox();
            this.lblSoQuyetDinh = new MetroFramework.Controls.MetroLabel();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNgay = new MetroFramework.Controls.MetroLabel();
            this.cboDonVi = new MetroFramework.Controls.MetroComboBox();
            this.lblDonVi = new MetroFramework.Controls.MetroLabel();
            this.lblThongTinDonVi = new MetroFramework.Controls.MetroLabel();
            this.btnLuu = new MetroFramework.Controls.MetroButton();
            this.lblHinhThuc = new MetroFramework.Controls.MetroLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonVi)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.02273F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.97727F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1600, 900);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDonVi);
            this.panel1.Controls.Add(this.metroTile1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(626, 874);
            this.panel1.TabIndex = 0;
            // 
            // dgvDonVi
            // 
            this.dgvDonVi.AllowUserToAddRows = false;
            this.dgvDonVi.AllowUserToDeleteRows = false;
            this.dgvDonVi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonVi.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDonVi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDonVi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDonVi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonVi.Location = new System.Drawing.Point(0, 40);
            this.dgvDonVi.MultiSelect = false;
            this.dgvDonVi.Name = "dgvDonVi";
            this.dgvDonVi.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDonVi.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDonVi.RowHeadersWidth = 51;
            this.dgvDonVi.RowTemplate.Height = 30;
            this.dgvDonVi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonVi.Size = new System.Drawing.Size(626, 834);
            this.dgvDonVi.TabIndex = 1;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(626, 40);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Danh sách Đơn vị";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(645, 13);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(942, 135);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnTimKiem);
            this.panel3.Controls.Add(this.txtTimKiem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(922, 115);
            this.panel3.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(800, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 40);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(13, 13);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PromptText = "Tìm kiếm đơn vị...";
            this.txtTimKiem.Size = new System.Drawing.Size(780, 40);
            this.txtTimKiem.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelHinhThuc);
            this.panel4.Controls.Add(this.lblFileDinhKem);
            this.panel4.Controls.Add(this.linkFileDinhKem);
            this.panel4.Controls.Add(this.rtxtNoiDung);
            this.panel4.Controls.Add(this.lblNoiDung);
            this.panel4.Controls.Add(this.txtCapQuyetDinh);
            this.panel4.Controls.Add(this.lblCapQuyetDinh);
            this.panel4.Controls.Add(this.txtSoQuyetDinh);
            this.panel4.Controls.Add(this.lblSoQuyetDinh);
            this.panel4.Controls.Add(this.dtpNgay);
            this.panel4.Controls.Add(this.lblNgay);
            this.panel4.Controls.Add(this.cboDonVi);
            this.panel4.Controls.Add(this.lblDonVi);
            this.panel4.Controls.Add(this.lblThongTinDonVi);
            this.panel4.Controls.Add(this.btnLuu);
            this.panel4.Controls.Add(this.lblHinhThuc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(645, 154);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(942, 733);
            this.panel4.TabIndex = 2;
            // 
            // panelHinhThuc
            // 
            this.panelHinhThuc.AutoScroll = true;
            this.panelHinhThuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHinhThuc.Location = new System.Drawing.Point(13, 65);
            this.panelHinhThuc.Name = "panelHinhThuc";
            this.panelHinhThuc.Size = new System.Drawing.Size(920, 320);
            this.panelHinhThuc.TabIndex = 15;
            // 
            // lblFileDinhKem
            // 
            this.lblFileDinhKem.AutoSize = true;
            this.lblFileDinhKem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDinhKem.ForeColor = System.Drawing.Color.Gray;
            this.lblFileDinhKem.Location = new System.Drawing.Point(150, 677);
            this.lblFileDinhKem.Name = "lblFileDinhKem";
            this.lblFileDinhKem.Size = new System.Drawing.Size(98, 20);
            this.lblFileDinhKem.TabIndex = 14;
            this.lblFileDinhKem.Text = "Chưa chọn file";
            // 
            // linkFileDinhKem
            // 
            this.linkFileDinhKem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkFileDinhKem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.linkFileDinhKem.Location = new System.Drawing.Point(13, 677);
            this.linkFileDinhKem.Name = "linkFileDinhKem";
            this.linkFileDinhKem.Size = new System.Drawing.Size(130, 23);
            this.linkFileDinhKem.TabIndex = 13;
            this.linkFileDinhKem.Text = "File đính kèm";
            this.linkFileDinhKem.UseStyleColors = true;
            // 
            // rtxtNoiDung
            // 
            this.rtxtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNoiDung.Location = new System.Drawing.Point(13, 470);
            this.rtxtNoiDung.Name = "rtxtNoiDung";
            this.rtxtNoiDung.Size = new System.Drawing.Size(920, 192);
            this.rtxtNoiDung.TabIndex = 12;
            this.rtxtNoiDung.Text = "";
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoiDung.Location = new System.Drawing.Point(13, 447);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(70, 20);
            this.lblNoiDung.TabIndex = 11;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtCapQuyetDinh
            // 
            this.txtCapQuyetDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapQuyetDinh.Location = new System.Drawing.Point(620, 412);
            this.txtCapQuyetDinh.Name = "txtCapQuyetDinh";
            this.txtCapQuyetDinh.PromptText = "Cấp quyết định";
            this.txtCapQuyetDinh.Size = new System.Drawing.Size(313, 30);
            this.txtCapQuyetDinh.TabIndex = 10;
            // 
            // lblCapQuyetDinh
            // 
            this.lblCapQuyetDinh.AutoSize = true;
            this.lblCapQuyetDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapQuyetDinh.Location = new System.Drawing.Point(620, 387);
            this.lblCapQuyetDinh.Name = "lblCapQuyetDinh";
            this.lblCapQuyetDinh.Size = new System.Drawing.Size(106, 20);
            this.lblCapQuyetDinh.TabIndex = 9;
            this.lblCapQuyetDinh.Text = "Cấp quyết định:";
            // 
            // txtSoQuyetDinh
            // 
            this.txtSoQuyetDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoQuyetDinh.Location = new System.Drawing.Point(280, 412);
            this.txtSoQuyetDinh.Name = "txtSoQuyetDinh";
            this.txtSoQuyetDinh.PromptText = "Số quyết định";
            this.txtSoQuyetDinh.Size = new System.Drawing.Size(320, 30);
            this.txtSoQuyetDinh.TabIndex = 8;
            // 
            // lblSoQuyetDinh
            // 
            this.lblSoQuyetDinh.AutoSize = true;
            this.lblSoQuyetDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoQuyetDinh.Location = new System.Drawing.Point(280, 387);
            this.lblSoQuyetDinh.Name = "lblSoQuyetDinh";
            this.lblSoQuyetDinh.Size = new System.Drawing.Size(97, 20);
            this.lblSoQuyetDinh.TabIndex = 7;
            this.lblSoQuyetDinh.Text = "Số quyết định:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Location = new System.Drawing.Point(13, 412);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(250, 26);
            this.dtpNgay.TabIndex = 6;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay.Location = new System.Drawing.Point(13, 387);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(45, 20);
            this.lblNgay.TabIndex = 5;
            this.lblNgay.Text = "Ngày:";
            // 
            // cboDonVi
            // 
            this.cboDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.ItemHeight = 24;
            this.cboDonVi.Location = new System.Drawing.Point(200, 10);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(400, 30);
            this.cboDonVi.TabIndex = 16;
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonVi.Location = new System.Drawing.Point(13, 13);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(63, 20);
            this.lblDonVi.TabIndex = 17;
            this.lblDonVi.Text = "Đơn vị:";
            // 
            // lblThongTinDonVi
            // 
            this.lblThongTinDonVi.AutoSize = true;
            this.lblThongTinDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinDonVi.ForeColor = System.Drawing.Color.Gray;
            this.lblThongTinDonVi.Location = new System.Drawing.Point(620, 13);
            this.lblThongTinDonVi.Name = "lblThongTinDonVi";
            this.lblThongTinDonVi.Size = new System.Drawing.Size(125, 20);
            this.lblThongTinDonVi.TabIndex = 2;
            this.lblThongTinDonVi.Text = "Chưa chọn đơn vị";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(800, 677);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(133, 40);
            this.btnLuu.Style = MetroFramework.MetroColorStyle.Red;
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHinhThuc.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblHinhThuc.Location = new System.Drawing.Point(13, 42);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(176, 20);
            this.lblHinhThuc.TabIndex = 3;
            this.lblHinhThuc.Text = "Hình thức khen thưởng:";
            // 
            // PageKhenThuongDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageKhenThuongDonVi";
            this.Size = new System.Drawing.Size(1600, 900);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonVi)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDonVi;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroButton btnTimKiem;
        private MetroFramework.Controls.MetroTextBox txtTimKiem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelHinhThuc;
        private MetroFramework.Controls.MetroLabel lblFileDinhKem;
        private MetroFramework.Controls.MetroLink linkFileDinhKem;
        private System.Windows.Forms.RichTextBox rtxtNoiDung;
        private MetroFramework.Controls.MetroLabel lblNoiDung;
        private MetroFramework.Controls.MetroTextBox txtCapQuyetDinh;
        private MetroFramework.Controls.MetroLabel lblCapQuyetDinh;
        private MetroFramework.Controls.MetroTextBox txtSoQuyetDinh;
        private MetroFramework.Controls.MetroLabel lblSoQuyetDinh;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private MetroFramework.Controls.MetroLabel lblNgay;
        private MetroFramework.Controls.MetroComboBox cboDonVi;
        private MetroFramework.Controls.MetroLabel lblDonVi;
        private MetroFramework.Controls.MetroLabel lblThongTinDonVi;
        private MetroFramework.Controls.MetroButton btnLuu;
        private MetroFramework.Controls.MetroLabel lblHinhThuc;
    }
}

