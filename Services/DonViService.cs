using Dapper;
using QuanLyDangVien.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDangVien.Services
{
    public class DonViService
    {
        public static List<DonVi> GetDonViData()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    string sql = "SELECT DonViID, TenDonVi FROM DonVi WHERE TrangThai = 1 ORDER BY TenDonVi";
                    var donViList = conn.Query<DonVi>(sql).ToList();

                    if (donViList == null || donViList.Count == 0)
                    {
                        return new List<DonVi>
                {
                    new DonVi { DonViID = 0, TenDonVi = "Chưa có đơn vị nào" }
                };
                    }

                    return donViList;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy dữ liệu đơn vị: {ex.Message}");
                return new List<DonVi>
        {
            new DonVi { DonViID = -1, TenDonVi = "Lỗi tải dữ liệu đơn vị" }
        };
            }
        }

    }
}
