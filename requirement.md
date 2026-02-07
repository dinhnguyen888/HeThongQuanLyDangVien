# YÊU CẦU PHẦN MỀM QUẢN LÝ ĐẢNG VIÊN

## CÔNG NGHỆ SỬ DỤNG

- [x] **Database**: SQL Server
- [x] **Frontend**: .NET Winform
- [x] **ORM**: Dapper (micro ORM)
- [x] **Query**: Dapper SQL thuần phía frontend
- [x] **Export**: Microsoft.Office.Interop.Word để xuất và mở file báo cáo

---

## 1. DANH SÁCH QUÂN NHÂN

### 1.1 Database & Models
- [x] **Tạo bảng QuanNhan** với các trường:
  - [x] TT (Số thứ tự)
  - [x] Họ tên
  - [x] Ngày tháng năm sinh
  - [x] SHSQ
  - [x] Số thẻ BHYT
  - [x] Số CCCD
  - [x] Cấp bậc
  - [x] Chức vụ
  - [x] Đơn vị
  - [x] Nhập ngũ
  - [x] Ngày vào đảng
  - [x] Số thẻ Đảng
  - [x] Đoàn
  - [x] Dân tộc
  - [x] Tôn giáo
  - [x] Sức khỏe
  - [x] Nhóm máu
  - [x] Họ tên cha-năm sinh
  - [x] Họ tên mẹ-năm sinh
  - [x] Họ tên vợ, con-năm sinh
  - [x] Nghề nghiệp cha mẹ
  - [x] Mấy anh chị em
  - [x] Quê quán
  - [x] Nơi ở hiện nay
  - [x] Khi cần báo tin
  - [x] Ghi chú

### 1.2 Form Danh sách quân nhân
- [ ] **Thiết kế DataGridView** với các cột gộp theo thứ tự:
  - [ ] TT (Số thứ tự)
  - [ ] Họ tên / Ngày tháng năm sinh / SHSQ / Số thẻ BHYT / Số CCCD
  - [ ] Cấp bậc / Chức vụ / Đơn vị
  - [ ] Nhập ngũ / Ngày vào đảng / Số thẻ Đảng / Đoàn
  - [ ] Dân tộc / Tôn giáo
  - [ ] Trình độ văn hóa
  - [ ] Sức khỏe / Nhóm máu
  - [ ] Họ tên cha năm sinh / Họ tên mẹ năm sinh / Họ tên vợ, con năm sinh
  - [ ] Nghề nghiệp
  - [ ] Mấy anh chị em
  - [ ] Quê quán / Nơi ở hiện nay
  - [ ] Khi cần báo tin
  - [ ] Ghi chú

### 1.3 Chức năng xuất file
- [ ] **Xuất ra file tổng hợp**
- [ ] **Chức năng in danh sách**

---

## 2. QUẢN LÝ HỒ SƠ ĐẢNG VIÊN

### 2.1 Database & Models
- [x] **Tạo bảng DangVien** với đầy đủ các trường cần thiết
- [x] **Tạo bảng lưu trữ ảnh đại diện** (path/binary)
- [x] **Tạo bảng lưu quá trình công tác**
- [x] **Tạo bảng hồ sơ gia đình**
- [x] **Tạo bảng hồ sơ khen thưởng - kỷ luật**
- [x] **Tạo bảng tài liệu hồ sơ đảng viên** (file attachments)

### 2.2 Form Danh sách đảng viên
- [ ] **Thiết kế giao diện với khung ảnh đại diện** (upload ảnh 3x4, 4x6)
- [ ] **Bộ lọc tìm kiếm** với các tiêu chí:
  - [ ] Đơn vị
  - [ ] Họ và tên
  - [ ] Cấp bậc
  - [ ] Chức vụ
  - [ ] Số thẻ đảng viên
  - [ ] Số CCCD
  - [ ] Đối tượng (SQ, QNCN, HSQ-BS, LĐHĐ, CNVCQP)
  - [ ] Tuổi Đảng
  - [ ] Quê quán
  - [ ] Trình độ
  - [ ] Tuổi Đời

