using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    public partial class UserControlDangNhap : UserControl
    {
        public UserControlDangNhap()
        {
            InitializeComponent();
        }

        private void DangNhapBtn_Click(object sender, EventArgs e)
        {
            // Get TaiKhoan and MatKhau from text boxes
            string taiKhoan = TaiKhoanTb.Text;
            string matKhau = MatKhauTb.Text;

            // Call Login method from NguoiDungService
            var nguoiDungService = new Services.NguoiDungService();
            bool dt = nguoiDungService.Login(taiKhoan, matKhau);
            if (dt)
            {
                // Login successful
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Restart application
                Application.Restart();




            }
        }

        private void LienHe_Click(object sender, EventArgs e)
        {
            // Mở ra một MessageBox hiển thị email, số điện thoại, Tên
            MessageBox.Show("Email:samle@gmail.com \nSố điện thoại: 0123456789 \nTên: Mã Chung" ,"Liên hệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
