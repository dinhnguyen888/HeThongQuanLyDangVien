using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static QuanLyDangVien.Program;

namespace QuanLyDangVien.Services
{
    public class NguoiDungService
    {
        /// <summary>
        /// Hash mật khẩu bằng SHA-256
        /// </summary>
        /// <param name="password">Mật khẩu cần hash</param>
        /// <returns>Chuỗi hash SHA-256 (64 ký tự hex)</returns>
        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert password to byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to hex string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // x2 = lowercase hex
                }

                return builder.ToString();
            }
        }

        public bool Login(string email, string password)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Hash password trước khi gửi đến stored procedure
                string hashedPassword = HashPassword(password);

                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@Password", hashedPassword);

                var user = conn.Query<NguoiDung>(
                    "DangNhap",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (user == null)
                    return false;

                SaveInforHelper.SaveInfo(
                    user.Email,
                    user.TenDangNhap,
                    user.VaiTro ?? "Chưa có vai trò",
                    user.TrangThai ? "Hoạt động" : "Khóa"
                );

                return true;
            }
        }

    }
}