- [ ] **Bảng danh sách** gồm có:
  - [ ] STT
  - [ ] Họ và tên
  - [ ] Ngày, tháng, năm sinh
  - [ ] Số thẻ đảng viên
  - [ ] Số LL Đ.viên
  - [ ] Số CCCD
  - [ ] Số điện thoại
  - [ ] Cấp bậc
  - [ ] Chức vụ
  - [ ] Đơn vị
  - [ ] Chính thức Hay dự Bị (Lựa chọn)
  - [ ] Giới tính
  - [ ] Nhập ngũ
  - [ ] Vào Đảng
  - [ ] Chính thức
  - [ ] Đối Tượng (SQ, QNCN, HSQ-BS, LĐHĐ, CNVCQP - lựa chọn)
  - [ ] Tuổi Đảng
  - [ ] Quê quán
  - [ ] Trình độ
  - [ ] Tuổi Đời
  - [ ] Quá trình công tác (Viết dài)
  - [ ] Hồ sơ gia đình
  - [ ] Hồ sơ khen thưởng – kỷ luật
  - [ ] Tài liệu Hồ sơ Đảng viên (File Word, ảnh, PDF Thêm vào Theo Thư mục)

### 2.3 Chức năng xuất file
- [ ] **Xuất ra file Word để in**
- [ ] **Xuất báo cáo**

### 2.4 Form Thêm/Sửa/Xóa đảng viên
- [x] **Form thêm đảng viên mới**
- [x] **Form sửa thông tin đảng viên**
- [x] **Chức năng lưu**
- [x] **Chức năng xóa** (với xác nhận)
- [x] **Chức năng cập nhật**

### 2.5 Form Mẫu T63
- [ ] **Thiết kế form nhập theo mẫu T63**
- [ ] **Xuất ra file Word theo mẫu T63**
- [ ] **Chức năng in mẫu T63**

---

## 3. QUẢN LÝ HỒ SƠ ĐƠN VỊ

### 3.1 Database & Models
- [x] **Tạo bảng ChiBo/DonVi**
- [x] **Tạo bảng CapUy** (Bí thư, Phó Bí thư, Ủy viên)
- [x] **Liên kết với bảng khen thưởng, kỷ luật**

### 3.2 Form Danh sách chi bộ, đơn vị
- [ ] **Hiển thị**:
  - [ ] STT
  - [ ] Danh sách Đơn vị
  - [ ] Tổng số đảng viên
  - [ ] Cấp ủy (Bí thư, Phó Bí Thư, Ủy viên là ai)
  - [ ] Khen thưởng
  - [ ] Kỷ luật
  - [ ] Ghi chú

- [ ] **Bộ lọc tìm kiếm**: Đơn vị

### 3.3 Form Chi tiết chi bộ
- [ ] **Lấy dữ liệu từ Form Danh sách đảng viên**
- [ ] **Xếp theo từng chi bộ của đơn vị**

### 3.4 Form Sinh hoạt chi bộ
- [ ] **Lịch sinh hoạt**: tạo lịch, nhắc nhở, điểm danh
- [ ] **Biên bản sinh hoạt**: ghi chép nội dung, nghị quyết

### 3.5 Form Thêm/Sửa/Xóa đơn vị
- [x] **Form thêm đơn vị**
- [x] **Form sửa đơn vị**
- [x] **Chức năng lưu**
- [x] **Chức năng xóa**
- [x] **Chức năng cập nhật**

---

## 4. CHUYỂN SINH HOẠT ĐẢNG

### 4.1 Database & Models
- [x] **Tạo bảng ChuyenSinhHoat**
- [x] **Lưu trữ file quyết định**

### 4.2 Form Chuyển sinh hoạt
- [ ] **Chọn đảng viên**: chọn trên hàng của datagridviewDangVien
- [ ] **Hiển thị**: Cấp bậc, Chức vụ, Vào Đảng, Chính thức, Số thẻ đảng viên
- [ ] **DropDown**: Đơn vị đi, Đơn vị đến, Ngày chuyển, Ghi chú
- [ ] **Upload file quyết định**

