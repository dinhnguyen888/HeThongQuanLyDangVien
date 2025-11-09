using QuanLyDangVien.UserControls;
using System;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    public partial class HeThongQuanLyDangVien : MetroFramework.Forms.MetroForm
    {
        private TabPage tabDangNhap;
        private TabPage tabQuanLyQuanNhan;
        private TabPage tabHoSoDangVien;
        private TabPage tabHoSoDonVi;
        private TabPage tabChuyenSinhHoatDang;
        private TabPage tabThiDuaKhenThuong;
        private TabPage tabCongTacPhatTrienDang;
        private TabPage tabTaiLieu;
        private TabPage tabBaoCaoThongKe;
        private TabPage tabQuanTriHeThong;

        public HeThongQuanLyDangVien()
        {
            InitializeComponent();

            tabDangNhap = new TabPage("Đăng nhập") { Name = "DangNhap" };
            tabQuanLyQuanNhan = new TabPage("Quản lý Quân nhân") { Name = "QuanLyQuanNhan" };
            tabHoSoDangVien = new TabPage("Hồ sơ Đảng Viên") { Name = "HoSoDangVien" };
            tabHoSoDonVi = new TabPage("Hồ sơ Đơn Vị") { Name = "HoSoDonVi" };
            tabChuyenSinhHoatDang = new TabPage("Chuyển Sinh Hoạt Đảng") { Name = "ChuyenSinhHoatDang" };
            tabThiDuaKhenThuong = new TabPage("Thi Đua Khen Thưởng") { Name = "ThiDuaKhenThuong" };
            tabCongTacPhatTrienDang = new TabPage("Công Tác Phát Triển Đảng") { Name = "CongTacPhatTrienDang" };
            tabTaiLieu = new TabPage("Tài Liệu") { Name = "TaiLieu" };
            tabBaoCaoThongKe = new TabPage("Báo Cáo - Thống Kê") { Name = "BaoCaoThongKe" };
            tabQuanTriHeThong = new TabPage("Quản Trị Hệ Thống") { Name = "QuanTriHeThong" };

            TabControl.TabPages.Clear();
            TabControl.FontSize = MetroFramework.MetroTabControlSize.Medium;
            TabControl.TabPages.Add(tabDangNhap);

            MainPn.Controls.Clear();

            bool isInfoValid = Helper.SaveInforHelper.GetSavedInfo();
            if (!isInfoValid)
            {
                UserControlDangNhap userControlDangNhap = new UserControlDangNhap();
                userControlDangNhap.Dock = DockStyle.Fill;
                MainPn.Controls.Add(userControlDangNhap);
            }
            else
            {
                OnLoginSuccess();
            }

            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == null) return;

            MainPn.Controls.Clear();

            switch (TabControl.SelectedTab.Name)
            {
                case "QuanLyQuanNhan":
                    var ucQuanNhan = new UserControlDanhSachQuanNhan();
                    ucQuanNhan.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucQuanNhan);
                    break;

                case "HoSoDangVien":
                    var ucDangVien = new UserControlQuanLyDangVien();
                    ucDangVien.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucDangVien);
                    break;

                case "HoSoDonVi":
                    var ucDonVi = new UserControlHoSoDonVi();
                    ucDonVi.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucDonVi);
                    break;

                case "ChuyenSinhHoatDang":
                    var ucChuyenSHD = new UserControlChuyenSinhHoatDang();
                    ucChuyenSHD.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucChuyenSHD);
                    break;

                case "ThiDuaKhenThuong":
                    var ucKhenThuong = new UserControlThiDuaKhenThuong();
                    ucKhenThuong.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucKhenThuong);
                    break;

                case "CongTacPhatTrienDang":
                    var ucPhatTrien = new UserControlCongTacPhatTrienDang();
                    ucPhatTrien.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucPhatTrien);
                    break;
                case "TaiLieu":
                    var ucTaiLieu = new UserControlTaiLieu();
                    ucTaiLieu.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucTaiLieu);
                    break;

                case "BaoCaoThongKe":
                    var ucThongKe = new UserControlBaoCaoThongKe();
                    ucThongKe.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucThongKe);
                    break;

                case "QuanTriHeThong":
                    var ucQuanTri = new UserControlQuanTriHeThong();
                    ucQuanTri.Dock = DockStyle.Fill;
                    MainPn.Controls.Add(ucQuanTri);
                    break;

                default:
                    break;
            }
        }

        public void ShowAllTabs()
        {
            if (!TabControl.TabPages.Contains(tabHoSoDangVien))
            {
                TabControl.TabPages.Clear();
                TabControl.TabPages.Add(tabQuanLyQuanNhan);
                TabControl.TabPages.Add(tabHoSoDangVien);
                TabControl.TabPages.Add(tabHoSoDonVi);
                TabControl.TabPages.Add(tabChuyenSinhHoatDang);
                TabControl.TabPages.Add(tabThiDuaKhenThuong);
                TabControl.TabPages.Add(tabCongTacPhatTrienDang);
      
                TabControl.TabPages.Add(tabBaoCaoThongKe);
                TabControl.TabPages.Add(tabQuanTriHeThong);
                TabControl.TabPages.Add(tabTaiLieu);
            }
        }

        public void HideAllExceptLogin()
        {
            TabControl.TabPages.Clear();
            TabControl.TabPages.Add(tabDangNhap);
        }

        public void OnLoginSuccess()
        {
            ShowAllTabs();
            this.WindowState = FormWindowState.Maximized;
            TabControl.SelectedTab = tabQuanLyQuanNhan; // tự động bật tab "Quản lý Quân nhân"
            TabControl_SelectedIndexChanged(null, null); // hiển thị UserControl tương ứng
        }
    }
}
