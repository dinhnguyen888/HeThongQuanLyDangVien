using QuanLyDangVien.DTOs;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FormXemChiTiet = QuanLyDangVien.FormXemChiTiet;
using FormSua = QuanLyDangVien.FormSua;

namespace QuanLyDangVien.Pages
{
    public partial class PageQuanLyDangVienDuBi : UserControl
    {
        private DangVienService _dangVienService;
        private DonViService _donViService;
        private List<DangVienDTO> _danhSachDangVien;
        private List<DangVienDTO> _hienThiDangVien;

        public PageQuanLyDangVienDuBi()
        {
            InitializeComponent();
            _dangVienService = new DangVienService();
            _donViService = new DonViService();
            _danhSachDangVien = new List<DangVienDTO>();
            _hienThiDangVien = new List<DangVienDTO>();

            SetupUI();
            LoadData();
            SetupFilters();
        }

        private void SetupUI()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            // Thêm cột chức năng
            var chucNangCol = new DataGridViewComboBoxColumn
            {
                Name = "ChucNang",
                HeaderText = "Chức năng",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            chucNangCol.Items.AddRange(new object[] { "Xem chi tiết", "Sửa", "Xóa", "Theo dõi" });
            dataGridView1.Columns.Add(chucNangCol);
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

        private void SetupFilters()
        {
            // Load đơn vị
            var donViList = _donViService.GetAll();
            metroComboBox1.Items.Add("Tất cả");
            foreach (var donVi in donViList)
            {
                metroComboBox1.Items.Add(donVi);
            }
            metroComboBox1.DisplayMember = "TenDonVi";
            metroComboBox1.SelectedIndex = 0;

            // Setup Đối tượng
            metroComboBox2.Items.AddRange(new object[] { "Tất cả", "SQ", "QNCN", "HSQ-BS", "LĐHĐ", "CNVCQP" });
            metroComboBox2.SelectedIndex = 0;

            // Setup Trình độ
            metroComboBox5.Items.AddRange(new object[] { "Tất cả", "Tiểu học", "Trung học cơ sở", "Trung học phổ thông", "Trung cấp", "Cao đẳng", "Đại học", "Thạc sĩ", "Tiến sĩ" });
            metroComboBox5.SelectedIndex = 0;

            // Quê quán sẽ được cập nhật sau khi load data
            metroComboBox4.Items.Add("Tất cả");
            metroComboBox4.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                // Lấy tất cả đảng viên
                _danhSachDangVien = _dangVienService.GetAll();
                
                // Lọc chỉ lấy đảng viên "Dự bị"
                _danhSachDangVien = _danhSachDangVien
                    .Where(dv => dv.LoaiDangVien != null && dv.LoaiDangVien.Trim().Equals("Dự bị", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Cập nhật filter quê quán sau khi load data
                UpdateQueQuanFilter();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateQueQuanFilter()
        {
            metroComboBox4.Items.Clear();
            metroComboBox4.Items.Add("Tất cả");
            var queQuanList = _danhSachDangVien
                .Where(dv => !string.IsNullOrWhiteSpace(dv.QueQuan))
                .Select(dv => dv.QueQuan)
                .Distinct()
                .OrderBy(q => q)
                .ToList();
            foreach (var queQuan in queQuanList)
            {
                metroComboBox4.Items.Add(queQuan);
            }
            metroComboBox4.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            _hienThiDangVien = _danhSachDangVien.ToList();

            // Lọc theo đơn vị
            if (metroComboBox1.SelectedItem != null && metroComboBox1.SelectedItem.ToString() != "Tất cả")
            {
                if (metroComboBox1.SelectedItem is DonViSimplified donVi)
                {
                    _hienThiDangVien = _hienThiDangVien.Where(dv => dv.DonViID == donVi.DonViID).ToList();
                }
            }

            // Lọc theo họ tên
            if (!string.IsNullOrWhiteSpace(TimTb.Text))
            {
                string searchText = TimTb.Text.ToLower();
                _hienThiDangVien = _hienThiDangVien
                    .Where(dv => dv.HoTen != null && dv.HoTen.ToLower().Contains(searchText))
                    .ToList();
            }

            // Lọc theo đối tượng
            if (metroComboBox2.SelectedItem != null && metroComboBox2.SelectedItem.ToString() != "Tất cả")
            {
                string doiTuong = metroComboBox2.SelectedItem.ToString();
                _hienThiDangVien = _hienThiDangVien
                    .Where(dv => dv.DoiTuong != null && dv.DoiTuong.Equals(doiTuong, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Lọc theo quê quán
            if (metroComboBox4.SelectedItem != null && metroComboBox4.SelectedItem.ToString() != "Tất cả")
            {
                string queQuan = metroComboBox4.SelectedItem.ToString();
                _hienThiDangVien = _hienThiDangVien
                    .Where(dv => dv.QueQuan != null && dv.QueQuan.Contains(queQuan))
                    .ToList();
            }

            // Lọc theo trình độ
            if (metroComboBox5.SelectedItem != null && metroComboBox5.SelectedItem.ToString() != "Tất cả")
            {
                string trinhDo = metroComboBox5.SelectedItem.ToString();
                _hienThiDangVien = _hienThiDangVien
                    .Where(dv => dv.TrinhDo != null && dv.TrinhDo.Equals(trinhDo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _hienThiDangVien;

            // Ẩn các cột không cần thiết và sắp xếp lại
            if (dataGridView1.Columns.Count > 0)
            {
                // Hiển thị các cột cần thiết
                var columnsToShow = new[] { "HoTen", "GioiTinh", "NgaySinh", "NgayVaoDang", "TenDonVi", "DoiTuong", "QueQuan", "TrinhDo", "TuoiDoi" };
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name == "ChucNang")
                    {
                        col.DisplayIndex = dataGridView1.Columns.Count - 1;
                        continue;
                    }
                    col.Visible = columnsToShow.Contains(col.Name);
                }

                // Đặt tên header
                if (dataGridView1.Columns["HoTen"] != null) dataGridView1.Columns["HoTen"].HeaderText = "Họ và tên";
                if (dataGridView1.Columns["GioiTinh"] != null) dataGridView1.Columns["GioiTinh"].HeaderText = "Giới tính";
                if (dataGridView1.Columns["NgaySinh"] != null) dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                if (dataGridView1.Columns["NgayVaoDang"] != null) dataGridView1.Columns["NgayVaoDang"].HeaderText = "Vào Đảng";
                if (dataGridView1.Columns["TenDonVi"] != null) dataGridView1.Columns["TenDonVi"].HeaderText = "Đơn vị";
                if (dataGridView1.Columns["DoiTuong"] != null) dataGridView1.Columns["DoiTuong"].HeaderText = "Đối tượng";
                if (dataGridView1.Columns["QueQuan"] != null) dataGridView1.Columns["QueQuan"].HeaderText = "Quê quán";
                if (dataGridView1.Columns["TrinhDo"] != null) dataGridView1.Columns["TrinhDo"].HeaderText = "Trình độ";
                if (dataGridView1.Columns["TuoiDoi"] != null) dataGridView1.Columns["TuoiDoi"].HeaderText = "Tuổi đời";
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var idObj = row.Cells["DangVienID"]?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int dangVienID))
                    return;

                var dangVien = _dangVienService.GetById(dangVienID);
                if (dangVien != null)
                {
                    var formXem = new FormXemChiTiet(dangVien);
                    var donViData = _donViService.GetDonViDataByDangVienId(dangVienID);
                    BindDonViComboBoxes(formXem, donViData);
                    formXem.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var idObj = row.Cells["DangVienID"]?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int dangVienID))
                    return;

                string action = comboBox.SelectedItem.ToString();
                dataGridView1.EndEdit();

                switch (action)
                {
                    case "Xem chi tiết":
                        var dangVien = _dangVienService.GetById(dangVienID);
                        if (dangVien != null)
                        {
                            var formXem = new FormXemChiTiet(dangVien);
                            var donViData = _donViService.GetDonViDataByDangVienId(dangVienID);
                            BindDonViComboBoxes(formXem, donViData);
                            formXem.ShowDialog();
                        }
                        break;

                    case "Sửa":
                        var dangVienToEdit = _dangVienService.GetById(dangVienID);
                        if (dangVienToEdit != null)
                        {
                            var model = ConvertDTOToModel(dangVienToEdit);
                            var formSua = new FormSua(model);
                            var donViDataEdit = _donViService.GetDonViDataByDangVienId(dangVienID);
                            BindDonViComboBoxes(formSua, donViDataEdit);
                            if (formSua.ShowDialog() == DialogResult.OK)
                            {
                                var updatedModel = formSua.GetData() as DangVien;
                                if (updatedModel != null)
                                {
                                    var (success, error) = _dangVienService.Update(updatedModel);
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
                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa đảng viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var (success, error) = _dangVienService.Delete(dangVienID);
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

                    case "Theo dõi":
                        if (MessageBox.Show("Bạn có chắc chắn muốn chuyển đảng viên này lên chính thức?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var dangVienToConvert = _dangVienService.GetById(dangVienID);
                            if (dangVienToConvert != null)
                            {
                                // Tạo bản ghi theo dõi chuyển chính thức
                                var theoDoiService = new TheoDoiChuyenChinhThucService();
                                var newTheoDoi = new TheoDoiChuyenChinhThuc
                                {
                                    DangVienID = dangVienID,
                                    NgayVaoDang = dangVienToConvert.NgayVaoDang ?? DateTime.Now,
                                    NgayChuyenChinhThuc = DateTime.Now,
                                    TrangThai = "Đang theo dõi",
                                    NguoiTao = "system" // Có thể lấy từ session sau
                                };

                                var (theoDoiId, theoDoiError) = theoDoiService.Insert(newTheoDoi);
                                
                                if (theoDoiId > 0)
                                {
                                    // Cập nhật đảng viên: chuyển từ "Dự bị" sang "Chính thức"
                                    var modelToUpdate = ConvertDTOToModel(dangVienToConvert);
                                    modelToUpdate.LoaiDangVien = "Chính thức";
                                    modelToUpdate.NgayChinhThuc = DateTime.Now;

                                    var (updateSuccess, updateError) = _dangVienService.Update(modelToUpdate);
                                    
                                    if (updateSuccess)
                                    {
                                        MessageBox.Show("Chuyển đảng viên lên chính thức thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        LoadData(); // Reload để cập nhật danh sách (đảng viên này sẽ không còn trong danh sách dự bị)
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Lỗi khi cập nhật đảng viên: {updateError}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Lỗi khi tạo bản ghi theo dõi: {theoDoiError}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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

        private void BindDonViComboBoxes(Form form, List<DonViSimplified> donViData)
        {
            BindDonViComboBoxesRecursive(form.Controls, donViData);
        }

        private void BindDonViComboBoxesRecursive(Control.ControlCollection controls, List<DonViSimplified> donViData)
        {
            foreach (Control control in controls)
            {
                if (control is ComboBox cbo && cbo.Name == "DonViID")
                {
                    cbo.DataSource = null;
                    cbo.DataSource = donViData;
                    cbo.DisplayMember = "TenDonVi";
                    cbo.ValueMember = "DonViID";
                    cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbo.FormattingEnabled = true;
                    cbo.Refresh();
                }
                else if (control.HasChildren)
                {
                    BindDonViComboBoxesRecursive(control.Controls, donViData);
                }
            }
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

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void metroComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void metroComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private DangVien ConvertDTOToModel(DangVienDTO dto)
        {
            return new DangVien
            {
                DangVienID = dto.DangVienID,
                DonViID = dto.DonViID,
                HoTen = dto.HoTen,
                NgaySinh = dto.NgaySinh,
                GioiTinh = dto.GioiTinh,
                SoCCCD = dto.SoCCCD,
                SoDienThoai = dto.SoDienThoai,
                SoTheDangVien = dto.SoTheDangVien,
                SoLyLichDangVien = dto.SoLyLichDangVien,
                NgayVaoDang = dto.NgayVaoDang,
                NgayChinhThuc = dto.NgayChinhThuc,
                LoaiDangVien = dto.LoaiDangVien,
                DoiTuong = dto.DoiTuong,
                CapBac = dto.CapBac,
                ChucVu = dto.ChucVu,
                QueQuan = dto.QueQuan,
                TrinhDo = dto.TrinhDo,
                DiaChi = dto.DiaChi,
                DanToc = dto.DanToc,
                TonGiao = dto.TonGiao,
                NgheNghiep = dto.NgheNghiep,
                TrinhDoHocVan = dto.TrinhDoHocVan,
                TrinhDoChuyenMon = dto.TrinhDoChuyenMon,
                LyLuanChinhTri = dto.LyLuanChinhTri,
                NgoaiNgu = dto.NgoaiNgu,
                TinHoc = dto.TinHoc,
                AnhDaiDien = dto.AnhDaiDien,
                QuaTrinhCongTac = dto.QuaTrinhCongTac,
                HoSoGiaDinh = dto.HoSoGiaDinh,
                TrangThai = dto.TrangThai,
                NgayTao = dto.NgayTao,
                NguoiTao = dto.NguoiTao
            };
        }
    }
}
