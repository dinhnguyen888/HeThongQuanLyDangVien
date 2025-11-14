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
            
            // Chỉ khởi tạo Page khi không ở design mode để tránh lỗi NullRef
            if (!DesignMode)
            {
                // Mặc định hiển thị Page QuanLyDangVienDuBi
                ShowQuanLyDangVienDuBi();
            }
        }

        private void QuanLyDangVienDuBi_Click(object sender, EventArgs e)
        {
            ShowQuanLyDangVienDuBi();
            SetActiveButton(QuanLyDangVienDuBi);
        }

        private void TheoDoiChuyenChinhThuc_Click(object sender, EventArgs e)
        {
            ShowTheoDoiChuyenChinhThuc();
            SetActiveButton(TheoDoiChuyenChinhThuc);
        }

        private void ShowQuanLyDangVienDuBi()
        {
            if (DesignMode) return;
            
            contentPanel.Controls.Clear();
            var page = new PageQuanLyDangVienDuBi();
            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);
        }

        private void ShowTheoDoiChuyenChinhThuc()
        {
            if (DesignMode) return;
            
            contentPanel.Controls.Clear();
            var page = new PageTheoDoiChuyenChinhThuc();
            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);
        }

        private void SetActiveButton(MetroFramework.Controls.MetroTile activeButton)
        {
            // Reset all buttons to default style
            QuanLyDangVienDuBi.Style = MetroFramework.MetroColorStyle.Default;
            TheoDoiChuyenChinhThuc.Style = MetroFramework.MetroColorStyle.Default;
            
            // Set active button style
            activeButton.Style = MetroFramework.MetroColorStyle.Blue;
        }
    }
}
