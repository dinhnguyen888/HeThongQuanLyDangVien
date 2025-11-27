@echo off
chcp 65001 >nul
echo ============================================
echo CÀI ĐẶT SQL SERVER EXPRESS
echo ============================================
echo.

REM ============================================
REM KIỂM TRA SQL SERVER ĐÃ CÀI CHƯA
REM ============================================
echo [1/4] Đang kiểm tra SQL Server...
sqlcmd -S localhost -Q "SELECT @@VERSION" -b >nul 2>&1
if not errorlevel 1 (
    echo ✓ SQL Server đã được cài đặt và đang chạy
    echo.
    goto :SetupDatabase
)

REM Kiểm tra SQL Server Service
sc query MSSQLSERVER >nul 2>&1
if not errorlevel 1 (
    echo ⚠ SQL Server đã được cài nhưng service chưa chạy
    echo    Đang khởi động SQL Server Service...
    net start MSSQLSERVER >nul 2>&1
    timeout /t 5 >nul
    sqlcmd -S localhost -Q "SELECT @@VERSION" -b >nul 2>&1
    if not errorlevel 1 (
        echo ✓ SQL Server đã được khởi động
        echo.
        goto :SetupDatabase
    )
)

REM ============================================
REM TẢI SQL SERVER EXPRESS
REM ============================================
echo [2/4] SQL Server chưa được cài đặt
echo    Đang tải SQL Server Express...
echo.

REM URL tải SQL Server Express 2019 (miễn phí)
set SQL_EXPRESS_URL=https://go.microsoft.com/fwlink/?linkid=866658
set SQL_EXPRESS_INSTALLER=%~dp0SQLServerExpress.exe

REM Kiểm tra xem có file installer sẵn không (nếu đã tải trước)
if exist "%SQL_EXPRESS_INSTALLER%" (
    echo ✓ File SQL Server Express đã tồn tại trong thư mục Setup
    echo    Sẽ sử dụng file này để cài đặt
    echo.
    goto :InstallSQL
)

echo    Đang tải SQL Server Express từ Microsoft...
echo    (Có thể mất vài phút tùy vào tốc độ internet)
echo    File sẽ được lưu tại: %SQL_EXPRESS_INSTALLER%
echo.

REM Sử dụng PowerShell để tải file
powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; $ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -Uri '%SQL_EXPRESS_URL%' -OutFile '%SQL_EXPRESS_INSTALLER%' -UseBasicParsing}"

if not exist "%SQL_EXPRESS_INSTALLER%" (
    echo ❌ Không thể tải SQL Server Express!
    echo.
    echo Vui lòng tải thủ công từ:
    echo https://www.microsoft.com/en-us/sql-server/sql-server-downloads
    echo.
    echo Sau khi tải, đặt file vào thư mục Setup với tên: SQLServerExpress.exe
    echo.
    echo Hoặc sử dụng SQL Server Express LocalDB (nhẹ hơn):
    echo Chạy: InstallSQLServerLocalDB.bat
    echo.
    pause
    exit /b 1
)

echo ✓ Đã tải SQL Server Express thành công
echo.

:InstallSQL

REM ============================================
REM CÀI ĐẶT SQL SERVER EXPRESS
REM ============================================
echo [3/4] Đang cài đặt SQL Server Express...
echo    (Quá trình này có thể mất 10-20 phút)
echo    Vui lòng làm theo hướng dẫn trên màn hình cài đặt
echo.

REM Tạo file cấu hình cài đặt
set CONFIG_FILE=%~dp0SQLExpress_ConfigurationFile.ini
(
    echo [OPTIONS]
    echo IACCEPTSQLSERVERLICENSETERMS="True"
    echo ACTION=Install
    echo FEATURES=SQLENGINE
    echo INSTANCENAME=MSSQLSERVER
    echo SQLSYSADMINACCOUNTS="BUILTIN\Administrators"
    echo SECURITYMODE=SQL
    echo SAPWD="QuanLyDangVien@2024"
    echo TCPENABLED=1
    echo NPENABLED=1
    echo SQLCOLLATION="SQL_Latin1_General_CP1_CI_AS"
    echo SQLSVCACCOUNT="NT AUTHORITY\SYSTEM"
    echo SQLSVCSTARTUPTYPE=Automatic
    echo SQLTEMPDBDIR="C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Data"
    echo SQLUSERDBDIR="C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Data"
    echo SQLUSERDBLOGDIR="C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Data"
    echo INSTALLSQLDATADIR="C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Data"
    echo SQLBACKUPDIR="C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup"
    echo AGTSVCACCOUNT="NT AUTHORITY\SYSTEM"
    echo AGTSVCSTARTUPTYPE=Disabled
    echo SQLSVCINSTANTFILEINIT="True"
    echo IACCEPTPYTHONLICENSETERMS="False"
    echo IACCEPTROPENLICENSETERMS="False"
    echo PRODUCTKEY=""
    echo ENU="True"
    echo UpdateEnabled="False"
    echo SUPPRESSPRIVACYSTATEMENTNOTICE="True"
    echo INDICATEPROGRESS="True"
    echo X86="False"
    echo FILESTREAMLEVEL=0
    echo FILESTREAMSHARENAME="MSSQLSERVER"
    echo FTSVCACCOUNT="NT AUTHORITY\SYSTEM"
    echo BROWSERSVCSTARTUPTYPE="Automatic"
    echo SQLSVCINSTANTFILEINIT="True"
) > "%CONFIG_FILE%"

REM Chạy cài đặt
echo    Đang chạy installer...
"%SQL_EXPRESS_INSTALLER%" /ConfigurationFile="%CONFIG_FILE%" /Q

if errorlevel 1 (
    echo.
    echo ⚠ Cài đặt có thể đã hoàn tất hoặc có lỗi
    echo    Đang kiểm tra lại...
    echo.
    
    REM Đợi một chút để SQL Server khởi động
    timeout /t 10 >nul
    
    REM Kiểm tra lại
    sqlcmd -S localhost -Q "SELECT @@VERSION" -b >nul 2>&1
    if errorlevel 1 (
        echo ❌ SQL Server vẫn chưa sẵn sàng
        echo    Vui lòng kiểm tra lại hoặc cài đặt thủ công
        echo.
        pause
        exit /b 1
    )
)

echo ✓ SQL Server Express đã được cài đặt
echo.

REM ============================================
REM SETUP DATABASE
REM ============================================
:SetupDatabase
echo [4/4] Đang chuyển sang bước setup database...
echo.

REM Chạy script setup database
call "%~dp0SetupDatabase.bat" %*

REM Xóa file installer nếu muốn (tùy chọn)
REM del "%SQL_EXPRESS_INSTALLER%" >nul 2>&1
REM del "%CONFIG_FILE%" >nul 2>&1

echo.
echo ============================================
echo ✓ HOÀN TẤT CÀI ĐẶT!
echo ============================================
pause

