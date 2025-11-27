@echo off
chcp 65001 >nul
echo ============================================
echo CÀI ĐẶT SQL SERVER EXPRESS LOCALDB
echo (Nhẹ hơn, phù hợp cho máy yếu)
echo ============================================
echo.

REM ============================================
REM KIỂM TRA SQL SERVER LOCALDB ĐÃ CÀI CHƯA
REM ============================================
echo [1/4] Đang kiểm tra SQL Server LocalDB...
sqllocaldb info >nul 2>&1
if not errorlevel 1 (
    echo ✓ SQL Server LocalDB đã được cài đặt
    echo.
    goto :SetupDatabase
)

REM ============================================
REM TẢI SQL SERVER EXPRESS LOCALDB
REM ============================================
echo [2/4] SQL Server LocalDB chưa được cài đặt
echo    Đang tải SQL Server Express LocalDB...
echo.

REM URL tải SQL Server Express LocalDB (nhẹ hơn)
set SQL_LOCALDB_URL=https://go.microsoft.com/fwlink/?linkid=866658
set SQL_LOCALDB_INSTALLER=%~dp0SQLServerLocalDB.exe

REM Kiểm tra xem có file installer sẵn không (nếu đã tải trước)
if exist "%SQL_LOCALDB_INSTALLER%" (
    echo ✓ File SQL Server Express LocalDB đã tồn tại trong thư mục Setup
    echo    Sẽ sử dụng file này để cài đặt
    echo.
    goto :InstallLocalDB
)

echo    Đang tải SQL Server Express LocalDB từ Microsoft...
echo    (Có thể mất vài phút tùy vào tốc độ internet)
echo    File sẽ được lưu tại: %SQL_LOCALDB_INSTALLER%
echo.

REM Sử dụng PowerShell để tải file
powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; $ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -Uri '%SQL_LOCALDB_URL%' -OutFile '%SQL_LOCALDB_INSTALLER%' -UseBasicParsing}"

if not exist "%SQL_LOCALDB_INSTALLER%" (
    echo ❌ Không thể tải SQL Server Express LocalDB!
    echo.
    echo Vui lòng tải thủ công từ:
    echo https://www.microsoft.com/en-us/sql-server/sql-server-downloads
    echo.
    echo Sau khi tải, đặt file vào thư mục Setup với tên: SQLServerLocalDB.exe
    echo.
    pause
    exit /b 1
)

echo ✓ Đã tải SQL Server Express LocalDB thành công
echo.

:InstallLocalDB

REM ============================================
REM CÀI ĐẶT SQL SERVER EXPRESS LOCALDB
REM ============================================
echo [3/4] Đang cài đặt SQL Server Express LocalDB...
echo    (Quá trình này có thể mất 5-10 phút)
echo.

REM Tạo file cấu hình cài đặt cho LocalDB
set CONFIG_FILE=%~dp0SQLLocalDB_ConfigurationFile.ini
(
    echo [OPTIONS]
    echo IACCEPTSQLSERVERLICENSETERMS="True"
    echo ACTION=Install
    echo FEATURES=SQLEngine
    echo INSTANCENAME=MSSQLLocalDB
    echo SQLSYSADMINACCOUNTS="BUILTIN\Administrators"
    echo SECURITYMODE=SQL
    echo SAPWD="QuanLyDangVien@2024"
    echo TCPENABLED=1
    echo NPENABLED=1
    echo SQLSVCACCOUNT="NT AUTHORITY\SYSTEM"
    echo SQLSVCSTARTUPTYPE=Automatic
) > "%CONFIG_FILE%"

REM Chạy cài đặt LocalDB (silent mode với config file)
"%SQL_LOCALDB_INSTALLER%" /ConfigurationFile="%CONFIG_FILE%" /Q

if errorlevel 1 (
    echo.
    echo ⚠ Cài đặt có thể đã hoàn tất hoặc có lỗi
    echo    Đang kiểm tra lại...
    echo.
    
    REM Đợi một chút
    timeout /t 5 >nul
    
    REM Kiểm tra lại
    sqllocaldb info >nul 2>&1
    if errorlevel 1 (
        echo ❌ SQL Server LocalDB vẫn chưa sẵn sàng
        echo    Vui lòng kiểm tra lại hoặc cài đặt thủ công
        echo.
        pause
        exit /b 1
    )
)

echo ✓ SQL Server Express LocalDB đã được cài đặt
echo.

REM ============================================
REM SETUP DATABASE
REM ============================================
:SetupDatabase
echo [4/4] Đang chuyển sang bước setup database...
echo.

REM Với LocalDB, connection string sẽ khác một chút
REM Tạo instance nếu chưa có
sqllocaldb create "MSSQLLocalDB" -s >nul 2>&1

REM Đợi một chút để LocalDB khởi động
timeout /t 5 >nul

REM Chạy script setup database với LocalDB connection
REM LocalDB sử dụng Windows Authentication mặc định
call "%~dp0SetupDatabase_WithWindowsAuth.bat" "(localdb)\MSSQLLocalDB" %*

echo.
echo ============================================
echo ✓ HOÀN TẤT CÀI ĐẶT!
echo ============================================
echo.
echo Lưu ý: Với LocalDB, connection string sẽ là:
echo   Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyDangVien;...
echo.
pause

