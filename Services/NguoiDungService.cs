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
                    user.TrangThai ? "Hoạt động" : "Khóa",
                    user.NguoiDungID,
                    user.DonViID
                );

                return true;
            }
        }

        public System.Collections.Generic.List<NguoiDung> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                return conn.Query<NguoiDung>(
                    "NguoiDung_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        public NguoiDung GetById(int nguoiDungID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NguoiDungID", nguoiDungID);
                return conn.Query<NguoiDung>(
                    "NguoiDung_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }

        public int Insert(NguoiDung nguoiDung, string matKhau)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", nguoiDung.DonViID);
                parameters.Add("@TenDangNhap", nguoiDung.TenDangNhap);
                parameters.Add("@MatKhau", HashPassword(matKhau));
                parameters.Add("@HoTen", nguoiDung.HoTen);
                parameters.Add("@Email", nguoiDung.Email);
                parameters.Add("@VaiTro", nguoiDung.VaiTro);
                parameters.Add("@NguoiTao", nguoiDung.NguoiTao ?? "System");
                parameters.Add("@NguoiDungID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("NguoiDung_Insert", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@NguoiDungID");
            }
        }

        public bool Update(NguoiDung nguoiDung, string matKhau = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NguoiDungID", nguoiDung.NguoiDungID);
                parameters.Add("@DonViID", nguoiDung.DonViID);
                parameters.Add("@TenDangNhap", nguoiDung.TenDangNhap);
                if (!string.IsNullOrEmpty(matKhau))
                    parameters.Add("@MatKhau", HashPassword(matKhau));
                parameters.Add("@HoTen", nguoiDung.HoTen);
                parameters.Add("@Email", nguoiDung.Email);
                parameters.Add("@VaiTro", nguoiDung.VaiTro);
                parameters.Add("@TrangThai", nguoiDung.TrangThai);

                int result = conn.Execute("NguoiDung_Update", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Delete(int nguoiDungID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NguoiDungID", nguoiDungID);
                int result = conn.Execute("NguoiDung_Delete", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool ResetPassword(int nguoiDungID, string newPassword)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NguoiDungID", nguoiDungID);
                parameters.Add("@MatKhau", HashPassword(newPassword));
                int result = conn.Execute("NguoiDung_ResetPassword", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
