@echo off
SETLOCAL

set SCRIPT_DIR=%~dp0
set DB_NAME=QuanLyDangVien
set LOGIN_NAME=adminquanly
set LOGIN_PASS=adminquanly

echo ============================
echo  KHOI TAO SQL TU DONG
echo ============================

REM --- 1. Tạo file SQL tạm: tao_login.sql ---
echo IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = '%LOGIN_NAME%') > "%SCRIPT_DIR%tao_login.sql"
echo BEGIN >> "%SCRIPT_DIR%tao_login.sql"
echo     CREATE LOGIN [%LOGIN_NAME%] WITH PASSWORD = '%LOGIN_PASS%', CHECK_POLICY = OFF; >> "%SCRIPT_DIR%tao_login.sql"
echo END >> "%SCRIPT_DIR%tao_login.sql"

echo Tao login...
sqlcmd -i "%SCRIPT_DIR%tao_login.sql"


REM --- 2. Tạo database ---
echo IF DB_ID('%DB_NAME%') IS NULL > "%SCRIPT_DIR%tao_db.sql"
echo BEGIN >> "%SCRIPT_DIR%tao_db.sql"
echo     CREATE DATABASE [%DB_NAME%]; >> "%SCRIPT_DIR%tao_db.sql"
echo END >> "%SCRIPT_DIR%tao_db.sql"

echo Tao database...
sqlcmd -i "%SCRIPT_DIR%tao_db.sql"


REM --- 3. Tạo user ---
echo USE [%DB_NAME%]; > "%SCRIPT_DIR%tao_user.sql"
echo IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = '%LOGIN_NAME%') >> "%SCRIPT_DIR%tao_user.sql"
echo BEGIN >> "%SCRIPT_DIR%tao_user.sql"
echo     CREATE USER [%LOGIN_NAME%] FOR LOGIN [%LOGIN_NAME%]; >> "%SCRIPT_DIR%tao_user.sql"
echo     ALTER ROLE db_owner ADD MEMBER [%LOGIN_NAME%]; >> "%SCRIPT_DIR%tao_user.sql"
echo END >> "%SCRIPT_DIR%tao_user.sql"

echo Tao user...
sqlcmd -i "%SCRIPT_DIR%tao_user.sql"


REM --- 4. Import query.sql ---
if not exist "%SCRIPT_DIR%query.sql" (
    echo Loi: Khong tim thay file query.sql trong thu muc:
    echo %SCRIPT_DIR%
    pause
    exit /b
)

echo Import query.sql...
sqlcmd -d %DB_NAME% -i "%SCRIPT_DIR%query.sql"


REM --- Xóa file tạm ---
del "%SCRIPT_DIR%tao_login.sql"
del "%SCRIPT_DIR%tao_db.sql"
del "%SCRIPT_DIR%tao_user.sql"

echo ============================
echo  HOAN TAT !!!
echo ============================
pause
