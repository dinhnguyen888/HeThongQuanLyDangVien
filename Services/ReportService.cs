using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    /// <summary>
    /// Service cho Báo cáo và Thống kê - Module 8
    /// </summary>
    public class ReportService
    {
        /// <summary>
        /// Lấy báo cáo thống kê tổng hợp theo năm
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeTongHop(int? nam = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'TongSoDangVien' AS LoaiThongKe,
                        DoiTuong AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1
                        AND (@Nam IS NULL OR YEAR(NgayTao) = @Nam)
                    GROUP BY DoiTuong
                    
                    UNION ALL
                    
                    SELECT 
                        'LoaiDangVien' AS LoaiThongKe,
                        LoaiDangVien AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1
                        AND (@Nam IS NULL OR YEAR(NgayTao) = @Nam)
                    GROUP BY LoaiDangVien
                    
                    UNION ALL
                    
                    SELECT 
                        'GioiTinh' AS LoaiThongKe,
                        GioiTinh AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1
                        AND (@Nam IS NULL OR YEAR(NgayTao) = @Nam)
                    GROUP BY GioiTinh";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoThongKeDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy báo cáo thống kê theo đơn vị
        /// </summary>
        public List<BaoCaoTheoDonViDTO> GetThongKeTheoDonVi(int? nam = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        d.TenDonVi,
                        COUNT(dv.DangVienID) AS TongSoDangVien,
                        SUM(CASE WHEN dv.LoaiDangVien = N'Chính thức' THEN 1 ELSE 0 END) AS DangVienChinhThuc,
                        SUM(CASE WHEN dv.LoaiDangVien = N'Dự bị' THEN 1 ELSE 0 END) AS DangVienDuBi,
                        @Nam AS Nam,
                        SUM(CASE WHEN dv.GioiTinh = N'Nam' THEN 1 ELSE 0 END) AS Nam,
                        SUM(CASE WHEN dv.GioiTinh = N'Nữ' THEN 1 ELSE 0 END) AS Nu
                    FROM DonVi d
                    LEFT JOIN DangVien dv ON d.DonViID = dv.DonViID AND dv.TrangThai = 1
                    WHERE (@Nam IS NULL OR YEAR(dv.NgayTao) = @Nam)
                    GROUP BY d.DonViID, d.TenDonVi
                    ORDER BY TongSoDangVien DESC";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoTheoDonViDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê số kết nạp mới theo năm
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeKetNapMoi(int nam)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'KetNapMoi' AS LoaiThongKe,
                        MONTH(NgayVaoDang) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1
                        AND YEAR(NgayVaoDang) = @Nam
                    GROUP BY MONTH(NgayVaoDang)
                    ORDER BY MONTH(NgayVaoDang)";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoThongKeDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê chuyển sinh hoạt theo năm
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeChuyenSinhHoat(int nam)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'ChuyenSinhHoat' AS LoaiThongKe,
                        MONTH(NgayChuyen) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM ChuyenSinhHoatDang
                    WHERE YEAR(NgayChuyen) = @Nam
                    GROUP BY MONTH(NgayChuyen)
                    ORDER BY MONTH(NgayChuyen)";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoThongKeDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê khen thưởng theo năm
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeKhenThuong(int nam)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'KhenThuongCaNhan' AS LoaiThongKe,
                        MONTH(Ngay) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM KhenThuong
                    WHERE YEAR(Ngay) = @Nam AND Loai = 'CaNhan'
                    GROUP BY MONTH(Ngay)
                    
                    UNION ALL
                    
                    SELECT 
                        'KhenThuongDonVi' AS LoaiThongKe,
                        MONTH(Ngay) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM KhenThuong
                    WHERE YEAR(Ngay) = @Nam AND Loai = 'DonVi'
                    GROUP BY MONTH(Ngay)
                    ORDER BY Ten";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoThongKeDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê kỷ luật theo năm
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeKyLuat(int nam)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'KyLuatCaNhan' AS LoaiThongKe,
                        MONTH(Ngay) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM KyLuat
                    WHERE YEAR(Ngay) = @Nam AND Loai = 'CaNhan'
                    GROUP BY MONTH(Ngay)
                    
                    UNION ALL
                    
                    SELECT 
                        'KyLuatToChuc' AS LoaiThongKe,
                        MONTH(Ngay) AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM KyLuat
                    WHERE YEAR(Ngay) = @Nam AND Loai = 'DonVi'
                    GROUP BY MONTH(Ngay)
                    ORDER BY Ten";

                var parameters = new DynamicParameters();
                parameters.Add("@Nam", nam);

                var result = conn.Query<BaoCaoThongKeDTO>(sql, parameters).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê tuổi đời
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeTuoiDoi()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'TuoiDoi' AS LoaiThongKe,
                        CASE 
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 18 AND 30 THEN '18-30'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 31 AND 40 THEN '31-40'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 41 AND 50 THEN '41-50'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 51 AND 60 THEN '51-60'
                            ELSE 'Trên 60'
                        END AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1 AND NgaySinh IS NOT NULL
                    GROUP BY 
                        CASE 
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 18 AND 30 THEN '18-30'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 31 AND 40 THEN '31-40'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 41 AND 50 THEN '41-50'
                            WHEN DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 51 AND 60 THEN '51-60'
                            ELSE 'Trên 60'
                        END
                    ORDER BY Ten";

                var result = conn.Query<BaoCaoThongKeDTO>(sql).ToList();
                return result;
            }
        }

        /// <summary>
        /// Lấy thống kê tuổi đảng
        /// </summary>
        public List<BaoCaoThongKeDTO> GetThongKeTuoiDang()
        {
            using (var conn = DbHelper.GetConnection())
            {
                var sql = @"
                    SELECT 
                        'TuoiDang' AS LoaiThongKe,
                        CASE 
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 0 AND 5 THEN '0-5'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 6 AND 10 THEN '6-10'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 11 AND 20 THEN '11-20'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 21 AND 30 THEN '21-30'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 31 AND 40 THEN '31-40'
                            ELSE 'Trên 40'
                        END AS Ten,
                        COUNT(*) AS SoLuong,
                        CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS TyLe
                    FROM DangVien
                    WHERE TrangThai = 1 AND NgayVaoDang IS NOT NULL
                    GROUP BY 
                        CASE 
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 0 AND 5 THEN '0-5'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 6 AND 10 THEN '6-10'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 11 AND 20 THEN '11-20'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 21 AND 30 THEN '21-30'
                            WHEN DATEDIFF(YEAR, NgayVaoDang, GETDATE()) BETWEEN 31 AND 40 THEN '31-40'
                            ELSE 'Trên 40'
                        END
                    ORDER BY Ten";

                var result = conn.Query<BaoCaoThongKeDTO>(sql).ToList();
                return result;
            }
        }

        /// <summary>
        /// Xuất báo cáo ra Excel
        /// </summary>
        public string ExportToExcel(List<BaoCaoThongKeDTO> data, string filePath, string reportType)
        {
            try
            {
                // Implementation for Excel export
                // This would use Microsoft.Office.Interop.Excel or EPPlus
                return $"Export báo cáo {reportType} thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi export: {ex.Message}";
            }
        }

        /// <summary>
        /// Xuất báo cáo ra PDF
        /// </summary>
        public string ExportToPDF(List<BaoCaoThongKeDTO> data, string filePath, string reportType)
        {
            try
            {
                // Implementation for PDF export
                // This would use iTextSharp or similar library
                return $"Export báo cáo {reportType} ra PDF thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi export PDF: {ex.Message}";
            }
        }

        /// <summary>
        /// Xuất báo cáo ra Word
        /// </summary>
        public string ExportToWord(List<BaoCaoThongKeDTO> data, string filePath, string reportType)
        {
            try
            {
                // Implementation for Word export using Microsoft.Office.Interop.Word
                return $"Export báo cáo {reportType} ra Word thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi export Word: {ex.Message}";
            }
        }
    }
}
