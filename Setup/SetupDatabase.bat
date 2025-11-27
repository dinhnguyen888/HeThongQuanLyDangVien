@echo off
chcp 65001 >nul
echo ============================================
echo SETUP DATABASE QUẢN LÝ ĐẢNG VIÊN
echo ============================================
echo.

REM ============================================
REM CẤU HÌNH
REM ============================================
set SQL_SERVER=localhost
set SQL_INSTANCE=.
set DATABASE_NAME=QuanLyDangVien
set DB_USER=QuanLyDangVien_User
set DB_PASSWORD=QuanLyDangVien@2024
set SQL_SCRIPT=query.sql

REM Kiểm tra xem có tham số từ command line không
if not "%1"=="" set SQL_SERVER=%1
if not "%2"=="" set DATABASE_NAME=%2
if not "%3"=="" set DB_USER=%3
if not "%4"=="" set DB_PASSWORD=%4

echo Cấu hình:
echo   - SQL Server: %SQL_SERVER%
echo   - Database: %DATABASE_NAME%
echo   - User: %DB_USER%
echo   - Password: %DB_PASSWORD%
echo.

REM ============================================
REM KIỂM TRA SQL SERVER
REM ============================================
echo [1/5] Đang kiểm tra SQL Server...
sqlcmd -S %SQL_SERVER% -Q "SELECT @@VERSION" -b >nul 2>&1
if errorlevel 1 (
    echo ❌ Không thể kết nối đến SQL Server!
    echo.
    echo Vui lòng kiểm tra:
    echo   1. SQL Server đã được cài đặt chưa?
    echo   2. SQL Server đang chạy chưa? (SQL Server Service)
    echo   3. SQL Server có cho phép kết nối từ xa không?
    echo   4. Tên server có đúng không? (hiện tại: %SQL_SERVER%)
    echo.
    pause
    exit /b 1
)
echo ✓ SQL Server đã sẵn sàng
echo.

REM ============================================
REM TẠO DATABASE
REM ============================================
echo [2/5] Đang tạo database...
sqlcmd -S %SQL_SERVER% -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '%DATABASE_NAME%') CREATE DATABASE [%DATABASE_NAME%]" -b
if errorlevel 1 (
    echo ❌ Lỗi khi tạo database!
    pause
    exit /b 1
)
echo ✓ Database đã được tạo hoặc đã tồn tại
echo.

REM ============================================
REM TẠO LOGIN VÀ USER
REM ============================================
echo [3/5] Đang tạo login và user...
sqlcmd -S %SQL_SERVER% -Q "IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = '%DB_USER%') CREATE LOGIN [%DB_USER%] WITH PASSWORD = '%DB_PASSWORD%', DEFAULT_DATABASE = [%DATABASE_NAME%], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF" -b
if errorlevel 1 (
    echo ❌ Lỗi khi tạo login!
    pause
    exit /b 1
)

sqlcmd -S %SQL_SERVER% -d %DATABASE_NAME% -Q "IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = '%DB_USER%') CREATE USER [%DB_USER%] FOR LOGIN [%DB_USER%]" -b
if errorlevel 1 (
    echo ❌ Lỗi khi tạo user!
    pause
    exit /b 1
)

sqlcmd -S %SQL_SERVER% -d %DATABASE_NAME% -Q "ALTER ROLE db_owner ADD MEMBER [%DB_USER%]" -b
if errorlevel 1 (
    echo ❌ Lỗi khi cấp quyền cho user!
    pause
    exit /b 1
)
echo ✓ Login và user đã được tạo
echo.

REM ============================================
REM CHẠY SCRIPT SQL
REM ============================================
echo [4/5] Đang chạy script SQL...
set SQL_SCRIPT_PATH=%~dp0%SQL_SCRIPT%
if not exist "%SQL_SCRIPT_PATH%" (
    REM Thử tìm trong thư mục Database
    set SQL_SCRIPT_PATH=%~dp0..\Database\%SQL_SCRIPT%
    if not exist "%SQL_SCRIPT_PATH%" (
        echo ❌ Không tìm thấy file %SQL_SCRIPT%!
        echo    Đã tìm tại:
        echo      - %~dp0%SQL_SCRIPT%
        echo      - %~dp0..\Database\%SQL_SCRIPT%
        echo    Vui lòng đảm bảo file %SQL_SCRIPT% tồn tại
        pause
        exit /b 1
    )
)

