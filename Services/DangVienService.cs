using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace QuanLyDangVien.Services
{
    public class DangVienService
    {
        private AuditLogService _auditLogService;

        public DangVienService()
        {
            _auditLogService = new AuditLogService();
        }
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
                parameters.Add("@DonViCap1", dangVien.DonViCap1);
                parameters.Add("@DonViCap2", dangVien.DonViCap2);
                parameters.Add("@HoTenKhaiSinh", dangVien.HoTenKhaiSinh);
                parameters.Add("@HoTen", dangVien.HoTen);
                parameters.Add("@HoTenKhac", dangVien.HoTenKhac);
                parameters.Add("@NgaySinh", dangVien.NgaySinh);
                parameters.Add("@GioiTinh", dangVien.GioiTinh);
                parameters.Add("@SoCCCD", dangVien.SoCCCD);
                parameters.Add("@SoDienThoai", dangVien.SoDienThoai);
                parameters.Add("@SoTheDangVien", dangVien.SoTheDangVien);
                parameters.Add("@SoLyLichDangVien", dangVien.SoLyLichDangVien);
                parameters.Add("@NgayThamGiaCachMang", dangVien.NgayThamGiaCachMang);
                parameters.Add("@NgayTuyenDung", dangVien.NgayTuyenDung);
                parameters.Add("@NgayNhapNgu", dangVien.NgayNhapNgu);
                parameters.Add("@NgayXuatNgu", dangVien.NgayXuatNgu);
                parameters.Add("@NgayTaiNgu", dangVien.NgayTaiNgu);
                parameters.Add("@NgayVaoDang", dangVien.NgayVaoDang);
                parameters.Add("@NgayChinhThuc", dangVien.NgayChinhThuc);
                parameters.Add("@LoaiDangVien", dangVien.LoaiDangVien);
                parameters.Add("@DoiTuong", dangVien.DoiTuong);
                parameters.Add("@CapBac", dangVien.CapBac);
                parameters.Add("@HeSoLuong", dangVien.HeSoLuong);
                parameters.Add("@ThangNamPhongCapBac", dangVien.ThangNamPhongCapBac);
                parameters.Add("@SoHieuQuanNhan", dangVien.SoHieuQuanNhan);
                parameters.Add("@ChucVu", dangVien.ChucVu);
                parameters.Add("@QueQuan", dangVien.QueQuan);
                parameters.Add("@TrinhDo", dangVien.TrinhDo);
                parameters.Add("@DiaChi", dangVien.DiaChi);
                parameters.Add("@DanToc", dangVien.DanToc);
                parameters.Add("@TonGiao", dangVien.TonGiao);
                parameters.Add("@NgheNghiep", dangVien.NgheNghiep);
                parameters.Add("@TrinhDoChuyenMon", dangVien.TrinhDoChuyenMon);
                parameters.Add("@LyLuanChinhTri", dangVien.LyLuanChinhTri);
                parameters.Add("@ChucDanhKhoaHoc", dangVien.ChucDanhKhoaHoc);
                parameters.Add("@HocViCaoNhat", dangVien.HocViCaoNhat);
                parameters.Add("@ChuyenNganh", dangVien.ChuyenNganh);
                parameters.Add("@ThoiGianHocVi", dangVien.ThoiGianHocVi);
                parameters.Add("@TrinhDoChiHuyQuanLy", dangVien.TrinhDoChiHuyQuanLy);
                parameters.Add("@NgoaiNgu", dangVien.NgoaiNgu);
                parameters.Add("@TrinhDoNgoaiNgu", dangVien.TrinhDoNgoaiNgu);
                parameters.Add("@ThoiGianNgoaiNgu", dangVien.ThoiGianNgoaiNgu);
                parameters.Add("@TiengDanToc", dangVien.TiengDanToc);
                parameters.Add("@QuaTrinhHocTap", dangVien.QuaTrinhHocTap);
                parameters.Add("@ChienDauPhucVuChienDau", dangVien.ChienDauPhucVuChienDau);
                parameters.Add("@DiNuocNgoai", dangVien.DiNuocNgoai);
                parameters.Add("@SucKhoeLoai", dangVien.SucKhoeLoai);
                parameters.Add("@NhomMau", dangVien.NhomMau);
                parameters.Add("@BenhChinh", dangVien.BenhChinh);
                parameters.Add("@ThuongTat", dangVien.ThuongTat);
                parameters.Add("@DanhHieuDuocPhong", dangVien.DanhHieuDuocPhong);
                parameters.Add("@NgheNghiepTruocNhapNgu", dangVien.NgheNghiepTruocNhapNgu);
                parameters.Add("@QuanHeCTXHTruocNhapNgu", dangVien.QuanHeCTXHTruocNhapNgu);
                parameters.Add("@TinhHinhNhaO", dangVien.TinhHinhNhaO);
                parameters.Add("@TinHoc", dangVien.TinHoc);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien, DbType.Binary);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@HoTenCha", dangVien.HoTenCha);
                parameters.Add("@NamSinhCha", dangVien.NamSinhCha);
                parameters.Add("@NgheNghiepCha", dangVien.NgheNghiepCha);
                parameters.Add("@HoTenMe", dangVien.HoTenMe);
                parameters.Add("@NamSinhMe", dangVien.NamSinhMe);
                parameters.Add("@NgheNghiepMe", dangVien.NgheNghiepMe);
                parameters.Add("@ThanhPhanGiaDinh", dangVien.ThanhPhanGiaDinh);
                parameters.Add("@QueQuanChaMe", dangVien.QueQuanChaMe);
                parameters.Add("@ChoOHienNayChaMe", dangVien.ChoOHienNayChaMe);
                parameters.Add("@SoConTrongGiaDinh", dangVien.SoConTrongGiaDinh);
                parameters.Add("@GioiTinhThuTuBanThan", dangVien.GioiTinhThuTuBanThan);
                parameters.Add("@TinhHinhKinhTeGiaDinh", dangVien.TinhHinhKinhTeGiaDinh);
                parameters.Add("@TinhHinhChinhTriGiaDinh", dangVien.TinhHinhChinhTriGiaDinh);
                parameters.Add("@HoTenChaVoChong", dangVien.HoTenChaVoChong);
                parameters.Add("@NamSinhChaVoChong", dangVien.NamSinhChaVoChong);
                parameters.Add("@NgheNghiepChaVoChong", dangVien.NgheNghiepChaVoChong);
                parameters.Add("@HoTenMeVoChong", dangVien.HoTenMeVoChong);
                parameters.Add("@NamSinhMeVoChong", dangVien.NamSinhMeVoChong);
                parameters.Add("@NgheNghiepMeVoChong", dangVien.NgheNghiepMeVoChong);
                parameters.Add("@ThanhPhanGiaDinhVoChong", dangVien.ThanhPhanGiaDinhVoChong);
                parameters.Add("@QueQuanGiaDinhVoChong", dangVien.QueQuanGiaDinhVoChong);
                parameters.Add("@ChoOHienNayGiaDinhVoChong", dangVien.ChoOHienNayGiaDinhVoChong);
                parameters.Add("@SoConTrongGiaDinhVoChong", dangVien.SoConTrongGiaDinhVoChong);
                parameters.Add("@ThuTuVoChongTrongGiaDinh", dangVien.ThuTuVoChongTrongGiaDinh);
                parameters.Add("@TinhHinhKTCTGiaDinhVoChong", dangVien.TinhHinhKTCTGiaDinhVoChong);
                parameters.Add("@NgheNghiepVoChong", dangVien.NgheNghiepVoChong);
                parameters.Add("@DangVienHayKhong", dangVien.DangVienHayKhong);
                parameters.Add("@NoiOHienNayVoChong", dangVien.NoiOHienNayVoChong);
                parameters.Add("@ThongTinCacCon", dangVien.ThongTinCacCon);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);
                parameters.Add("@DangVienID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = conn.Execute("DangVien_Insert", parameters, commandType: CommandType.StoredProcedure);
                
                string errorMessage = parameters.Get<string>("@ErrorMessage");
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return (0, errorMessage);
                }

                int dangVienID = parameters.Get<int>("@DangVienID");
                
                // Ghi Audit Log
                try
                {
                    string newValues = JsonConvert.SerializeObject(dangVien, Formatting.None);
                    _auditLogService.LogAction("Insert", "DangVien", dangVienID, null, newValues);
                }
                catch { } // Không throw nếu audit log lỗi

                return (dangVienID, null);
            }
        }

        /// <summary>
        /// Cập nhật thông tin đảng viên
        /// </summary>
        public (bool success, string error) Update(DangVien dangVien)
        {
            using (var conn = DbHelper.GetConnection())
            {
                // Lấy dữ liệu cũ trước khi update để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldDangVienDTO = GetById(dangVien.DangVienID);
                    if (oldDangVienDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldDangVienDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

                var parameters = new DynamicParameters();
                parameters.Add("@DangVienID", dangVien.DangVienID);
                parameters.Add("@DonViID", dangVien.DonViID);
                parameters.Add("@DonViCap1", dangVien.DonViCap1);
                parameters.Add("@DonViCap2", dangVien.DonViCap2);
                parameters.Add("@HoTenKhaiSinh", dangVien.HoTenKhaiSinh);
                parameters.Add("@HoTen", dangVien.HoTen);
                parameters.Add("@HoTenKhac", dangVien.HoTenKhac);
                parameters.Add("@NgaySinh", dangVien.NgaySinh);
                parameters.Add("@GioiTinh", dangVien.GioiTinh);
                parameters.Add("@SoCCCD", dangVien.SoCCCD);
                parameters.Add("@SoDienThoai", dangVien.SoDienThoai);
                parameters.Add("@SoTheDangVien", dangVien.SoTheDangVien);
                parameters.Add("@SoLyLichDangVien", dangVien.SoLyLichDangVien);
                parameters.Add("@NgayThamGiaCachMang", dangVien.NgayThamGiaCachMang);
                parameters.Add("@NgayTuyenDung", dangVien.NgayTuyenDung);
                parameters.Add("@NgayNhapNgu", dangVien.NgayNhapNgu);
                parameters.Add("@NgayXuatNgu", dangVien.NgayXuatNgu);
                parameters.Add("@NgayTaiNgu", dangVien.NgayTaiNgu);
                parameters.Add("@NgayVaoDang", dangVien.NgayVaoDang);
                parameters.Add("@NgayChinhThuc", dangVien.NgayChinhThuc);
                parameters.Add("@LoaiDangVien", dangVien.LoaiDangVien);
                parameters.Add("@DoiTuong", dangVien.DoiTuong);
                parameters.Add("@CapBac", dangVien.CapBac);
                parameters.Add("@HeSoLuong", dangVien.HeSoLuong);
                parameters.Add("@ThangNamPhongCapBac", dangVien.ThangNamPhongCapBac);
                parameters.Add("@SoHieuQuanNhan", dangVien.SoHieuQuanNhan);
                parameters.Add("@ChucVu", dangVien.ChucVu);
                parameters.Add("@QueQuan", dangVien.QueQuan);
                parameters.Add("@TrinhDo", dangVien.TrinhDo);
                parameters.Add("@DiaChi", dangVien.DiaChi);
                parameters.Add("@DanToc", dangVien.DanToc);
                parameters.Add("@TonGiao", dangVien.TonGiao);
                parameters.Add("@NgheNghiep", dangVien.NgheNghiep);
                parameters.Add("@TrinhDoChuyenMon", dangVien.TrinhDoChuyenMon);
                parameters.Add("@LyLuanChinhTri", dangVien.LyLuanChinhTri);
                parameters.Add("@ChucDanhKhoaHoc", dangVien.ChucDanhKhoaHoc);
                parameters.Add("@HocViCaoNhat", dangVien.HocViCaoNhat);
                parameters.Add("@ChuyenNganh", dangVien.ChuyenNganh);
                parameters.Add("@ThoiGianHocVi", dangVien.ThoiGianHocVi);
                parameters.Add("@TrinhDoChiHuyQuanLy", dangVien.TrinhDoChiHuyQuanLy);
                parameters.Add("@NgoaiNgu", dangVien.NgoaiNgu);
                parameters.Add("@TrinhDoNgoaiNgu", dangVien.TrinhDoNgoaiNgu);
                parameters.Add("@ThoiGianNgoaiNgu", dangVien.ThoiGianNgoaiNgu);
                parameters.Add("@TiengDanToc", dangVien.TiengDanToc);
                parameters.Add("@QuaTrinhHocTap", dangVien.QuaTrinhHocTap);
                parameters.Add("@ChienDauPhucVuChienDau", dangVien.ChienDauPhucVuChienDau);
                parameters.Add("@DiNuocNgoai", dangVien.DiNuocNgoai);
                parameters.Add("@SucKhoeLoai", dangVien.SucKhoeLoai);
                parameters.Add("@NhomMau", dangVien.NhomMau);
                parameters.Add("@BenhChinh", dangVien.BenhChinh);
                parameters.Add("@ThuongTat", dangVien.ThuongTat);
                parameters.Add("@DanhHieuDuocPhong", dangVien.DanhHieuDuocPhong);
                parameters.Add("@NgheNghiepTruocNhapNgu", dangVien.NgheNghiepTruocNhapNgu);
                parameters.Add("@QuanHeCTXHTruocNhapNgu", dangVien.QuanHeCTXHTruocNhapNgu);
                parameters.Add("@TinhHinhNhaO", dangVien.TinhHinhNhaO);
                parameters.Add("@TinHoc", dangVien.TinHoc);
                parameters.Add("@AnhDaiDien", dangVien.AnhDaiDien, DbType.Binary);
                parameters.Add("@QuaTrinhCongTac", dangVien.QuaTrinhCongTac);
                parameters.Add("@HoSoGiaDinh", dangVien.HoSoGiaDinh);
                parameters.Add("@HoTenCha", dangVien.HoTenCha);
                parameters.Add("@NamSinhCha", dangVien.NamSinhCha);
                parameters.Add("@NgheNghiepCha", dangVien.NgheNghiepCha);
                parameters.Add("@HoTenMe", dangVien.HoTenMe);
                parameters.Add("@NamSinhMe", dangVien.NamSinhMe);
                parameters.Add("@NgheNghiepMe", dangVien.NgheNghiepMe);
                parameters.Add("@ThanhPhanGiaDinh", dangVien.ThanhPhanGiaDinh);
                parameters.Add("@QueQuanChaMe", dangVien.QueQuanChaMe);
                parameters.Add("@ChoOHienNayChaMe", dangVien.ChoOHienNayChaMe);
                parameters.Add("@SoConTrongGiaDinh", dangVien.SoConTrongGiaDinh);
                parameters.Add("@GioiTinhThuTuBanThan", dangVien.GioiTinhThuTuBanThan);
                parameters.Add("@TinhHinhKinhTeGiaDinh", dangVien.TinhHinhKinhTeGiaDinh);
                parameters.Add("@TinhHinhChinhTriGiaDinh", dangVien.TinhHinhChinhTriGiaDinh);
                parameters.Add("@HoTenChaVoChong", dangVien.HoTenChaVoChong);
                parameters.Add("@NamSinhChaVoChong", dangVien.NamSinhChaVoChong);
                parameters.Add("@NgheNghiepChaVoChong", dangVien.NgheNghiepChaVoChong);
                parameters.Add("@HoTenMeVoChong", dangVien.HoTenMeVoChong);
                parameters.Add("@NamSinhMeVoChong", dangVien.NamSinhMeVoChong);
                parameters.Add("@NgheNghiepMeVoChong", dangVien.NgheNghiepMeVoChong);
                parameters.Add("@ThanhPhanGiaDinhVoChong", dangVien.ThanhPhanGiaDinhVoChong);
                parameters.Add("@QueQuanGiaDinhVoChong", dangVien.QueQuanGiaDinhVoChong);
                parameters.Add("@ChoOHienNayGiaDinhVoChong", dangVien.ChoOHienNayGiaDinhVoChong);
                parameters.Add("@SoConTrongGiaDinhVoChong", dangVien.SoConTrongGiaDinhVoChong);
                parameters.Add("@ThuTuVoChongTrongGiaDinh", dangVien.ThuTuVoChongTrongGiaDinh);
                parameters.Add("@TinhHinhKTCTGiaDinhVoChong", dangVien.TinhHinhKTCTGiaDinhVoChong);
                parameters.Add("@NgheNghiepVoChong", dangVien.NgheNghiepVoChong);
                parameters.Add("@DangVienHayKhong", dangVien.DangVienHayKhong);
                parameters.Add("@NoiOHienNayVoChong", dangVien.NoiOHienNayVoChong);
                parameters.Add("@ThongTinCacCon", dangVien.ThongTinCacCon);
                parameters.Add("@TrangThai", dangVien.TrangThai);
                parameters.Add("@NguoiTao", dangVien.NguoiTao);
                parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = conn.Execute("DangVien_Update", parameters, commandType: CommandType.StoredProcedure);
                
                string errorMessage = parameters.Get<string>("@ErrorMessage");
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return (false, errorMessage);
                }

                // Ghi Audit Log
                try
                {
                    string newValues = JsonConvert.SerializeObject(dangVien, Formatting.None);
                    _auditLogService.LogAction("Update", "DangVien", dangVien.DangVienID, oldValues, newValues);
                }
                catch { } // Không throw nếu audit log lỗi

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
                // Lấy dữ liệu cũ trước khi delete để ghi Audit Log
                string oldValues = null;
                try
                {
                    var oldDangVienDTO = GetById(dangVienID);
                    if (oldDangVienDTO != null)
                    {
                        oldValues = JsonConvert.SerializeObject(oldDangVienDTO, Formatting.None);
                    }
                }
                catch { } // Không throw nếu không lấy được old values

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
                    // Ghi Audit Log
                    try
                    {
                        _auditLogService.LogAction("Delete", "DangVien", dangVienID, oldValues, null);
                    }
                    catch { } // Không throw nếu audit log lỗi

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
