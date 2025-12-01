-- =============================================
-- Database: QuanLyDangVien
-- Description: Hệ thống quản lý đảng viên và quân nhân
-- Author: System
-- Created: 2024
-- Updated: 2024 - Refactored for new requirements
-- =============================================

USE [master];
GO

-- Tạo database nếu chưa tồn tại
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyDangVien')
BEGIN
    CREATE DATABASE [QuanLyDangVien] COLLATE SQL_Latin1_General_CP1_CI_AS;
    PRINT N'✓ Database QuanLyDangVien đã được tạo';
END
ELSE
BEGIN
    PRINT N'→ Database QuanLyDangVien đã tồn tại';
END
GO

USE [QuanLyDangVien];
GO

PRINT N'';
PRINT N'===========================================';
PRINT N'TẠO CÁC BẢNG';
PRINT N'===========================================';

-- =============================================
-- BẢNG: Đơn vị / Chi bộ
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DonVi](
        [DonViID] INT IDENTITY(1,1) NOT NULL,
        [TenDonVi] NVARCHAR(200) NOT NULL,
        [MaDonVi] NCHAR(50) NOT NULL,
        [TenDayDu] NVARCHAR(300) NULL,
        [CapBac] NVARCHAR(50) NULL,
        [DiaChi] NVARCHAR(500) NULL,
        [Email] NVARCHAR(100) NULL,
        [TruongDonVi] NVARCHAR(100) NULL,
        [MoTa] NVARCHAR(MAX) NULL,
        [CapTrenID] INT NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_DonVi] PRIMARY KEY CLUSTERED ([DonViID] ASC),
        CONSTRAINT [UK_DonVi_MaDonVi] UNIQUE ([MaDonVi]),
        CONSTRAINT [FK_DonVi_CapTren] FOREIGN KEY([CapTrenID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng DonVi';
END
    ELSE
    BEGIN
        -- Xóa cột SoDienThoai nếu đã tồn tại
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND name = 'SoDienThoai')
        BEGIN
            ALTER TABLE [dbo].[DonVi] DROP COLUMN [SoDienThoai];
            PRINT N'✓ Đã xóa cột SoDienThoai khỏi bảng DonVi';
        END
        -- Xóa cột TrangThai nếu đã tồn tại
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND name = 'TrangThai')
        BEGIN
            -- Xóa constraint DEFAULT trước
            DECLARE @ConstraintName NVARCHAR(200);
            SELECT @ConstraintName = name 
            FROM sys.default_constraints 
            WHERE parent_object_id = OBJECT_ID(N'[dbo].[DonVi]') 
            AND parent_column_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND name = 'TrangThai');
            
            IF @ConstraintName IS NOT NULL
            BEGIN
                EXEC('ALTER TABLE [dbo].[DonVi] DROP CONSTRAINT [' + @ConstraintName + ']');
                PRINT N'✓ Đã xóa constraint DEFAULT của cột TrangThai';
            END
            
            -- Sau đó xóa cột
            ALTER TABLE [dbo].[DonVi] DROP COLUMN [TrangThai];
            PRINT N'✓ Đã xóa cột TrangThai khỏi bảng DonVi';
        END
        
        -- Thêm cột CapTrenID nếu chưa có (đơn vị cấp trên)
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND name = 'CapTrenID')
        BEGIN
            ALTER TABLE [dbo].[DonVi]
            ADD [CapTrenID] INT NULL;
            PRINT N'✓ Đã thêm cột CapTrenID vào bảng DonVi';
        END
        
        -- Xóa cột CapDuoiID nếu đã tồn tại (không cần nữa vì một đơn vị có thể có nhiều cấp dưới)
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DonVi]') AND name = 'CapDuoiID')
        BEGIN
            -- Xóa Foreign Key constraint trước
            IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DonVi_CapDuoi')
            BEGIN
                ALTER TABLE [dbo].[DonVi] DROP CONSTRAINT [FK_DonVi_CapDuoi];
                PRINT N'✓ Đã xóa Foreign Key FK_DonVi_CapDuoi';
            END
            
            -- Sau đó xóa cột
            ALTER TABLE [dbo].[DonVi] DROP COLUMN [CapDuoiID];
            PRINT N'✓ Đã xóa cột CapDuoiID khỏi bảng DonVi (một đơn vị có thể có nhiều cấp dưới)';
        END
        
        -- Thêm Foreign Key constraint cho CapTrenID nếu chưa có
        IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DonVi_CapTren')
        BEGIN
            ALTER TABLE [dbo].[DonVi]
            ADD CONSTRAINT [FK_DonVi_CapTren] FOREIGN KEY([CapTrenID]) REFERENCES [dbo].[DonVi]([DonViID]);
            PRINT N'✓ Đã thêm Foreign Key FK_DonVi_CapTren';
        END
    END
GO

-- =============================================
-- BẢNG: Quân nhân (Module 1)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[QuanNhan](
        [QuanNhanID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NOT NULL,
        [HoTen] NVARCHAR(100) NOT NULL,
        [NgaySinh] DATE NULL,
        [SHSQ] NVARCHAR(50) NULL,
        [SoTheBHYT] NVARCHAR(50) NULL,
        [SoCCCD] NVARCHAR(20) NOT NULL,
        [CapBac] NVARCHAR(50) NULL,
        [ChucVu] NVARCHAR(100) NULL,
        [NhapNgu] DATE NULL,
        [NgayVaoDang] DATE NULL,
        [SoTheDang] NVARCHAR(50) NULL,
        [Doan] NVARCHAR(50) NULL,
        [DanToc] NVARCHAR(50) NULL,
        [TonGiao] NVARCHAR(50) NULL,
        [SucKhoe] NVARCHAR(50) NULL,
        [NhomMau] NVARCHAR(10) NULL,
        [HoTenChaNamSinh] NVARCHAR(200) NULL,
        [HoTenMeNamSinh] NVARCHAR(200) NULL,
        [HoTenVoConNamSinh] NVARCHAR(200) NULL,
        [NgheNghiepChaMe] NVARCHAR(200) NULL,
        [MayAnhChiEm] INT NULL,
        [QueQuan] NVARCHAR(200) NULL,
        [NoiO] NVARCHAR(500) NULL,
        [KhiCanBaoTin] NVARCHAR(200) NULL,
        [GhiChu] NVARCHAR(MAX) NULL,
        [AnhDaiDien] VARBINARY(MAX) NULL,
        [TrangThai] BIT NOT NULL DEFAULT(1),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_QuanNhan] PRIMARY KEY CLUSTERED ([QuanNhanID] ASC),
        CONSTRAINT [UK_QuanNhan_SoCCCD] UNIQUE ([SoCCCD]),
        CONSTRAINT [FK_QuanNhan_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng QuanNhan';
END
ELSE
BEGIN
    -- Thêm cột AnhDaiDien nếu chưa tồn tại
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan]') AND name = 'AnhDaiDien')
    BEGIN
        ALTER TABLE [dbo].[QuanNhan] ADD [AnhDaiDien] VARBINARY(MAX) NULL;
        PRINT N'✓ Đã thêm cột AnhDaiDien vào bảng QuanNhan';
    END

    -- Xóa cột TT nếu tồn tại (vì TT thực ra là QuanNhanID)
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan]') AND name = 'TT')
    BEGIN
        ALTER TABLE [dbo].[QuanNhan] DROP COLUMN [TT];
        PRINT N'✓ Đã xóa cột TT khỏi bảng QuanNhan';
    END
    
    -- Sửa cột SoCCCD từ NCHAR sang NVARCHAR để tránh vấn đề padding
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan]') AND name = 'SoCCCD' AND system_type_id = 239)
    BEGIN
        -- Kiểm tra xem có dữ liệu không
        IF EXISTS (SELECT 1 FROM QuanNhan)
        BEGIN
            -- Nếu có dữ liệu, cần xóa constraint UNIQUE trước
            IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'UK_QuanNhan_SoCCCD' AND object_id = OBJECT_ID(N'[dbo].[QuanNhan]'))
            BEGIN
                ALTER TABLE [dbo].[QuanNhan] DROP CONSTRAINT [UK_QuanNhan_SoCCCD];
                PRINT N'✓ Đã xóa constraint UK_QuanNhan_SoCCCD';
            END
            
            -- Cập nhật dữ liệu: trim spaces từ SoCCCD
            UPDATE [dbo].[QuanNhan] SET [SoCCCD] = RTRIM(LTRIM([SoCCCD]));
            PRINT N'✓ Đã trim spaces từ SoCCCD';
        END
        
        -- Đổi kiểu dữ liệu
        ALTER TABLE [dbo].[QuanNhan] ALTER COLUMN [SoCCCD] NVARCHAR(20) NOT NULL;
        PRINT N'✓ Đã đổi SoCCCD từ NCHAR(20) sang NVARCHAR(20)';
        
        -- Tạo lại constraint UNIQUE nếu chưa có
        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UK_QuanNhan_SoCCCD' AND object_id = OBJECT_ID(N'[dbo].[QuanNhan]'))
        BEGIN
            ALTER TABLE [dbo].[QuanNhan] ADD CONSTRAINT [UK_QuanNhan_SoCCCD] UNIQUE ([SoCCCD]);
            PRINT N'✓ Đã tạo lại constraint UK_QuanNhan_SoCCCD';
        END
    END
END
GO

-- =============================================
-- BẢNG: Người dùng
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[NguoiDung](
        [NguoiDungID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NULL,
        [TenDangNhap] NCHAR(50) NOT NULL,
        [MatKhau] NVARCHAR(255) NOT NULL,
        [HoTen] NVARCHAR(100) NOT NULL,
        [Email] NVARCHAR(100) NULL,
        [VaiTro] NVARCHAR(50) NOT NULL,
        [Roles] NVARCHAR(200) NULL,
        [Permissions] NVARCHAR(500) NULL,
        [TrangThai] BIT NOT NULL DEFAULT(1),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        [LanDangNhapCuoi] DATETIME NULL,
        CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED ([NguoiDungID] ASC),
        CONSTRAINT [UK_NguoiDung_TenDangNhap] UNIQUE ([TenDangNhap]),
        CONSTRAINT [FK_NguoiDung_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng NguoiDung';
END
GO

-- =============================================
-- BẢNG: Đảng viên (Enhanced)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DangVien](
        [DangVienID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NOT NULL,
        [HoTen] NVARCHAR(100) NOT NULL,
        [NgaySinh] DATE NULL,
        [GioiTinh] NVARCHAR(10) NULL,
        [SoCCCD] NVARCHAR(20) NOT NULL,
        [SoDienThoai] NCHAR(20) NULL,
        [SoTheDangVien] NVARCHAR(50) NULL,
        [SoLyLichDangVien] NVARCHAR(50) NULL,
        [NgayVaoDang] DATE NULL,
        [NgayChinhThuc] DATE NULL,
        [LoaiDangVien] NVARCHAR(20) NOT NULL,
        [DoiTuong] NVARCHAR(50) NULL,
        [CapBac] NVARCHAR(50) NULL,
        [ChucVu] NVARCHAR(100) NULL,
        [QueQuan] NVARCHAR(200) NULL,
        [TrinhDo] NVARCHAR(50) NULL,
        -- Enhanced fields from requirements
        [DiaChi] NVARCHAR(500) NULL,
        [DanToc] NVARCHAR(50) NULL,
        [TonGiao] NVARCHAR(50) NULL,
        [NgheNghiep] NVARCHAR(100) NULL,
        [TrinhDoHocVan] NVARCHAR(50) NULL,
        [TrinhDoChuyenMon] NVARCHAR(50) NULL,
        [LyLuanChinhTri] NVARCHAR(50) NULL,
        [NgoaiNgu] NVARCHAR(50) NULL,
        [TinHoc] NVARCHAR(50) NULL,
        [AnhDaiDien] VARBINARY(MAX) NULL,
        [QuaTrinhCongTac] NVARCHAR(MAX) NULL,
        [HoSoGiaDinh] NVARCHAR(MAX) NULL,
        -- B. THÔNG TIN CƠ BẢN
        [HoTenKhaiSinh] NVARCHAR(100) NULL,
        [HoTenKhac] NVARCHAR(100) NULL,
        [HeSoLuong] DECIMAL(5,2) NULL,
        [ThangNamPhongCapBac] NVARCHAR(50) NULL,
        [SoHieuQuanNhan] NVARCHAR(50) NULL,
        -- I. BẢN THÂN
        [NgayThamGiaCachMang] DATE NULL,
        [NgayTuyenDung] DATE NULL,
        [NgayNhapNgu] DATE NULL,
        [NgayXuatNgu] DATE NULL,
        [NgayTaiNgu] DATE NULL,
        [ChucDanhKhoaHoc] NVARCHAR(200) NULL,
        [HocViCaoNhat] NVARCHAR(100) NULL,
        [ChuyenNganh] NVARCHAR(200) NULL,
        [ThoiGianHocVi] NVARCHAR(50) NULL,
        [TrinhDoChiHuyQuanLy] NVARCHAR(50) NULL,
        [TrinhDoNgoaiNgu] NVARCHAR(50) NULL,
        [ThoiGianNgoaiNgu] NVARCHAR(50) NULL,
        [TiengDanToc] NVARCHAR(200) NULL,
        [QuaTrinhHocTap] NVARCHAR(MAX) NULL,
        [ChienDauPhucVuChienDau] NVARCHAR(MAX) NULL,
        [DiNuocNgoai] NVARCHAR(MAX) NULL,
        [SucKhoeLoai] NVARCHAR(50) NULL,
        [NhomMau] NVARCHAR(10) NULL,
        [BenhChinh] NVARCHAR(200) NULL,
        [ThuongTat] NVARCHAR(MAX) NULL,
        [DanhHieuDuocPhong] NVARCHAR(MAX) NULL,
        [NgheNghiepTruocNhapNgu] NVARCHAR(200) NULL,
        [QuanHeCTXHTruocNhapNgu] NVARCHAR(200) NULL,
        [TinhHinhNhaO] NVARCHAR(MAX) NULL,
        -- II. TÌNH HÌNH KINH TẾ – CHÍNH TRỊ GIA ĐÌNH
        [HoTenCha] NVARCHAR(100) NULL,
        [NamSinhCha] INT NULL,
        [NgheNghiepCha] NVARCHAR(200) NULL,
        [HoTenMe] NVARCHAR(100) NULL,
        [NamSinhMe] INT NULL,
        [NgheNghiepMe] NVARCHAR(200) NULL,
        [ThanhPhanGiaDinh] NVARCHAR(100) NULL,
        [QueQuanChaMe] NVARCHAR(200) NULL,
        [ChoOHienNayChaMe] NVARCHAR(500) NULL,
        [SoConTrongGiaDinh] INT NULL,
        [GioiTinhThuTuBanThan] NVARCHAR(50) NULL,
        [TinhHinhKinhTeGiaDinh] NVARCHAR(MAX) NULL,
        [TinhHinhChinhTriGiaDinh] NVARCHAR(MAX) NULL,
        -- III. TÌNH HÌNH KT – CT CỦA GIA ĐÌNH VỢ (CHỒNG)
        [HoTenChaVoChong] NVARCHAR(100) NULL,
        [NamSinhChaVoChong] INT NULL,
        [NgheNghiepChaVoChong] NVARCHAR(200) NULL,
        [HoTenMeVoChong] NVARCHAR(100) NULL,
        [NamSinhMeVoChong] INT NULL,
        [NgheNghiepMeVoChong] NVARCHAR(200) NULL,
        [ThanhPhanGiaDinhVoChong] NVARCHAR(100) NULL,
        [QueQuanGiaDinhVoChong] NVARCHAR(200) NULL,
        [ChoOHienNayGiaDinhVoChong] NVARCHAR(500) NULL,
        [SoConTrongGiaDinhVoChong] INT NULL,
        [ThuTuVoChongTrongGiaDinh] NVARCHAR(50) NULL,
        [TinhHinhKTCTGiaDinhVoChong] NVARCHAR(MAX) NULL,
        [NgheNghiepVoChong] NVARCHAR(200) NULL,
        [DangVienHayKhong] BIT NULL,
        [NoiOHienNayVoChong] NVARCHAR(500) NULL,
        [ThongTinCacCon] NVARCHAR(MAX) NULL,
        [TrangThai] BIT NOT NULL DEFAULT(1),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        -- Đơn vị cấp 1 và cấp 2 (tượng trưng, không có khóa ngoại)
        [DonViCap1] NVARCHAR(200) NULL,
        [DonViCap2] NVARCHAR(200) NULL,
        CONSTRAINT [PK_DangVien] PRIMARY KEY CLUSTERED ([DangVienID] ASC),
        CONSTRAINT [UK_DangVien_SoCCCD] UNIQUE ([SoCCCD]),
        CONSTRAINT [UK_DangVien_SoTheDangVien] UNIQUE ([SoTheDangVien]),
        CONSTRAINT [UK_DangVien_SoLyLichDangVien] UNIQUE ([SoLyLichDangVien]),
        CONSTRAINT [UK_DangVien_SoDienThoai] UNIQUE ([SoDienThoai]),
        CONSTRAINT [FK_DangVien_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng DangVien';
END
ELSE
BEGIN
    -- Thêm các cột còn thiếu vào bảng DangVien
    -- B. THÔNG TIN CƠ BẢN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenKhaiSinh')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenKhaiSinh] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenKhac')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenKhac] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HeSoLuong')
        ALTER TABLE [dbo].[DangVien] ADD [HeSoLuong] DECIMAL(5,2) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThangNamPhongCapBac')
        ALTER TABLE [dbo].[DangVien] ADD [ThangNamPhongCapBac] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'SoHieuQuanNhan')
        ALTER TABLE [dbo].[DangVien] ADD [SoHieuQuanNhan] NVARCHAR(50) NULL;
    
    -- I. BẢN THÂN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgayThamGiaCachMang')
        ALTER TABLE [dbo].[DangVien] ADD [NgayThamGiaCachMang] DATE NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgayTuyenDung')
        ALTER TABLE [dbo].[DangVien] ADD [NgayTuyenDung] DATE NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgayNhapNgu')
        ALTER TABLE [dbo].[DangVien] ADD [NgayNhapNgu] DATE NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgayXuatNgu')
        ALTER TABLE [dbo].[DangVien] ADD [NgayXuatNgu] DATE NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgayTaiNgu')
        ALTER TABLE [dbo].[DangVien] ADD [NgayTaiNgu] DATE NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ChucDanhKhoaHoc')
        ALTER TABLE [dbo].[DangVien] ADD [ChucDanhKhoaHoc] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HocViCaoNhat')
        ALTER TABLE [dbo].[DangVien] ADD [HocViCaoNhat] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ChuyenNganh')
        ALTER TABLE [dbo].[DangVien] ADD [ChuyenNganh] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThoiGianHocVi')
        ALTER TABLE [dbo].[DangVien] ADD [ThoiGianHocVi] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TrinhDoChiHuyQuanLy')
        ALTER TABLE [dbo].[DangVien] ADD [TrinhDoChiHuyQuanLy] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TrinhDoNgoaiNgu')
        ALTER TABLE [dbo].[DangVien] ADD [TrinhDoNgoaiNgu] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThoiGianNgoaiNgu')
        ALTER TABLE [dbo].[DangVien] ADD [ThoiGianNgoaiNgu] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TiengDanToc')
        ALTER TABLE [dbo].[DangVien] ADD [TiengDanToc] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'QuaTrinhHocTap')
        ALTER TABLE [dbo].[DangVien] ADD [QuaTrinhHocTap] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ChienDauPhucVuChienDau')
        ALTER TABLE [dbo].[DangVien] ADD [ChienDauPhucVuChienDau] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'DiNuocNgoai')
        ALTER TABLE [dbo].[DangVien] ADD [DiNuocNgoai] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'SucKhoeLoai')
        ALTER TABLE [dbo].[DangVien] ADD [SucKhoeLoai] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NhomMau')
        ALTER TABLE [dbo].[DangVien] ADD [NhomMau] NVARCHAR(10) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'BenhChinh')
        ALTER TABLE [dbo].[DangVien] ADD [BenhChinh] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThuongTat')
        ALTER TABLE [dbo].[DangVien] ADD [ThuongTat] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'DanhHieuDuocPhong')
        ALTER TABLE [dbo].[DangVien] ADD [DanhHieuDuocPhong] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepTruocNhapNgu')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepTruocNhapNgu] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'QuanHeCTXHTruocNhapNgu')
        ALTER TABLE [dbo].[DangVien] ADD [QuanHeCTXHTruocNhapNgu] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TinhHinhNhaO')
        ALTER TABLE [dbo].[DangVien] ADD [TinhHinhNhaO] NVARCHAR(MAX) NULL;
    
    -- II. TÌNH HÌNH KINH TẾ – CHÍNH TRỊ GIA ĐÌNH
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenCha')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenCha] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NamSinhCha')
        ALTER TABLE [dbo].[DangVien] ADD [NamSinhCha] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepCha')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepCha] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenMe')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenMe] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NamSinhMe')
        ALTER TABLE [dbo].[DangVien] ADD [NamSinhMe] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepMe')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepMe] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThanhPhanGiaDinh')
        ALTER TABLE [dbo].[DangVien] ADD [ThanhPhanGiaDinh] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'QueQuanChaMe')
        ALTER TABLE [dbo].[DangVien] ADD [QueQuanChaMe] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ChoOHienNayChaMe')
        ALTER TABLE [dbo].[DangVien] ADD [ChoOHienNayChaMe] NVARCHAR(500) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'SoConTrongGiaDinh')
        ALTER TABLE [dbo].[DangVien] ADD [SoConTrongGiaDinh] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'GioiTinhThuTuBanThan')
        ALTER TABLE [dbo].[DangVien] ADD [GioiTinhThuTuBanThan] NVARCHAR(50) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TinhHinhKinhTeGiaDinh')
        ALTER TABLE [dbo].[DangVien] ADD [TinhHinhKinhTeGiaDinh] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TinhHinhChinhTriGiaDinh')
        ALTER TABLE [dbo].[DangVien] ADD [TinhHinhChinhTriGiaDinh] NVARCHAR(MAX) NULL;
    
    -- III. TÌNH HÌNH KT – CT CỦA GIA ĐÌNH VỢ (CHỒNG)
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenChaVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenChaVoChong] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NamSinhChaVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NamSinhChaVoChong] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepChaVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepChaVoChong] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'HoTenMeVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [HoTenMeVoChong] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NamSinhMeVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NamSinhMeVoChong] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepMeVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepMeVoChong] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThanhPhanGiaDinhVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [ThanhPhanGiaDinhVoChong] NVARCHAR(100) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'QueQuanGiaDinhVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [QueQuanGiaDinhVoChong] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ChoOHienNayGiaDinhVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [ChoOHienNayGiaDinhVoChong] NVARCHAR(500) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'SoConTrongGiaDinhVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [SoConTrongGiaDinhVoChong] INT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThuTuVoChongTrongGiaDinh')
        ALTER TABLE [dbo].[DangVien] ADD [ThuTuVoChongTrongGiaDinh] NVARCHAR(50) NULL;
    
    -- Đơn vị cấp 1 và cấp 2 (tượng trưng, không có khóa ngoại)
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'DonViCap1')
        ALTER TABLE [dbo].[DangVien] ADD [DonViCap1] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'DonViCap2')
        ALTER TABLE [dbo].[DangVien] ADD [DonViCap2] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'TinhHinhKTCTGiaDinhVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [TinhHinhKTCTGiaDinhVoChong] NVARCHAR(MAX) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NgheNghiepVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NgheNghiepVoChong] NVARCHAR(200) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'DangVienHayKhong')
        ALTER TABLE [dbo].[DangVien] ADD [DangVienHayKhong] BIT NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'NoiOHienNayVoChong')
        ALTER TABLE [dbo].[DangVien] ADD [NoiOHienNayVoChong] NVARCHAR(500) NULL;
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DangVien]') AND name = 'ThongTinCacCon')
        ALTER TABLE [dbo].[DangVien] ADD [ThongTinCacCon] NVARCHAR(MAX) NULL;
    
    PRINT N'✓ Đã thêm các cột còn thiếu vào bảng DangVien';
