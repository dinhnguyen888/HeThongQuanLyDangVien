namespace QuanLyDangVien.Pages
{
    partial class PageKyLuat
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
            this.dgvDangVien = new System.Windows.Forms.DataGridView();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboLocTheo = new MetroFramework.Controls.MetroComboBox();
            this.btnTimKiem = new MetroFramework.Controls.MetroButton();
            this.txtTimKiem = new MetroFramework.Controls.MetroTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelHinhThuc = new System.Windows.Forms.Panel();
            this.lblFileDinhKem = new MetroFramework.Controls.MetroLabel();
            this.linkFileDinhKem = new MetroFramework.Controls.MetroLink();
            this.rtxtGhiChu = new System.Windows.Forms.RichTextBox();
            this.lblGhiChu = new MetroFramework.Controls.MetroLabel();
            this.rtxtNoiDung = new System.Windows.Forms.RichTextBox();
            this.lblNoiDung = new MetroFramework.Controls.MetroLabel();
            this.txtCapQuyetDinh = new MetroFramework.Controls.MetroTextBox();
            this.lblCapQuyetDinh = new MetroFramework.Controls.MetroLabel();
            this.txtSoQuyetDinh = new MetroFramework.Controls.MetroTextBox();
            this.lblSoQuyetDinh = new MetroFramework.Controls.MetroLabel();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNgay = new MetroFramework.Controls.MetroLabel();
            this.lblLoaiDangVien = new MetroFramework.Controls.MetroLabel();
            this.lblThongTinDangVien = new MetroFramework.Controls.MetroLabel();
            this.btnLuu = new MetroFramework.Controls.MetroButton();
            this.lblHinhThuc = new MetroFramework.Controls.MetroLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangVien)).BeginInit();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1600, 900);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDangVien);
            this.panel1.Controls.Add(this.metroTile1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(626, 874);
            this.panel1.TabIndex = 0;
            // 
            // dgvDangVien
            // 
            this.dgvDangVien.AllowUserToAddRows = false;
            this.dgvDangVien.AllowUserToDeleteRows = false;
            this.dgvDangVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDangVien.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDangVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDangVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDangVien.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDangVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangVien.Location = new System.Drawing.Point(0, 40);
            this.dgvDangVien.MultiSelect = false;
            this.dgvDangVien.Name = "dgvDangVien";
            this.dgvDangVien.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDangVien.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDangVien.RowHeadersWidth = 51;
            this.dgvDangVien.RowTemplate.Height = 30;
            this.dgvDangVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDangVien.Size = new System.Drawing.Size(626, 834);
            this.dgvDangVien.TabIndex = 1;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(626, 40);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Danh sách Đảng viên";
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
            this.panel2.Size = new System.Drawing.Size(942, 302);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cboLocTheo);
            this.panel3.Controls.Add(this.btnTimKiem);
            this.panel3.Controls.Add(this.txtTimKiem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(922, 282);
            this.panel3.TabIndex = 0;
            // 
            // cboLocTheo
            // 
            this.cboLocTheo.FormattingEnabled = true;
            this.cboLocTheo.ItemHeight = 24;
            this.cboLocTheo.Location = new System.Drawing.Point(13, 60);
            this.cboLocTheo.Name = "cboLocTheo";
            this.cboLocTheo.Size = new System.Drawing.Size(250, 30);
            this.cboLocTheo.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(800, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 40);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTimKiem.Location = new System.Drawing.Point(13, 13);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PromptText = "Tìm kiếm theo tên, CCCD, số thẻ Đảng...";
            this.txtTimKiem.Size = new System.Drawing.Size(780, 40);
            this.txtTimKiem.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelHinhThuc);
            this.panel4.Controls.Add(this.lblFileDinhKem);
            this.panel4.Controls.Add(this.linkFileDinhKem);
            this.panel4.Controls.Add(this.rtxtGhiChu);
            this.panel4.Controls.Add(this.lblGhiChu);
            this.panel4.Controls.Add(this.rtxtNoiDung);
            this.panel4.Controls.Add(this.lblNoiDung);
            this.panel4.Controls.Add(this.txtCapQuyetDinh);
            this.panel4.Controls.Add(this.lblCapQuyetDinh);
            this.panel4.Controls.Add(this.txtSoQuyetDinh);
            this.panel4.Controls.Add(this.lblSoQuyetDinh);
            this.panel4.Controls.Add(this.dtpNgay);
            this.panel4.Controls.Add(this.lblNgay);
            this.panel4.Controls.Add(this.lblLoaiDangVien);
            this.panel4.Controls.Add(this.lblThongTinDangVien);
            this.panel4.Controls.Add(this.btnLuu);
            this.panel4.Controls.Add(this.lblHinhThuc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(645, 321);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(942, 566);
            this.panel4.TabIndex = 2;
            // 
            // panelHinhThuc
            // 
            this.panelHinhThuc.AutoScroll = true;
            this.panelHinhThuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHinhThuc.Location = new System.Drawing.Point(13, 35);
            this.panelHinhThuc.Name = "panelHinhThuc";
            this.panelHinhThuc.Size = new System.Drawing.Size(920, 120);
            this.panelHinhThuc.TabIndex = 16;
            // 
            // lblFileDinhKem
            // 
            this.lblFileDinhKem.AutoSize = true;
            this.lblFileDinhKem.ForeColor = System.Drawing.Color.Gray;
            this.lblFileDinhKem.Location = new System.Drawing.Point(150, 500);
            this.lblFileDinhKem.Name = "lblFileDinhKem";
            this.lblFileDinhKem.Size = new System.Drawing.Size(98, 20);
            this.lblFileDinhKem.TabIndex = 15;
            this.lblFileDinhKem.Text = "Chưa chọn file";
            // 
            // linkFileDinhKem
            // 
            this.linkFileDinhKem.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.linkFileDinhKem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.linkFileDinhKem.Location = new System.Drawing.Point(13, 500);
            this.linkFileDinhKem.Name = "linkFileDinhKem";
            this.linkFileDinhKem.Size = new System.Drawing.Size(130, 23);
            this.linkFileDinhKem.TabIndex = 14;
            this.linkFileDinhKem.Text = "File đính kèm";
            this.linkFileDinhKem.UseStyleColors = true;
            // 
            // rtxtGhiChu
            // 
            this.rtxtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtGhiChu.Location = new System.Drawing.Point(480, 295);
            this.rtxtGhiChu.Name = "rtxtGhiChu";
            this.rtxtGhiChu.Size = new System.Drawing.Size(453, 199);
            this.rtxtGhiChu.TabIndex = 13;
            this.rtxtGhiChu.Text = "";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(480, 270);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(60, 20);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // rtxtNoiDung
            // 
            this.rtxtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNoiDung.Location = new System.Drawing.Point(13, 295);
            this.rtxtNoiDung.Name = "rtxtNoiDung";
            this.rtxtNoiDung.Size = new System.Drawing.Size(450, 199);
            this.rtxtNoiDung.TabIndex = 11;
            this.rtxtNoiDung.Text = "";
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(13, 270);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(70, 20);
            this.lblNoiDung.TabIndex = 10;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtCapQuyetDinh
            // 
            this.txtCapQuyetDinh.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCapQuyetDinh.Location = new System.Drawing.Point(620, 225);
            this.txtCapQuyetDinh.Name = "txtCapQuyetDinh";
            this.txtCapQuyetDinh.PromptText = "Cấp quyết định";
            this.txtCapQuyetDinh.Size = new System.Drawing.Size(313, 30);
            this.txtCapQuyetDinh.TabIndex = 9;
            // 
            // lblCapQuyetDinh
            // 
            this.lblCapQuyetDinh.AutoSize = true;
            this.lblCapQuyetDinh.Location = new System.Drawing.Point(620, 200);
            this.lblCapQuyetDinh.Name = "lblCapQuyetDinh";
            this.lblCapQuyetDinh.Size = new System.Drawing.Size(106, 20);
            this.lblCapQuyetDinh.TabIndex = 8;
            this.lblCapQuyetDinh.Text = "Cấp quyết định:";
            // 
            // txtSoQuyetDinh
            // 
            this.txtSoQuyetDinh.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSoQuyetDinh.Location = new System.Drawing.Point(280, 225);
            this.txtSoQuyetDinh.Name = "txtSoQuyetDinh";
            this.txtSoQuyetDinh.PromptText = "Số quyết định";
            this.txtSoQuyetDinh.Size = new System.Drawing.Size(320, 30);
            this.txtSoQuyetDinh.TabIndex = 7;
            // 
            // lblSoQuyetDinh
            // 
            this.lblSoQuyetDinh.AutoSize = true;
            this.lblSoQuyetDinh.Location = new System.Drawing.Point(280, 200);
            this.lblSoQuyetDinh.Name = "lblSoQuyetDinh";
            this.lblSoQuyetDinh.Size = new System.Drawing.Size(97, 20);
            this.lblSoQuyetDinh.TabIndex = 6;
            this.lblSoQuyetDinh.Text = "Số quyết định:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Location = new System.Drawing.Point(13, 225);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(250, 26);
            this.dtpNgay.TabIndex = 5;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(13, 200);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(45, 20);
            this.lblNgay.TabIndex = 4;
            this.lblNgay.Text = "Ngày:";
            // 
            // lblLoaiDangVien
            // 
            this.lblLoaiDangVien.AutoSize = true;
            this.lblLoaiDangVien.ForeColor = System.Drawing.Color.Gray;
            this.lblLoaiDangVien.Location = new System.Drawing.Point(13, 165);
            this.lblLoaiDangVien.Name = "lblLoaiDangVien";
            this.lblLoaiDangVien.Size = new System.Drawing.Size(141, 20);
            this.lblLoaiDangVien.TabIndex = 3;
            this.lblLoaiDangVien.Text = "Chưa chọn đảng viên";
            // 
            // lblThongTinDangVien
            // 
            this.lblThongTinDangVien.AutoSize = true;
            this.lblThongTinDangVien.ForeColor = System.Drawing.Color.Gray;
            this.lblThongTinDangVien.Location = new System.Drawing.Point(200, 10);
            this.lblThongTinDangVien.Name = "lblThongTinDangVien";
            this.lblThongTinDangVien.Size = new System.Drawing.Size(141, 20);
            this.lblThongTinDangVien.TabIndex = 1;
            this.lblThongTinDangVien.Text = "Chưa chọn đảng viên";
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(800, 500);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(133, 40);
            this.btnLuu.Style = MetroFramework.MetroColorStyle.Red;
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblHinhThuc.Location = new System.Drawing.Point(13, 10);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(133, 20);
            this.lblHinhThuc.TabIndex = 2;
            this.lblHinhThuc.Text = "Hình thức kỷ luật:";
            // 
            // PageKyLuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageKyLuat";
            this.Size = new System.Drawing.Size(1600, 900);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangVien)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDangVien;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroComboBox cboLocTheo;
        private MetroFramework.Controls.MetroButton btnTimKiem;
        private MetroFramework.Controls.MetroTextBox txtTimKiem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelHinhThuc;
        private MetroFramework.Controls.MetroLabel lblFileDinhKem;
        private MetroFramework.Controls.MetroLink linkFileDinhKem;
        private System.Windows.Forms.RichTextBox rtxtGhiChu;
        private MetroFramework.Controls.MetroLabel lblGhiChu;
        private System.Windows.Forms.RichTextBox rtxtNoiDung;
        private MetroFramework.Controls.MetroLabel lblNoiDung;
        private MetroFramework.Controls.MetroTextBox txtCapQuyetDinh;
        private MetroFramework.Controls.MetroLabel lblCapQuyetDinh;
        private MetroFramework.Controls.MetroTextBox txtSoQuyetDinh;
        private MetroFramework.Controls.MetroLabel lblSoQuyetDinh;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private MetroFramework.Controls.MetroLabel lblNgay;
        private MetroFramework.Controls.MetroLabel lblLoaiDangVien;
        private MetroFramework.Controls.MetroLabel lblThongTinDangVien;
        private MetroFramework.Controls.MetroButton btnLuu;
        private MetroFramework.Controls.MetroLabel lblHinhThuc;
    }
}