### 4.3 Form Lịch sử chuyển sinh hoạt
- [ ] **Xem theo từng đảng viên**
- [ ] **Xem toàn hệ thống**
- [ ] **Lọc theo năm**
- [ ] **Lọc theo đơn vị**

### 4.4 Dùng form Thêm/Sửa/Xóa đã được định nghĩa
- [ ] **Chức năng thêm**
- [ ] **Chức năng sửa**
- [ ] **Chức năng xóa**


---

## 5. THI ĐUA - KHEN THƯỞNG

### 5.1 Database & Models
- [x] **Tạo bảng KhenThuongCaNhan**
- [x] **Tạo bảng KhenThuongDonVi**
- [x] **Tạo bảng KyLuatCaNhan**
- [x] **Tạo bảng KyLuatToChuc**
- [x] **Lưu trữ file đính kèm** (ảnh, file quyết định)

### 5.2 Form Khen thưởng cá nhân
- [ ] **Chọn đảng viên**
- [ ] **Dropdown hình thức khen thưởng**:

#### A. Danh hiệu thi đua đối với cá nhân
  - [ ] "Chiến sĩ thi đua toàn quốc"
  - [ ] "Chiến sĩ thi đua cấp bộ, ngành, tỉnh, đoàn thể trung ương"
  - [ ] "Chiến sĩ thi đua cơ sở"
  - [ ] "Lao động tiên tiến"
  - [ ] "Chiến sĩ tiên tiến"

#### B. Hình thức khen thưởng
##### 1. Huân chương
  - [ ] "Huân chương Sao vàng"
  - [ ] "Huân chương Hồ Chí Minh"
  - [ ] "Huân chương Độc lập" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Quân công" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Lao động" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Bảo vệ Tổ quốc" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Chiến công" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Đại đoàn kết dân tộc"
  - [ ] "Huân chương Dũng cảm"
  - [ ] "Huân chương Hữu nghị"

##### 2. Huy chương
  - [ ] "Huy chương Quân kỳ quyết thắng"
  - [ ] "Huy chương Vì an ninh Tổ quốc"
  - [ ] "Huy chương Chiến sĩ vẻ vang" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huy chương Hữu nghị"

##### 3. Danh hiệu vinh dự nhà nước
  - [ ] "Anh hùng Lực lượng vũ trang nhân dân"
  - [ ] "Anh hùng Lao động"

##### 4. Giải thưởng
  - [ ] "Giải thưởng Hồ Chí Minh"
  - [ ] "Giải thưởng nhà nước"

##### 5. Khác
  - [ ] "Kỷ niệm chương"
  - [ ] "Huy hiệu"
  - [ ] "Bằng khen của Thủ tướng Chính phủ"
  - [ ] "Bằng khen cấp bộ, ngành, tỉnh, đoàn thể trung ương"
  - [ ] "Giấy khen"

- [ ] **Nhập**: Ngày, Số quyết định, Cấp quyết định
- [ ] **Upload file đính kèm** (ảnh, file)

### 5.3 Form Khen thưởng Đơn vị
- [ ] **Chọn chi bộ**
- [ ] **Radiobutton hình thức**:

#### A. Danh hiệu thi đua đối với tập thể
  - [ ] "Cờ thi đua của Chính phủ"
  - [ ] "Cờ thi đua cấp bộ, ngành, tỉnh, đoàn thể trung ương"
  - [ ] "Tập thể lao động xuất sắc"
  - [ ] "Đơn vị quyết thắng"
  - [ ] "Tập thể lao động tiên tiến"
  - [ ] "Đơn vị tiên tiến"

#### B. Hình thức khen thưởng tập thể
##### 1. Huân chương tập thể
  - [ ] "Huân chương Sao vàng"
  - [ ] "Huân chương Hồ Chí Minh"
  - [ ] "Huân chương Độc lập" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Quân công" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Lao động" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Bảo vệ Tổ quốc" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Chiến công" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huân chương Đại đoàn kết dân tộc"
  - [ ] "Huân chương Dũng cảm"
  - [ ] "Huân chương Hữu nghị"