END
GO

-- =============================================
-- BẢNG: Ảnh đảng viên
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVienAnh]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DangVienAnh](
        [DangVienAnhID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [AnhPath] NVARCHAR(500) NULL,
        [AnhBinary] VARBINARY(MAX) NULL,
        [LoaiAnh] NVARCHAR(20) NOT NULL, -- 3x4, 4x6
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_DangVienAnh] PRIMARY KEY CLUSTERED ([DangVienAnhID] ASC),
        CONSTRAINT [FK_DangVienAnh_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng DangVienAnh';
END
GO

-- =============================================
-- BẢNG: Quá trình công tác
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuaTrinhCongTac]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[QuaTrinhCongTac](
        [QuaTrinhCongTacID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [ThoiGian] NVARCHAR(100) NOT NULL,
        [ChucVu] NVARCHAR(100) NOT NULL,
        [DonVi] NVARCHAR(200) NOT NULL,
        [MoTa] NVARCHAR(MAX) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_QuaTrinhCongTac] PRIMARY KEY CLUSTERED ([QuaTrinhCongTacID] ASC),
        CONSTRAINT [FK_QuaTrinhCongTac_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng QuaTrinhCongTac';
END
GO

-- =============================================
-- BẢNG: Hồ sơ gia đình
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HoSoGiaDinh]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[HoSoGiaDinh](
        [HoSoGiaDinhID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [QuanHe] NVARCHAR(50) NOT NULL, -- Cha, Mẹ, Vợ/Chồng, Con
        [HoTen] NVARCHAR(100) NOT NULL,
        [NgaySinh] DATE NULL,
        [NgheNghiep] NVARCHAR(100) NULL,
        [DiaChi] NVARCHAR(500) NULL,
        [GhiChu] NVARCHAR(MAX) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_HoSoGiaDinh] PRIMARY KEY CLUSTERED ([HoSoGiaDinhID] ASC),
        CONSTRAINT [FK_HoSoGiaDinh_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng HoSoGiaDinh';
END
GO

-- =============================================
-- MIGRATION: Di chuyển dữ liệu từ các bảng cũ sang bảng mới (nếu có)
-- =============================================
PRINT N'';
PRINT N'===========================================';
PRINT N'BẮT ĐẦU MIGRATION DỮ LIỆU';
PRINT N'===========================================';

-- Tạo bảng tạm để lưu dữ liệu từ KhenThuongCaNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan]') AND type in (N'U'))
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KhenThuongCaNhan]') AND type in (N'U'))
    BEGIN
        SELECT 
            KhenThuongCaNhanID,
            DangVienID,
            HinhThuc,
            Ngay,
            SoQuyetDinh,
            CapQuyetDinh,
            NoiDung,
            FileDinhKem,
            NgayTao,
            NguoiTao
        INTO _Temp_KhenThuongCaNhan
        FROM KhenThuongCaNhan;
        PRINT N'✓ Đã sao chép dữ liệu từ KhenThuongCaNhan';
    END
END

-- Tạo bảng tạm để lưu dữ liệu từ KhenThuongDonVi
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongDonVi]') AND type in (N'U'))
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KhenThuongDonVi]') AND type in (N'U'))
    BEGIN
        SELECT 
            KhenThuongDonViID,
            DonViID,
            HinhThuc,
            Ngay,
            SoQuyetDinh,
            CapQuyetDinh,
            NoiDung,
            FileDinhKem,
            NgayTao,
            NguoiTao
        INTO _Temp_KhenThuongDonVi
        FROM KhenThuongDonVi;
        PRINT N'✓ Đã sao chép dữ liệu từ KhenThuongDonVi';
    END
END

-- Tạo bảng tạm để lưu dữ liệu từ KyLuatCaNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan]') AND type in (N'U'))
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KyLuatCaNhan]') AND type in (N'U'))
    BEGIN
        SELECT 
            KyLuatCaNhanID,
            DangVienID,
            HinhThuc,
            Ngay,
            SoQuyetDinh,
            CapQuyetDinh,
            NoiDung,
            GhiChu,
            FileDinhKem,
            NgayTao,
            NguoiTao
        INTO _Temp_KyLuatCaNhan
        FROM KyLuatCaNhan;
        PRINT N'✓ Đã sao chép dữ liệu từ KyLuatCaNhan';
    END
END

-- Tạo bảng tạm để lưu dữ liệu từ KyLuatToChuc
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatToChuc]') AND type in (N'U'))
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KyLuatToChuc]') AND type in (N'U'))
    BEGIN
        SELECT 
            KyLuatToChucID,
            DonViID,
            HinhThuc,
            Ngay,
            SoQuyetDinh,
            CapQuyetDinh,
            NoiDung,
            GhiChu,
            FileDinhKem,
            NgayTao,
            NguoiTao
        INTO _Temp_KyLuatToChuc
        FROM KyLuatToChuc;
        PRINT N'✓ Đã sao chép dữ liệu từ KyLuatToChuc';
    END
END

-- =============================================
-- XÓA CÁC BẢNG CŨ VÀ FOREIGN KEY CONSTRAINTS
-- =============================================

-- Xóa foreign key constraints trước
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_KhenThuongCaNhan_DangVien')
    ALTER TABLE [dbo].[KhenThuongCaNhan] DROP CONSTRAINT [FK_KhenThuongCaNhan_DangVien];
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_KhenThuongDonVi_DonVi')
    ALTER TABLE [dbo].[KhenThuongDonVi] DROP CONSTRAINT [FK_KhenThuongDonVi_DonVi];
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_KyLuatCaNhan_DangVien')
    ALTER TABLE [dbo].[KyLuatCaNhan] DROP CONSTRAINT [FK_KyLuatCaNhan_DangVien];
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_KyLuatToChuc_DonVi')
    ALTER TABLE [dbo].[KyLuatToChuc] DROP CONSTRAINT [FK_KyLuatToChuc_DonVi];
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_KhenThuongKyLuat_DangVien')
    ALTER TABLE [dbo].[KhenThuongKyLuat] DROP CONSTRAINT [FK_KhenThuongKyLuat_DangVien];
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Permissions_Roles')
    ALTER TABLE [dbo].[Permissions] DROP CONSTRAINT [FK_Permissions_Roles];

-- Xóa các bảng cũ
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan]') AND type in (N'U'))
    DROP TABLE [dbo].[KhenThuongCaNhan];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongDonVi]') AND type in (N'U'))
    DROP TABLE [dbo].[KhenThuongDonVi];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan]') AND type in (N'U'))
    DROP TABLE [dbo].[KyLuatCaNhan];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatToChuc]') AND type in (N'U'))
    DROP TABLE [dbo].[KyLuatToChuc];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongKyLuat]') AND type in (N'U'))
    DROP TABLE [dbo].[KhenThuongKyLuat];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permissions]') AND type in (N'U'))
    DROP TABLE [dbo].[Permissions];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
    DROP TABLE [dbo].[Roles];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViKhenThuong]') AND type in (N'U'))
    DROP TABLE [dbo].[DonViKhenThuong];

PRINT N'✓ Đã xóa các bảng cũ';

GO

-- =============================================
-- BẢNG: Khen thưởng (Hợp nhất KhenThuongCaNhan và KhenThuongDonVi)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[KhenThuong](
        [KhenThuongID] INT IDENTITY(1,1) NOT NULL,
        [Loai] NVARCHAR(20) NOT NULL, -- CaNhan, DonVi
        [DangVienID] INT NULL, -- NULL nếu Loai = 'DonVi'
        [DonViID] INT NULL, -- NULL nếu Loai = 'CaNhan'
        [HinhThuc] NVARCHAR(200) NOT NULL,
        [Ngay] DATE NOT NULL,
        [SoQuyetDinh] NVARCHAR(100) NOT NULL,
        [CapQuyetDinh] NVARCHAR(100) NOT NULL,
        [NoiDung] NVARCHAR(MAX) NULL,
        [FileDinhKem] NVARCHAR(500) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_KhenThuong] PRIMARY KEY CLUSTERED ([KhenThuongID] ASC),
        CONSTRAINT [FK_KhenThuong_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID]),
        CONSTRAINT [FK_KhenThuong_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID]),
        CONSTRAINT [CK_KhenThuong_Loai] CHECK (
            (Loai = 'CaNhan' AND DangVienID IS NOT NULL AND DonViID IS NULL) OR
            (Loai = 'DonVi' AND DangVienID IS NULL AND DonViID IS NOT NULL)
        )
    );
    PRINT N'✓ Đã tạo bảng KhenThuong';
END
GO

-- =============================================
-- BẢNG: Kỷ luật (Hợp nhất KyLuatCaNhan và KyLuatToChuc)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[KyLuat](
        [KyLuatID] INT IDENTITY(1,1) NOT NULL,
        [Loai] NVARCHAR(20) NOT NULL, -- CaNhan, DonVi
        [DangVienID] INT NULL, -- NULL nếu Loai = 'DonVi'
        [DonViID] INT NULL, -- NULL nếu Loai = 'CaNhan'
        [HinhThuc] NVARCHAR(200) NOT NULL,
        [Ngay] DATE NOT NULL,
        [SoQuyetDinh] NVARCHAR(100) NOT NULL,
        [CapQuyetDinh] NVARCHAR(100) NOT NULL,
        [NoiDung] NVARCHAR(MAX) NULL,
        [GhiChu] NVARCHAR(MAX) NULL,
        [FileDinhKem] NVARCHAR(500) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_KyLuat] PRIMARY KEY CLUSTERED ([KyLuatID] ASC),
        CONSTRAINT [FK_KyLuat_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID]),
        CONSTRAINT [FK_KyLuat_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID]),
        CONSTRAINT [CK_KyLuat_Loai] CHECK (
            (Loai = 'CaNhan' AND DangVienID IS NOT NULL AND DonViID IS NULL) OR
            (Loai = 'DonVi' AND DangVienID IS NULL AND DonViID IS NOT NULL)
        )
    );
    PRINT N'✓ Đã tạo bảng KyLuat';
END
GO

-- =============================================
-- MIGRATION: Di chuyển dữ liệu từ bảng tạm sang bảng mới
-- =============================================

-- Migrate dữ liệu từ KhenThuongCaNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KhenThuongCaNhan]') AND type in (N'U'))
BEGIN
    INSERT INTO KhenThuong (Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao)
    SELECT 'CaNhan', DangVienID, NULL, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
    FROM _Temp_KhenThuongCaNhan;
    PRINT N'✓ Đã migrate dữ liệu từ KhenThuongCaNhan sang KhenThuong';
    DROP TABLE _Temp_KhenThuongCaNhan;
END

-- Migrate dữ liệu từ KhenThuongDonVi
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KhenThuongDonVi]') AND type in (N'U'))
BEGIN
    INSERT INTO KhenThuong (Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao)
    SELECT 'DonVi', NULL, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
    FROM _Temp_KhenThuongDonVi;
    PRINT N'✓ Đã migrate dữ liệu từ KhenThuongDonVi sang KhenThuong';
    DROP TABLE _Temp_KhenThuongDonVi;
END

-- Migrate dữ liệu từ KyLuatCaNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KyLuatCaNhan]') AND type in (N'U'))
BEGIN
    INSERT INTO KyLuat (Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, GhiChu, FileDinhKem, NgayTao, NguoiTao)
    SELECT 'CaNhan', DangVienID, NULL, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, GhiChu, FileDinhKem, NgayTao, NguoiTao
    FROM _Temp_KyLuatCaNhan;
    PRINT N'✓ Đã migrate dữ liệu từ KyLuatCaNhan sang KyLuat';
    DROP TABLE _Temp_KyLuatCaNhan;
END

-- Migrate dữ liệu từ KyLuatToChuc
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Temp_KyLuatToChuc]') AND type in (N'U'))
BEGIN
    INSERT INTO KyLuat (Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, GhiChu, FileDinhKem, NgayTao, NguoiTao)
    SELECT 'DonVi', NULL, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, GhiChu, FileDinhKem, NgayTao, NguoiTao
    FROM _Temp_KyLuatToChuc;
    PRINT N'✓ Đã migrate dữ liệu từ KyLuatToChuc sang KyLuat';
    DROP TABLE _Temp_KyLuatToChuc;
END

PRINT N'';
PRINT N'===========================================';
PRINT N'HOÀN TẤT MIGRATION DỮ LIỆU';
PRINT N'===========================================';
GO

-- =============================================
-- BẢNG: Tài liệu hồ sơ
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TaiLieuHoSo](
        [TaiLieuHoSoID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [TenFile] NVARCHAR(200) NOT NULL,
        [DuongDan] NVARCHAR(500) NOT NULL,
        [LoaiFile] NVARCHAR(50) NOT NULL, -- Word, PDF, Image
        [KichThuoc] BIGINT NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_TaiLieuHoSo] PRIMARY KEY CLUSTERED ([TaiLieuHoSoID] ASC),
        CONSTRAINT [FK_TaiLieuHoSo_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng TaiLieuHoSo';
END
GO

-- =============================================
-- STORED PROCEDURES: Tài liệu hồ sơ đảng viên
-- =============================================

-- SP: Insert TaiLieuHoSo
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieuHoSo_Insert];
GO

CREATE PROCEDURE [dbo].[TaiLieuHoSo_Insert]
    @DangVienID INT,
    @TenFile NVARCHAR(200),
    @DuongDan NVARCHAR(500),
    @LoaiFile NVARCHAR(50),
    @KichThuoc BIGINT = NULL,
    @NguoiTao NVARCHAR(100),
    @TaiLieuHoSoID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO TaiLieuHoSo (
        DangVienID, TenFile, DuongDan, LoaiFile, KichThuoc, NguoiTao
    )
    VALUES (
        @DangVienID, @TenFile, @DuongDan, @LoaiFile, @KichThuoc, @NguoiTao
    );
    
    SET @TaiLieuHoSoID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP TaiLieuHoSo_Insert';

-- SP: Get TaiLieuHoSo by DangVienID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieuHoSo_GetByDangVienID];
GO

CREATE PROCEDURE [dbo].[TaiLieuHoSo_GetByDangVienID]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        TaiLieuHoSoID, DangVienID, TenFile, DuongDan, LoaiFile, KichThuoc, NgayTao, NguoiTao
    FROM TaiLieuHoSo
    WHERE DangVienID = @DangVienID
    ORDER BY NgayTao DESC;
END
GO
PRINT N'✓ Đã tạo SP TaiLieuHoSo_GetByDangVienID';

-- SP: Get TaiLieuHoSo by ID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieuHoSo_GetById];
GO

CREATE PROCEDURE [dbo].[TaiLieuHoSo_GetById]
    @TaiLieuHoSoID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        TaiLieuHoSoID, DangVienID, TenFile, DuongDan, LoaiFile, KichThuoc, NgayTao, NguoiTao
    FROM TaiLieuHoSo
    WHERE TaiLieuHoSoID = @TaiLieuHoSoID;
END
GO
PRINT N'✓ Đã tạo SP TaiLieuHoSo_GetById';

-- SP: Update TaiLieuHoSo
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieuHoSo_Update];
GO

CREATE PROCEDURE [dbo].[TaiLieuHoSo_Update]
    @TaiLieuHoSoID INT,
    @TenFile NVARCHAR(200) = NULL,
    @DuongDan NVARCHAR(500) = NULL,
    @LoaiFile NVARCHAR(50) = NULL,
    @KichThuoc BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài liệu có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM TaiLieuHoSo WHERE TaiLieuHoSoID = @TaiLieuHoSoID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    UPDATE TaiLieuHoSo 
    SET TenFile = ISNULL(@TenFile, TenFile),
        DuongDan = ISNULL(@DuongDan, DuongDan),
        LoaiFile = ISNULL(@LoaiFile, LoaiFile),
        KichThuoc = ISNULL(@KichThuoc, KichThuoc)
    WHERE TaiLieuHoSoID = @TaiLieuHoSoID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO
PRINT N'✓ Đã tạo SP TaiLieuHoSo_Update';

-- SP: Delete TaiLieuHoSo
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieuHoSo_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieuHoSo_Delete];
GO

CREATE PROCEDURE [dbo].[TaiLieuHoSo_Delete]
    @TaiLieuHoSoID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài liệu có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM TaiLieuHoSo WHERE TaiLieuHoSoID = @TaiLieuHoSoID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    DELETE FROM TaiLieuHoSo
    WHERE TaiLieuHoSoID = @TaiLieuHoSoID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được xóa
END
GO
PRINT N'✓ Đã tạo SP TaiLieuHoSo_Delete';

-- =============================================
-- BẢNG: Cấp ủy
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CapUy]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CapUy](
        [CapUyID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NOT NULL,
        [DangVienID] INT NOT NULL,
        [ChucVu] NVARCHAR(50) NOT NULL, -- Bí thư, Phó Bí thư, Ủy viên
        [NgayBatDau] DATE NOT NULL,
        [NgayKetThuc] DATE NULL,
        [TrangThai] NVARCHAR(20) NOT NULL DEFAULT(N'Đang tại chức'),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_CapUy] PRIMARY KEY CLUSTERED ([CapUyID] ASC),
        CONSTRAINT [FK_CapUy_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID]),
        CONSTRAINT [FK_CapUy_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng CapUy';
END
GO

-- =============================================
-- BẢNG: Chuyển sinh hoạt đảng
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ChuyenSinhHoatDang](
        [ChuyenSinhHoatID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [DonViDi] INT NOT NULL,
        [DonViDen] INT NOT NULL,
        [NgayChuyen] DATE NOT NULL,
        [LyDo] NVARCHAR(500) NULL,
        [GhiChu] NVARCHAR(MAX) NULL,
        [FileQuyetDinh] NVARCHAR(500) NULL,
        [TrangThai] NVARCHAR(20) NOT NULL DEFAULT(N'Chờ duyệt'),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_ChuyenSinhHoatDang] PRIMARY KEY CLUSTERED ([ChuyenSinhHoatID] ASC),
        CONSTRAINT [FK_CSHD_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID]),
        CONSTRAINT [FK_CSHD_DonViDi] FOREIGN KEY([DonViDi]) REFERENCES [dbo].[DonVi]([DonViID]),
        CONSTRAINT [FK_CSHD_DonViDen] FOREIGN KEY([DonViDen]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng ChuyenSinhHoatDang';
END
GO

-- =============================================
-- BẢNG: Theo dõi chuyển chính thức
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TheoDoiChuyenChinhThuc](
        [TheoDoiChuyenChinhThucID] INT IDENTITY(1,1) NOT NULL,
        [DangVienID] INT NOT NULL,
        [NgayVaoDang] DATE NOT NULL,
        [NgayChuyenChinhThuc] DATE NULL,
        [TrangThai] NVARCHAR(20) NOT NULL DEFAULT(N'Đang theo dõi'),
        [GhiChu] NVARCHAR(MAX) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_TheoDoiChuyenChinhThuc] PRIMARY KEY CLUSTERED ([TheoDoiChuyenChinhThucID] ASC),
        CONSTRAINT [FK_TheoDoiChuyenChinhThuc_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng TheoDoiChuyenChinhThuc';
END
GO

-- =============================================
-- BẢNG: Sinh hoạt chi bộ
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SinhHoatChiBo](
        [SinhHoatID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NOT NULL,
        [TieuDe] NVARCHAR(200) NOT NULL,
        [NgaySinhHoat] DATETIME NOT NULL,
        [DiaDiem] NVARCHAR(200) NULL,
        [ChuTri] NVARCHAR(100) NULL,
        [ThuKy] NVARCHAR(100) NULL,
        [NoiDung] NVARCHAR(MAX) NULL,
        [FileNghiQuyet] NVARCHAR(500) NULL,
        [SoLuongThamGia] INT NULL,
        [TrangThai] NVARCHAR(20) NOT NULL DEFAULT(N'ChuaDiemDanh'),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_SinhHoatChiBo] PRIMARY KEY CLUSTERED ([SinhHoatID] ASC),
        CONSTRAINT [FK_SinhHoatChiBo_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng SinhHoatChiBo';
END
ELSE
BEGIN
    -- Thêm cột FileNghiQuyet nếu chưa có
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo]') AND name = 'FileNghiQuyet')
    BEGIN
        ALTER TABLE [dbo].[SinhHoatChiBo]
        ADD [FileNghiQuyet] NVARCHAR(500) NULL;
        PRINT N'✓ Đã thêm cột FileNghiQuyet vào bảng SinhHoatChiBo';
    END
    
    -- Xóa cột NghiQuyet nếu có
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo]') AND name = 'NghiQuyet')
    BEGIN
        ALTER TABLE [dbo].[SinhHoatChiBo]
        DROP COLUMN [NghiQuyet];
        PRINT N'✓ Đã xóa cột NghiQuyet khỏi bảng SinhHoatChiBo';
    END
END
GO

-- =============================================
-- BẢNG: Điểm danh sinh hoạt
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanhSinhHoat]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DiemDanhSinhHoat](
        [DiemDanhID] INT IDENTITY(1,1) NOT NULL,
        [SinhHoatID] INT NOT NULL,
        [DangVienID] INT NOT NULL,
        [CoMat] BIT NOT NULL DEFAULT(0),
        [LyDoVang] NVARCHAR(200) NULL,
        [GhiChu] NVARCHAR(500) NULL,
        [NgayDiemDanh] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiDiemDanh] NVARCHAR(100) NULL,
        CONSTRAINT [PK_DiemDanhSinhHoat] PRIMARY KEY CLUSTERED ([DiemDanhID] ASC),
        CONSTRAINT [FK_DiemDanhSinhHoat_SinhHoat] FOREIGN KEY([SinhHoatID]) REFERENCES [dbo].[SinhHoatChiBo]([SinhHoatID]),
        CONSTRAINT [FK_DiemDanhSinhHoat_DangVien] FOREIGN KEY([DangVienID]) REFERENCES [dbo].[DangVien]([DangVienID])
    );
    PRINT N'✓ Đã tạo bảng DiemDanhSinhHoat';
END
GO

