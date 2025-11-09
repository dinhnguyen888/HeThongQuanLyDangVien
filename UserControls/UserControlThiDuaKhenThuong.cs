using QuanLyDangVien.Pages;
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
    public partial class UserControlThiDuaKhenThuong : UserControl
    {
        public UserControlThiDuaKhenThuong()
        {
            InitializeComponent();
            Panel.Controls.Clear();
            var ucKhenThuong = new PageKhenThuong();
            ucKhenThuong.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucKhenThuong);
        }

        private void KhenThuong_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucKhenThuong = new PageKhenThuong();
            ucKhenThuong.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucKhenThuong);
        }

        private void KhenThuongDonVi_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucKhenThuongDonVi = new PageKhenThuongDonVi();
            ucKhenThuongDonVi.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucKhenThuongDonVi);
        }

        private void KyLuat_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucKyLuat = new PageKyLuat();
            ucKyLuat.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucKyLuat);
        }

        private void KyLuatToChuc_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucKyLuatToChuc = new PageKyLuatToChuc();
            ucKyLuatToChuc.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucKyLuatToChuc);
        }

        private void DanhSach_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucDanhSachThiDua = new PageDanhSachKhenThuongKyLuat();
            ucDanhSachThiDua.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDanhSachThiDua);
        }
    }
}
