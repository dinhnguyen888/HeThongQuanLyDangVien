@echo off
chcp 65001 >nul
echo ============================================
echo SETUP HOÀN CHỈNH - QUẢN LÝ ĐẢNG VIÊN
echo ============================================
echo.
echo Script này sẽ:
echo   1. Kiểm tra/cài đặt SQL Server
echo   2. Tạo database và user
echo   3. Chạy script SQL để init dữ liệu
echo   4. Cập nhật App.config
echo.
echo ============================================
echo CHỌN PHƯƠNG THỨC CÀI ĐẶT SQL SERVER
echo ============================================
echo.
echo [1] SQL Server Express (Đầy đủ tính năng, ~500MB)
echo [2] SQL Server Express LocalDB (Nhẹ, ~100MB, khuyến nghị)
echo [3] Bỏ qua - SQL Server đã được cài đặt
echo.
set /p choice="Chọn (1, 2 hoặc 3): "

if "%choice%"=="1" (
    call "%~dp0InstallSQLServer.bat"
) else if "%choice%"=="2" (
    call "%~dp0InstallSQLServerLocalDB.bat"
) else if "%choice%"=="3" (
    echo.
    echo Bạn đã chọn bỏ qua cài đặt SQL Server
    echo Đang chuyển sang setup database...
    echo.
    call "%~dp0QuickSetup.bat"
) else (
    echo Lựa chọn không hợp lệ!
    pause
    exit /b 1
)

