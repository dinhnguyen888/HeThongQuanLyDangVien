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
using QuanLyDangVien.DTOs;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;

namespace QuanLyDangVien
{
    public partial class UserControlChuyenSinhHoatDang : UserControl
    {
        private ChuyenSinhHoatDangService _chuyenSinhHoatDangService;
        private DonViService _donViService;
        private DangVienService _dangVienService;
        private List<ChuyenSinhHoatDangDTO> _danhSachChuyenSinhHoat;
        private List<ChuyenSinhHoatDangDTO> _hienThiChuyenSinhHoat;
        private int? _dangVienIDFilter = null;
        private int? _donViIDFilter = null;
        private int? _namFilter = null;
        private string _trangThaiFilter = null;

        public UserControlChuyenSinhHoatDang()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
        }

        private void InitializeServices()
        {
            _chuyenSinhHoatDangService = new ChuyenSinhHoatDangService();
            _donViService = new DonViService();
            _dangVienService = new DangVienService();
            _danhSachChuyenSinhHoat = new List<ChuyenSinhHoatDangDTO>();
            _hienThiChuyenSinhHoat = new List<ChuyenSinhHoatDangDTO>();
        }

        private void SetupUI()
        {
            SetupDataGridView();
            SetupEvents();
            LoadDonViComboBox();
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            
            // Font size 10pt
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            
            // Row height cao hơn
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            
            // Fill width - sẽ được set trong RefreshDataGridView cho từng cột
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Tạm thời set None, sẽ set Fill cho từng cột
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Setup cột Chức năng
            var chucNangCol = ChucNang as DataGridViewComboBoxColumn;
            if (chucNangCol != null)
            {
                chucNangCol.Items.Clear();
                chucNangCol.Items.Add("Xem chi tiết");
                chucNangCol.Items.Add("Sửa");
                chucNangCol.Items.Add("Xóa");
                chucNangCol.Width = 150;
                chucNangCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

        private void SetupEvents()
        {
            TimBtn.Click += TimBtn_Click;
            TimTb.KeyDown += TimTb_KeyDown;
        }

        private void LoadDonViComboBox()
        {
            try
            {
                // Có thể thêm ComboBox cho lọc đơn vị nếu cần
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                _danhSachChuyenSinhHoat = _chuyenSinhHoatDangService.GetAll(
                    dangVienID: _dangVienIDFilter,
                    donViID: _donViIDFilter,
                    nam: _namFilter,
                    trangThai: _trangThaiFilter
                );

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // Thêm các cột
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ChuyenSinhHoatID",
                    HeaderText = "ID",
                    DataPropertyName = "ChuyenSinhHoatID",
                    Visible = false
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenDangVien",
                    HeaderText = "Tên Đảng viên",
                    DataPropertyName = "TenDangVien",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenDonViDi",
                    HeaderText = "Đơn vị đi",
                    DataPropertyName = "TenDonViDi",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 18
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenDonViDen",
                    HeaderText = "Đơn vị đến",
                    DataPropertyName = "TenDonViDen",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 18
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NgayChuyen",
                    HeaderText = "Ngày chuyển",
                    DataPropertyName = "NgayChuyen",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 12,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "LyDo",
                    HeaderText = "Lý do",
                    DataPropertyName = "LyDo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TrangThai",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 12
                });

                // Thêm cột Chức năng (width cố định)
                ChucNang.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ChucNang.Width = 150;
                dataGridView1.Columns.Add(ChucNang);

                // Set AutoSizeColumnsMode cho DataGridView để fill width
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.DataSource = _danhSachChuyenSinhHoat;
                
                // Apply font và padding sau khi bind data
                dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật DataGridView: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "ChucNang" && e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ChucNang_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ChucNang_SelectedIndexChanged;
            }
        }

