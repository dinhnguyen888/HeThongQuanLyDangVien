using QuanLyDangVien.Attributes;
using System;

namespace QuanLyDangVien.Models
{
    public class DonVi
    {
        [ReadOnlyField(IsReadOnly = true)]
        [DisplayName("Mã đơn vị")]
        public int DonViID { get; set; }

        [Required(IsRequired = true)]
        [DisplayName("Tên đơn vị")]
        public string TenDonVi { get; set; }

        [Required(IsRequired = true)]
        [DisplayName("Mã đơn vị")]
        public string MaDonVi { get; set; }

        [DisplayName("Cấp bậc")]
        [ControlType(ControlInputType.ComboBox)]
        [ComboBoxData("Tá", "Tá", "Úy", "Úy")]
        public string CapBac { get; set; }

        [DisplayName("Địa chỉ")]
        [ControlType(ControlInputType.RichTextBox)]
        public string DiaChi { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Trưởng đơn vị")]
        public string TruongDonVi { get; set; }

        [ReadOnlyField(IsReadOnly = true)]
        [DisplayName("Ngày tạo")]
        [ControlType(ControlInputType.DateTimePicker)]
        public DateTime NgayTao { get; set; }

        [ReadOnlyField(IsReadOnly = true)]
        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }
    }

    // Simplified class for dropdown lists
    public class DonViSimplified
    {
        public int DonViID { get; set; }
        public string TenDonVi { get; set; }
    }
}
