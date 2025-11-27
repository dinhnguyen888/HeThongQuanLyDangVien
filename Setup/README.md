# Hướng dẫn Setup Database

Thư mục này chứa các script để tự động setup database cho ứng dụng Quản lý Đảng viên.

## ⚡ Quick Start (Khuyến nghị cho máy mới)

Nếu máy tính chưa có SQL Server, chạy:
```batch
FullSetup.bat
```

Script này sẽ tự động:
- ✅ Tải và cài đặt SQL Server Express (nếu chưa có)
- ✅ Tạo database và user
- ✅ Chạy script SQL để init dữ liệu
- ✅ Cập nhật App.config

## Các file trong thư mục Setup

### Script chính:
1. **FullSetup.bat** ⭐ - Setup hoàn chỉnh (tự động cài SQL Server + setup database)
2. **InstallSQLServer.bat** - Tải và cài SQL Server Express (đầy đủ)
3. **InstallSQLServerLocalDB.bat** - Tải và cài SQL Server Express LocalDB (nhẹ)
4. **QuickSetup.bat** - Menu chọn phương thức setup (nếu SQL Server đã có)

### Script setup database:
5. **SetupDatabase.bat** - Setup với SQL Authentication (tạo user và password)
6. **SetupDatabase_WithWindowsAuth.bat** - Setup với Windows Authentication
7. **UpdateAppConfig.ps1** - PowerShell script để cập nhật App.config

### File dữ liệu:
8. **query.sql** - Script SQL để tạo database và dữ liệu mẫu

## Cách sử dụng

### 🚀 Cách 1: Setup hoàn chỉnh (Máy mới, chưa có SQL Server) ⭐

**Khuyến nghị cho khách hàng:**

1. Mở Command Prompt với quyền **Administrator**
2. Chạy file `FullSetup.bat`

```batch
cd Setup
FullSetup.bat
```

Script sẽ hỏi bạn chọn:
- **[1]** SQL Server Express (đầy đủ tính năng, ~500MB)
- **[2]** SQL Server Express LocalDB (nhẹ, ~100MB) ⭐ **Khuyến nghị**
- **[3]** Bỏ qua (nếu SQL Server đã có)

Sau đó script sẽ tự động:
- Tải và cài đặt SQL Server (nếu chọn 1 hoặc 2)
- Setup database và user
- Chạy script SQL
- Cập nhật App.config

### Cách 2: Chỉ cài SQL Server (nếu cần)

Nếu chỉ muốn cài SQL Server mà không setup database:

```batch
InstallSQLServer.bat          # SQL Server Express đầy đủ
InstallSQLServerLocalDB.bat   # SQL Server Express LocalDB (nhẹ)
```

### Cách 3: Setup database (SQL Server đã có)

Nếu SQL Server đã được cài đặt:

#### Option A: SQL Authentication (Khuyến nghị)

```batch
SetupDatabase.bat
```

Hoặc với tham số tùy chỉnh:
```batch
SetupDatabase.bat [SQL_SERVER] [DATABASE_NAME] [USER_NAME] [PASSWORD]
```

Ví dụ:
```batch
SetupDatabase.bat localhost QuanLyDangVien QuanLyDangVien_User MyPassword123
```

**Mặc định:**
- SQL Server: `localhost`
- Database: `QuanLyDangVien`
- User: `QuanLyDangVien_User`
- Password: `QuanLyDangVien@2024`

#### Option B: Windows Authentication

```batch
SetupDatabase_WithWindowsAuth.bat
```

Hoặc với tham số tùy chỉnh:
```batch
SetupDatabase_WithWindowsAuth.bat [SQL_SERVER] [DATABASE_NAME]
```

Ví dụ:
```batch
SetupDatabase_WithWindowsAuth.bat localhost QuanLyDangVien
```

**Mặc định:**
- SQL Server: `localhost`
- Database: `QuanLyDangVien`
- Authentication: Windows Authentication (sử dụng tài khoản Windows hiện tại)

## Yêu cầu

### Cho máy mới (chưa có SQL Server):
1. **Kết nối Internet** (để tải SQL Server Express)
2. **Quyền Administrator** để chạy script
3. **Ổ cứng trống ~500MB** (cho SQL Server Express) hoặc **~100MB** (cho LocalDB)
4. **Windows 7 trở lên**

### Cho máy đã có SQL Server:
1. **SQL Server đã được cài đặt** (SQL Server 2012 trở lên)
2. **SQL Server đang chạy** (SQL Server Service)
3. **sqlcmd** đã được cài đặt (thường đi kèm với SQL Server)
4. **Quyền Administrator** để chạy script
5. **Quyền sa hoặc sysadmin** trên SQL Server (để tạo database và user)