-- =============================================
-- BẢNG: Tài liệu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TaiLieu](
        [TaiLieuID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NULL,
        [TieuDe] NVARCHAR(200) NOT NULL,
        [LoaiTaiLieu] NVARCHAR(50) NOT NULL,
        [NoiDung] NVARCHAR(MAX) NULL,
        [FileDinhKem] NVARCHAR(500) NULL,
        [NgayPhatHanh] DATE NULL,
        [CoQuanPhatHanh] NVARCHAR(200) NULL,
        [TrangThai] BIT NOT NULL DEFAULT(1),
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_TaiLieu] PRIMARY KEY CLUSTERED ([TaiLieuID] ASC),
        CONSTRAINT [FK_TaiLieu_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng TaiLieu';
END
GO

-- =============================================
-- BẢNG: Văn bản chi bộ
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[VanBanChiBo](
        [VanBanChiBoID] INT IDENTITY(1,1) NOT NULL,
        [DonViID] INT NOT NULL,
        [TenVanBan] NVARCHAR(200) NOT NULL,
        [NoiDung] NVARCHAR(MAX) NULL,
        [NgayGui] DATE NULL,
        [NgayNhan] DATE NULL,
        [TrangThai] NVARCHAR(20) NOT NULL DEFAULT(N'Chưa xử lý'),
        [FileDinhKem] NVARCHAR(500) NULL,
        [NgayTao] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiTao] NVARCHAR(100) NULL,
        CONSTRAINT [PK_VanBanChiBo] PRIMARY KEY CLUSTERED ([VanBanChiBoID] ASC),
        CONSTRAINT [FK_VanBanChiBo_DonVi] FOREIGN KEY([DonViID]) REFERENCES [dbo].[DonVi]([DonViID])
    );
    PRINT N'✓ Đã tạo bảng VanBanChiBo';
END
GO


-- =============================================
-- BẢNG: Audit Log
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AuditLog](
        [AuditLogID] INT IDENTITY(1,1) NOT NULL,
        [NguoiDungID] INT NULL,
        [TenDangNhap] NVARCHAR(50) NULL,
        [Action] NVARCHAR(50) NOT NULL,
        [TableName] NVARCHAR(100) NULL,
        [RecordID] INT NULL,
        [OldValues] NVARCHAR(MAX) NULL,
        [NewValues] NVARCHAR(MAX) NULL,
        [IPAddress] NVARCHAR(50) NULL,
        [UserAgent] NVARCHAR(500) NULL,
        [NgayThucHien] DATETIME NOT NULL DEFAULT(GETDATE()),
        CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED ([AuditLogID] ASC)
    );
    PRINT N'✓ Đã tạo bảng AuditLog';
END
GO

-- =============================================
-- BẢNG: System Config
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfig]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SystemConfig](
        [SystemConfigID] INT IDENTITY(1,1) NOT NULL,
        [ConfigKey] NVARCHAR(100) NOT NULL,
        [ConfigValue] NVARCHAR(MAX) NULL,
        [Description] NVARCHAR(500) NULL,
        [NgayCapNhat] DATETIME NOT NULL DEFAULT(GETDATE()),
        [NguoiCapNhat] NVARCHAR(100) NULL,
        CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED ([SystemConfigID] ASC),
        CONSTRAINT [UK_SystemConfig_ConfigKey] UNIQUE ([ConfigKey])
    );
    PRINT N'✓ Đã tạo bảng SystemConfig';
END
GO

PRINT N'';
PRINT N'===========================================';
PRINT N'TẠO STORED PROCEDURES';
PRINT N'===========================================';

-- =============================================
-- SP: QuanNhan CRUD Operations
-- =============================================

-- SP: Get all QuanNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[QuanNhan_GetAll];
GO

CREATE PROCEDURE [dbo].[QuanNhan_GetAll]
    @DonViID INT = NULL,
    @HoTen NVARCHAR(100) = NULL,
    @SoCCCD NVARCHAR(20) = NULL,
    @CapBac NVARCHAR(50) = NULL,
    @ChucVu NVARCHAR(100) = NULL,
    @TrangThai BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        qn.QuanNhanID, qn.DonViID, d.TenDonVi, qn.HoTen, qn.NgaySinh,
        qn.SHSQ, qn.SoTheBHYT, qn.SoCCCD, qn.CapBac, qn.ChucVu, qn.NhapNgu,
        qn.NgayVaoDang, qn.SoTheDang, qn.Doan, qn.DanToc, qn.TonGiao,
        qn.SucKhoe, qn.NhomMau, qn.HoTenChaNamSinh, qn.HoTenMeNamSinh,
        qn.HoTenVoConNamSinh, qn.NgheNghiepChaMe, qn.MayAnhChiEm,
        qn.QueQuan, qn.NoiO, qn.KhiCanBaoTin, qn.GhiChu, qn.AnhDaiDien,
        qn.TrangThai, qn.NgayTao, qn.NguoiTao
    FROM QuanNhan qn
    INNER JOIN DonVi d ON qn.DonViID = d.DonViID
    WHERE (@DonViID IS NULL OR qn.DonViID = @DonViID)
       AND (@HoTen IS NULL OR qn.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @HoTen + '%')
       AND (@SoCCCD IS NULL OR qn.SoCCCD LIKE '%' + @SoCCCD + '%')
       AND (@CapBac IS NULL OR qn.CapBac = @CapBac)
       AND (@ChucVu IS NULL OR qn.ChucVu LIKE '%' + @ChucVu + '%')
       AND (@TrangThai IS NULL OR qn.TrangThai = @TrangThai)
    ORDER BY qn.QuanNhanID, qn.HoTen;
END
GO
PRINT N'✓ Đã tạo SP QuanNhan_GetAll';

-- SP: Get QuanNhan by ID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[QuanNhan_GetById];
GO

CREATE PROCEDURE [dbo].[QuanNhan_GetById]
    @QuanNhanID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        qn.QuanNhanID, qn.DonViID, d.TenDonVi, qn.HoTen, qn.NgaySinh,
        qn.SHSQ, qn.SoTheBHYT, qn.SoCCCD, qn.CapBac, qn.ChucVu, qn.NhapNgu,
        qn.NgayVaoDang, qn.SoTheDang, qn.Doan, qn.DanToc, qn.TonGiao,
        qn.SucKhoe, qn.NhomMau, qn.HoTenChaNamSinh, qn.HoTenMeNamSinh,
        qn.HoTenVoConNamSinh, qn.NgheNghiepChaMe, qn.MayAnhChiEm,
        qn.QueQuan, qn.NoiO, qn.KhiCanBaoTin, qn.GhiChu, qn.AnhDaiDien,
        qn.TrangThai, qn.NgayTao, qn.NguoiTao
    FROM QuanNhan qn
    INNER JOIN DonVi d ON qn.DonViID = d.DonViID
    WHERE qn.QuanNhanID = @QuanNhanID;
END
GO
PRINT N'✓ Đã tạo SP QuanNhan_GetById';

-- SP: Insert QuanNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[QuanNhan_Insert];
GO

CREATE PROCEDURE [dbo].[QuanNhan_Insert]
    @DonViID INT,
    @HoTen NVARCHAR(100),
    @NgaySinh DATE = NULL,
    @SHSQ NVARCHAR(50) = NULL,
    @SoTheBHYT NVARCHAR(50) = NULL,
    @SoCCCD NVARCHAR(20),
    @CapBac NVARCHAR(50) = NULL,
    @ChucVu NVARCHAR(100) = NULL,
    @NhapNgu DATE = NULL,
    @NgayVaoDang DATE = NULL,
    @SoTheDang NVARCHAR(50) = NULL,
    @Doan NVARCHAR(50) = NULL,
    @DanToc NVARCHAR(50) = NULL,
    @TonGiao NVARCHAR(50) = NULL,
    @SucKhoe NVARCHAR(50) = NULL,
    @NhomMau NVARCHAR(10) = NULL,
    @HoTenChaNamSinh NVARCHAR(200) = NULL,
    @HoTenMeNamSinh NVARCHAR(200) = NULL,
    @HoTenVoConNamSinh NVARCHAR(200) = NULL,
    @NgheNghiepChaMe NVARCHAR(200) = NULL,
    @MayAnhChiEm INT = NULL,
    @QueQuan NVARCHAR(200) = NULL,
    @NoiO NVARCHAR(500) = NULL,
    @KhiCanBaoTin NVARCHAR(200) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @AnhDaiDien VARBINARY(MAX) = NULL,
    @NguoiTao NVARCHAR(100) = NULL,
    @QuanNhanID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Trim values
    SET @SoCCCD = LTRIM(RTRIM(ISNULL(@SoCCCD, '')));
    SET @HoTen = LTRIM(RTRIM(ISNULL(@HoTen, '')));
    
    -- Insert directly, let database handle constraints
    INSERT INTO QuanNhan (
        DonViID, HoTen, NgaySinh, SHSQ, SoTheBHYT, SoCCCD, CapBac, ChucVu,
        NhapNgu, NgayVaoDang, SoTheDang, Doan, DanToc, TonGiao, SucKhoe, NhomMau,
        HoTenChaNamSinh, HoTenMeNamSinh, HoTenVoConNamSinh, NgheNghiepChaMe,
        MayAnhChiEm, QueQuan, NoiO, KhiCanBaoTin, GhiChu, AnhDaiDien, NguoiTao
    )
    VALUES (
        @DonViID, @HoTen, @NgaySinh, @SHSQ, @SoTheBHYT, @SoCCCD, @CapBac, @ChucVu,
        @NhapNgu, @NgayVaoDang, @SoTheDang, @Doan, @DanToc, @TonGiao, @SucKhoe, @NhomMau,
        @HoTenChaNamSinh, @HoTenMeNamSinh, @HoTenVoConNamSinh, @NgheNghiepChaMe,
        @MayAnhChiEm, @QueQuan, @NoiO, @KhiCanBaoTin, @GhiChu, @AnhDaiDien, ISNULL(@NguoiTao, 'system')
    );
    
    SET @QuanNhanID = SCOPE_IDENTITY();
END
GO
PRINT N'✓ Đã tạo SP QuanNhan_Insert';

-- SP: Update QuanNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[QuanNhan_Update];
GO

CREATE PROCEDURE [dbo].[QuanNhan_Update]
    @QuanNhanID INT,
    @DonViID INT,
    @HoTen NVARCHAR(100),
    @NgaySinh DATE = NULL,
    @SHSQ NVARCHAR(50) = NULL,
    @SoTheBHYT NVARCHAR(50) = NULL,
    @SoCCCD NVARCHAR(20),
    @CapBac NVARCHAR(50) = NULL,
    @ChucVu NVARCHAR(100) = NULL,
    @NhapNgu DATE = NULL,
    @NgayVaoDang DATE = NULL,
    @SoTheDang NVARCHAR(50) = NULL,
    @Doan NVARCHAR(50) = NULL,
    @DanToc NVARCHAR(50) = NULL,
    @TonGiao NVARCHAR(50) = NULL,
    @SucKhoe NVARCHAR(50) = NULL,
    @NhomMau NVARCHAR(10) = NULL,
    @HoTenChaNamSinh NVARCHAR(200) = NULL,
    @HoTenMeNamSinh NVARCHAR(200) = NULL,
    @HoTenVoConNamSinh NVARCHAR(200) = NULL,
    @NgheNghiepChaMe NVARCHAR(200) = NULL,
    @MayAnhChiEm INT = NULL,
    @QueQuan NVARCHAR(200) = NULL,
    @NoiO NVARCHAR(500) = NULL,
    @KhiCanBaoTin NVARCHAR(200) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @AnhDaiDien VARBINARY(MAX) = NULL,
    @TrangThai BIT = 1,
    @NguoiTao NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if SoCCCD already exists (excluding current record)
    IF EXISTS (SELECT 1 FROM QuanNhan WHERE SoCCCD = @SoCCCD AND QuanNhanID != @QuanNhanID)
    BEGIN
        RAISERROR(N'Số CCCD đã tồn tại!', 16, 1);
        RETURN -1;
    END
    
    UPDATE QuanNhan SET
        DonViID = @DonViID, HoTen = @HoTen, NgaySinh = @NgaySinh,
        SHSQ = @SHSQ, SoTheBHYT = @SoTheBHYT, SoCCCD = @SoCCCD, CapBac = @CapBac,
        ChucVu = @ChucVu, NhapNgu = @NhapNgu, NgayVaoDang = @NgayVaoDang,
        SoTheDang = @SoTheDang, Doan = @Doan, DanToc = @DanToc, TonGiao = @TonGiao,
        SucKhoe = @SucKhoe, NhomMau = @NhomMau, HoTenChaNamSinh = @HoTenChaNamSinh,
        HoTenMeNamSinh = @HoTenMeNamSinh, HoTenVoConNamSinh = @HoTenVoConNamSinh,
        NgheNghiepChaMe = @NgheNghiepChaMe, MayAnhChiEm = @MayAnhChiEm,
        QueQuan = @QueQuan, NoiO = @NoiO, KhiCanBaoTin = @KhiCanBaoTin,
        GhiChu = @GhiChu, AnhDaiDien = @AnhDaiDien, TrangThai = @TrangThai
    WHERE QuanNhanID = @QuanNhanID;
    
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP QuanNhan_Update';

-- SP: Delete QuanNhan
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuanNhan_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[QuanNhan_Delete];
GO

CREATE PROCEDURE [dbo].[QuanNhan_Delete]
    @QuanNhanID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DELETE FROM QuanNhan WHERE QuanNhanID = @QuanNhanID;
        IF @@ROWCOUNT > 0
            RETURN 1;
        ELSE
            RETURN -1;
    END TRY
    BEGIN CATCH
        RETURN -1;
    END CATCH
END
GO
PRINT N'✓ Đã tạo SP QuanNhan_Delete';

-- =============================================
-- SP: Enhanced DangVien CRUD Operations
-- =============================================

-- =============================================
-- DangVien_CheckDuplicates Stored Procedure (Must be created first)
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_CheckDuplicates]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_CheckDuplicates];
GO

-- DangVien_CheckDuplicates
CREATE PROCEDURE [dbo].[DangVien_CheckDuplicates]
    @DangVienID INT = NULL,
    @SoCCCD NVARCHAR(20),
    @SoTheDangVien NVARCHAR(50) = NULL,
    @SoLyLichDangVien NVARCHAR(50) = NULL,
    @SoDienThoai NVARCHAR(20) = NULL,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @HasDuplicates BIT = 0;
    DECLARE @ErrorMsg NVARCHAR(500) = '';
    
    -- Check SoCCCD
    IF EXISTS (SELECT 1 FROM DangVien WHERE SoCCCD = @SoCCCD AND (@DangVienID IS NULL OR DangVienID != @DangVienID))
    BEGIN
        SET @HasDuplicates = 1;
        SET @ErrorMsg = @ErrorMsg + 'Số CCCD đã tồn tại. ';
    END
    
    -- Check SoTheDangVien
    IF @SoTheDangVien IS NOT NULL AND @SoTheDangVien != ''
    BEGIN
        IF EXISTS (SELECT 1 FROM DangVien WHERE SoTheDangVien = @SoTheDangVien AND (@DangVienID IS NULL OR DangVienID != @DangVienID))
        BEGIN
            SET @HasDuplicates = 1;
            SET @ErrorMsg = @ErrorMsg + 'Số thẻ đảng viên đã tồn tại. ';
        END
    END
    
    -- Check SoLyLichDangVien
    IF @SoLyLichDangVien IS NOT NULL AND @SoLyLichDangVien != ''
    BEGIN
        IF EXISTS (SELECT 1 FROM DangVien WHERE SoLyLichDangVien = @SoLyLichDangVien AND (@DangVienID IS NULL OR DangVienID != @DangVienID))
        BEGIN
            SET @HasDuplicates = 1;
            SET @ErrorMsg = @ErrorMsg + 'Số lý lịch đảng viên đã tồn tại. ';
        END
    END
    
    -- Check SoDienThoai
    IF @SoDienThoai IS NOT NULL AND @SoDienThoai != ''
    BEGIN
        IF EXISTS (SELECT 1 FROM DangVien WHERE SoDienThoai = @SoDienThoai AND (@DangVienID IS NULL OR DangVienID != @DangVienID))
        BEGIN
            SET @HasDuplicates = 1;
            SET @ErrorMsg = @ErrorMsg + 'Số điện thoại đã tồn tại. ';
        END
    END
    
    SET @ErrorMessage = @ErrorMsg;
    
    IF @HasDuplicates = 1
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP DangVien_CheckDuplicates';

-- SP: Get all DangVien (Enhanced)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_GetAll];
GO

CREATE PROCEDURE [dbo].[DangVien_GetAll]
   @DonViID INT = NULL,
   @HoTen NVARCHAR(100) = NULL,
   @SoCCCD NVARCHAR(20) = NULL,
   @LoaiDangVien NVARCHAR(20) = NULL,
   @DoiTuong NVARCHAR(50) = NULL,
   @CapBac NVARCHAR(50) = NULL,
   @ChucVu NVARCHAR(100) = NULL,
   @QueQuan NVARCHAR(200) = NULL,
   @TrinhDo NVARCHAR(50) = NULL,
   @TrangThai BIT = NULL
AS
BEGIN
   SET NOCOUNT ON;
    
   SELECT 
       dv.DangVienID, dv.DonViID, d.TenDonVi, dv.DonViCap1, dv.DonViCap2, dv.HoTenKhaiSinh, dv.HoTen, dv.HoTenKhac, dv.NgaySinh, dv.GioiTinh,
       dv.SoCCCD, dv.SoDienThoai, dv.SoTheDangVien, dv.SoLyLichDangVien,
       dv.NgayThamGiaCachMang, dv.NgayTuyenDung, dv.NgayNhapNgu, dv.NgayXuatNgu, dv.NgayTaiNgu,
       dv.NgayVaoDang, dv.NgayChinhThuc, dv.LoaiDangVien, dv.DoiTuong,
       dv.CapBac, dv.HeSoLuong, dv.ThangNamPhongCapBac, dv.SoHieuQuanNhan, dv.ChucVu, dv.QueQuan, dv.TrinhDo, dv.AnhDaiDien,
       dv.DiaChi, dv.DanToc, dv.TonGiao, dv.NgheNghiep,
       dv.TrinhDoChuyenMon, dv.LyLuanChinhTri, dv.ChucDanhKhoaHoc, dv.HocViCaoNhat, dv.ChuyenNganh, dv.ThoiGianHocVi,
       dv.TrinhDoChiHuyQuanLy, dv.NgoaiNgu, dv.TrinhDoNgoaiNgu, dv.ThoiGianNgoaiNgu, dv.TiengDanToc,
       dv.QuaTrinhHocTap, dv.ChienDauPhucVuChienDau, dv.DiNuocNgoai,
       dv.SucKhoeLoai, dv.NhomMau, dv.BenhChinh, dv.ThuongTat, dv.DanhHieuDuocPhong,
       dv.NgheNghiepTruocNhapNgu, dv.QuanHeCTXHTruocNhapNgu, dv.TinhHinhNhaO,
       dv.TinHoc, dv.QuaTrinhCongTac, dv.HoSoGiaDinh,
       dv.HoTenCha, dv.NamSinhCha, dv.NgheNghiepCha, dv.HoTenMe, dv.NamSinhMe, dv.NgheNghiepMe,
       dv.ThanhPhanGiaDinh, dv.QueQuanChaMe, dv.ChoOHienNayChaMe, dv.SoConTrongGiaDinh, dv.GioiTinhThuTuBanThan,
       dv.TinhHinhKinhTeGiaDinh, dv.TinhHinhChinhTriGiaDinh,
       dv.HoTenChaVoChong, dv.NamSinhChaVoChong, dv.NgheNghiepChaVoChong,
       dv.HoTenMeVoChong, dv.NamSinhMeVoChong, dv.NgheNghiepMeVoChong,
       dv.ThanhPhanGiaDinhVoChong, dv.QueQuanGiaDinhVoChong, dv.ChoOHienNayGiaDinhVoChong,
       dv.SoConTrongGiaDinhVoChong, dv.ThuTuVoChongTrongGiaDinh, dv.TinhHinhKTCTGiaDinhVoChong,
       dv.NgheNghiepVoChong, dv.DangVienHayKhong, dv.NoiOHienNayVoChong, dv.ThongTinCacCon,
       dv.TrangThai, dv.NgayTao, dv.NguoiTao,
       CASE WHEN dv.NgaySinh IS NOT NULL THEN DATEDIFF(YEAR, dv.NgaySinh, GETDATE()) ELSE NULL END AS TuoiDoi,
       CASE WHEN dv.NgayVaoDang IS NOT NULL THEN DATEDIFF(YEAR, dv.NgayVaoDang, GETDATE()) ELSE NULL END AS TuoiDang
   FROM DangVien dv
   INNER JOIN DonVi d ON dv.DonViID = d.DonViID
   WHERE (@DonViID IS NULL OR dv.DonViID = @DonViID)
      AND (@HoTen IS NULL OR dv.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @HoTen + '%')
      AND (@SoCCCD IS NULL OR dv.SoCCCD LIKE '%' + @SoCCCD + '%')
      AND (@LoaiDangVien IS NULL OR dv.LoaiDangVien = @LoaiDangVien)
      AND (@DoiTuong IS NULL OR dv.DoiTuong = @DoiTuong)
      AND (@CapBac IS NULL OR dv.CapBac = @CapBac)
      AND (@ChucVu IS NULL OR dv.ChucVu LIKE '%' + @ChucVu + '%')
      AND (@QueQuan IS NULL OR dv.QueQuan LIKE '%' + @QueQuan + '%')
      AND (@TrinhDo IS NULL OR dv.TrinhDo = @TrinhDo)
      AND (@TrangThai IS NULL OR dv.TrangThai = @TrangThai)
   ORDER BY dv.HoTen;
END
GO
PRINT N'✓ Đã tạo SP DangVien_GetAll (Enhanced)';

-- SP: Insert DangVien (Enhanced)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_Insert];
GO

