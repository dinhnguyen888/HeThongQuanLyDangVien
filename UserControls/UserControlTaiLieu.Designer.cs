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
            this.metroLink1 = new MetroFramework.Controls.MetroLink();
            this.ChonTheoCb = new MetroFramework.Controls.MetroComboBox();
            this.ChonCb = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // metroLink1
            // 
            this.metroLink1.Location = new System.Drawing.Point(674, 98);
            this.metroLink1.Name = "metroLink1";
            this.metroLink1.Size = new System.Drawing.Size(292, 23);
            this.metroLink1.TabIndex = 0;
            this.metroLink1.Text = "Ấn để xem văn bản lưu trên máy chủ ";
            // 
            // ChonTheoCb
            // 
            this.ChonTheoCb.FormattingEnabled = true;
            this.ChonTheoCb.ItemHeight = 24;
            this.ChonTheoCb.Location = new System.Drawing.Point(3, 91);
            this.ChonTheoCb.Name = "ChonTheoCb";
            this.ChonTheoCb.Size = new System.Drawing.Size(121, 30);
            this.ChonTheoCb.TabIndex = 1;
            // 
            // ChonCb
            // 
            this.ChonCb.FormattingEnabled = true;
            this.ChonCb.ItemHeight = 24;
            this.ChonCb.Location = new System.Drawing.Point(148, 91);
            this.ChonCb.Name = "ChonCb";
            this.ChonCb.Size = new System.Drawing.Size(121, 30);
            this.ChonCb.TabIndex = 2;
            // 
            // UserControlTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChonCb);
            this.Controls.Add(this.ChonTheoCb);
            this.Controls.Add(this.metroLink1);
            this.Name = "UserControlTaiLieu";
            this.Size = new System.Drawing.Size(969, 554);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLink metroLink1;
        private MetroFramework.Controls.MetroComboBox ChonTheoCb;
        private MetroFramework.Controls.MetroComboBox ChonCb;
    }
}
