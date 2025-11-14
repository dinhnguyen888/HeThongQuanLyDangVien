using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyDangVien.Pages
{
    public partial class PageQuanLyDonVi : UserControl
    {
        private List<DonVi> _donViList;
        private List<DonViThongKe> _thongKeList;
        private DonViService _donViService;

        public PageQuanLyDonVi()
        {
            InitializeComponent();
            _donViService = new DonViService();
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            // Thiết lập DataGridView
            SetupDataGridView();
            
            // Thiết lập events
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnXemChiTiet.Click += BtnXemChiTiet_Click;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            dgvDonVi.SelectionChanged += DgvDonVi_SelectionChanged;
            dgvDonVi.CellDoubleClick += DgvDonVi_CellDoubleClick;

            // Thiết lập trạng thái ban đầu
            UpdateButtonStates();
        }

        private void SetupDataGridView()
        {
            dgvDonVi.AutoGenerateColumns = false;
            dgvDonVi.AllowUserToAddRows = false;
            dgvDonVi.AllowUserToDeleteRows = false;
            dgvDonVi.ReadOnly = true;
            dgvDonVi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonVi.MultiSelect = false;
            dgvDonVi.RowHeadersVisible = false;
            dgvDonVi.BackgroundColor = Color.White;
            dgvDonVi.BorderStyle = BorderStyle.None;
            dgvDonVi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDonVi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDonVi.EnableHeadersVisualStyles = false;
            
            // Header styling
            dgvDonVi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 174, 219);
            dgvDonVi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDonVi.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDonVi.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvDonVi.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDonVi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDonVi.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 174, 219);
            dgvDonVi.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvDonVi.ColumnHeadersHeight = 60;
            dgvDonVi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            // Row styling
            dgvDonVi.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDonVi.RowsDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvDonVi.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            dgvDonVi.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDonVi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvDonVi.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            dgvDonVi.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDonVi.RowTemplate.Height = 40;

            // Thêm các cột
            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonViID",
                DataPropertyName = "DonViID",
                HeaderText = "ID",
                Width = 60,
                Visible = false
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaDonVi",
                DataPropertyName = "MaDonVi",
                HeaderText = "Mã đơn vị",
                Width = 100
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDonVi",
                DataPropertyName = "TenDonVi",
                HeaderText = "Tên đơn vị",
                Width = 200
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CapBac",
                DataPropertyName = "CapBac",
                HeaderText = "Cấp bậc",
                Width = 100
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenCapTren",
                DataPropertyName = "TenCapTren",
                HeaderText = "Cấp trên",
                Width = 150
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TruongDonVi",
                DataPropertyName = "TruongDonVi",
                HeaderText = "Trưởng đơn vị",
                Width = 150
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 150
            });

            dgvDonVi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Ngày tạo",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // Cột thống kê (nếu có)
            var colSoLuong = new DataGridViewTextBoxColumn
            {
                Name = "SoLuongDangVien",
                HeaderText = "Số ĐV",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvDonVi.Columns.Add(colSoLuong);
            
            // Format date column
            dgvDonVi.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                
                try
                {
                    if (e.ColumnIndex == dgvDonVi.Columns["NgayTao"].Index)
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
                    else if (e.ColumnIndex == colSoLuong.Index)
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
                }
                catch
                {
                    e.FormattingApplied = false;
                }
            };
            
            // Handle DataError to prevent dialog from showing
            dgvDonVi.DataError += (s, e) =>
            {
                e.ThrowException = false;
            };
        }

        private void LoadData()
        {
            try
            {
                // Load danh sách đơn vị
                _donViList = _donViService.GetAll();
                
                // Load thống kê
                _thongKeList = _donViService.GetDangVienCountByDonVi();
                
                // Gộp dữ liệu
                var displayData = from donVi in _donViList
                                  join thongKe in _thongKeList on donVi.DonViID equals thongKe.DonViID into gj
                                  from subThongKe in gj.DefaultIfEmpty()
                                  select new
                                  {
                                      donVi.DonViID,
                                      donVi.MaDonVi,
                                      donVi.TenDonVi,
                                      donVi.CapBac,
                                      donVi.TenCapTren,
                                      donVi.TruongDonVi,
                                      donVi.Email,
                                      donVi.NgayTao,
                                      SoLuongDangVien = subThongKe?.SoLuongDangVien ?? 0
                                  };

                dgvDonVi.DataSource = displayData.ToList();
                
                // Cập nhật label thống kê
                lblTongSo.Text = $"Tổng số: {_donViList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var newDonVi = new DonVi
                {
                    NgayTao = DateTime.Now,
                    NguoiTao = "admin" // TODO: Lấy từ session
                };

                using (var form = new FormThem(typeof(DonVi)))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var data = (DonVi)form.GetData();
                        data.NgayTao = DateTime.Now;
                        data.NguoiTao = "admin"; // TODO: Lấy từ session

                        int newId = _donViService.Insert(data);
                        if (newId > 0)
                        {
                            MessageBox.Show("Thêm đơn vị thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
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

        private void BtnSua_Click(object sender, EventArgs e)
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
                // Lấy dữ liệu chi tiết từ database
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
                        bool success = _donViService.Update(updatedData);
                        
                        if (success)
                        {
                            MessageBox.Show("Cập nhật đơn vị thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật đơn vị thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", 
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            var selectedDonVi = GetSelectedDonVi();
            if (selectedDonVi == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đơn vị '{selectedDonVi.TenDonVi}'?", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _donViService.Delete(selectedDonVi.DonViID);
                    if (success)
                    {
                        MessageBox.Show("Xóa đơn vị thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa đơn vị thất bại! Vui lòng kiểm tra lại.", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đơn vị: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            var selectedDonVi = GetSelectedDonVi();
            if (selectedDonVi == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần xem!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy dữ liệu chi tiết từ database
                var donViDetail = _donViService.GetById(selectedDonVi.DonViID);
                if (donViDetail == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn vị!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var form = new FormXemChiTiet(donViDetail))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            txtTimKiem.Text = "";
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (_donViList == null) return;

            string keyword = txtTimKiem.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var filteredData = from donVi in _donViList
                              where donVi.TenDonVi.ToLower().Contains(keyword) ||
                                    donVi.MaDonVi.ToLower().Contains(keyword) ||
                                    (donVi.TruongDonVi != null && donVi.TruongDonVi.ToLower().Contains(keyword)) ||
                                    (donVi.TenCapTren != null && donVi.TenCapTren.ToLower().Contains(keyword))
                              join thongKe in _thongKeList on donVi.DonViID equals thongKe.DonViID into gj
                              from subThongKe in gj.DefaultIfEmpty()
                              select new
                              {
                                  donVi.DonViID,
                                  donVi.MaDonVi,
                                  donVi.TenDonVi,
                                  donVi.CapBac,
                                  donVi.TenCapTren,
                                  donVi.TruongDonVi,
                                  donVi.Email,
                                  donVi.NgayTao,
                                  SoLuongDangVien = subThongKe?.SoLuongDangVien ?? 0
                              };

            dgvDonVi.DataSource = filteredData.ToList();
        }

        private void DgvDonVi_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void DgvDonVi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnXemChiTiet_Click(sender, e);
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvDonVi.SelectedRows.Count > 0;
            btnSua.Enabled = hasSelection;
            btnXoa.Enabled = hasSelection;
            btnXemChiTiet.Enabled = hasSelection;
        }

        private DonVi GetSelectedDonVi()
        {
            if (dgvDonVi.SelectedRows.Count == 0) return null;

            var selectedRow = dgvDonVi.SelectedRows[0];
            int donViId = Convert.ToInt32(selectedRow.Cells["DonViID"].Value);
            
            return _donViList?.FirstOrDefault(x => x.DonViID == donViId);
        }
    }
}