using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Services;
using QuanLyDangVien.Models;

namespace QuanLyDangVien.UserControls
{
    public partial class UserControlTaiLieu : UserControl
    {
        private TaiLieuService _taiLieuService;
        private VanBanChiBoService _vanBanChiBoService;
        private DonViService _donViService;
        private string _currentSelectedPath = ""; // Đường dẫn thư mục được chọn trong TreeView
        private string _currentType = "TaiLieu"; // "TaiLieu" hoặc "VanBanChiBo"

        public UserControlTaiLieu()
        {
            InitializeComponent();
            _taiLieuService = new TaiLieuService();
            _vanBanChiBoService = new VanBanChiBoService();
            _donViService = new DonViService();
        }

        private void UserControlTaiLieu_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            SetupDataGridView();
            LoadData();
        }

        /// <summary>
        /// Load cấu trúc thư mục Server vào TreeView
        /// </summary>
        private void LoadTreeView()
        {
            try
            {
                treeViewTaiLieu.Nodes.Clear();
                
                string serverFolder = FileHelper.GetServerFolder();
                
                if (!Directory.Exists(serverFolder))
                {
                    Directory.CreateDirectory(serverFolder);
                }

                // Tạo node gốc "Server"
                TreeNode rootNode = new TreeNode("Server")
                {
                    Tag = serverFolder,
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                // Load các thư mục con
                LoadDirectoryNodes(rootNode, serverFolder);

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
        /// Load các node thư mục con (đệ quy)
        /// </summary>
        private void LoadDirectoryNodes(TreeNode parentNode, string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                    return;

                // Chỉ load thư mục, không load file
                string[] directories = Directory.GetDirectories(directoryPath);
                
                foreach (string dir in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    TreeNode childNode = new TreeNode(dirInfo.Name)
                    {
                        Tag = dir,
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };

                    // Load các thư mục con (đệ quy)
                    LoadDirectoryNodes(childNode, dir);

                    parentNode.Nodes.Add(childNode);
                }

                // Load các file trong thư mục
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TreeNode fileNode = new TreeNode(fileInfo.Name)
                    {
                        Tag = file,
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };

                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load thư mục {directoryPath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn node trong TreeView
        /// </summary>
        private void TreeViewTaiLieu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == null || e.Node.Tag == null)
                {
                    dataGridViewTaiLieu.DataSource = null;
                    return;
                }

                string selectedPath = e.Node.Tag.ToString();

                // Kiểm tra nếu là file thì không làm gì (file sẽ được mở khi double-click)
                if (File.Exists(selectedPath))
                {
                    return;
                }

                // Xác định loại dữ liệu dựa trên đường dẫn thư mục
                string serverFolder = FileHelper.GetServerFolder();
                string taiLieuFolder = FileHelper.GetTaiLieuFolder();
                string vanBanChiBoFolder = FileHelper.GetVanBanChiBoFolder();
                
                // Kiểm tra nếu chọn thư mục "TaiLieu" hoặc thư mục con trong "TaiLieu"
                if (selectedPath.StartsWith(taiLieuFolder) || selectedPath == taiLieuFolder)
                {
                    _currentType = "TaiLieu";
                    _currentSelectedPath = selectedPath;
                    LoadTaiLieuData();
                }
                // Kiểm tra nếu chọn thư mục "VanBanChiBo" hoặc thư mục con trong "VanBanChiBo"
                else if (selectedPath.StartsWith(vanBanChiBoFolder) || selectedPath == vanBanChiBoFolder)
                {
                    _currentType = "VanBanChiBo";
                    _currentSelectedPath = selectedPath;
                    LoadVanBanChiBoData();
                }
                // Nếu chọn node "Server" (root) hoặc thư mục khác
                else
                {
                    // Không load dữ liệu, chỉ hiển thị cấu trúc thư mục
                    dataGridViewTaiLieu.DataSource = null;
                    _currentType = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn node: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện double-click vào node (mở file)
        /// </summary>
        private void TreeViewTaiLieu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node == null || e.Node.Tag == null)
                    return;

                string selectedPath = e.Node.Tag.ToString();

                // Nếu là file, mở file
                if (File.Exists(selectedPath))
                {
                    // Lấy đường dẫn tương đối từ thư mục Server base
                    string serverBase = FileHelper.GetServerFolder();
                    string serverBaseParent = Path.GetDirectoryName(serverBase); // C:\QuanLyDangVien
                    
                    if (selectedPath.StartsWith(serverBaseParent))
                    {
                        string relativePath = selectedPath.Substring(serverBaseParent.Length + 1);
                        FileHelper.OpenFile(relativePath);
                    }
                    else
                    {
                        // Nếu không thể tạo đường dẫn tương đối, mở file trực tiếp
                        System.Diagnostics.Process.Start(selectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Setup DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            dataGridViewTaiLieu.AutoGenerateColumns = false;
            dataGridViewTaiLieu.AllowUserToAddRows = false;
            dataGridViewTaiLieu.AllowUserToDeleteRows = false;
            dataGridViewTaiLieu.ReadOnly = true;
            dataGridViewTaiLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTaiLieu.MultiSelect = false;
            dataGridViewTaiLieu.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewTaiLieu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewTaiLieu.RowTemplate.Height = 30;
            
            // Đăng ký sự kiện
            dataGridViewTaiLieu.CellDoubleClick += DataGridViewTaiLieu_CellDoubleClick;
        }

        /// <summary>
        /// Load dữ liệu Tài liệu
        /// </summary>
        private void LoadTaiLieuData()
        {
            try
            {
                var taiLieuList = _taiLieuService.GetAll();
                
                // Tạo DataTable để hiển thị
                DataTable dt = new DataTable();
                dt.Columns.Add("TaiLieuID", typeof(int));
                dt.Columns.Add("TieuDe", typeof(string));
                dt.Columns.Add("LoaiTaiLieu", typeof(string));
                dt.Columns.Add("TenDonVi", typeof(string));
                dt.Columns.Add("NgayPhatHanh", typeof(DateTime));
                dt.Columns.Add("CoQuanPhatHanh", typeof(string));
                dt.Columns.Add("FileDinhKem", typeof(string));

                foreach (var taiLieu in taiLieuList)
                {
                    dt.Rows.Add(
                        taiLieu.TaiLieuID,
                        taiLieu.TieuDe,
                        taiLieu.LoaiTaiLieu,
                        taiLieu.TenDonVi ?? "",
                        taiLieu.NgayPhatHanh,
                        taiLieu.CoQuanPhatHanh ?? "",
                        taiLieu.FileDinhKem ?? ""
                    );
                }

                dataGridViewTaiLieu.DataSource = dt;
                
                // Setup columns
                dataGridViewTaiLieu.Columns.Clear();
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TaiLieuID",
                    HeaderText = "ID",
                    Width = 60,
                    Visible = false
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TieuDe",
                    HeaderText = "Tiêu đề",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "LoaiTaiLieu",
                    HeaderText = "Loại",
                    Width = 120
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenDonVi",
                    HeaderText = "Đơn vị",
                    Width = 150
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayPhatHanh",
                    HeaderText = "Ngày phát hành",
                    Width = 130
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CoQuanPhatHanh",
                    HeaderText = "Cơ quan phát hành",
                    Width = 150
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu tài liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load dữ liệu Văn bản chi bộ
        /// </summary>
        private void LoadVanBanChiBoData()
        {
            try
            {
                var vanBanList = _vanBanChiBoService.GetAll();
                
                // Tạo DataTable để hiển thị
                DataTable dt = new DataTable();
                dt.Columns.Add("VanBanChiBoID", typeof(int));
                dt.Columns.Add("TenVanBan", typeof(string));
                dt.Columns.Add("TenDonVi", typeof(string));
                dt.Columns.Add("NgayGui", typeof(DateTime));
                dt.Columns.Add("NgayNhan", typeof(DateTime));
                dt.Columns.Add("TrangThai", typeof(string));
                dt.Columns.Add("FileDinhKem", typeof(string));

                foreach (var vanBan in vanBanList)
                {
                    dt.Rows.Add(
                        vanBan.VanBanChiBoID,
                        vanBan.TenVanBan,
                        vanBan.TenDonVi ?? "",
                        vanBan.NgayGui,
                        vanBan.NgayNhan,
                        vanBan.TrangThai ?? "",
                        vanBan.FileDinhKem ?? ""
                    );
                }

                dataGridViewTaiLieu.DataSource = dt;
                
                // Setup columns
                dataGridViewTaiLieu.Columns.Clear();
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "VanBanChiBoID",
                    HeaderText = "ID",
                    Width = 60,
                    Visible = false
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenVanBan",
                    HeaderText = "Tên văn bản",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenDonVi",
                    HeaderText = "Đơn vị",
                    Width = 150
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayGui",
                    HeaderText = "Ngày gửi",
                    Width = 120
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayNhan",
                    HeaderText = "Ngày nhận",
                    Width = 120
                });
                dataGridViewTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TrangThai",
                    HeaderText = "Trạng thái",
                    Width = 120
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu văn bản chi bộ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load dữ liệu dựa trên loại hiện tại
        /// </summary>
        private void LoadData()
        {
            if (_currentType == "TaiLieu")
            {
                LoadTaiLieuData();
            }
            else if (_currentType == "VanBanChiBo")
            {
                LoadVanBanChiBoData();
            }
        }

        /// <summary>
        /// Xử lý double-click vào DataGridView (xem chi tiết)
        /// </summary>
        private void DataGridViewTaiLieu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                if (_currentType == "TaiLieu")
                {
                    int taiLieuID = Convert.ToInt32(dataGridViewTaiLieu.Rows[e.RowIndex].Cells["TaiLieuID"].Value);
                    var taiLieuDTO = _taiLieuService.GetById(taiLieuID);
                    if (taiLieuDTO != null)
                    {
                        var taiLieu = new TaiLieu
                        {
                            TaiLieuID = taiLieuDTO.TaiLieuID,
                            DonViID = taiLieuDTO.DonViID,
                            TieuDe = taiLieuDTO.TieuDe,
                            LoaiTaiLieu = taiLieuDTO.LoaiTaiLieu,
                            NoiDung = taiLieuDTO.NoiDung,
                            FileDinhKem = taiLieuDTO.FileDinhKem,
                            NgayPhatHanh = taiLieuDTO.NgayPhatHanh,
                            CoQuanPhatHanh = taiLieuDTO.CoQuanPhatHanh,
                            TrangThai = taiLieuDTO.TrangThai,
                            NgayTao = taiLieuDTO.NgayTao,
                            NguoiTao = taiLieuDTO.NguoiTao
                        };

                        FormXemChiTiet formXemChiTiet = new FormXemChiTiet(taiLieu);
                        BindDonViComboBoxes(formXemChiTiet);
                        formXemChiTiet.ShowDialog();
                    }
                }
                else if (_currentType == "VanBanChiBo")
                {
                    int vanBanID = Convert.ToInt32(dataGridViewTaiLieu.Rows[e.RowIndex].Cells["VanBanChiBoID"].Value);
                    var vanBanDTO = _vanBanChiBoService.GetById(vanBanID);
                    if (vanBanDTO != null)
                    {
                        var vanBan = new VanBanChiBo
                        {
                            VanBanChiBoID = vanBanDTO.VanBanChiBoID,
                            DonViID = vanBanDTO.DonViID,
                            TenVanBan = vanBanDTO.TenVanBan,
                            NoiDung = vanBanDTO.NoiDung,
                            NgayGui = vanBanDTO.NgayGui,
                            NgayNhan = vanBanDTO.NgayNhan,
                            TrangThai = vanBanDTO.TrangThai,
                            FileDinhKem = vanBanDTO.FileDinhKem,
                            NgayTao = vanBanDTO.NgayTao,
                            NguoiTao = vanBanDTO.NguoiTao
                        };

                        FormXemChiTiet formXemChiTiet = new FormXemChiTiet(vanBan);
                        BindDonViComboBoxes(formXemChiTiet);
                        formXemChiTiet.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bind đơn vị vào ComboBox trong form
        /// </summary>
        private void BindDonViComboBoxes(Form form)
        {
            try
            {
                var donViData = _donViService.GetDonViData();
                
                foreach (Control control in form.Controls)
                {
                    if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                    {
                        comboBox.DataSource = donViData;
                        comboBox.DisplayMember = "TenDonVi";
                        comboBox.ValueMember = "DonViID";
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    
                    if (control.HasChildren)
                    {
                        BindDonViComboBoxesRecursive(control, donViData);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi bind đơn vị: {ex.Message}");
            }
        }

        private void BindDonViComboBoxesRecursive(Control parent, List<DonViSimplified> donViData)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                {
                    comboBox.DataSource = donViData;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                
                if (control.HasChildren)
                {
                    BindDonViComboBoxesRecursive(control, donViData);
                }
            }
        }

        // Event handlers
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã chọn loại dữ liệu chưa
                if (string.IsNullOrEmpty(_currentType))
                {
                    MessageBox.Show("Vui lòng chọn thư mục TaiLieu hoặc VanBanChiBo trong TreeView!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentType == "TaiLieu")
                {
                    FormThem formThem = new FormThem(typeof(TaiLieu));
                    BindDonViComboBoxes(formThem);
                    
                    if (formThem.ShowDialog() == DialogResult.OK)
                    {
                        var taiLieu = formThem.GetData() as TaiLieu;
                        if (taiLieu != null)
                        {
                            // Xử lý file đính kèm
                            if (!string.IsNullOrWhiteSpace(taiLieu.FileDinhKem))
                            {
                                // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                                if (File.Exists(taiLieu.FileDinhKem))
                                {
                                    string subFolder = taiLieu.LoaiTaiLieu ?? ""; // Sử dụng LoaiTaiLieu làm thư mục con
                                    string relativePath = FileHelper.SaveTaiLieuFile(taiLieu.FileDinhKem, subFolder);
                                    taiLieu.FileDinhKem = relativePath;
                                }
                                // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                            }
                            
                            taiLieu.NguoiTao = Environment.UserName;
                            (int id, string error) = _taiLieuService.Insert(taiLieu);
                            
                            if (string.IsNullOrEmpty(error))
                            {
                                MessageBox.Show("Thêm tài liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadTreeView();
                                LoadTaiLieuData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else if (_currentType == "VanBanChiBo")
                {
                    FormThem formThem = new FormThem(typeof(VanBanChiBo));
                    BindDonViComboBoxes(formThem);
                    
                    if (formThem.ShowDialog() == DialogResult.OK)
                    {
                        var vanBan = formThem.GetData() as VanBanChiBo;
                        if (vanBan != null)
                        {
                            // Xử lý file đính kèm
                            if (!string.IsNullOrWhiteSpace(vanBan.FileDinhKem))
                            {
                                // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                                if (File.Exists(vanBan.FileDinhKem))
                                {
                                    string relativePath = FileHelper.SaveVanBanChiBoFile(vanBan.FileDinhKem, vanBan.DonViID);
                                    vanBan.FileDinhKem = relativePath;
                                }
                                // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                            }
                            
                            Services.VanBanChiBoModel vanBanModel = new Services.VanBanChiBoModel
                            {
                                DonViID = vanBan.DonViID,
                                TenVanBan = vanBan.TenVanBan,
                                NoiDung = vanBan.NoiDung,
                                NgayGui = vanBan.NgayGui,
                                NgayNhan = vanBan.NgayNhan,
                                TrangThai = vanBan.TrangThai ?? "Chưa xử lý",
                                FileDinhKem = vanBan.FileDinhKem,
                                NguoiTao = Environment.UserName
                            };
                            
                            (int id, string error) = _vanBanChiBoService.Insert(vanBanModel);
                            
                            if (string.IsNullOrEmpty(error))
                            {
                                MessageBox.Show("Thêm văn bản chi bộ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadTreeView();
                                LoadVanBanChiBoData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thư mục TaiLieu hoặc VanBanChiBo trong TreeView!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewTaiLieu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_currentType))
            {
                MessageBox.Show("Vui lòng chọn thư mục TaiLieu hoặc VanBanChiBo trong TreeView!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_currentType == "TaiLieu")
                {
                    int taiLieuID = Convert.ToInt32(dataGridViewTaiLieu.SelectedRows[0].Cells["TaiLieuID"].Value);
                    var taiLieuDTO = _taiLieuService.GetById(taiLieuID);
                    if (taiLieuDTO != null)
                    {
                        var taiLieu = new TaiLieu
                        {
                            TaiLieuID = taiLieuDTO.TaiLieuID,
                            DonViID = taiLieuDTO.DonViID,
                            TieuDe = taiLieuDTO.TieuDe,
                            LoaiTaiLieu = taiLieuDTO.LoaiTaiLieu,
                            NoiDung = taiLieuDTO.NoiDung,
                            FileDinhKem = taiLieuDTO.FileDinhKem,
                            NgayPhatHanh = taiLieuDTO.NgayPhatHanh,
                            CoQuanPhatHanh = taiLieuDTO.CoQuanPhatHanh,
                            TrangThai = taiLieuDTO.TrangThai,
                            NgayTao = taiLieuDTO.NgayTao,
                            NguoiTao = taiLieuDTO.NguoiTao
                        };

                        FormSua formSua = new FormSua(taiLieu);
                        BindDonViComboBoxes(formSua);
                        
                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedTaiLieu = formSua.GetData() as TaiLieu;
                            if (updatedTaiLieu != null)
                            {
                                // Xử lý file đính kèm nếu có thay đổi
                                if (!string.IsNullOrWhiteSpace(updatedTaiLieu.FileDinhKem))
                                {
                                    // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                                    if (File.Exists(updatedTaiLieu.FileDinhKem))
                                    {
                                        // Xóa file cũ nếu có
                                        if (!string.IsNullOrWhiteSpace(taiLieu.FileDinhKem))
                                        {
                                            FileHelper.DeleteFile(taiLieu.FileDinhKem);
                                        }
                                        
                                        string subFolder = updatedTaiLieu.LoaiTaiLieu ?? "";
                                        string relativePath = FileHelper.SaveTaiLieuFile(updatedTaiLieu.FileDinhKem, subFolder);
                                        updatedTaiLieu.FileDinhKem = relativePath;
                                    }
                                    // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                                }
                                
                                (bool success, string error) = _taiLieuService.Update(updatedTaiLieu);
                                
                                if (success)
                                {
                                    MessageBox.Show("Cập nhật tài liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadTreeView();
                                    LoadTaiLieuData();
                                }
                                else
                                {
                                    MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else if (_currentType == "VanBanChiBo")
                {
                    int vanBanID = Convert.ToInt32(dataGridViewTaiLieu.SelectedRows[0].Cells["VanBanChiBoID"].Value);
                    var vanBanDTO = _vanBanChiBoService.GetById(vanBanID);
                    if (vanBanDTO != null)
                    {
                        var vanBan = new VanBanChiBo
                        {
                            VanBanChiBoID = vanBanDTO.VanBanChiBoID,
                            DonViID = vanBanDTO.DonViID,
                            TenVanBan = vanBanDTO.TenVanBan,
                            NoiDung = vanBanDTO.NoiDung,
                            NgayGui = vanBanDTO.NgayGui,
                            NgayNhan = vanBanDTO.NgayNhan,
                            TrangThai = vanBanDTO.TrangThai,
                            FileDinhKem = vanBanDTO.FileDinhKem,
                            NgayTao = vanBanDTO.NgayTao,
                            NguoiTao = vanBanDTO.NguoiTao
                        };

                        FormSua formSua = new FormSua(vanBan);
                        BindDonViComboBoxes(formSua);
                        
                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedVanBan = formSua.GetData() as VanBanChiBo;
                            if (updatedVanBan != null)
                            {
                                // Xử lý file đính kèm nếu có thay đổi
                                if (!string.IsNullOrWhiteSpace(updatedVanBan.FileDinhKem))
                                {
                                    // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                                    if (File.Exists(updatedVanBan.FileDinhKem))
                                    {
                                        // Xóa file cũ nếu có
                                        if (!string.IsNullOrWhiteSpace(vanBan.FileDinhKem))
                                        {
                                            FileHelper.DeleteFile(vanBan.FileDinhKem);
                                        }
                                        
                                        string relativePath = FileHelper.SaveVanBanChiBoFile(updatedVanBan.FileDinhKem, updatedVanBan.DonViID);
                                        updatedVanBan.FileDinhKem = relativePath;
                                    }
                                    // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                                }
                                
                                Services.VanBanChiBoModel vanBanModel = new Services.VanBanChiBoModel
                                {
                                    VanBanChiBoID = updatedVanBan.VanBanChiBoID,
                                    DonViID = updatedVanBan.DonViID,
                                    TenVanBan = updatedVanBan.TenVanBan,
                                    NoiDung = updatedVanBan.NoiDung,
                                    NgayGui = updatedVanBan.NgayGui,
                                    NgayNhan = updatedVanBan.NgayNhan,
                                    TrangThai = updatedVanBan.TrangThai,
                                    FileDinhKem = updatedVanBan.FileDinhKem
                                };
                                
                                (bool success, string error) = _vanBanChiBoService.Update(vanBanModel);
                                
                                if (success)
                                {
                                    MessageBox.Show("Cập nhật văn bản chi bộ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadTreeView();
                                    LoadVanBanChiBoData();
                                }
                                else
                                {
                                    MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewTaiLieu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_currentType))
            {
                MessageBox.Show("Vui lòng chọn thư mục TaiLieu hoặc VanBanChiBo trong TreeView!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (_currentType == "TaiLieu")
                    {
                        int taiLieuID = Convert.ToInt32(dataGridViewTaiLieu.SelectedRows[0].Cells["TaiLieuID"].Value);
                        (bool success, string error) = _taiLieuService.Delete(taiLieuID);
                        
                        if (success)
                        {
                            MessageBox.Show("Xóa tài liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTreeView();
                            LoadTaiLieuData();
                        }
                        else
                        {
                            MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (_currentType == "VanBanChiBo")
                    {
                        int vanBanID = Convert.ToInt32(dataGridViewTaiLieu.SelectedRows[0].Cells["VanBanChiBoID"].Value);
                        (bool success, string error) = _vanBanChiBoService.Delete(vanBanID);
                        
                        if (success)
                        {
                            MessageBox.Show("Xóa văn bản chi bộ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTreeView();
                            LoadVanBanChiBoData();
                        }
                        else
                        {
                            MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTreeView();
            LoadData();
        }
    }
}
