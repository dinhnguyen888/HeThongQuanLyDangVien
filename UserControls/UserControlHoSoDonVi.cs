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
    public partial class UserControlHoSoDonVi : UserControl
    {
        public UserControlHoSoDonVi()
        {
            InitializeComponent();
            var ucDanhSachChiBo = new PageDanhSachChiBo();
            ucDanhSachChiBo.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDanhSachChiBo);
        }

        private void DanhSachChiBo_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucDanhSachChibo = new PageDanhSachChiBo();
            ucDanhSachChibo.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDanhSachChibo);
        }

        private void SinhHoatChiBo_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucSinhHoatChiBo = new PageSinhHoatChiBo();
            ucSinhHoatChiBo.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucSinhHoatChiBo);
        }
    }
}
