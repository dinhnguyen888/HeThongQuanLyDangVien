using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static QuanLyDangVien.Program;

namespace QuanLyDangVien.Services
{
    public class NguoiDungService
    {
        public bool Login(string email, string password)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@Password", password);

                var user = conn.Query<NguoiDung>(
                    "DangNhap",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (user != null)
                {
                    // Call SaveInfo method to save user info to local file
                    SaveInforHelper.SaveInfo(
                        user.Email,
                        user.TenDangNhap,
                        user.VaiTro ?? "Chưa có vai trò",
                        user.TrangThai ? "Hoạt động" : "Khóa"
                    );
                }
                else return false;

                return true; 
            }
        }
    }
}
