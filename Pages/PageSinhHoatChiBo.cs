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
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using QuanLyDangVien.Helper;
using QuanLyDangVien.DTOs;

namespace QuanLyDangVien.Pages
{
    public partial class PageSinhHoatChiBo : UserControl
    {
        private SinhHoatChiBoService _sinhHoatService;
        private DonViService _donViService;
        private List<SinhHoatChiBo> _danhSachSinhHoat;
        private List<SinhHoatChiBo> _danhSachSinhHoatFiltered;

        public PageSinhHoatChiBo()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
            ApplyPermissions();
        }

        private void InitializeServices()
        {
            _sinhHoatService = new SinhHoatChiBoService();
            _donViService = new DonViService();
        }

        private void SetupUI()
        {
            SetupDataGridView();
            SetupFilterPanel();
        }

        private void SetupFilterPanel()
        {
            // Load đơn vị vào ComboBox
            var donViList = _donViService.GetDonViData();
            cmbDonVi.DataSource = new List<DonViSimplified> { new DonViSimplified { DonViID = 0, TenDonVi = "Tất cả" } }
                .Concat(donViList).ToList();
            cmbDonVi.DisplayMember = "TenDonVi";
            cmbDonVi.ValueMember = "DonViID";

            // Load trạng thái
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "Chưa điểm danh", "Đã điểm danh", "Hoàn thành" });
            cmbTrangThai.SelectedIndex = 0;

            // Set default date range (current month)
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void SetupDataGridView()
        {
            dgvSinhHoat.AutoGenerateColumns = false;
            dgvSinhHoat.AllowUserToAddRows = false;
            dgvSinhHoat.AllowUserToDeleteRows = false;
            dgvSinhHoat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhHoat.MultiSelect = false;
            dgvSinhHoat.ReadOnly = true;
            dgvSinhHoat.RowHeadersVisible = false; // Ẩn row headers để không ảnh hưởng đến header
            dgvSinhHoat.BackgroundColor = Color.White;
            dgvSinhHoat.BorderStyle = BorderStyle.None;
            dgvSinhHoat.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSinhHoat.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSinhHoat.EnableHeadersVisualStyles = false;
            
            // Header styling - màu xanh đẹp
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 174, 219);
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 174, 219); // Giữ màu header khi select
            dgvSinhHoat.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White; // Giữ màu chữ header khi select
            dgvSinhHoat.ColumnHeadersHeight = 60;
            dgvSinhHoat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            // Row styling
            dgvSinhHoat.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvSinhHoat.RowsDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvSinhHoat.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255); // Màu xanh nhạt khi chọn row
            dgvSinhHoat.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSinhHoat.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvSinhHoat.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            dgvSinhHoat.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSinhHoat.RowTemplate.Height = 40;
            
            // Allow column resizing
            dgvSinhHoat.AllowUserToResizeColumns = true;

            // Clear existing columns
            dgvSinhHoat.Columns.Clear();

            // ID column (hidden)
            dgvSinhHoat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SinhHoatID",
                HeaderText = "ID",
                DataPropertyName = "SinhHoatID",
                Visible = false
            });

            // STT column
            var colSTT = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 70,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(5, 0, 5, 0)
                }
            };
            dgvSinhHoat.Columns.Add(colSTT);

            // Tiêu đề
            dgvSinhHoat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TieuDe",
                HeaderText = "Tiêu đề",
                DataPropertyName = "TieuDe",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 25
            });

            // Ngày sinh hoạt
            var colNgaySinhHoat = new DataGridViewTextBoxColumn
            {
                Name = "NgaySinhHoat",
                HeaderText = "Ngày sinh hoạt",
                DataPropertyName = "NgaySinhHoat",
                Width = 150
            };
            dgvSinhHoat.Columns.Add(colNgaySinhHoat);

            // Địa điểm
            dgvSinhHoat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaDiem",
                HeaderText = "Địa điểm",
                DataPropertyName = "DiaDiem",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 20
            });

            // Chủ trì
            dgvSinhHoat.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ChuTri",
                HeaderText = "Chủ trì",
                DataPropertyName = "ChuTri",
                Width = 150
            });

            // Số lượng tham gia
            var colSoLuongThamGia = new DataGridViewTextBoxColumn
            {
                Name = "SoLuongThamGia",
                HeaderText = "Số lượng",
                DataPropertyName = "SoLuongThamGia",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvSinhHoat.Columns.Add(colSoLuongThamGia);

            // Trạng thái
            var colTrangThai = new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 150
            };
            dgvSinhHoat.Columns.Add(colTrangThai);

            // File nghị quyết column
            var colFileNghiQuyet = new DataGridViewButtonColumn
            {
                Name = "FileNghiQuyet",
                HeaderText = "File nghị quyết",
                Text = "Xem file",
                UseColumnTextForButtonValue = true,
                Width = 140,
                ReadOnly = true
            };
            dgvSinhHoat.Columns.Add(colFileNghiQuyet);

            // Action column
            var colAction = new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "Thao tác",
                Text = "Xem chi tiết",
                UseColumnTextForButtonValue = true,
                Width = 140,
                ReadOnly = true
            };
            dgvSinhHoat.Columns.Add(colAction);

            // Format date column
            dgvSinhHoat.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                try
                {
                    if (e.ColumnIndex == colSTT.Index)
                    {
                        e.Value = e.RowIndex + 1;
                        e.FormattingApplied = true;
                    }
                    else if (e.ColumnIndex == colNgaySinhHoat.Index)
                    {
                        if (e.Value != null && e.Value != DBNull.Value)
                        {
                            DateTime dateValue;
                            if (e.Value is DateTime)
                            {
                                dateValue = (DateTime)e.Value;
                            }
                            else if (DateTime.TryParse(e.Value.ToString(), out dateValue))
                            {
                                // Parsed successfully
                            }
                            else
                            {
                                e.Value = "";
                                e.FormattingApplied = true;
                                return;
                            }
                            e.Value = dateValue.ToString("dd/MM/yyyy");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                            e.FormattingApplied = true;
                        }
                    }
                    else if (e.ColumnIndex == colSoLuongThamGia.Index)
                    {
                        if (e.Value != null && e.Value != DBNull.Value)
                        {
                            int intValue;
                            if (e.Value is int)
                            {
                                intValue = (int)e.Value;
                            }
                            else if (int.TryParse(e.Value.ToString(), out intValue))
                            {
                                // Parsed successfully
                            }
                            else
                            {
                                e.Value = "0";
                                e.FormattingApplied = true;
                                return;
                            }
                            e.Value = intValue.ToString();
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0";
                            e.FormattingApplied = true;
                        }
                    }
                    else if (e.ColumnIndex == colTrangThai.Index && e.Value != null && e.Value != DBNull.Value)
                    {
                        string trangThai = e.Value.ToString();
                        if (trangThai == "Chưa điểm danh")
                            e.CellStyle.ForeColor = Color.Orange;
                        else if (trangThai == "Đã điểm danh")
                            e.CellStyle.ForeColor = Color.Blue;
                        else if (trangThai == "Hoàn thành")
                            e.CellStyle.ForeColor = Color.Green;
                        else
                            e.CellStyle.ForeColor = Color.Black;
                    }
                    else if (e.ColumnIndex == colFileNghiQuyet.Index || e.ColumnIndex == colAction.Index)
                    {
                        // Button columns - always use the button text, don't format from data
                        e.FormattingApplied = false; // Let the button use its Text property
                    }
                }
                catch
                {
                    e.FormattingApplied = false;
                }
            };

            // Handle DataError to prevent dialog from showing
            dgvSinhHoat.DataError += (s, e) =>
            {
                e.ThrowException = false;
                // Suppress the error dialog
            };

            // Handle action button click
            dgvSinhHoat.CellContentClick += DgvSinhHoat_CellContentClick;
            dgvSinhHoat.CellDoubleClick += DgvSinhHoat_CellDoubleClick;
        }

        private void LoadData()
        {
            try
            {
                int? donViID = null;
                if (cmbDonVi.SelectedValue != null && (int)cmbDonVi.SelectedValue > 0)
                {
                    donViID = (int)cmbDonVi.SelectedValue;
                }

                DateTime? tuNgay = dtpTuNgay.Value.Date;
                DateTime? denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                _danhSachSinhHoat = _sinhHoatService.GetSinhHoatChiBoList(donViID, tuNgay, denNgay);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            _danhSachSinhHoatFiltered = _danhSachSinhHoat.ToList();

            // Filter by Trạng thái
            if (cmbTrangThai.SelectedIndex > 0)
            {
                string trangThai = cmbTrangThai.SelectedItem.ToString();
                _danhSachSinhHoatFiltered = _danhSachSinhHoatFiltered
                    .Where(x => x.TrangThai == trangThai)
                    .ToList();
            }

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                string searchText = txtTimKiem.Text.ToLower();
                _danhSachSinhHoatFiltered = _danhSachSinhHoatFiltered
                    .Where(x => 
                        (x.TieuDe != null && x.TieuDe.ToLower().Contains(searchText)) ||
                        (x.DiaDiem != null && x.DiaDiem.ToLower().Contains(searchText)) ||
                        (x.ChuTri != null && x.ChuTri.ToLower().Contains(searchText))
                    )
                    .ToList();
            }

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            try
            {
                // Suspend layout to prevent errors during binding
                dgvSinhHoat.SuspendLayout();
                
                // Clear data source first
                dgvSinhHoat.DataSource = null;
                
                // Bind data directly - DataError handler will catch any format errors
                dgvSinhHoat.DataSource = _danhSachSinhHoatFiltered;
                
                dgvSinhHoat.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu vào bảng: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSinhHoat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvSinhHoat.Rows[e.RowIndex];
            var sinhHoatID = Convert.ToInt32(row.Cells["SinhHoatID"].Value);

            if (e.ColumnIndex == dgvSinhHoat.Columns["FileNghiQuyet"].Index)
            {
                // Xem file nghị quyết
                XemFileNghiQuyet(sinhHoatID);
            }
            else if (e.ColumnIndex == dgvSinhHoat.Columns["Action"].Index)
            {
                // Xem chi tiết
                XemChiTiet(sinhHoatID);
            }
        }

        private void DgvSinhHoat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvSinhHoat.Columns["Action"].Index)
            {
                var row = dgvSinhHoat.Rows[e.RowIndex];
                var sinhHoatID = Convert.ToInt32(row.Cells["SinhHoatID"].Value);
                XemChiTiet(sinhHoatID);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("SinhHoatChiBo", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm sinh hoạt chi bộ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var formThem = new FormThem(typeof(SinhHoatChiBo));
                var donViData = _donViService.GetDonViData();
                BindDonViComboBoxesRecursive(formThem, donViData);
                
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    var newSinhHoat = (SinhHoatChiBo)formThem.GetData();
                    
                    // Xử lý upload file nghị quyết nếu có
                    if (!string.IsNullOrWhiteSpace(newSinhHoat.FileNghiQuyet) && File.Exists(newSinhHoat.FileNghiQuyet))
                    {
                        try
                        {
                            string savedPath = FileHelper.SaveSinhHoatChiBoFile(newSinhHoat.FileNghiQuyet);
                            newSinhHoat.FileNghiQuyet = savedPath;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    
                    var sinhHoatID = _sinhHoatService.AddSinhHoatChiBo(newSinhHoat);
                    MessageBox.Show($"Đã tạo lịch sinh hoạt thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo lịch sinh hoạt: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaSinhHoat(int sinhHoatID)
        {
            try
            {
                var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                if (sinhHoat == null)
                {
                    MessageBox.Show("Không tìm thấy buổi sinh hoạt!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var formSua = new FormSua(sinhHoat);
                var donViData = _donViService.GetDonViData();
                BindDonViComboBoxesRecursive(formSua, donViData);
                
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedSinhHoat = (SinhHoatChiBo)formSua.GetData();
                    
                    // Xử lý upload file nghị quyết nếu có file mới
                    if (!string.IsNullOrWhiteSpace(updatedSinhHoat.FileNghiQuyet) && File.Exists(updatedSinhHoat.FileNghiQuyet))
                    {
                        // Kiểm tra xem có phải là file mới (đường dẫn đầy đủ) hay đường dẫn đã lưu (tương đối)
                        if (Path.IsPathRooted(updatedSinhHoat.FileNghiQuyet))
                        {
                            try
                            {
                                // Lưu file mới và xóa file cũ nếu có
                                string oldFilePath = sinhHoat.FileNghiQuyet;
                                string savedPath = FileHelper.SaveSinhHoatChiBoFile(updatedSinhHoat.FileNghiQuyet);
                                updatedSinhHoat.FileNghiQuyet = savedPath;
                                
                                // Xóa file cũ nếu có
                                if (!string.IsNullOrWhiteSpace(oldFilePath))
                                {
                                    try
                                    {
                                        string fullOldPath = FileHelper.GetFullPath(oldFilePath);
                                        if (File.Exists(fullOldPath))
                                        {
                                            File.Delete(fullOldPath);
                                        }
                                    }
                                    catch { } // Bỏ qua lỗi xóa file cũ
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    
                    _sinhHoatService.UpdateSinhHoatChiBo(updatedSinhHoat);
                    MessageBox.Show("Đã cập nhật thông tin sinh hoạt thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa thông tin: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaSinhHoat(int sinhHoatID)
        {
            try
            {
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa buổi sinh hoạt này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _sinhHoatService.DeleteSinhHoatChiBo(sinhHoatID);
                    MessageBox.Show("Đã xóa buổi sinh hoạt thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemChiTiet(int sinhHoatID)
        {
            try
            {
                var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                if (sinhHoat == null)
                {
                    MessageBox.Show("Không tìm thấy buổi sinh hoạt!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var formXemChiTiet = new FormXemChiTiet(sinhHoat);
                var donViData = _donViService.GetDonViData();
                BindDonViComboBoxesRecursive(formXemChiTiet, donViData);
                formXemChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cmbDonVi.SelectedIndex = 0;
            cmbTrangThai.SelectedIndex = 0;
            txtTimKiem.Clear();
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            LoadData();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilters();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("SinhHoatChiBo", "Update"))
            {
                MessageBox.Show("Bạn không có quyền sửa sinh hoạt chi bộ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvSinhHoat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn buổi sinh hoạt để sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvSinhHoat.SelectedRows[0];
                var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
            SuaSinhHoat(sinhHoatID);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("SinhHoatChiBo", "Delete"))
            {
                MessageBox.Show("Bạn không có quyền xóa sinh hoạt chi bộ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvSinhHoat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn buổi sinh hoạt để xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvSinhHoat.SelectedRows[0];
            var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
            XoaSinhHoat(sinhHoatID);
        }

        private void XemFileNghiQuyet(int sinhHoatID)
        {
            try
            {
                var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                if (sinhHoat == null)
                {
                    MessageBox.Show("Không tìm thấy buổi sinh hoạt!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(sinhHoat.FileNghiQuyet))
                {
                    MessageBox.Show("Buổi sinh hoạt này chưa có file nghị quyết!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Kiểm tra file có tồn tại không
                string filePath = sinhHoat.FileNghiQuyet;
                
                // Nếu là đường dẫn tương đối, cần resolve thành đường dẫn đầy đủ
                if (!Path.IsPathRooted(filePath))
                {
                    filePath = FileHelper.GetFullPath(filePath);
                }

                if (File.Exists(filePath))
                {
                    // Mở file bằng ứng dụng mặc định
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy file: {filePath}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bind dữ liệu đơn vị vào các ComboBox trong form (recursive)
        /// </summary>
        private void BindDonViComboBoxesRecursive(Control parent, List<DonViSimplified> donViData)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                {
                    // Save current selected value
                    object selectedValue = comboBox.SelectedValue;

                    comboBox.DataSource = donViData;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";

                    // Restore selected value if it exists
                    if (selectedValue != null)
                    {
                        try
                        {
                            comboBox.SelectedValue = selectedValue;
                        }
                        catch
                        {
                            // If value doesn't exist, select first item
                            comboBox.SelectedIndex = 0;
                        }
                    }
                }

                if (control.HasChildren)
                {
                    BindDonViComboBoxesRecursive(control, donViData);
                }
            }
        }

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canCreate = AuthorizationHelper.HasPermission("SinhHoatChiBo", "Create");
            bool canUpdate = AuthorizationHelper.HasPermission("SinhHoatChiBo", "Update");
            bool canDelete = AuthorizationHelper.HasPermission("SinhHoatChiBo", "Delete");

            if (btnThem != null) btnThem.Enabled = canCreate;
            if (btnSua != null) btnSua.Enabled = canUpdate;
            if (btnXoa != null) btnXoa.Enabled = canDelete;
        }
    }
}
