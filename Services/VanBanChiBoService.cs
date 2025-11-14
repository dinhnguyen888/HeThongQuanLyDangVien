using Dapper;
using QuanLyDangVien.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class VanBanChiBoService
    {
        /// <summary>
        /// Lấy danh sách văn bản chi bộ
        /// </summary>
        public List<VanBanChiBoDTO> GetAll(int? donViID = null, string trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<VanBanChiBoDTO>(
                    "VanBanChiBo_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy văn bản chi bộ theo ID
        /// </summary>
        public VanBanChiBoDTO GetById(int vanBanChiBoID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@VanBanChiBoID", vanBanChiBoID);

                var result = conn.QueryFirstOrDefault<VanBanChiBoDTO>(
                    "VanBanChiBo_GetById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        /// <summary>
        /// Thêm văn bản chi bộ mới
        /// </summary>
        public (int id, string error) Insert(VanBanChiBoModel vanBan)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DonViID", vanBan.DonViID);
                    parameters.Add("@TenVanBan", vanBan.TenVanBan);
                    parameters.Add("@NoiDung", vanBan.NoiDung);
                    parameters.Add("@NgayGui", vanBan.NgayGui);
                    parameters.Add("@NgayNhan", vanBan.NgayNhan);
                    parameters.Add("@TrangThai", vanBan.TrangThai);
                    parameters.Add("@FileDinhKem", vanBan.FileDinhKem);
                    parameters.Add("@NguoiTao", vanBan.NguoiTao);
                    parameters.Add("@VanBanChiBoID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute("VanBanChiBo_Insert", parameters, commandType: CommandType.StoredProcedure);

                    int id = parameters.Get<int>("@VanBanChiBoID");
                    return (id, null);
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        /// <summary>
        /// Cập nhật văn bản chi bộ
        /// </summary>
        public (bool success, string error) Update(VanBanChiBoModel vanBan)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@VanBanChiBoID", vanBan.VanBanChiBoID);
                    parameters.Add("@DonViID", vanBan.DonViID);
                    parameters.Add("@TenVanBan", vanBan.TenVanBan);
                    parameters.Add("@NoiDung", vanBan.NoiDung);
                    parameters.Add("@NgayGui", vanBan.NgayGui);
                    parameters.Add("@NgayNhan", vanBan.NgayNhan);
                    parameters.Add("@TrangThai", vanBan.TrangThai);
                    parameters.Add("@FileDinhKem", vanBan.FileDinhKem);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("VanBanChiBo_Update", parameters, commandType: CommandType.StoredProcedure);

                    int returnValue = parameters.Get<int>("ReturnValue");

                    if (returnValue == 0)
                        return (true, null);
                    else
                        return (false, "Không tìm thấy văn bản hoặc không thể cập nhật");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Xóa văn bản chi bộ
        /// </summary>
        public (bool success, string error) Delete(int vanBanChiBoID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                try
                {
                    // Lấy thông tin file đính kèm trước khi xóa
                    var vanBan = GetById(vanBanChiBoID);
                    string filePath = vanBan?.FileDinhKem;

                    var parameters = new DynamicParameters();
                    parameters.Add("@VanBanChiBoID", vanBanChiBoID);
                    parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    conn.Execute("VanBanChiBo_Delete", parameters, commandType: CommandType.StoredProcedure);

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
                        return (false, "Không tìm thấy văn bản hoặc không thể xóa");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// Model cho Văn bản chi bộ
    /// </summary>
    public class VanBanChiBoModel
    {
        public int VanBanChiBoID { get; set; }
        public int DonViID { get; set; }
        public string TenVanBan { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public DateTime? NgayNhan { get; set; }
        public string TrangThai { get; set; }
        public string FileDinhKem { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }

    /// <summary>
    /// DTO cho Văn bản chi bộ
    /// </summary>
    public class VanBanChiBoDTO
    {
        public int VanBanChiBoID { get; set; }
        public int DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string TenVanBan { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public DateTime? NgayNhan { get; set; }
        public string TrangThai { get; set; }
        public string FileDinhKem { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
    }
}