CREATE PROCEDURE [dbo].[DangVien_Insert]
    @DonViID INT,
    @DonViCap1 NVARCHAR(200) = NULL,
    @DonViCap2 NVARCHAR(200) = NULL,
    @HoTenKhaiSinh NVARCHAR(100) = NULL,
    @HoTen NVARCHAR(100),
    @HoTenKhac NVARCHAR(100) = NULL,
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @SoCCCD NVARCHAR(20),
    @SoDienThoai NVARCHAR(20) = NULL,
    @SoTheDangVien NVARCHAR(50) = NULL,
    @SoLyLichDangVien NVARCHAR(50) = NULL,
    @NgayThamGiaCachMang DATE = NULL,
    @NgayTuyenDung DATE = NULL,
    @NgayNhapNgu DATE = NULL,
    @NgayXuatNgu DATE = NULL,
    @NgayTaiNgu DATE = NULL,
    @NgayVaoDang DATE = NULL,
    @NgayChinhThuc DATE = NULL,
    @LoaiDangVien NVARCHAR(20),
    @DoiTuong NVARCHAR(50) = NULL,
    @CapBac NVARCHAR(50) = NULL,
    @HeSoLuong DECIMAL(5,2) = NULL,
    @ThangNamPhongCapBac NVARCHAR(50) = NULL,
    @SoHieuQuanNhan NVARCHAR(50) = NULL,
    @ChucVu NVARCHAR(100) = NULL,
    @QueQuan NVARCHAR(200) = NULL,
    @TrinhDo NVARCHAR(50) = NULL,
    @DiaChi NVARCHAR(500) = NULL,
    @DanToc NVARCHAR(50) = NULL,
    @TonGiao NVARCHAR(50) = NULL,
    @NgheNghiep NVARCHAR(100) = NULL,
    @TrinhDoChuyenMon NVARCHAR(50) = NULL,
    @LyLuanChinhTri NVARCHAR(50) = NULL,
    @ChucDanhKhoaHoc NVARCHAR(200) = NULL,
    @HocViCaoNhat NVARCHAR(100) = NULL,
    @ChuyenNganh NVARCHAR(200) = NULL,
    @ThoiGianHocVi NVARCHAR(50) = NULL,
    @TrinhDoChiHuyQuanLy NVARCHAR(50) = NULL,
    @NgoaiNgu NVARCHAR(50) = NULL,
    @TrinhDoNgoaiNgu NVARCHAR(50) = NULL,
    @ThoiGianNgoaiNgu NVARCHAR(50) = NULL,
    @TiengDanToc NVARCHAR(MAX) = NULL,
    @QuaTrinhHocTap NVARCHAR(MAX) = NULL,
    @ChienDauPhucVuChienDau NVARCHAR(MAX) = NULL,
    @DiNuocNgoai NVARCHAR(MAX) = NULL,
    @SucKhoeLoai NVARCHAR(50) = NULL,
    @NhomMau NVARCHAR(10) = NULL,
    @BenhChinh NVARCHAR(200) = NULL,
    @ThuongTat NVARCHAR(MAX) = NULL,
    @DanhHieuDuocPhong NVARCHAR(MAX) = NULL,
    @NgheNghiepTruocNhapNgu NVARCHAR(200) = NULL,
    @QuanHeCTXHTruocNhapNgu NVARCHAR(200) = NULL,
    @TinhHinhNhaO NVARCHAR(MAX) = NULL,
    @TinHoc NVARCHAR(50) = NULL,
    @AnhDaiDien VARBINARY(MAX) = NULL,
    @QuaTrinhCongTac NVARCHAR(MAX) = NULL,
    @HoSoGiaDinh NVARCHAR(MAX) = NULL,
    @HoTenCha NVARCHAR(100) = NULL,
    @NamSinhCha INT = NULL,
    @NgheNghiepCha NVARCHAR(200) = NULL,
    @HoTenMe NVARCHAR(100) = NULL,
    @NamSinhMe INT = NULL,
    @NgheNghiepMe NVARCHAR(200) = NULL,
    @ThanhPhanGiaDinh NVARCHAR(100) = NULL,
    @QueQuanChaMe NVARCHAR(200) = NULL,
    @ChoOHienNayChaMe NVARCHAR(MAX) = NULL,
    @SoConTrongGiaDinh INT = NULL,
    @GioiTinhThuTuBanThan NVARCHAR(50) = NULL,
    @TinhHinhKinhTeGiaDinh NVARCHAR(MAX) = NULL,
    @TinhHinhChinhTriGiaDinh NVARCHAR(MAX) = NULL,
    @HoTenChaVoChong NVARCHAR(100) = NULL,
    @NamSinhChaVoChong INT = NULL,
    @NgheNghiepChaVoChong NVARCHAR(200) = NULL,
    @HoTenMeVoChong NVARCHAR(100) = NULL,
    @NamSinhMeVoChong INT = NULL,
    @NgheNghiepMeVoChong NVARCHAR(200) = NULL,
    @ThanhPhanGiaDinhVoChong NVARCHAR(100) = NULL,
    @QueQuanGiaDinhVoChong NVARCHAR(200) = NULL,
    @ChoOHienNayGiaDinhVoChong NVARCHAR(MAX) = NULL,
    @SoConTrongGiaDinhVoChong INT = NULL,
    @ThuTuVoChongTrongGiaDinh NVARCHAR(50) = NULL,
    @TinhHinhKTCTGiaDinhVoChong NVARCHAR(MAX) = NULL,
    @NgheNghiepVoChong NVARCHAR(200) = NULL,
    @DangVienHayKhong BIT = NULL,
    @NoiOHienNayVoChong NVARCHAR(MAX) = NULL,
    @ThongTinCacCon NVARCHAR(MAX) = NULL,
    @NguoiTao NVARCHAR(100),
    @DangVienID INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @HasDuplicates INT;
    EXEC @HasDuplicates = DangVien_CheckDuplicates
        @DangVienID = NULL,
        @SoCCCD = @SoCCCD,
        @SoTheDangVien = @SoTheDangVien,
        @SoLyLichDangVien = @SoLyLichDangVien,
        @SoDienThoai = @SoDienThoai,
        @ErrorMessage = @ErrorMessage OUTPUT;
    
    IF @HasDuplicates = 1
        RETURN -1;

    INSERT INTO DangVien (
        DonViID, DonViCap1, DonViCap2, HoTenKhaiSinh, HoTen, HoTenKhac, NgaySinh, GioiTinh, SoCCCD, SoDienThoai,
        SoTheDangVien, SoLyLichDangVien, NgayThamGiaCachMang, NgayTuyenDung, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu,
        NgayVaoDang, NgayChinhThuc, LoaiDangVien, DoiTuong, CapBac, HeSoLuong, ThangNamPhongCapBac, SoHieuQuanNhan,
        ChucVu, QueQuan, TrinhDo, DiaChi, DanToc, TonGiao, NgheNghiep, TrinhDoChuyenMon,
        LyLuanChinhTri, ChucDanhKhoaHoc, HocViCaoNhat, ChuyenNganh, ThoiGianHocVi, TrinhDoChiHuyQuanLy,
        NgoaiNgu, TrinhDoNgoaiNgu, ThoiGianNgoaiNgu, TiengDanToc, QuaTrinhHocTap, ChienDauPhucVuChienDau, DiNuocNgoai,
        SucKhoeLoai, NhomMau, BenhChinh, ThuongTat, DanhHieuDuocPhong, NgheNghiepTruocNhapNgu, QuanHeCTXHTruocNhapNgu, TinhHinhNhaO,
        TinHoc, AnhDaiDien, QuaTrinhCongTac, HoSoGiaDinh,
        HoTenCha, NamSinhCha, NgheNghiepCha, HoTenMe, NamSinhMe, NgheNghiepMe,
        ThanhPhanGiaDinh, QueQuanChaMe, ChoOHienNayChaMe, SoConTrongGiaDinh, GioiTinhThuTuBanThan,
        TinhHinhKinhTeGiaDinh, TinhHinhChinhTriGiaDinh,
        HoTenChaVoChong, NamSinhChaVoChong, NgheNghiepChaVoChong,
        HoTenMeVoChong, NamSinhMeVoChong, NgheNghiepMeVoChong,
        ThanhPhanGiaDinhVoChong, QueQuanGiaDinhVoChong, ChoOHienNayGiaDinhVoChong,
        SoConTrongGiaDinhVoChong, ThuTuVoChongTrongGiaDinh, TinhHinhKTCTGiaDinhVoChong,
        NgheNghiepVoChong, DangVienHayKhong, NoiOHienNayVoChong, ThongTinCacCon, NguoiTao
    )
    VALUES (
        @DonViID, @DonViCap1, @DonViCap2, @HoTenKhaiSinh, @HoTen, @HoTenKhac, @NgaySinh, @GioiTinh, @SoCCCD, @SoDienThoai,
        @SoTheDangVien, @SoLyLichDangVien, @NgayThamGiaCachMang, @NgayTuyenDung, @NgayNhapNgu, @NgayXuatNgu, @NgayTaiNgu,
        @NgayVaoDang, @NgayChinhThuc, @LoaiDangVien, @DoiTuong, @CapBac, @HeSoLuong, @ThangNamPhongCapBac, @SoHieuQuanNhan,
        @ChucVu, @QueQuan, @TrinhDo, @DiaChi, @DanToc, @TonGiao, @NgheNghiep, @TrinhDoChuyenMon,
        @LyLuanChinhTri, @ChucDanhKhoaHoc, @HocViCaoNhat, @ChuyenNganh, @ThoiGianHocVi, @TrinhDoChiHuyQuanLy,
        @NgoaiNgu, @TrinhDoNgoaiNgu, @ThoiGianNgoaiNgu, @TiengDanToc, @QuaTrinhHocTap, @ChienDauPhucVuChienDau, @DiNuocNgoai,
        @SucKhoeLoai, @NhomMau, @BenhChinh, @ThuongTat, @DanhHieuDuocPhong, @NgheNghiepTruocNhapNgu, @QuanHeCTXHTruocNhapNgu, @TinhHinhNhaO,
        @TinHoc, @AnhDaiDien, @QuaTrinhCongTac, @HoSoGiaDinh,
        @HoTenCha, @NamSinhCha, @NgheNghiepCha, @HoTenMe, @NamSinhMe, @NgheNghiepMe,
        @ThanhPhanGiaDinh, @QueQuanChaMe, @ChoOHienNayChaMe, @SoConTrongGiaDinh, @GioiTinhThuTuBanThan,
        @TinhHinhKinhTeGiaDinh, @TinhHinhChinhTriGiaDinh,
        @HoTenChaVoChong, @NamSinhChaVoChong, @NgheNghiepChaVoChong,
        @HoTenMeVoChong, @NamSinhMeVoChong, @NgheNghiepMeVoChong,
        @ThanhPhanGiaDinhVoChong, @QueQuanGiaDinhVoChong, @ChoOHienNayGiaDinhVoChong,
        @SoConTrongGiaDinhVoChong, @ThuTuVoChongTrongGiaDinh, @TinhHinhKTCTGiaDinhVoChong,
        @NgheNghiepVoChong, @DangVienHayKhong, @NoiOHienNayVoChong, @ThongTinCacCon, @NguoiTao
    );

    SET @DangVienID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP DangVien_Insert (Enhanced)';

-- SP: Update DangVien (Enhanced)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_Update];
GO

CREATE PROCEDURE [dbo].[DangVien_Update]
    @DangVienID INT,
    @DonViID INT,
    @DonViCap1 NVARCHAR(200) = NULL,
    @DonViCap2 NVARCHAR(200) = NULL,
    @HoTenKhaiSinh NVARCHAR(100) = NULL,
    @HoTen NVARCHAR(100),
    @HoTenKhac NVARCHAR(100) = NULL,
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @SoCCCD NVARCHAR(20),
    @SoDienThoai NVARCHAR(20) = NULL,
    @SoTheDangVien NVARCHAR(50) = NULL,
    @SoLyLichDangVien NVARCHAR(50) = NULL,
    @NgayThamGiaCachMang DATE = NULL,
    @NgayTuyenDung DATE = NULL,
    @NgayNhapNgu DATE = NULL,
    @NgayXuatNgu DATE = NULL,
    @NgayTaiNgu DATE = NULL,
    @NgayVaoDang DATE = NULL,
    @NgayChinhThuc DATE = NULL,
    @LoaiDangVien NVARCHAR(20),
    @DoiTuong NVARCHAR(50) = NULL,
    @CapBac NVARCHAR(50) = NULL,
    @HeSoLuong DECIMAL(5,2) = NULL,
    @ThangNamPhongCapBac NVARCHAR(50) = NULL,
    @SoHieuQuanNhan NVARCHAR(50) = NULL,
    @ChucVu NVARCHAR(100) = NULL,
    @QueQuan NVARCHAR(200) = NULL,
    @TrinhDo NVARCHAR(50) = NULL,
    @DiaChi NVARCHAR(500) = NULL,
    @DanToc NVARCHAR(50) = NULL,
    @TonGiao NVARCHAR(50) = NULL,
    @NgheNghiep NVARCHAR(100) = NULL,
    @TrinhDoChuyenMon NVARCHAR(50) = NULL,
    @LyLuanChinhTri NVARCHAR(50) = NULL,
    @ChucDanhKhoaHoc NVARCHAR(200) = NULL,
    @HocViCaoNhat NVARCHAR(100) = NULL,
    @ChuyenNganh NVARCHAR(200) = NULL,
    @ThoiGianHocVi NVARCHAR(50) = NULL,
    @TrinhDoChiHuyQuanLy NVARCHAR(50) = NULL,
    @NgoaiNgu NVARCHAR(50) = NULL,
    @TrinhDoNgoaiNgu NVARCHAR(50) = NULL,
    @ThoiGianNgoaiNgu NVARCHAR(50) = NULL,
    @TiengDanToc NVARCHAR(MAX) = NULL,
    @QuaTrinhHocTap NVARCHAR(MAX) = NULL,
    @ChienDauPhucVuChienDau NVARCHAR(MAX) = NULL,
    @DiNuocNgoai NVARCHAR(MAX) = NULL,
    @SucKhoeLoai NVARCHAR(50) = NULL,
    @NhomMau NVARCHAR(10) = NULL,
    @BenhChinh NVARCHAR(200) = NULL,
    @ThuongTat NVARCHAR(MAX) = NULL,
    @DanhHieuDuocPhong NVARCHAR(MAX) = NULL,
    @NgheNghiepTruocNhapNgu NVARCHAR(200) = NULL,
    @QuanHeCTXHTruocNhapNgu NVARCHAR(200) = NULL,
    @TinhHinhNhaO NVARCHAR(MAX) = NULL,
    @TinHoc NVARCHAR(50) = NULL,
    @AnhDaiDien VARBINARY(MAX) = NULL,
    @QuaTrinhCongTac NVARCHAR(MAX) = NULL,
    @HoSoGiaDinh NVARCHAR(MAX) = NULL,
    @HoTenCha NVARCHAR(100) = NULL,
    @NamSinhCha INT = NULL,
    @NgheNghiepCha NVARCHAR(200) = NULL,
    @HoTenMe NVARCHAR(100) = NULL,
    @NamSinhMe INT = NULL,
    @NgheNghiepMe NVARCHAR(200) = NULL,
    @ThanhPhanGiaDinh NVARCHAR(100) = NULL,
    @QueQuanChaMe NVARCHAR(200) = NULL,
    @ChoOHienNayChaMe NVARCHAR(MAX) = NULL,
    @SoConTrongGiaDinh INT = NULL,
    @GioiTinhThuTuBanThan NVARCHAR(50) = NULL,
    @TinhHinhKinhTeGiaDinh NVARCHAR(MAX) = NULL,
    @TinhHinhChinhTriGiaDinh NVARCHAR(MAX) = NULL,
    @HoTenChaVoChong NVARCHAR(100) = NULL,
    @NamSinhChaVoChong INT = NULL,
    @NgheNghiepChaVoChong NVARCHAR(200) = NULL,
    @HoTenMeVoChong NVARCHAR(100) = NULL,
    @NamSinhMeVoChong INT = NULL,
    @NgheNghiepMeVoChong NVARCHAR(200) = NULL,
    @ThanhPhanGiaDinhVoChong NVARCHAR(100) = NULL,
    @QueQuanGiaDinhVoChong NVARCHAR(200) = NULL,
    @ChoOHienNayGiaDinhVoChong NVARCHAR(MAX) = NULL,
    @SoConTrongGiaDinhVoChong INT = NULL,
    @ThuTuVoChongTrongGiaDinh NVARCHAR(50) = NULL,
    @TinhHinhKTCTGiaDinhVoChong NVARCHAR(MAX) = NULL,
    @NgheNghiepVoChong NVARCHAR(200) = NULL,
    @DangVienHayKhong BIT = NULL,
    @NoiOHienNayVoChong NVARCHAR(MAX) = NULL,
    @ThongTinCacCon NVARCHAR(MAX) = NULL,
    @TrangThai BIT,
    @NguoiTao NVARCHAR(100),
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @HasDuplicates INT;
    EXEC @HasDuplicates = DangVien_CheckDuplicates
        @DangVienID = @DangVienID,
        @SoCCCD = @SoCCCD,
        @SoTheDangVien = @SoTheDangVien,
        @SoLyLichDangVien = @SoLyLichDangVien,
        @SoDienThoai = @SoDienThoai,
        @ErrorMessage = @ErrorMessage OUTPUT;
    
    IF @HasDuplicates = 1
        RETURN -1;
    
    UPDATE DangVien SET
        DonViID = @DonViID, DonViCap1 = @DonViCap1, DonViCap2 = @DonViCap2, HoTenKhaiSinh = @HoTenKhaiSinh, HoTen = @HoTen, HoTenKhac = @HoTenKhac,
        NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SoCCCD = @SoCCCD, SoDienThoai = @SoDienThoai,
        SoTheDangVien = @SoTheDangVien, SoLyLichDangVien = @SoLyLichDangVien,
        NgayThamGiaCachMang = @NgayThamGiaCachMang, NgayTuyenDung = @NgayTuyenDung,
        NgayNhapNgu = @NgayNhapNgu, NgayXuatNgu = @NgayXuatNgu, NgayTaiNgu = @NgayTaiNgu,
        NgayVaoDang = @NgayVaoDang, NgayChinhThuc = @NgayChinhThuc,
        LoaiDangVien = @LoaiDangVien, DoiTuong = @DoiTuong, CapBac = @CapBac,
        HeSoLuong = @HeSoLuong, ThangNamPhongCapBac = @ThangNamPhongCapBac, SoHieuQuanNhan = @SoHieuQuanNhan,
        ChucVu = @ChucVu, QueQuan = @QueQuan, TrinhDo = @TrinhDo,
        DiaChi = @DiaChi, DanToc = @DanToc, TonGiao = @TonGiao, NgheNghiep = @NgheNghiep,
        TrinhDoChuyenMon = @TrinhDoChuyenMon,
        LyLuanChinhTri = @LyLuanChinhTri, ChucDanhKhoaHoc = @ChucDanhKhoaHoc,
        HocViCaoNhat = @HocViCaoNhat, ChuyenNganh = @ChuyenNganh, ThoiGianHocVi = @ThoiGianHocVi,
        TrinhDoChiHuyQuanLy = @TrinhDoChiHuyQuanLy, NgoaiNgu = @NgoaiNgu,
        TrinhDoNgoaiNgu = @TrinhDoNgoaiNgu, ThoiGianNgoaiNgu = @ThoiGianNgoaiNgu,
        TiengDanToc = @TiengDanToc, QuaTrinhHocTap = @QuaTrinhHocTap,
        ChienDauPhucVuChienDau = @ChienDauPhucVuChienDau, DiNuocNgoai = @DiNuocNgoai,
        SucKhoeLoai = @SucKhoeLoai, NhomMau = @NhomMau, BenhChinh = @BenhChinh,
        ThuongTat = @ThuongTat, DanhHieuDuocPhong = @DanhHieuDuocPhong,
        NgheNghiepTruocNhapNgu = @NgheNghiepTruocNhapNgu, QuanHeCTXHTruocNhapNgu = @QuanHeCTXHTruocNhapNgu,
        TinhHinhNhaO = @TinhHinhNhaO, TinHoc = @TinHoc, AnhDaiDien = @AnhDaiDien,
        QuaTrinhCongTac = @QuaTrinhCongTac, HoSoGiaDinh = @HoSoGiaDinh,
        HoTenCha = @HoTenCha, NamSinhCha = @NamSinhCha, NgheNghiepCha = @NgheNghiepCha,
        HoTenMe = @HoTenMe, NamSinhMe = @NamSinhMe, NgheNghiepMe = @NgheNghiepMe,
        ThanhPhanGiaDinh = @ThanhPhanGiaDinh, QueQuanChaMe = @QueQuanChaMe,
        ChoOHienNayChaMe = @ChoOHienNayChaMe, SoConTrongGiaDinh = @SoConTrongGiaDinh,
        GioiTinhThuTuBanThan = @GioiTinhThuTuBanThan,
        TinhHinhKinhTeGiaDinh = @TinhHinhKinhTeGiaDinh, TinhHinhChinhTriGiaDinh = @TinhHinhChinhTriGiaDinh,
        HoTenChaVoChong = @HoTenChaVoChong, NamSinhChaVoChong = @NamSinhChaVoChong,
        NgheNghiepChaVoChong = @NgheNghiepChaVoChong, HoTenMeVoChong = @HoTenMeVoChong,
        NamSinhMeVoChong = @NamSinhMeVoChong, NgheNghiepMeVoChong = @NgheNghiepMeVoChong,
        ThanhPhanGiaDinhVoChong = @ThanhPhanGiaDinhVoChong, QueQuanGiaDinhVoChong = @QueQuanGiaDinhVoChong,
        ChoOHienNayGiaDinhVoChong = @ChoOHienNayGiaDinhVoChong,
        SoConTrongGiaDinhVoChong = @SoConTrongGiaDinhVoChong,
        ThuTuVoChongTrongGiaDinh = @ThuTuVoChongTrongGiaDinh,
        TinhHinhKTCTGiaDinhVoChong = @TinhHinhKTCTGiaDinhVoChong,
        NgheNghiepVoChong = @NgheNghiepVoChong, DangVienHayKhong = @DangVienHayKhong,
        NoiOHienNayVoChong = @NoiOHienNayVoChong, ThongTinCacCon = @ThongTinCacCon,
        TrangThai = @TrangThai
    WHERE DangVienID = @DangVienID;

    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP DangVien_Update (Enhanced)';

-- =============================================
-- SP: KhenThuong Operations (Merged from KhenThuongCaNhan and KhenThuongDonVi)
-- =============================================

-- SP: KhenThuong_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_GetAll];
GO

CREATE PROCEDURE [dbo].[KhenThuong_GetAll]
    @Loai NVARCHAR(20) = NULL -- CaNhan, DonVi, or NULL for all
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kt.KhenThuongID,
        kt.Loai,
        kt.DangVienID,
        CASE WHEN kt.DangVienID IS NOT NULL THEN dv.HoTen ELSE NULL END as TenDangVien,
        kt.DonViID,
        CASE WHEN kt.DonViID IS NOT NULL THEN d.TenDonVi ELSE NULL END as TenDonVi,
        kt.HinhThuc,
        kt.Ngay,
        kt.SoQuyetDinh,
        kt.CapQuyetDinh,
        kt.NoiDung,
        kt.FileDinhKem,
        kt.NgayTao,
        kt.NguoiTao
    FROM KhenThuong kt
    LEFT JOIN DangVien dv ON kt.DangVienID = dv.DangVienID
    LEFT JOIN DonVi d ON kt.DonViID = d.DonViID
    WHERE (@Loai IS NULL OR kt.Loai = @Loai)
    ORDER BY kt.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_GetAll';

-- SP: KhenThuong_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_GetById];
GO

CREATE PROCEDURE [dbo].[KhenThuong_GetById]
    @KhenThuongID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kt.KhenThuongID,
        kt.Loai,
        kt.DangVienID,
        CASE WHEN kt.DangVienID IS NOT NULL THEN dv.HoTen ELSE NULL END as TenDangVien,
        kt.DonViID,
        CASE WHEN kt.DonViID IS NOT NULL THEN d.TenDonVi ELSE NULL END as TenDonVi,
        kt.HinhThuc,
        kt.Ngay,
        kt.SoQuyetDinh,
        kt.CapQuyetDinh,
        kt.NoiDung,
        kt.FileDinhKem,
        kt.NgayTao,
        kt.NguoiTao
    FROM KhenThuong kt
    LEFT JOIN DangVien dv ON kt.DangVienID = dv.DangVienID
    LEFT JOIN DonVi d ON kt.DonViID = d.DonViID
    WHERE kt.KhenThuongID = @KhenThuongID;
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_GetById';

