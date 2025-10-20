namespace QuanLyDangVien.Pages
{
    partial class PageSinhHoatChiBo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.NhacNhoCaNhan = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.NhacNho = new MetroFramework.Controls.MetroTile();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.16667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.83334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(974, 576);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroButton4);
            this.panel1.Controls.Add(this.metroButton3);
            this.panel1.Controls.Add(this.NhacNhoCaNhan);
            this.panel1.Controls.Add(this.metroButton2);
            this.panel1.Controls.Add(this.metroButton1);
            this.panel1.Controls.Add(this.NhacNho);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(968, 162);
            this.panel1.TabIndex = 0;
            // 
            // metroButton4
            // 
            this.metroButton4.Location = new System.Drawing.Point(699, 76);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(247, 46);
            this.metroButton4.TabIndex = 6;
            this.metroButton4.Text = "Up file nghị quyết cho buổi sinh hoạt";
            // 
            // metroButton3
            // 
            this.metroButton3.Location = new System.Drawing.Point(464, 76);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(229, 46);
            this.metroButton3.TabIndex = 5;
            this.metroButton3.Text = "Thêm nội dung cho Buổi sinh hoạt";
            // 
            // NhacNhoCaNhan
            // 
            this.NhacNhoCaNhan.Location = new System.Drawing.Point(120, 76);
            this.NhacNhoCaNhan.Name = "NhacNhoCaNhan";
            this.NhacNhoCaNhan.Size = new System.Drawing.Size(137, 46);
            this.NhacNhoCaNhan.TabIndex = 4;
            this.NhacNhoCaNhan.Text = "Nhắc nhở cá nhân";
            this.NhacNhoCaNhan.Click += new System.EventHandler(this.NhacNhoCaNhan_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(290, 76);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(151, 46);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "Điểm Danh";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(10, 76);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 46);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "Tạo lịch";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // NhacNho
            // 
            this.NhacNho.Dock = System.Windows.Forms.DockStyle.Top;
            this.NhacNho.Location = new System.Drawing.Point(0, 0);
            this.NhacNho.Name = "NhacNho";
            this.NhacNho.Size = new System.Drawing.Size(968, 30);
            this.NhacNho.TabIndex = 1;
            this.NhacNho.Text = "Thông báo: sắp đến buổi họp sinh hoạt chi bộ với tiêu đề:  ";
            this.NhacNho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NhacNho.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.NhacNho.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.metroTile1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 402);
            this.panel2.TabIndex = 1;
            // 
            // metroTile1
            // 
            this.metroTile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTile1.Location = new System.Drawing.Point(0, 0);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(968, 30);
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Lịch sinh hoạt chi bộ";
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(968, 372);
            this.dataGridView1.TabIndex = 4;
            // 
            // PageSinhHoatChiBo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageSinhHoatChiBo";
            this.Size = new System.Drawing.Size(974, 576);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTile NhacNho;
        private MetroFramework.Controls.MetroButton NhacNhoCaNhan;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroButton metroButton3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroTile metroTile1;
    }
}
