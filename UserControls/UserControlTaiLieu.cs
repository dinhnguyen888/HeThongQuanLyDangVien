using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Services;
using QuanLyDangVien.Models;
using PdfiumViewer;

namespace QuanLyDangVien.UserControls
{
    public partial class UserControlTaiLieu : UserControl
    {
        private PdfViewer _pdfViewer; // PdfiumViewer control
        private FileInfo _currentSelectedFile = null; // File hiện tại được chọn
        private string _currentSelectedFolderPath = ""; // Đường dẫn thư mục được chọn
        private TreeNode _currentSelectedNode = null; // Node hiện tại được chọn
        
        // Danh sách các folder cố định
        private readonly string[] _defaultFolders = new string[]
        {
            "Tuyên huấn",
            "Tổ chức",
            "Cán bộ",
            "Quần chúng",
            "BVAN-DV",
            "Tổng hợp"
        };

        public UserControlTaiLieu()
        {
            InitializeComponent();
            ApplyPermissions();
        }

        private void UserControlTaiLieu_Load(object sender, EventArgs e)
        {
            InitializeFolders();
            LoadTreeView();
            InitializePdfViewer();
        }

        /// <summary>
        /// Khởi tạo PdfiumViewer control
        /// </summary>
        private void InitializePdfViewer()
        {
            _pdfViewer = new PdfViewer();
            _pdfViewer.Dock = DockStyle.Fill;
            _pdfViewer.Margin = new Padding(0);
            panelPdfViewer.Controls.Add(_pdfViewer);
            _pdfViewer.Visible = false;
            _pdfViewer.BringToFront();
        }

