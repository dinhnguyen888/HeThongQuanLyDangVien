using System;
using System.Collections.Generic;
using QuanLyDangVien.Attributes;

namespace QuanLyDangVien.DTOs
{
    /// <summary>
    /// DTO cho Quân nhân - Module 1
    /// </summary>
    public class QuanNhanDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int QuanNhanID { get; set; }

        [DisplayName("Đơn vị")]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Họ và tên")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string HoTen { get; set; }

        [DisplayName("Ngày sinh")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("SHSQ")]
        [ControlType(ControlInputType.TextBox)]
        public string SHSQ { get; set; }

        [DisplayName("Số thẻ BHYT")]
        [ControlType(ControlInputType.TextBox)]
        public string SoTheBHYT { get; set; }

        [DisplayName("Số CCCD")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoCCCD { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Thượng úy", "Thượng úy", "Trung úy", "Trung úy", "Đại úy", "Đại úy", "Thiếu tá", "Thiếu tá", "Trung tá", "Trung tá", "Thượng tá", "Thượng tá")]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        [ControlType(ControlInputType.TextBox)]
        public string ChucVu { get; set; }

        [DisplayName("Ngày nhập ngũ")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NhapNgu { get; set; }

        [DisplayName("Ngày vào Đảng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayVaoDang { get; set; }

        [DisplayName("Số thẻ Đảng")]
        [ControlType(ControlInputType.TextBox)]
        public string SoTheDang { get; set; }

        [DisplayName("Đoàn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đoàn viên", "Đoàn viên", "Không", "Không")]
        public string Doan { get; set; }

        [DisplayName("Dân tộc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Kinh", "Kinh", "Tày", "Tày", "Thái", "Thái", "Mường", "Mường", "Khmer", "Khmer", "Hoa", "Hoa", "Nùng", "Nùng", "H'Mông", "H'Mông", "Dao", "Dao", "Gia Rai", "Gia Rai")]
        public string DanToc { get; set; }

        [DisplayName("Tôn giáo")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Phật giáo", "Phật giáo", "Thiên chúa", "Thiên chúa", "Tin lành", "Tin lành", "Cao đài", "Cao đài", "Hòa hảo", "Hòa hảo")]
        public string TonGiao { get; set; }

        [DisplayName("Sức khỏe")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tốt", "Tốt", "Khá", "Khá", "Trung bình", "Trung bình", "Yếu", "Yếu")]
        public string SucKhoe { get; set; }

        [DisplayName("Nhóm máu")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("A", "A", "B", "B", "AB", "AB", "O", "O")]
        public string NhomMau { get; set; }

        [DisplayName("Họ tên cha (năm sinh)")]
        [ControlType(ControlInputType.TextBox)]
        public string HoTenChaNamSinh { get; set; }

        [DisplayName("Họ tên mẹ (năm sinh)")]
        [ControlType(ControlInputType.TextBox)]
        public string HoTenMeNamSinh { get; set; }

        [DisplayName("Họ tên vợ/chồng (năm sinh)")]
        [ControlType(ControlInputType.TextBox)]
        public string HoTenVoConNamSinh { get; set; }

        [DisplayName("Nghề nghiệp cha mẹ")]
        [ControlType(ControlInputType.TextBox)]
        public string NgheNghiepChaMe { get; set; }

        [DisplayName("Mấy anh chị em")]
        [ControlType(ControlInputType.NumericUpDown)]
        public int? MayAnhChiEm { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Nơi ở")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiO { get; set; }

        [DisplayName("Khi cần báo tin")]
        [ControlType(ControlInputType.TextBox)]
        public string KhiCanBaoTin { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("Ảnh đại diện")]
        [ControlType(ControlInputType.PictureBox)]
        public byte[] AnhDaiDien { get; set; }

        [DisplayName("Trạng thái")]
        [ReadOnlyField]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
        
        // Calculated fields
        [DisplayName("Tuổi đời")]
        [ReadOnlyField]
        public int? TuoiDoi { get; set; }

        [DisplayName("Tuổi Đảng")]
        [ReadOnlyField]
        public int? TuoiDang { get; set; }
    }

    /// <summary>
    /// DTO cho Đảng viên - Module 2 (Enhanced)
    /// </summary>
    public class DangVienDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Đơn vị cấp 3")]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Đơn vị cấp 1")]
        [ControlType(ControlInputType.TextBox)]
        [ReadOnlyField]
        public string DonViCap1 { get; set; }

        [DisplayName("Đơn vị cấp 2")]
        [ControlType(ControlInputType.TextBox)]
        [ReadOnlyField]
        public string DonViCap2 { get; set; }

        [DisplayName("Tên đơn vị")]
        [ControlType(ControlInputType.TextBox)]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Họ tên khai sinh")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string HoTenKhaiSinh { get; set; }

        [DisplayName("Họ và tên")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string HoTen { get; set; }

        [DisplayName("Họ tên khác")]
        [ControlType(ControlInputType.TextBox)]
        public string HoTenKhac { get; set; }

        [DisplayName("Ngày sinh")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Giới tính")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Nam", "Nam", "Nữ", "Nữ")]
        public string GioiTinh { get; set; }

        [DisplayName("Số CCCD")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoCCCD { get; set; }

        [DisplayName("Số điện thoại")]
        [ControlType(ControlInputType.TextBox)]
        public string SoDienThoai { get; set; }

        [DisplayName("Số thẻ Đảng viên")]
        [ControlType(ControlInputType.TextBox)]
        public string SoTheDangVien { get; set; }

        [DisplayName("Số lý lịch Đảng viên")]
        [ControlType(ControlInputType.TextBox)]
        public string SoLyLichDangVien { get; set; }

        [DisplayName("Ngày tham gia cách mạng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayThamGiaCachMang { get; set; }

        [DisplayName("Ngày tuyển dụng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayTuyenDung { get; set; }

        [DisplayName("Ngày nhập ngũ")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayNhapNgu { get; set; }

        [DisplayName("Ngày xuất ngũ")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayXuatNgu { get; set; }

        [DisplayName("Ngày tái ngũ")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayTaiNgu { get; set; }

        [DisplayName("Ngày vào Đảng")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayVaoDang { get; set; }

        [DisplayName("Ngày chính thức")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayChinhThuc { get; set; }

        [DisplayName("Loại Đảng viên")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chính thức", "Chính thức", "Dự bị", "Dự bị")]
        public string LoaiDangVien { get; set; }

        [DisplayName("Đối tượng")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("QNCN", "QNCN", "HSQ-BS", "HSQ-BS", "LĐHĐ", "LĐHĐ", "CNVCQP", "CNVCQP")]
        public string DoiTuong { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Thượng úy", "Thượng úy", "Trung úy", "Trung úy", "Đại úy", "Đại úy", "Thiếu tá", "Thiếu tá", "Trung tá", "Trung tá", "Thượng tá", "Thượng tá")]
        public string CapBac { get; set; }

        [DisplayName("Chức vụ")]
        [ControlType(ControlInputType.TextBox)]
        public string ChucVu { get; set; }

        [DisplayName("Quê quán")]
        [ControlType(ControlInputType.TextBox)]
        public string QueQuan { get; set; }

        [DisplayName("Trình độ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiểu học", "Tiểu học", "Trung học cơ sở", "Trung học cơ sở", "Trung học phổ thông", "Trung học phổ thông", "Đại học", "Đại học", "Cao học", "Cao học", "Tiến sĩ", "Tiến sĩ")]
        public string TrinhDo { get; set; }
        
        // Enhanced fields
        [DisplayName("Địa chỉ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiaChi { get; set; }

        [DisplayName("Dân tộc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Kinh", "Kinh", "Tày", "Tày", "Thái", "Thái", "Mường", "Mường", "Khmer", "Khmer", "Hoa", "Hoa", "Nùng", "Nùng", "H'Mông", "H'Mông", "Dao", "Dao", "Gia Rai", "Gia Rai")]
        public string DanToc { get; set; }

        [DisplayName("Tôn giáo")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Không", "Không", "Phật giáo", "Phật giáo", "Thiên chúa", "Thiên chúa", "Tin lành", "Tin lành", "Cao đài", "Cao đài", "Hòa hảo", "Hòa hảo")]
        public string TonGiao { get; set; }

        [DisplayName("Nghề nghiệp")]
        [ControlType(ControlInputType.TextBox)]
        public string NgheNghiep { get; set; }

        [DisplayName("Trình độ chuyên môn")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Quân sự", "Quân sự", "Y tế", "Y tế", "Kỹ thuật", "Kỹ thuật", "Quản lý", "Quản lý", "Tài chính", "Tài chính", "Khác", "Khác")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Lý luận chính trị")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Cao", "Cao", "Trung bình", "Trung bình", "Thấp", "Thấp")]
        public string LyLuanChinhTri { get; set; }

        [DisplayName("Chức danh khoa học")]
        public string ChucDanhKhoaHoc { get; set; }

        [DisplayName("Học vị cao nhất")]
        public string HocViCaoNhat { get; set; }

        [DisplayName("Chuyên ngành")]
        public string ChuyenNganh { get; set; }

        [DisplayName("Thời gian học vị")]
        public string ThoiGianHocVi { get; set; }

        [DisplayName("Trình độ chỉ huy, quản lý")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao cấp", "Cao cấp")]
        public string TrinhDoChiHuyQuanLy { get; set; }

        [DisplayName("Ngoại ngữ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tiếng Anh", "Tiếng Anh", "Tiếng Pháp", "Tiếng Pháp", "Tiếng Trung", "Tiếng Trung", "Tiếng Nhật", "Tiếng Nhật", "Khác", "Khác")]
        public string NgoaiNgu { get; set; }

        [DisplayName("Trình độ ngoại ngữ")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Sơ cấp", "Sơ cấp", "Trung cấp", "Trung cấp", "Cao cấp", "Cao cấp")]
        public string TrinhDoNgoaiNgu { get; set; }

        [DisplayName("Thời gian ngoại ngữ")]
        public string ThoiGianNgoaiNgu { get; set; }

        [DisplayName("Tiếng dân tộc – mức độ nghe/nói/viết")]
        [ControlType(ControlInputType.RichTextBox)]
        public string TiengDanToc { get; set; }

        [DisplayName("Quá trình học tập tại các trường")]
        [ControlType(ControlInputType.RichTextBox)]
        public string QuaTrinhHocTap { get; set; }

        [DisplayName("Chiến đấu, phục vụ chiến đấu")]
        [ControlType(ControlInputType.RichTextBox)]
        public string ChienDauPhucVuChienDau { get; set; }

        [DisplayName("Đi nước ngoài")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiNuocNgoai { get; set; }

        [DisplayName("Sức khỏe loại")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tốt", "Tốt", "Khá", "Khá", "Trung bình", "Trung bình", "Yếu", "Yếu")]
        public string SucKhoeLoai { get; set; }

        [DisplayName("Nhóm máu")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("A", "A", "B", "B", "AB", "AB", "O", "O")]
        public string NhomMau { get; set; }

        [DisplayName("Bệnh chính")]
        public string BenhChinh { get; set; }

        [DisplayName("Thương tật")]
        [ControlType(ControlInputType.RichTextBox)]
        public string ThuongTat { get; set; }

        [DisplayName("Danh hiệu được phong")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DanhHieuDuocPhong { get; set; }

        [DisplayName("Nghề nghiệp trước khi nhập ngũ")]
        public string NgheNghiepTruocNhapNgu { get; set; }

        [DisplayName("Quan hệ CT-XH trước khi nhập ngũ")]
        public string QuanHeCTXHTruocNhapNgu { get; set; }

        [DisplayName("Tình hình nhà ở")]
        [ControlType(ControlInputType.RichTextBox)]
        public string TinhHinhNhaO { get; set; }

        [DisplayName("Tin học")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Giỏi", "Giỏi", "Khá", "Khá", "Trung bình", "Trung bình", "Yếu", "Yếu")]
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

        // II. TÌNH HÌNH KINH TẾ – CHÍNH TRỊ GIA ĐÌNH
        [DisplayName("Họ tên cha")]
        public string HoTenCha { get; set; }

        [DisplayName("Năm sinh cha")]
        public int? NamSinhCha { get; set; }

        [DisplayName("Nghề nghiệp cha")]
        public string NgheNghiepCha { get; set; }

        [DisplayName("Họ tên mẹ")]
        public string HoTenMe { get; set; }

        [DisplayName("Năm sinh mẹ")]
        public int? NamSinhMe { get; set; }

        [DisplayName("Nghề nghiệp mẹ")]
        public string NgheNghiepMe { get; set; }

        [DisplayName("Thành phần gia đình")]
        public string ThanhPhanGiaDinh { get; set; }

        [DisplayName("Quê quán của cha mẹ")]
        public string QueQuanChaMe { get; set; }

        [DisplayName("Chỗ ở hiện nay của cha mẹ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string ChoOHienNayChaMe { get; set; }

        [DisplayName("Số con trong gia đình (cha mẹ sinh được)")]
        public int? SoConTrongGiaDinh { get; set; }

        [DisplayName("Giới tính/Thứ tự của bản thân trong gia đình")]
        public string GioiTinhThuTuBanThan { get; set; }

        [DisplayName("Tình hình kinh tế của gia đình")]
        [ControlType(ControlInputType.RichTextBox)]
        public string TinhHinhKinhTeGiaDinh { get; set; }

        [DisplayName("Tình hình chính trị của gia đình")]
        [ControlType(ControlInputType.RichTextBox)]
        public string TinhHinhChinhTriGiaDinh { get; set; }

        // III. TÌNH HÌNH KT – CT CỦA GIA ĐÌNH VỢ (CHỒNG)
        [DisplayName("Họ tên cha vợ/chồng")]
        public string HoTenChaVoChong { get; set; }

        [DisplayName("Năm sinh cha vợ/chồng")]
        public int? NamSinhChaVoChong { get; set; }

        [DisplayName("Nghề nghiệp cha vợ/chồng")]
        public string NgheNghiepChaVoChong { get; set; }

        [DisplayName("Họ tên mẹ vợ/chồng")]
        public string HoTenMeVoChong { get; set; }

        [DisplayName("Năm sinh mẹ vợ/chồng")]
        public int? NamSinhMeVoChong { get; set; }

        [DisplayName("Nghề nghiệp mẹ vợ/chồng")]
        public string NgheNghiepMeVoChong { get; set; }

        [DisplayName("Thành phần gia đình vợ/chồng")]
        public string ThanhPhanGiaDinhVoChong { get; set; }

        [DisplayName("Quê quán gia đình vợ/chồng")]
        public string QueQuanGiaDinhVoChong { get; set; }

        [DisplayName("Chỗ ở hiện nay của gia đình vợ/chồng")]
        [ControlType(ControlInputType.RichTextBox)]
        public string ChoOHienNayGiaDinhVoChong { get; set; }

        [DisplayName("Số con trong gia đình vợ/chồng")]
        public int? SoConTrongGiaDinhVoChong { get; set; }

        [DisplayName("Thứ tự của vợ/chồng trong gia đình")]
        public string ThuTuVoChongTrongGiaDinh { get; set; }

        [DisplayName("Tình hình KT-CT của gia đình vợ/chồng")]
        [ControlType(ControlInputType.RichTextBox)]
        public string TinhHinhKTCTGiaDinhVoChong { get; set; }

        [DisplayName("Nghề nghiệp của vợ/chồng")]
        public string NgheNghiepVoChong { get; set; }

        [DisplayName("Đảng viên hay không")]
        [ControlType(ControlInputType.CheckBox)]
        public bool? DangVienHayKhong { get; set; }

        [DisplayName("Nơi ở hiện nay của vợ/chồng")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiOHienNayVoChong { get; set; }

        [DisplayName("Họ tên – năm sinh – nghề nghiệp các con")]
        [ControlType(ControlInputType.RichTextBox)]
        public string ThongTinCacCon { get; set; }

        [DisplayName("Trạng thái")]
        [ReadOnlyField]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
        
        // Calculated fields
        [DisplayName("Tuổi đời")]
        [ReadOnlyField]
        public int? TuoiDoi { get; set; }

        [DisplayName("Tuổi Đảng")]
        [ReadOnlyField]
        public int? TuoiDang { get; set; }
    }

    /// <summary>
    /// DTO cho Đơn vị - Module 3
    /// </summary>
    public class DonViDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string TenDonVi { get; set; }

        [DisplayName("Mã đơn vị")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string MaDonVi { get; set; }

        [DisplayName("Tên đầy đủ")]
        [ControlType(ControlInputType.TextBox)]
        public string TenDayDu { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chi bộ", "Chi bộ", "Đảng bộ", "Đảng bộ", "Tỉnh ủy", "Tỉnh ủy", "Trung ương", "Trung ương")]
        public string CapBac { get; set; }

        [DisplayName("Địa chỉ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiaChi { get; set; }

        [DisplayName("Số điện thoại")]
        [ControlType(ControlInputType.TextBox)]
        public string SoDienThoai { get; set; }

        [DisplayName("Email")]
        [ControlType(ControlInputType.TextBox)]
        public string Email { get; set; }

        [DisplayName("Trưởng đơn vị")]
        [ControlType(ControlInputType.TextBox)]
        public string TruongDonVi { get; set; }

        [DisplayName("Đơn vị cấp trên")]
        [ControlType(ControlInputType.ComboBox)]
        public int? CapTrenID { get; set; }

        [DisplayName("Tên đơn vị cấp trên")]
        [ReadOnlyField]
        public string TenCapTren { get; set; }

        [DisplayName("Danh sách đơn vị cấp dưới")]
        [ReadOnlyField]
        public string DanhSachCapDuoi { get; set; }

        [DisplayName("Mô tả")]
        [ControlType(ControlInputType.RichTextBox)]
        public string MoTa { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.CheckBox)]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
        
        // Aggregate data
        [DisplayName("Tổng số Đảng viên")]
        [ReadOnlyField]
        public int TongSoDangVien { get; set; }

        [DisplayName("Đảng viên chính thức")]
        [ReadOnlyField]
        public int DangVienChinhThuc { get; set; }

        [DisplayName("Đảng viên dự bị")]
        [ReadOnlyField]
        public int DangVienDuBi { get; set; }

        [DisplayName("Thông tin cấp ủy")]
        [ReadOnlyField]
        public string CapUyInfo { get; set; }
    }

    /// <summary>
    /// DTO cho Khen thưởng cá nhân
    /// </summary>
    public class KhenThuongCaNhanDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int KhenThuongCaNhanID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Tên Đảng viên")]
        [ReadOnlyField]
        public string TenDangVien { get; set; }
        
        // Alias for compatibility
        public string HoTenDangVien { get => TenDangVien; set => TenDangVien = value; }

        [DisplayName("Hình thức")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Giấy khen", "Giấy khen", "Bằng khen", "Bằng khen", "Huân chương", "Huân chương", "Huy chương", "Huy chương")]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chi bộ", "Chi bộ", "Đảng bộ", "Đảng bộ", "Tỉnh ủy", "Tỉnh ủy", "Trung ương", "Trung ương")]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Khen thưởng đơn vị
    /// </summary>
    public class KhenThuongDonViDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int KhenThuongDonViID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Hình thức")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Giấy khen", "Giấy khen", "Bằng khen", "Bằng khen", "Huân chương", "Huân chương", "Huy chương", "Huy chương")]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chi bộ", "Chi bộ", "Đảng bộ", "Đảng bộ", "Tỉnh ủy", "Tỉnh ủy", "Trung ương", "Trung ương")]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Kỷ luật cá nhân
    /// </summary>
    public class KyLuatCaNhanDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int KyLuatCaNhanID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Tên Đảng viên")]
        [ReadOnlyField]
        public string TenDangVien { get; set; }
        
        // Alias for compatibility
        public string HoTenDangVien { get => TenDangVien; set => TenDangVien = value; }

        [DisplayName("Hình thức")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Khiển trách", "Khiển trách", "Cảnh cáo", "Cảnh cáo", "Cách chức", "Cách chức", "Khai trừ", "Khai trừ")]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chi bộ", "Chi bộ", "Đảng bộ", "Đảng bộ", "Tỉnh ủy", "Tỉnh ủy", "Trung ương", "Trung ương")]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Kỷ luật tổ chức đảng
    /// </summary>
    public class KyLuatToChucDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int KyLuatToChucID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Hình thức")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Khiển trách", "Khiển trách", "Cảnh cáo", "Cảnh cáo", "Giải tán", "Giải tán")]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chi bộ", "Chi bộ", "Đảng bộ", "Đảng bộ", "Tỉnh ủy", "Tỉnh ủy", "Trung ương", "Trung ương")]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Chuyển sinh hoạt đảng
    /// </summary>
    public class ChuyenSinhHoatDangDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int ChuyenSinhHoatID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Tên Đảng viên")]
        [ReadOnlyField]
        public string TenDangVien { get; set; }
        
        // Alias for compatibility
        public string HoTenDangVien { get => TenDangVien; set => TenDangVien = value; }

        [DisplayName("Đơn vị đi")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViDi { get; set; }

        [DisplayName("Tên đơn vị đi")]
        [ReadOnlyField]
        public string TenDonViDi { get; set; }

        [DisplayName("Đơn vị đến")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViDen { get; set; }

        [DisplayName("Tên đơn vị đến")]
        [ReadOnlyField]
        public string TenDonViDen { get; set; }

        [DisplayName("Ngày chuyển")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime NgayChuyen { get; set; }

        [DisplayName("Lý do")]
        [ControlType(ControlInputType.RichTextBox)]
        public string LyDo { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File quyết định")]
        [ControlType(ControlInputType.TextBox)]
        public string FileQuyetDinh { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chờ duyệt", "Chờ duyệt", "Đã duyệt", "Đã duyệt", "Từ chối", "Từ chối")]
        public string TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Cấp ủy
    /// </summary>
    public class CapUyDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int CapUyID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DangVienID { get; set; }

        [DisplayName("Tên Đảng viên")]
        [ReadOnlyField]
        public string TenDangVien { get; set; }
        
        // Alias for compatibility
        public string HoTenDangVien { get => TenDangVien; set => TenDangVien = value; }

        [DisplayName("Chức vụ")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Bí thư", "Bí thư", "Phó Bí thư", "Phó Bí thư", "Ủy viên", "Ủy viên")]
        public string ChucVu { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime NgayBatDau { get; set; }

        [DisplayName("Ngày kết thúc")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayKetThuc { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đang tại chức", "Đang tại chức", "Đã nghỉ", "Đã nghỉ")]
        public string TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Sinh hoạt chi bộ
    /// </summary>
    public class SinhHoatChiBoDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int SinhHoatID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string TieuDe { get; set; }

        [DisplayName("Ngày sinh hoạt")]
        [Required]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime NgaySinhHoat { get; set; }

        [DisplayName("Địa điểm")]
        [ControlType(ControlInputType.TextBox)]
        public string DiaDiem { get; set; }

        [DisplayName("Chủ trì")]
        [ControlType(ControlInputType.TextBox)]
        public string ChuTri { get; set; }

        [DisplayName("Thư ký")]
        [ControlType(ControlInputType.TextBox)]
        public string ThuKy { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File nghị quyết")]
        [ControlType(ControlInputType.FileDialog)]
        public string FileNghiQuyet { get; set; }

        [DisplayName("Số lượng tham gia")]
        [ControlType(ControlInputType.NumericUpDown)]
        public int? SoLuongThamGia { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chưa điểm danh", "Chưa điểm danh", "Đã điểm danh", "Đã điểm danh")]
        public string TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Báo cáo thống kê
    /// </summary>
    public class BaoCaoThongKeDTO
    {
        [DisplayName("Loại thống kê")]
        [ReadOnlyField]
        public string LoaiThongKe { get; set; }

        [DisplayName("Tên")]
        [ReadOnlyField]
        public string Ten { get; set; }

        [DisplayName("Số lượng")]
        [ReadOnlyField]
        public int SoLuong { get; set; }

        [DisplayName("Tỷ lệ")]
        [ReadOnlyField]
        public decimal TyLe { get; set; }
    }

    /// <summary>
    /// DTO cho Báo cáo theo đơn vị
    /// </summary>
    public class BaoCaoTheoDonViDTO
    {
        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Tổng số Đảng viên")]
        [ReadOnlyField]
        public int TongSoDangVien { get; set; }

        [DisplayName("Đảng viên chính thức")]
        [ReadOnlyField]
        public int DangVienChinhThuc { get; set; }

        [DisplayName("Đảng viên dự bị")]
        [ReadOnlyField]
        public int DangVienDuBi { get; set; }

        [DisplayName("Nam")]
        [ReadOnlyField]
        public int Nam { get; set; }

        [DisplayName("Nữ")]
        [ReadOnlyField]
        public int Nu { get; set; }
    }

    /// <summary>
    /// DTO cho Tài liệu
    /// </summary>
    public class TaiLieuDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int TaiLieuID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        public int? DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string TieuDe { get; set; }

        [DisplayName("Loại tài liệu")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Văn bản", "Văn bản", "Báo cáo", "Báo cáo", "Nghị quyết", "Nghị quyết", "Quyết định", "Quyết định")]
        public string LoaiTaiLieu { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày phát hành")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayPhatHanh { get; set; }

        [DisplayName("Cơ quan phát hành")]
        [ControlType(ControlInputType.TextBox)]
        public string CoQuanPhatHanh { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.CheckBox)]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Văn bản chi bộ
    /// </summary>
    public class VanBanChiBoDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int VanBanChiBoID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [ReadOnlyField]
        public int DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Tên văn bản")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string TenVanBan { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Ngày gửi")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayGui { get; set; }

        [DisplayName("Ngày nhận")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayNhan { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chưa xử lý", "Chưa xử lý", "Đang xử lý", "Đang xử lý", "Đã xử lý", "Đã xử lý")]
        public string TrangThai { get; set; }

        [DisplayName("File đính kèm")]
        [ControlType(ControlInputType.TextBox)]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Người dùng
    /// </summary>
    public class NguoiDungDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int NguoiDungID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        public int? DonViID { get; set; }

        [DisplayName("Tên đơn vị")]
        [ReadOnlyField]
        public string TenDonVi { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string TenDangNhap { get; set; }

        [DisplayName("Họ và tên")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string HoTen { get; set; }

        [DisplayName("Email")]
        [ControlType(ControlInputType.TextBox)]
        public string Email { get; set; }

        [DisplayName("Vai trò")]
        [Required]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Admin", "Admin", "BiThu", "Bí thư", "VanPhong", "Văn phòng")]
        public string VaiTro { get; set; }

        [DisplayName("Roles")]
        [ControlType(ControlInputType.TextBox)]
        public string Roles { get; set; }

        [DisplayName("Permissions")]
        [ControlType(ControlInputType.TextBox)]
        public string Permissions { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.CheckBox)]
        public bool TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }

        [DisplayName("Lần đăng nhập cuối")]
        [ReadOnlyField]
        public DateTime? LanDangNhapCuoi { get; set; }
    }

    /// <summary>
    /// DTO cho Audit Log
    /// </summary>
    public class AuditLogDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int AuditLogID { get; set; }

        [DisplayName("Người dùng")]
        [ReadOnlyField]
        public int? NguoiDungID { get; set; }

        [DisplayName("Tên đăng nhập")]
        [ReadOnlyField]
        public string TenDangNhap { get; set; }

        [DisplayName("Hành động")]
        [ReadOnlyField]
        public string Action { get; set; }

        [DisplayName("Bảng")]
        [ReadOnlyField]
        public string TableName { get; set; }

        [DisplayName("ID bản ghi")]
        [ReadOnlyField]
        public int? RecordID { get; set; }

        [DisplayName("Giá trị cũ")]
        [ReadOnlyField]
        public string OldValues { get; set; }

        [DisplayName("Giá trị mới")]
        [ReadOnlyField]
        public string NewValues { get; set; }

        [DisplayName("IP Address")]
        [ReadOnlyField]
        public string IPAddress { get; set; }

        [DisplayName("User Agent")]
        [ReadOnlyField]
        public string UserAgent { get; set; }

        [DisplayName("Ngày thực hiện")]
        [ReadOnlyField]
        public DateTime NgayThucHien { get; set; }
    }

    /// <summary>
    /// DTO cho System Config
    /// </summary>
    public class SystemConfigDTO
    {
        [DisplayName("ID")]
        [ReadOnlyField]
        public int SystemConfigID { get; set; }

        [DisplayName("Config Key")]
        [Required]
        [ControlType(ControlInputType.TextBox)]
        public string ConfigKey { get; set; }

        [DisplayName("Config Value")]
        [ControlType(ControlInputType.TextBox)]
        public string ConfigValue { get; set; }

        [DisplayName("Mô tả")]
        [ControlType(ControlInputType.TextBox)]
        public string Description { get; set; }

        [DisplayName("Ngày cập nhật")]
        [ReadOnlyField]
        public DateTime? NgayCapNhat { get; set; }

        [DisplayName("Người cập nhật")]
        [ReadOnlyField]
        public string NguoiCapNhat { get; set; }
    }

    /// <summary>
    /// DTO cho Response chung
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    /// <summary>
    /// DTO cho Filter parameters
    /// </summary>
    public class FilterDTO
    {
        public int? DonViID { get; set; }
        public string HoTen { get; set; }
        public string SoCCCD { get; set; }
        public string LoaiDangVien { get; set; }
        public string DoiTuong { get; set; }
        public string CapBac { get; set; }
        public string ChucVu { get; set; }
        public string QueQuan { get; set; }
        public string TrinhDo { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? Nam { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}