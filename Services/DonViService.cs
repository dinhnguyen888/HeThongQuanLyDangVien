using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDangVien.Services
{
    public class DonViService
    {
        #region Get Operations

        /// <summary>
        /// Lấy danh sách đơn vị đơn giản (chỉ ID và tên) cho dropdown
        /// </summary>
        public List<DonViSimplified> GetDonViData()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    string sql = "SELECT DonViID, TenDonVi FROM DonVi ORDER BY TenDonVi";
                    var donViList = conn.Query<DonViSimplified>(sql).ToList();

                    if (donViList == null || donViList.Count == 0)
                    {
                        return new List<DonViSimplified>
                        {
                            new DonViSimplified { DonViID = 0, TenDonVi = "Chưa có đơn vị nào" }
                        };
                    }

                    return donViList;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy dữ liệu đơn vị: {ex.Message}");
                return new List<DonViSimplified>
                {
                    new DonViSimplified { DonViID = -1, TenDonVi = "Lỗi tải dữ liệu đơn vị" }
                };
            }
        }

        /// <summary>
        /// Lấy danh sách đơn vị theo ID đảng viên
        /// </summary>
        public List<DonViSimplified> GetDonViDataByDangVienId(int dangVienId)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    string sql = @"
                        SELECT d.DonViID, d.TenDonVi
                        FROM DonVi d
                        INNER JOIN DangVien dv ON d.DonViID = dv.DonViID
                        WHERE dv.DangVienID = @DangVienID
                        ORDER BY d.TenDonVi";

                    var donViList = conn.Query<DonViSimplified>(sql, new { DangVienID = dangVienId }).ToList();

                    if (donViList == null || donViList.Count == 0)
                    {
                        return new List<DonViSimplified>
                        {
                            new DonViSimplified { DonViID = 0, TenDonVi = "Chưa có đơn vị nào" }
                        };
                    }

                    return donViList;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy dữ liệu đơn vị: {ex.Message}");
                return new List<DonViSimplified>
                {
                    new DonViSimplified { DonViID = -1, TenDonVi = "Lỗi tải dữ liệu đơn vị" }
                };
            }
        }

        /// <summary>
        /// Lấy tất cả đơn vị với đầy đủ thông tin
        /// </summary>
        public List<DonVi> GetAll()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    var result = conn.Query<DonVi>("DonVi_GetAll", commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy danh sách đơn vị: {ex.Message}");
                throw new Exception($"Lỗi khi lấy danh sách đơn vị: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy đơn vị theo ID
        /// </summary>
        public DonVi GetById(int donViId)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    var parameters = new { DonViID = donViId };
                    var result = conn.QueryFirstOrDefault<DonVi>("DonVi_GetByID", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy đơn vị theo ID: {ex.Message}");
                throw new Exception($"Lỗi khi lấy đơn vị theo ID: {ex.Message}");
            }
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Thêm mới đơn vị
        /// </summary>
        public int Insert(DonVi donVi)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    var parameters = new DynamicParameters();
                    parameters.Add("@TenDonVi", donVi.TenDonVi);
                    parameters.Add("@MaDonVi", donVi.MaDonVi);
                    parameters.Add("@CapBac", donVi.CapBac);
                    parameters.Add("@DiaChi", donVi.DiaChi);
                    parameters.Add("@Email", donVi.Email);
                    parameters.Add("@TruongDonVi", donVi.TruongDonVi);
                    parameters.Add("@CapTrenID", donVi.CapTrenID);
                    parameters.Add("@NguoiTao", donVi.NguoiTao);
                    parameters.Add("@DonViID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute("DonVi_Insert", parameters, commandType: CommandType.StoredProcedure);

                    return parameters.Get<int>("@DonViID");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi thêm đơn vị: {ex.Message}");
                throw new Exception($"Lỗi khi thêm đơn vị: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật thông tin đơn vị
        /// </summary>
        public bool Update(DonVi donVi)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    var parameters = new DynamicParameters();
                    parameters.Add("@DonViID", donVi.DonViID);
                    parameters.Add("@TenDonVi", donVi.TenDonVi);
                    parameters.Add("@MaDonVi", donVi.MaDonVi);
                    parameters.Add("@CapBac", donVi.CapBac);
                    parameters.Add("@DiaChi", donVi.DiaChi);
                    parameters.Add("@Email", donVi.Email);
                    parameters.Add("@TruongDonVi", donVi.TruongDonVi);
                    parameters.Add("@CapTrenID", donVi.CapTrenID);
                    parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("DonVi_Update", parameters, commandType: CommandType.StoredProcedure);
                    
                    int returnValue = parameters.Get<int>("@ReturnValue");
                    return returnValue == 0; // 0 = thành công
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi cập nhật đơn vị: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật đơn vị: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa đơn vị
        /// </summary>
        public bool Delete(int donViId)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        throw new Exception("Không thể tạo database connection");

                    var parameters = new DynamicParameters();
                    parameters.Add("@DonViID", donViId);
                    parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("DonVi_Delete", parameters, commandType: CommandType.StoredProcedure);
                    
                    int returnValue = parameters.Get<int>("@ReturnValue");
                    if (returnValue == -2)
                    {
                        throw new Exception("Không thể xóa đơn vị vì đang có đảng viên hoặc quân nhân thuộc đơn vị này!");
                    }
                    
                    return returnValue == 0; // 0 = thành công
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa đơn vị: {ex.Message}");
                throw new Exception($"Lỗi khi xóa đơn vị: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Kiểm tra mã đơn vị đã tồn tại chưa
        /// </summary>
        public bool IsCodeExists(string maDonVi, int excludeId = 0)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        return false;

                    var parameters = new DynamicParameters();
                    parameters.Add("@MaDonVi", maDonVi);
                    parameters.Add("@DonViID", excludeId > 0 ? excludeId : (int?)null);
                    parameters.Add("@Exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    conn.Execute("DonVi_CheckCodeExists", parameters, commandType: CommandType.StoredProcedure);

                    return parameters.Get<bool>("@Exists");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi kiểm tra mã đơn vị: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy thống kê số lượng đảng viên theo đơn vị
        /// </summary>
        public List<DonViThongKe> GetDangVienCountByDonVi()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    if (conn == null)
                        return new List<DonViThongKe>();

                    var result = conn.Query<DonViThongKe>("DonVi_GetDangVienCount", commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy thống kê đảng viên: {ex.Message}");
                return new List<DonViThongKe>();
            }
        }

        #endregion
    }

    /// <summary>
    /// Class cho thống kê đơn vị
    /// </summary>
    public class DonViThongKe
    {
        public int DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string MaDonVi { get; set; }
        public int SoLuongDangVien { get; set; }
        public int DangVienChinhThuc { get; set; }
        public int DangVienDuBi { get; set; }
    }
}
