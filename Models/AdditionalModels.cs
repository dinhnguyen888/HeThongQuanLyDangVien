using System;
using QuanLyDangVien.Attributes;

namespace QuanLyDangVien.Models
{
    /// <summary>
    /// Model cho Khen thưởng cá nhân
    /// </summary>
    public class KhenThuongCaNhan
    {
        [DisplayName("Mã Khen thưởng")]
        [ReadOnlyField]
        public int KhenThuongCaNhanID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DangVienID { get; set; }

        [DisplayName("Hình thức")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Khen thưởng đơn vị
    /// </summary>
    public class KhenThuongDonVi
    {
        [DisplayName("Mã Khen thưởng")]
        [ReadOnlyField]
        public int KhenThuongDonViID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Hình thức")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Kỷ luật cá nhân
    /// </summary>
    public class KyLuatCaNhan
    {
        [DisplayName("Mã Kỷ luật")]
        [ReadOnlyField]
        public int KyLuatCaNhanID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DangVienID { get; set; }

        [DisplayName("Hình thức")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File đính kèm")]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Kỷ luật tổ chức đảng
    /// </summary>
    public class KyLuatToChuc
    {
        [DisplayName("Mã Kỷ luật")]
        [ReadOnlyField]
        public int KyLuatToChucID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Hình thức")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public string HinhThuc { get; set; }

        [DisplayName("Ngày")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime Ngay { get; set; }

        [DisplayName("Số quyết định")]
        [Required]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Cấp quyết định")]
        [Required]
        public string CapQuyetDinh { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File đính kèm")]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Chuyển sinh hoạt đảng
    /// </summary>
    public class ChuyenSinhHoatDang
    {
        [DisplayName("Mã Chuyển sinh hoạt")]
        [ReadOnlyField]
        public int ChuyenSinhHoatID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DangVienID { get; set; }

        [DisplayName("Đơn vị đi")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViDi { get; set; }

        [DisplayName("Đơn vị đến")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViDen { get; set; }

        [DisplayName("Ngày chuyển")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime NgayChuyen { get; set; }

        [DisplayName("Lý do")]
        [ControlType(ControlInputType.RichTextBox)]
        public string LyDo { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("File quyết định")]
        public string FileQuyetDinh { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chờ duyệt", "Chờ duyệt", "Đã duyệt", "Đã duyệt", "Từ chối", "Từ chối")]
        public string TrangThai { get; set; } = "Chờ duyệt";

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Cấp ủy
    /// </summary>
    public class CapUy
    {
        [DisplayName("Mã Cấp ủy")]
        [ReadOnlyField]
        public int CapUyID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DangVienID { get; set; }

        [DisplayName("Chức vụ")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        [ComboBoxData("Bí thư", "Bí thư", "Phó Bí thư", "Phó Bí thư", "Ủy viên", "Ủy viên")]
        public string ChucVu { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime NgayBatDau { get; set; }

        [DisplayName("Ngày kết thúc")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayKetThuc { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Đang tại chức", "Đang tại chức", "Đã nghỉ", "Đã nghỉ")]
        public string TrangThai { get; set; } = "Đang tại chức";

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Sinh hoạt chi bộ
    /// </summary>
    public class SinhHoatChiBo
    {
        [DisplayName("Mã Sinh hoạt")]
        [ReadOnlyField]
        public int SinhHoatID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DonViID { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        public string TieuDe { get; set; }

        [DisplayName("Ngày sinh hoạt")]
        [ControlType(ControlInputType.DateTimePicker)]
        [Required]
        public DateTime NgaySinhHoat { get; set; }

        [DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }

        [DisplayName("Chủ trì")]
        public string ChuTri { get; set; }

        [DisplayName("Thư ký")]
        public string ThuKy { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("Nghị quyết")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NghiQuyet { get; set; }

        [DisplayName("Số lượng tham gia")]
        [ControlType(ControlInputType.NumericUpDown)]
        public int? SoLuongThamGia { get; set; }

        [DisplayName("Trạng thái")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Chưa điểm danh", "Chưa điểm danh", "Đã điểm danh", "Đã điểm danh", "Hoàn thành", "Hoàn thành")]
        public string TrangThai { get; set; } = "Chưa điểm danh";

        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [ReadOnlyField]
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Model cho Điểm danh sinh hoạt chi bộ
    /// </summary>
    public class DiemDanhSinhHoat
    {
        [DisplayName("Mã Điểm danh")]
        [ReadOnlyField]
        public int DiemDanhID { get; set; }

        [DisplayName("Sinh hoạt")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int SinhHoatID { get; set; }

        [DisplayName("Đảng viên")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        public int DangVienID { get; set; }

        [DisplayName("Trạng thái tham gia")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        [ComboBoxData("Có mặt", "Có mặt", "Vắng mặt", "Vắng mặt", "Vắng có lý do", "Vắng có lý do")]
        public string TrangThaiThamGia { get; set; } = "Có mặt";

        [DisplayName("Lý do vắng mặt")]
        [ControlType(ControlInputType.RichTextBox)]
        public string LyDoVangMat { get; set; }

        [DisplayName("Ghi chú")]
        [ControlType(ControlInputType.RichTextBox)]
        public string GhiChu { get; set; }

        [DisplayName("Ngày điểm danh")]
        [ControlType(ControlInputType.DateTimePicker)]
        [ReadOnlyField]
        public DateTime? NgayDiemDanh { get; set; }

        [DisplayName("Người điểm danh")]
        [ReadOnlyField]
        public string NguoiDiemDanh { get; set; }
    }


    /// <summary>
    /// Model cho Tài liệu
    /// </summary>
    public class TaiLieu
    {
        [DisplayName("Mã Tài liệu")]
        [ReadOnlyField]
        public int TaiLieuID { get; set; }

        [DisplayName("Đơn vị")]
        [ControlType(ControlInputType.ComboBox)]
        public int? DonViID { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        public string TieuDe { get; set; }

        [DisplayName("Loại tài liệu")]
        [ControlType(ControlInputType.ComboBox)]
        [Required]
        [ComboBoxData("Văn bản", "Văn bản", "Hướng dẫn", "Hướng dẫn", "Quy định", "Quy định", "Thông báo", "Thông báo", "Khác", "Khác")]
        public string LoaiTaiLieu { get; set; }

        [DisplayName("Nội dung")]
        [ControlType(ControlInputType.RichTextBox)]
        public string NoiDung { get; set; }

        [DisplayName("File đính kèm")]
        public string FileDinhKem { get; set; }

        [DisplayName("Ngày phát hành")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime? NgayPhatHanh { get; set; }

        [DisplayName("Cơ quan phát hành")]
        public string CoQuanPhatHanh { get; set; }

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
