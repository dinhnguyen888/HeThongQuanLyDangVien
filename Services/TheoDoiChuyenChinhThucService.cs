using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class TheoDoiChuyenChinhThucService
    {
        /// <summary>
        /// Lấy tất cả theo dõi chuyển chính thức
        /// </summary>
        public List<TheoDoiChuyenChinhThucDTO> GetAll(int? dangVienID = null, string trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<TheoDoiChuyenChinhThucDTO>(
                    "TheoDoiChuyenChinhThuc_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy theo dõi chuyển chính thức theo ID
        /// </summary>
        public TheoDoiChuyenChinhThucDTO GetById(int theoDoiChuyenChinhThucID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TheoDoiChuyenChinhThucID", theoDoiChuyenChinhThucID);

                var result = conn.Query<TheoDoiChuyenChinhThucDTO>(
                    "TheoDoiChuyenChinhThuc_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// Lấy theo dõi chuyển chính thức theo DangVienID
        /// </summary>
        public List<TheoDoiChuyenChinhThucDTO> GetByDangVienID(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<TheoDoiChuyenChinhThucDTO>(
                    "TheoDoiChuyenChinhThuc_GetByDangVienID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy danh sách cần nhắc nhở (sắp đến hạn chuyển chính thức)
        /// </summary>
        public List<TheoDoiChuyenChinhThucDTO> GetCanNhacNho(int soNgayTruoc = 30)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SoNgayTruoc", soNgayTruoc);

                var result = conn.Query<TheoDoiChuyenChinhThucDTO>(
                    "TheoDoiChuyenChinhThuc_GetCanNhacNho",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Thêm mới theo dõi chuyển chính thức
        /// </summary>
        public (int id, string error) Insert(TheoDoiChuyenChinhThuc model)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", model.DangVienID);
                parameters.Add("@NgayVaoDang", model.NgayVaoDang);
                parameters.Add("@NgayChuyenChinhThuc", model.NgayChuyenChinhThuc);
                parameters.Add("@TrangThai", model.TrangThai);
                parameters.Add("@GhiChu", model.GhiChu);
                parameters.Add("@NguoiTao", model.NguoiTao);
                parameters.Add("@TheoDoiChuyenChinhThucID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("TheoDoiChuyenChinhThuc_Insert", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    
                    if (returnValue == 0)
                    {
                        int id = parameters.Get<int>("@TheoDoiChuyenChinhThucID");
                        return (id, null);
                    }
                    else
                    {
                        return (0, "Thêm theo dõi chuyển chính thức thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật theo dõi chuyển chính thức
        /// </summary>
        public (bool success, string error) Update(TheoDoiChuyenChinhThuc model)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TheoDoiChuyenChinhThucID", model.TheoDoiChuyenChinhThucID);
                parameters.Add("@DangVienID", model.DangVienID);
                parameters.Add("@NgayVaoDang", model.NgayVaoDang);
                parameters.Add("@NgayChuyenChinhThuc", model.NgayChuyenChinhThuc);
                parameters.Add("@TrangThai", model.TrangThai);
                parameters.Add("@GhiChu", model.GhiChu);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("TheoDoiChuyenChinhThuc_Update", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    
                    if (returnValue == 0)
                    {
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Cập nhật theo dõi chuyển chính thức thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa theo dõi chuyển chính thức
        /// </summary>
        public (bool success, string error) Delete(int theoDoiChuyenChinhThucID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TheoDoiChuyenChinhThucID", theoDoiChuyenChinhThucID);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("TheoDoiChuyenChinhThuc_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    
                    if (returnValue == 0)
                    {
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Xóa theo dõi chuyển chính thức thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// DTO cho TheoDoiChuyenChinhThuc
    /// </summary>
    public class TheoDoiChuyenChinhThucDTO
    {
        public int TheoDoiChuyenChinhThucID { get; set; }
        public int DangVienID { get; set; }
        public string TenDangVien { get; set; }
        public DateTime NgayVaoDang { get; set; }
        public DateTime? NgayChuyenChinhThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int? SoNgay { get; set; }
        public int? SoNgayDaVaoDang { get; set; }
        public int? SoNgayConLai { get; set; }
    }
}

