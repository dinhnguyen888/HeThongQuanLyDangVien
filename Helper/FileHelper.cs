using System;
using System.Configuration;
using System.IO;

namespace QuanLyDangVien.Helper
{
    public static class FileHelper
    {
        /// <summary>
        /// Lấy đường dẫn thư mục Server base từ App.config
        /// </summary>
        private static string GetServerBaseFolder()
        {
            // Lấy base path từ App.config giống SaveInforHelper
            string baseFolderPath = ConfigurationManager.AppSettings["UserInfoFolderPath"];
            if (string.IsNullOrWhiteSpace(baseFolderPath))
            {
                baseFolderPath = @"C:\QuanLyDangVien\Data";
            }

            // Lấy thư mục cha (C:\QuanLyDangVien) từ Data folder
            // Ví dụ: C:\QuanLyDangVien\Data -> C:\QuanLyDangVien
            DirectoryInfo dirInfo = new DirectoryInfo(baseFolderPath);
            DirectoryInfo parentDir = dirInfo.Parent;
            
            if (parentDir != null && parentDir.Exists)
            {
                return parentDir.FullName;
            }

            // Nếu không lấy được parent, thử dùng Path.GetDirectoryName
            string parentFolder = Path.GetDirectoryName(baseFolderPath);
            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                return parentFolder;
            }

            // Fallback về C:\QuanLyDangVien nếu vẫn null
            return @"C:\QuanLyDangVien";
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Khen thưởng
        /// </summary>
        public static string GetKhenThuongFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "KhenThuong");
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Kỷ luật
        /// </summary>
        public static string GetKyLuatFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "KyLuat");
        }

        /// <summary>
        /// Lưu file vào thư mục Khen thưởng
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\KhenThuong\filename.pdf)</returns>
        public static string SaveKhenThuongFile(string sourceFilePath)
        {
            try
            {
                string folder = GetKhenThuongFolder();
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "KhenThuong", newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file khen thưởng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu file vào thư mục Kỷ luật
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\KyLuat\filename.pdf)</returns>
        public static string SaveKyLuatFile(string sourceFilePath)
        {
            try
            {
                string folder = GetKyLuatFolder();
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "KyLuat", newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file kỷ luật: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ từ đường dẫn tương đối
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối (ví dụ: Server\KhenThuong\file.pdf)</param>
        /// <returns>Đường dẫn đầy đủ</returns>
        public static string GetFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return null;
            }

            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, relativePath);
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Hồ sơ đảng viên
        /// </summary>
        public static string GetHoSoDangVienFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "HoSoDangVien");
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Chuyển sinh hoạt đảng
        /// </summary>
        public static string GetChuyenSinhHoatFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "ChuyenSinhHoat");
        }

        /// <summary>
        /// Lưu file quyết định chuyển sinh hoạt đảng vào thư mục
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\ChuyenSinhHoat\filename.pdf)</returns>
        public static string SaveChuyenSinhHoatFile(string sourceFilePath)
        {
            try
            {
                string folder = GetChuyenSinhHoatFolder();
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "ChuyenSinhHoat", newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file chuyển sinh hoạt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Sinh hoạt chi bộ
        /// </summary>
        public static string GetSinhHoatChiBoFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "SinhHoatChiBo");
        }

        /// <summary>
        /// Lưu file nghị quyết sinh hoạt chi bộ vào thư mục
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\SinhHoatChiBo\filename.pdf)</returns>
        public static string SaveSinhHoatChiBoFile(string sourceFilePath)
        {
            try
            {
                string folder = GetSinhHoatChiBoFolder();
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "SinhHoatChiBo", newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file sinh hoạt chi bộ: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu file hồ sơ đảng viên vào thư mục
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\HoSoDangVien\filename.pdf)</returns>
        public static string SaveHoSoDangVienFile(string sourceFilePath)
        {
            try
            {
                string folder = GetHoSoDangVienFolder();
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "HoSoDangVien", newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file hồ sơ đảng viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Mở file từ đường dẫn tương đối
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối (ví dụ: Server\HoSoDangVien\file.pdf)</param>
        /// <returns>True nếu mở thành công, False nếu có lỗi</returns>
        public static bool OpenFile(string relativePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    return false;
                }

                string fullPath = GetFullPath(relativePath);

                if (File.Exists(fullPath))
                {
                    System.Diagnostics.Process.Start(fullPath);
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"File không tồn tại: {fullPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi mở file {relativePath}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa file đính kèm từ đường dẫn tương đối
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối (ví dụ: Server\KhenThuong\file.pdf hoặc Server\KyLuat\file.pdf)</param>
        /// <returns>True nếu xóa thành công hoặc file không tồn tại, False nếu có lỗi</returns>
        public static bool DeleteFile(string relativePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    return true; // Không có file để xóa, coi như thành công
                }

                string fullPath = GetFullPath(relativePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }

                // File không tồn tại, coi như đã xóa thành công
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không throw exception để không ảnh hưởng đến việc xóa record
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa file {relativePath}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục Server
        /// </summary>
        public static string GetServerFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server");
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Tài liệu
        /// </summary>
        public static string GetTaiLieuFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "TaiLieu");
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ đến thư mục lưu file Văn bản chi bộ
        /// </summary>
        public static string GetVanBanChiBoFolder()
        {
            string serverBase = GetServerBaseFolder();
            return Path.Combine(serverBase, "Server", "VanBanChiBo");
        }

        /// <summary>
        /// Lưu file tài liệu vào thư mục
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <param name="subFolder">Thư mục con (ví dụ: "VanBan", "TaiLieuHuongDan")</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\TaiLieu\VanBan\filename.pdf)</returns>
        public static string SaveTaiLieuFile(string sourceFilePath, string subFolder = "")
        {
            try
            {
                string folder = GetTaiLieuFolder();
                
                // Thêm thư mục con nếu có
                if (!string.IsNullOrWhiteSpace(subFolder))
                {
                    folder = Path.Combine(folder, subFolder);
                }
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                if (!string.IsNullOrWhiteSpace(subFolder))
                {
                    return Path.Combine("Server", "TaiLieu", subFolder, newFileName);
                }
                else
                {
                    return Path.Combine("Server", "TaiLieu", newFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file tài liệu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu file văn bản chi bộ vào thư mục
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn file nguồn</param>
        /// <param name="donViID">ID đơn vị (tạo thư mục con theo DonViID)</param>
        /// <returns>Đường dẫn tương đối từ Server (ví dụ: Server\VanBanChiBo\1\filename.pdf)</returns>
        public static string SaveVanBanChiBoFile(string sourceFilePath, int donViID)
        {
            try
            {
                string folder = GetVanBanChiBoFolder();
                
                // Thêm thư mục con theo DonViID
                folder = Path.Combine(folder, donViID.ToString());
                
                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Tạo tên file mới với timestamp
                string fileName = Path.GetFileName(sourceFilePath);
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(folder, newFileName);

                // Copy file
                File.Copy(sourceFilePath, destinationPath, true);

                // Trả về đường dẫn tương đối
                return Path.Combine("Server", "VanBanChiBo", donViID.ToString(), newFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file văn bản chi bộ: {ex.Message}", ex);
            }
        }
    }
}

