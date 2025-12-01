@echo off
setlocal ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

set DB_NAME=QuanLyDangVien
set SQL_FILE=query.sql
set LOCALDB_INSTANCE=MSSQLLocalDB

echo ============================
echo  KHOI TAO LOCALDB TU DONG
echo ============================
echo.

echo [1/6] Kiem tra LocalDB da duoc cai dat...
sqllocaldb info "%LOCALDB_INSTANCE%" >nul 2>&1
if errorlevel 1 (
    echo LocalDB chua duoc cai dat. Bat dau qua trinh cai dat tu dong...
    echo.
    call :InstallLocalDB
    if errorlevel 1 (
        echo.
        echo LOI: Khong the cai dat LocalDB tu dong.
        echo Vui long kiem tra:
        echo   1. File SqlLocalDB.msi co nam cung thu muc voi setup.bat khong
        echo   2. Ban da chay setup.bat voi quyen Administrator chua
        echo   3. Neu chua co file, tai ve tu: https://go.microsoft.com/fwlink/?LinkID=866658
        echo.
        goto :end
    )
    echo.
    echo OK: LocalDB da duoc cai dat thanh cong.
) else (
    echo OK: LocalDB da duoc cai dat.
)
echo.

echo [2/6] Khoi dong LocalDB instance...
sqllocaldb start "%LOCALDB_INSTANCE%" >nul 2>&1
if errorlevel 1 (
    echo Canh bao: Khong the khoi dong LocalDB instance. Co the da chay roi.
)
echo OK: LocalDB instance da san sang.
echo.

echo [3/6] Kiem tra va cai dat PowerShell module SqlServer...
powershell -ExecutionPolicy Bypass -Command "Set-PSRepository -Name PSGallery -InstallationPolicy Trusted; if (-not (Get-Module -ListAvailable -Name SqlServer)) { Write-Host 'Dang cai dat module SqlServer...'; Install-Module -Name SqlServer -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop; Write-Host 'OK: Module SqlServer da duoc cai dat.' } else { Write-Host 'OK: Module SqlServer da duoc cai dat.' }"
if errorlevel 1 (
    echo LOI: Khong the cai dat module SqlServer.
    echo Vui long kiem tra:
    echo   1. Ket noi internet de tai module tu PowerShell Gallery
    echo   2. Hoac cai dat thu cong: Install-Module -Name SqlServer -Scope CurrentUser -Force
    echo.
    goto :end
)
echo OK: Module SqlServer da san sang.
echo.

echo [4/6] Tao database %DB_NAME% neu chua ton tai...
set SCRIPT_DIR=%~dp0
REM Tao database voi duong dan trong thu muc AppData cua user (co quyen ghi)
powershell -ExecutionPolicy Bypass -Command "$server = '(localdb)\%LOCALDB_INSTANCE%'; $dbName = '%DB_NAME%'; $dbPath = [Environment]::GetFolderPath('LocalApplicationData') + '\QuanLyDangVien'; if (-not (Test-Path $dbPath)) { New-Item -ItemType Directory -Path $dbPath -Force | Out-Null }; $mdfPath = Join-Path $dbPath ($dbName + '.mdf'); $ldfPath = Join-Path $dbPath ($dbName + '_log.ldf'); Import-Module SqlServer -ErrorAction Stop; $query = \"IF DB_ID('$dbName') IS NULL BEGIN CREATE DATABASE [$dbName] ON (NAME = '$dbName', FILENAME = '$mdfPath') LOG ON (NAME = '${dbName}_log', FILENAME = '$ldfPath'); END\"; Invoke-Sqlcmd -ServerInstance $server -Query $query -ErrorAction Stop"
if errorlevel 1 (
    echo LOI: Khong tao duoc database %DB_NAME%.
    echo Vui long kiem tra lai LocalDB da chay chua.
    echo.
    goto :end
)
echo OK: Database %DB_NAME% da san sang.
echo.

echo [5/6] Chay file %SQL_FILE% de tao cau truc database...
if not exist "%SQL_FILE%" (
    echo LOI: Khong tim thay file %SQL_FILE% trong thu muc hien tai.
    echo Dam bao file %SQL_FILE% nam cung thu muc voi setup.bat.
    echo.
    goto :end
)

powershell -ExecutionPolicy Bypass -Command "$ErrorActionPreference = 'Stop'; $server = '(localdb)\%LOCALDB_INSTANCE%'; $dbName = '%DB_NAME%'; $sqlFile = '%SCRIPT_DIR%%SQL_FILE%'; [Console]::OutputEncoding = [System.Text.Encoding]::UTF8; [Console]::InputEncoding = [System.Text.Encoding]::UTF8; $PSDefaultParameterValues['*:Encoding'] = 'utf8'; Import-Module SqlServer -ErrorAction Stop; Invoke-Sqlcmd -ServerInstance $server -Database $dbName -InputFile $sqlFile -ErrorAction Stop"
if errorlevel 1 (
    echo LOI: Co loi khi chay file %SQL_FILE%.
    echo Vui long kiem tra lai noi dung file SQL.
    echo.
    goto :end
)

echo.
echo [6/6] Hoan tat khoi tao database.
echo.
echo === HOAN TAT KHOI TAO DATABASE ===
echo Thong tin ket noi de su dung trong App.config:
echo   Server=(localdb)\%LOCALDB_INSTANCE%;Integrated Security=true;Database=%DB_NAME%;Connect Timeout=30
echo.

