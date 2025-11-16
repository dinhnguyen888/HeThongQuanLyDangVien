using System;
using System.IO;
using System.Configuration; // <-- cần dòng này
using Newtonsoft.Json;
using System.Text;

namespace QuanLyDangVien.Helper
{
    public static class SaveInforHelper
    {
        private static readonly string FolderPath;
        private static readonly string FilePath;

        static SaveInforHelper()
        {
            // Đọc đường dẫn từ app.config
            FolderPath = ConfigurationManager.AppSettings["UserInfoFolderPath"];

            // Nếu bị null (không có trong config) → fallback về C:\QuanLyDangVien\Data
            if (string.IsNullOrWhiteSpace(FolderPath))
                FolderPath = @"C:\QuanLyDangVien\Data";

            FilePath = Path.Combine(FolderPath, "user_info.json");
        }

        public static bool SaveInfo(string email, string userSignIn, string role, string status, int? nguoiDungID = null, int? donViID = null)
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                var userInfo = new
                {
                    NguoiDungID = nguoiDungID,
                    Email = email,
                    TenDangNhap = userSignIn,
                    VaiTro = role,
                    TrangThai = status,
                    DonViID = donViID,
                    ThoiGianDangNhap = DateTime.Now
                };

                string jsonData = JsonConvert.SerializeObject(userInfo, Formatting.Indented);
                File.WriteAllText(FilePath, jsonData, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu thông tin người dùng: " + ex.Message);
                return false;
            }
        }

        public static bool GetSavedInfo()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath, Encoding.UTF8);

                    // đọc json và check xem json này có đúng định dạng không

                    var userInfo = JsonConvert.DeserializeObject<dynamic>(json);
                    if (userInfo != null)
                    {
                        // check xem người dùng có ở trạng thái " Hoạt động" không
                        if (userInfo.TrangThai == "Hoạt động")
                            return true;
                        else return false;

                    }
                    else return false;



                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc thông tin người dùng: " + ex.Message);
                return false;
            }
        }

        public static void ClearSavedInfo()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
