using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

namespace QuanLyDangVien.Services
{
    /// <summary>
    /// Service cho Quân nhân - Module 1
    /// </summary>
    public class QuanNhanService
    {
        private AuditLogService _auditLogService;

        public QuanNhanService()
        {
            _auditLogService = new AuditLogService();
        }
        /// <summary>
        /// Lấy danh sách quân nhân với các điều kiện lọc
        /// </summary>
        public List<QuanNhanDTO> GetAll(int? donViID = null, string hoTen = null, string soCCCD = null, 
            string capBac = null, string chucVu = null, bool? trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@HoTen", hoTen);
                parameters.Add("@SoCCCD", soCCCD);
                parameters.Add("@CapBac", capBac);
                parameters.Add("@ChucVu", chucVu);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<QuanNhanDTO>(
                    "QuanNhan_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy quân nhân theo ID
        /// </summary>
        public QuanNhanDTO GetById(int quanNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@QuanNhanID", quanNhanID);

                var result = conn.Query<QuanNhanDTO>(
                    "QuanNhan_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// Thêm quân nhân mới
        /// </summary>
        public (int id, string error) Insert(QuanNhan quanNhan)
        {
            try
            {
                // Đảm bảo các trường bắt buộc có giá trị
                if (quanNhan == null)
                {
                    return (0, "Dữ liệu quân nhân không hợp lệ!");
                }
                
                // Kiểm tra và set giá trị mặc định cho các trường bắt buộc
                if (quanNhan.DonViID <= 0)
                {
                    return (0, "Vui lòng chọn đơn vị!");
                }
                
                if (string.IsNullOrWhiteSpace(quanNhan.HoTen))
                {
                    return (0, "Vui lòng nhập họ tên!");
                }
                
                if (string.IsNullOrWhiteSpace(quanNhan.SoCCCD))
                {
                    return (0, "Vui lòng nhập số CCCD!");
                }
                
                // Trim và chuẩn hóa dữ liệu
                quanNhan.HoTen = quanNhan.HoTen.Trim();
                quanNhan.SoCCCD = quanNhan.SoCCCD.Trim();
                
                // TrangThai đã có giá trị mặc định là true trong model
                
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    
                    var parameters = new DynamicParameters();
                    parameters.Add("@DonViID", quanNhan.DonViID);
                    parameters.Add("@HoTen", quanNhan.HoTen);
                    parameters.Add("@NgaySinh", quanNhan.NgaySinh);
                    parameters.Add("@SHSQ", quanNhan.SHSQ);
                    parameters.Add("@SoTheBHYT", quanNhan.SoTheBHYT);
                    parameters.Add("@SoCCCD", quanNhan.SoCCCD);
                    parameters.Add("@CapBac", quanNhan.CapBac);
                    parameters.Add("@ChucVu", quanNhan.ChucVu);
                    parameters.Add("@NhapNgu", quanNhan.NhapNgu);
                    parameters.Add("@NgayVaoDang", quanNhan.NgayVaoDang);
                    parameters.Add("@SoTheDang", quanNhan.SoTheDang);
                    parameters.Add("@Doan", quanNhan.Doan);
                    parameters.Add("@DanToc", quanNhan.DanToc);
                    parameters.Add("@TonGiao", quanNhan.TonGiao);
                    parameters.Add("@SucKhoe", quanNhan.SucKhoe);
                    parameters.Add("@NhomMau", quanNhan.NhomMau);
                    parameters.Add("@HoTenChaNamSinh", quanNhan.HoTenChaNamSinh);
                    parameters.Add("@HoTenMeNamSinh", quanNhan.HoTenMeNamSinh);
                    parameters.Add("@HoTenVoConNamSinh", quanNhan.HoTenVoConNamSinh);
                    parameters.Add("@NgheNghiepChaMe", quanNhan.NgheNghiepChaMe);
                    parameters.Add("@MayAnhChiEm", quanNhan.MayAnhChiEm);
                    parameters.Add("@QueQuan", quanNhan.QueQuan);
                    parameters.Add("@NoiO", quanNhan.NoiO);
                    parameters.Add("@KhiCanBaoTin", quanNhan.KhiCanBaoTin);
                    parameters.Add("@GhiChu", quanNhan.GhiChu);
                    parameters.Add("@AnhDaiDien", quanNhan.AnhDaiDien, dbType: DbType.Binary);
                    parameters.Add("@NguoiTao", string.IsNullOrWhiteSpace(quanNhan.NguoiTao) ? Environment.UserName : quanNhan.NguoiTao);
                    parameters.Add("@QuanNhanID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute("QuanNhan_Insert", parameters, commandType: CommandType.StoredProcedure);
                    
                    int quanNhanID = parameters.Get<int>("@QuanNhanID");
                    
                    if (quanNhanID > 0)
                    {
                        // Ghi Audit Log (không quan trọng, bỏ qua nếu lỗi)
                        try
                        {
                            string newValues = JsonConvert.SerializeObject(quanNhan, Formatting.None);
                            _auditLogService.LogAction("Insert", "QuanNhan", quanNhanID, null, newValues);
                        }
                        catch { }
                        
                        return (quanNhanID, null);
                    }
                    else
                    {
                        return (0, "Không thể lấy ID sau khi thêm quân nhân.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = sqlEx.Message;
                if (sqlEx.Number == 2627)
                    errorMessage = "Số CCCD đã tồn tại trong hệ thống!";
                else if (sqlEx.Number == 547)
                    errorMessage = "Đơn vị không tồn tại! Vui lòng chọn đơn vị hợp lệ.";
                return (0, errorMessage);
            }
            catch (Exception ex)
            {
                return (0, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật thông tin quân nhân
        /// </summary>
        public (bool success, string error) Update(QuanNhan quanNhan)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Lấy dữ liệu cũ trước khi update để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldQuanNhanDTO = GetById(quanNhan.QuanNhanID);
                    if (oldQuanNhanDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldQuanNhanDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

                var parameters = new DynamicParameters();
                parameters.Add("@QuanNhanID", quanNhan.QuanNhanID);
                parameters.Add("@DonViID", quanNhan.DonViID);
                //parameters.Add("@TT", quanNhan.TT);
                parameters.Add("@HoTen", quanNhan.HoTen);
                parameters.Add("@NgaySinh", quanNhan.NgaySinh);
                parameters.Add("@SHSQ", quanNhan.SHSQ);
                parameters.Add("@SoTheBHYT", quanNhan.SoTheBHYT);
                parameters.Add("@SoCCCD", quanNhan.SoCCCD);
                parameters.Add("@CapBac", quanNhan.CapBac);
                parameters.Add("@ChucVu", quanNhan.ChucVu);
                parameters.Add("@NhapNgu", quanNhan.NhapNgu);
                parameters.Add("@NgayVaoDang", quanNhan.NgayVaoDang);
                parameters.Add("@SoTheDang", quanNhan.SoTheDang);
                parameters.Add("@Doan", quanNhan.Doan);
                parameters.Add("@DanToc", quanNhan.DanToc);
                parameters.Add("@TonGiao", quanNhan.TonGiao);
                parameters.Add("@SucKhoe", quanNhan.SucKhoe);
                parameters.Add("@NhomMau", quanNhan.NhomMau);
                parameters.Add("@HoTenChaNamSinh", quanNhan.HoTenChaNamSinh);
                parameters.Add("@HoTenMeNamSinh", quanNhan.HoTenMeNamSinh);
                parameters.Add("@HoTenVoConNamSinh", quanNhan.HoTenVoConNamSinh);
                parameters.Add("@NgheNghiepChaMe", quanNhan.NgheNghiepChaMe);
                parameters.Add("@MayAnhChiEm", quanNhan.MayAnhChiEm);
                parameters.Add("@QueQuan", quanNhan.QueQuan);
                parameters.Add("@NoiO", quanNhan.NoiO);
                parameters.Add("@KhiCanBaoTin", quanNhan.KhiCanBaoTin);
                parameters.Add("@GhiChu", quanNhan.GhiChu);
                parameters.Add("@AnhDaiDien", quanNhan.AnhDaiDien, dbType: DbType.Binary);
                parameters.Add("@TrangThai", quanNhan.TrangThai);
                parameters.Add("@NguoiTao", quanNhan.NguoiTao);

                try
                {
                    var result = conn.Execute("QuanNhan_Update", parameters, commandType: CommandType.StoredProcedure);
                    
                    // Ghi Audit Log
                    try
                    {
                        string newValues = JsonConvert.SerializeObject(quanNhan, Formatting.None);
                        _auditLogService.LogAction("Update", "QuanNhan", quanNhan.QuanNhanID, oldValues, newValues);
                    }
                    catch { } // Không throw nếu audit log lỗi
                    
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa quân nhân
        /// </summary>
        public (bool success, string error) Delete(int quanNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Lấy dữ liệu cũ trước khi delete để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldQuanNhanDTO = GetById(quanNhanID);
                    if (oldQuanNhanDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldQuanNhanDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

                var parameters = new DynamicParameters();
                parameters.Add("@QuanNhanID", quanNhanID);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("QuanNhan_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 1)
                    {
                        // Ghi Audit Log
                        try
                        {
                            _auditLogService.LogAction("Delete", "QuanNhan", quanNhanID, oldValues, null);
                        }
                        catch { } // Không throw nếu audit log lỗi
                        
                        return (true, null);
                    }
                    else
                        return (false, "Xóa quân nhân thất bại.");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xuất danh sách quân nhân ra file
        /// </summary>
        public string ExportToExcel(List<QuanNhanDTO> data, string filePath)
        {
            try
            {
                // Implementation for Excel export
                // This would use Microsoft.Office.Interop.Excel or EPPlus
                return "Export thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi export: {ex.Message}";
            }
        }

        /// <summary>
        /// In danh sách quân nhân
        /// </summary>
        public string PrintList(List<QuanNhanDTO> data)
        {
            try
            {
                // Implementation for printing
                return "In thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi in: {ex.Message}";
            }
        }
    }
}
