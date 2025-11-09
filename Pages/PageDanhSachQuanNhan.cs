using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDangVien.UserControls;

namespace QuanLyDangVien.Pages
{
    public partial class PageDanhSachQuanNhan : UserControl
    {
        private UserControlDanhSachQuanNhan _userControlQuanNhan;

        public PageDanhSachQuanNhan()
        {
            InitializeComponent();
            InitializePage();
        }

        private void InitializePage()
        {
            // Tạo UserControl chính
            _userControlQuanNhan = new UserControlDanhSachQuanNhan();
            _userControlQuanNhan.Dock = DockStyle.Fill;
            
            // Thêm vào panel chính
            this.Controls.Add(_userControlQuanNhan);
        }

        /// <summary>
        /// Refresh dữ liệu khi cần thiết
        /// </summary>
        public void RefreshData()
        {
            _userControlQuanNhan?.Refresh();
        }
    }
}
