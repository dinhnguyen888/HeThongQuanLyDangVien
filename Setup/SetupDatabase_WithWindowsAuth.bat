@echo off
chcp 65001 >nul
echo ============================================
echo SETUP DATABASE QUẢN LÝ ĐẢNG VIÊN
echo (Windows Authentication)
echo ============================================
echo.

REM ============================================
REM CẤU HÌNH
REM ============================================
set SQL_SERVER=localhost
set DATABASE_NAME=QuanLyDangVien
set SQL_SCRIPT=query.sql

REM Kiểm tra xem có tham số từ command line không
if not "%1"=="" set SQL_SERVER=%1
if not "%2"=="" set DATABASE_NAME=%2

echo Cấu hình:
echo   - SQL Server: %SQL_SERVER%
echo   - Database: %DATABASE_NAME%
echo   - Authentication: Windows Authentication
echo.

REM ============================================
REM KIỂM TRA SQL SERVER
REM ============================================
echo [1/4] Đang kiểm tra SQL Server...
sqlcmd -S %SQL_SERVER% -E -Q "SELECT @@VERSION" -b >nul 2>&1
if errorlevel 1 (
    echo ❌ Không thể kết nối đến SQL Server!
    echo.
    echo Vui lòng kiểm tra:
    echo   1. SQL Server đã được cài đặt chưa?
    echo   2. SQL Server đang chạy chưa? (SQL Server Service)
    echo   3. SQL Server có cho phép kết nối từ xa không?
    echo   4. Tên server có đúng không? (hiện tại: %SQL_SERVER%)
    echo   5. Tài khoản Windows hiện tại có quyền truy cập SQL Server không?
    echo.
    pause
    exit /b 1
)
echo ✓ SQL Server đã sẵn sàng
echo.

REM ============================================
REM TẠO DATABASE
REM ============================================
echo [2/4] Đang tạo database...
sqlcmd -S %SQL_SERVER% -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '%DATABASE_NAME%') CREATE DATABASE [%DATABASE_NAME%]" -b
if errorlevel 1 (
    echo ❌ Lỗi khi tạo database!
    pause
    exit /b 1
)
echo ✓ Database đã được tạo hoặc đã tồn tại
echo.

REM ============================================
REM CHẠY SCRIPT SQL
REM ============================================
echo [3/4] Đang chạy script SQL...
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
sqlcmd -S %SQL_SERVER% -d %DATABASE_NAME% -E -i "%SQL_SCRIPT_PATH%" -b
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
echo [4/4] Đang cập nhật App.config...
set APP_CONFIG_PATH=%~dp0..\App.config
if not exist "%APP_CONFIG_PATH%" (
    echo ⚠ Cảnh báo: Không tìm thấy App.config tại: %APP_CONFIG_PATH%
    echo    Bạn có thể cần cập nhật thủ công connection string trong App.config
    echo    Connection string mẫu:
    echo    Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30
    echo.
) else (
    REM Sử dụng PowerShell script riêng để cập nhật App.config
    set PS_SCRIPT=%~dp0UpdateAppConfig.ps1
    set CONNECTION_STRING=Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30
    
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
echo   Authentication: Windows Authentication
echo.
echo Connection String:
echo   Data Source=%SQL_SERVER%;Initial Catalog=%DATABASE_NAME%;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30
echo.
pause

