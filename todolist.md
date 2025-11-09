# TODO LIST KỸ THUẬT - PHẦN MỀM QUẢN LÝ ĐẢNG VIÊN

## 1. DANH SÁCH QUÂN NHÂN

### 1.1 Database & Models
- [ ] **Tạo bảng QuanNhan**
  - Tạo stored procedure `QuanNhan_CreateTable`
  - Thiết kế schema với các trường: TT, HoTen, NgaySinh, SHSQ, SoTheBHYT, SoCCCD, CapBac, ChucVu, DonViID, NhapNgu, NgayVaoDang, SoTheDang, Doan, DanToc, TonGiao, SucKhoe, NhomMau, HoTenChaNamSinh, HoTenMeNamSinh, HoTenVoConNamSinh, NgheNghiepChaMe, MayAnhChiEm, QueQuan, NoiO, KhiCanBaoTin, GhiChu
  - Tạo foreign key với bảng DonVi: `FK_QuanNhan_DonVi`
  - Tạo indexes cho các trường tìm kiếm
  - Tạo stored procedures: `QuanNhan_GetAll`, `QuanNhan_GetById`, `QuanNhan_Insert`, `QuanNhan_Update`, `QuanNhan_Delete`

- [ ] **Tạo Model QuanNhan.cs**
  - Tạo class QuanNhan với các properties theo PascalCase
  - Thêm DataAnnotations cho validation: `[Required]`, `[DisplayName]`
  - Thêm ControlType attributes cho form generation: `[ControlType(ControlInputType.ComboBox)]`
  - Thêm ComboBoxData attributes cho dropdown values

- [ ] **Tạo QuanNhanService.cs**
  - Method GetAll() với parameters filter
  - Method GetById(int id)
  - Method Create(QuanNhan model)
  - Method Update(QuanNhan model)
  - Method Delete(int id)
  - Method Search() với multiple criteria
  - Sử dụng stored procedures đã tạo

### 1.2 Form Danh sách quân nhân
- [ ] **Tạo UserControlQuanLyQuanNhan.cs**
  - Thiết kế DataGridView với custom columns
  - Implement virtual scrolling cho performance
  - Tạo context menu (Thêm, Sửa, Xóa, Xuất file)
  - Implement search/filter functionality
  - Sử dụng QuanNhanService để load data
  - **Sử dụng FormThem template**: `new FormThem(typeof(QuanNhan))`
  - **Sử dụng FormSua template**: `new FormSua(existingQuanNhan)`
  - **Sử dụng FormXemChiTiet template**: `new FormXemChiTiet(quanNhan)`

### 1.3 Chức năng xuất file
- [ ] **Tạo ExportService.cs**
  - Method ExportToExcel(List<QuanNhan> data)
  - Method ExportToWord(List<QuanNhan> data)
  - Method ExportToPDF(List<QuanNhan> data)
  - Sử dụng EPPlus cho Excel, DocumentFormat.OpenXml cho Word

- [ ] **Tạo PrintService.cs**
  - Method PrintList(List<QuanNhan> data)
  - Thiết kế report template
  - Implement print preview

---

## 2. QUẢN LÝ HỒ SƠ ĐẢNG VIÊN

### 2.1 Database & Models
- [ ] **Cập nhật bảng DangVien**
  - Thêm các trường còn thiếu: DiaChi, DanToc, TonGiao, NgheNghiep, TrinhDoHocVan, TrinhDoChuyenMon, LyLuanChinhTri, NgoaiNgu, TinHoc
  - Tạo indexes cho performance
  - Cập nhật stored procedures: `DangVien_Insert`, `DangVien_Update`

