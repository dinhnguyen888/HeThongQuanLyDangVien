# Hướng dẫn bàn giao cho khách hàng

## 📦 Nội dung bàn giao

Khi bàn giao cho khách hàng, chỉ cần giao:

1. **File EXE của ứng dụng** (từ thư mục `bin\Release\` hoặc `bin\Debug\`)
2. **Thư mục `Setup\`** (toàn bộ nội dung)

## 🚀 Hướng dẫn cài đặt cho khách hàng

### Bước 1: Chạy FullSetup.bat

1. Mở thư mục `Setup`
2. **Click chuột phải** vào file `FullSetup.bat`
3. Chọn **"Run as administrator"** (Chạy với quyền quản trị viên)

### Bước 2: Chọn phương thức cài đặt

Script sẽ hiển thị menu:

```
[1] SQL Server Express (Đầy đủ tính năng, ~500MB)
[2] SQL Server Express LocalDB (Nhẹ, ~100MB, khuyến nghị) ⭐
[3] Bỏ qua - SQL Server đã được cài đặt
```

**Khuyến nghị:** Chọn **[2]** - SQL Server Express LocalDB (nhẹ, đủ dùng)

### Bước 3: Chờ quá trình cài đặt

- Script sẽ tự động tải SQL Server (nếu chưa có)
- Quá trình tải có thể mất 5-15 phút tùy tốc độ internet
- Quá trình cài đặt có thể mất 10-20 phút
- **Không tắt cửa sổ Command Prompt** trong quá trình này

### Bước 4: Hoàn tất

Sau khi hoàn tất, bạn sẽ thấy thông báo:
```
✓ HOÀN TẤT CÀI ĐẶT!
```

### Bước 5: Chạy ứng dụng

1. Chạy file EXE của ứng dụng
2. Đăng nhập với tài khoản mặc định:
   - Username: `admin`
   - Password: `admin123`

## ⚠️ Lưu ý quan trọng

1. **Quyền Administrator:** Phải chạy script với quyền Administrator
2. **Kết nối Internet:** Cần internet để tải SQL Server Express (lần đầu)
3. **Thời gian:** Quá trình cài đặt có thể mất 20-30 phút
4. **Ổ cứng:** Cần ít nhất 500MB trống (hoặc 100MB nếu dùng LocalDB)
5. **Windows Firewall:** Có thể hiện cảnh báo, chọn "Allow"

## 🔧 Xử lý lỗi

### Lỗi: "Access is denied"

**Giải pháp:** Chạy lại với quyền Administrator (Click chuột phải → Run as administrator)

### Lỗi: "Không thể tải SQL Server Express"

**Giải pháp:**
1. Kiểm tra kết nối internet
2. Tải thủ công từ: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
3. Đặt file tải về vào thư mục `Setup` với tên `SQLServerExpress.exe` hoặc `SQLServerLocalDB.exe`
4. Chạy lại script

### Lỗi: "SQL Server vẫn chưa sẵn sàng"

**Giải pháp:**
1. Kiểm tra SQL Server Service đang chạy:
   - Mở Services (services.msc)
   - Tìm "SQL Server (MSSQLSERVER)" hoặc "SQL Server (MSSQLLocalDB)"
   - Đảm bảo Status = Running
2. Khởi động lại máy tính
3. Chạy lại script

### Lỗi: "Không tìm thấy App.config"

**Giải pháp:** 
- Script sẽ hiển thị connection string để bạn cập nhật thủ công
- Mở file `App.config` trong thư mục gốc của ứng dụng
- Cập nhật connection string theo hướng dẫn

## 📋 Checklist bàn giao

- [ ] File EXE của ứng dụng
- [ ] Thư mục `Setup\` (đầy đủ các file)
- [ ] File `HUONG_DAN_BAN_GIAO.md` (file này)
- [ ] Hướng dẫn khách hàng chạy `FullSetup.bat` với quyền Administrator

## 💡 Tips cho khách hàng

1. **Lần đầu cài đặt:** Chạy `FullSetup.bat` để tự động setup tất cả
2. **Cài lại database:** Chạy `QuickSetup.bat` hoặc `SetupDatabase.bat`
3. **Thay đổi password:** Sau khi setup, nên đổi password mặc định `QuanLyDangVien@2024`
4. **Backup:** Nên backup database định kỳ (tính năng có sẵn trong ứng dụng)

## 📞 Hỗ trợ

Nếu khách hàng gặp vấn đề, hãy kiểm tra:
- Log output của script
- SQL Server Error Log
- Windows Event Viewer
- File `README.md` trong thư mục Setup để biết thêm chi tiết

