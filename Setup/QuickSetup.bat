@echo off
chcp 65001 >nul
echo ============================================
echo QUICK SETUP - TỰ ĐỘNG CHỌN PHƯƠNG THỨC
echo ============================================
echo.
echo Bạn muốn setup với phương thức nào?
echo.
echo [1] SQL Authentication (Tạo user và password)
echo [2] Windows Authentication (Sử dụng tài khoản Windows)
echo.
set /p choice="Chọn (1 hoặc 2): "

if "%choice%"=="1" (
    call "%~dp0SetupDatabase.bat" %*
) else if "%choice%"=="2" (
    call "%~dp0SetupDatabase_WithWindowsAuth.bat" %*
) else (
    echo Lựa chọn không hợp lệ!
    pause
    exit /b 1
)

