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
        private DonViService _donViService;
        private string _currentSelectedDonViPath = ""; // Đường dẫn thư mục chi bộ được chọn
        private int? _currentSelectedDonViID = null; // ID chi bộ được chọn
        private PdfViewer _pdfViewer; // PdfiumViewer control
        private FileInfo _currentSelectedFile = null; // File hiện tại được chọn

        public UserControlTaiLieu()
        {
            InitializeComponent();
            _donViService = new DonViService();
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
        /// Khởi tạo folders: Fetch chi bộ từ database và tạo folder nếu chưa có
        /// </summary>
        private void InitializeFolders()
        {
            try
            {
                // Lấy danh sách chi bộ từ database
                var donViList = _donViService.GetAll();
                
                // Lấy đường dẫn thư mục TaiLieu
                string taiLieuFolder = FileHelper.GetTaiLieuFolder();
                
                // Tạo thư mục TaiLieu nếu chưa có
                if (!Directory.Exists(taiLieuFolder))
                {
                    Directory.CreateDirectory(taiLieuFolder);
                }

                // Tạo folder cho từng chi bộ nếu chưa có
                foreach (var donVi in donViList)
                {
                    if (donVi != null && !string.IsNullOrWhiteSpace(donVi.TenDonVi))
                    {
                        string donViFolder = Path.Combine(taiLieuFolder, donVi.TenDonVi);
                        if (!Directory.Exists(donViFolder))
                        {
                            Directory.CreateDirectory(donViFolder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo folders: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load TreeView với folder TaiLieu, các chi bộ và files
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
                    Tag = taiLieuFolder,
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                // Lấy danh sách chi bộ từ database
                var donViList = _donViService.GetAll();
                
                // Thêm các node chi bộ và files
                foreach (var donVi in donViList)
                {
                    if (donVi != null && !string.IsNullOrWhiteSpace(donVi.TenDonVi))
                    {
                        string donViFolder = Path.Combine(taiLieuFolder, donVi.TenDonVi);
                        
                        // Đảm bảo folder tồn tại
                        if (!Directory.Exists(donViFolder))
                        {
                            Directory.CreateDirectory(donViFolder);
                        }

                        // Tạo dictionary để lưu thông tin
                        Dictionary<string, object> tagData = new Dictionary<string, object>
                        {
                            { "Path", donViFolder },
                            { "DonViID", donVi.DonViID },
                            { "Type", "DonVi" }
                        };

                        TreeNode donViNode = new TreeNode(donVi.TenDonVi)
                        {
                            Tag = tagData,
                            ImageIndex = 1,
                            SelectedImageIndex = 1
                        };
                        
                        // Load files trong thư mục chi bộ
                        LoadFilesIntoNode(donViNode, donViFolder);
                        
                        rootNode.Nodes.Add(donViNode);
                    }
                }

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
        /// Load files vào node chi bộ
        /// </summary>
        private void LoadFilesIntoNode(TreeNode parentNode, string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    return;

                DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
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
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load files vào node: {ex.Message}");
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
                
                if (selectedNode == null || selectedNode.Tag == null)
                {
                    ClearPreview();
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
                            PreviewFile(fileInfo);
                        }
                    }
                    else if (nodeType == "DonVi")
                    {
                        // Node chi bộ: Clear preview
                        _currentSelectedDonViPath = tagData.ContainsKey("Path") ? tagData["Path"].ToString() : "";
                        _currentSelectedDonViID = tagData.ContainsKey("DonViID") ? (int?)tagData["DonViID"] : null;
                        _currentSelectedFile = null;
                        ClearPreview();
                    }
                }
                else
                {
                    ClearPreview();
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
        /// Nút Thêm
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Create", _currentSelectedDonViID))
            {
                MessageBox.Show("Bạn không có quyền thêm tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_currentSelectedDonViID == null)
                {
                    MessageBox.Show("Vui lòng chọn chi bộ trước khi thêm tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                        string destinationPath = Path.Combine(_currentSelectedDonViPath, fileName);

                        // Copy file vào thư mục chi bộ
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
        /// Nút Sửa
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Update", _currentSelectedDonViID))
            {
                MessageBox.Show("Bạn không có quyền sửa tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_currentSelectedFile == null)
                {
                    MessageBox.Show("Vui lòng chọn file cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở file bằng ứng dụng mặc định
                System.Diagnostics.Process.Start(_currentSelectedFile.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Xóa
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("TaiLieu", "Delete", _currentSelectedDonViID))
            {
                MessageBox.Show("Bạn không có quyền xóa tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_currentSelectedFile == null)
                {
                    MessageBox.Show("Vui lòng chọn file cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
