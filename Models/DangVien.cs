using System;
using QuanLyDangVien.Attributes;

namespace QuanLyDangVien.Models
{
    public class DangVien
    {
        [DisplayName("Mã Đảng viên")]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        //[ComboBoxData(1, "Chi bộ 1", 2, "Chi bộ 2", 3, "Chi bộ 3")]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Họ tên")]
        [Required]
        public string HoTen { get; set; }

        [DisplayName("Ngày sinh")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Giới tính")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Nam", "Nam", "Nữ", "Nữ")]
        public string GioiTinh { get; set; }

        [DisplayName("Số CCCD")]
        [Required]
        public string SoCCCD { get; set; }

        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [DisplayName("Số thẻ Đảng viên")]
        public string SoTheDangVien { get; set; }

        [DisplayName("Số lý lịch Đảng viên")]
        public string SoLyLichDangVien { get; set; }

        [DisplayName("Ngày vào Đảng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayVaoDang { get; set; }

        [DisplayName("Ngày chính thức")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayChinhThuc { get; set; }

        [DisplayName("Loại Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chính thức", "Chính thức", "Dự bị", "Dự bị")]
        public string LoaiDangVien { get; set; }

        [DisplayName("Đối tượng")]
        public string DoiTuong { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData(1, "Đảng viên", 2, "Bí thư", 3, "Phó bí thư", 4, "Ủy viên")]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Trình độ học vấn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData(1, "Đại học", 2, "Cao học", 3, "Tiến sĩ", 4, "Khác")]
        public string TrinhDo { get; set; }

        [DisplayName("Ảnh đại diện")]
        [ControlType(ControlInputType.PictureBox)]
        public string AnhDaiDien { get; set; }

        [DisplayName("Quá trình công tác")]
        [ControlType(ControlInputType.RichTextBox)]
        public string QuaTrinhCongTac { get; set; }

        [DisplayName("Hồ sơ gia đình")]
        [ControlType(ControlInputType.RichTextBox)]
        public string HoSoGiaDinh { get; set; }

        [DisplayName("Trạng thái hoạt động")]
        [ControlType(ControlInputType.CheckBox)]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }
}
