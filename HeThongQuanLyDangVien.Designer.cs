namespace QuanLyDangVien
{
    partial class HeThongQuanLyDangVien
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new MetroFramework.Controls.MetroTabControl();
            this.MainPn = new MetroFramework.Controls.MetroPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.CustomBackground = true;
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.TabControl.Location = new System.Drawing.Point(20, 60);
            this.TabControl.Name = "TabControl";
            this.TabControl.Size = new System.Drawing.Size(1004, 39);
            this.TabControl.Style = MetroFramework.MetroColorStyle.Red;
            this.TabControl.TabIndex = 2;
            this.TabControl.UseStyleColors = true;
            // 
            // MainPn
            // 
            this.MainPn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPn.HorizontalScrollbarBarColor = true;
            this.MainPn.HorizontalScrollbarHighlightOnWheel = false;
            this.MainPn.HorizontalScrollbarSize = 10;
            this.MainPn.Location = new System.Drawing.Point(20, 99);
            this.MainPn.Name = "MainPn";
            this.MainPn.Size = new System.Drawing.Size(1004, 472);
            this.MainPn.TabIndex = 3;
            this.MainPn.VerticalScrollbarBarColor = true;
            this.MainPn.VerticalScrollbarHighlightOnWheel = false;
            this.MainPn.VerticalScrollbarSize = 10;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::QuanLyDangVien.Properties.Resources.anh_bua_liem;
            this.pictureBox2.Location = new System.Drawing.Point(81, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(55, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QuanLyDangVien.Properties.Resources.emblem_of_vietnam_communist_party;
            this.pictureBox1.Location = new System.Drawing.Point(20, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // HeThongQuanLyDangVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1044, 591);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MainPn);
            this.Controls.Add(this.TabControl);
            this.ForeColor = System.Drawing.Color.Red;
            this.Name = "HeThongQuanLyDangVien";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "                 Hệ Thống Quản Lý Đảng Viên";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl TabControl;
        public MetroFramework.Controls.MetroPanel MainPn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}