namespace QuanLyDangVien
{
    partial class UserControlBaoCaoThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panelCharts = new System.Windows.Forms.Panel();
            this.panelKetNapChuyen = new System.Windows.Forms.Panel();
            this.lblKetNapChuyenTitle = new MetroFramework.Controls.MetroLabel();
            this.chartKetNapChuyen = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelDonVi = new System.Windows.Forms.Panel();
            this.lblDonViTitle = new MetroFramework.Controls.MetroLabel();
            this.chartDonVi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelCards = new System.Windows.Forms.TableLayoutPanel();
            this.panelCardTongSoDangVien = new System.Windows.Forms.Panel();
            this.lblTongSoDangVienValue = new System.Windows.Forms.Label();
            this.lblTongSoDangVienText = new System.Windows.Forms.Label();
            this.panelCardTongSoDonVi = new System.Windows.Forms.Panel();
            this.lblTongSoDonViValue = new System.Windows.Forms.Label();
            this.lblTongSoDonViText = new System.Windows.Forms.Label();
            this.panelCardKetNapMoi = new System.Windows.Forms.Panel();
            this.lblKetNapMoiValue = new System.Windows.Forms.Label();
            this.lblKetNapMoiText = new System.Windows.Forms.Label();
            this.panelCardChuyenSinhHoat = new System.Windows.Forms.Panel();
            this.lblChuyenSinhHoatValue = new System.Windows.Forms.Label();
            this.lblChuyenSinhHoatText = new System.Windows.Forms.Label();
            this.panelCardChinhThuc = new System.Windows.Forms.Panel();
            this.lblChinhThucValue = new System.Windows.Forms.Label();
            this.lblChinhThucText = new System.Windows.Forms.Label();
            this.panelCardDuBi = new System.Windows.Forms.Panel();
            this.lblDuBiValue = new System.Windows.Forms.Label();
            this.lblDuBiText = new System.Windows.Forms.Label();
            this.panelCardKhenThuong = new System.Windows.Forms.Panel();
            this.lblKhenThuongValue = new System.Windows.Forms.Label();
            this.lblKhenThuongText = new System.Windows.Forms.Label();
            this.panelCardKyLuat = new System.Windows.Forms.Panel();
            this.lblKyLuatValue = new System.Windows.Forms.Label();
            this.lblKyLuatText = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.cboNam = new MetroFramework.Controls.MetroComboBox();
            this.lblNam = new MetroFramework.Controls.MetroLabel();
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            this.scrollPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.panelCharts.SuspendLayout();
            this.panelKetNapChuyen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKetNapChuyen)).BeginInit();
            this.panelDonVi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDonVi)).BeginInit();
            this.panelCards.SuspendLayout();
            this.panelCardTongSoDangVien.SuspendLayout();
            this.panelCardTongSoDonVi.SuspendLayout();
            this.panelCardKetNapMoi.SuspendLayout();
            this.panelCardChuyenSinhHoat.SuspendLayout();
            this.panelCardChinhThuc.SuspendLayout();
            this.panelCardDuBi.SuspendLayout();
            this.panelCardKhenThuong.SuspendLayout();
            this.panelCardKyLuat.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.mainPanel);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(1200, 600);
            this.scrollPanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.panelCharts);
            this.mainPanel.Controls.Add(this.panelCards);
            this.mainPanel.Controls.Add(this.panelHeader);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainPanel.Size = new System.Drawing.Size(1179, 800);
            this.mainPanel.TabIndex = 0;
            // 
            // panelCharts
            // 
            this.panelCharts.Controls.Add(this.panelKetNapChuyen);
            this.panelCharts.Controls.Add(this.panelDonVi);
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCharts.Location = new System.Drawing.Point(10, 420);
            this.panelCharts.Name = "panelCharts";
            this.panelCharts.Size = new System.Drawing.Size(1159, 380);
            this.panelCharts.TabIndex = 2;
            // 
            // panelKetNapChuyen
            // 
            this.panelKetNapChuyen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKetNapChuyen.Controls.Add(this.lblKetNapChuyenTitle);
            this.panelKetNapChuyen.Controls.Add(this.chartKetNapChuyen);
            this.panelKetNapChuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKetNapChuyen.Location = new System.Drawing.Point(586, 0);
            this.panelKetNapChuyen.Name = "panelKetNapChuyen";
            this.panelKetNapChuyen.Padding = new System.Windows.Forms.Padding(10);
            this.panelKetNapChuyen.Size = new System.Drawing.Size(573, 380);
            this.panelKetNapChuyen.TabIndex = 1;
            // 
            // lblKetNapChuyenTitle
            // 
            this.lblKetNapChuyenTitle.AutoSize = true;
            this.lblKetNapChuyenTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblKetNapChuyenTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblKetNapChuyenTitle.Location = new System.Drawing.Point(13, 13);
            this.lblKetNapChuyenTitle.Name = "lblKetNapChuyenTitle";
            this.lblKetNapChuyenTitle.Size = new System.Drawing.Size(257, 25);
            this.lblKetNapChuyenTitle.TabIndex = 1;
            this.lblKetNapChuyenTitle.Text = "Kết nạp mới / Chuyển đi-đến";
            // 
            // chartKetNapChuyen
            // 
            chartArea1.Name = "ChartArea1";
            this.chartKetNapChuyen.ChartAreas.Add(chartArea1);
            this.chartKetNapChuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartKetNapChuyen.Legends.Add(legend1);
            this.chartKetNapChuyen.Location = new System.Drawing.Point(10, 10);
            this.chartKetNapChuyen.Name = "chartKetNapChuyen";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Kết nạp mới";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Chuyển đi-đến";
            this.chartKetNapChuyen.Series.Add(series1);
            this.chartKetNapChuyen.Series.Add(series2);
            this.chartKetNapChuyen.Size = new System.Drawing.Size(551, 358);
            this.chartKetNapChuyen.TabIndex = 0;
            this.chartKetNapChuyen.Text = "chartKetNapChuyen";
            // 
            // panelDonVi
            // 
            this.panelDonVi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDonVi.Controls.Add(this.lblDonViTitle);
            this.panelDonVi.Controls.Add(this.chartDonVi);
            this.panelDonVi.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDonVi.Location = new System.Drawing.Point(0, 0);
            this.panelDonVi.Name = "panelDonVi";
            this.panelDonVi.Padding = new System.Windows.Forms.Padding(10);
            this.panelDonVi.Size = new System.Drawing.Size(586, 380);
            this.panelDonVi.TabIndex = 0;
            // 
            // lblDonViTitle
            // 
            this.lblDonViTitle.AutoSize = true;
            this.lblDonViTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDonViTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblDonViTitle.Location = new System.Drawing.Point(13, 13);
            this.lblDonViTitle.Name = "lblDonViTitle";
            this.lblDonViTitle.Size = new System.Drawing.Size(181, 25);
            this.lblDonViTitle.TabIndex = 1;
            this.lblDonViTitle.Text = "Biểu đồ theo Đơn vị";
            // 
            // chartDonVi
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDonVi.ChartAreas.Add(chartArea2);
            this.chartDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartDonVi.Legends.Add(legend2);
            this.chartDonVi.Location = new System.Drawing.Point(10, 10);
            this.chartDonVi.Name = "chartDonVi";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Số lượng";
            this.chartDonVi.Series.Add(series3);
            this.chartDonVi.Size = new System.Drawing.Size(564, 358);
            this.chartDonVi.TabIndex = 0;
            this.chartDonVi.Text = "chartDonVi";
            // 
            // panelCards
            // 
            this.panelCards.ColumnCount = 4;
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.Controls.Add(this.panelCardTongSoDangVien, 0, 0);
            this.panelCards.Controls.Add(this.panelCardTongSoDonVi, 1, 0);
            this.panelCards.Controls.Add(this.panelCardKetNapMoi, 2, 0);
            this.panelCards.Controls.Add(this.panelCardChuyenSinhHoat, 3, 0);
            this.panelCards.Controls.Add(this.panelCardChinhThuc, 0, 1);
            this.panelCards.Controls.Add(this.panelCardDuBi, 1, 1);
            this.panelCards.Controls.Add(this.panelCardKhenThuong, 2, 1);
            this.panelCards.Controls.Add(this.panelCardKyLuat, 3, 1);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(10, 80);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(5);
            this.panelCards.RowCount = 2;
            this.panelCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelCards.Size = new System.Drawing.Size(1159, 340);
            this.panelCards.TabIndex = 1;
            // 
            // panelCardTongSoDangVien
            // 
            this.panelCardTongSoDangVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelCardTongSoDangVien.Controls.Add(this.lblTongSoDangVienValue);
            this.panelCardTongSoDangVien.Controls.Add(this.lblTongSoDangVienText);
            this.panelCardTongSoDangVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardTongSoDangVien.Location = new System.Drawing.Point(8, 8);
            this.panelCardTongSoDangVien.Name = "panelCardTongSoDangVien";
            this.panelCardTongSoDangVien.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardTongSoDangVien.Size = new System.Drawing.Size(281, 159);
            this.panelCardTongSoDangVien.TabIndex = 0;
            // 
            // lblTongSoDangVienValue
            // 
            this.lblTongSoDangVienValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongSoDangVienValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSoDangVienValue.ForeColor = System.Drawing.Color.White;
            this.lblTongSoDangVienValue.Location = new System.Drawing.Point(20, 20);
            this.lblTongSoDangVienValue.Name = "lblTongSoDangVienValue";
            this.lblTongSoDangVienValue.Size = new System.Drawing.Size(241, 90);
            this.lblTongSoDangVienValue.TabIndex = 1;
            this.lblTongSoDangVienValue.Text = "0";
            this.lblTongSoDangVienValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongSoDangVienText
            // 
            this.lblTongSoDangVienText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongSoDangVienText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSoDangVienText.ForeColor = System.Drawing.Color.White;
            this.lblTongSoDangVienText.Location = new System.Drawing.Point(20, 110);
            this.lblTongSoDangVienText.Name = "lblTongSoDangVienText";
            this.lblTongSoDangVienText.Size = new System.Drawing.Size(241, 29);
            this.lblTongSoDangVienText.TabIndex = 0;
            this.lblTongSoDangVienText.Text = "Tổng số đảng viên";
            this.lblTongSoDangVienText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardTongSoDonVi
            // 
            this.panelCardTongSoDonVi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panelCardTongSoDonVi.Controls.Add(this.lblTongSoDonViValue);
            this.panelCardTongSoDonVi.Controls.Add(this.lblTongSoDonViText);
            this.panelCardTongSoDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardTongSoDonVi.Location = new System.Drawing.Point(295, 8);
            this.panelCardTongSoDonVi.Name = "panelCardTongSoDonVi";
            this.panelCardTongSoDonVi.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardTongSoDonVi.Size = new System.Drawing.Size(281, 159);
            this.panelCardTongSoDonVi.TabIndex = 1;
            // 
            // lblTongSoDonViValue
            // 
            this.lblTongSoDonViValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongSoDonViValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSoDonViValue.ForeColor = System.Drawing.Color.White;
            this.lblTongSoDonViValue.Location = new System.Drawing.Point(20, 20);
            this.lblTongSoDonViValue.Name = "lblTongSoDonViValue";
            this.lblTongSoDonViValue.Size = new System.Drawing.Size(241, 90);
            this.lblTongSoDonViValue.TabIndex = 1;
            this.lblTongSoDonViValue.Text = "0";
            this.lblTongSoDonViValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongSoDonViText
            // 
            this.lblTongSoDonViText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongSoDonViText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSoDonViText.ForeColor = System.Drawing.Color.White;
            this.lblTongSoDonViText.Location = new System.Drawing.Point(20, 110);
            this.lblTongSoDonViText.Name = "lblTongSoDonViText";
            this.lblTongSoDonViText.Size = new System.Drawing.Size(241, 29);
            this.lblTongSoDonViText.TabIndex = 0;
            this.lblTongSoDonViText.Text = "Tổng số đơn vị";
            this.lblTongSoDonViText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardKetNapMoi
            // 
            this.panelCardKetNapMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.panelCardKetNapMoi.Controls.Add(this.lblKetNapMoiValue);
            this.panelCardKetNapMoi.Controls.Add(this.lblKetNapMoiText);
            this.panelCardKetNapMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardKetNapMoi.Location = new System.Drawing.Point(582, 8);
            this.panelCardKetNapMoi.Name = "panelCardKetNapMoi";
            this.panelCardKetNapMoi.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardKetNapMoi.Size = new System.Drawing.Size(281, 159);
            this.panelCardKetNapMoi.TabIndex = 2;
            // 
            // lblKetNapMoiValue
            // 
            this.lblKetNapMoiValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKetNapMoiValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetNapMoiValue.ForeColor = System.Drawing.Color.White;
            this.lblKetNapMoiValue.Location = new System.Drawing.Point(20, 20);
            this.lblKetNapMoiValue.Name = "lblKetNapMoiValue";
            this.lblKetNapMoiValue.Size = new System.Drawing.Size(241, 90);
            this.lblKetNapMoiValue.TabIndex = 1;
            this.lblKetNapMoiValue.Text = "0";
            this.lblKetNapMoiValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKetNapMoiText
            // 
            this.lblKetNapMoiText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblKetNapMoiText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetNapMoiText.ForeColor = System.Drawing.Color.White;
            this.lblKetNapMoiText.Location = new System.Drawing.Point(20, 110);
            this.lblKetNapMoiText.Name = "lblKetNapMoiText";
            this.lblKetNapMoiText.Size = new System.Drawing.Size(241, 29);
            this.lblKetNapMoiText.TabIndex = 0;
            this.lblKetNapMoiText.Text = "Kết nạp mới trong năm";
            this.lblKetNapMoiText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardChuyenSinhHoat
            // 
            this.panelCardChuyenSinhHoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.panelCardChuyenSinhHoat.Controls.Add(this.lblChuyenSinhHoatValue);
            this.panelCardChuyenSinhHoat.Controls.Add(this.lblChuyenSinhHoatText);
            this.panelCardChuyenSinhHoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardChuyenSinhHoat.Location = new System.Drawing.Point(869, 8);
            this.panelCardChuyenSinhHoat.Name = "panelCardChuyenSinhHoat";
            this.panelCardChuyenSinhHoat.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardChuyenSinhHoat.Size = new System.Drawing.Size(282, 159);
            this.panelCardChuyenSinhHoat.TabIndex = 3;
            // 
            // lblChuyenSinhHoatValue
            // 
            this.lblChuyenSinhHoatValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChuyenSinhHoatValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuyenSinhHoatValue.ForeColor = System.Drawing.Color.White;
            this.lblChuyenSinhHoatValue.Location = new System.Drawing.Point(20, 20);
            this.lblChuyenSinhHoatValue.Name = "lblChuyenSinhHoatValue";
            this.lblChuyenSinhHoatValue.Size = new System.Drawing.Size(242, 90);
            this.lblChuyenSinhHoatValue.TabIndex = 1;
            this.lblChuyenSinhHoatValue.Text = "0";
            this.lblChuyenSinhHoatValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChuyenSinhHoatText
            // 
            this.lblChuyenSinhHoatText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblChuyenSinhHoatText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuyenSinhHoatText.ForeColor = System.Drawing.Color.White;
            this.lblChuyenSinhHoatText.Location = new System.Drawing.Point(20, 110);
            this.lblChuyenSinhHoatText.Name = "lblChuyenSinhHoatText";
            this.lblChuyenSinhHoatText.Size = new System.Drawing.Size(242, 29);
            this.lblChuyenSinhHoatText.TabIndex = 0;
            this.lblChuyenSinhHoatText.Text = "Chuyển sinh hoạt trong năm";
            this.lblChuyenSinhHoatText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardChinhThuc
            // 
            this.panelCardChinhThuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panelCardChinhThuc.Controls.Add(this.lblChinhThucValue);
            this.panelCardChinhThuc.Controls.Add(this.lblChinhThucText);
            this.panelCardChinhThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardChinhThuc.Location = new System.Drawing.Point(8, 173);
            this.panelCardChinhThuc.Name = "panelCardChinhThuc";
            this.panelCardChinhThuc.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardChinhThuc.Size = new System.Drawing.Size(281, 159);
            this.panelCardChinhThuc.TabIndex = 4;
            // 
            // lblChinhThucValue
            // 
            this.lblChinhThucValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChinhThucValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChinhThucValue.ForeColor = System.Drawing.Color.White;
            this.lblChinhThucValue.Location = new System.Drawing.Point(20, 20);
            this.lblChinhThucValue.Name = "lblChinhThucValue";
            this.lblChinhThucValue.Size = new System.Drawing.Size(241, 90);
            this.lblChinhThucValue.TabIndex = 1;
            this.lblChinhThucValue.Text = "0";
            this.lblChinhThucValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChinhThucText
            // 
            this.lblChinhThucText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblChinhThucText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChinhThucText.ForeColor = System.Drawing.Color.White;
            this.lblChinhThucText.Location = new System.Drawing.Point(20, 110);
            this.lblChinhThucText.Name = "lblChinhThucText";
            this.lblChinhThucText.Size = new System.Drawing.Size(241, 29);
            this.lblChinhThucText.TabIndex = 0;
            this.lblChinhThucText.Text = "Đảng viên chính thức";
            this.lblChinhThucText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardDuBi
            // 
            this.panelCardDuBi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.panelCardDuBi.Controls.Add(this.lblDuBiValue);
            this.panelCardDuBi.Controls.Add(this.lblDuBiText);
            this.panelCardDuBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardDuBi.Location = new System.Drawing.Point(295, 173);
            this.panelCardDuBi.Name = "panelCardDuBi";
            this.panelCardDuBi.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardDuBi.Size = new System.Drawing.Size(281, 159);
            this.panelCardDuBi.TabIndex = 5;
            // 
            // lblDuBiValue
            // 
            this.lblDuBiValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDuBiValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuBiValue.ForeColor = System.Drawing.Color.White;
            this.lblDuBiValue.Location = new System.Drawing.Point(20, 20);
            this.lblDuBiValue.Name = "lblDuBiValue";
            this.lblDuBiValue.Size = new System.Drawing.Size(241, 90);
            this.lblDuBiValue.TabIndex = 1;
            this.lblDuBiValue.Text = "0";
            this.lblDuBiValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDuBiText
            // 
            this.lblDuBiText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDuBiText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuBiText.ForeColor = System.Drawing.Color.White;
            this.lblDuBiText.Location = new System.Drawing.Point(20, 110);
            this.lblDuBiText.Name = "lblDuBiText";
            this.lblDuBiText.Size = new System.Drawing.Size(241, 29);
            this.lblDuBiText.TabIndex = 0;
            this.lblDuBiText.Text = "Đảng viên dự bị";
            this.lblDuBiText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardKhenThuong
            // 
            this.panelCardKhenThuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelCardKhenThuong.Controls.Add(this.lblKhenThuongValue);
            this.panelCardKhenThuong.Controls.Add(this.lblKhenThuongText);
            this.panelCardKhenThuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardKhenThuong.Location = new System.Drawing.Point(582, 173);
            this.panelCardKhenThuong.Name = "panelCardKhenThuong";
            this.panelCardKhenThuong.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardKhenThuong.Size = new System.Drawing.Size(281, 159);
            this.panelCardKhenThuong.TabIndex = 6;
            // 
            // lblKhenThuongValue
            // 
            this.lblKhenThuongValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKhenThuongValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhenThuongValue.ForeColor = System.Drawing.Color.White;
            this.lblKhenThuongValue.Location = new System.Drawing.Point(20, 20);
            this.lblKhenThuongValue.Name = "lblKhenThuongValue";
            this.lblKhenThuongValue.Size = new System.Drawing.Size(241, 90);
            this.lblKhenThuongValue.TabIndex = 1;
            this.lblKhenThuongValue.Text = "0";
            this.lblKhenThuongValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKhenThuongText
            // 
            this.lblKhenThuongText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblKhenThuongText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhenThuongText.ForeColor = System.Drawing.Color.White;
            this.lblKhenThuongText.Location = new System.Drawing.Point(20, 110);
            this.lblKhenThuongText.Name = "lblKhenThuongText";
            this.lblKhenThuongText.Size = new System.Drawing.Size(241, 29);
            this.lblKhenThuongText.TabIndex = 0;
            this.lblKhenThuongText.Text = "Khen thưởng trong năm";
            this.lblKhenThuongText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCardKyLuat
            // 
            this.panelCardKyLuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panelCardKyLuat.Controls.Add(this.lblKyLuatValue);
            this.panelCardKyLuat.Controls.Add(this.lblKyLuatText);
            this.panelCardKyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardKyLuat.Location = new System.Drawing.Point(869, 173);
            this.panelCardKyLuat.Name = "panelCardKyLuat";
            this.panelCardKyLuat.Padding = new System.Windows.Forms.Padding(20);
            this.panelCardKyLuat.Size = new System.Drawing.Size(282, 159);
            this.panelCardKyLuat.TabIndex = 7;
            // 
            // lblKyLuatValue
            // 
            this.lblKyLuatValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKyLuatValue.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKyLuatValue.ForeColor = System.Drawing.Color.White;
            this.lblKyLuatValue.Location = new System.Drawing.Point(20, 20);
            this.lblKyLuatValue.Name = "lblKyLuatValue";
            this.lblKyLuatValue.Size = new System.Drawing.Size(242, 90);
            this.lblKyLuatValue.TabIndex = 1;
            this.lblKyLuatValue.Text = "0";
            this.lblKyLuatValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKyLuatText
            // 
            this.lblKyLuatText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblKyLuatText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKyLuatText.ForeColor = System.Drawing.Color.White;
            this.lblKyLuatText.Location = new System.Drawing.Point(20, 110);
            this.lblKyLuatText.Name = "lblKyLuatText";
            this.lblKyLuatText.Size = new System.Drawing.Size(242, 29);
            this.lblKyLuatText.TabIndex = 0;
            this.lblKyLuatText.Text = "Kỷ luật trong năm";
            this.lblKyLuatText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnXuatBaoCao);
            this.panelHeader.Controls.Add(this.cboNam);
            this.panelHeader.Controls.Add(this.lblNam);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(10, 10);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1159, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatBaoCao.FlatAppearance.BorderSize = 0;
            this.btnXuatBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatBaoCao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(996, 20);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(150, 35);
            this.btnXuatBaoCao.TabIndex = 3;
            this.btnXuatBaoCao.Text = "Xuất báo cáo Word";
            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // cboNam
            // 
            this.cboNam.FormattingEnabled = true;
            this.cboNam.ItemHeight = 24;
            this.cboNam.Location = new System.Drawing.Point(100, 25);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(150, 30);
            this.cboNam.TabIndex = 2;
            this.cboNam.SelectedIndexChanged += new System.EventHandler(this.cboNam_SelectedIndexChanged);
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(20, 30);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(76, 20);
            this.lblNam.TabIndex = 1;
            this.lblNam.Text = "Chọn năm:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblTitle.Location = new System.Drawing.Point(300, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO THỐNG KÊ";
            // 
            // UserControlBaoCaoThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollPanel);
            this.Name = "UserControlBaoCaoThongKe";
            this.Size = new System.Drawing.Size(1200, 600);
            this.scrollPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.panelCharts.ResumeLayout(false);
            this.panelKetNapChuyen.ResumeLayout(false);
            this.panelKetNapChuyen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKetNapChuyen)).EndInit();
            this.panelDonVi.ResumeLayout(false);
            this.panelDonVi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDonVi)).EndInit();
            this.panelCards.ResumeLayout(false);
            this.panelCardTongSoDangVien.ResumeLayout(false);
            this.panelCardTongSoDonVi.ResumeLayout(false);
            this.panelCardKetNapMoi.ResumeLayout(false);
            this.panelCardChuyenSinhHoat.ResumeLayout(false);
            this.panelCardChinhThuc.ResumeLayout(false);
            this.panelCardDuBi.ResumeLayout(false);
            this.panelCardKhenThuong.ResumeLayout(false);
            this.panelCardKyLuat.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panelHeader;
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblNam;
        private MetroFramework.Controls.MetroComboBox cboNam;
        private System.Windows.Forms.Button btnXuatBaoCao;
        private System.Windows.Forms.TableLayoutPanel panelCards;
        private System.Windows.Forms.Panel panelCardTongSoDangVien;
        private System.Windows.Forms.Label lblTongSoDangVienValue;
        private System.Windows.Forms.Label lblTongSoDangVienText;
        private System.Windows.Forms.Panel panelCardTongSoDonVi;
        private System.Windows.Forms.Label lblTongSoDonViValue;
        private System.Windows.Forms.Label lblTongSoDonViText;
        private System.Windows.Forms.Panel panelCardKetNapMoi;
        private System.Windows.Forms.Label lblKetNapMoiValue;
        private System.Windows.Forms.Label lblKetNapMoiText;
        private System.Windows.Forms.Panel panelCardChuyenSinhHoat;
        private System.Windows.Forms.Label lblChuyenSinhHoatValue;
        private System.Windows.Forms.Label lblChuyenSinhHoatText;
        private System.Windows.Forms.Panel panelCardChinhThuc;
        private System.Windows.Forms.Label lblChinhThucValue;
        private System.Windows.Forms.Label lblChinhThucText;
        private System.Windows.Forms.Panel panelCardDuBi;
        private System.Windows.Forms.Label lblDuBiValue;
        private System.Windows.Forms.Label lblDuBiText;
        private System.Windows.Forms.Panel panelCardKhenThuong;
        private System.Windows.Forms.Label lblKhenThuongValue;
        private System.Windows.Forms.Label lblKhenThuongText;
        private System.Windows.Forms.Panel panelCardKyLuat;
        private System.Windows.Forms.Label lblKyLuatValue;
        private System.Windows.Forms.Label lblKyLuatText;
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.Panel panelDonVi;
        private MetroFramework.Controls.MetroLabel lblDonViTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDonVi;
        private System.Windows.Forms.Panel panelKetNapChuyen;
        private MetroFramework.Controls.MetroLabel lblKetNapChuyenTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKetNapChuyen;
    }
}
