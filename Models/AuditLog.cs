using System;

namespace QuanLyDangVien.Models
{
    public class AuditLog
    {
        public int AuditLogID { get; set; }
        public int? NguoiDungID { get; set; }
        public string TenDangNhap { get; set; }
        public string Action { get; set; } // Insert, Update, Delete, View
        public string TableName { get; set; }
        public int? RecordID { get; set; }
        public string OldValues { get; set; } // JSON
        public string NewValues { get; set; } // JSON
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime NgayThucHien { get; set; }
    }
}