- [ ] **Tạo bảng DangVienAnh**
  - Trường: DangVienAnhID, DangVienID, AnhPath, AnhBinary, LoaiAnh, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_DangVienAnh_DangVien`
  - Stored procedures: `DangVienAnh_GetByDangVienID`, `DangVienAnh_Insert`, `DangVienAnh_Delete`

- [ ] **Tạo bảng QuaTrinhCongTac**
  - Trường: QuaTrinhCongTacID, DangVienID, ThoiGian, ChucVu, DonVi, MoTa, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_QuaTrinhCongTac_DangVien`
  - Stored procedures: `QuaTrinhCongTac_GetByDangVienID`, `QuaTrinhCongTac_Insert`, `QuaTrinhCongTac_Update`, `QuaTrinhCongTac_Delete`

- [ ] **Tạo bảng HoSoGiaDinh**
  - Trường: HoSoGiaDinhID, DangVienID, QuanHe, HoTen, NgaySinh, NgheNghiep, DiaChi, GhiChu, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_HoSoGiaDinh_DangVien`
  - Stored procedures: `HoSoGiaDinh_GetByDangVienID`, `HoSoGiaDinh_Insert`, `HoSoGiaDinh_Update`, `HoSoGiaDinh_Delete`

- [ ] **Tạo bảng KhenThuongKyLuat**
  - Trường: KhenThuongKyLuatID, DangVienID, Loai, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_KhenThuongKyLuat_DangVien`
  - Stored procedures: `KhenThuongKyLuat_GetByDangVienID`, `KhenThuongKyLuat_Insert`, `KhenThuongKyLuat_Update`, `KhenThuongKyLuat_Delete`

