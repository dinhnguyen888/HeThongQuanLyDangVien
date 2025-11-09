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
    public class DangVienService
    {
        /// <summary>
        /// Lấy danh sách đảng viên với các điều kiện lọc (Enhanced)
        /// </summary>
        public List<DangVienDTO> GetAll(int? donViID = null, string hoTen = null, string soCCCD = null, 
            string loaiDangVien = null, string doiTuong = null, string capBac = null, 
            string chucVu = null, string queQuan = null, string trinhDo = null, bool? trangThai = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);
                parameters.Add("@HoTen", hoTen);
                parameters.Add("@SoCCCD", soCCCD);
                parameters.Add("@LoaiDangVien", loaiDangVien);
                parameters.Add("@DoiTuong", doiTuong);
                parameters.Add("@CapBac", capBac);
                parameters.Add("@ChucVu", chucVu);
                parameters.Add("@QueQuan", queQuan);
                parameters.Add("@TrinhDo", trinhDo);
                parameters.Add("@TrangThai", trangThai);

                var result = conn.Query<DangVienDTO>(
                    "DangVien_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        /// <summary>
        /// Convert DangVienDetails thành DangVien
        /// </summary>
        public DangVien ConvertToDangVien(DangVienDetails details)
        {
            if (details == null) return null;

            return new DangVien
            {
                DangVienID = details.DangVienID,
                DonViID = GetDonViIDByName(details.TenDonVi), // Cần lấy DonViID từ TenDonVi
                HoTen = details.HoTen,
                NgaySinh = details.NgaySinh,
                GioiTinh = details.GioiTinh,
                SoCCCD = details.SoCCCD,
                SoDienThoai = details.SoDienThoai,
                SoTheDangVien = details.SoTheDangVien,
                SoLyLichDangVien = details.SoLyLichDangVien,
                NgayVaoDang = details.NgayVaoDang,
                NgayChinhThuc = details.NgayChinhThuc,
                LoaiDangVien = details.LoaiDangVien,
                DoiTuong = details.DoiTuong,
                CapBac = details.CapBac,
                ChucVu = details.ChucVu,
                QueQuan = details.QueQuan,
                TrinhDo = details.TrinhDo,
                AnhDaiDien = details.AnhDaiDien,
                QuaTrinhCongTac = details.QuaTrinhCongTac,
                HoSoGiaDinh = details.HoSoGiaDinh,
                TrangThai = details.TrangThai,
                NgayTao = details.NgayTao,
                NguoiTao = details.NguoiTao
            };
        }

        /// <summary>
        /// Lấy DonViID từ TenDonVi
        /// </summary>
        private int GetDonViIDByName(string tenDonVi)
        {
            if (string.IsNullOrEmpty(tenDonVi)) return 0;

            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TenDonVi", tenDonVi);

                var result = conn.QueryFirstOrDefault<int>(
                    "SELECT DonViID FROM DonVi WHERE TenDonVi = @TenDonVi",
                    parameters
                );

                return result;
            }
        }

        /// <summary>
        /// Lấy đảng viên theo ID (Enhanced)
        /// </summary>
        public DangVienDTO GetById(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);

                var result = conn.Query<DangVienDTO>(
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
        public (int id, string error) Insert(DangVien dangVien)
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
                parameters.Add("@DiaChi", dangVien.DiaChi);
                parameters.Add("@DanToc", dangVien.DanToc);
                parameters.Add("@TonGiao", dangVien.TonGiao);
                parameters.Add("@NgheNghiep", dangVien.NgheNghiep);
                parameters.Add("@TrinhDoHocVan", dangVien.TrinhDoHocVan);
                parameters.Add("@TrinhDoChuyenMon", dangVien.TrinhDoChuyenMon);
                parameters.Add("@LyLuanChinhTri", dangVien.LyLuanChinhTri);
                parameters.Add("@NgoaiNgu", dangVien.NgoaiNgu);
                parameters.Add("@TinHoc", dangVien.TinHoc);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien, DbType.Binary);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);
                parameters.Add("@DangVienID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = conn.Execute("DangVien_Insert", parameters, commandType: CommandType.StoredProcedure);
                
                string errorMessage = parameters.Get<string>("@ErrorMessage");
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return (0, errorMessage);
                }

                return (parameters.Get<int>("@DangVienID"), null);
            }
        }

        /// <summary>
        /// Cập nhật thông tin đảng viên
        /// </summary>
        public (bool success, string error) Update(DangVien dangVien)
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
                parameters.Add("@DiaChi", dangVien.DiaChi);
                parameters.Add("@DanToc", dangVien.DanToc);
                parameters.Add("@TonGiao", dangVien.TonGiao);
                parameters.Add("@NgheNghiep", dangVien.NgheNghiep);
                parameters.Add("@TrinhDoHocVan", dangVien.TrinhDoHocVan);
                parameters.Add("@TrinhDoChuyenMon", dangVien.TrinhDoChuyenMon);
                parameters.Add("@LyLuanChinhTri", dangVien.LyLuanChinhTri);
                parameters.Add("@NgoaiNgu", dangVien.NgoaiNgu);
                parameters.Add("@TinHoc", dangVien.TinHoc);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien, DbType.Binary);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@TrangThai", dangVien.TrangThai);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);
                parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = conn.Execute("DangVien_Update", parameters, commandType: CommandType.StoredProcedure);
                
                string errorMessage = parameters.Get<string>("@ErrorMessage");
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return (false, errorMessage);
                }

                return (true, null);
            }
        }

        /// <summary>
        /// Xóa đảng viên (soft delete)
        /// </summary>
        public (bool success, string error) Delete(int dangVienID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVienID);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("DangVien_Delete", parameters, commandType: CommandType.StoredProcedure);

                int returnValue = parameters.Get<int>("ReturnValue");


                // return sucess is 1
                if (returnValue == -1)
                {
                    return (false, "Xóa đảng viên thất bại.");

                }
                else
                {
                    return (true, null);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách đảng viên theo đơn vị ID (Enhanced)
        /// </summary>
        public List<DangVienDTO> GetByDonViID(int donViID)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DonViID", donViID);

                var result = conn.Query<DangVienDTO>(
                    "DangVien_GetByDonViID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }
    }
}
