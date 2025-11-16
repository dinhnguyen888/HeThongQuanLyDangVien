using Dapper;
using QuanLyDangVien.DTOs;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace QuanLyDangVien.Services
{
    /// <summary>
    /// Service cho Chuyển sinh hoạt đảng
    /// </summary>
    public class ChuyenSinhHoatDangService
    {
        private AuditLogService _auditLogService;

        public ChuyenSinhHoatDangService()
        {
            _auditLogService = new AuditLogService();
        }
        /// <summary>
        /// Lấy tất cả chuyển sinh hoạt đảng
        /// </summary>
        public List<ChuyenSinhHoatDangDTO> GetAll(int? dangVienID = null, int? donViID = null, int? nam = null, string trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);
                parameters.Add("@DonViID", donViID);
                parameters.Add("@Nam", nam);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<ChuyenSinhHoatDangDTO>(
                    "ChuyenSinhHoatDang_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy chuyển sinh hoạt đảng theo ID
        /// </summary>
        public ChuyenSinhHoatDangDTO GetById(int chuyenSinhHoatID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ChuyenSinhHoatID", chuyenSinhHoatID);

                var result = conn.QueryFirstOrDefault<ChuyenSinhHoatDangDTO>(
                    "ChuyenSinhHoatDang_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        /// <summary>
        /// Lấy danh sách chuyển sinh hoạt theo đảng viên
        /// </summary>
        public List<ChuyenSinhHoatDangDTO> GetByDangVienID(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<ChuyenSinhHoatDangDTO>(
                    "ChuyenSinhHoatDang_GetByDangVienID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy danh sách chuyển sinh hoạt theo đơn vị
        /// </summary>
        public List<ChuyenSinhHoatDangDTO> GetByDonViID(int donViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);

                var result = conn.Query<ChuyenSinhHoatDangDTO>(
                    "ChuyenSinhHoatDang_GetByDonViID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy danh sách chuyển sinh hoạt theo năm
        /// </summary>
        public List<ChuyenSinhHoatDangDTO> GetByYear(int nam)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<ChuyenSinhHoatDangDTO>(
                    "ChuyenSinhHoatDang_GetByYear",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Thêm chuyển sinh hoạt đảng
        /// </summary>
        public (int id, string error) Insert(ChuyenSinhHoatDang chuyenSinhHoat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DangVienID", chuyenSinhHoat.DangVienID);
                    parameters.Add("@DonViDi", chuyenSinhHoat.DonViDi);
                    parameters.Add("@DonViDen", chuyenSinhHoat.DonViDen);
                    parameters.Add("@NgayChuyen", chuyenSinhHoat.NgayChuyen);
                    parameters.Add("@LyDo", chuyenSinhHoat.LyDo);
                    parameters.Add("@GhiChu", chuyenSinhHoat.GhiChu);
                    parameters.Add("@FileQuyetDinh", chuyenSinhHoat.FileQuyetDinh);
                    parameters.Add("@TrangThai", chuyenSinhHoat.TrangThai ?? "Chờ duyệt");
                    parameters.Add("@NguoiTao", chuyenSinhHoat.NguoiTao ?? Environment.UserName);
                    parameters.Add("@ChuyenSinhHoatID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("ChuyenSinhHoatDang_Insert", parameters, commandType: CommandType.StoredProcedure);

                    int returnValue = parameters.Get<int>("ReturnValue");
                    string errorMessage = parameters.Get<string>("@ErrorMessage");
                    
                    if (returnValue == 0 && string.IsNullOrEmpty(errorMessage))
                    {
                        int id = parameters.Get<int>("@ChuyenSinhHoatID");
                        
                        // Ghi Audit Log
                        try
                        {
                            string newValues = JsonConvert.SerializeObject(chuyenSinhHoat, Formatting.None);
                            _auditLogService.LogAction("Insert", "ChuyenSinhHoatDang", id, null, newValues);
                        }
                        catch { } // Không throw nếu audit log lỗi
                        
                        return (id, null);
                    }
                    else
                    {
                        return (0, errorMessage ?? "Lỗi khi thêm chuyển sinh hoạt đảng");
                    }
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật chuyển sinh hoạt đảng
        /// </summary>
        public (bool success, string error) Update(ChuyenSinhHoatDang chuyenSinhHoat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Lấy dữ liệu cũ trước khi update để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldDTO = GetById(chuyenSinhHoat.ChuyenSinhHoatID);
                    if (oldDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ChuyenSinhHoatID", chuyenSinhHoat.ChuyenSinhHoatID);
                    parameters.Add("@DonViDi", chuyenSinhHoat.DonViDi);
                    parameters.Add("@DonViDen", chuyenSinhHoat.DonViDen);
                    parameters.Add("@NgayChuyen", chuyenSinhHoat.NgayChuyen);
                    parameters.Add("@LyDo", chuyenSinhHoat.LyDo);
                    parameters.Add("@GhiChu", chuyenSinhHoat.GhiChu);
                    parameters.Add("@FileQuyetDinh", chuyenSinhHoat.FileQuyetDinh);
                    parameters.Add("@TrangThai", chuyenSinhHoat.TrangThai);
                    parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("ChuyenSinhHoatDang_Update", parameters, commandType: CommandType.StoredProcedure);

                    int returnValue = parameters.Get<int>("ReturnValue");
                    string errorMessage = parameters.Get<string>("@ErrorMessage");
                    
                    if (returnValue == 0 && string.IsNullOrEmpty(errorMessage))
                    {
                        // Ghi Audit Log
                        try
                        {
                            string newValues = JsonConvert.SerializeObject(chuyenSinhHoat, Formatting.None);
                            _auditLogService.LogAction("Update", "ChuyenSinhHoatDang", chuyenSinhHoat.ChuyenSinhHoatID, oldValues, newValues);
                        }
                        catch { } // Không throw nếu audit log lỗi
                        
                        return (true, null);
                    }
                    else
                        return (false, errorMessage ?? "Không tìm thấy chuyển sinh hoạt đảng hoặc không thể cập nhật");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa chuyển sinh hoạt đảng
        /// </summary>
        public (bool success, string error) Delete(int chuyenSinhHoatID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Lấy dữ liệu cũ trước khi delete để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldDTO = GetById(chuyenSinhHoatID);
                    if (oldDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ChuyenSinhHoatID", chuyenSinhHoatID);
                    parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("ChuyenSinhHoatDang_Delete", parameters, commandType: CommandType.StoredProcedure);

                    int returnValue = parameters.Get<int>("ReturnValue");
                    string errorMessage = parameters.Get<string>("@ErrorMessage");
                    
                    if (returnValue == 0 && string.IsNullOrEmpty(errorMessage))
                    {
                        // Ghi Audit Log
                        try
                        {
                            _auditLogService.LogAction("Delete", "ChuyenSinhHoatDang", chuyenSinhHoatID, oldValues, null);
                        }
                        catch { } // Không throw nếu audit log lỗi
                        
                        return (true, null);
                    }
                    else
                        return (false, errorMessage ?? "Không tìm thấy chuyển sinh hoạt đảng hoặc không thể xóa");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }
    }
}

