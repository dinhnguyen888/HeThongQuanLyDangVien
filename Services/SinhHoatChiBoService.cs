using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Helper;

namespace QuanLyDangVien.Services
{
    public class SinhHoatChiBoService
    {
        #region Sinh hoạt chi bộ

        /// <summary>
        /// Lấy danh sách sinh hoạt chi bộ
        /// </summary>
        public List<SinhHoatChiBo> GetSinhHoatChiBoList(int? donViID = null, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@TuNgay", tuNgay);
                parameters.Add("@DenNgay", denNgay);

                var result = conn.Query<SinhHoatChiBo>("SinhHoatChiBo_GetAll", 
                    parameters, 
                    commandType: CommandType.StoredProcedure).ToList();

                // Xử lý convert dữ liệu để tránh lỗi cast
                foreach (var item in result)
                {
                    if (item.SoLuongThamGia == null)
                        item.SoLuongThamGia = 0;
                }

                return result;
            }
        }

        /// <summary>
        /// Lấy thông tin sinh hoạt chi bộ theo ID
        /// </summary>
        public SinhHoatChiBo GetSinhHoatChiBoById(int sinhHoatID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", sinhHoatID);

                var result = conn.QueryFirstOrDefault<SinhHoatChiBo>("SinhHoatChiBo_GetById", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);

                // Xử lý convert dữ liệu để tránh lỗi cast
                if (result != null && result.SoLuongThamGia == null)
                    result.SoLuongThamGia = 0;

                return result;
            }
        }

        /// <summary>
        /// Thêm sinh hoạt chi bộ mới
        /// </summary>
        public int AddSinhHoatChiBo(SinhHoatChiBo sinhHoat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", sinhHoat.DonViID);
                parameters.Add("@TieuDe", sinhHoat.TieuDe);
                parameters.Add("@NgaySinhHoat", sinhHoat.NgaySinhHoat);
                parameters.Add("@DiaDiem", sinhHoat.DiaDiem);
                parameters.Add("@ChuTri", sinhHoat.ChuTri);
                parameters.Add("@ThuKy", sinhHoat.ThuKy);
                parameters.Add("@NoiDung", sinhHoat.NoiDung);
                parameters.Add("@FileNghiQuyet", sinhHoat.FileNghiQuyet);
                parameters.Add("@SoLuongThamGia", sinhHoat.SoLuongThamGia ?? 0);
                parameters.Add("@NguoiTao", Environment.UserName);
                parameters.Add("@SinhHoatID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("SinhHoatChiBo_Insert", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@SinhHoatID");
            }
        }

        /// <summary>
        /// Cập nhật sinh hoạt chi bộ
        /// </summary>
        public bool UpdateSinhHoatChiBo(SinhHoatChiBo sinhHoat)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", sinhHoat.SinhHoatID);
                parameters.Add("@DonViID", sinhHoat.DonViID);
                parameters.Add("@TieuDe", sinhHoat.TieuDe);
                parameters.Add("@NgaySinhHoat", sinhHoat.NgaySinhHoat);
                parameters.Add("@DiaDiem", sinhHoat.DiaDiem);
                parameters.Add("@ChuTri", sinhHoat.ChuTri);
                parameters.Add("@ThuKy", sinhHoat.ThuKy);
                parameters.Add("@NoiDung", sinhHoat.NoiDung);
                parameters.Add("@FileNghiQuyet", sinhHoat.FileNghiQuyet);
                parameters.Add("@SoLuongThamGia", sinhHoat.SoLuongThamGia ?? 0);
                parameters.Add("@TrangThai", sinhHoat.TrangThai);

                var result = conn.Execute("SinhHoatChiBo_Update", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);
                
                return result > 0;
            }
        }

        /// <summary>
        /// Xóa sinh hoạt chi bộ
        /// </summary>
        public bool DeleteSinhHoatChiBo(int sinhHoatID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", sinhHoatID);

                var result = conn.Execute("SinhHoatChiBo_Delete", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);
                
                return result > 0;
            }
        }

        #endregion

        #region Điểm danh

        /// <summary>
        /// Lấy danh sách điểm danh theo sinh hoạt
        /// </summary>
        public List<DiemDanhSinhHoat> GetDiemDanhBySinhHoat(int sinhHoatID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", sinhHoatID);

                return conn.Query<DiemDanhSinhHoat>("DiemDanhSinhHoat_GetBySinhHoatID", 
                    parameters, 
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Thêm điểm danh cho đảng viên
        /// </summary>
        public int AddDiemDanh(DiemDanhSinhHoat diemDanh)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", diemDanh.SinhHoatID);
                parameters.Add("@DangVienID", diemDanh.DangVienID);
                parameters.Add("@CoMat", diemDanh.TrangThaiThamGia == "Có mặt");
                parameters.Add("@LyDoVang", diemDanh.LyDoVangMat);
                parameters.Add("@GhiChu", diemDanh.GhiChu);
                parameters.Add("@NguoiDiemDanh", Environment.UserName);
                parameters.Add("@DiemDanhID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("DiemDanhSinhHoat_Insert", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@DiemDanhID");
            }
        }

        /// <summary>
        /// Cập nhật điểm danh
        /// </summary>
        public bool UpdateDiemDanh(DiemDanhSinhHoat diemDanh)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DiemDanhID", diemDanh.DiemDanhID);
                parameters.Add("@CoMat", diemDanh.TrangThaiThamGia == "Có mặt");
                parameters.Add("@LyDoVang", diemDanh.LyDoVangMat);
                parameters.Add("@GhiChu", diemDanh.GhiChu);

                var result = conn.Execute("DiemDanhSinhHoat_Update", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);
                
                return result > 0;
            }
        }

        /// <summary>
        /// Điểm danh hàng loạt cho tất cả đảng viên trong đơn vị
        /// </summary>
        public bool DiemDanhHangLoat(int sinhHoatID, int donViID, bool coMat = true)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SinhHoatID", sinhHoatID);
                parameters.Add("@DonViID", donViID);
                parameters.Add("@CoMat", coMat);
                parameters.Add("@NguoiDiemDanh", Environment.UserName);

                var result = conn.Execute("DiemDanhSinhHoat_HangLoat", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);
                
                return result > 0;
            }
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Lấy thống kê sinh hoạt chi bộ
        /// </summary>
        public DataTable GetThongKeSinhHoat(int? donViID = null, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        dv.TenDonVi,
                        COUNT(sh.SinhHoatID) as SoLanSinhHoat,
                        COUNT(CASE WHEN dd.CoMat = 1 THEN 1 END) as SoLanCoMat,
                        COUNT(CASE WHEN dd.CoMat = 0 THEN 1 END) as SoLanVangMat
                    FROM DonVi dv
                    LEFT JOIN SinhHoatChiBo sh ON dv.DonViID = sh.DonViID
                    LEFT JOIN DiemDanhSinhHoat dd ON sh.SinhHoatID = dd.SinhHoatID
                    WHERE (@DonViID IS NULL OR dv.DonViID = @DonViID)
                    AND (@TuNgay IS NULL OR sh.NgaySinhHoat >= @TuNgay)
                    AND (@DenNgay IS NULL OR sh.NgaySinhHoat <= @DenNgay)
                    GROUP BY dv.DonViID, dv.TenDonVi
                    ORDER BY dv.TenDonVi";

                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@TuNgay", tuNgay);
                parameters.Add("@DenNgay", denNgay);

                var result = conn.Query(sql, parameters).ToList();
                
                var dataTable = new DataTable();
                dataTable.Columns.Add("Tên Đơn vị", typeof(string));
                dataTable.Columns.Add("Số lần sinh hoạt", typeof(int));
                dataTable.Columns.Add("Số lần có mặt", typeof(int));
                dataTable.Columns.Add("Số lần vắng mặt", typeof(int));

                foreach (var row in result)
                {
                    dataTable.Rows.Add(
                        row.TenDonVi,
                        row.SoLanSinhHoat,
                        row.SoLanCoMat,
                        row.SoLanVangMat
                    );
                }

                return dataTable;
            }
        }

        #endregion
    }
}
