using System;
using QuanLyDangVien.Attributes;

namespace QuanLyDangVien.Models
{
    /// <summary>
    /// Model cho Đảng viên - Module 2 (Enhanced)
    /// </summary>
    public class DangVien
    {
        [DisplayName("Mã Đảng viên")]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
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
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("SQ", "SQ", "QNCN", "QNCN", "HSQ-BS", "HSQ-BS", "LĐHĐ", "LĐHĐ", "CNVCQP", "CNVCQP")]
        public string DoiTuong { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đảng viên", "Đảng viên", "Bí thư", "Bí thư", "Phó Bí thư", "Phó Bí thư", "Ủy viên", "Ủy viên")]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Trình độ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiểu học", "Tiểu học", "Trung học cơ sở", "Trung học cơ sở", "Trung học phổ thông", "Trung học phổ thông", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDo { get; set; }

        // Enhanced fields from requirements
        [DisplayName("Địa chỉ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiaChi { get; set; }

        [DisplayName("Dân tộc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Kinh", "Kinh", "Tày", "Tày", "Thái", "Thái", "Mường", "Mường", "Khmer", "Khmer", "Hoa", "Hoa", "Nùng", "Nùng", "H'Mông", "H'Mông", "Dao", "Dao", "Gia Rai", "Gia Rai", "Ê Đê", "Ê Đê", "Ba Na", "Ba Na", "Xơ Đăng", "Xơ Đăng", "Sán Chay", "Sán Chay", "Cơ Ho", "Cơ Ho", "Chăm", "Chăm", "Sán Dìu", "Sán Dìu", "Hrê", "Hrê", "Mnông", "Mnông", "Ra Glai", "Ra Glai", "Xtiêng", "Xtiêng", "Bru-Vân Kiều", "Bru-Vân Kiều", "Thổ", "Thổ", "Giáy", "Giáy", "Cơ Tu", "Cơ Tu", "Gié-Triêng", "Gié-Triêng", "Mạ", "Mạ", "Co", "Co", "Chơ Ro", "Chơ Ro", "Xinh Mun", "Xinh Mun", "Hà Nhì", "Hà Nhì", "Chu Ru", "Chu Ru", "Lào", "Lào", "La Chí", "La Chí", "Kháng", "Kháng", "Phù Lá", "Phù Lá", "La Hủ", "La Hủ", "La Ha", "La Ha", "Pà Thẻn", "Pà Thẻn", "Lự", "Lự", "Ngái", "Ngái", "Chứt", "Chứt", "Lô Lô", "Lô Lô", "Mảng", "Mảng", "Pà Hy", "Pà Hy", "Cờ Lao", "Cờ Lao", "Cống", "Cống", "Bố Y", "Bố Y", "Si La", "Si La", "Pu Péo", "Pu Péo", "Brâu", "Brâu", "Ơ Đu", "Ơ Đu", "Rơ Măm", "Rơ Măm")]
        public string DanToc { get; set; }

        [DisplayName("Tôn giáo")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Phật giáo", "Phật giáo", "Thiên chúa", "Thiên chúa", "Tin lành", "Tin lành", "Hồi giáo", "Hồi giáo", "Cao đài", "Cao đài", "Hòa hảo", "Hòa hảo", "Tứ ân hiếu nghĩa", "Tứ ân hiếu nghĩa", "Baha'i", "Baha'i", "Tịnh độ cư sĩ Phật hội Việt Nam", "Tịnh độ cư sĩ Phật hội Việt Nam", "Phật giáo Hòa hảo", "Phật giáo Hòa hảo", "Minh sư đạo", "Minh sư đạo", "Minh lý đạo", "Minh lý đạo", "Bửu sơn kỳ hương", "Bửu sơn kỳ hương")]
        public string TonGiao { get; set; }

        [DisplayName("Nghề nghiệp")]
        public string NgheNghiep { get; set; }

        [DisplayName("Trình độ học vấn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiểu học", "Tiểu học", "Trung học cơ sở", "Trung học cơ sở", "Trung học phổ thông", "Trung học phổ thông", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDoHocVan { get; set; }

        [DisplayName("Trình độ chuyên môn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Lý luận chính trị")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao cấp", "Cao cấp")]
        public string LyLuanChinhTri { get; set; }

        [DisplayName("Ngoại ngữ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Tiếng Anh", "Tiếng Anh", "Tiếng Pháp", "Tiếng Pháp", "Tiếng Nga", "Tiếng Nga", "Tiếng Trung", "Tiếng Trung", "Tiếng Nhật", "Tiếng Nhật", "Tiếng Hàn", "Tiếng Hàn", "Tiếng Đức", "Tiếng Đức", "Tiếng Tây Ban Nha", "Tiếng Tây Ban Nha", "Tiếng Ý", "Tiếng Ý", "Tiếng Ả Rập", "Tiếng Ả Rập")]
        public string NgoaiNgu { get; set; }

        [DisplayName("Tin học")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Cơ bản", "Cơ bản", "Trung bình", "Trung bình", "Khá", "Khá", "Giỏi", "Giỏi")]
        public string TinHoc { get; set; }

        [DisplayName("Ảnh đại diện")]
        [ControlType(ControlInputType.PictureBox)]
        public byte[] AnhDaiDien { get; set; }

        [DisplayName("Quá trình công tác")]
        [ControlType(ControlInputType.RichTextBox)]
        public string QuaTrinhCongTac { get; set; }

        [DisplayName("Hồ sơ gia đình")]
        [ControlType(ControlInputType.RichTextBox)]
        public string HoSoGiaDinh { get; set; }

        [DisplayName("Trạng thái hoạt động")]
        [ControlType(ControlInputType.CheckBox)]
        public bool TrangThai { get; set; } = true;

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho DangVienDetails với thông tin đơn vị
    /// </summary>
    public class DangVienDetails
    {
        [DisplayName("Mã Đảng viên")]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Đơn vị")]
        [Required]
        public string TenDonVi { get; set; }

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
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("SQ", "SQ", "QNCN", "QNCN", "HSQ-BS", "HSQ-BS", "LĐHĐ", "LĐHĐ", "CNVCQP", "CNVCQP")]
        public string DoiTuong { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đảng viên", "Đảng viên", "Bí thư", "Bí thư", "Phó Bí thư", "Phó Bí thư", "Ủy viên", "Ủy viên")]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Trình độ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiểu học", "Tiểu học", "Trung học cơ sở", "Trung học cơ sở", "Trung học phổ thông", "Trung học phổ thông", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDo { get; set; }

        // Enhanced fields
        [DisplayName("Địa chỉ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiaChi { get; set; }

        [DisplayName("Dân tộc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Kinh", "Kinh", "Tày", "Tày", "Thái", "Thái", "Mường", "Mường", "Khmer", "Khmer", "Hoa", "Hoa", "Nùng", "Nùng", "H'Mông", "H'Mông", "Dao", "Dao", "Gia Rai", "Gia Rai", "Ê Đê", "Ê Đê", "Ba Na", "Ba Na", "Xơ Đăng", "Xơ Đăng", "Sán Chay", "Sán Chay", "Cơ Ho", "Cơ Ho", "Chăm", "Chăm", "Sán Dìu", "Sán Dìu", "Hrê", "Hrê", "Mnông", "Mnông", "Ra Glai", "Ra Glai", "Xtiêng", "Xtiêng", "Bru-Vân Kiều", "Bru-Vân Kiều", "Thổ", "Thổ", "Giáy", "Giáy", "Cơ Tu", "Cơ Tu", "Gié-Triêng", "Gié-Triêng", "Mạ", "Mạ", "Co", "Co", "Chơ Ro", "Chơ Ro", "Xinh Mun", "Xinh Mun", "Hà Nhì", "Hà Nhì", "Chu Ru", "Chu Ru", "Lào", "Lào", "La Chí", "La Chí", "Kháng", "Kháng", "Phù Lá", "Phù Lá", "La Hủ", "La Hủ", "La Ha", "La Ha", "Pà Thẻn", "Pà Thẻn", "Lự", "Lự", "Ngái", "Ngái", "Chứt", "Chứt", "Lô Lô", "Lô Lô", "Mảng", "Mảng", "Pà Hy", "Pà Hy", "Cờ Lao", "Cờ Lao", "Cống", "Cống", "Bố Y", "Bố Y", "Si La", "Si La", "Pu Péo", "Pu Péo", "Brâu", "Brâu", "Ơ Đu", "Ơ Đu", "Rơ Măm", "Rơ Măm")]
        public string DanToc { get; set; }

        [DisplayName("Tôn giáo")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Phật giáo", "Phật giáo", "Thiên chúa", "Thiên chúa", "Tin lành", "Tin lành", "Hồi giáo", "Hồi giáo", "Cao đài", "Cao đài", "Hòa hảo", "Hòa hảo", "Tứ ân hiếu nghĩa", "Tứ ân hiếu nghĩa", "Baha'i", "Baha'i", "Tịnh độ cư sĩ Phật hội Việt Nam", "Tịnh độ cư sĩ Phật hội Việt Nam", "Phật giáo Hòa hảo", "Phật giáo Hòa hảo", "Minh sư đạo", "Minh sư đạo", "Minh lý đạo", "Minh lý đạo", "Bửu sơn kỳ hương", "Bửu sơn kỳ hương")]
        public string TonGiao { get; set; }

        [DisplayName("Nghề nghiệp")]
        public string NgheNghiep { get; set; }

        [DisplayName("Trình độ học vấn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiểu học", "Tiểu học", "Trung học cơ sở", "Trung học cơ sở", "Trung học phổ thông", "Trung học phổ thông", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDoHocVan { get; set; }

        [DisplayName("Trình độ chuyên môn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao đẳng", "Cao đẳng", "Đại học", "Đại học", "Thạc sĩ", "Thạc sĩ", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Lý luận chính trị")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao cấp", "Cao cấp")]
        public string LyLuanChinhTri { get; set; }

        [DisplayName("Ngoại ngữ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Tiếng Anh", "Tiếng Anh", "Tiếng Pháp", "Tiếng Pháp", "Tiếng Nga", "Tiếng Nga", "Tiếng Trung", "Tiếng Trung", "Tiếng Nhật", "Tiếng Nhật", "Tiếng Hàn", "Tiếng Hàn", "Tiếng Đức", "Tiếng Đức", "Tiếng Tây Ban Nha", "Tiếng Tây Ban Nha", "Tiếng Ý", "Tiếng Ý", "Tiếng Ả Rập", "Tiếng Ả Rập")]
        public string NgoaiNgu { get; set; }

        [DisplayName("Tin học")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Cơ bản", "Cơ bản", "Trung bình", "Trung bình", "Khá", "Khá", "Giỏi", "Giỏi")]
        public string TinHoc { get; set; }

        [DisplayName("Ảnh đại diện")]
        [ControlType(ControlInputType.PictureBox)]
        public byte[] AnhDaiDien { get; set; }

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

        // Calculated fields
        public int? TuoiDoi { get; set; }
        public int? TuoiDang { get; set; }
    }
}