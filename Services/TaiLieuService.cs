using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class TaiLieuService
    {
        /// <summary>
        /// Lấy danh sách tài liệu
        /// </summary>
        public List<TaiLieuDTO> GetAll(int? donViID = null, string loaiTaiLieu = null, bool? trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@LoaiTaiLieu", loaiTaiLieu);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<TaiLieuDTO>(
                    "TaiLieu_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy tài liệu theo ID
        /// </summary>
        public TaiLieuDTO GetById(int taiLieuID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TaiLieuID", taiLieuID);

                var result = conn.QueryFirstOrDefault<TaiLieuDTO>(
                    "TaiLieu_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        /// <summary>
        /// Thêm tài liệu mới
        /// </summary>
        public (int id, string error) Insert(TaiLieu taiLieu)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DonViID", taiLieu.DonViID);
                    parameters.Add("@TieuDe", taiLieu.TieuDe);
                    parameters.Add("@LoaiTaiLieu", taiLieu.LoaiTaiLieu);
                    parameters.Add("@NoiDung", taiLieu.NoiDung);
                    parameters.Add("@FileDinhKem", taiLieu.FileDinhKem);
                    parameters.Add("@NgayPhatHanh", taiLieu.NgayPhatHanh);
                    parameters.Add("@CoQuanPhatHanh", taiLieu.CoQuanPhatHanh);
                    parameters.Add("@NguoiTao", taiLieu.NguoiTao);
                    parameters.Add("@TaiLieuID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute("TaiLieu_Insert", parameters, commandType: CommandType.StoredProcedure);

                    int id = parameters.Get<int>("@TaiLieuID");
                    return (id, null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật tài liệu
        /// </summary>
        public (bool success, string error) Update(TaiLieu taiLieu)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TaiLieuID", taiLieu.TaiLieuID);
                    parameters.Add("@DonViID", taiLieu.DonViID);
                    parameters.Add("@TieuDe", taiLieu.TieuDe);
                    parameters.Add("@LoaiTaiLieu", taiLieu.LoaiTaiLieu);
                    parameters.Add("@NoiDung", taiLieu.NoiDung);
                    parameters.Add("@FileDinhKem", taiLieu.FileDinhKem);
                    parameters.Add("@NgayPhatHanh", taiLieu.NgayPhatHanh);
                    parameters.Add("@CoQuanPhatHanh", taiLieu.CoQuanPhatHanh);
                    parameters.Add("@TrangThai", taiLieu.TrangThai);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("TaiLieu_Update", parameters, commandType: CommandType.StoredProcedure);

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
        /// Xóa tài liệu
        /// </summary>
        public (bool success, string error) Delete(int taiLieuID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    var taiLieu = GetById(taiLieuID);
                    string filePath = taiLieu?.FileDinhKem;

                    var parameters = new DynamicParameters();
                    parameters.Add("@TaiLieuID", taiLieuID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("TaiLieu_Delete", parameters, commandType: CommandType.StoredProcedure);

                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                    {
                        // Xóa file đính kèm nếu có
                        if (!string.IsNullOrWhiteSpace(filePath))
                        {
                            FileHelper.DeleteFile(filePath);
                        }
                        return (true, null);
                    }
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

    /// <summary>
    /// DTO cho Tài liệu
    /// </summary>
    public class TaiLieuDTO
    {
        public int TaiLieuID { get; set; }
        public int? DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string TieuDe { get; set; }
        public string LoaiTaiLieu { get; set; }
        public string NoiDung { get; set; }
        public string FileDinhKem { get; set; }
        public DateTime? NgayPhatHanh { get; set; }
        public string CoQuanPhatHanh { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }
}