##### 2. Huy chương tập thể
  - [ ] "Huy chương Quân kỳ quyết thắng"
  - [ ] "Huy chương Vì an ninh Tổ quốc"
  - [ ] "Huy chương Chiến sĩ vẻ vang" hạng nhất, hạng nhì, hạng ba
  - [ ] "Huy chương Hữu nghị"

##### 3. Danh hiệu vinh dự nhà nước tập thể
  - [ ] "Anh hùng Lực lượng vũ trang nhân dân"
  - [ ] "Anh hùng Lao động"

##### 4. Giải thưởng tập thể
  - [ ] "Giải thưởng Hồ Chí Minh"
  - [ ] "Giải thưởng nhà nước"

##### 5. Khác
  - [ ] "Kỷ niệm chương"
  - [ ] "Huy hiệu"
  - [ ] "Bằng khen của Thủ tướng Chính phủ"
  - [ ] "Bằng khen cấp bộ, ngành, tỉnh, đoàn thể trung ương"
  - [ ] "Giấy khen"

- [ ] **Nhập**: Ngày, Số quyết định, Cấp quyết định
- [ ] **Upload file đính kèm**

### 5.4 Form Kỷ luật cá nhân
- [ ] **Chọn đảng viên**
- [ ] **Dropdown hình thức Kỷ luật Đối với đảng viên chính thức**:
  - [ ] Khiển trách
  - [ ] Cảnh cáo
  - [ ] Cách chức
  - [ ] Khai trừ

- [ ] **Dropdown hình thức Kỷ luật Đối với đảng viên dự bị**:
  - [ ] Khiển trách
  - [ ] Cảnh cáo

- [ ] **Nhập**: Ngày, Số quyết định, Cấp quyết định, Nội dung, Ghi chú
- [ ] **Upload file đính kèm**

### 5.5 Form Kỷ luật tổ chức đảng
- [ ] **Chọn Đơn vị** (Chi bộ, Đảng bộ)
- [ ] **Radio hình thức Kỷ luật**:
  - [ ] Khiển trách
  - [ ] Cảnh cáo
  - [ ] Giải tán

- [ ] **Nhập**: Ngày, Số quyết định, Cấp quyết định, Nội dung, Ghi chú
- [ ] **Upload file đính kèm** (các quyết định hồ sơ liên quan)

### 5.6 Form Bộ lọc tìm kiếm
- [ ] **Lọc khen thưởng cá nhân** theo năm, theo hình thức
- [ ] **Lọc khen thưởng tổ chức đảng** theo năm, theo hình thức
- [ ] **Lọc kỷ luật cá nhân** theo năm, theo hình thức
- [ ] **Lọc kỷ luật tổ chức đảng** theo năm, theo hình thức

### 5.7 Form Danh sách
- [ ] **Danh sách khen thưởng** (lọc theo năm, cá nhân, đơn vị)
- [ ] **Danh sách kỷ luật** (lọc theo năm, cá nhân, đơn vị)

---

## 6. CÔNG TÁC PHÁT TRIỂN ĐẢNG

### 6.1 Database & Models
- [ ] **Sử dụng bảng DangVien** với filter đảng viên dự bị
- [ ] **Tạo bảng theo dõi chuyển chính thức**

### 6.2 Form Quản lý đảng viên dự bị
- [ ] **Hiển thị**:
  - [ ] STT
  - [ ] Họ và tên
  - [ ] Giới tính
  - [ ] Ngày sinh
  - [ ] Vào Đảng
  - [ ] Đơn vị
  - [ ] Đối Tượng (SQ, QNCN, HSQ-BS, LĐHĐ, CNVCQP)
  - [ ] Quê quán
  - [ ] Trình độ
  - [ ] Tuổi Đời
  - [ ] Quá trình công tác
  - [ ] Hồ sơ gia đình
  - [ ] Tài liệu Hồ sơ Đảng viên (ảnh, PDF)

### 6.3 Form Theo dõi chuyển chính thức
- [ ] **Chức năng nhắc lịch tự động**
- [ ] **Tạo báo cáo**