- [ ] **Tạo bảng TaiLieuHoSo**
  - Trường: TaiLieuHoSoID, DangVienID, TenFile, DuongDan, LoaiFile, KichThuoc, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_TaiLieuHoSo_DangVien`
  - Stored procedures: `TaiLieuHoSo_GetByDangVienID`, `TaiLieuHoSo_Insert`, `TaiLieuHoSo_Delete`

### 2.2 Form Danh sách đảng viên
- [ ] **Cập nhật UserControlQuanLyDangVien.cs**
  - Thêm khung ảnh đại diện với PictureBox
  - Implement image upload (3x4, 4x6)
  - Thêm các cột mới vào DataGridView
  - Implement advanced filtering
  - Sử dụng DangVienService.GetAll() với filters

- [ ] **Tạo FormChiTietDangVien.cs**
  - Tab 1: Thông tin cơ bản (sử dụng FormHelper)
  - Tab 2: Quá trình công tác (RichTextBox với CRUD)
  - Tab 3: Hồ sơ gia đình (DataGridView với CRUD)
  - Tab 4: Khen thưởng - Kỷ luật (DataGridView với CRUD)
  - Tab 5: Tài liệu hồ sơ (File list với preview)

- [ ] **Tạo FileManager.cs**
  - Method UploadFile(string path, byte[] data)
  - Method DownloadFile(int fileId)
  - Method DeleteFile(int fileId)
  - Method GetFileList(int dangVienId)
  - Sử dụng TaiLieuHoSoService

### 2.3 Chức năng xuất file
- [ ] **Tạo ReportService.cs**
  - Method GenerateWordReport(DangVien dangVien)
  - Method GeneratePDFReport(DangVien dangVien)
  - Sử dụng template Word với mail merge
  - Implement chart generation cho báo cáo

### 2.4 Form Thêm/Sửa/Xóa đảng viên
- [ ] **Cập nhật FormThem.cs** (đã có template)
  - Thêm validation cho các trường mới
  - Implement image upload
  - Multi-step form với validation
  - Sử dụng DangVienService.Create()

- [ ] **Cập nhật FormSua.cs** (đã có template)
  - Load existing images
  - Update image handling
  - Maintain data integrity
  - Sử dụng DangVienService.Update()

### 2.5 Form Mẫu T63
- [ ] **Tạo FormMauT63.cs**
  - Thiết kế form theo mẫu T63
  - Implement data binding
  - Validation theo quy định
  - Sử dụng FormHelper cho dynamic form

- [ ] **Tạo MauT63Service.cs**
  - Method GenerateT63Report(DangVien dangVien)
  - Method PrintT63(DangVien dangVien)
  - Template management
  - Sử dụng ReportService

---

## 3. QUẢN LÝ HỒ SƠ ĐỠN VỊ

### 3.1 Database & Models
- [ ] **Cập nhật bảng DonVi**
  - Thêm trường: TenDayDu, DiaChi, SoDienThoai, Email, MoTa
  - Tạo indexes
  - Cập nhật stored procedures: `DonVi_Insert`, `DonVi_Update`

- [ ] **Tạo bảng CapUy**
  - Trường: CapUyID, DonViID, DangVienID, ChucVu, NgayBatDau, NgayKetThuc, TrangThai, NgayTao, NguoiTao
  - Foreign keys với DonVi và DangVien: `FK_CapUy_DonVi`, `FK_CapUy_DangVien`
  - Stored procedures: `CapUy_GetByDonViID`, `CapUy_Insert`, `CapUy_Update`, `CapUy_Delete`

- [ ] **Tạo bảng DonViKhenThuong**
  - Trường: DonViKhenThuongID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DonVi: `FK_DonViKhenThuong_DonVi`
  - Stored procedures: `DonViKhenThuong_GetByDonViID`, `DonViKhenThuong_Insert`, `DonViKhenThuong_Update`, `DonViKhenThuong_Delete`

### 3.2 Form Danh sách chi bộ, đơn vị
- [ ] **Tạo UserControlQuanLyDonVi.cs**
  - DataGridView với custom columns
  - Aggregate data (TongSoDangVien, CapUy info)
  - Context menu actions
  - Sử dụng DonViService.GetAll()
  - **Sử dụng FormThem template**: `new FormThem(typeof(DonVi))`
  - **Sử dụng FormSua template**: `new FormSua(existingDonVi)`
  - **Sử dụng FormXemChiTiet template**: `new FormXemChiTiet(donVi)`

### 3.3 Form Chi tiết chi bộ
- [ ] **Tạo FormChiTietChiBo.cs**
  - Group by DonVi
  - Hierarchical display
  - Drill-down functionality
  - Sử dụng DonViService và DangVienService

### 3.4 Form Sinh hoạt chi bộ
- [ ] **Tạo bảng SinhHoatChiBo**
  - Trường: SinhHoatChiBoID, DonViID, NgaySinhHoat, NoiDung, NghiQuyet, ThamGia, VangMat, GhiChu, NgayTao, NguoiTao
  - Foreign key với DonVi: `FK_SinhHoatChiBo_DonVi`
  - Stored procedures: `SinhHoatChiBo_GetByDonViID`, `SinhHoatChiBo_Insert`, `SinhHoatChiBo_Update`, `SinhHoatChiBo_Delete`

- [ ] **Tạo FormSinhHoatChiBo.cs**
  - Calendar control cho lịch sinh hoạt
  - RichTextBox cho biên bản
  - Attendance tracking
  - Sử dụng SinhHoatChiBoService

### 3.5 Form Thêm/Sửa/Xóa đơn vị
- [ ] **Sử dụng FormThem template** (đã có)
  - Dynamic form generation (sử dụng FormHelper)
  - Validation rules
  - Parent-child relationship
  - Sử dụng DonViService.Create()

---

## 4. CHUYỂN SINH HOẠT ĐẢNG

### 4.1 Database & Models
- [ ] **Tạo bảng ChuyenSinhHoat**
  - Trường: ChuyenSinhHoatID, DangVienID, DonViDi, DonViDen, NgayChuyen, LyDo, GhiChu, FileQuyetDinh, NgayTao, NguoiTao
  - Foreign keys với DangVien và DonVi: `FK_ChuyenSinhHoat_DangVien`, `FK_ChuyenSinhHoat_DonViDi`, `FK_ChuyenSinhHoat_DonViDen`
  - Stored procedures: `ChuyenSinhHoat_GetAll`, `ChuyenSinhHoat_GetByDangVienID`, `ChuyenSinhHoat_Insert`, `ChuyenSinhHoat_Update`, `ChuyenSinhHoat_Delete`

- [ ] **Tạo ChuyenSinhHoatService.cs**
  - CRUD operations
  - Validation logic
  - File handling
  - Sử dụng stored procedures đã tạo

### 4.2 Form Chuyển sinh hoạt
- [ ] **Tạo FormChuyenSinhHoat.cs**
  - ComboBox cho đảng viên (searchable)
  - Auto-populate thông tin đảng viên
  - File upload cho quyết định
  - Validation rules
  - Sử dụng ChuyenSinhHoatService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(ChuyenSinhHoat))`