echo    Đang chạy: %SQL_SCRIPT_PATH%
sqlcmd -S %SQL_SERVER% -d %DATABASE_NAME% -U %DB_USER% -P %DB_PASSWORD% -i "%SQL_SCRIPT_PATH%" -b
if errorlevel 1 (
    echo ❌ Lỗi khi chạy script SQL!
    echo    Vui lòng kiểm tra file %SQL_SCRIPT% và thử lại
    pause
    exit /b 1
)
echo ✓ Script SQL đã được chạy thành công
echo.

REM ============================================
REM CẬP NHẬT APP.CONFIG
REM ============================================
echo [5/5] Đang cập nhật App.config...
set APP_CONFIG_PATH=%~dp0..\App.config
if not exist "%APP_CONFIG_PATH%" (
    echo ⚠ Cảnh báo: Không tìm thấy App.config tại: %APP_CONFIG_PATH%
    echo    Bạn có thể cần cập nhật thủ công connection string trong App.config
    echo    Connection string mẫu:
    echo    Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;User ID=%DB_USER%;Password=%DB_PASSWORD%;TrustServerCertificate=True;Connect Timeout=30
    echo.
) else (
    REM Sử dụng PowerShell script riêng để cập nhật App.config
    set PS_SCRIPT=%~dp0UpdateAppConfig.ps1
    set CONNECTION_STRING=Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;User ID=%DB_USER%;Password=%DB_PASSWORD%;TrustServerCertificate=True;Connect Timeout=30
    
    if exist "%PS_SCRIPT%" (
        powershell -ExecutionPolicy Bypass -File "%PS_SCRIPT%" -ConfigPath "%APP_CONFIG_PATH%" -ConnectionString "%CONNECTION_STRING%"
        if errorlevel 1 (
            echo ⚠ Không thể tự động cập nhật App.config
            echo    Vui lòng cập nhật thủ công connection string:
            echo    %CONNECTION_STRING%
        ) else (
            echo ✓ App.config đã được cập nhật
        )
    ) else (
        REM Fallback: Tạo script PowerShell tạm
        set PS_SCRIPT_TEMP=%TEMP%\UpdateAppConfig_%RANDOM%.ps1
        (
            echo $configPath = '%APP_CONFIG_PATH%'
            echo $xml = [xml]^(Get-Content $configPath -Encoding UTF8^)
            echo $connectionString = '%CONNECTION_STRING%'
            echo $connectionNode = $xml.configuration.connectionStrings.add ^| Where-Object {$_.name -eq 'DbConnection'}
            echo if ^($connectionNode^) {
            echo     $connectionNode.connectionString = $connectionString
            echo } else {
            echo     $connectionStrings = $xml.configuration.connectionStrings
            echo     if ^(-not $connectionStrings^) {
            echo         $connectionStrings = $xml.CreateElement^('connectionStrings'^)
            echo         $xml.configuration.AppendChild^($connectionStrings^) ^| Out-Null
            echo     }
            echo     $newNode = $xml.CreateElement^('add'^)
            echo     $newNode.SetAttribute^('name', 'DbConnection'^)
            echo     $newNode.SetAttribute^('connectionString', $connectionString^)
            echo     $newNode.SetAttribute^('providerName', 'System.Data.SqlClient'^)
            echo     $connectionStrings.AppendChild^($newNode^) ^| Out-Null
            echo }
            echo $xml.Save^($configPath^)
        ) > "%PS_SCRIPT_TEMP%"
        
        powershell -ExecutionPolicy Bypass -File "%PS_SCRIPT_TEMP%"
        if errorlevel 1 (
            echo ⚠ Không thể tự động cập nhật App.config
            echo    Vui lòng cập nhật thủ công connection string:
            echo    %CONNECTION_STRING%
        ) else (
            echo ✓ App.config đã được cập nhật
        )
        del "%PS_SCRIPT_TEMP%" >nul 2>&1
    )
)
echo.

REM ============================================
REM HOÀN TẤT
REM ============================================
echo ============================================
echo ✓ SETUP HOÀN TẤT!
echo ============================================
echo.
echo Thông tin kết nối:
echo   Server: %SQL_SERVER%
echo   Database: %DATABASE_NAME%
echo   User: %DB_USER%
echo   Password: %DB_PASSWORD%
echo.
echo Connection String:
echo   Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;User ID=%DB_USER%;Password=%DB_PASSWORD%;TrustServerCertificate=True;Connect Timeout=30
echo.
pause

