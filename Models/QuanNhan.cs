using System;
using QuanLyDangVien.Attributes;

namespace QuanLyDangVien.Models
{
    /// <summary>
    /// Model cho Quân nhân - Module 1
    /// </summary>
    public class QuanNhan
    {
        [DisplayName("Mã Quân nhân")]
        [ReadOnlyField]
        public int QuanNhanID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Họ tên")]
        [Required]
        public string HoTen { get; set; }

        [DisplayName("Ngày sinh")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("SHSQ")]
        [Required]
        public string SHSQ { get; set; }

        [DisplayName("Số thẻ BHYT")]
        [Required]
        public string SoTheBHYT { get; set; }

        [DisplayName("Số CCCD")]
        [Required]
        public string SoCCCD { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData(
    "Binh nhì", "Binh nhì",
    "Binh nhất", "Binh nhất",
    "Hạ sĩ", "Hạ sĩ",
    "Trung sĩ", "Trung sĩ",
    "Thượng sĩ", "Thượng sĩ",

    "Thiếu úy", "Thiếu úy",
    "Trung úy", "Trung úy",
    "Thượng úy", "Thượng úy",
    "Đại úy", "Đại úy",

    // Cấp tá
    "Thiếu tá", "Thiếu tá",
    "Trung tá", "Trung tá",
    "Thượng tá", "Thượng tá",
    "Đại tá", "Đại tá",

    // Cấp tướng
    "Thiếu tướng", "Thiếu tướng",
    "Trung tướng", "Trung tướng",
    "Thượng tướng", "Thượng tướng",
    "Đại tướng", "Đại tướng"
)]

        [Required]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        [Required]
        public string ChucVu { get; set; }

        [DisplayName("Nhập ngũ")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NhapNgu { get; set; }

        [DisplayName("Ngày vào đảng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayVaoDang { get; set; }

        [DisplayName("Số thẻ đảng")]
        public string SoTheDang { get; set; }

        [DisplayName("Đoàn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đoàn viên", "Đoàn viên", "Không", "Không")]
        public string Doan { get; set; }

        [DisplayName("Dân tộc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Kinh", "Kinh", "Tày", "Tày", "Thái", "Thái", "Mường", "Mường", "Khmer", "Khmer", "Hoa", "Hoa", "Nùng", "Nùng", "H'Mông", "H'Mông", "Dao", "Dao", "Gia Rai", "Gia Rai", "Ê Đê", "Ê Đê", "Ba Na", "Ba Na", "Xơ Đăng", "Xơ Đăng", "Sán Chay", "Sán Chay", "Cơ Ho", "Cơ Ho", "Chăm", "Chăm", "Sán Dìu", "Sán Dìu", "Hrê", "Hrê", "Mnông", "Mnông", "Ra Glai", "Ra Glai", "Xtiêng", "Xtiêng", "Bru-Vân Kiều", "Bru-Vân Kiều", "Thổ", "Thổ", "Giáy", "Giáy", "Cơ Tu", "Cơ Tu", "Gié-Triêng", "Gié-Triêng", "Mạ", "Mạ", "Co", "Co", "Chơ Ro", "Chơ Ro", "Xinh Mun", "Xinh Mun", "Hà Nhì", "Hà Nhì", "Chu Ru", "Chu Ru", "Lào", "Lào", "La Chí", "La Chí", "Kháng", "Kháng", "Phù Lá", "Phù Lá", "La Hủ", "La Hủ", "La Ha", "La Ha", "Pà Thẻn", "Pà Thẻn", "Lự", "Lự", "Ngái", "Ngái", "Chứt", "Chứt", "Lô Lô", "Lô Lô", "Mảng", "Mảng", "Pà Hy", "Pà Hy", "Cờ Lao", "Cờ Lao", "Cống", "Cống", "Bố Y", "Bố Y", "Si La", "Si La", "Pu Péo", "Pu Péo", "Brâu", "Brâu", "Ơ Đu", "Ơ Đu", "Rơ Măm", "Rơ Măm")]
        public string DanToc { get; set; }

        [DisplayName("Tôn giáo")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Phật giáo", "Phật giáo", "Thiên chúa", "Thiên chúa", "Tin lành", "Tin lành", "Hồi giáo", "Hồi giáo", "Cao đài", "Cao đài", "Hòa hảo", "Hòa hảo", "Tứ ân hiếu nghĩa", "Tứ ân hiếu nghĩa", "Baha'i", "Baha'i", "Tịnh độ cư sĩ Phật hội Việt Nam", "Tịnh độ cư sĩ Phật hội Việt Nam", "Phật giáo Hòa hảo", "Phật giáo Hòa hảo", "Minh sư đạo", "Minh sư đạo", "Minh lý đạo", "Minh lý đạo", "Bửu sơn kỳ hương", "Bửu sơn kỳ hương")]
        public string TonGiao { get; set; }

        [DisplayName("Sức khỏe")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tốt", "Tốt", "Khá", "Khá", "Trung bình", "Trung bình", "Yếu", "Yếu")]
        public string SucKhoe { get; set; }

        [DisplayName("Nhóm máu")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("A", "A", "B", "B", "AB", "AB", "O", "O")]
        public string NhomMau { get; set; }

        [DisplayName("Họ tên cha - năm sinh")]
        public string HoTenChaNamSinh { get; set; }

        [DisplayName("Họ tên mẹ - năm sinh")]
        public string HoTenMeNamSinh { get; set; }

        [DisplayName("Họ tên vợ/con - năm sinh")]
        public string HoTenVoConNamSinh { get; set; }

        [DisplayName("Nghề nghiệp cha mẹ")]
        public string NgheNghiepChaMe { get; set; }

        [DisplayName("Mấy anh chị em")]
        public int? MayAnhChiEm { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Nơi ở")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiO { get; set; }

        [DisplayName("Khi cần báo tin")]
        public string KhiCanBaoTin { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("Ảnh đại diện")]
        [ControlType(ControlInputType.PictureBox)]
        public byte[] AnhDaiDien { get; set; }

        [DisplayName("Trạng thái")]
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
}
