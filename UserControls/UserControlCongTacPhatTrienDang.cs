using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDangVien.Pages;
namespace QuanLyDangVien
{
    public partial class UserControlCongTacPhatTrienDang : UserControl
    {
        public UserControlCongTacPhatTrienDang()
        {
            InitializeComponent();
            // gán Page QuanLyDangVienDuBi vào Panel khi khởi tạo
            var ucDangVienDuBi = new PageQuanLyDangVienDuBi();
            ucDangVienDuBi.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDangVienDuBi);

        }

        private void QuanLyDangVienDuBi_Click(object sender, EventArgs e)
        {
            // gán Page QuanLyDangVienDuBi vào Panel
            Panel.Controls.Clear();
            var ucDangVienDuBi = new PageQuanLyDangVienDuBi();
            ucDangVienDuBi.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDangVienDuBi);

        }

        private void TheoDoiChuyenChinhThuc_Click(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            var ucDangVienDuBi = new PageTheoDoiChuyenChinhThuc();
            ucDangVienDuBi.Dock = DockStyle.Fill;
            Panel.Controls.Add(ucDangVienDuBi);
        }
    }
}