### 6.4 Form Bộ lọc tìm kiếm
- [ ] **Lọc theo**:
  - [ ] Đơn vị
  - [ ] Họ và tên
  - [ ] Đối Tượng (SQ, QNCN, HSQ-BS, LĐHĐ, CNVCQP)
  - [ ] Quê quán
  - [ ] Trình độ

---

## 7. TÀI LIỆU

### 7.1 Database & Models
- [ ] **Tạo bảng TaiLieu** với phân loại folder/mục
- [ ] **Tạo bảng VanBanChiBo** (liên kết với chi bộ/đảng bộ)

### 7.2 Form Quản lý tài liệu
- [ ] **Quản lý các văn bản, tài liệu hướng dẫn** theo folder/mục riêng
- [ ] **Form chọn chi bộ/đảng bộ**
- [ ] **Lưu trữ văn bản của đảng gửi cho chi bộ/đảng bộ**
- [ ] **Chức năng upload/download file**

---

## 8. BÁO CÁO - THỐNG KÊ

### 8.1 Báo cáo cơ bản
- [ ] **Báo cáo danh sách quân nhân**
- [ ] **Báo cáo danh sách đảng viên**
- [ ] **Báo cáo danh sách đơn vị**
- [ ] **Báo cáo Thi đua, khen thưởng**
- [ ] **Báo cáo Kỷ luật**
- [ ] **Báo cáo Chuyển sinh hoạt đảng**
- [ ] **Báo cáo phát triển đảng viên**

### 8.2 Form Báo cáo tổng hợp theo năm
- [ ] **Tổng số đảng viên** (theo từng Đối tượng: SQ, QNCN, HSQ-BS, LĐHĐ, CNVCQP)
- [ ] **Số kết nạp mới**
- [ ] **Số chuyển đi/đến**
- [ ] **Thống kê Chính thức/Dự bị**
- [ ] **Thống kê Tuổi đời**:
  - [ ] Từ 18 - 30 tuổi
  - [ ] Từ 41 đến 50
  - [ ] Tuổi Bình quân
- [ ] **Thống kê Tuổi đảng**:
  - [ ] Từ 30 đến dưới 40 tuổi đảng
  - [ ] Từ 50 năm tuổi đảng trở lên
- [ ] **Số khen thưởng, số kỷ luật**

### 8.3 Form Biểu đồ thống kê
- [ ] **Biểu đồ theo Đơn vị**
- [ ] **Phân tích Tổng số đảng viên**
- [ ] **Thống kê theo giới tính**
- [ ] **Thống kê theo Quê quán**
- [ ] **Thống kê theo tuổi**
- [ ] **Thống kê theo chức vụ**
- [ ] **Thống kê theo trình độ**
- [ ] **Thống kê số kết nạp mới, chuyển đi/đến**
- [ ] **Thống kê Chính thức/Dự bị**

### 8.4 Form Xuất báo cáo
- [ ] **Xuất Excel**
- [ ] **Xuất PDF**
- [ ] **Xuất Word** (theo mẫu)
- [ ] **Xem trực tiếp trên ứng dụng**

---

## 9. QUẢN TRỊ HỆ THỐNG

### 9.1 Database & Models
- [x] **Tạo bảng Users** (tài khoản)
- [x] **Tạo bảng AuditLog** (nhật ký hệ thống)
- [x] **Tạo bảng SystemConfig** (cấu hình)

### 9 User control quản trị hệ thống
- [ ] **Quản lý tài khoản người dùng**
- [ ] **Phân quyền Admin**: truy cập, sửa tất cả các nội dung
- [ ] **Phân quyền Bí thư**: xem, thêm sửa xóa các nội dung của cá nhân, chi bộ, đảng bộ đó
- [ ] **Phân quyền Văn phòng**: chỉ Xem báo cáo, xem danh sách đảng viên chi bộ, đảng bộ, báo cáo tổng hợp số lượng

