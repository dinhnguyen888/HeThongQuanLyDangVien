# PowerShell script để cập nhật App.config
# Sử dụng bởi SetupDatabase.bat và SetupDatabase_WithWindowsAuth.bat

param(
    [Parameter(Mandatory=$true)]
    [string]$ConfigPath,
    
    [Parameter(Mandatory=$true)]
    [string]$ConnectionString
)

try {
    # Kiểm tra file tồn tại
    if (-not (Test-Path $ConfigPath)) {
        Write-Host "❌ Không tìm thấy file: $ConfigPath" -ForegroundColor Red
        exit 1
    }
    
    # Đọc file XML
    [xml]$xml = Get-Content $ConfigPath -Encoding UTF8
    
    # Tìm node connectionStrings
    $connectionStrings = $xml.configuration.connectionStrings
    
    if ($null -eq $connectionStrings) {
        # Tạo node connectionStrings nếu chưa có
        $connectionStrings = $xml.CreateElement('connectionStrings')
        $xml.configuration.AppendChild($connectionStrings) | Out-Null
    }
    
    # Tìm node DbConnection
    $dbConnectionNode = $connectionStrings.add | Where-Object { $_.name -eq 'DbConnection' }
    
    if ($null -eq $dbConnectionNode) {
        # Tạo node mới nếu chưa có
        $dbConnectionNode = $xml.CreateElement('add')
        $dbConnectionNode.SetAttribute('name', 'DbConnection')
        $dbConnectionNode.SetAttribute('connectionString', $ConnectionString)
        $dbConnectionNode.SetAttribute('providerName', 'System.Data.SqlClient')
        $connectionStrings.AppendChild($dbConnectionNode) | Out-Null
    } else {
        # Cập nhật connection string
        $dbConnectionNode.connectionString = $ConnectionString
    }
    
    # Lưu file
    $xml.Save($ConfigPath)
    
    Write-Host "✓ Đã cập nhật App.config thành công" -ForegroundColor Green
    exit 0
} catch {
    Write-Host "❌ Lỗi khi cập nhật App.config: $_" -ForegroundColor Red
    exit 1
}