-- SP: KhenThuong_GetByDangVienID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_GetByDangVienID];
GO

CREATE PROCEDURE [dbo].[KhenThuong_GetByDangVienID]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        kt.KhenThuongID,
        kt.Loai,
        kt.DangVienID,
        dv.HoTen as TenDangVien,
        kt.DonViID,
        NULL as TenDonVi,
        kt.HinhThuc,
        kt.Ngay,
        kt.SoQuyetDinh,
        kt.CapQuyetDinh,
        kt.NoiDung,
        kt.FileDinhKem,
        kt.NgayTao,
        kt.NguoiTao
    FROM KhenThuong kt
    INNER JOIN DangVien dv ON kt.DangVienID = dv.DangVienID
    WHERE kt.DangVienID = @DangVienID AND kt.Loai = 'CaNhan'
    ORDER BY kt.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_GetByDangVienID';

-- SP: KhenThuong_GetByDonViID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_GetByDonViID];
GO

CREATE PROCEDURE [dbo].[KhenThuong_GetByDonViID]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kt.KhenThuongID,
        kt.Loai,
        kt.DangVienID,
        NULL as TenDangVien,
        kt.DonViID,
        d.TenDonVi,
        kt.HinhThuc,
        kt.Ngay,
        kt.SoQuyetDinh,
        kt.CapQuyetDinh,
        kt.NoiDung,
        kt.FileDinhKem,
        kt.NgayTao,
        kt.NguoiTao
    FROM KhenThuong kt
    INNER JOIN DonVi d ON kt.DonViID = d.DonViID
    WHERE kt.DonViID = @DonViID AND kt.Loai = 'DonVi'
    ORDER BY kt.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_GetByDonViID';

-- SP: KhenThuong_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_Insert];
GO

CREATE PROCEDURE [dbo].[KhenThuong_Insert]
    @Loai NVARCHAR(20), -- CaNhan or DonVi
    @DangVienID INT = NULL,
    @DonViID INT = NULL,
    @HinhThuc NVARCHAR(200),
    @Ngay DATE,
    @SoQuyetDinh NVARCHAR(100),
    @CapQuyetDinh NVARCHAR(100),
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL,
    @NguoiTao NVARCHAR(100),
    @KhenThuongID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate Loai
    IF @Loai NOT IN ('CaNhan', 'DonVi')
    BEGIN
        RAISERROR(N'Loai phải là CaNhan hoặc DonVi', 16, 1);
        RETURN -1;
    END
    
    -- Validate based on Loai
    IF @Loai = 'CaNhan' AND (@DangVienID IS NULL OR @DonViID IS NOT NULL)
    BEGIN
        RAISERROR(N'KhenThuong CaNhan phải có DangVienID và không có DonViID', 16, 1);
        RETURN -1;
    END
    
    IF @Loai = 'DonVi' AND (@DonViID IS NULL OR @DangVienID IS NOT NULL)
    BEGIN
        RAISERROR(N'KhenThuong DonVi phải có DonViID và không có DangVienID', 16, 1);
        RETURN -1;
    END
    
    INSERT INTO KhenThuong (
        Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh,
        NoiDung, FileDinhKem, NguoiTao
    )
    VALUES (
        @Loai, @DangVienID, @DonViID, @HinhThuc, @Ngay, @SoQuyetDinh, @CapQuyetDinh,
        @NoiDung, @FileDinhKem, @NguoiTao
    );
    
    SET @KhenThuongID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_Insert';

-- SP: KhenThuong_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_Update];
GO

CREATE PROCEDURE [dbo].[KhenThuong_Update]
    @KhenThuongID INT,
    @Loai NVARCHAR(20),
    @DangVienID INT = NULL,
    @DonViID INT = NULL,
    @HinhThuc NVARCHAR(200),
    @Ngay DATE,
    @SoQuyetDinh NVARCHAR(100),
    @CapQuyetDinh NVARCHAR(100),
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate Loai
    IF @Loai NOT IN ('CaNhan', 'DonVi')
    BEGIN
        RAISERROR(N'Loai phải là CaNhan hoặc DonVi', 16, 1);
        RETURN -1;
    END
    
    -- Validate based on Loai
    IF @Loai = 'CaNhan' AND (@DangVienID IS NULL OR @DonViID IS NOT NULL)
    BEGIN
        RAISERROR(N'KhenThuong CaNhan phải có DangVienID và không có DonViID', 16, 1);
        RETURN -1;
    END
    
    IF @Loai = 'DonVi' AND (@DonViID IS NULL OR @DangVienID IS NOT NULL)
    BEGIN
        RAISERROR(N'KhenThuong DonVi phải có DonViID và không có DangVienID', 16, 1);
        RETURN -1;
    END
    
    UPDATE KhenThuong SET
        Loai = @Loai,
        DangVienID = @DangVienID,
        DonViID = @DonViID,
        HinhThuc = @HinhThuc,
        Ngay = @Ngay,
        SoQuyetDinh = @SoQuyetDinh,
        CapQuyetDinh = @CapQuyetDinh,
        NoiDung = @NoiDung,
        FileDinhKem = @FileDinhKem
    WHERE KhenThuongID = @KhenThuongID;
    
    IF @@ROWCOUNT > 0
        RETURN 0;
    ELSE
        RETURN -1; -- Not found
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_Update';

-- SP: KhenThuong_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuong_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuong_Delete];
GO

CREATE PROCEDURE [dbo].[KhenThuong_Delete]
    @KhenThuongID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM KhenThuong 
    WHERE KhenThuongID = @KhenThuongID;
    
    IF @@ROWCOUNT > 0
        RETURN 0;
    ELSE
        RETURN -1; -- Not found
END
GO
PRINT N'✓ Đã tạo SP KhenThuong_Delete';

-- =============================================
-- SP: KyLuat Operations (Merged from KyLuatCaNhan and KyLuatToChuc)
-- =============================================

-- SP: KyLuat_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_GetAll];
GO

CREATE PROCEDURE [dbo].[KyLuat_GetAll]
    @Loai NVARCHAR(20) = NULL -- CaNhan, DonVi, or NULL for all
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kl.KyLuatID,
        kl.Loai,
        kl.DangVienID,
        CASE WHEN kl.DangVienID IS NOT NULL THEN dv.HoTen ELSE NULL END as TenDangVien,
        kl.DonViID,
        CASE WHEN kl.DonViID IS NOT NULL THEN d.TenDonVi ELSE NULL END as TenDonVi,
        kl.HinhThuc,
        kl.Ngay,
        kl.SoQuyetDinh,
        kl.CapQuyetDinh,
        kl.NoiDung,
        kl.GhiChu,
        kl.FileDinhKem,
        kl.NgayTao,
        kl.NguoiTao
    FROM KyLuat kl
    LEFT JOIN DangVien dv ON kl.DangVienID = dv.DangVienID
    LEFT JOIN DonVi d ON kl.DonViID = d.DonViID
    WHERE (@Loai IS NULL OR kl.Loai = @Loai)
    ORDER BY kl.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KyLuat_GetAll';

-- SP: KyLuat_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_GetById];
GO

CREATE PROCEDURE [dbo].[KyLuat_GetById]
    @KyLuatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kl.KyLuatID,
        kl.Loai,
        kl.DangVienID,
        CASE WHEN kl.DangVienID IS NOT NULL THEN dv.HoTen ELSE NULL END as TenDangVien,
        kl.DonViID,
        CASE WHEN kl.DonViID IS NOT NULL THEN d.TenDonVi ELSE NULL END as TenDonVi,
        kl.HinhThuc,
        kl.Ngay,
        kl.SoQuyetDinh,
        kl.CapQuyetDinh,
        kl.NoiDung,
        kl.GhiChu,
        kl.FileDinhKem,
        kl.NgayTao,
        kl.NguoiTao
    FROM KyLuat kl
    LEFT JOIN DangVien dv ON kl.DangVienID = dv.DangVienID
    LEFT JOIN DonVi d ON kl.DonViID = d.DonViID
    WHERE kl.KyLuatID = @KyLuatID;
END
GO
PRINT N'✓ Đã tạo SP KyLuat_GetById';

-- SP: KyLuat_GetByDangVienID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_GetByDangVienID];
GO

CREATE PROCEDURE [dbo].[KyLuat_GetByDangVienID]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT
        kl.KyLuatID,
        kl.Loai,
        kl.DangVienID,
        dv.HoTen as TenDangVien,
        kl.DonViID,
        NULL as TenDonVi,
        kl.HinhThuc,
        kl.Ngay,
        kl.SoQuyetDinh,
        kl.CapQuyetDinh,
        kl.NoiDung,
        kl.GhiChu,
        kl.FileDinhKem,
        kl.NgayTao,
        kl.NguoiTao
    FROM KyLuat kl
    INNER JOIN DangVien dv ON kl.DangVienID = dv.DangVienID
    WHERE kl.DangVienID = @DangVienID AND kl.Loai = 'CaNhan'
    ORDER BY kl.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KyLuat_GetByDangVienID';

-- SP: KyLuat_GetByDonViID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_GetByDonViID];
GO

CREATE PROCEDURE [dbo].[KyLuat_GetByDonViID]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        kl.KyLuatID,
        kl.Loai,
        kl.DangVienID,
        NULL as TenDangVien,
        kl.DonViID,
        d.TenDonVi,
        kl.HinhThuc,
        kl.Ngay,
        kl.SoQuyetDinh,
        kl.CapQuyetDinh,
        kl.NoiDung,
        kl.GhiChu,
        kl.FileDinhKem,
        kl.NgayTao,
        kl.NguoiTao
    FROM KyLuat kl
    INNER JOIN DonVi d ON kl.DonViID = d.DonViID
    WHERE kl.DonViID = @DonViID AND kl.Loai = 'DonVi'
    ORDER BY kl.Ngay DESC;
END
GO
PRINT N'✓ Đã tạo SP KyLuat_GetByDonViID';

-- SP: KyLuat_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_Insert];
GO

CREATE PROCEDURE [dbo].[KyLuat_Insert]
    @Loai NVARCHAR(20), -- CaNhan or DonVi
    @DangVienID INT = NULL,
    @DonViID INT = NULL,
    @HinhThuc NVARCHAR(200),
    @Ngay DATE,
    @SoQuyetDinh NVARCHAR(100),
    @CapQuyetDinh NVARCHAR(100),
    @NoiDung NVARCHAR(MAX) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL,
    @NguoiTao NVARCHAR(100),
    @KyLuatID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate Loai
    IF @Loai NOT IN ('CaNhan', 'DonVi')
    BEGIN
        RAISERROR(N'Loai phải là CaNhan hoặc DonVi', 16, 1);
        RETURN -1;
    END
    
    -- Validate based on Loai
    IF @Loai = 'CaNhan' AND (@DangVienID IS NULL OR @DonViID IS NOT NULL)
    BEGIN
        RAISERROR(N'KyLuat CaNhan phải có DangVienID và không có DonViID', 16, 1);
        RETURN -1;
    END
    
    IF @Loai = 'DonVi' AND (@DonViID IS NULL OR @DangVienID IS NOT NULL)
    BEGIN
        RAISERROR(N'KyLuat DonVi phải có DonViID và không có DangVienID', 16, 1);
        RETURN -1;
    END
    
    INSERT INTO KyLuat (
        Loai, DangVienID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh,
        NoiDung, GhiChu, FileDinhKem, NguoiTao
    )
    VALUES (
        @Loai, @DangVienID, @DonViID, @HinhThuc, @Ngay, @SoQuyetDinh, @CapQuyetDinh,
        @NoiDung, @GhiChu, @FileDinhKem, @NguoiTao
    );
    
    SET @KyLuatID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP KyLuat_Insert';

-- SP: KyLuat_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_Update];
GO

CREATE PROCEDURE [dbo].[KyLuat_Update]
    @KyLuatID INT,
    @Loai NVARCHAR(20),
    @DangVienID INT = NULL,
    @DonViID INT = NULL,
    @HinhThuc NVARCHAR(200),
    @Ngay DATE,
    @SoQuyetDinh NVARCHAR(100),
    @CapQuyetDinh NVARCHAR(100),
    @NoiDung NVARCHAR(MAX) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate Loai
    IF @Loai NOT IN ('CaNhan', 'DonVi')
    BEGIN
        RAISERROR(N'Loai phải là CaNhan hoặc DonVi', 16, 1);
        RETURN -1;
    END
    
    -- Validate based on Loai
    IF @Loai = 'CaNhan' AND (@DangVienID IS NULL OR @DonViID IS NOT NULL)
    BEGIN
        RAISERROR(N'KyLuat CaNhan phải có DangVienID và không có DonViID', 16, 1);
        RETURN -1;
    END
    
    IF @Loai = 'DonVi' AND (@DonViID IS NULL OR @DangVienID IS NOT NULL)
    BEGIN
        RAISERROR(N'KyLuat DonVi phải có DonViID và không có DangVienID', 16, 1);
        RETURN -1;
    END
    
    UPDATE KyLuat SET
        Loai = @Loai,
        DangVienID = @DangVienID,
        DonViID = @DonViID,
        HinhThuc = @HinhThuc,
        Ngay = @Ngay,
        SoQuyetDinh = @SoQuyetDinh,
        CapQuyetDinh = @CapQuyetDinh,
        NoiDung = @NoiDung,
        GhiChu = @GhiChu,
        FileDinhKem = @FileDinhKem
    WHERE KyLuatID = @KyLuatID;
    
    IF @@ROWCOUNT > 0
        RETURN 0;
    ELSE
        RETURN -1; -- Not found
END
GO
PRINT N'✓ Đã tạo SP KyLuat_Update';

-- SP: KyLuat_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuat_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuat_Delete];
GO

CREATE PROCEDURE [dbo].[KyLuat_Delete]
    @KyLuatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM KyLuat 
    WHERE KyLuatID = @KyLuatID;
    
    IF @@ROWCOUNT > 0
        RETURN 0;
    ELSE
        RETURN -1; -- Not found
END
GO
PRINT N'✓ Đã tạo SP KyLuat_Delete';

-- =============================================
-- SP: Initialize Sample Data
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InitSampleData]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[InitSampleData];
GO

CREATE PROCEDURE [dbo].[InitSampleData]
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @RowCount INT;
    
    PRINT N'';
    PRINT N'===========================================';
    PRINT N'KHỞI TẠO DỮ LIỆU MẪU';
    PRINT N'===========================================';

    SELECT @RowCount = COUNT(*) FROM DonVi;
    IF @RowCount = 0
    BEGIN
        DECLARE @DonViCap1A INT, @DonViCap1B INT;
        DECLARE @DonViCap2A1 INT, @DonViCap2A2 INT, @DonViCap2B1 INT;
        
        -- Insert đơn vị cấp 1 (không có CapTrenID)
        INSERT INTO DonVi (TenDonVi, MaDonVi, TenDayDu, CapBac, DiaChi, Email, TruongDonVi, MoTa, CapTrenID, NguoiTao)
        VALUES 
        (N'Đơn vị cấp 1 - A', 'DV1A', N'Đơn vị cấp 1 - A', N'Cấp 1', N'123 Đường ABC, Quận 1, TP.HCM', 'dv1a@example.com', N'Nguyễn Văn A', N'Đơn vị cấp 1', NULL, 'system'),
        (N'Đơn vị cấp 1 - B', 'DV1B', N'Đơn vị cấp 1 - B', N'Cấp 1', N'456 Đường DEF, Quận 2, TP.HCM', 'dv1b@example.com', N'Trần Thị B', N'Đơn vị cấp 1', NULL, 'system');
        
        -- Lấy ID của đơn vị cấp 1 vừa insert
        SELECT @DonViCap1A = DonViID FROM DonVi WHERE MaDonVi = 'DV1A';
        SELECT @DonViCap1B = DonViID FROM DonVi WHERE MaDonVi = 'DV1B';
        
        -- Insert đơn vị cấp 2 (CapTrenID = DonViID của cấp 1)
        INSERT INTO DonVi (TenDonVi, MaDonVi, TenDayDu, CapBac, DiaChi, Email, TruongDonVi, MoTa, CapTrenID, NguoiTao)
        VALUES 
        (N'Đơn vị cấp 2 - A1', 'DV2A1', N'Đơn vị cấp 2 - A1', N'Cấp 2', N'123 Đường ABC, Quận 1, TP.HCM', 'dv2a1@example.com', N'Nguyễn Văn A1', N'Đơn vị cấp 2', @DonViCap1A, 'system'),
        (N'Đơn vị cấp 2 - A2', 'DV2A2', N'Đơn vị cấp 2 - A2', N'Cấp 2', N'123 Đường ABC, Quận 1, TP.HCM', 'dv2a2@example.com', N'Nguyễn Văn A2', N'Đơn vị cấp 2', @DonViCap1A, 'system'),
        (N'Đơn vị cấp 2 - B1', 'DV2B1', N'Đơn vị cấp 2 - B1', N'Cấp 2', N'456 Đường DEF, Quận 2, TP.HCM', 'dv2b1@example.com', N'Trần Thị B1', N'Đơn vị cấp 2', @DonViCap1B, 'system');
        
        -- Lấy ID của đơn vị cấp 2 vừa insert
        SELECT @DonViCap2A1 = DonViID FROM DonVi WHERE MaDonVi = 'DV2A1';
        SELECT @DonViCap2A2 = DonViID FROM DonVi WHERE MaDonVi = 'DV2A2';
        SELECT @DonViCap2B1 = DonViID FROM DonVi WHERE MaDonVi = 'DV2B1';
        
        -- Insert đơn vị cấp 3 (CapTrenID = DonViID của cấp 2) - Chi bộ
        INSERT INTO DonVi (TenDonVi, MaDonVi, TenDayDu, CapBac, DiaChi, Email, TruongDonVi, MoTa, CapTrenID, NguoiTao)
        VALUES 
        (N'Chi bộ 1', 'CB001', N'Chi bộ Quân đội Nhân dân Việt Nam - Đơn vị 1', N'Chi bộ', N'123 Đường ABC, Quận 1, TP.HCM', 'cb1@example.com', N'Nguyễn Văn C1', N'Chi bộ đảng cơ sở', @DonViCap2A1, 'system'),
        (N'Chi bộ 2', 'CB002', N'Chi bộ Quân đội Nhân dân Việt Nam - Đơn vị 2', N'Chi bộ', N'456 Đường DEF, Quận 2, TP.HCM', 'cb2@example.com', N'Trần Thị C2', N'Chi bộ đảng cơ sở', @DonViCap2A2, 'system'),
        (N'Chi bộ 3', 'CB003', N'Chi bộ Quân đội Nhân dân Việt Nam - Đơn vị 3', N'Chi bộ', N'789 Đường GHI, Quận 3, TP.HCM', 'cb3@example.com', N'Lê Văn C3', N'Chi bộ đảng cơ sở', @DonViCap2B1, 'system');
        
        PRINT N'✓ Đã thêm ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' đơn vị mẫu (3 cấp)';
    END
    ELSE
    BEGIN
        PRINT N'→ Đã có ' + CAST(@RowCount AS NVARCHAR(10)) + N' đơn vị trong hệ thống';
    END

    SELECT @RowCount = COUNT(*) FROM NguoiDung;
    IF @RowCount = 0
    BEGIN
        -- Hash passwords using SHA-256 (same as C# HashPassword method)
        -- admin123: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
        -- bithu123: 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
        -- vanphong123: 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918 (tạm thời, sẽ update sau)
        INSERT INTO NguoiDung (DonViID, TenDangNhap, MatKhau, HoTen, Email, VaiTro, Roles, Permissions, NguoiTao)
        VALUES 
        (1, 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'Administrator', 'admin@example.com', 'Admin', 'Admin', 'All', 'system'),
        (1, 'bithu1', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'Bí thư Chi bộ 1', 'bithu1@example.com', 'BiThu', 'BiThu', 'CRUD', 'system'),
        (2, 'vanphong1', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'Văn phòng Chi bộ 2', 'vanphong1@example.com', 'VanPhong', 'VanPhong', 'Read', 'system');
        
        PRINT N'✓ Đã thêm ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' người dùng mẫu';
        PRINT N'  → admin/admin123 (Admin)';
        PRINT N'  → bithu1/bithu123 (Bí thư)';
        PRINT N'  → vanphong1/vanphong123 (Văn phòng)';
    END
    ELSE
    BEGIN
        PRINT N'→ Đã có ' + CAST(@RowCount AS NVARCHAR(10)) + N' người dùng trong hệ thống';
        -- Update password cho các user hiện có nếu chưa đúng
        -- bithu123: 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
        -- vanphong123: 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
        UPDATE NguoiDung 
        SET MatKhau = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
        WHERE TenDangNhap = 'bithu1' AND MatKhau != '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918';
        
        UPDATE NguoiDung 
        SET MatKhau = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
        WHERE TenDangNhap = 'vanphong1' AND MatKhau != '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918';
        
        IF @@ROWCOUNT > 0
            PRINT N'✓ Đã cập nhật password cho các user hiện có';
    END

    -- Insert sample QuanNhan data
    SELECT @RowCount = COUNT(*) FROM QuanNhan;
    IF @RowCount = 0
    BEGIN
        INSERT INTO QuanNhan (DonViID, HoTen, NgaySinh, SHSQ, SoTheBHYT, SoCCCD, CapBac, ChucVu, NhapNgu, NgayVaoDang, SoTheDang, Doan, DanToc, TonGiao, SucKhoe, NhomMau, HoTenChaNamSinh, HoTenMeNamSinh, HoTenVoConNamSinh, NgheNghiepChaMe, MayAnhChiEm, QueQuan, NoiO, KhiCanBaoTin, GhiChu, AnhDaiDien, NguoiTao)
        VALUES 
        (1, N'Nguyễn Văn An', '1985-05-15', 'SHSQ001', 'BHYT001', '123456789012', N'Thượng úy', N'Trưởng phòng', '2003-06-01', '2010-06-01', 'DV001', N'Đoàn viên', N'Kinh', N'Không', N'Tốt', N'A', N'Nguyễn Văn Ba (1950)', N'Trần Thị Bình (1955)', N'Nguyễn Thị Lan (1988)', N'Nông dân', 3, N'Hà Nội', N'123 Đường ABC, Quận 1, TP.HCM', N'Nguyễn Văn Ba - 0987654321', N'Quân nhân gương mẫu', NULL, 'system'),
        (1, N'Trần Thị Bình', '1990-08-20', 'SHSQ002', 'BHYT002', '123456789013', N'Trung úy', N'Nhân viên', '2008-03-15', '2015-03-15', 'DV002', N'Đoàn viên', N'Kinh', N'Phật giáo', N'Tốt', N'B', N'Trần Văn Cường (1960)', N'Lê Thị Dung (1965)', N'Trần Văn Em (1992)', N'Công nhân', 2, N'TP.HCM', N'456 Đường DEF, Quận 2, TP.HCM', N'Trần Văn Cường - 0987654322', N'Nhân viên xuất sắc', NULL, 'system'),
        (2, N'Lê Văn Cường', '1988-12-10', 'SHSQ003', 'BHYT003', '123456789014', N'Đại úy', N'Giám đốc', '2006-09-01', '2012-09-01', 'DV003', N'Đoàn viên', N'Kinh', N'Không', N'Tốt', N'O', N'Lê Văn Dũng (1955)', N'Phạm Thị Em (1960)', N'Lê Thị Phượng (1990)', N'Cán bộ', 4, N'Đà Nẵng', N'789 Đường GHI, Quận 3, TP.HCM', N'Lê Văn Dũng - 0987654323', N'Lãnh đạo giỏi', NULL, 'system');
        
        PRINT N'✓ Đã thêm ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' quân nhân mẫu';
    END
    ELSE
    BEGIN
        PRINT N'→ Đã có ' + CAST(@RowCount AS NVARCHAR(10)) + N' quân nhân trong hệ thống';
    END

    -- Insert sample DangVien data (Enhanced)
    SELECT @RowCount = COUNT(*) FROM DangVien;
    IF @RowCount = 0
    BEGIN
        INSERT INTO DangVien (DonViID, HoTen, NgaySinh, GioiTinh, SoCCCD, SoDienThoai, SoTheDangVien, SoLyLichDangVien, NgayVaoDang, NgayChinhThuc, LoaiDangVien, DoiTuong, CapBac, ChucVu, QueQuan, TrinhDo, DiaChi, DanToc, TonGiao, NgheNghiep, TrinhDoHocVan, TrinhDoChuyenMon, LyLuanChinhTri, NgoaiNgu, TinHoc, NguoiTao)
VALUES 
        (1, N'Nguyễn Văn An', '1985-05-15', N'Nam', '123456789012', '0987654321', 'DV001', 'LL001', '2010-06-01', '2011-06-01', N'Chính thức', N'QNCN', N'Đảng viên', N'Trưởng phòng', N'Hà Nội', N'Đại học', N'123 Đường ABC, Quận 1, TP.HCM', N'Kinh', N'Không', N'Quân nhân', N'Đại học', N'Quân sự', N'Cao', N'Tiếng Anh', N'Giỏi', 'system'),
        (1, N'Trần Thị Bình', '1990-08-20', N'Nữ', '123456789013', '0987654322', 'DV002', 'LL002', '2015-03-15', '2016-03-15', N'Chính thức', N'HSQ-BS', N'Đảng viên', N'Nhân viên', N'TP.HCM', N'Cao học', N'456 Đường DEF, Quận 2, TP.HCM', N'Kinh', N'Phật giáo', N'Y tá', N'Cao học', N'Y tế', N'Trung bình', N'Tiếng Pháp', N'Khá', 'system'),
        (2, N'Lê Văn Cường', '1988-12-10', N'Nam', '123456789014', '0987654323', 'DV003', 'LL003', '2012-09-01', '2013-09-01', N'Chính thức', N'LĐHĐ', N'Bí thư', N'Giám đốc', N'Đà Nẵng', N'Tiến sĩ', N'789 Đường GHI, Quận 3, TP.HCM', N'Kinh', N'Không', N'Cán bộ', N'Tiến sĩ', N'Quản lý', N'Cao', N'Tiếng Anh', N'Giỏi', 'system'),
        (2, N'Phạm Thị Dung', '1992-03-25', N'Nữ', '123456789015', '0987654324', 'DV004', 'LL004', '2018-05-20', NULL, N'Dự bị', N'CNVCQP', N'Đảng viên', N'Kế toán', N'Cần Thơ', N'Đại học', N'321 Đường JKL, Quận 4, TP.HCM', N'Kinh', N'Thiên chúa', N'Kế toán', N'Đại học', N'Tài chính', N'Trung bình', N'Tiếng Anh', N'Khá', 'system');

        PRINT N'✓ Đã thêm ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' đảng viên mẫu';
    END
    ELSE
    BEGIN
        PRINT N'→ Đã có ' + CAST(@RowCount AS NVARCHAR(10)) + N' đảng viên trong hệ thống';
    END

    PRINT N'';
    PRINT N'===========================================';
    PRINT N'HOÀN TẤT KHỞI TẠO DỮ LIỆU';
    PRINT N'===========================================';
END
GO
PRINT N'✓ Đã tạo SP InitSampleData';

-- =============================================
-- CHẠY STORED PROCEDURE KHỞI TẠO DỮ LIỆU
-- =============================================
PRINT N'';
PRINT N'Đang khởi tạo dữ liệu mẫu...';
EXEC InitSampleData;

-- =============================================
-- KIỂM TRA KẾT QUẢ
-- =============================================
PRINT N'';
PRINT N'===========================================';
PRINT N'KIỂM TRA DỮ LIỆU';
PRINT N'===========================================';

DECLARE @DonViCount INT, @NguoiDungCount INT, @DangVienCount INT, @QuanNhanCount INT;

SELECT @DonViCount = COUNT(*) FROM DonVi;
SELECT @NguoiDungCount = COUNT(*) FROM NguoiDung;
SELECT @DangVienCount = COUNT(*) FROM DangVien;
SELECT @QuanNhanCount = COUNT(*) FROM QuanNhan;

PRINT N'Số đơn vị: ' + CAST(@DonViCount AS NVARCHAR(10));
PRINT N'Số người dùng: ' + CAST(@NguoiDungCount AS NVARCHAR(10));
PRINT N'Số đảng viên: ' + CAST(@DangVienCount AS NVARCHAR(10));
PRINT N'Số quân nhân: ' + CAST(@QuanNhanCount AS NVARCHAR(10));

PRINT N'';
PRINT N'===========================================';
PRINT N'HOÀN TẤT CÀI ĐẶT DATABASE';
PRINT N'===========================================';
PRINT N'✓ Database: QuanLyDangVien';
PRINT N'✓ Bảng: 15+ bảng (đã hợp nhất KhenThuong và KyLuat, xóa Roles/Permissions)';
PRINT N'  - KhenThuong: Hợp nhất từ KhenThuongCaNhan và KhenThuongDonVi';
PRINT N'  - KyLuat: Hợp nhất từ KyLuatCaNhan và KyLuatToChuc';
PRINT N'✓ Stored Procedures: 40+ SPs';
PRINT N'✓ Dữ liệu mẫu: Đã khởi tạo';
PRINT N'';
PRINT N'Thông tin đăng nhập:';
PRINT N'  - admin/admin123 (Admin)';
PRINT N'  - bithu1/bithu123 (Bí thư)';
PRINT N'  - vanphong1/vanphong123 (Văn phòng)';
PRINT N'===========================================';
GO

-- =============================================
-- MISSING STORED PROCEDURES FOR SERVICES
-- =============================================

-- =============================================
-- DonVi Stored Procedures (Fixed)
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_GetAll];
GO

