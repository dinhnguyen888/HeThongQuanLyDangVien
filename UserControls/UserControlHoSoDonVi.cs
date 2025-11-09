using QuanLyDangVien.Pages;
using QuanLyDangVien.Services;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    public partial class UserControlHoSoDonVi : UserControl
    {
        private List<DonVi> _donViList;
        private List<DangVienDTO> _dangVienList;
        private List<DangVienDTO> _hienThiDangVien; // Danh sách hiển thị tạm thời
        private DangVienService _dangVienService;
        private DonViService _donViService;
        private int _selectedDonViId = 0;

        public UserControlHoSoDonVi()
        {
            InitializeComponent();
            _dangVienService = new DangVienService();
            _donViService = new DonViService();
            InitializeUI();
            LoadDonViData();
            
            // Initialize collections
            _hienThiDangVien = new List<DangVienDTO>();
            
            // Mặc định hiển thị tab hồ sơ đơn vị
            ShowHoSoDonVi();
        }

        private void InitializeUI()
        {
            // Setup DataGridView cho đơn vị
            SetupDonViDataGridView();
            
            // Setup DataGridView cho đảng viên
            SetupDangVienDataGridView();
            
            // Setup events
            dgvDonVi.SelectionChanged += DgvDonVi_SelectionChanged;
            dgvDangVien.SelectionChanged += DgvDangVien_SelectionChanged;
            
            // Đơn vị buttons
            btnThemDonVi.Click += BtnThemDonVi_Click;
            btnSuaDonVi.Click += BtnSuaDonVi_Click;
            btnXoaDonVi.Click += BtnXoaDonVi_Click;
            
            // Đảng viên buttons
            btnThemDangVien.Click += BtnThemDangVien_Click;
            btnSuaDangVien.Click += BtnSuaDangVien_Click;
            btnXoaDangVien.Click += BtnXoaDangVien_Click;
            btnXemChiTiet.Click += BtnXemChiTiet_Click;
            
            // Initially disable buttons
            UpdateDonViButtonStates();
            UpdateDangVienButtonStates();
        }

        private void SetupDonViDataGridView()
        {
            dgvDonVi.AutoGenerateColumns = false;
            dgvDonVi.AllowUserToAddRows = false;
            dgvDonVi.AllowUserToDeleteRows = false;
            dgvDonVi.ReadOnly = true;
            dgvDonVi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonVi.MultiSelect = false;
            dgvDonVi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Add all columns for DonVi
            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonViID",
                DataPropertyName = "DonViID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaDonVi",
                DataPropertyName = "MaDonVi",
                HeaderText = "Mã đơn vị",
                FillWeight = 15
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDonVi",
                DataPropertyName = "TenDonVi",
                HeaderText = "Tên đơn vị",
                FillWeight = 25
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CapBac",
                DataPropertyName = "CapBac",
                HeaderText = "Cấp bậc",
                FillWeight = 12
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                DataPropertyName = "DiaChi",
                HeaderText = "Địa chỉ",
                FillWeight = 20
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                FillWeight = 16
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TruongDonVi",
                DataPropertyName = "TruongDonVi",
                HeaderText = "Trưởng đơn vị",
                FillWeight = 16
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Ngày tạo",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }
        private void RefreshData()
{
    // Reload đơn vị data
    LoadDonViData();
    
    // Nếu đang có đơn vị được chọn, reload lại danh sách đảng viên của đơn vị đó
    if (_selectedDonViId > 0)
    {
        LoadDangVienData(_selectedDonViId);
    }
}

        private void SetupDangVienDataGridView()
        {
            dgvDangVien.AutoGenerateColumns = false;
            dgvDangVien.AllowUserToAddRows = false;
            dgvDangVien.AllowUserToDeleteRows = false;
            dgvDangVien.ReadOnly = true;
            dgvDangVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDangVien.MultiSelect = true; // Enable multi-select for batch operations
            dgvDangVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDangVien.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDangVien.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            dgvDangVien.DefaultCellStyle.Font = new Font("Arial", 12);
            dgvDangVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Add important columns for DangVien
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DangVienID",
                DataPropertyName = "DangVienID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                DataPropertyName = "HoTen",
                HeaderText = "Họ tên",
                FillWeight = 25
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoTheDangVien",
                DataPropertyName = "SoTheDangVien",
                HeaderText = "Số thẻ ĐV",
                FillWeight = 15
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgaySinh",
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày sinh",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GioiTinh",
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới tính",
                FillWeight = 8
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ChucVu",
                DataPropertyName = "ChucVu",
                HeaderText = "Chức vụ",
                FillWeight = 15
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiDangVien",
                DataPropertyName = "LoaiDangVien",
                HeaderText = "Loại ĐV",
                FillWeight = 12
            });

            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DoiTuong",
                DataPropertyName = "DoiTuong",
                HeaderText = "Đối tượng",
                FillWeight = 13
            });
        }

        private void LoadDonViData()
        {
            try
            {
                _donViList = _donViService.GetAll();
                dgvDonVi.DataSource = _donViList;
                
                // Clear đảng viên data
                dgvDangVien.DataSource = null;
                _dangVienList = new List<DangVienDTO>();
                _selectedDonViId = 0;
                
                UpdateDonViButtonStates();
                UpdateDangVienButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDangVienData(int donViId)
        {
            try
            {
                _dangVienList = _dangVienService.GetByDonViID(donViId);
                _selectedDonViId = donViId;
                
                // Initialize display list
                CapNhatHienThiDangVien();
                
                UpdateDangVienButtonStates();
                
                // Update group box title
                var selectedDonVi = _donViList?.FirstOrDefault(x => x.DonViID == donViId);
                if (selectedDonVi != null)
                {
                    groupBoxDangVien.Text = $"Danh sách đảng viên - {selectedDonVi.TenDonVi} ({_dangVienList.Count} đảng viên)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu đảng viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật danh sách hiển thị đảng viên (có thể áp dụng filter/search sau này)
        /// </summary>
        private void CapNhatHienThiDangVien()
        {
            if (_dangVienList == null)
            {
                _hienThiDangVien = new List<DangVienDTO>();
            }
            else
            {
                _hienThiDangVien = _dangVienList.ToList();
            }

            dgvDangVien.DataSource = null;
            dgvDangVien.DataSource = _hienThiDangVien;
        }

        private void DgvDonVi_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonVi.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDonVi.SelectedRows[0];
                if (selectedRow.Cells["DonViID"].Value != null)
                {
                    int donViId = Convert.ToInt32(selectedRow.Cells["DonViID"].Value);
                    LoadDangVienData(donViId);
                }
            }
            UpdateDonViButtonStates();
        }

        private void DgvDangVien_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDangVienButtonStates();
        }

        private void UpdateDonViButtonStates()
        {
            bool hasSelectedDonVi = dgvDonVi.SelectedRows.Count > 0;
            
            btnSuaDonVi.Enabled = hasSelectedDonVi;
            btnXoaDonVi.Enabled = hasSelectedDonVi;
        }

        private void UpdateDangVienButtonStates()
        {
            bool hasSelectedDonVi = _selectedDonViId > 0;
            bool hasSelectedDangVien = dgvDangVien.SelectedRows.Count > 0;
            bool hasMultipleSelected = dgvDangVien.SelectedRows.Count > 1;

            btnThemDangVien.Enabled = hasSelectedDonVi;
            btnSuaDangVien.Enabled = hasSelectedDangVien && !hasMultipleSelected; // Only single selection for edit
            btnXoaDangVien.Enabled = hasSelectedDangVien; // Allow multiple deletion
            btnXemChiTiet.Enabled = hasSelectedDangVien && !hasMultipleSelected; // Only single selection for view
        }

        private void BtnThemDangVien_Click(object sender, EventArgs e)
        {
            if (_selectedDonViId <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn vị trước!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FormThem form = new FormThem(typeof(DangVien));
                var donViData = _donViService.GetDonViData();   
                RefreshDonViData(form, donViData);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newDangVien = form.GetData() as DangVien;
                    newDangVien.DonViID = _selectedDonViId; // Ensure correct DonViID
                    newDangVien.NgayTao = DateTime.Now;
                    newDangVien.NguoiTao = "admin"; // TODO: Get from session

                    var (id, error) = _dangVienService.Insert(newDangVien);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Thêm đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaDangVien_Click(object sender, EventArgs e)
        {
            var selectedDangVien = GetSelectedDangVien();
            if (selectedDangVien == null)
            {
                MessageBox.Show("Vui lòng chọn đảng viên cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy full data từ database để đảm bảo có đầy đủ thông tin
                var dangVienSua = _dangVienService.GetById(selectedDangVien.DangVienID);
                if (dangVienSua == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FormSua formSua = new FormSua(dangVienSua);
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formSua, donViData);

                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedDangVien = formSua.GetData() as DangVien;
                    
                    // **QUAN TRỌNG: Đảm bảo DangVienID được giữ nguyên**
                    if (updatedDangVien.DangVienID == 0)
                    {
                        updatedDangVien.DangVienID = dangVienSua.DangVienID;
                    }
                    
                    var (success, error) = _dangVienService.Update(updatedDangVien);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật đảng viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaDangVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDangVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một đảng viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa {dgvDangVien.SelectedRows.Count} đảng viên đã chọn?",
                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                int successCount = 0, failCount = 0;
                foreach (DataGridViewRow row in dgvDangVien.SelectedRows)
                {
                    var idObj = row.Cells["DangVienID"].Value;
                    if (idObj != null && int.TryParse(idObj.ToString(), out int dangVienID))
                    {
                        var result = _dangVienService.Delete(dangVienID);
                        if (result.Item1) successCount++; else failCount++;
                    }
                    else failCount++;
                }

                RefreshData();
                MessageBox.Show($"Đã xóa thành công {successCount} đảng viên.\nKhông thể xóa {failCount} đảng viên.",
                                "Kết quả", MessageBoxButtons.OK,
                                successCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            var selectedDangVien = GetSelectedDangVien();
            if (selectedDangVien == null)
            {
                MessageBox.Show("Vui lòng chọn đảng viên cần xem!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dangVienXem = _dangVienService.GetById(selectedDangVien.DangVienID);
                if (dangVienXem != null)
                {
                    FormXemChiTiet formXem = new FormXemChiTiet(dangVienXem);
                    var donViData = _donViService.GetDonViDataByDangVienId(selectedDangVien.DangVienID);
                    RefreshDonViData(formXem, donViData);
                    formXem.ShowDialog();
                }
                else
                    MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DangVienDTO GetSelectedDangVien()
        {
            if (dgvDangVien.SelectedRows.Count == 0) return null;

            var selectedRow = dgvDangVien.SelectedRows[0];
            if (selectedRow.Cells["DangVienID"].Value == null) return null;

            int dangVienId = Convert.ToInt32(selectedRow.Cells["DangVienID"].Value);
            return _hienThiDangVien?.FirstOrDefault(x => x.DangVienID == dangVienId);
        }

        public void RefreshDonViData(Form form, List<DonViSimplified> donViData)
        {
            if (form == null) return;
            BindDonViComboBoxes(form, donViData);
        }

        private void BindDonViComboBoxes(Control parent, List<DonViSimplified> donViData)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                {
                    comboBox.DataSource = donViData;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";
                    
                    // Set selected value to current unit
                    if (_selectedDonViId > 0)
                    {
                        comboBox.SelectedValue = _selectedDonViId;
                    }
                }

                if (control.HasChildren)
                    BindDonViComboBoxes(control, donViData);
            }
        }

        private void DanhSachChiBo_Click(object sender, EventArgs e)
        {
            ShowHoSoDonVi();
        }

        private void SinhHoatChiBo_Click(object sender, EventArgs e)
        {
            ShowSinhHoatChiBo();
        }

        private void ShowHoSoDonVi()
        {
            // Clear content panel and show the main split layout (units and party members)
            contentTableLayoutPanel.Controls.Clear();
            
            // Re-add the left and right panels
            contentTableLayoutPanel.Controls.Add(leftPanel, 0, 0);
            contentTableLayoutPanel.Controls.Add(rightPanel, 1, 0);
            
            // Set the active button
            SetActiveButton(DanhSachChiBo);
        }

        private void ShowSinhHoatChiBo()
        {
            // Clear content panel and show PageSinhHoatChiBo
            contentTableLayoutPanel.Controls.Clear();
            
            // Create and add PageSinhHoatChiBo to fill the entire content area
            var pageSinhHoat = new PageSinhHoatChiBo();
            pageSinhHoat.Dock = DockStyle.Fill;
            
            // Add to span both columns
            contentTableLayoutPanel.Controls.Add(pageSinhHoat, 0, 0);
            contentTableLayoutPanel.SetColumnSpan(pageSinhHoat, 2);
            
            // Set the active button
            SetActiveButton(SinhHoatChiBo);
        }

        private void SetActiveButton(MetroFramework.Controls.MetroTile activeButton)
        {
            // Reset all buttons to default style
            DanhSachChiBo.Style = MetroFramework.MetroColorStyle.Default;
            SinhHoatChiBo.Style = MetroFramework.MetroColorStyle.Default;
            
            // Set active button style
            activeButton.Style = MetroFramework.MetroColorStyle.Blue;
        }

        // ===== CRUD OPERATIONS FOR ĐƠN VỊ =====

        private void BtnThemDonVi_Click(object sender, EventArgs e)
        {
            try
            {
                var newDonVi = new DonVi
                {
                    NgayTao = DateTime.Now,
                    NguoiTao = "admin" // TODO: Get from session
                };

                using (var form = new FormThem(typeof(DonVi)))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var data = (DonVi)form.GetData();
                        data.NgayTao = DateTime.Now;
                        data.NguoiTao = "admin"; // TODO: Get from session

                        try
                        {
                            int id = _donViService.Insert(data);
                            MessageBox.Show("Thêm đơn vị thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonViData();
                        }
                        catch (Exception insertEx)
                        {
                            MessageBox.Show($"Lỗi khi thêm đơn vị: {insertEx.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaDonVi_Click(object sender, EventArgs e)
        {
            var selectedDonVi = GetSelectedDonVi();
            if (selectedDonVi == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var donViDetail = _donViService.GetById(selectedDonVi.DonViID);
                if (donViDetail == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn vị!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var form = new FormSua(donViDetail))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var updatedData = (DonVi)form.GetData();
                        try
                        {
                            bool success = _donViService.Update(updatedData);
                            if (!success)
                            {
                                MessageBox.Show("Cập nhật đơn vị thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show("Cập nhật đơn vị thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonViData();
                        }
                        catch (Exception updateEx)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật đơn vị: {updateEx.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaDonVi_Click(object sender, EventArgs e)
        {
            var selectedDonVi = GetSelectedDonVi();
            if (selectedDonVi == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đơn vị '{selectedDonVi.TenDonVi}'?\n\nLưu ý: Không thể xóa đơn vị đang có đảng viên!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _donViService.Delete(selectedDonVi.DonViID);
                    if (!success)
                    {
                        MessageBox.Show("Xóa đơn vị thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Xóa đơn vị thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonViData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đơn vị: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private DonVi GetSelectedDonVi()
        {
            if (dgvDonVi.SelectedRows.Count == 0) return null;

            var selectedRow = dgvDonVi.SelectedRows[0];
            if (selectedRow.Cells["DonViID"].Value == null) return null;

            int donViId = Convert.ToInt32(selectedRow.Cells["DonViID"].Value);
            return _donViList?.FirstOrDefault(x => x.DonViID == donViId);
        }

       
    }
}
