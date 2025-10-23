using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class DangVienService
    {
        /// <summary>
        /// Lấy danh sách đảng viên với các điều kiện lọc
        /// </summary>
        public List<DangVien> GetAll(int? donViID = null, string hoTen = null, string soCCCD = null, 
            string loaiDangVien = null, string doiTuong = null, bool? trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@HoTen", hoTen);
                parameters.Add("@SoCCCD", soCCCD);
                parameters.Add("@LoaiDangVien", loaiDangVien);
                parameters.Add("@DoiTuong", doiTuong);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<DangVien>(
                    "DangVien_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Lấy đảng viên theo ID
        /// </summary>
        public DangVien GetById(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<DangVien>(
                    "DangVien_GetByID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// Thêm đảng viên mới
        /// </summary>
        public int Insert(DangVien dangVien)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", dangVien.DonViID);
                parameters.Add("@HoTen", dangVien.HoTen);
                parameters.Add("@NgaySinh", dangVien.NgaySinh);
                parameters.Add("@GioiTinh", dangVien.GioiTinh);
                parameters.Add("@SoCCCD", dangVien.SoCCCD);
                parameters.Add("@SoDienThoai", dangVien.SoDienThoai);
                parameters.Add("@SoTheDangVien", dangVien.SoTheDangVien);
                parameters.Add("@SoLyLichDangVien", dangVien.SoLyLichDangVien);
                parameters.Add("@NgayVaoDang", dangVien.NgayVaoDang);
                parameters.Add("@NgayChinhThuc", dangVien.NgayChinhThuc);
                parameters.Add("@LoaiDangVien", dangVien.LoaiDangVien);
                parameters.Add("@DoiTuong", dangVien.DoiTuong);
                parameters.Add("@CapBac", dangVien.CapBac);
                parameters.Add("@ChucVu", dangVien.ChucVu);
                parameters.Add("@QueQuan", dangVien.QueQuan);
                parameters.Add("@TrinhDo", dangVien.TrinhDo);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);
                parameters.Add("@DangVienID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("DangVien_Insert", parameters, commandType: CommandType.StoredProcedure);
                
                return parameters.Get<int>("@DangVienID");
            }
        }

        /// <summary>
        /// Cập nhật thông tin đảng viên
        /// </summary>
        public bool Update(DangVien dangVien)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVien.DangVienID);
                parameters.Add("@DonViID", dangVien.DonViID);
                parameters.Add("@HoTen", dangVien.HoTen);
                parameters.Add("@NgaySinh", dangVien.NgaySinh);
                parameters.Add("@GioiTinh", dangVien.GioiTinh);
                parameters.Add("@SoCCCD", dangVien.SoCCCD);
                parameters.Add("@SoDienThoai", dangVien.SoDienThoai);
                parameters.Add("@SoTheDangVien", dangVien.SoTheDangVien);
                parameters.Add("@SoLyLichDangVien", dangVien.SoLyLichDangVien);
                parameters.Add("@NgayVaoDang", dangVien.NgayVaoDang);
                parameters.Add("@NgayChinhThuc", dangVien.NgayChinhThuc);
                parameters.Add("@LoaiDangVien", dangVien.LoaiDangVien);
                parameters.Add("@DoiTuong", dangVien.DoiTuong);
                parameters.Add("@CapBac", dangVien.CapBac);
                parameters.Add("@ChucVu", dangVien.ChucVu);
                parameters.Add("@QueQuan", dangVien.QueQuan);
                parameters.Add("@TrinhDo", dangVien.TrinhDo);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@TrangThai", dangVien.TrangThai);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);

                int rowsAffected = conn.Execute("DangVien_Update", parameters, commandType: CommandType.StoredProcedure);
                
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Xóa đảng viên (soft delete)
        /// </summary>
        public bool Delete(int dangVienID, string nguoiTao)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);
                parameters.Add("@NguoiTao", nguoiTao);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("DangVien_Delete", parameters, commandType: CommandType.StoredProcedure);

                int result = parameters.Get<int>("ReturnValue");
                return result > 0; // 1 = thành công, -1 = thất bại
            }
        }
    


        /// <summary>
        /// Kiểm tra CCCD đã tồn tại chưa
        /// </summary>
        public bool IsCCCDExists(string soCCCD, int? excludeDangVienID = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM DangVien WHERE SoCCCD = @SoCCCD";
                var parameters = new DynamicParameters();
                parameters.Add("@SoCCCD", soCCCD);

                if (excludeDangVienID.HasValue)
                {
                    sql += " AND DangVienID != @DangVienID";
                    parameters.Add("@DangVienID", excludeDangVienID.Value);
                }

                int count = conn.QuerySingle<int>(sql, parameters);
                return count > 0;
            }
        }

        /// <summary>
        /// Lấy danh sách đơn vị
        /// </summary>
        public List<dynamic> GetDonViList()
        {
            using (var conn = DbHelper.GetConnection())
            {
                string sql = "SELECT DonViID, TenDonVi FROM DonVi WHERE TrangThai = 1 ORDER BY TenDonVi";
                var result = conn.Query(sql).ToList();
                return result;
            }
        }
    }
}