### 4.3 Form Lịch sử chuyển sinh hoạt
- [ ] **Tạo FormLichSuChuyenSinhHoat.cs**
  - DataGridView với filtering
  - Export functionality
  - Timeline view
  - Sử dụng ChuyenSinhHoatService.GetAll()

---

## 5. THI ĐUA - KHEN THƯỞNG

### 5.1 Database & Models
- [ ] **Tạo bảng KhenThuongCaNhan**
  - Trường: KhenThuongCaNhanID, DangVienID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_KhenThuongCaNhan_DangVien`
  - Stored procedures: `KhenThuongCaNhan_GetByDangVienID`, `KhenThuongCaNhan_Insert`, `KhenThuongCaNhan_Update`, `KhenThuongCaNhan_Delete`

- [ ] **Tạo bảng KhenThuongDonVi**
  - Trường: KhenThuongDonViID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DonVi: `FK_KhenThuongDonVi_DonVi`
  - Stored procedures: `KhenThuongDonVi_GetByDonViID`, `KhenThuongDonVi_Insert`, `KhenThuongDonVi_Update`, `KhenThuongDonVi_Delete`

- [ ] **Tạo bảng KyLuatCaNhan**
  - Trường: KyLuatCaNhanID, DangVienID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_KyLuatCaNhan_DangVien`
  - Stored procedures: `KyLuatCaNhan_GetByDangVienID`, `KyLuatCaNhan_Insert`, `KyLuatCaNhan_Update`, `KyLuatCaNhan_Delete`

- [ ] **Tạo bảng KyLuatToChuc**
  - Trường: KyLuatToChucID, DonViID, HinhThuc, Ngay, SoQuyetDinh, CapQuyetDinh, NoiDung, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DonVi: `FK_KyLuatToChuc_DonVi`
  - Stored procedures: `KyLuatToChuc_GetByDonViID`, `KyLuatToChuc_Insert`, `KyLuatToChuc_Update`, `KyLuatToChuc_Delete`

### 5.2 Form Khen thưởng cá nhân
- [ ] **Tạo FormKhenThuongCaNhan.cs**
  - ComboBox cho đảng viên
  - Dropdown với predefined values
  - File upload functionality
  - Validation và save logic
  - Sử dụng KhenThuongCaNhanService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(KhenThuongCaNhan))`

### 5.3 Form Khen thưởng Đơn vị
- [ ] **Tạo FormKhenThuongDonVi.cs**
  - ComboBox cho đơn vị
  - Dropdown với predefined values
  - Similar structure to cá nhân
  - Sử dụng KhenThuongDonViService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(KhenThuongDonVi))`

### 5.4 Form Kỷ luật cá nhân
- [ ] **Tạo FormKyLuatCaNhan.cs**
  - Conditional dropdown based on LoaiDangVien
  - RichTextBox cho nội dung
  - File upload
  - Sử dụng KyLuatCaNhanService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(KyLuatCaNhan))`

### 5.5 Form Kỷ luật tổ chức đảng
- [ ] **Tạo FormKyLuatToChuc.cs**
  - ComboBox cho đơn vị
  - Dropdown với predefined values
  - Similar structure
  - Sử dụng KyLuatToChucService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(KyLuatToChuc))`

### 5.6 Form Bộ lọc tìm kiếm
- [ ] **Tạo FormBoLocKhenThuongKyLuat.cs**
  - Multiple filter criteria
  - Date range picker
  - Export filtered results
  - Sử dụng các Service tương ứng

