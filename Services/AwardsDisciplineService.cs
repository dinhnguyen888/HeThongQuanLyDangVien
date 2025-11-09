using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    /// <summary>
    /// Service cho Khen thưởng cá nhân (Wrapper - sử dụng stored procedures mới)
    /// </summary>
    public class KhenThuongCaNhanService
    {
        /// <summary>
        /// Lấy tất cả khen thưởng cá nhân
        /// </summary>
        public List<KhenThuongCaNhanDTO> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "CaNhan");

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KhenThuongSPDTO sang KhenThuongCaNhanDTO
                return result.Select(r => new KhenThuongCaNhanDTO
                {
                    KhenThuongCaNhanID = r.KhenThuongID,
                    DangVienID = r.DangVienID ?? 0,
                    TenDangVien = r.TenDangVien,
                    HoTenDangVien = r.TenDangVien,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy danh sách khen thưởng cá nhân theo đảng viên
        /// </summary>
        public List<KhenThuongCaNhanDTO> GetByDangVienID(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetByDangVienID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KhenThuongSPDTO sang KhenThuongCaNhanDTO
                return result.Select(r => new KhenThuongCaNhanDTO
                {
                    KhenThuongCaNhanID = r.KhenThuongID,
                    DangVienID = r.DangVienID ?? 0,
                    TenDangVien = r.TenDangVien,
                    HoTenDangVien = r.TenDangVien,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy khen thưởng cá nhân theo ID
        /// </summary>
        public KhenThuongCaNhanDTO GetById(int khenThuongCaNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KhenThuongID", khenThuongCaNhanID);

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (result == null || result.Loai != "CaNhan")
                    return null;

                return new KhenThuongCaNhanDTO
                {
                    KhenThuongCaNhanID = result.KhenThuongID,
                    DangVienID = result.DangVienID ?? 0,
                    TenDangVien = result.TenDangVien,
                    HoTenDangVien = result.TenDangVien,
                    HinhThuc = result.HinhThuc,
                    Ngay = result.Ngay,
                    SoQuyetDinh = result.SoQuyetDinh,
                    CapQuyetDinh = result.CapQuyetDinh,
                    NoiDung = result.NoiDung,
                    FileDinhKem = result.FileDinhKem,
                    NgayTao = result.NgayTao,
                    NguoiTao = result.NguoiTao
                };
            }
        }

        /// <summary>
        /// Thêm khen thưởng cá nhân
        /// </summary>
        public (int id, string error) Insert(KhenThuongCaNhan khenThuong)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "CaNhan");
                parameters.Add("@DangVienID", khenThuong.DangVienID);
                parameters.Add("@DonViID", (int?)null);
                parameters.Add("@HinhThuc", khenThuong.HinhThuc);
                parameters.Add("@Ngay", khenThuong.Ngay);
                parameters.Add("@SoQuyetDinh", khenThuong.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", khenThuong.CapQuyetDinh);
                parameters.Add("@NoiDung", khenThuong.NoiDung);
                parameters.Add("@FileDinhKem", khenThuong.FileDinhKem);
                parameters.Add("@NguoiTao", khenThuong.NguoiTao);
                parameters.Add("@KhenThuongID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    conn.Execute("KhenThuong_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return (parameters.Get<int>("@KhenThuongID"), null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật khen thưởng cá nhân
        /// </summary>
        public (bool success, string error) Update(KhenThuongCaNhan khenThuong)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KhenThuongID", khenThuong.KhenThuongCaNhanID);
                parameters.Add("@Loai", "CaNhan");
                parameters.Add("@DangVienID", khenThuong.DangVienID);
                parameters.Add("@DonViID", (int?)null);
                parameters.Add("@HinhThuc", khenThuong.HinhThuc);
                parameters.Add("@Ngay", khenThuong.Ngay);
                parameters.Add("@SoQuyetDinh", khenThuong.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", khenThuong.CapQuyetDinh);
                parameters.Add("@NoiDung", khenThuong.NoiDung);
                parameters.Add("@FileDinhKem", khenThuong.FileDinhKem);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("KhenThuong_Update", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    return (returnValue == 0, returnValue == 0 ? null : "Cập nhật thất bại");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa khen thưởng cá nhân
        /// </summary>
        public (bool success, string error) Delete(int khenThuongCaNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    string fileDinhKem = null;
                    var khenThuong = GetById(khenThuongCaNhanID);
                    if (khenThuong != null && !string.IsNullOrWhiteSpace(khenThuong.FileDinhKem))
                    {
                        fileDinhKem = khenThuong.FileDinhKem;
                    }

                    // Xóa record trong database
                    var parameters = new DynamicParameters();
                    parameters.Add("@KhenThuongID", khenThuongCaNhanID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("KhenThuong_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                    {
                        // Xóa file đính kèm nếu có
                        if (!string.IsNullOrWhiteSpace(fileDinhKem))
                        {
                            FileHelper.DeleteFile(fileDinhKem);
                        }
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Xóa thất bại");
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
    /// Service cho Khen thưởng đơn vị (Wrapper - sử dụng stored procedures mới)
    /// </summary>
    public class KhenThuongDonViService
    {
        /// <summary>
        /// Lấy tất cả khen thưởng đơn vị
        /// </summary>
        public List<KhenThuongDonViDTO> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "DonVi");

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KhenThuongSPDTO sang KhenThuongDonViDTO
                return result.Select(r => new KhenThuongDonViDTO
                {
                    KhenThuongDonViID = r.KhenThuongID,
                    DonViID = r.DonViID ?? 0,
                    TenDonVi = r.TenDonVi,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy danh sách khen thưởng đơn vị
        /// </summary>
        public List<KhenThuongDonViDTO> GetByDonViID(int donViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetByDonViID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KhenThuongSPDTO sang KhenThuongDonViDTO
                return result.Select(r => new KhenThuongDonViDTO
                {
                    KhenThuongDonViID = r.KhenThuongID,
                    DonViID = r.DonViID ?? 0,
                    TenDonVi = r.TenDonVi,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy khen thưởng đơn vị theo ID
        /// </summary>
        public KhenThuongDonViDTO GetById(int khenThuongDonViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KhenThuongID", khenThuongDonViID);

                var result = conn.Query<KhenThuongSPDTO>(
                    "KhenThuong_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (result == null || result.Loai != "DonVi")
                    return null;

                return new KhenThuongDonViDTO
                {
                    KhenThuongDonViID = result.KhenThuongID,
                    DonViID = result.DonViID ?? 0,
                    TenDonVi = result.TenDonVi,
                    HinhThuc = result.HinhThuc,
                    Ngay = result.Ngay,
                    SoQuyetDinh = result.SoQuyetDinh,
                    CapQuyetDinh = result.CapQuyetDinh,
                    NoiDung = result.NoiDung,
                    FileDinhKem = result.FileDinhKem,
                    NgayTao = result.NgayTao,
                    NguoiTao = result.NguoiTao
                };
            }
        }

        /// <summary>
        /// Thêm khen thưởng đơn vị
        /// </summary>
        public (int id, string error) Insert(KhenThuongDonVi khenThuong)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "DonVi");
                parameters.Add("@DangVienID", (int?)null);
                parameters.Add("@DonViID", khenThuong.DonViID);
                parameters.Add("@HinhThuc", khenThuong.HinhThuc);
                parameters.Add("@Ngay", khenThuong.Ngay);
                parameters.Add("@SoQuyetDinh", khenThuong.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", khenThuong.CapQuyetDinh);
                parameters.Add("@NoiDung", khenThuong.NoiDung);
                parameters.Add("@FileDinhKem", khenThuong.FileDinhKem);
                parameters.Add("@NguoiTao", khenThuong.NguoiTao);
                parameters.Add("@KhenThuongID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    conn.Execute("KhenThuong_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return (parameters.Get<int>("@KhenThuongID"), null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật khen thưởng đơn vị
        /// </summary>
        public (bool success, string error) Update(KhenThuongDonVi khenThuong)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KhenThuongID", khenThuong.KhenThuongDonViID);
                parameters.Add("@Loai", "DonVi");
                parameters.Add("@DangVienID", (int?)null);
                parameters.Add("@DonViID", khenThuong.DonViID);
                parameters.Add("@HinhThuc", khenThuong.HinhThuc);
                parameters.Add("@Ngay", khenThuong.Ngay);
                parameters.Add("@SoQuyetDinh", khenThuong.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", khenThuong.CapQuyetDinh);
                parameters.Add("@NoiDung", khenThuong.NoiDung);
                parameters.Add("@FileDinhKem", khenThuong.FileDinhKem);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("KhenThuong_Update", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    return (returnValue == 0, returnValue == 0 ? null : "Cập nhật thất bại");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa khen thưởng đơn vị
        /// </summary>
        public (bool success, string error) Delete(int khenThuongDonViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    string fileDinhKem = null;
                    var khenThuong = GetById(khenThuongDonViID);
                    if (khenThuong != null && !string.IsNullOrWhiteSpace(khenThuong.FileDinhKem))
                    {
                        fileDinhKem = khenThuong.FileDinhKem;
                    }

                    // Xóa record trong database
                    var parameters = new DynamicParameters();
                    parameters.Add("@KhenThuongID", khenThuongDonViID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("KhenThuong_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                    {
                        // Xóa file đính kèm nếu có
                        if (!string.IsNullOrWhiteSpace(fileDinhKem))
                        {
                            FileHelper.DeleteFile(fileDinhKem);
                        }
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Xóa thất bại");
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
    /// Service cho Kỷ luật cá nhân (Wrapper - sử dụng stored procedures mới)
    /// </summary>
    public class KyLuatCaNhanService
    {
        /// <summary>
        /// Lấy tất cả kỷ luật cá nhân
        /// </summary>
        public List<KyLuatCaNhanDTO> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "CaNhan");

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KyLuatSPDTO sang KyLuatCaNhanDTO
                return result.Select(r => new KyLuatCaNhanDTO
                {
                    KyLuatCaNhanID = r.KyLuatID,
                    DangVienID = r.DangVienID ?? 0,
                    TenDangVien = r.TenDangVien,
                    HoTenDangVien = r.TenDangVien,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    GhiChu = r.GhiChu,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy danh sách kỷ luật cá nhân theo đảng viên
        /// </summary>
        public List<KyLuatCaNhanDTO> GetByDangVienID(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetByDangVienID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KyLuatSPDTO sang KyLuatCaNhanDTO
                return result.Select(r => new KyLuatCaNhanDTO
                {
                    KyLuatCaNhanID = r.KyLuatID,
                    DangVienID = r.DangVienID ?? 0,
                    TenDangVien = r.TenDangVien,
                    HoTenDangVien = r.TenDangVien,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    GhiChu = r.GhiChu,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy kỷ luật cá nhân theo ID
        /// </summary>
        public KyLuatCaNhanDTO GetById(int kyLuatCaNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KyLuatID", kyLuatCaNhanID);

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (result == null || result.Loai != "CaNhan")
                    return null;

                return new KyLuatCaNhanDTO
                {
                    KyLuatCaNhanID = result.KyLuatID,
                    DangVienID = result.DangVienID ?? 0,
                    TenDangVien = result.TenDangVien,
                    HoTenDangVien = result.TenDangVien,
                    HinhThuc = result.HinhThuc,
                    Ngay = result.Ngay,
                    SoQuyetDinh = result.SoQuyetDinh,
                    CapQuyetDinh = result.CapQuyetDinh,
                    NoiDung = result.NoiDung,
                    GhiChu = result.GhiChu,
                    FileDinhKem = result.FileDinhKem,
                    NgayTao = result.NgayTao,
                    NguoiTao = result.NguoiTao
                };
            }
        }

        /// <summary>
        /// Thêm kỷ luật cá nhân
        /// </summary>
        public (int id, string error) Insert(KyLuatCaNhan kyLuat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "CaNhan");
                parameters.Add("@DangVienID", kyLuat.DangVienID);
                parameters.Add("@DonViID", (int?)null);
                parameters.Add("@HinhThuc", kyLuat.HinhThuc);
                parameters.Add("@Ngay", kyLuat.Ngay);
                parameters.Add("@SoQuyetDinh", kyLuat.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", kyLuat.CapQuyetDinh);
                parameters.Add("@NoiDung", kyLuat.NoiDung);
                parameters.Add("@GhiChu", kyLuat.GhiChu);
                parameters.Add("@FileDinhKem", kyLuat.FileDinhKem);
                parameters.Add("@NguoiTao", kyLuat.NguoiTao);
                parameters.Add("@KyLuatID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    conn.Execute("KyLuat_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return (parameters.Get<int>("@KyLuatID"), null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật kỷ luật cá nhân
        /// </summary>
        public (bool success, string error) Update(KyLuatCaNhan kyLuat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KyLuatID", kyLuat.KyLuatCaNhanID);
                parameters.Add("@Loai", "CaNhan");
                parameters.Add("@DangVienID", kyLuat.DangVienID);
                parameters.Add("@DonViID", (int?)null);
                parameters.Add("@HinhThuc", kyLuat.HinhThuc);
                parameters.Add("@Ngay", kyLuat.Ngay);
                parameters.Add("@SoQuyetDinh", kyLuat.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", kyLuat.CapQuyetDinh);
                parameters.Add("@NoiDung", kyLuat.NoiDung);
                parameters.Add("@GhiChu", kyLuat.GhiChu);
                parameters.Add("@FileDinhKem", kyLuat.FileDinhKem);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("KyLuat_Update", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    return (returnValue == 0, returnValue == 0 ? null : "Cập nhật thất bại");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa kỷ luật cá nhân
        /// </summary>
        public (bool success, string error) Delete(int kyLuatCaNhanID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    string fileDinhKem = null;
                    var kyLuat = GetById(kyLuatCaNhanID);
                    if (kyLuat != null && !string.IsNullOrWhiteSpace(kyLuat.FileDinhKem))
                    {
                        fileDinhKem = kyLuat.FileDinhKem;
                    }

                    // Xóa record trong database
                    var parameters = new DynamicParameters();
                    parameters.Add("@KyLuatID", kyLuatCaNhanID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("KyLuat_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                    {
                        // Xóa file đính kèm nếu có
                        if (!string.IsNullOrWhiteSpace(fileDinhKem))
                        {
                            FileHelper.DeleteFile(fileDinhKem);
                        }
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Xóa thất bại");
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
    /// Service cho Kỷ luật tổ chức đảng (Wrapper - sử dụng stored procedures mới)
    /// </summary>
    public class KyLuatToChucService
    {
        /// <summary>
        /// Lấy tất cả kỷ luật tổ chức đảng
        /// </summary>
        public List<KyLuatToChucDTO> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "DonVi");

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KyLuatSPDTO sang KyLuatToChucDTO
                return result.Select(r => new KyLuatToChucDTO
                {
                    KyLuatToChucID = r.KyLuatID,
                    DonViID = r.DonViID ?? 0,
                    TenDonVi = r.TenDonVi,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    GhiChu = r.GhiChu,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy danh sách kỷ luật tổ chức đảng theo đơn vị
        /// </summary>
        public List<KyLuatToChucDTO> GetByDonViID(int donViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetByDonViID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Map từ KyLuatSPDTO sang KyLuatToChucDTO
                return result.Select(r => new KyLuatToChucDTO
                {
                    KyLuatToChucID = r.KyLuatID,
                    DonViID = r.DonViID ?? 0,
                    TenDonVi = r.TenDonVi,
                    HinhThuc = r.HinhThuc,
                    Ngay = r.Ngay,
                    SoQuyetDinh = r.SoQuyetDinh,
                    CapQuyetDinh = r.CapQuyetDinh,
                    NoiDung = r.NoiDung,
                    GhiChu = r.GhiChu,
                    FileDinhKem = r.FileDinhKem,
                    NgayTao = r.NgayTao,
                    NguoiTao = r.NguoiTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy kỷ luật tổ chức đảng theo ID
        /// </summary>
        public KyLuatToChucDTO GetById(int kyLuatToChucID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KyLuatID", kyLuatToChucID);

                var result = conn.Query<KyLuatSPDTO>(
                    "KyLuat_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                if (result == null || result.Loai != "DonVi")
                    return null;

                return new KyLuatToChucDTO
                {
                    KyLuatToChucID = result.KyLuatID,
                    DonViID = result.DonViID ?? 0,
                    TenDonVi = result.TenDonVi,
                    HinhThuc = result.HinhThuc,
                    Ngay = result.Ngay,
                    SoQuyetDinh = result.SoQuyetDinh,
                    CapQuyetDinh = result.CapQuyetDinh,
                    NoiDung = result.NoiDung,
                    GhiChu = result.GhiChu,
                    FileDinhKem = result.FileDinhKem,
                    NgayTao = result.NgayTao,
                    NguoiTao = result.NguoiTao
                };
            }
        }

        /// <summary>
        /// Thêm kỷ luật tổ chức đảng
        /// </summary>
        public (int id, string error) Insert(KyLuatToChuc kyLuat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Loai", "DonVi");
                parameters.Add("@DangVienID", (int?)null);
                parameters.Add("@DonViID", kyLuat.DonViID);
                parameters.Add("@HinhThuc", kyLuat.HinhThuc);
                parameters.Add("@Ngay", kyLuat.Ngay);
                parameters.Add("@SoQuyetDinh", kyLuat.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", kyLuat.CapQuyetDinh);
                parameters.Add("@NoiDung", kyLuat.NoiDung);
                parameters.Add("@GhiChu", kyLuat.GhiChu);
                parameters.Add("@FileDinhKem", kyLuat.FileDinhKem);
                parameters.Add("@NguoiTao", kyLuat.NguoiTao);
                parameters.Add("@KyLuatID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    conn.Execute("KyLuat_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return (parameters.Get<int>("@KyLuatID"), null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật kỷ luật tổ chức đảng
        /// </summary>
        public (bool success, string error) Update(KyLuatToChuc kyLuat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KyLuatID", kyLuat.KyLuatToChucID);
                parameters.Add("@Loai", "DonVi");
                parameters.Add("@DangVienID", (int?)null);
                parameters.Add("@DonViID", kyLuat.DonViID);
                parameters.Add("@HinhThuc", kyLuat.HinhThuc);
                parameters.Add("@Ngay", kyLuat.Ngay);
                parameters.Add("@SoQuyetDinh", kyLuat.SoQuyetDinh);
                parameters.Add("@CapQuyetDinh", kyLuat.CapQuyetDinh);
                parameters.Add("@NoiDung", kyLuat.NoiDung);
                parameters.Add("@GhiChu", kyLuat.GhiChu);
                parameters.Add("@FileDinhKem", kyLuat.FileDinhKem);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                try
                {
                    conn.Execute("KyLuat_Update", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");
                    return (returnValue == 0, returnValue == 0 ? null : "Cập nhật thất bại");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa kỷ luật tổ chức đảng
        /// </summary>
        public (bool success, string error) Delete(int kyLuatToChucID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    string fileDinhKem = null;
                    var kyLuat = GetById(kyLuatToChucID);
                    if (kyLuat != null && !string.IsNullOrWhiteSpace(kyLuat.FileDinhKem))
                    {
                        fileDinhKem = kyLuat.FileDinhKem;
                    }

                    // Xóa record trong database
                    var parameters = new DynamicParameters();
                    parameters.Add("@KyLuatID", kyLuatToChucID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("KyLuat_Delete", parameters, commandType: CommandType.StoredProcedure);
                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                    {
                        // Xóa file đính kèm nếu có
                        if (!string.IsNullOrWhiteSpace(fileDinhKem))
                        {
                            FileHelper.DeleteFile(fileDinhKem);
                        }
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Xóa thất bại");
                    }
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }
    }


    #region DTOs for Stored Procedures

    /// <summary>
    /// DTO cho KhenThuong từ stored procedure (internal)
    /// </summary>
    internal class KhenThuongSPDTO
    {
        public int KhenThuongID { get; set; }
        public string Loai { get; set; }
        public int? DangVienID { get; set; }
        public string TenDangVien { get; set; }
        public int? DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string HinhThuc { get; set; }
        public DateTime Ngay { get; set; }
        public string SoQuyetDinh { get; set; }
        public string CapQuyetDinh { get; set; }
        public string NoiDung { get; set; }
        public string FileDinhKem { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho KyLuat từ stored procedure (internal)
    /// </summary>
    internal class KyLuatSPDTO
    {
        public int KyLuatID { get; set; }
        public string Loai { get; set; }
        public int? DangVienID { get; set; }
        public string TenDangVien { get; set; }
        public int? DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string HinhThuc { get; set; }
        public DateTime Ngay { get; set; }
        public string SoQuyetDinh { get; set; }
        public string CapQuyetDinh { get; set; }
        public string NoiDung { get; set; }
        public string GhiChu { get; set; }
        public string FileDinhKem { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }

    #endregion
}