goto :end

:InstallLocalDB
echo.
echo === CAI DAT LOCALDB ===
echo.

REM Kiem tra quyen admin
net session >nul 2>&1
if errorlevel 1 (
    echo Canh bao: Khong co quyen Administrator.
    echo Cai dat LocalDB can quyen Administrator.
    echo Vui long chuot phai vao setup.bat va chon "Run as administrator".
    echo.
    exit /b 1
)

REM Tim file SqlLocalDB.msi trong cung thu muc voi setup.bat
set SCRIPT_DIR=%~dp0
set INSTALLER_FILE=%SCRIPT_DIR%SqlLocalDB.msi

if not exist "%INSTALLER_FILE%" (
    echo LOI: Khong tim thay file SqlLocalDB.msi trong thu muc cai dat.
    echo Duong dan kiem tra: %INSTALLER_FILE%
    echo.
    echo Vui long dam bao file SqlLocalDB.msi nam cung thu muc voi setup.bat.
    echo Ban co the tai ve tu: https://go.microsoft.com/fwlink/?LinkID=866658
    echo.
    exit /b 1
)

echo Tim thay file SqlLocalDB.msi. Dang cai dat ngam...
msiexec /i "%INSTALLER_FILE%" /quiet /norestart IACCEPTSQLLOCALDBLICENSETERMS=YES
if errorlevel 1 (
    echo LOI: Qua trinh cai dat LocalDB that bai.
    exit /b 1
)

echo OK: LocalDB da duoc cai dat thanh cong.
timeout /t 2 >nul

REM Kiem tra lai sau khi cai dat
sqllocaldb info "%LOCALDB_INSTANCE%" >nul 2>&1
if errorlevel 1 (
    echo Canh bao: LocalDB da duoc cai dat nhung chua the ket noi.
    echo Vui long khoi dong lai may hoac chay lai setup.bat.
    exit /b 1
)

exit /b 0

:end
echo Nhan phim bat ky de thoat...
pause >nul
endlocal
exit /b 0

REM ============================================
REM PHAN SCRIPT SQL SERVER CU (DA COMMENT)
REM Neu can chuyen lai SQL Server, bo comment phan duoi va comment phan LocalDB o tren
REM ============================================
REM @echo off
REM SETLOCAL
REM 
REM set SCRIPT_DIR=%~dp0
REM set DB_NAME=QuanLyDangVien
REM set LOGIN_NAME=adminquanly
REM set LOGIN_PASS=adminquanly
REM 
REM echo ============================
REM echo  KHOI TAO SQL TU DONG
REM echo ============================
REM 
REM REM --- 1. Tao file SQL tam: tao_login.sql ---
REM echo IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = '%LOGIN_NAME%') > "%SCRIPT_DIR%tao_login.sql"
REM echo BEGIN >> "%SCRIPT_DIR%tao_login.sql"
REM echo     CREATE LOGIN [%LOGIN_NAME%] WITH PASSWORD = '%LOGIN_PASS%', CHECK_POLICY = OFF; >> "%SCRIPT_DIR%tao_login.sql"
REM echo END >> "%SCRIPT_DIR%tao_login.sql"
REM 
REM echo Tao login...
REM sqlcmd -i "%SCRIPT_DIR%tao_login.sql"
REM 
REM REM --- 2. Tao database ---
REM echo IF DB_ID('%DB_NAME%') IS NULL > "%SCRIPT_DIR%tao_db.sql"
REM echo BEGIN >> "%SCRIPT_DIR%tao_db.sql"
REM echo     CREATE DATABASE [%DB_NAME%]; >> "%SCRIPT_DIR%tao_db.sql"
REM echo END >> "%SCRIPT_DIR%tao_db.sql"
REM 
REM echo Tao database...
REM sqlcmd -i "%SCRIPT_DIR%tao_db.sql"
REM 
REM REM --- 3. Tao user ---
REM echo USE [%DB_NAME%]; > "%SCRIPT_DIR%tao_user.sql"
REM echo IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = '%LOGIN_NAME%') >> "%SCRIPT_DIR%tao_user.sql"
REM echo BEGIN >> "%SCRIPT_DIR%tao_user.sql"
REM echo     CREATE USER [%LOGIN_NAME%] FOR LOGIN [%LOGIN_NAME%]; >> "%SCRIPT_DIR%tao_user.sql"
REM echo     ALTER ROLE db_owner ADD MEMBER [%LOGIN_NAME%]; >> "%SCRIPT_DIR%tao_user.sql"
REM echo END >> "%SCRIPT_DIR%tao_user.sql"
REM 
REM echo Tao user...
REM sqlcmd -i "%SCRIPT_DIR%tao_user.sql"
REM 
REM REM --- 4. Import query.sql ---
REM if not exist "%SCRIPT_DIR%query.sql" (
REM     echo Loi: Khong tim thay file query.sql trong thu muc:
REM     echo %SCRIPT_DIR%
REM     pause
REM     exit /b
REM )
REM 
REM echo Import query.sql...
REM sqlcmd -d %DB_NAME% -i "%SCRIPT_DIR%query.sql"
REM 
REM REM --- Xoa file tam ---
REM del "%SCRIPT_DIR%tao_login.sql"
REM del "%SCRIPT_DIR%tao_db.sql"
REM del "%SCRIPT_DIR%tao_user.sql"
REM 
REM echo ============================
REM echo  HOAN TAT !!!
REM echo ============================
REM pause