### 5.7 Form Danh sách
- [ ] **Tạo FormDanhSachKhenThuongKyLuat.cs**
  - Tabbed interface
  - DataGridView với custom columns
  - Summary statistics
  - Sử dụng các Service tương ứng

---

## 6. CÔNG TÁC PHÁT TRIỂN ĐẢNG

### 6.1 Database & Models
- [ ] **Tạo bảng TheoDoiChuyenChinhThuc**
  - Trường: TheoDoiChuyenChinhThucID, DangVienID, NgayVaoDang, NgayChuyenChinhThuc, TrangThai, GhiChu, NgayTao, NguoiTao
  - Foreign key với DangVien: `FK_TheoDoiChuyenChinhThuc_DangVien`
  - Stored procedures: `TheoDoiChuyenChinhThuc_GetAll`, `TheoDoiChuyenChinhThuc_Insert`, `TheoDoiChuyenChinhThuc_Update`

- [ ] **Tạo PhatTrienDangService.cs**
  - Method GetDangVienDuBi()
  - Method GetCanChuyenChinhThuc()
  - Method UpdateTrangThai()
  - Sử dụng stored procedures đã tạo

### 6.2 Form Quản lý đảng viên dự bị
- [ ] **Tạo FormQuanLyDangVienDuBi.cs**
  - Filter by LoaiDangVien = "Dự bị"
  - Custom DataGridView columns
  - File management integration
  - Sử dụng PhatTrienDangService
  - **Sử dụng FormThem template**: `new FormThem(typeof(DangVien))`
  - **Sử dụng FormSua template**: `new FormSua(existingDangVien)`
  - **Sử dụng FormXemChiTiet template**: `new FormXemChiTiet(dangVien)`