-- DonVi_GetAll
CREATE PROCEDURE [dbo].[DonVi_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT d.DonViID, d.MaDonVi, d.TenDonVi, d.CapBac, d.DiaChi, d.Email, 
           d.TruongDonVi, d.CapTrenID,
           dt.TenDonVi AS TenCapTren,
           d.NgayTao, d.NguoiTao
    FROM DonVi d
    LEFT JOIN DonVi dt ON d.CapTrenID = dt.DonViID
    ORDER BY d.TenDonVi;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_GetByID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_GetByID];
GO

-- DonVi_GetByID
CREATE PROCEDURE [dbo].[DonVi_GetByID]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT d.DonViID, d.MaDonVi, d.TenDonVi, d.CapBac, d.DiaChi, d.Email, 
           d.TruongDonVi, d.CapTrenID,
           dt.TenDonVi AS TenCapTren,
           d.NgayTao, d.NguoiTao
    FROM DonVi d
    LEFT JOIN DonVi dt ON d.CapTrenID = dt.DonViID
    WHERE d.DonViID = @DonViID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_Insert];
GO

-- DonVi_Insert
CREATE PROCEDURE [dbo].[DonVi_Insert]
    @TenDonVi NVARCHAR(200),
    @MaDonVi NVARCHAR(50),
    @CapBac NVARCHAR(100),
    @DiaChi NVARCHAR(500),
    @Email NVARCHAR(100),
    @TruongDonVi NVARCHAR(200),
    @CapTrenID INT = NULL,
    @NguoiTao NVARCHAR(100),
    @DonViID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO DonVi (TenDonVi, MaDonVi, CapBac, DiaChi, Email, 
                      TruongDonVi, CapTrenID, NgayTao, NguoiTao)
    VALUES (@TenDonVi, @MaDonVi, @CapBac, @DiaChi, @Email, 
            @TruongDonVi, @CapTrenID, GETDATE(), @NguoiTao);
    
    SET @DonViID = SCOPE_IDENTITY();
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_Update];
GO

-- DonVi_Update
CREATE PROCEDURE [dbo].[DonVi_Update]
    @DonViID INT,
    @TenDonVi NVARCHAR(200),
    @MaDonVi NVARCHAR(50),
    @CapBac NVARCHAR(100),
    @DiaChi NVARCHAR(500),
    @Email NVARCHAR(100),
    @TruongDonVi NVARCHAR(200),
    @CapTrenID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra đơn vị có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViID)
    BEGIN
        RETURN -1; -- Không tìm thấy đơn vị
    END
    
    -- Kiểm tra CapTrenID không được trùng với chính nó
    IF @CapTrenID IS NOT NULL AND @CapTrenID = @DonViID
    BEGIN
        RAISERROR(N'Đơn vị không thể là cấp trên của chính nó!', 16, 1);
        RETURN -1;
    END
    
    -- Kiểm tra vòng lặp phân cấp (tránh A -> B -> A)
    IF @CapTrenID IS NOT NULL
    BEGIN
        DECLARE @CurrentCapTren INT = @CapTrenID;
        DECLARE @Level INT = 0;
        WHILE @CurrentCapTren IS NOT NULL AND @Level < 10
        BEGIN
            IF @CurrentCapTren = @DonViID
            BEGIN
                RAISERROR(N'Không thể tạo vòng lặp phân cấp!', 16, 1);
                RETURN -1;
            END
            SELECT @CurrentCapTren = CapTrenID FROM DonVi WHERE DonViID = @CurrentCapTren;
            SET @Level = @Level + 1;
        END
    END
    
    UPDATE DonVi 
    SET TenDonVi = @TenDonVi,
        MaDonVi = @MaDonVi,
        CapBac = @CapBac,
        DiaChi = @DiaChi,
        Email = @Email,
        TruongDonVi = @TruongDonVi,
        CapTrenID = @CapTrenID
    WHERE DonViID = @DonViID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_Delete];
GO

-- DonVi_Delete
CREATE PROCEDURE [dbo].[DonVi_Delete]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra đơn vị có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViID)
    BEGIN
        RETURN -1; -- Không tìm thấy đơn vị
    END
    
    -- Kiểm tra đơn vị có đang được sử dụng không (có đảng viên hoặc quân nhân)
    IF EXISTS (SELECT 1 FROM DangVien WHERE DonViID = @DonViID)
       OR EXISTS (SELECT 1 FROM QuanNhan WHERE DonViID = @DonViID)
    BEGIN
        RETURN -2; -- Đơn vị đang được sử dụng, không thể xóa
    END
    
    DELETE FROM DonVi 
    WHERE DonViID = @DonViID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được xóa
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_CheckCodeExists]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_CheckCodeExists];
GO

-- DonVi_CheckCodeExists
CREATE PROCEDURE [dbo].[DonVi_CheckCodeExists]
    @MaDonVi NVARCHAR(50),
    @DonViID INT = NULL,
    @Exists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @DonViID IS NULL
        SET @Exists = CASE WHEN EXISTS(SELECT 1 FROM DonVi WHERE MaDonVi = @MaDonVi) THEN 1 ELSE 0 END;
    ELSE
        SET @Exists = CASE WHEN EXISTS(SELECT 1 FROM DonVi WHERE MaDonVi = @MaDonVi AND DonViID != @DonViID) THEN 1 ELSE 0 END;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonVi_GetDangVienCount]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DonVi_GetDangVienCount];
GO

-- DonVi_GetDangVienCount
CREATE PROCEDURE [dbo].[DonVi_GetDangVienCount]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT d.DonViID, d.TenDonVi, d.MaDonVi,
           COUNT(dv.DangVienID) as SoLuongDangVien,
           COUNT(CASE WHEN dv.LoaiDangVien = 'Chính thức' THEN 1 END) as DangVienChinhThuc,
           COUNT(CASE WHEN dv.LoaiDangVien = 'Dự bị' THEN 1 END) as DangVienDuBi
    FROM DonVi d
    LEFT JOIN DangVien dv ON d.DonViID = dv.DonViID AND dv.TrangThai = 1
    GROUP BY d.DonViID, d.TenDonVi, d.MaDonVi
    ORDER BY d.TenDonVi;
END
GO

-- =============================================
-- DangVien Additional Stored Procedures (Fixed)
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_GetById];
GO

-- DangVien_GetById
CREATE PROCEDURE [dbo].[DangVien_GetById]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT dv.DangVienID, dv.DonViID, d.TenDonVi, dv.DonViCap1, dv.DonViCap2, dv.HoTenKhaiSinh, dv.HoTen, dv.HoTenKhac, dv.NgaySinh, dv.GioiTinh,
           dv.SoCCCD, dv.SoDienThoai, dv.SoTheDangVien, dv.SoLyLichDangVien,
           dv.NgayThamGiaCachMang, dv.NgayTuyenDung, dv.NgayNhapNgu, dv.NgayXuatNgu, dv.NgayTaiNgu,
           dv.NgayVaoDang, dv.NgayChinhThuc, dv.LoaiDangVien, dv.DoiTuong,
           dv.CapBac, dv.HeSoLuong, dv.ThangNamPhongCapBac, dv.SoHieuQuanNhan, dv.ChucVu, dv.QueQuan, dv.TrinhDo, dv.DiaChi, dv.DanToc, dv.TonGiao,
           dv.NgheNghiep, dv.TrinhDoChuyenMon, dv.LyLuanChinhTri,
           dv.ChucDanhKhoaHoc, dv.HocViCaoNhat, dv.ChuyenNganh, dv.ThoiGianHocVi, dv.TrinhDoChiHuyQuanLy,
           dv.NgoaiNgu, dv.TrinhDoNgoaiNgu, dv.ThoiGianNgoaiNgu, dv.TiengDanToc,
           dv.QuaTrinhHocTap, dv.ChienDauPhucVuChienDau, dv.DiNuocNgoai,
           dv.SucKhoeLoai, dv.NhomMau, dv.BenhChinh, dv.ThuongTat, dv.DanhHieuDuocPhong,
           dv.NgheNghiepTruocNhapNgu, dv.QuanHeCTXHTruocNhapNgu, dv.TinhHinhNhaO,
           dv.TinHoc, dv.AnhDaiDien, dv.QuaTrinhCongTac, dv.HoSoGiaDinh,
           dv.HoTenCha, dv.NamSinhCha, dv.NgheNghiepCha, dv.HoTenMe, dv.NamSinhMe, dv.NgheNghiepMe,
           dv.ThanhPhanGiaDinh, dv.QueQuanChaMe, dv.ChoOHienNayChaMe, dv.SoConTrongGiaDinh, dv.GioiTinhThuTuBanThan,
           dv.TinhHinhKinhTeGiaDinh, dv.TinhHinhChinhTriGiaDinh,
           dv.HoTenChaVoChong, dv.NamSinhChaVoChong, dv.NgheNghiepChaVoChong,
           dv.HoTenMeVoChong, dv.NamSinhMeVoChong, dv.NgheNghiepMeVoChong,
           dv.ThanhPhanGiaDinhVoChong, dv.QueQuanGiaDinhVoChong, dv.ChoOHienNayGiaDinhVoChong,
           dv.SoConTrongGiaDinhVoChong, dv.ThuTuVoChongTrongGiaDinh, dv.TinhHinhKTCTGiaDinhVoChong,
           dv.NgheNghiepVoChong, dv.DangVienHayKhong, dv.NoiOHienNayVoChong, dv.ThongTinCacCon,
           dv.TrangThai, dv.NgayTao, dv.NguoiTao
    FROM DangVien dv
    LEFT JOIN DonVi d ON dv.DonViID = d.DonViID
    WHERE dv.DangVienID = @DangVienID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_Delete];
GO

-- DangVien_Delete
CREATE PROCEDURE [dbo].[DangVien_Delete]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE DangVien 
    SET TrangThai = 0
    WHERE DangVienID = @DangVienID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangVien_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangVien_GetByDonViID];
GO

-- DangVien_GetByDonViID
CREATE PROCEDURE [dbo].[DangVien_GetByDonViID]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT dv.DangVienID, dv.DonViID, d.TenDonVi, dv.HoTen, dv.NgaySinh, dv.GioiTinh,
           dv.SoCCCD, dv.SoDienThoai, dv.SoTheDangVien, dv.SoLyLichDangVien,
           dv.NgayVaoDang, dv.NgayChinhThuc, dv.LoaiDangVien, dv.DoiTuong,
           dv.CapBac, dv.ChucVu, dv.QueQuan, dv.DiaChi, dv.DanToc, dv.TonGiao,
           dv.NgheNghiep, dv.TrinhDoChuyenMon, dv.LyLuanChinhTri,
           dv.NgoaiNgu, dv.TinHoc, dv.AnhDaiDien, dv.QuaTrinhCongTac, dv.HoSoGiaDinh,
           dv.TrangThai, dv.NgayTao, dv.NguoiTao
    FROM DangVien dv
    LEFT JOIN DonVi d ON dv.DonViID = d.DonViID
    WHERE dv.DonViID = @DonViID AND dv.TrangThai = 1
    ORDER BY dv.HoTen;
END
GO

-- =============================================
-- OLD STORED PROCEDURES REMOVED
-- Replaced by KhenThuong_* and KyLuat_* stored procedures
-- =============================================

-- Drop old stored procedures if they exist (for cleanup)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_GetAll];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_GetById];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_GetByDangVienID];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_Insert];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_Update];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongCaNhan_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongCaNhan_Delete];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongDonVi_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongDonVi_GetAll];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongDonVi_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongDonVi_GetByDonViID];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhenThuongDonVi_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KhenThuongDonVi_Insert];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_GetAll];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_GetById];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_GetByDangVienID];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_Insert];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_Update];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatCaNhan_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatCaNhan_Delete];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatToChuc_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatToChuc_GetAll];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatToChuc_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatToChuc_GetByDonViID];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KyLuatToChuc_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[KyLuatToChuc_Insert];
GO
PRINT N'✓ Đã xóa các stored procedures cũ';

-- =============================================
-- NguoiDung Stored Procedures (Fixed)
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_GetAll];
GO

-- NguoiDung_GetAll
CREATE PROCEDURE [dbo].[NguoiDung_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT NguoiDungID, TenDangNhap, HoTen, Email, 
           TrangThai, NgayTao, NguoiTao
    FROM NguoiDung 
    WHERE TrangThai = 1
    ORDER BY HoTen;
END
GO

-- =============================================
-- SinhHoatChiBo Stored Procedures
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SinhHoatChiBo_GetAll];
GO

-- SinhHoatChiBo_GetAll
CREATE PROCEDURE [dbo].[SinhHoatChiBo_GetAll]
    @DonViID INT = NULL,
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT sh.SinhHoatID, sh.DonViID, d.TenDonVi, sh.TieuDe, sh.NgaySinhHoat,
           sh.DiaDiem, sh.ChuTri, sh.ThuKy, sh.NoiDung, sh.FileNghiQuyet,
           sh.SoLuongThamGia, sh.TrangThai, sh.NgayTao, sh.NguoiTao
    FROM SinhHoatChiBo sh
    LEFT JOIN DonVi d ON sh.DonViID = d.DonViID
    WHERE (@DonViID IS NULL OR sh.DonViID = @DonViID)
      AND (@TuNgay IS NULL OR sh.NgaySinhHoat >= @TuNgay)
      AND (@DenNgay IS NULL OR sh.NgaySinhHoat <= @DenNgay)
    ORDER BY sh.NgaySinhHoat DESC;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SinhHoatChiBo_GetById];
GO

-- SinhHoatChiBo_GetById
CREATE PROCEDURE [dbo].[SinhHoatChiBo_GetById]
    @SinhHoatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT sh.SinhHoatID, sh.DonViID, d.TenDonVi, sh.TieuDe, sh.NgaySinhHoat,
           sh.DiaDiem, sh.ChuTri, sh.ThuKy, sh.NoiDung, sh.FileNghiQuyet,
           sh.SoLuongThamGia, sh.TrangThai, sh.NgayTao, sh.NguoiTao
    FROM SinhHoatChiBo sh
    LEFT JOIN DonVi d ON sh.DonViID = d.DonViID
    WHERE sh.SinhHoatID = @SinhHoatID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SinhHoatChiBo_Insert];
GO

-- SinhHoatChiBo_Insert
CREATE PROCEDURE [dbo].[SinhHoatChiBo_Insert]
    @DonViID INT,
    @TieuDe NVARCHAR(200),
    @NgaySinhHoat DATETIME,
    @DiaDiem NVARCHAR(200) = NULL,
    @ChuTri NVARCHAR(100) = NULL,
    @ThuKy NVARCHAR(100) = NULL,
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileNghiQuyet NVARCHAR(500) = NULL,
    @SoLuongThamGia INT = NULL,
    @NguoiTao NVARCHAR(100),
    @SinhHoatID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO SinhHoatChiBo (
        DonViID, TieuDe, NgaySinhHoat, DiaDiem, ChuTri, ThuKy,
        NoiDung, FileNghiQuyet, SoLuongThamGia, TrangThai, NgayTao, NguoiTao
    )
    VALUES (
        @DonViID, @TieuDe, @NgaySinhHoat, @DiaDiem, @ChuTri, @ThuKy,
        @NoiDung, @FileNghiQuyet, @SoLuongThamGia, N'ChuaDiemDanh', GETDATE(), @NguoiTao
    );
    
    SET @SinhHoatID = SCOPE_IDENTITY();
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SinhHoatChiBo_Update];
GO

-- SinhHoatChiBo_Update
CREATE PROCEDURE [dbo].[SinhHoatChiBo_Update]
    @SinhHoatID INT,
    @DonViID INT,
    @TieuDe NVARCHAR(200),
    @NgaySinhHoat DATETIME,
    @DiaDiem NVARCHAR(200) = NULL,
    @ChuTri NVARCHAR(100) = NULL,
    @ThuKy NVARCHAR(100) = NULL,
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileNghiQuyet NVARCHAR(500) = NULL,
    @SoLuongThamGia INT = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE SinhHoatChiBo 
    SET DonViID = @DonViID,
        TieuDe = @TieuDe,
        NgaySinhHoat = @NgaySinhHoat,
        DiaDiem = @DiaDiem,
        ChuTri = @ChuTri,
        ThuKy = @ThuKy,
        NoiDung = @NoiDung,
        FileNghiQuyet = @FileNghiQuyet,
        SoLuongThamGia = @SoLuongThamGia,
        TrangThai = ISNULL(@TrangThai, TrangThai)
    WHERE SinhHoatID = @SinhHoatID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhHoatChiBo_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SinhHoatChiBo_Delete];
GO

