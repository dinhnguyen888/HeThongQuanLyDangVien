using System;

namespace QuanLyDangVien.Models
{
    public class NguoiDung
    {
        public int NguoiDungID { get; set; }
        public int? DonViID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
    }
}