### 6.3 Form Theo dõi chuyển chính thức
- [ ] **Tạo FormTheoDoiChuyenChinhThuc.cs**
  - Calendar integration
  - Notification system
  - Report generation
  - Sử dụng PhatTrienDangService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(TheoDoiChuyenChinhThuc))`

### 6.4 Form Bộ lọc tìm kiếm
- [ ] **Tạo FormBoLocPhatTrienDang.cs**
  - Multiple criteria filtering
  - Export functionality
  - Sử dụng PhatTrienDangService

---

## 7. TEMPLATE FORMS SYSTEM

### 7.1 Các Template Forms có sẵn
- [ ] **FormThem.cs** - Template cho thêm mới dữ liệu
  - Constructor: `FormThem(Type modelType)`
  - Sử dụng: `new FormThem(typeof(QuanNhan))`
  - Dynamic form generation với FormHelper
  - Validation và save logic

- [ ] **FormSua.cs** - Template cho chỉnh sửa dữ liệu
  - Constructor: `FormSua(object existingData)`
  - Sử dụng: `new FormSua(existingQuanNhan)`
  - Load existing data vào controls
  - Update logic

- [ ] **FormXemChiTiet.cs** - Template cho xem chi tiết
  - Constructor: `FormXemChiTiet(object existingData)`
  - Sử dụng: `new FormXemChiTiet(quanNhan)`
  - Read-only mode
  - Display only

- [ ] **FormThaoTacDulieu.cs** - Template tổng hợp
  - Constructor: `FormThaoTacDulieu(Type modelType)` (thêm mới)
  - Constructor: `FormThaoTacDulieu(object existingData)` (chỉnh sửa)
  - Sử dụng: `new FormThaoTacDulieu(typeof(QuanNhan))`
  - Có thể làm cả thêm và sửa

### 7.2 Cách sử dụng Template Forms
- [ ] **Thay thế tất cả Form riêng lẻ** bằng template forms
- [ ] **Chỉ cần tạo Models** với attributes phù hợp
- [ ] **FormHelper** sẽ tự động generate controls
- [ ] **Services** sẽ handle business logic
- [ ] **UserControls** chỉ cần gọi template forms

---

## 8. TÀI LIỆU

### 8.1 Database & Models
- [ ] **Tạo bảng TaiLieu**
  - Trường: TaiLieuID, TenTaiLieu, LoaiTaiLieu, DuongDan, MoTa, NgayTao, NguoiTao, TrangThai
  - File system integration
  - Stored procedures: `TaiLieu_GetAll`, `TaiLieu_Insert`, `TaiLieu_Update`, `TaiLieu_Delete`

- [ ] **Tạo bảng VanBanChiBo**
  - Trường: VanBanChiBoID, DonViID, TenVanBan, NoiDung, NgayGui, NgayNhan, TrangThai, FileDinhKem, NgayTao, NguoiTao
  - Foreign key với DonVi: `FK_VanBanChiBo_DonVi`
  - Stored procedures: `VanBanChiBo_GetByDonViID`, `VanBanChiBo_Insert`, `VanBanChiBo_Update`, `VanBanChiBo_Delete`

### 8.2 Form Quản lý tài liệu
- [ ] **Tạo FormQuanLyTaiLieu.cs**
  - TreeView cho folder structure
  - File upload/download
  - Search functionality
  - Sử dụng TaiLieuService
  - **Sử dụng FormThem template**: `new FormThem(typeof(TaiLieu))`
  - **Sử dụng FormSua template**: `new FormSua(existingTaiLieu)`
  - **Sử dụng FormXemChiTiet template**: `new FormXemChiTiet(taiLieu)`

- [ ] **Tạo FormVanBanChiBo.cs**
  - ComboBox cho đơn vị
  - RichTextBox cho nội dung
  - File attachment
  - Sử dụng VanBanChiBoService
  - **Hoặc sử dụng FormThaoTacDulieu template**: `new FormThaoTacDulieu(typeof(VanBanChiBo))`

---

## 8. BÁO CÁO - THỐNG KÊ

### 8.1 Báo cáo cơ bản
- [ ] **Tạo ReportService.cs**
  - Method GenerateBasicReport(string reportType)
  - Method GenerateStatisticalReport()
  - Chart generation utilities
  - Sử dụng các stored procedures báo cáo

### 8.2 Form Báo cáo tổng hợp theo năm
- [ ] **Tạo FormBaoCaoTongHop.cs**
  - Year selector
  - Multiple report types
  - Export options (Excel, PDF, Word)
  - Sử dụng ReportService

### 8.3 Form Biểu đồ thống kê
- [ ] **Tạo FormBieuDoThongKe.cs**
  - Chart controls (Chart.js hoặc System.Windows.Forms.DataVisualization)
  - Interactive charts
  - Export chart functionality
  - Sử dụng ReportService

### 8.4 Form Xuất báo cáo
- [ ] **Tạo FormXuatBaoCao.cs**
  - Format selection
  - Template management
  - Batch export
  - Sử dụng ReportService

---

## 9. QUẢN TRỊ HỆ THỐNG

### 9.1 Database & Models
- [ ] **Cập nhật bảng NguoiDung**
  - Thêm trường: Roles, Permissions
  - Password hashing
  - Cập nhật stored procedures: `NguoiDung_Insert`, `NguoiDung_Update`

- [ ] **Tạo bảng Roles**
  - Trường: RoleID, TenRole, MoTa, TrangThai, NgayTao, NguoiTao
  - Predefined roles: Admin, BiThu, VanPhong
  - Stored procedures: `Roles_GetAll`, `Roles_Insert`, `Roles_Update`, `Roles_Delete`

- [ ] **Tạo bảng Permissions**
  - Trường: PermissionID, RoleID, Module, Action, TrangThai, NgayTao, NguoiTao
  - CRUD permissions matrix
  - Stored procedures: `Permissions_GetByRoleID`, `Permissions_Insert`, `Permissions_Update`, `Permissions_Delete`

- [ ] **Cập nhật bảng AuditLog**
  - Trường: AuditLogID, NguoiDungID, TenDangNhap, Action, TableName, RecordID, OldValues, NewValues, IPAddress, UserAgent, NgayThucHien
  - JSON storage for OldValues/NewValues
  - Stored procedures: `AuditLog_Insert`, `AuditLog_GetAll`, `AuditLog_GetByDateRange`

- [ ] **Tạo bảng SystemConfig**
  - Trường: SystemConfigID, ConfigKey, ConfigValue, Description, NgayCapNhat, NguoiCapNhat
  - Key-value configuration
  - Stored procedures: `SystemConfig_GetAll`, `SystemConfig_GetByKey`, `SystemConfig_SetValue`

### 9.2 Form Quản lý người dùng
- [ ] **Tạo FormQuanLyNguoiDung.cs**
  - DataGridView với user list
  - Role assignment
  - Password reset functionality
  - Sử dụng NguoiDungService

- [ ] **Tạo FormPhanQuyen.cs**
  - Permission matrix UI
  - Role-based access control
  - Module-specific permissions
  - Sử dụng RolesService và PermissionsService

### 9.3 Phân quyền chi tiết
- [ ] **Tạo AuthorizationService.cs**
  - Method CheckPermission(string module, string action)
  - Method GetUserPermissions(int userId)
  - Method HasRole(int userId, string role)
  - Sử dụng PermissionsService

- [ ] **Implement Authorization trong các Form**
  - Add permission checks
  - Disable controls based on permissions
  - Show/hide menu items
  - Sử dụng AuthorizationService

### 9.4 Form Nhật ký hệ thống (Audit Log)
- [ ] **Tạo FormAuditLog.cs**
  - DataGridView với filtering
  - Export functionality
  - Real-time monitoring
  - Sử dụng AuditLogService

- [ ] **Tạo AuditService.cs**
  - Method LogAction(string action, object oldValue, object newValue)
  - Method GetAuditLog(DateTime from, DateTime to)
  - Automatic logging for CRUD operations
  - Sử dụng stored procedures đã tạo

### 9.5 Form Cấu hình hệ thống
- [ ] **Tạo FormCauHinhHeThong.cs**
  - Configuration management
  - Backup settings
  - Security settings
  - Sử dụng SystemConfigService

- [ ] **Tạo BackupService.cs**
  - Method CreateBackup()
  - Method RestoreBackup(string backupPath)
  - Scheduled backup functionality
  - Database backup utilities

---

## 10. INFRASTRUCTURE & UTILITIES

### 10.1 Common Services
- [ ] **Tạo LoggingService.cs**
  - Method LogInfo(string message)
  - Method LogError(string message, Exception ex)
  - Method LogWarning(string message)
  - File-based logging với rotation

- [ ] **Tạo ConfigurationService.cs**
  - Method GetConfig(string key)
  - Method SetConfig(string key, string value)
  - App.config integration
  - Sử dụng SystemConfigService

- [ ] **Tạo EmailService.cs**
  - Method SendEmail(string to, string subject, string body)
  - SMTP configuration
  - Template support

### 10.2 Data Access Layer
- [ ] **Cập nhật DbHelper.cs**
  - Connection string management
  - Transaction support
  - Error handling
  - Logging integration

- [ ] **Tạo Repository Pattern**
  - IRepository<T> interface
  - GenericRepository<T> implementation
  - Unit of Work pattern
  - Sử dụng stored procedures

### 10.3 UI Components
- [ ] **Tạo Custom Controls**
  - SearchableComboBox
  - DateRangePicker
  - FileUploadControl
  - ImagePreviewControl

- [ ] **Tạo Theme System**
  - Color scheme management
  - Font configuration
  - Responsive design

### 10.4 Testing & Quality Assurance
- [ ] **Unit Tests**
  - Service layer tests
  - Data access tests
  - Business logic tests

- [ ] **Integration Tests**
  - Database integration
  - File system integration
  - External service integration

- [ ] **Performance Testing**
  - Load testing for large datasets
  - Memory usage optimization
  - Database query optimization

---

## 11. DEPLOYMENT & MAINTENANCE

### 11.1 Installation & Setup
- [ ] **Tạo Setup Project**
  - Windows Installer package
  - Database setup scripts
  - Configuration files

- [ ] **Tạo Documentation**
  - User manual
  - Administrator guide
  - API documentation

### 11.2 Maintenance Tools
- [ ] **Tạo Database Maintenance**
  - Index optimization
  - Data cleanup utilities
  - Backup verification

- [ ] **Tạo System Monitoring**
  - Performance metrics
  - Error tracking
  - Usage statistics

---

## PRIORITY ORDER

### Phase 1 (Core Functionality)
1. Database & Models cho DangVien, DonVi (đã có)
2. Basic CRUD operations (đã có)
3. FormThem, FormSua, FormXem (đã có)
4. Basic reporting (đã có)

### Phase 2 (Advanced Features)
1. File management
2. Advanced reporting
3. User management
4. Audit logging

### Phase 3 (Enhancement)
1. Advanced UI components
2. Performance optimization
3. Advanced security
4. Integration features

### Phase 4 (Deployment)
1. Installation package
2. Documentation
3. Training materials
4. Support tools

---

## 12. TEMPLATE FORMS SYSTEM - TỔNG KẾT

### 12.1 Các Template Forms có sẵn
- [ ] **FormThem.cs** - Template cho thêm mới dữ liệu
  - Constructor: `FormThem(Type modelType)`
  - Sử dụng: `new FormThem(typeof(QuanNhan))`
  - Dynamic form generation với FormHelper
  - Validation và save logic

- [ ] **FormSua.cs** - Template cho chỉnh sửa dữ liệu
  - Constructor: `FormSua(object existingData)`
  - Sử dụng: `new FormSua(existingQuanNhan)`
  - Load existing data vào controls
  - Update logic

- [ ] **FormXemChiTiet.cs** - Template cho xem chi tiết
  - Constructor: `FormXemChiTiet(object existingData)`
  - Sử dụng: `new FormXemChiTiet(quanNhan)`
  - Read-only mode
  - Display only

- [ ] **FormThaoTacDulieu.cs** - Template tổng hợp
  - Constructor: `FormThaoTacDulieu(Type modelType)` (thêm mới)
  - Constructor: `FormThaoTacDulieu(object existingData)` (chỉnh sửa)
  - Sử dụng: `new FormThaoTacDulieu(typeof(QuanNhan))`
  - Có thể làm cả thêm và sửa

### 12.2 Cách sử dụng Template Forms
- [ ] **Thay thế tất cả Form riêng lẻ** bằng template forms
- [ ] **Chỉ cần tạo Models** với attributes phù hợp
- [ ] **FormHelper** sẽ tự động generate controls
- [ ] **Services** sẽ handle business logic
- [ ] **UserControls** chỉ cần gọi template forms

### 12.3 Lợi ích của Template Forms System
- [ ] **Giảm thời gian phát triển** từ 10 tuần xuống còn 6-7 tuần
- [ ] **Tăng tính nhất quán** trong UI/UX
- [ ] **Dễ maintain** và **extend** trong tương lai
- [ ] **Giảm bugs** do sử dụng code đã được test
- [ ] **Tái sử dụng code** hiệu quả
- [ ] **Dễ học** cho developer mới

### 12.4 Development Workflow với Template Forms
1. **Tạo Database** (tables, stored procedures)
2. **Tạo Models** với attributes
3. **Tạo Services** với CRUD methods
4. **Tạo UserControls** với DataGridView
5. **Sử dụng Template Forms** cho CRUD operations
6. **Test và optimize**

---

## 13. KẾT LUẬN

Với hệ thống template forms có sẵn, việc phát triển sẽ nhanh hơn rất nhiều. Chỉ cần tập trung vào:
- **Database design** và **Models**
- **Business logic** trong **Services**
- **UI/UX** trong **UserControls**
- **Template forms** sẽ handle tất cả CRUD operations

Điều này giúp:
- **Giảm thời gian phát triển** từ 10 tuần xuống còn 6-7 tuần
- **Tăng tính nhất quán** trong UI/UX
- **Dễ maintain** và **extend** trong tương lai
- **Giảm bugs** do sử dụng code đã được test