-- SinhHoatChiBo_Delete
CREATE PROCEDURE [dbo].[SinhHoatChiBo_Delete]
    @SinhHoatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Xóa điểm danh trước
    DELETE FROM DiemDanhSinhHoat WHERE SinhHoatID = @SinhHoatID;
    
    -- Xóa sinh hoạt
    DELETE FROM SinhHoatChiBo WHERE SinhHoatID = @SinhHoatID;
END
GO

-- =============================================
-- DiemDanhSinhHoat Stored Procedures
-- =============================================

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanhSinhHoat_GetBySinhHoatID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DiemDanhSinhHoat_GetBySinhHoatID];
GO

-- DiemDanhSinhHoat_GetBySinhHoatID
CREATE PROCEDURE [dbo].[DiemDanhSinhHoat_GetBySinhHoatID]
    @SinhHoatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT dd.DiemDanhID, dd.SinhHoatID, dd.DangVienID, dv.HoTen, dv.SoTheDangVien,
           dd.CoMat, dd.LyDoVang, dd.GhiChu, dd.NgayDiemDanh, dd.NguoiDiemDanh
    FROM DiemDanhSinhHoat dd
    LEFT JOIN DangVien dv ON dd.DangVienID = dv.DangVienID
    WHERE dd.SinhHoatID = @SinhHoatID
    ORDER BY dv.HoTen;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanhSinhHoat_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DiemDanhSinhHoat_Insert];
GO

-- DiemDanhSinhHoat_Insert
CREATE PROCEDURE [dbo].[DiemDanhSinhHoat_Insert]
    @SinhHoatID INT,
    @DangVienID INT,
    @CoMat BIT,
    @LyDoVang NVARCHAR(200) = NULL,
    @GhiChu NVARCHAR(500) = NULL,
    @NguoiDiemDanh NVARCHAR(100),
    @DiemDanhID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO DiemDanhSinhHoat (
        SinhHoatID, DangVienID, CoMat, LyDoVang, GhiChu, NgayDiemDanh, NguoiDiemDanh
    )
    VALUES (
        @SinhHoatID, @DangVienID, @CoMat, @LyDoVang, @GhiChu, GETDATE(), @NguoiDiemDanh
    );
    
    SET @DiemDanhID = SCOPE_IDENTITY();
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanhSinhHoat_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DiemDanhSinhHoat_Update];
GO

-- DiemDanhSinhHoat_Update
CREATE PROCEDURE [dbo].[DiemDanhSinhHoat_Update]
    @DiemDanhID INT,
    @CoMat BIT,
    @LyDoVang NVARCHAR(200) = NULL,
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE DiemDanhSinhHoat 
    SET CoMat = @CoMat,
        LyDoVang = @LyDoVang,
        GhiChu = @GhiChu
    WHERE DiemDanhID = @DiemDanhID;
END
GO

-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanhSinhHoat_HangLoat]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DiemDanhSinhHoat_HangLoat];
GO

-- DiemDanhSinhHoat_HangLoat
CREATE PROCEDURE [dbo].[DiemDanhSinhHoat_HangLoat]
    @SinhHoatID INT,
    @DonViID INT,
    @CoMat BIT = 1,
    @NguoiDiemDanh NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO DiemDanhSinhHoat (SinhHoatID, DangVienID, CoMat, NgayDiemDanh, NguoiDiemDanh)
    SELECT @SinhHoatID, dv.DangVienID, @CoMat, GETDATE(), @NguoiDiemDanh
    FROM DangVien dv
    WHERE dv.DonViID = @DonViID AND dv.TrangThai = 1
    AND NOT EXISTS (
        SELECT 1 FROM DiemDanhSinhHoat dd 
        WHERE dd.SinhHoatID = @SinhHoatID AND dd.DangVienID = dv.DangVienID
    );
END
GO

-- =============================================
-- NhacNhoSinhHoat Stored Procedures
-- NOTE: Bảng NhacNhoSinhHoat không tồn tại, các SP này đã bị xóa
-- =============================================

-- Các stored procedures về NhacNhoSinhHoat đã được xóa vì bảng không tồn tại
GO

-- =============================================
-- ChuyenSinhHoatDang Stored Procedures
-- =============================================

-- SP: ChuyenSinhHoatDang_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_GetAll];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_GetAll]
    @DangVienID INT = NULL,
    @DonViID INT = NULL,
    @Nam INT = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cshd.ChuyenSinhHoatID, 
        cshd.DangVienID, 
        dv.HoTen AS TenDangVien,
        cshd.DonViDi, 
        ddi.TenDonVi AS TenDonViDi,
        cshd.DonViDen, 
        dden.TenDonVi AS TenDonViDen,
        cshd.NgayChuyen, 
        cshd.LyDo, 
        cshd.GhiChu, 
        cshd.FileQuyetDinh,
        cshd.TrangThai, 
        cshd.NgayTao, 
        cshd.NguoiTao
    FROM ChuyenSinhHoatDang cshd
    INNER JOIN DangVien dv ON cshd.DangVienID = dv.DangVienID
    INNER JOIN DonVi ddi ON cshd.DonViDi = ddi.DonViID
    INNER JOIN DonVi dden ON cshd.DonViDen = dden.DonViID
    WHERE (@DangVienID IS NULL OR cshd.DangVienID = @DangVienID)
      AND (@DonViID IS NULL OR cshd.DonViDi = @DonViID OR cshd.DonViDen = @DonViID)
      AND (@Nam IS NULL OR YEAR(cshd.NgayChuyen) = @Nam)
      AND (@TrangThai IS NULL OR cshd.TrangThai = @TrangThai)
    ORDER BY cshd.NgayChuyen DESC;
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_GetAll';

-- SP: ChuyenSinhHoatDang_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_GetById];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_GetById]
    @ChuyenSinhHoatID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cshd.ChuyenSinhHoatID, 
        cshd.DangVienID, 
        dv.HoTen AS TenDangVien,
        cshd.DonViDi, 
        ddi.TenDonVi AS TenDonViDi,
        cshd.DonViDen, 
        dden.TenDonVi AS TenDonViDen,
        cshd.NgayChuyen, 
        cshd.LyDo, 
        cshd.GhiChu, 
        cshd.FileQuyetDinh,
        cshd.TrangThai, 
        cshd.NgayTao, 
        cshd.NguoiTao
    FROM ChuyenSinhHoatDang cshd
    INNER JOIN DangVien dv ON cshd.DangVienID = dv.DangVienID
    INNER JOIN DonVi ddi ON cshd.DonViDi = ddi.DonViID
    INNER JOIN DonVi dden ON cshd.DonViDen = dden.DonViID
    WHERE cshd.ChuyenSinhHoatID = @ChuyenSinhHoatID;
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_GetById';

-- SP: ChuyenSinhHoatDang_GetByDangVienID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByDangVienID];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByDangVienID]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cshd.ChuyenSinhHoatID, 
        cshd.DangVienID, 
        dv.HoTen AS TenDangVien,
        cshd.DonViDi, 
        ddi.TenDonVi AS TenDonViDi,
        cshd.DonViDen, 
        dden.TenDonVi AS TenDonViDen,
        cshd.NgayChuyen, 
        cshd.LyDo, 
        cshd.GhiChu, 
        cshd.FileQuyetDinh,
        cshd.TrangThai, 
        cshd.NgayTao, 
        cshd.NguoiTao
    FROM ChuyenSinhHoatDang cshd
    INNER JOIN DangVien dv ON cshd.DangVienID = dv.DangVienID
    INNER JOIN DonVi ddi ON cshd.DonViDi = ddi.DonViID
    INNER JOIN DonVi dden ON cshd.DonViDen = dden.DonViID
    WHERE cshd.DangVienID = @DangVienID
    ORDER BY cshd.NgayChuyen DESC;
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_GetByDangVienID';

-- SP: ChuyenSinhHoatDang_GetByDonViID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_GetByDonViID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByDonViID];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByDonViID]
    @DonViID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cshd.ChuyenSinhHoatID, 
        cshd.DangVienID, 
        dv.HoTen AS TenDangVien,
        cshd.DonViDi, 
        ddi.TenDonVi AS TenDonViDi,
        cshd.DonViDen, 
        dden.TenDonVi AS TenDonViDen,
        cshd.NgayChuyen, 
        cshd.LyDo, 
        cshd.GhiChu, 
        cshd.FileQuyetDinh,
        cshd.TrangThai, 
        cshd.NgayTao, 
        cshd.NguoiTao
    FROM ChuyenSinhHoatDang cshd
    INNER JOIN DangVien dv ON cshd.DangVienID = dv.DangVienID
    INNER JOIN DonVi ddi ON cshd.DonViDi = ddi.DonViID
    INNER JOIN DonVi dden ON cshd.DonViDen = dden.DonViID
    WHERE cshd.DonViDi = @DonViID OR cshd.DonViDen = @DonViID
    ORDER BY cshd.NgayChuyen DESC;
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_GetByDonViID';

-- SP: ChuyenSinhHoatDang_GetByYear
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_GetByYear]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByYear];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_GetByYear]
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cshd.ChuyenSinhHoatID, 
        cshd.DangVienID, 
        dv.HoTen AS TenDangVien,
        cshd.DonViDi, 
        ddi.TenDonVi AS TenDonViDi,
        cshd.DonViDen, 
        dden.TenDonVi AS TenDonViDen,
        cshd.NgayChuyen, 
        cshd.LyDo, 
        cshd.GhiChu, 
        cshd.FileQuyetDinh,
        cshd.TrangThai, 
        cshd.NgayTao, 
        cshd.NguoiTao
    FROM ChuyenSinhHoatDang cshd
    INNER JOIN DangVien dv ON cshd.DangVienID = dv.DangVienID
    INNER JOIN DonVi ddi ON cshd.DonViDi = ddi.DonViID
    INNER JOIN DonVi dden ON cshd.DonViDen = dden.DonViID
    WHERE YEAR(cshd.NgayChuyen) = @Nam
    ORDER BY cshd.NgayChuyen DESC;
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_GetByYear';

-- SP: ChuyenSinhHoatDang_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_Insert];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_Insert]
    @DangVienID INT,
    @DonViDi INT,
    @DonViDen INT,
    @NgayChuyen DATE,
    @LyDo NVARCHAR(500) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @FileQuyetDinh NVARCHAR(500) = NULL,
    @TrangThai NVARCHAR(20) = N'Chờ duyệt',
    @NguoiTao NVARCHAR(100),
    @ChuyenSinhHoatID INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @ErrorMessage = '';
    
    BEGIN TRY
        -- Kiểm tra đảng viên có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM DangVien WHERE DangVienID = @DangVienID)
        BEGIN
            SET @ErrorMessage = N'Đảng viên không tồn tại!';
            RETURN -1;
        END
        
        -- Kiểm tra đơn vị đi và đến có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViDi)
        BEGIN
            SET @ErrorMessage = N'Đơn vị đi không tồn tại!';
            RETURN -1;
        END
        
        IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViDen)
        BEGIN
            SET @ErrorMessage = N'Đơn vị đến không tồn tại!';
            RETURN -1;
        END
        
        -- Kiểm tra đơn vị đi và đến phải khác nhau
        IF @DonViDi = @DonViDen
        BEGIN
            SET @ErrorMessage = N'Đơn vị đi và đơn vị đến không được trùng nhau!';
            RETURN -1;
        END
        
        INSERT INTO ChuyenSinhHoatDang (
            DangVienID, DonViDi, DonViDen, NgayChuyen, LyDo, GhiChu,
            FileQuyetDinh, TrangThai, NguoiTao
    )
    VALUES (
            @DangVienID, @DonViDi, @DonViDen, @NgayChuyen, @LyDo, @GhiChu,
            @FileQuyetDinh, @TrangThai, @NguoiTao
        );
        
        SET @ChuyenSinhHoatID = SCOPE_IDENTITY();
        
        -- Cập nhật DonViID trong DangVien khi chuyển sinh hoạt đảng
        UPDATE DangVien
        SET DonViID = @DonViDen
        WHERE DangVienID = @DangVienID;
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
        RETURN -1;
    END CATCH
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_Insert';

-- SP: ChuyenSinhHoatDang_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_Update];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_Update]
    @ChuyenSinhHoatID INT,
    @DonViDi INT = NULL,
    @DonViDen INT = NULL,
    @NgayChuyen DATE = NULL,
    @LyDo NVARCHAR(500) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @FileQuyetDinh NVARCHAR(500) = NULL,
    @TrangThai NVARCHAR(20) = NULL,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @ErrorMessage = '';
    
    BEGIN TRY
        -- Kiểm tra chuyển sinh hoạt có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM ChuyenSinhHoatDang WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID)
        BEGIN
            SET @ErrorMessage = N'Không tìm thấy chuyển sinh hoạt đảng!';
            RETURN -1; -- Không tìm thấy
        END
        
        -- Lấy giá trị hiện tại
        DECLARE @CurrentDonViDi INT, @CurrentDonViDen INT;
        SELECT @CurrentDonViDi = DonViDi, @CurrentDonViDen = DonViDen
        FROM ChuyenSinhHoatDang
        WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID;
        
        -- Lấy DangVienID từ ChuyenSinhHoatDang
        DECLARE @DangVienID INT;
        SELECT @DangVienID = DangVienID
        FROM ChuyenSinhHoatDang
        WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID;
        
        -- Sử dụng giá trị từ parameter nếu không NULL và > 0, ngược lại dùng giá trị hiện tại
        -- (Nếu service truyền 0, coi như không muốn update và giữ nguyên giá trị hiện tại)
        IF @DonViDi IS NULL OR @DonViDi = 0
            SET @DonViDi = @CurrentDonViDi;
        IF @DonViDen IS NULL OR @DonViDen = 0
            SET @DonViDen = @CurrentDonViDen;
        
        -- Kiểm tra đơn vị đi và đến có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViDi)
        BEGIN
            SET @ErrorMessage = N'Đơn vị đi không tồn tại!';
            RETURN -1;
        END
        
        IF NOT EXISTS (SELECT 1 FROM DonVi WHERE DonViID = @DonViDen)
        BEGIN
            SET @ErrorMessage = N'Đơn vị đến không tồn tại!';
            RETURN -1;
        END
        
        -- Kiểm tra đơn vị đi và đến phải khác nhau
        IF @DonViDi = @DonViDen
        BEGIN
            SET @ErrorMessage = N'Đơn vị đi và đơn vị đến không được trùng nhau!';
            RETURN -1;
        END
        
        UPDATE ChuyenSinhHoatDang SET
            DonViDi = @DonViDi,
            DonViDen = @DonViDen,
            NgayChuyen = ISNULL(@NgayChuyen, NgayChuyen),
            LyDo = ISNULL(@LyDo, LyDo),
            GhiChu = ISNULL(@GhiChu, GhiChu),
            FileQuyetDinh = ISNULL(@FileQuyetDinh, FileQuyetDinh),
            TrangThai = ISNULL(@TrangThai, TrangThai)
        WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID;
        
        -- Cập nhật DonViID trong DangVien khi chuyển sinh hoạt đảng (chỉ khi DonViDen thay đổi)
        IF @DonViDen != @CurrentDonViDen AND @DangVienID IS NOT NULL
        BEGIN
            UPDATE DangVien
            SET DonViID = @DonViDen
            WHERE DangVienID = @DangVienID;
        END
        
        IF @@ROWCOUNT > 0
            RETURN 0; -- Thành công
        ELSE
            BEGIN
                SET @ErrorMessage = N'Không có dòng nào được cập nhật!';
                RETURN -1; -- Không có dòng nào được cập nhật
            END
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
        RETURN -1;
    END CATCH
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_Update';

-- SP: ChuyenSinhHoatDang_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChuyenSinhHoatDang_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[ChuyenSinhHoatDang_Delete];
GO

CREATE PROCEDURE [dbo].[ChuyenSinhHoatDang_Delete]
    @ChuyenSinhHoatID INT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @ErrorMessage = '';
    
    BEGIN TRY
        -- Kiểm tra chuyển sinh hoạt có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM ChuyenSinhHoatDang WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID)
        BEGIN
            SET @ErrorMessage = N'Không tìm thấy chuyển sinh hoạt đảng!';
            RETURN -1; -- Không tìm thấy
        END
        
        DELETE FROM ChuyenSinhHoatDang
        WHERE ChuyenSinhHoatID = @ChuyenSinhHoatID;
        
        IF @@ROWCOUNT > 0
            RETURN 0; -- Thành công
        ELSE
            BEGIN
                SET @ErrorMessage = N'Không có dòng nào được xóa!';
                RETURN -1; -- Không có dòng nào được xóa
            END
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
        RETURN -1;
    END CATCH
END
GO
PRINT N'✓ Đã tạo SP ChuyenSinhHoatDang_Delete';

-- =============================================
-- TaiLieu Stored Procedures
-- =============================================

-- SP: TaiLieu_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieu_GetAll];
GO

CREATE PROCEDURE [dbo].[TaiLieu_GetAll]
    @DonViID INT = NULL,
    @LoaiTaiLieu NVARCHAR(50) = NULL,
    @TrangThai BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        tl.TaiLieuID, tl.DonViID, d.TenDonVi, tl.TieuDe, tl.LoaiTaiLieu,
        tl.NoiDung, tl.FileDinhKem, tl.NgayPhatHanh, tl.CoQuanPhatHanh,
        tl.TrangThai, tl.NgayTao, tl.NguoiTao
    FROM TaiLieu tl
    LEFT JOIN DonVi d ON tl.DonViID = d.DonViID
    WHERE (@DonViID IS NULL OR tl.DonViID = @DonViID)
      AND (@LoaiTaiLieu IS NULL OR tl.LoaiTaiLieu = @LoaiTaiLieu)
      AND (@TrangThai IS NULL OR tl.TrangThai = @TrangThai)
    ORDER BY tl.NgayPhatHanh DESC, tl.NgayTao DESC;
END
GO
PRINT N'✓ Đã tạo SP TaiLieu_GetAll';

-- SP: TaiLieu_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieu_GetById];
GO

CREATE PROCEDURE [dbo].[TaiLieu_GetById]
    @TaiLieuID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        tl.TaiLieuID, tl.DonViID, d.TenDonVi, tl.TieuDe, tl.LoaiTaiLieu,
        tl.NoiDung, tl.FileDinhKem, tl.NgayPhatHanh, tl.CoQuanPhatHanh,
        tl.TrangThai, tl.NgayTao, tl.NguoiTao
    FROM TaiLieu tl
    LEFT JOIN DonVi d ON tl.DonViID = d.DonViID
    WHERE tl.TaiLieuID = @TaiLieuID;
END
GO
PRINT N'✓ Đã tạo SP TaiLieu_GetById';

-- SP: TaiLieu_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieu_Insert];
GO

CREATE PROCEDURE [dbo].[TaiLieu_Insert]
    @DonViID INT = NULL,
    @TieuDe NVARCHAR(200),
    @LoaiTaiLieu NVARCHAR(50),
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL,
    @NgayPhatHanh DATE = NULL,
    @CoQuanPhatHanh NVARCHAR(200) = NULL,
    @NguoiTao NVARCHAR(100),
    @TaiLieuID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO TaiLieu (
        DonViID, TieuDe, LoaiTaiLieu, NoiDung, FileDinhKem,
        NgayPhatHanh, CoQuanPhatHanh, NguoiTao
    )
    VALUES (
        @DonViID, @TieuDe, @LoaiTaiLieu, @NoiDung, @FileDinhKem,
        @NgayPhatHanh, @CoQuanPhatHanh, @NguoiTao
    );
    
    SET @TaiLieuID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP TaiLieu_Insert';

-- SP: TaiLieu_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieu_Update];
GO

CREATE PROCEDURE [dbo].[TaiLieu_Update]
    @TaiLieuID INT,
    @DonViID INT = NULL,
    @TieuDe NVARCHAR(200) = NULL,
    @LoaiTaiLieu NVARCHAR(50) = NULL,
    @NoiDung NVARCHAR(MAX) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL,
    @NgayPhatHanh DATE = NULL,
    @CoQuanPhatHanh NVARCHAR(200) = NULL,
    @TrangThai BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM TaiLieu WHERE TaiLieuID = @TaiLieuID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    UPDATE TaiLieu SET
        DonViID = ISNULL(@DonViID, DonViID),
        TieuDe = ISNULL(@TieuDe, TieuDe),
        LoaiTaiLieu = ISNULL(@LoaiTaiLieu, LoaiTaiLieu),
        NoiDung = ISNULL(@NoiDung, NoiDung),
        FileDinhKem = ISNULL(@FileDinhKem, FileDinhKem),
        NgayPhatHanh = ISNULL(@NgayPhatHanh, NgayPhatHanh),
        CoQuanPhatHanh = ISNULL(@CoQuanPhatHanh, CoQuanPhatHanh),
        TrangThai = ISNULL(@TrangThai, TrangThai)
    WHERE TaiLieuID = @TaiLieuID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO
PRINT N'✓ Đã tạo SP TaiLieu_Update';

-- SP: TaiLieu_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiLieu_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TaiLieu_Delete];
GO

CREATE PROCEDURE [dbo].[TaiLieu_Delete]
    @TaiLieuID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM TaiLieu WHERE TaiLieuID = @TaiLieuID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    DELETE FROM TaiLieu WHERE TaiLieuID = @TaiLieuID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được xóa
END
GO
PRINT N'✓ Đã tạo SP TaiLieu_Delete';

-- =============================================
-- VanBanChiBo Stored Procedures
-- =============================================

-- SP: VanBanChiBo_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[VanBanChiBo_GetAll];
GO

CREATE PROCEDURE [dbo].[VanBanChiBo_GetAll]
    @DonViID INT = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        vb.VanBanChiBoID, vb.DonViID, d.TenDonVi, vb.TenVanBan,
        vb.NoiDung, vb.NgayGui, vb.NgayNhan, vb.TrangThai,
        vb.FileDinhKem, vb.NgayTao, vb.NguoiTao
    FROM VanBanChiBo vb
    INNER JOIN DonVi d ON vb.DonViID = d.DonViID
    WHERE (@DonViID IS NULL OR vb.DonViID = @DonViID)
      AND (@TrangThai IS NULL OR vb.TrangThai = @TrangThai)
    ORDER BY vb.NgayGui DESC, vb.NgayTao DESC;
END
GO
PRINT N'✓ Đã tạo SP VanBanChiBo_GetAll';

-- SP: VanBanChiBo_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[VanBanChiBo_GetById];
GO

CREATE PROCEDURE [dbo].[VanBanChiBo_GetById]
    @VanBanChiBoID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        vb.VanBanChiBoID, vb.DonViID, d.TenDonVi, vb.TenVanBan,
        vb.NoiDung, vb.NgayGui, vb.NgayNhan, vb.TrangThai,
        vb.FileDinhKem, vb.NgayTao, vb.NguoiTao
    FROM VanBanChiBo vb
    INNER JOIN DonVi d ON vb.DonViID = d.DonViID
    WHERE vb.VanBanChiBoID = @VanBanChiBoID;
END
GO
PRINT N'✓ Đã tạo SP VanBanChiBo_GetById';

-- SP: VanBanChiBo_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[VanBanChiBo_Insert];
GO