## Quy trình setup

### Với FullSetup.bat (Máy mới):
1. ✅ Kiểm tra SQL Server đã cài chưa
2. ✅ Tải SQL Server Express (nếu chưa có)
3. ✅ Cài đặt SQL Server Express
4. ✅ Kiểm tra kết nối SQL Server
5. ✅ Tạo database (nếu chưa tồn tại)
6. ✅ Tạo login và user (chỉ với SQL Authentication)
7. ✅ Cấp quyền db_owner cho user (chỉ với SQL Authentication)
8. ✅ Chạy script `query.sql` để tạo tables, stored procedures và dữ liệu mẫu
9. ✅ Cập nhật `App.config` với connection string mới

### Với SetupDatabase.bat (SQL Server đã có):
1. ✅ Kiểm tra kết nối SQL Server
2. ✅ Tạo database (nếu chưa tồn tại)
3. ✅ Tạo login và user (chỉ với SQL Authentication)
4. ✅ Cấp quyền db_owner cho user (chỉ với SQL Authentication)
5. ✅ Chạy script `query.sql` để tạo tables, stored procedures và dữ liệu mẫu
6. ✅ Cập nhật `App.config` với connection string mới

## Lưu ý

- **Bảo mật**: Mật khẩu mặc định là `QuanLyDangVien@2024`. Nên thay đổi sau khi setup.
- **Backup**: Script sẽ không xóa database nếu đã tồn tại, nhưng sẽ chạy lại `query.sql` (có thể ghi đè dữ liệu).
- **App.config**: Script sẽ tự động cập nhật `App.config` nếu tìm thấy file. Nếu không, bạn cần cập nhật thủ công.

## Xử lý lỗi

### Lỗi: "Không thể kết nối đến SQL Server"

**Nguyên nhân:**
- SQL Server chưa được cài đặt
- SQL Server Service chưa chạy
- Tên server không đúng
- SQL Server không cho phép kết nối từ xa

**Giải pháp:**
1. Kiểm tra SQL Server Service đang chạy:
   - Mở Services (services.msc)
   - Tìm "SQL Server (MSSQLSERVER)" hoặc "SQL Server (SQLEXPRESS)"
   - Đảm bảo Status = Running

2. Kiểm tra tên server:
   - Mở SQL Server Management Studio (SSMS)
   - Xem tên server trong Object Explorer
   - Sử dụng tên đó trong script

3. Kiểm tra SQL Server Configuration Manager:
   - Mở SQL Server Configuration Manager
   - SQL Server Network Configuration → Protocols for MSSQLSERVER
   - Đảm bảo TCP/IP và Named Pipes đã được Enable

### Lỗi: "Không tìm thấy file query.sql"

**Giải pháp:**
- Đảm bảo file `query.sql` nằm trong cùng thư mục với file `.bat`
- Hoặc copy file `query.sql` từ thư mục `Database` vào thư mục `Setup`

### Lỗi: "Lỗi khi chạy script SQL"

**Giải pháp:**
- Kiểm tra file `query.sql` có lỗi cú pháp không
- Kiểm tra quyền của user có đủ để tạo tables, stored procedures không
- Xem log chi tiết trong output của script

## Cập nhật App.config thủ công

Nếu script không thể tự động cập nhật `App.config`, bạn có thể cập nhật thủ công:

### Với SQL Authentication:
```xml
<connectionStrings>
    <add name="DbConnection" 
         connectionString="Data Source=localhost;Initial Catalog=QuanLyDangVien;User ID=QuanLyDangVien_User;Password=QuanLyDangVien@2024;TrustServerCertificate=True;Connect Timeout=30" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Với Windows Authentication:
```xml
<connectionStrings>
    <add name="DbConnection" 
         connectionString="Data Source=localhost;Initial Catalog=QuanLyDangVien;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

## Kiểm tra setup thành công

Sau khi chạy script, bạn có thể kiểm tra:

1. Mở SQL Server Management Studio (SSMS)
2. Kết nối đến SQL Server
3. Kiểm tra database `QuanLyDangVien` đã được tạo
4. Kiểm tra các tables đã được tạo
5. Chạy ứng dụng và thử đăng nhập

## Hỗ trợ

Nếu gặp vấn đề, vui lòng kiểm tra:
- Log output của script
- SQL Server Error Log
- Windows Event Viewer