        /// <summary>
        /// Khởi tạo folders: Tạo các folder cố định nếu chưa có
        /// </summary>
        private void InitializeFolders()
        {
            try
            {
                // Lấy đường dẫn thư mục TaiLieu
                string taiLieuFolder = FileHelper.GetTaiLieuFolder();
                
                // Tạo thư mục TaiLieu nếu chưa có
                if (!Directory.Exists(taiLieuFolder))
                {
                    Directory.CreateDirectory(taiLieuFolder);
                }

                // Tạo các folder cố định nếu chưa có
                foreach (string folderName in _defaultFolders)
                {
                    string folderPath = Path.Combine(taiLieuFolder, folderName);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo folders: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load TreeView với folder TaiLieu, các folder cố định và files
        /// </summary>
        private void LoadTreeView()
        {
            try
            {
                treeViewTaiLieu.Nodes.Clear();
                
                string taiLieuFolder = FileHelper.GetTaiLieuFolder();
                
                if (!Directory.Exists(taiLieuFolder))
                {
                    Directory.CreateDirectory(taiLieuFolder);
                }

                // Tạo node gốc "Tài liệu"
                TreeNode rootNode = new TreeNode("Tài liệu")
                {
                    Tag = new Dictionary<string, object> { { "Path", taiLieuFolder }, { "Type", "Root" } },
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                // Load các folder cố định
                foreach (string folderName in _defaultFolders)
                {
                    string folderPath = Path.Combine(taiLieuFolder, folderName);
                    
                    // Đảm bảo folder tồn tại
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Tạo dictionary để lưu thông tin
                    Dictionary<string, object> tagData = new Dictionary<string, object>
                    {
                        { "Path", folderPath },
                        { "Type", "Folder" },
                        { "IsDefault", true }
                    };

                    TreeNode folderNode = new TreeNode(folderName)
                    {
                        Tag = tagData,
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };
                    
                    // Load files và subfolders trong thư mục
                    LoadFolderContent(folderNode, folderPath);
                    
                    rootNode.Nodes.Add(folderNode);
                }
                
                // Load các folder tùy chỉnh (không phải folder cố định)
                LoadCustomFolders(rootNode, taiLieuFolder);

                treeViewTaiLieu.Nodes.Add(rootNode);
                rootNode.Expand();
                
                // Đăng ký sự kiện
                treeViewTaiLieu.AfterSelect += TreeViewTaiLieu_AfterSelect;
                treeViewTaiLieu.NodeMouseDoubleClick += TreeViewTaiLieu_NodeMouseDoubleClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load TreeView: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Load các folder tùy chỉnh (không phải folder cố định)
        /// </summary>
        private void LoadCustomFolders(TreeNode rootNode, string taiLieuFolder)
        {
            try
            {
                if (!Directory.Exists(taiLieuFolder))
                    return;

                DirectoryInfo dirInfo = new DirectoryInfo(taiLieuFolder);
                DirectoryInfo[] directories = dirInfo.GetDirectories();

                foreach (DirectoryInfo dir in directories)
                {
                    // Bỏ qua các folder cố định
                    if (_defaultFolders.Contains(dir.Name))
                        continue;

                    Dictionary<string, object> tagData = new Dictionary<string, object>
                    {
                        { "Path", dir.FullName },
                        { "Type", "Folder" },
                        { "IsDefault", false }
                    };

                    TreeNode folderNode = new TreeNode(dir.Name)
                    {
                        Tag = tagData,
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };
                    
                    // Load files và subfolders trong thư mục
                    LoadFolderContent(folderNode, dir.FullName);
                    
                    rootNode.Nodes.Add(folderNode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load custom folders: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Load nội dung của folder (files và subfolders)
        /// </summary>
        private void LoadFolderContent(TreeNode parentNode, string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    return;

                DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
                
                // Load subfolders
                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    Dictionary<string, object> tagData = new Dictionary<string, object>
                    {
                        { "Path", subDir.FullName },
                        { "Type", "Folder" },
                        { "IsDefault", false }
                    };

                    TreeNode subFolderNode = new TreeNode(subDir.Name)
                    {
                        Tag = tagData,
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };
                    
                    // Recursive load subfolder content
                    LoadFolderContent(subFolderNode, subDir.FullName);
                    
                    parentNode.Nodes.Add(subFolderNode);
                }
                
                // Load files
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    Dictionary<string, object> fileTag = new Dictionary<string, object>
                    {
                        { "Path", file.FullName },
                        { "Type", "File" },
                        { "FileInfo", file }
                    };

                    TreeNode fileNode = new TreeNode(file.Name)
                    {
                        Tag = fileTag,
                        ImageIndex = 2,
                        SelectedImageIndex = 2
                    };

                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load folder content: {ex.Message}");
            }
        }


        /// <summary>
        /// Xử lý khi chọn node trong TreeView
        /// </summary>
        private void TreeViewTaiLieu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = e.Node;
                _currentSelectedNode = selectedNode;
                
                if (selectedNode == null || selectedNode.Tag == null)
                {
                    ClearPreview();
                    _currentSelectedFolderPath = "";
                    _currentSelectedFile = null;
                    return;
                }

                // Kiểm tra xem có phải là node file không
                if (selectedNode.Tag is Dictionary<string, object> tagData)
                {
                    string nodeType = tagData.ContainsKey("Type") ? tagData["Type"].ToString() : "";
                    
                    if (nodeType == "File")
                    {
                        // Node file: Preview file
                        if (tagData.ContainsKey("FileInfo") && tagData["FileInfo"] is FileInfo fileInfo)
                        {
                            _currentSelectedFile = fileInfo;
                            _currentSelectedFolderPath = fileInfo.DirectoryName;
                            PreviewFile(fileInfo);
                        }
                    }
                    else if (nodeType == "Folder" || nodeType == "Root")
                    {
                        // Node folder: Clear preview, lưu đường dẫn folder
                        _currentSelectedFolderPath = tagData.ContainsKey("Path") ? tagData["Path"].ToString() : "";
                        _currentSelectedFile = null;
                        ClearPreview();
                    }
                }
                else
                {
                    ClearPreview();
                    _currentSelectedFolderPath = "";
                    _currentSelectedFile = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn node: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý khi double-click node trong TreeView
        /// </summary>
        private void TreeViewTaiLieu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node?.Tag is Dictionary<string, object> tagData)
                {
                    string nodeType = tagData.ContainsKey("Type") ? tagData["Type"].ToString() : "";
                    
                    if (nodeType == "File")
                    {
                        // Mở file bằng ứng dụng mặc định
                        if (tagData.ContainsKey("Path"))
                        {
                            string filePath = tagData["Path"].ToString();
                            if (File.Exists(filePath))
                            {
                                System.Diagnostics.Process.Start(filePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Preview file (chỉ hỗ trợ PDF)
        /// </summary>
        private void PreviewFile(FileInfo fileInfo)
        {
            try
            {
                if (fileInfo == null || !fileInfo.Exists)
                {
                    ClearPreview();
                    return;
                }

                string extension = fileInfo.Extension.ToLower();
                
                if (extension == ".pdf")
                {
                    // Preview PDF bằng PdfiumViewer
                    try
                    {
                        _pdfViewer.Document?.Dispose();
                        _pdfViewer.Document = PdfDocument.Load(fileInfo.FullName);
                        _pdfViewer.Visible = true;
                        lblPreviewMessage.Visible = false;
                        lblPreview.Text = $"Xem trước: {fileInfo.Name}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể load PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowUnsupportedFileMessage(fileInfo);
                    }
                }
                else
                {
                    // File khác: Hiển thị thông báo
                    ShowUnsupportedFileMessage(fileInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi preview file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị thông báo cho file không hỗ trợ preview
        /// </summary>
        private void ShowUnsupportedFileMessage(FileInfo fileInfo)
        {
            _pdfViewer.Visible = false;
            lblPreviewMessage.Visible = true;
            lblPreviewMessage.Text = $"Không hỗ trợ xem trực tiếp file {fileInfo.Extension.ToUpper()}. Vui lòng double-click vào file để mở bằng ứng dụng mặc định.";
            lblPreview.Text = $"File: {fileInfo.Name}";
        }

        /// <summary>
        /// Clear preview
        /// </summary>
        private void ClearPreview()
        {
            try
            {
                _pdfViewer.Visible = false;
                _pdfViewer.Document?.Dispose();
                _pdfViewer.Document = null;
                lblPreviewMessage.Visible = true;
                lblPreviewMessage.Text = "Chọn file để xem trước";
                lblPreview.Text = "Xem trước";
                _currentSelectedFile = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi clear preview: {ex.Message}");
            }
        }

        /// <summary>
        /// Nút Thêm - Có thể thêm file hoặc tạo folder mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Kiểm tra xem đã chọn folder chưa
                if (string.IsNullOrEmpty(_currentSelectedFolderPath))
                {
                    // Nếu chưa chọn folder, sử dụng thư mục gốc
                    _currentSelectedFolderPath = FileHelper.GetTaiLieuFolder();
                }
                
                // Hiển thị menu để chọn: Thêm file hoặc Tạo folder
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem menuItemFile = new ToolStripMenuItem("Thêm file");
                ToolStripMenuItem menuItemFolder = new ToolStripMenuItem("Tạo thư mục");
                
                menuItemFile.Click += (s, args) => AddFile();
                menuItemFolder.Click += (s, args) => CreateFolder();
                
                menu.Items.Add(menuItemFile);
                menu.Items.Add(menuItemFolder);
                
                menu.Show(btnThem, new Point(0, btnThem.Height));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Thêm file vào folder đã chọn
        /// </summary>
        private void AddFile()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentSelectedFolderPath))
                {
                    _currentSelectedFolderPath = FileHelper.GetTaiLieuFolder();
                }

                // Mở dialog chọn file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Tất cả files|*.*|PDF files|*.pdf|Word files|*.docx;*.doc";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string sourceFilePath = openFileDialog.FileName;
                        string fileName = Path.GetFileName(sourceFilePath);
                        string destinationPath = Path.Combine(_currentSelectedFolderPath, fileName);

                        // Copy file vào thư mục
                        File.Copy(sourceFilePath, destinationPath, true);

                        // Refresh TreeView
                        LoadTreeView();

                        MessageBox.Show("Thêm tài liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm tài liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Tạo thư mục mới
        /// </summary>
        private void CreateFolder()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentSelectedFolderPath))
                {
                    _currentSelectedFolderPath = FileHelper.GetTaiLieuFolder();
                }
                
                // Hiển thị dialog nhập tên folder
                string folderName = ShowInputDialog("Nhập tên thư mục mới:", "Tạo thư mục");
                
                if (string.IsNullOrWhiteSpace(folderName))
                    return;
                
                // Kiểm tra tên folder hợp lệ
                char[] invalidChars = Path.GetInvalidFileNameChars();
                if (folderName.IndexOfAny(invalidChars) >= 0)
                {
                    MessageBox.Show("Tên thư mục chứa ký tự không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                string newFolderPath = Path.Combine(_currentSelectedFolderPath, folderName);
                
                // Kiểm tra folder đã tồn tại chưa
                if (Directory.Exists(newFolderPath))
                {
                    MessageBox.Show("Thư mục đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Tạo folder
                Directory.CreateDirectory(newFolderPath);
                
                // Refresh TreeView
                LoadTreeView();
                
                MessageBox.Show("Tạo thư mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo thư mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Sửa - Có thể sửa file (mở file) hoặc đổi tên folder
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Update"))
            {
                MessageBox.Show("Bạn không có quyền sửa tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                if (_currentSelectedNode == null || _currentSelectedNode.Tag == null)
                {
                    MessageBox.Show("Vui lòng chọn file hoặc thư mục cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (_currentSelectedNode.Tag is Dictionary<string, object> tagData)
                {
                    string nodeType = tagData.ContainsKey("Type") ? tagData["Type"].ToString() : "";
                    
                    if (nodeType == "File")
                    {
                        // Mở file bằng ứng dụng mặc định
                        if (_currentSelectedFile != null && _currentSelectedFile.Exists)
                        {
                            System.Diagnostics.Process.Start(_currentSelectedFile.FullName);
                        }
                    }
                    else if (nodeType == "Folder")
                    {
                        // Đổi tên folder
                        bool isDefault = tagData.ContainsKey("IsDefault") && (bool)tagData["IsDefault"];
                        if (isDefault)
                        {
                            MessageBox.Show("Không thể đổi tên thư mục cố định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        string currentPath = tagData.ContainsKey("Path") ? tagData["Path"].ToString() : "";
                        if (string.IsNullOrEmpty(currentPath))
                            return;
                        
                        string currentName = Path.GetFileName(currentPath);
                        string newName = ShowInputDialog("Nhập tên mới cho thư mục:", "Đổi tên thư mục", currentName);
                        
                        if (string.IsNullOrWhiteSpace(newName) || newName == currentName)
                            return;
                        
                        // Kiểm tra tên folder hợp lệ
                        char[] invalidChars = Path.GetInvalidFileNameChars();
                        if (newName.IndexOfAny(invalidChars) >= 0)
                        {
                            MessageBox.Show("Tên thư mục chứa ký tự không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        string parentPath = Path.GetDirectoryName(currentPath);
                        string newPath = Path.Combine(parentPath, newName);
                        
                        // Kiểm tra folder đã tồn tại chưa
                        if (Directory.Exists(newPath))
                        {
                            MessageBox.Show("Thư mục đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        // Đổi tên folder
                        Directory.Move(currentPath, newPath);
                        
                        // Refresh TreeView
                        LoadTreeView();
                        
                        MessageBox.Show("Đổi tên thư mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Xóa - Có thể xóa file hoặc folder
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Delete"))
            {
                MessageBox.Show("Bạn không có quyền xóa tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                if (_currentSelectedNode == null || _currentSelectedNode.Tag == null)
                {
                    MessageBox.Show("Vui lòng chọn file hoặc thư mục cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (_currentSelectedNode.Tag is Dictionary<string, object> tagData)
                {
                    string nodeType = tagData.ContainsKey("Type") ? tagData["Type"].ToString() : "";
                    
                    if (nodeType == "File")
                    {
                        // Xóa file
                        if (_currentSelectedFile == null || !_currentSelectedFile.Exists)
                        {
                            MessageBox.Show("File không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        DialogResult result = MessageBox.Show(
                            $"Bạn có chắc chắn muốn xóa file '{_currentSelectedFile.Name}'?",
                            "Xác nhận xóa",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            File.Delete(_currentSelectedFile.FullName);
                            ClearPreview();
                            LoadTreeView();
                            MessageBox.Show("Xóa file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (nodeType == "Folder")
                    {
                        // Xóa folder
                        bool isDefault = tagData.ContainsKey("IsDefault") && (bool)tagData["IsDefault"];
                        if (isDefault)
                        {
                            MessageBox.Show("Không thể xóa thư mục cố định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        string folderPath = tagData.ContainsKey("Path") ? tagData["Path"].ToString() : "";
                        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                        {
                            MessageBox.Show("Thư mục không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        string folderName = Path.GetFileName(folderPath);
                        
                        // Kiểm tra folder có chứa file không
                        DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
                        bool hasContent = dirInfo.GetFiles().Length > 0 || dirInfo.GetDirectories().Length > 0;
                        
                        string message = hasContent 
                            ? $"Bạn có chắc chắn muốn xóa thư mục '{folderName}' và tất cả nội dung bên trong?"
                            : $"Bạn có chắc chắn muốn xóa thư mục '{folderName}'?";
                        
                        DialogResult result = MessageBox.Show(
                            message,
                            "Xác nhận xóa",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            Directory.Delete(folderPath, true);
                            ClearPreview();
                            LoadTreeView();
                            MessageBox.Show("Xóa thư mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Làm mới
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh không cần quyền đặc biệt
            try
            {
                InitializeFolders();
                LoadTreeView();
                ClearPreview();
                _currentSelectedNode = null;
                _currentSelectedFolderPath = "";
                _currentSelectedFile = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị dialog nhập liệu đơn giản
        /// </summary>
        private string ShowInputDialog(string prompt, string title, string defaultValue = "")
        {
            Form inputForm = new Form();
            inputForm.Width = 400;
            inputForm.Height = 150;
            inputForm.Text = title;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;

            Label promptLabel = new Label() { Left = 20, Top = 20, Width = 350, Text = prompt };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350, Text = defaultValue };
            Button okButton = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Hủy", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

            okButton.Click += (sender, e) => { inputForm.Close(); };
            cancelButton.Click += (sender, e) => { inputForm.Close(); };

            inputForm.Controls.Add(promptLabel);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(okButton);
            inputForm.Controls.Add(cancelButton);
            inputForm.AcceptButton = okButton;
            inputForm.CancelButton = cancelButton;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                return textBox.Text;
            }
            return null;
        }

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canCreate = AuthorizationHelper.HasPermission("TaiLieu", "Create");
            bool canUpdate = AuthorizationHelper.HasPermission("TaiLieu", "Update");
            bool canDelete = AuthorizationHelper.HasPermission("TaiLieu", "Delete");

            if (btnThem != null) btnThem.Enabled = canCreate;
            if (btnSua != null) btnSua.Enabled = canUpdate;
            if (btnXoa != null) btnXoa.Enabled = canDelete;
        }
    }
}
