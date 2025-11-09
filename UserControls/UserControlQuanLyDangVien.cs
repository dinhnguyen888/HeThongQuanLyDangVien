using MetroFramework.Controls;
using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
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
    public partial class UserControlQuanLyDangVien : UserControl
    {
        private DangVienService _dangVienService;
        private DonViService _donViService;
        private List<DangVienDTO> _danhSachDangVien;
        private List<DangVienDTO> _hienThiDangVien; // Danh sách hiển thị tạm thời

        public UserControlQuanLyDangVien()
        {
            InitializeComponent();
            _dangVienService = new DangVienService();
            _donViService = new DonViService();
            _danhSachDangVien = new List<DangVienDTO>();
            _hienThiDangVien = new List<DangVienDTO>();

            LoadData();
            SetupUI();

            // Setup số lượng hiển thị
            SoluongCb.Items.AddRange(new object[] { "10", "20", "50", "Tất cả" });
            SoluongCb.SelectedIndex = 3;
            SoluongCb.SelectedIndexChanged += SoluongCb_SelectedIndexChanged;

            // Set DoiTuongComboBox
            DoiTuongCb.Items.AddRange(new object[] {
                new KeyValuePair<string, string>("Tất cả", "---Chọn Đối Tượng---"),
                new KeyValuePair<string, string>("SQ", "Đảng viên sĩ quan"),
                new KeyValuePair<string, string>("QNCN", "Đảng viên quân nhân chuyên nghiệp"),
                new KeyValuePair<string, string>("HSQ-BS", "Đảng viên hợp đồng binh sĩ"),
                new KeyValuePair<string, string>("LĐHĐ", "Đảng viên lao động hợp đồng"),
                new KeyValuePair<string, string>("CNVCQP", "Công nhân viên chức quốc phòng")
            });
            DoiTuongCb.DisplayMember = "Value";
            DoiTuongCb.ValueMember = "Key";
            DoiTuongCb.SelectedIndex = 0;
        }

        private void SetupUI()
        {
            if (string.IsNullOrEmpty(TimTb.Text))
            {
                TimTb.Text = "";
            }

            var col = DangVienGridView.Columns["ChucNang"] as DataGridViewComboBoxColumn;
            col.Items.Clear();
            col.Items.Add("Xem chi tiết");
            col.Items.Add("Sửa thông tin");
            col.Items.Add("Xóa đảng viên");

            DangVienGridView.EditingControlShowing += DangVienGridView_EditingControlShowing;
            DangVienGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DangVienGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            DangVienGridView.DefaultCellStyle.Font = new Font("Arial", 12);
            DangVienGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void LoadData()
        {
            try
            {
                _danhSachDangVien = _dangVienService.GetAll();
                CapNhatHienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật danh sách hiển thị theo số lượng được chọn
        /// </summary>
        private void CapNhatHienThi()
        {
            if (SoluongCb.SelectedItem == null)
                return;

            string selected = SoluongCb.SelectedItem.ToString();

            if (selected == "Tất cả")
            {
                // Hiển thị toàn bộ danh sách
                _hienThiDangVien = _danhSachDangVien.ToList();
            }
            else if (int.TryParse(selected, out int soLuong))
            {
                // Giới hạn số lượng hiển thị
                _hienThiDangVien = _danhSachDangVien.Take(soLuong).ToList();
            }
            else
            {
                // Mặc định: hiển thị tất cả nếu có lỗi
                _hienThiDangVien = _danhSachDangVien.ToList();
            }

            DangVienGridView.DataSource = null;
            DangVienGridView.DataSource = _hienThiDangVien;
        }

        private void SoluongCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHienThi();
        }

        private void DangVienGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DangVienGridView.CurrentCell.OwningColumn.Name == "ChucNang" && e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ChucNang_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ChucNang_SelectedIndexChanged;
            }
        }

        private void ChucNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || comboBox.SelectedItem == null) return;

            string selectedAction = comboBox.SelectedItem.ToString();
            var row = DangVienGridView.CurrentRow;
            if (row == null) return;

            var id = row.Cells["dangVienIDDataGridViewTextBoxColumn"].Value?.ToString();
            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int dangVienID))
            {
                MessageBox.Show("Không thể xác định đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                switch (selectedAction)
                {
                    case "Xem chi tiết":
                        var dangVienXem = _dangVienService.GetById(dangVienID);
                        if (dangVienXem != null)
                        {
                            FormXemChiTiet formXem = new FormXemChiTiet(dangVienXem);
                            var donViData = _donViService.GetDonViDataByDangVienId(dangVienID);
                            RefreshDonViData(formXem, donViData);
                            formXem.ShowDialog();
                        }
                        else
                            MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case "Sửa thông tin":
                        var dangVienDetails = _dangVienService.GetById(dangVienID);
                        if (dangVienDetails != null)
                        {
                            // Convert DangVienDTO thành DangVien để sử dụng với FormSua
                            var dangVienSua = new DangVien
                            {
                                DangVienID = dangVienDetails.DangVienID,
                                DonViID = dangVienDetails.DonViID,
                                HoTen = dangVienDetails.HoTen,
                                NgaySinh = dangVienDetails.NgaySinh,
                                GioiTinh = dangVienDetails.GioiTinh,
                                SoCCCD = dangVienDetails.SoCCCD,
                                SoDienThoai = dangVienDetails.SoDienThoai,
                                SoTheDangVien = dangVienDetails.SoTheDangVien,
                                SoLyLichDangVien = dangVienDetails.SoLyLichDangVien,
                                NgayVaoDang = dangVienDetails.NgayVaoDang,
                                NgayChinhThuc = dangVienDetails.NgayChinhThuc,
                                LoaiDangVien = dangVienDetails.LoaiDangVien,
                                DoiTuong = dangVienDetails.DoiTuong,
                                CapBac = dangVienDetails.CapBac,
                                ChucVu = dangVienDetails.ChucVu,
                                QueQuan = dangVienDetails.QueQuan,
                                DiaChi = dangVienDetails.DiaChi,
                                DanToc = dangVienDetails.DanToc,
                                TonGiao = dangVienDetails.TonGiao,
                                NgheNghiep = dangVienDetails.NgheNghiep,
                                TrinhDoHocVan = dangVienDetails.TrinhDoHocVan,
                                TrinhDoChuyenMon = dangVienDetails.TrinhDoChuyenMon,
                                LyLuanChinhTri = dangVienDetails.LyLuanChinhTri,
                                NgoaiNgu = dangVienDetails.NgoaiNgu,
                                TinHoc = dangVienDetails.TinHoc,
                                AnhDaiDien = dangVienDetails.AnhDaiDien,
                                QuaTrinhCongTac = dangVienDetails.QuaTrinhCongTac,
                                HoSoGiaDinh = dangVienDetails.HoSoGiaDinh,
                                TrangThai = dangVienDetails.TrangThai,
                                NgayTao = dangVienDetails.NgayTao,
                                NguoiTao = dangVienDetails.NguoiTao
                            };
                            
                            FormSua formSua = new FormSua(dangVienSua);
                            var donViData = _donViService.GetDonViData();
                            RefreshDonViData(formSua, donViData);

                            if (formSua.ShowDialog() == DialogResult.OK)
                            {
                                var updatedDangVien = formSua.GetData() as DangVien;
                                
                                // **QUAN TRỌNG: Đảm bảo DangVienID được giữ nguyên**
                                if (updatedDangVien.DangVienID == 0)
                                {
                                    updatedDangVien.DangVienID = dangVienDetails.DangVienID;
                                }
                                
                                var (success, error) = _dangVienService.Update(updatedDangVien);
                                if (!string.IsNullOrEmpty(error))
                                {
                                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                LoadData();
                                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                            MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case "Xóa đảng viên":
                        if (MessageBox.Show($"Bạn có chắc muốn xóa đảng viên {dangVienID}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            var (success, error) = _dangVienService.Delete(dangVienID);
                            if (!string.IsNullOrEmpty(error))
                            {
                                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            LoadData();
                            MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            comboBox.SelectedIndex = -1;
        }

        private void TimKiemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = TimTb.Text?.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadData();
                    return;
                }

                // Sử dụng các tham số mới từ DangVienService
                _danhSachDangVien = _dangVienService.GetAll(
                    hoTen: searchText,
                    soCCCD: searchText,
                    capBac: null,
                    chucVu: null,
                    queQuan: null,
                    trinhDo: null
                );

                CapNhatHienThi();

                if (_danhSachDangVien.Count == 0)
                    MessageBox.Show("Không tìm thấy đảng viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FormThem form = new FormThem(typeof(DangVien));
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(form, donViData);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newDangVien = form.GetData() as DangVien;
                    var (id, error) = _dangVienService.Insert(newDangVien);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Thêm đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaNhieuBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DangVienGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một đảng viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa {DangVienGridView.SelectedRows.Count} đảng viên đã chọn?",
                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                int successCount = 0, failCount = 0;
                foreach (DataGridViewRow row in DangVienGridView.SelectedRows)
                {
                    var idObj = row.Cells["dangVienIDDataGridViewTextBoxColumn"].Value;
                    if (idObj != null && int.TryParse(idObj.ToString(), out int dangVienID))
                    {
                        var result = _dangVienService.Delete(dangVienID);
                        if (result.Item1) successCount++; else failCount++;
                    }
                    else failCount++;
                }

                LoadData();
                MessageBox.Show($"Đã xóa thành công {successCount} đảng viên.\nKhông thể xóa {failCount} đảng viên.",
                                "Kết quả", MessageBoxButtons.OK,
                                successCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                }

                if (control.HasChildren)
                    BindDonViComboBoxes(control, donViData);
            }
        }

        private void DoiTuongCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Lọc theo loại đối tượng nếu cần
        }
    }
}