        private void ChucNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || dataGridView1.CurrentRow == null)
                return;

            try
            {
                string selectedAction = comboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedAction))
                    return;

                var row = dataGridView1.CurrentRow;
                var idObj = row.Cells["ChuyenSinhHoatID"].Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int chuyenSinhHoatID))
                {
                    MessageBox.Show("Không thể xác định ID chuyển sinh hoạt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (selectedAction)
                {
                    case "Xem chi tiết":
                        XemChiTiet(chuyenSinhHoatID);
                        break;
                    case "Sửa":
                        SuaChuyenSinhHoat(chuyenSinhHoatID);
                        break;
                    case "Xóa":
                        XoaChuyenSinhHoat(chuyenSinhHoatID);
                        break;
                }

                // Reset combobox selection
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực hiện chức năng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemChiTiet(int chuyenSinhHoatID)
        {
            try
            {
                var chuyenSinhHoat = _chuyenSinhHoatDangService.GetById(chuyenSinhHoatID);
                if (chuyenSinhHoat == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin chuyển sinh hoạt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert DTO to Model
                var model = new ChuyenSinhHoatDang
                {
                    ChuyenSinhHoatID = chuyenSinhHoat.ChuyenSinhHoatID,
                    DangVienID = chuyenSinhHoat.DangVienID,
                    DonViDi = chuyenSinhHoat.DonViDi,
                    DonViDen = chuyenSinhHoat.DonViDen,
                    NgayChuyen = chuyenSinhHoat.NgayChuyen,
                    LyDo = chuyenSinhHoat.LyDo,
                    GhiChu = chuyenSinhHoat.GhiChu,
                    FileQuyetDinh = chuyenSinhHoat.FileQuyetDinh,
                    TrangThai = chuyenSinhHoat.TrangThai,
                    NgayTao = chuyenSinhHoat.NgayTao,
                    NguoiTao = chuyenSinhHoat.NguoiTao
                };

                FormXemChiTiet formXemChiTiet = new FormXemChiTiet(model);
                
                // Bind đơn vị cho ComboBox (chỉ DonViDen)
                var donViData = _donViService.GetDonViData();
                BindDonViComboBoxes(formXemChiTiet, donViData);
                
                // Set tên đơn vị đi vào TextBox DonViDi (read-only) từ thông tin đảng viên hiện tại
                // Lấy thông tin đảng viên để lấy DonViID hiện tại
                var dangVien = _dangVienService.GetById(chuyenSinhHoat.DangVienID);
                if (dangVien != null && dangVien.DonViID > 0)
                {
                    SetDonViDiNameToTextBox(formXemChiTiet, dangVien.DonViID);
                }
                else if (chuyenSinhHoat.DonViDi > 0)
                {
                    // Fallback: nếu không lấy được từ đảng viên, dùng giá trị đã lưu
                    SetDonViDiNameToTextBox(formXemChiTiet, chuyenSinhHoat.DonViDi);
                }

                formXemChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaChuyenSinhHoat(int chuyenSinhHoatID)
        {
            try
            {
                var chuyenSinhHoatDTO = _chuyenSinhHoatDangService.GetById(chuyenSinhHoatID);
                if (chuyenSinhHoatDTO == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin chuyển sinh hoạt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert DTO to Model
                var model = new ChuyenSinhHoatDang
                {
                    ChuyenSinhHoatID = chuyenSinhHoatDTO.ChuyenSinhHoatID,
                    DangVienID = chuyenSinhHoatDTO.DangVienID,
                    DonViDi = chuyenSinhHoatDTO.DonViDi,
                    DonViDen = chuyenSinhHoatDTO.DonViDen,
                    NgayChuyen = chuyenSinhHoatDTO.NgayChuyen,
                    LyDo = chuyenSinhHoatDTO.LyDo,
                    GhiChu = chuyenSinhHoatDTO.GhiChu,
                    FileQuyetDinh = chuyenSinhHoatDTO.FileQuyetDinh,
                    TrangThai = chuyenSinhHoatDTO.TrangThai,
                    NgayTao = chuyenSinhHoatDTO.NgayTao,
                    NguoiTao = chuyenSinhHoatDTO.NguoiTao
                };

                FormSua formSua = new FormSua(model);
                
                // Bind đơn vị cho ComboBox (chỉ DonViDen)
                var donViData = _donViService.GetDonViData();
                BindDonViComboBoxes(formSua, donViData);
                
                // Set tên đơn vị đi vào TextBox DonViDi (read-only) từ thông tin đảng viên hiện tại
                // Lấy thông tin đảng viên để lấy DonViID hiện tại
                var dangVien = _dangVienService.GetById(chuyenSinhHoatDTO.DangVienID);
                if (dangVien != null && dangVien.DonViID > 0)
                {
                    SetDonViDiNameToTextBox(formSua, dangVien.DonViID);
                    // Cập nhật DonViDi trong model để đảm bảo giá trị đúng
                    model.DonViDi = dangVien.DonViID;
                }
                else if (chuyenSinhHoatDTO.DonViDi > 0)
                {
                    // Fallback: nếu không lấy được từ đảng viên, dùng giá trị đã lưu
                    SetDonViDiNameToTextBox(formSua, chuyenSinhHoatDTO.DonViDi);
                }

                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedChuyenSinhHoat = formSua.GetData() as ChuyenSinhHoatDang;
                    
                    // Đảm bảo DonViDi luôn lấy từ thông tin đảng viên hiện tại
                    if (dangVien != null && dangVien.DonViID > 0)
                    {
                        updatedChuyenSinhHoat.DonViDi = dangVien.DonViID;
                    }
                    
                    // Xử lý file quyết định nếu có thay đổi
                    // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                    if (!string.IsNullOrWhiteSpace(updatedChuyenSinhHoat.FileQuyetDinh))
                    {
                        if (File.Exists(updatedChuyenSinhHoat.FileQuyetDinh))
                        {
                            try
                            {
                                // Copy file vào thư mục Server/ChuyenSinhHoat
                                string relativePath = FileHelper.SaveChuyenSinhHoatFile(updatedChuyenSinhHoat.FileQuyetDinh);
                                updatedChuyenSinhHoat.FileQuyetDinh = relativePath;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi khi lưu file quyết định: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                    }

                    var (success, error) = _chuyenSinhHoatDangService.Update(updatedChuyenSinhHoat);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Cập nhật chuyển sinh hoạt đảng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa chuyển sinh hoạt: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaChuyenSinhHoat(int chuyenSinhHoatID)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chuyển sinh hoạt đảng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                var (success, error) = _chuyenSinhHoatDangService.Delete(chuyenSinhHoatID);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Xóa chuyển sinh hoạt đảng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chuyển sinh hoạt: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDonViComboBoxes(Form form, List<DonViSimplified> donViData)
        {
            try
            {
                foreach (Control control in form.Controls)
                {
                    if (control is ComboBox comboBox)
                    {
                        string controlName = comboBox.Name;
                        // Chỉ bind DonViDen và DonViID, không bind DonViDi nữa vì nó là TextBox read-only
                        if (controlName == "DonViDen" || controlName == "DonViID")
                        {
                            comboBox.DataSource = null;
                            comboBox.DataSource = donViData;
                            comboBox.DisplayMember = "TenDonVi";
                            comboBox.ValueMember = "DonViID";
                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        }
                    }
                    else if (control.HasChildren)
                    {
                        BindDonViComboBoxesRecursive(control, donViData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi bind đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDonViComboBoxesRecursive(Control parent, List<DonViSimplified> donViData)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    string controlName = comboBox.Name;
                    // Chỉ bind DonViDen và DonViID, không bind DonViDi nữa vì nó là TextBox read-only
                    if (controlName == "DonViDen" || controlName == "DonViID")
                    {
                        comboBox.DataSource = null;
                        comboBox.DataSource = donViData;
                        comboBox.DisplayMember = "TenDonVi";
                        comboBox.ValueMember = "DonViID";
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
                else if (control.HasChildren)
                {
                    BindDonViComboBoxesRecursive(control, donViData);
                }
            }
        }

        /// <summary>
        /// Set tên đơn vị đi vào TextBox DonViDi (read-only) từ thông tin đảng viên
        /// </summary>
        private void SetDonViDiNameToTextBox(Form form, int donViID)
        {
            try
            {
                if (donViID <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("DonViID không hợp lệ");
                    return;
                }

                // Lấy thông tin đơn vị từ service
                var donVi = _donViService.GetById(donViID);
                if (donVi == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Không tìm thấy đơn vị với ID: {donViID}");
                    return;
                }

                // Tìm TextBox DonViDi trong form (recursive)
                TextBox txtDonViDi = FindControlByName<TextBox>(form, "DonViDi");
                if (txtDonViDi != null)
                {
                    // Set tên đơn vị vào Text
                    txtDonViDi.Text = donVi.TenDonVi ?? $"Đơn vị ID: {donViID}";
                    // Lưu DonViID vào Tag để khi lấy data, có thể giữ nguyên
                    txtDonViDi.Tag = donViID;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi set tên đơn vị đi: {ex.Message}");
            }
        }

        /// <summary>
        /// Tìm control theo tên trong form (recursive)
        /// </summary>
        private T FindControlByName<T>(Control parent, string name) where T : Control
        {
            if (parent == null) return null;
            
            if (parent.Name == name && parent is T)
            {
                return parent as T;
            }
            
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name && control is T)
                {
                    return control as T;
                }
                
                // Tìm đệ quy trong các control con
                T found = FindControlByName<T>(control, name);
                if (found != null)
                {
                    return found;
                }
            }
            
            return null;
        }

        private void TimBtn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void TimTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            try
            {
                string searchText = TimTb.Text?.Trim();
                
                // Tìm kiếm theo tên đảng viên
                if (!string.IsNullOrEmpty(searchText))
                {
                    string searchTextLower = searchText.ToLower();
                    _danhSachChuyenSinhHoat = _chuyenSinhHoatDangService.GetAll(
                        dangVienID: null,
                        donViID: null,
                        nam: null,
                        trangThai: null
                    ).Where(x => x.TenDangVien != null && x.TenDangVien.ToLower().Contains(searchTextLower)).ToList();
                }
                else
                {
                    _danhSachChuyenSinhHoat = _chuyenSinhHoatDangService.GetAll(
                        dangVienID: _dangVienIDFilter,
                        donViID: _donViIDFilter,
                        nam: _namFilter,
                        trangThai: _trangThaiFilter
                    );
                }

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc theo đảng viên
        /// </summary>
        public void FilterByDangVien(int dangVienID)
        {
            _dangVienIDFilter = dangVienID;
            _donViIDFilter = null;
            _namFilter = null;
            _trangThaiFilter = null;
            LoadData();
        }

        /// <summary>
        /// Lọc theo đơn vị
        /// </summary>
        public void FilterByDonVi(int donViID)
        {
            _donViIDFilter = donViID;
            _dangVienIDFilter = null;
            _namFilter = null;
            _trangThaiFilter = null;
            LoadData();
        }

        /// <summary>
        /// Lọc theo năm
        /// </summary>
        public void FilterByYear(int nam)
        {
            _namFilter = nam;
            _dangVienIDFilter = null;
            _donViIDFilter = null;
            _trangThaiFilter = null;
            LoadData();
        }

        /// <summary>
        /// Xem toàn hệ thống (không lọc)
        /// </summary>
        public void ViewAll()
        {
            _dangVienIDFilter = null;
            _donViIDFilter = null;
            _namFilter = null;
            _trangThaiFilter = null;
            LoadData();
        }
    }
}
