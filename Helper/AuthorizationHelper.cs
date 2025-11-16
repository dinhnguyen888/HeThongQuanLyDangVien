using System;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace QuanLyDangVien.Helper
{
    /// <summary>
    /// Helper class để quản lý phân quyền người dùng
    /// </summary>
    public static class AuthorizationHelper
    {
        private static readonly string FilePath;

        static AuthorizationHelper()
        {
            string folderPath = System.Configuration.ConfigurationManager.AppSettings["UserInfoFolderPath"];
            if (string.IsNullOrWhiteSpace(folderPath))
                folderPath = @"C:\QuanLyDangVien\Data";
            FilePath = Path.Combine(folderPath, "user_info.json");
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại từ user_info.json
        /// </summary>
        public static UserInfo GetCurrentUser()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return null;

                string json = File.ReadAllText(FilePath, Encoding.UTF8);
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
                return userInfo;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra người dùng có vai trò Admin không
        /// </summary>
        public static bool IsAdmin()
        {
            var user = GetCurrentUser();
            return user != null && user.VaiTro == "Admin";
        }

        /// <summary>
        /// Kiểm tra người dùng có vai trò Bí thư không
        /// </summary>
        public static bool IsBiThu()
        {
            var user = GetCurrentUser();
            return user != null && user.VaiTro == "BiThu";
        }

        /// <summary>
        /// Kiểm tra người dùng có vai trò Văn phòng không
        /// </summary>
        public static bool IsVanPhong()
        {
            var user = GetCurrentUser();
            return user != null && user.VaiTro == "VanPhong";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền thực hiện action trên module không
        /// </summary>
        /// <param name="module">Tên module (VD: "DangVien", "QuanNhan", "TaiLieu")</param>
        /// <param name="action">Action (VD: "Create", "Update", "Delete", "View")</param>
        /// <param name="donViID">DonViID của record (null nếu không cần check)</param>
        /// <returns>True nếu có quyền</returns>
        public static bool HasPermission(string module, string action, int? donViID = null)
        {
            var user = GetCurrentUser();
            if (user == null)
                return false;

            // Admin có toàn quyền
            if (IsAdmin())
                return true;

            // Văn phòng chỉ được xem và xuất báo cáo
            if (IsVanPhong())
            {
                return action == "View" || action == "Export";
            }

            // Bí thư có quyền trong đơn vị của mình
            if (IsBiThu())
            {
                // Nếu có donViID, kiểm tra xem có phải đơn vị của Bí thư không
                if (donViID.HasValue && user.DonViID.HasValue)
                {
                    return user.DonViID.Value == donViID.Value;
                }
                // Nếu không có donViID, cho phép (sẽ check ở service layer)
                return true;
            }

            return false;
        }

        /// <summary>
        /// Lấy DonViID của người dùng hiện tại
        /// </summary>
        public static int? GetCurrentUserDonViID()
        {
            var user = GetCurrentUser();
            return user?.DonViID;
        }

        /// <summary>
        /// Lấy NguoiDungID của người dùng hiện tại
        /// </summary>
        public static int? GetCurrentUserID()
        {
            var user = GetCurrentUser();
            return user?.NguoiDungID;
        }

        /// <summary>
        /// Lấy tên đăng nhập của người dùng hiện tại
        /// </summary>
        public static string GetCurrentUserName()
        {
            var user = GetCurrentUser();
            return user?.TenDangNhap ?? "Unknown";
        }
    }

    /// <summary>
    /// Class để lưu thông tin người dùng từ user_info.json
    /// </summary>
    public class UserInfo
    {
        public int? NguoiDungID { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public int? DonViID { get; set; }
        public DateTime? ThoiGianDangNhap { get; set; }
    }
}


