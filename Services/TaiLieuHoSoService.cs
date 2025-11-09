using Dapper;
using QuanLyDangVien.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    /// <summary>
    /// Model cho Tài liệu hồ sơ đảng viên
    /// </summary>
    public class TaiLieuHoSoModel
    {
        public int TaiLieuHoSoID { get; set; }
        public int DangVienID { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public string LoaiFile { get; set; }
        public long? KichThuoc { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// Service cho Tài liệu hồ sơ đảng viên
    /// </summary>
    public class TaiLieuHoSoService
    {
        /// <summary>
        /// Thêm tài liệu hồ sơ đảng viên
        /// </summary>
        public (int id, string error) Insert(int dangVienID, string tenFile, string duongDan, string loaiFile, long? kichThuoc, string nguoiTao)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DangVienID", dangVienID);
                    parameters.Add("@TenFile", tenFile);
                    parameters.Add("@DuongDan", duongDan);
                    parameters.Add("@LoaiFile", loaiFile);
                    parameters.Add("@KichThuoc", kichThuoc);
                    parameters.Add("@NguoiTao", nguoiTao);
                    parameters.Add("@TaiLieuHoSoID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute("TaiLieuHoSo_Insert", parameters, commandType: CommandType.StoredProcedure);
                    
                    int id = parameters.Get<int>("@TaiLieuHoSoID");
                    return (id, null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách tài liệu hồ sơ theo DangVienID
        /// </summary>
        public List<TaiLieuHoSoModel> GetByDangVienID(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<TaiLieuHoSoModel>(
                    "TaiLieuHoSo_GetByDangVienID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy tài liệu hồ sơ theo ID
        /// </summary>
        public TaiLieuHoSoModel GetById(int taiLieuHoSoID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TaiLieuHoSoID", taiLieuHoSoID);

                var result = conn.QueryFirstOrDefault<TaiLieuHoSoModel>(
                    "TaiLieuHoSo_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        /// <summary>
        /// Cập nhật tài liệu hồ sơ
        /// </summary>
        public (bool success, string error) Update(int taiLieuHoSoID, string tenFile = null, string duongDan = null, string loaiFile = null, long? kichThuoc = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TaiLieuHoSoID", taiLieuHoSoID);
                    parameters.Add("@TenFile", tenFile);
                    parameters.Add("@DuongDan", duongDan);
                    parameters.Add("@LoaiFile", loaiFile);
                    parameters.Add("@KichThuoc", kichThuoc);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("TaiLieuHoSo_Update", parameters, commandType: CommandType.StoredProcedure);
                    
                    int returnValue = parameters.Get<int>("ReturnValue");
                    
                    if (returnValue == 0)
                        return (true, null);
                    else
                        return (false, "Không tìm thấy tài liệu hoặc không thể cập nhật");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa tài liệu hồ sơ
        /// </summary>
        public (bool success, string error) Delete(int taiLieuHoSoID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TaiLieuHoSoID", taiLieuHoSoID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("TaiLieuHoSo_Delete", parameters, commandType: CommandType.StoredProcedure);
                    
                    int returnValue = parameters.Get<int>("ReturnValue");
                    
                    if (returnValue == 0)
                        return (true, null);
                    else
                        return (false, "Không tìm thấy tài liệu hoặc không thể xóa");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }
    }
}