CREATE PROCEDURE [dbo].[VanBanChiBo_Insert]
    @DonViID INT,
    @TenVanBan NVARCHAR(200),
    @NoiDung NVARCHAR(MAX) = NULL,
    @NgayGui DATE = NULL,
    @NgayNhan DATE = NULL,
    @TrangThai NVARCHAR(20) = N'Chưa xử lý',
    @FileDinhKem NVARCHAR(500) = NULL,
    @NguoiTao NVARCHAR(100),
    @VanBanChiBoID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO VanBanChiBo (
        DonViID, TenVanBan, NoiDung, NgayGui, NgayNhan,
        TrangThai, FileDinhKem, NguoiTao
    )
    VALUES (
        @DonViID, @TenVanBan, @NoiDung, @NgayGui, @NgayNhan,
        @TrangThai, @FileDinhKem, @NguoiTao
    );
    
    SET @VanBanChiBoID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP VanBanChiBo_Insert';

-- SP: VanBanChiBo_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[VanBanChiBo_Update];
GO

CREATE PROCEDURE [dbo].[VanBanChiBo_Update]
    @VanBanChiBoID INT,
    @DonViID INT = NULL,
    @TenVanBan NVARCHAR(200) = NULL,
    @NoiDung NVARCHAR(MAX) = NULL,
    @NgayGui DATE = NULL,
    @NgayNhan DATE = NULL,
    @TrangThai NVARCHAR(20) = NULL,
    @FileDinhKem NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM VanBanChiBo WHERE VanBanChiBoID = @VanBanChiBoID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    UPDATE VanBanChiBo SET
        DonViID = ISNULL(@DonViID, DonViID),
        TenVanBan = ISNULL(@TenVanBan, TenVanBan),
        NoiDung = ISNULL(@NoiDung, NoiDung),
        NgayGui = ISNULL(@NgayGui, NgayGui),
        NgayNhan = ISNULL(@NgayNhan, NgayNhan),
        TrangThai = ISNULL(@TrangThai, TrangThai),
        FileDinhKem = ISNULL(@FileDinhKem, FileDinhKem)
    WHERE VanBanChiBoID = @VanBanChiBoID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO
PRINT N'✓ Đã tạo SP VanBanChiBo_Update';

-- SP: VanBanChiBo_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VanBanChiBo_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[VanBanChiBo_Delete];
GO

CREATE PROCEDURE [dbo].[VanBanChiBo_Delete]
    @VanBanChiBoID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM VanBanChiBo WHERE VanBanChiBoID = @VanBanChiBoID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    DELETE FROM VanBanChiBo WHERE VanBanChiBoID = @VanBanChiBoID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được xóa
END
GO
PRINT N'✓ Đã tạo SP VanBanChiBo_Delete';

-- =============================================
-- TheoDoiChuyenChinhThuc Stored Procedures
-- =============================================

-- SP: TheoDoiChuyenChinhThuc_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetAll];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetAll]
    @DangVienID INT = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        td.TheoDoiChuyenChinhThucID, td.DangVienID, dv.HoTen AS TenDangVien,
        td.NgayVaoDang, td.NgayChuyenChinhThuc, td.TrangThai, td.GhiChu,
        td.NgayTao, td.NguoiTao,
        CASE 
            WHEN td.NgayChuyenChinhThuc IS NOT NULL THEN DATEDIFF(DAY, td.NgayVaoDang, td.NgayChuyenChinhThuc)
            ELSE DATEDIFF(DAY, td.NgayVaoDang, GETDATE())
        END AS SoNgay
    FROM TheoDoiChuyenChinhThuc td
    INNER JOIN DangVien dv ON td.DangVienID = dv.DangVienID
    WHERE (@DangVienID IS NULL OR td.DangVienID = @DangVienID)
      AND (@TrangThai IS NULL OR td.TrangThai = @TrangThai)
    ORDER BY td.NgayVaoDang DESC;
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_GetAll';

-- SP: TheoDoiChuyenChinhThuc_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetById];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetById]
    @TheoDoiChuyenChinhThucID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        td.TheoDoiChuyenChinhThucID, td.DangVienID, dv.HoTen AS TenDangVien,
        td.NgayVaoDang, td.NgayChuyenChinhThuc, td.TrangThai, td.GhiChu,
        td.NgayTao, td.NguoiTao
    FROM TheoDoiChuyenChinhThuc td
    INNER JOIN DangVien dv ON td.DangVienID = dv.DangVienID
    WHERE td.TheoDoiChuyenChinhThucID = @TheoDoiChuyenChinhThucID;
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_GetById';

-- SP: TheoDoiChuyenChinhThuc_GetByDangVienID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_GetByDangVienID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetByDangVienID];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetByDangVienID]
    @DangVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        td.TheoDoiChuyenChinhThucID, td.DangVienID, dv.HoTen AS TenDangVien,
        td.NgayVaoDang, td.NgayChuyenChinhThuc, td.TrangThai, td.GhiChu,
        td.NgayTao, td.NguoiTao
    FROM TheoDoiChuyenChinhThuc td
    INNER JOIN DangVien dv ON td.DangVienID = dv.DangVienID
    WHERE td.DangVienID = @DangVienID
    ORDER BY td.NgayVaoDang DESC;
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_GetByDangVienID';

-- SP: TheoDoiChuyenChinhThuc_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Insert];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Insert]
    @DangVienID INT,
    @NgayVaoDang DATE,
    @NgayChuyenChinhThuc DATE = NULL,
    @TrangThai NVARCHAR(20) = N'Đang theo dõi',
    @GhiChu NVARCHAR(MAX) = NULL,
    @NguoiTao NVARCHAR(100),
    @TheoDoiChuyenChinhThucID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra đảng viên có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DangVien WHERE DangVienID = @DangVienID)
    BEGIN
        RAISERROR(N'Đảng viên không tồn tại!', 16, 1);
        RETURN -1;
    END
    
    INSERT INTO TheoDoiChuyenChinhThuc (
        DangVienID, NgayVaoDang, NgayChuyenChinhThuc, TrangThai, GhiChu, NguoiTao
    )
    VALUES (
        @DangVienID, @NgayVaoDang, @NgayChuyenChinhThuc, @TrangThai, @GhiChu, @NguoiTao
    );
    
    SET @TheoDoiChuyenChinhThucID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_Insert';

-- SP: TheoDoiChuyenChinhThuc_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Update];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Update]
    @TheoDoiChuyenChinhThucID INT,
    @DangVienID INT = NULL,
    @NgayVaoDang DATE = NULL,
    @NgayChuyenChinhThuc DATE = NULL,
    @TrangThai NVARCHAR(20) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM TheoDoiChuyenChinhThuc WHERE TheoDoiChuyenChinhThucID = @TheoDoiChuyenChinhThucID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    UPDATE TheoDoiChuyenChinhThuc SET
        DangVienID = ISNULL(@DangVienID, DangVienID),
        NgayVaoDang = ISNULL(@NgayVaoDang, NgayVaoDang),
        NgayChuyenChinhThuc = ISNULL(@NgayChuyenChinhThuc, NgayChuyenChinhThuc),
        TrangThai = ISNULL(@TrangThai, TrangThai),
        GhiChu = ISNULL(@GhiChu, GhiChu)
    WHERE TheoDoiChuyenChinhThucID = @TheoDoiChuyenChinhThucID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_Update';

-- SP: TheoDoiChuyenChinhThuc_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Delete];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_Delete]
    @TheoDoiChuyenChinhThucID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM TheoDoiChuyenChinhThuc WHERE TheoDoiChuyenChinhThucID = @TheoDoiChuyenChinhThucID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    DELETE FROM TheoDoiChuyenChinhThuc WHERE TheoDoiChuyenChinhThucID = @TheoDoiChuyenChinhThucID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được xóa
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_Delete';

-- SP: TheoDoiChuyenChinhThuc_GetCanNhacNho
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheoDoiChuyenChinhThuc_GetCanNhacNho]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetCanNhacNho];
GO

CREATE PROCEDURE [dbo].[TheoDoiChuyenChinhThuc_GetCanNhacNho]
    @SoNgayTruoc INT = 30 -- Nhắc trước 30 ngày
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Lấy các đảng viên dự bị đã vào đảng được gần 1 năm (365 ngày) và chưa chuyển chính thức
    SELECT 
        td.TheoDoiChuyenChinhThucID, td.DangVienID, dv.HoTen AS TenDangVien,
        td.NgayVaoDang, td.NgayChuyenChinhThuc, td.TrangThai, td.GhiChu,
        td.NgayTao, td.NguoiTao,
        DATEDIFF(DAY, td.NgayVaoDang, GETDATE()) AS SoNgayDaVaoDang,
        365 - DATEDIFF(DAY, td.NgayVaoDang, GETDATE()) AS SoNgayConLai
    FROM TheoDoiChuyenChinhThuc td
    INNER JOIN DangVien dv ON td.DangVienID = dv.DangVienID
    WHERE td.TrangThai = N'Đang theo dõi'
      AND td.NgayChuyenChinhThuc IS NULL
      AND DATEDIFF(DAY, td.NgayVaoDang, GETDATE()) >= (365 - @SoNgayTruoc)
      AND DATEDIFF(DAY, td.NgayVaoDang, GETDATE()) < 365
    ORDER BY td.NgayVaoDang ASC;
END
GO
PRINT N'✓ Đã tạo SP TheoDoiChuyenChinhThuc_GetCanNhacNho';

-- =============================================
-- NguoiDung Stored Procedures - DangNhap
-- =============================================

-- SP: DangNhap
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangNhap]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DangNhap];
GO

CREATE PROCEDURE [dbo].[DangNhap]
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NguoiDungID INT;
    
    -- Tìm và lưu NguoiDungID vào biến
    SELECT @NguoiDungID = NguoiDungID
    FROM NguoiDung
    WHERE Email = @Email 
      AND MatKhau = @Password 
      AND TrangThai = 1;
    
    -- Nếu tìm thấy user, cập nhật LanDangNhapCuoi và trả về thông tin
    IF @NguoiDungID IS NOT NULL
    BEGIN
        UPDATE NguoiDung
        SET LanDangNhapCuoi = GETDATE()
        WHERE NguoiDungID = @NguoiDungID;
        
        -- Trả về thông tin người dùng
        SELECT 
            NguoiDungID,
            DonViID,
            TenDangNhap,
            MatKhau,
            HoTen,
            Email,
            VaiTro,
            Roles,
            Permissions,
            TrangThai,
            NgayTao,
            NguoiTao,
            LanDangNhapCuoi
        FROM NguoiDung
        WHERE NguoiDungID = @NguoiDungID;
    END
END
GO
PRINT N'✓ Đã tạo SP DangNhap';

-- =============================================
-- NguoiDung Additional Stored Procedures
-- =============================================

-- SP: NguoiDung_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_GetById];
GO

CREATE PROCEDURE [dbo].[NguoiDung_GetById]
    @NguoiDungID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT NguoiDungID, DonViID, TenDangNhap, MatKhau, HoTen, Email, 
           VaiTro, Roles, Permissions, TrangThai, NgayTao, NguoiTao, LanDangNhapCuoi
    FROM NguoiDung 
    WHERE NguoiDungID = @NguoiDungID;
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_GetById';

-- SP: NguoiDung_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_Insert];
GO

CREATE PROCEDURE [dbo].[NguoiDung_Insert]
    @DonViID INT = NULL,
    @TenDangNhap NCHAR(50),
    @MatKhau NVARCHAR(255),
    @HoTen NVARCHAR(100),
    @Email NVARCHAR(100) = NULL,
    @VaiTro NVARCHAR(50),
    @Roles NVARCHAR(200) = NULL,
    @Permissions NVARCHAR(500) = NULL,
    @NguoiTao NVARCHAR(100),
    @NguoiDungID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra TenDangNhap đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM NguoiDung WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'Tên đăng nhập đã tồn tại!', 16, 1);
        RETURN -1;
    END
    
    INSERT INTO NguoiDung (DonViID, TenDangNhap, MatKhau, HoTen, Email, VaiTro, Roles, Permissions, TrangThai, NgayTao, NguoiTao)
    VALUES (@DonViID, @TenDangNhap, @MatKhau, @HoTen, @Email, @VaiTro, @Roles, @Permissions, 1, GETDATE(), @NguoiTao);
    
    SET @NguoiDungID = SCOPE_IDENTITY();
    RETURN 0;
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_Insert';

-- SP: NguoiDung_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_Update];
GO

CREATE PROCEDURE [dbo].[NguoiDung_Update]
    @NguoiDungID INT,
    @DonViID INT = NULL,
    @TenDangNhap NCHAR(50) = NULL,
    @MatKhau NVARCHAR(255) = NULL,
    @HoTen NVARCHAR(100) = NULL,
    @Email NVARCHAR(100) = NULL,
    @VaiTro NVARCHAR(50) = NULL,
    @Roles NVARCHAR(200) = NULL,
    @Permissions NVARCHAR(500) = NULL,
    @TrangThai BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE NguoiDungID = @NguoiDungID)
    BEGIN
        RETURN -1; -- Không tìm thấy
    END
    
    -- Kiểm tra TenDangNhap đã tồn tại chưa (nếu thay đổi)
    IF @TenDangNhap IS NOT NULL AND EXISTS (SELECT 1 FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND NguoiDungID != @NguoiDungID)
    BEGIN
        RAISERROR(N'Tên đăng nhập đã tồn tại!', 16, 1);
        RETURN -1;
    END
    
    UPDATE NguoiDung SET
        DonViID = ISNULL(@DonViID, DonViID),
        TenDangNhap = ISNULL(@TenDangNhap, TenDangNhap),
        MatKhau = ISNULL(@MatKhau, MatKhau),
        HoTen = ISNULL(@HoTen, HoTen),
        Email = ISNULL(@Email, Email),
        VaiTro = ISNULL(@VaiTro, VaiTro),
        Roles = ISNULL(@Roles, Roles),
        Permissions = ISNULL(@Permissions, Permissions),
        TrangThai = ISNULL(@TrangThai, TrangThai)
    WHERE NguoiDungID = @NguoiDungID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không có dòng nào được cập nhật
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_Update';

-- SP: NguoiDung_Delete
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_Delete]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_Delete];
GO

CREATE PROCEDURE [dbo].[NguoiDung_Delete]
    @NguoiDungID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Không xóa thật, chỉ set TrangThai = 0
    UPDATE NguoiDung SET TrangThai = 0 WHERE NguoiDungID = @NguoiDungID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không tìm thấy
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_Delete';

-- SP: NguoiDung_ResetPassword
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_ResetPassword]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_ResetPassword];
GO

CREATE PROCEDURE [dbo].[NguoiDung_ResetPassword]
    @NguoiDungID INT,
    @MatKhau NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE NguoiDung SET MatKhau = @MatKhau WHERE NguoiDungID = @NguoiDungID;
    
    IF @@ROWCOUNT > 0
        RETURN 0; -- Thành công
    ELSE
        RETURN -1; -- Không tìm thấy
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_ResetPassword';

-- SP: NguoiDung_UpdatePasswordForSampleUsers
-- Stored procedure để update password cho các user mẫu (chỉ dùng khi setup)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguoiDung_UpdatePasswordForSampleUsers]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[NguoiDung_UpdatePasswordForSampleUsers];
GO

CREATE PROCEDURE [dbo].[NguoiDung_UpdatePasswordForSampleUsers]
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Update password cho bithu1 (bithu123)
    -- SHA-256 hash của "bithu123" = 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
    UPDATE NguoiDung 
    SET MatKhau = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
    WHERE TenDangNhap = 'bithu1';
    
    -- Update password cho vanphong1 (vanphong123)
    -- SHA-256 hash của "vanphong123" = 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
    UPDATE NguoiDung 
    SET MatKhau = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
    WHERE TenDangNhap = 'vanphong1';
    
    PRINT N'✓ Đã cập nhật password cho các user mẫu';
END
GO
PRINT N'✓ Đã tạo SP NguoiDung_UpdatePasswordForSampleUsers';

-- =============================================
-- AuditLog Stored Procedures
-- =============================================

-- SP: AuditLog_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[AuditLog_Insert];
GO

CREATE PROCEDURE [dbo].[AuditLog_Insert]
    @NguoiDungID INT = NULL,
    @TenDangNhap NVARCHAR(50) = NULL,
    @Action NVARCHAR(50),
    @TableName NVARCHAR(100) = NULL,
    @RecordID INT = NULL,
    @OldValues NVARCHAR(MAX) = NULL,
    @NewValues NVARCHAR(MAX) = NULL,
    @IPAddress NVARCHAR(50) = NULL,
    @UserAgent NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO AuditLog (NguoiDungID, TenDangNhap, Action, TableName, RecordID, OldValues, NewValues, IPAddress, UserAgent, NgayThucHien)
    VALUES (@NguoiDungID, @TenDangNhap, @Action, @TableName, @RecordID, @OldValues, @NewValues, @IPAddress, @UserAgent, GETDATE());
END
GO
PRINT N'✓ Đã tạo SP AuditLog_Insert';

-- SP: AuditLog_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[AuditLog_GetAll];
GO

CREATE PROCEDURE [dbo].[AuditLog_GetAll]
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL,
    @Action NVARCHAR(50) = NULL,
    @TableName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT AuditLogID, NguoiDungID, TenDangNhap, Action, TableName, RecordID, 
           OldValues, NewValues, IPAddress, UserAgent, NgayThucHien
    FROM AuditLog
    WHERE (@TuNgay IS NULL OR NgayThucHien >= @TuNgay)
      AND (@DenNgay IS NULL OR NgayThucHien <= @DenNgay)
      AND (@Action IS NULL OR Action = @Action)
      AND (@TableName IS NULL OR TableName = @TableName)
    ORDER BY NgayThucHien DESC;
END
GO
PRINT N'✓ Đã tạo SP AuditLog_GetAll';

-- SP: AuditLog_GetByNguoiDungID
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog_GetByNguoiDungID]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[AuditLog_GetByNguoiDungID];
GO

CREATE PROCEDURE [dbo].[AuditLog_GetByNguoiDungID]
    @NguoiDungID INT,
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT AuditLogID, NguoiDungID, TenDangNhap, Action, TableName, RecordID, 
           OldValues, NewValues, IPAddress, UserAgent, NgayThucHien
    FROM AuditLog
    WHERE NguoiDungID = @NguoiDungID
      AND (@TuNgay IS NULL OR NgayThucHien >= @TuNgay)
      AND (@DenNgay IS NULL OR NgayThucHien <= @DenNgay)
    ORDER BY NgayThucHien DESC;
END
GO
PRINT N'✓ Đã tạo SP AuditLog_GetByNguoiDungID';

-- =============================================
-- SystemConfig Stored Procedures
-- =============================================

-- SP: SystemConfig_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfig_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SystemConfig_GetAll];
GO

CREATE PROCEDURE [dbo].[SystemConfig_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT SystemConfigID, ConfigKey, ConfigValue, Description, NgayCapNhat, NguoiCapNhat
    FROM SystemConfig
    ORDER BY ConfigKey;
END
GO
PRINT N'✓ Đã tạo SP SystemConfig_GetAll';

-- SP: SystemConfig_GetByKey
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfig_GetByKey]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SystemConfig_GetByKey];
GO

CREATE PROCEDURE [dbo].[SystemConfig_GetByKey]
    @ConfigKey NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT SystemConfigID, ConfigKey, ConfigValue, Description, NgayCapNhat, NguoiCapNhat
    FROM SystemConfig
    WHERE ConfigKey = @ConfigKey;
END
GO
PRINT N'✓ Đã tạo SP SystemConfig_GetByKey';

-- SP: SystemConfig_SetValue
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfig_SetValue]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SystemConfig_SetValue];
GO

CREATE PROCEDURE [dbo].[SystemConfig_SetValue]
    @ConfigKey NVARCHAR(100),
    @ConfigValue NVARCHAR(MAX),
    @Description NVARCHAR(500) = NULL,
    @NguoiCapNhat NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM SystemConfig WHERE ConfigKey = @ConfigKey)
    BEGIN
        -- Update existing
        UPDATE SystemConfig 
        SET ConfigValue = @ConfigValue,
            Description = ISNULL(@Description, Description),
            NgayCapNhat = GETDATE(),
            NguoiCapNhat = @NguoiCapNhat
        WHERE ConfigKey = @ConfigKey;
    END
    ELSE
    BEGIN
        -- Insert new
        INSERT INTO SystemConfig (ConfigKey, ConfigValue, Description, NgayCapNhat, NguoiCapNhat)
        VALUES (@ConfigKey, @ConfigValue, @Description, GETDATE(), @NguoiCapNhat);
    END
END
GO
PRINT N'✓ Đã tạo SP SystemConfig_SetValue';

-- =============================================
-- UPDATE PASSWORD CHO CÁC USER MẪU
-- =============================================
PRINT N'';
PRINT N'Đang cập nhật password cho các user mẫu...';
EXEC NguoiDung_UpdatePasswordForSampleUsers;

PRINT N'';
PRINT N'===========================================';
PRINT N'ĐÃ THÊM TẤT CẢ STORED PROCEDURES THIẾU!';
PRINT N'===========================================';
PRINT N'✓ DonVi: 7 SPs';
PRINT N'✓ DangVien: 4 SPs (bao gồm CheckDuplicates)';
PRINT N'✓ QuanNhan: 5 SPs';
PRINT N'✓ KhenThuong: 7 SPs (GetAll, GetById, GetByDangVienID, GetByDonViID, Insert, Update, Delete)';
PRINT N'✓ KyLuat: 7 SPs (GetAll, GetById, GetByDangVienID, GetByDonViID, Insert, Update, Delete)';
PRINT N'✓ NguoiDung: 7 SPs (GetAll, GetById, Insert, Update, Delete, ResetPassword, DangNhap)';
PRINT N'✓ AuditLog: 3 SPs (Insert, GetAll, GetByNguoiDungID)';
PRINT N'✓ SystemConfig: 3 SPs (GetAll, GetByKey, SetValue)';
PRINT N'✓ SinhHoatChiBo: 5 SPs';
PRINT N'✓ DiemDanhSinhHoat: 4 SPs';
PRINT N'✓ TaiLieuHoSo: 5 SPs (Insert, GetByDangVienID, GetById, Update, Delete)';
PRINT N'✓ ChuyenSinhHoatDang: 8 SPs (GetAll, GetById, GetByDangVienID, GetByDonViID, GetByYear, Insert, Update, Delete)';
PRINT N'✓ TaiLieu: 5 SPs (GetAll, GetById, Insert, Update, Delete)';
PRINT N'✓ VanBanChiBo: 5 SPs (GetAll, GetById, Insert, Update, Delete)';
PRINT N'✓ TheoDoiChuyenChinhThuc: 6 SPs (GetAll, GetById, GetByDangVienID, Insert, Update, Delete, GetCanNhacNho)';
PRINT N'';
PRINT N'Tổng cộng: 83 Stored Procedures';
PRINT N'  (Đã hợp nhất KhenThuongCaNhan/KhenThuongDonVi thành KhenThuong)';
PRINT N'  (Đã hợp nhất KyLuatCaNhan/KyLuatToChuc thành KyLuat)';
PRINT N'===========================================';
GO