### 9.3 Phân quyền chi tiết
- [ ] **Thiết kế CRUD** (Create, Read, Update, Delete) cho từng module/form
- [ ] **Áp dụng phân quyền theo chức năng cụ thể**
- [ ] **Ví dụ**: Văn phòng chỉ được "Xem" và "Xuất Báo cáo" nhưng không được "Sửa" hồ sơ đảng viên
- [ ] **Phân quyền ảnh hưởng đến khả năng truy cập UserControl tài liệu**: admin không bị giới hạn, Bí thư được toàn quyền nhưng chỉ trong DonVi mà Bí thư đang ở, Văn phòng chỉ được xem và tải tài liệu
- [ ] **Cách triễn khai**: Ở mỗi UserControl sẽ tiến hành dùng một attribute hoặc một static class để check xem user hiện tại đang có role và DonViID nào, sau đó thực hiện ẩn các nút tương ứng với role của user đó (admin, biThu, vanPhong) 

### 9.4 Panel Nhật ký hệ thống (Audit Log)
- [ ] **Ghi lại thao tác thêm**
- [ ] **Ghi lại thao tác sửa**
- [ ] **Ghi lại thao tác xóa**
- [ ] **Ghi lại thao tác cập nhật**
- [ ] **Lưu thông tin tài khoản thực hiện**
- [ ] **Lưu timestamp**
- [ ] **Cách triễn khai**: tạo một storeProceduce AuditLog_Insert vào trong query.sql, sau đó thực hiện gọi ở tất cả các storeProceduce khác, với 3 tham số là userID, Action và Timestamps 

### 9.5 Panel Cấu hình hệ thống
- [ ] **Cài đặt tự động sao lưu** (backup DB hàng ngày)
- [ ] **Đăng xuất**
- [ ] **Cách triễn khai**: đối với tự động sao lưu sẽ tiến hành chạy query backup database, file .bak sẽ được lưu ở C:QuanLyDangVien\Server/Backup, có tính năng set chạy backup ngầm dựa vào thời gian cụ thể (ví dụ backup sau:3 5 7 ngày), có nút restore. Đối với Đăng xuất thực hiện xóa C:\QuanLyDangVien\Data\user_info.json, sau đó reload lại winform 


---

## 10. HÌNH ẢNH DEMO (UI MOCKUP)

### 10.1 Màn hình Dashboard
- [ ] **Hiển thị số liệu tổng quan**
- [ ] **Các form để chọn**
- [ ] **Giao diện đẹp có các ảnh của đơn vị**

### 10.2 Màn hình Danh sách Quân nhân
- [ ] **Tìm kiếm**
- [ ] **Danh sách**
- [ ] **Nút thao tác**
- [ ] **Thông tin của đảng viên**
- [ ] **Khen thưởng, kỷ luật**

### 10.3 Màn hình Danh sách đảng viên
- [ ] **Tìm kiếm**
- [ ] **Danh sách**
- [ ] **Nút thao tác**
- [ ] **Thông tin của đảng viên**
- [ ] **Khen thưởng, kỷ luật**

### 10.4 Màn hình Hồ Sơ Đơn vị
- [ ] **Có các Đơn vị để chọn**
- [ ] **Thông tin hồ sơ của từng đơn vị**
- [ ] **Kết nối với danh sách đảng viên**
- [ ] **Khen thưởng/kỷ luật**

### 10.5 Chuyển sinh hoạt đảng
- [ ] **Kết nối các thông tin với danh sách đảng viên**
- [ ] **Chỉ có form chọn, tìm kiếm**

### 10.6 Thi đua - Khen thưởng
- [ ] **Các dữ liệu của đảng viên đều được kết nối với nhau**

### 10.7 Công tác phát triển Đảng
- [ ] **Form quản lý đảng viên dự bị**
- [ ] **Theo dõi chuyển chính thức**

### 10.8 Màn hình Báo cáo tổng hợp
- [ ] **Biểu đồ**
- [ ] **Xuất file báo cáo**
- [ ] **Xem trực tiếp**

---

## 11. TÀI LIỆU THAM KHẢO

### 11.1 Mẫu file Word
- [ ] **Mẫu file word được lưu trữ trong /Word**
- [ ] **Sử dụng các mẫu có sẵn để thiết kế form**

### 11.2 Danh hiệu thi đua
- [ ] **Sử dụng file danh-hieu-thi-dua.txt** để làm dropdown options
