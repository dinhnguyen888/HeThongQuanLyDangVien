using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FormSua = QuanLyDangVien.FormSua;

namespace QuanLyDangVien.Pages
{
    public partial class PageTheoDoiChuyenChinhThuc : UserControl
    {
        private TheoDoiChuyenChinhThucService _theoDoiService;
        private DangVienService _dangVienService;
        private DonViService _donViService;
        private List<TheoDoiChuyenChinhThucDTO> _danhSachTheoDoi;
        private List<TheoDoiChuyenChinhThucDTO> _hienThiTheoDoi;

        public PageTheoDoiChuyenChinhThuc()
        {
            InitializeComponent();
            _theoDoiService = new TheoDoiChuyenChinhThucService();
            _dangVienService = new DangVienService();
            _donViService = new DonViService();
            _danhSachTheoDoi = new List<TheoDoiChuyenChinhThucDTO>();
            _hienThiTheoDoi = new List<TheoDoiChuyenChinhThucDTO>();

            SetupUI();
            LoadData();
            CheckAutoReminder();
        }

        private void SetupUI()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);

            // Thêm cột chức năng
            var chucNangCol = new DataGridViewComboBoxColumn
            {
                Name = "ChucNang",
                HeaderText = "Chức năng",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            chucNangCol.Items.AddRange(new object[] { "Sửa", "Xóa" });
            dataGridView1.Columns.Add(chucNangCol);
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;

            // Setup filter combobox
            metroComboBox1.Items.AddRange(new object[] { "Tất cả", "Đang theo dõi", "Đã chuyển chính thức", "Quá hạn" });
            metroComboBox1.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                _danhSachTheoDoi = _theoDoiService.GetAll();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            _hienThiTheoDoi = _danhSachTheoDoi.ToList();

            // Lọc theo trạng thái
            if (metroComboBox1.SelectedItem != null && metroComboBox1.SelectedItem.ToString() != "Tất cả")
            {
                string trangThai = metroComboBox1.SelectedItem.ToString();
                _hienThiTheoDoi = _hienThiTheoDoi
                    .Where(td => td.TrangThai != null && td.TrangThai.Equals(trangThai, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Tìm kiếm theo tên đảng viên
            if (!string.IsNullOrWhiteSpace(TimTb.Text))
            {
                string searchText = TimTb.Text.ToLower();
                _hienThiTheoDoi = _hienThiTheoDoi
                    .Where(td => td.TenDangVien != null && td.TenDangVien.ToLower().Contains(searchText))
                    .ToList();
            }

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _hienThiTheoDoi;

            // Ẩn các cột không cần thiết và sắp xếp lại
            if (dataGridView1.Columns.Count > 0)
            {
                var columnsToShow = new[] { "TenDangVien", "NgayVaoDang", "NgayChuyenChinhThuc", "TrangThai", "SoNgay", "GhiChu" };
                foreach (System.Windows.Forms.DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name == "ChucNang")
                    {
                        col.DisplayIndex = dataGridView1.Columns.Count - 1;
                        continue;
                    }
                    col.Visible = columnsToShow.Contains(col.Name);
                }

                // Đặt tên header
                if (dataGridView1.Columns["TenDangVien"] != null) dataGridView1.Columns["TenDangVien"].HeaderText = "Đảng viên";
                if (dataGridView1.Columns["NgayVaoDang"] != null) dataGridView1.Columns["NgayVaoDang"].HeaderText = "Ngày vào Đảng";
                if (dataGridView1.Columns["NgayChuyenChinhThuc"] != null) dataGridView1.Columns["NgayChuyenChinhThuc"].HeaderText = "Ngày chuyển chính thức";
                if (dataGridView1.Columns["TrangThai"] != null) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";
                if (dataGridView1.Columns["SoNgay"] != null) dataGridView1.Columns["SoNgay"].HeaderText = "Số ngày";
                if (dataGridView1.Columns["GhiChu"] != null) dataGridView1.Columns["GhiChu"].HeaderText = "Ghi chú";
            }
        }

        private void CheckAutoReminder()
        {
            try
            {
                // Lấy danh sách cần nhắc nhở (trước 30 ngày)
                var canNhacNho = _theoDoiService.GetCanNhacNho(30);
                
                if (canNhacNho != null && canNhacNho.Count > 0)
                {
                    string message = $"Có {canNhacNho.Count} đảng viên sắp đến hạn chuyển chính thức:\n\n";
                    foreach (var item in canNhacNho.Take(10)) // Hiển thị tối đa 10
                    {
                        int soNgayConLai = item.SoNgayConLai ?? 0;
                        message += $"- {item.TenDangVien}: Còn {soNgayConLai} ngày\n";
                    }
                    if (canNhacNho.Count > 10)
                    {
                        message += $"\n... và {canNhacNho.Count - 10} đảng viên khác.";
                    }
                    
                    MessageBox.Show(message, "Nhắc nhở chuyển chính thức", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Không hiển thị lỗi nếu không có dữ liệu nhắc nhở
                System.Diagnostics.Debug.WriteLine($"Lỗi khi kiểm tra nhắc nhở: {ex.Message}");
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
            if (comboBox == null || comboBox.SelectedItem == null) return;

            try
            {
                var row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                var idObj = row.Cells["TheoDoiChuyenChinhThucID"]?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int theoDoiID))
                    return;

                string action = comboBox.SelectedItem.ToString();
                dataGridView1.EndEdit();

                switch (action)
                {
                    case "Sửa":
                        var theoDoiToEdit = _theoDoiService.GetById(theoDoiID);
                        if (theoDoiToEdit != null)
                        {
                            var modelEdit = ConvertToModel(theoDoiToEdit);
                            var formSua = new FormSua(modelEdit);
                            // Set tên đảng viên vào TextBox DangVienID (read-only)
                            SetDangVienNameToTextBox(formSua, theoDoiToEdit.TenDangVien, theoDoiToEdit.DangVienID);
                            if (formSua.ShowDialog() == DialogResult.OK)
                            {
                                var updatedModel = formSua.GetData() as TheoDoiChuyenChinhThuc;
                                if (updatedModel != null)
                                {
                                    var (success, error) = _theoDoiService.Update(updatedModel);
                                    if (success)
                                    {
                                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        LoadData();
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        break;

                    case "Xóa":
                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var (success, error) = _theoDoiService.Delete(theoDoiID);
                            if (success)
                            {
                                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                }

                // Reset combo box
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TheoDoiChuyenChinhThuc ConvertToModel(TheoDoiChuyenChinhThucDTO dto)
        {
            return new TheoDoiChuyenChinhThuc
            {
                TheoDoiChuyenChinhThucID = dto.TheoDoiChuyenChinhThucID,
                DangVienID = dto.DangVienID,
                NgayVaoDang = dto.NgayVaoDang,
                NgayChuyenChinhThuc = dto.NgayChuyenChinhThuc,
                TrangThai = dto.TrangThai,
                GhiChu = dto.GhiChu,
                NgayTao = dto.NgayTao,
                NguoiTao = dto.NguoiTao
            };
        }

        /// <summary>
        /// Set tên đảng viên vào TextBox DangVienID (read-only) và lưu DangVienID vào Tag
        /// </summary>
        private void SetDangVienNameToTextBox(Form form, string tenDangVien, int dangVienID)
        {
            try
            {
                // Tìm TextBox DangVienID trong form
                TextBox txtDangVien = FindControlByName<TextBox>(form, "DangVienID");
                if (txtDangVien != null)
                {
                    // Set tên đảng viên vào Text
                    txtDangVien.Text = tenDangVien ?? $"Đảng viên ID: {dangVienID}";
                    // Lưu DangVienID vào Tag để khi lấy data, có thể giữ nguyên
                    txtDangVien.Tag = dangVienID;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi set tên đảng viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Tìm control theo tên trong form (recursive)
        /// </summary>
        private T FindControlByName<T>(Control parent, string name) where T : Control
        {
            if (parent == null || parent.IsDisposed) return null;

            if (parent is T control && control.Name == name)
            {
                return control;
            }

            foreach (Control child in parent.Controls)
            {
                T found = FindControlByName<T>(child, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private void TimKiemBtn_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TimTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilters();
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Tạo báo cáo
                var canNhacNho = _theoDoiService.GetCanNhacNho(30);
                var allData = _theoDoiService.GetAll();

                string report = "BÁO CÁO THEO DÕI CHUYỂN CHÍNH THỨC\n";
                report += "==========================================\n\n";
                report += $"Tổng số bản ghi: {allData.Count}\n";
                report += $"Đang theo dõi: {allData.Count(x => x.TrangThai == "Đang theo dõi")}\n";
                report += $"Đã chuyển chính thức: {allData.Count(x => x.TrangThai == "Đã chuyển chính thức")}\n";
                report += $"Quá hạn: {allData.Count(x => x.TrangThai == "Quá hạn")}\n\n";
                
                if (canNhacNho != null && canNhacNho.Count > 0)
                {
                    report += $"CẦN NHẮC NHỞ ({canNhacNho.Count} đảng viên):\n";
                    report += "----------------------------------------\n";
                    foreach (var item in canNhacNho)
                    {
                        int soNgayConLai = item.SoNgayConLai ?? 0;
                        report += $"- {item.TenDangVien}: Còn {soNgayConLai} ngày\n";
                    }
                }

                MessageBox.Show(report, "Báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
