using System;

namespace QuanLyDangVien.Models
{
    public class SystemConfig
    {
        public int SystemConfigID { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiCapNhat { get; set; }
    }
}


