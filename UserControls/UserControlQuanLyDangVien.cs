using MetroFramework.Controls;
using QuanLyDangVien.Models;
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
    public partial class UserControlQuanLyDangVien : UserControl
    {
        public UserControlQuanLyDangVien()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(TimTb.Text))
            {
                TimTb.Text = " ";
                TimTb.Text = "";
            }
            var col = DangVienGridView.Columns["ChucNang"] as DataGridViewComboBoxColumn;
            col.Items.Clear();
            col.Items.Add("Xem chi tiết");
            col.Items.Add("Sửa thông tin");
            col.Items.Add("Xóa đảng viên");
            DangVienGridView.EditingControlShowing += DangVienGridView_EditingControlShowing;
            DangVienGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DangVienGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            DangVienGridView.DefaultCellStyle.Font = new Font("Arial", 12);
            DangVienGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void DangVienGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DangVienGridView.CurrentCell.OwningColumn.Name == "ChucNang" && e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ChucNang_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ChucNang_SelectedIndexChanged;
            }
        }

        private void ChucNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;
            if (comboBox.SelectedItem == null)
                return;

            string selectedAction = comboBox.SelectedItem.ToString();


            // Lấy dòng hiện tại
            var row = DangVienGridView.CurrentRow;
            if (row == null) return;

            // Giả sử có cột MaDangVien hoặc ID để định danh
            var id = row.Cells["dangVienIDDataGridViewTextBoxColumn"].Value?.ToString();

            switch (selectedAction)
            {
                case "Xem chi tiết":
                    MessageBox.Show($"Xem chi tiết đảng viên {id}");
                    // TODO: Mở form chi tiết hoặc hiển thị thông tin
                    break;

                case "Sửa thông tin":
                    MessageBox.Show($"Sửa thông tin đảng viên {id}");
                    // Tạo dữ liệu giả cho DangVien
                    DangVien dangVienGia = new DangVien
                    {
                        DangVienID = 1,
                        DonViID = "Chi bộ 1",
                        HoTen = "Nguyễn Văn A",
                        NgaySinh = new DateTime(1990, 5, 15),
                        GioiTinh = "Nam",
                        SoCCCD = "001234567890",
                        SoDienThoai = "0987654321",
                        SoTheDangVien = "DV2020001",
                        SoLyLichDangVien = "LL2020001",
                        NgayVaoDang = new DateTime(2015, 6, 1),
                        NgayChinhThuc = new DateTime(2016, 6, 1),
                        LoaiDangVien = "Chính thức",
                        DoiTuong = "Quần chúng",
                        CapBac = "Đảng viên",
                        ChucVu = "Trưởng phòng",
                        QueQuan = "Hà Nội, Việt Nam",
                        TrinhDo = "Đại học",
                        AnhDaiDien = "", // Để trống hoặc path ảnh nếu có
                        QuaTrinhCongTac = @"2010 - 2014: Học tại Đại học Bách Khoa Hà Nội
2014 - 2018: Nhân viên phòng Kỹ thuật
2018 - 2022: Phó phòng Kỹ thuật
2022 - nay: Trưởng phòng Kỹ thuật",
                        HoSoGiaDinh = @"Cha: Nguyễn Văn B - Nghề nghiệp: Nông dân
Mẹ: Trần Thị C - Nghề nghiệp: Giáo viên
Vợ: Lê Thị D - Nghề nghiệp: Y tá
Con: Nguyễn Văn E - Sinh năm 2018",
                        TrangThai = true,
                        NgayTao = DateTime.Now.AddMonths(-6),
                        NguoiTao = "admin"
                    };

                    // Hiển thị form
                    FormSua form = new FormSua(dangVienGia);
                    form.ShowDialog(); // Dùng ShowDialog() thay vì Show() để chờ kết quả
                    break;

                case "Xóa đảng viên":
                    if (MessageBox.Show($"Bạn có chắc muốn xóa đảng viên {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // TODO: Thực hiện xóa đảng viên khỏi DB
                        MessageBox.Show($"Đã xóa đảng viên {id}");
                    }
                    break;
            }

            // Sau khi xử lý xong thì reset chọn trong ComboBox (nếu muốn)
            comboBox.SelectedIndex = -1;
        }


        private void TimKiemBtn_Click(object sender, EventArgs e)
        {

        }

        private void ThemBtn_Click(object sender, EventArgs e)
        {
            //FormThaoTacDulieu form = new FormThaoTacDulieu(typeof(DangVien));
            FormThem form = new FormThem(typeof(DangVien));
            form.Show();
        }

        private void SoluongCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void XoaBtn_Click(object sender, EventArgs e)
        {

        }

        private void DangVienGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}