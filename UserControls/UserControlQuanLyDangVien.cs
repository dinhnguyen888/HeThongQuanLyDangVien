using MetroFramework.Controls;
using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using QuanLyDangVien.DTOs;
using static QuanLyDangVien.Helper.AuthorizationHelper;
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
using ClosedXML.Excel;

namespace QuanLyDangVien
{
    public partial class UserControlQuanLyDangVien : UserControl
    {
        private DangVienService _dangVienService;
        private DonViService _donViService;
        private TaiLieuHoSoService _taiLieuHoSoService;
        private ChuyenSinhHoatDangService _chuyenSinhHoatDangService;
        private KhenThuongCaNhanService _khenThuongService;
        private KyLuatCaNhanService _kyLuatService;
        private List<DangVienDTO> _danhSachDangVien;
        private List<DangVienDTO> _hienThiDangVien; // Danh sách hiển thị tạm thời

        public UserControlQuanLyDangVien()
        {
            InitializeComponent();
            _dangVienService = new DangVienService();
            _donViService = new DonViService();
            _taiLieuHoSoService = new TaiLieuHoSoService();
            _chuyenSinhHoatDangService = new ChuyenSinhHoatDangService();
            _khenThuongService = new KhenThuongCaNhanService();
            _kyLuatService = new KyLuatCaNhanService();
            _danhSachDangVien = new List<DangVienDTO>();
            _hienThiDangVien = new List<DangVienDTO>();

            LoadData();
            SetupUI();
            ApplyPermissions();

            // Setup số lượng hiển thị
            SoluongCb.Items.AddRange(new object[] { "10", "20", "50", "Tất cả" });
            SoluongCb.SelectedIndex = 3;
            SoluongCb.SelectedIndexChanged += SoluongCb_SelectedIndexChanged;

            // Set DoiTuongComboBox - hiển thị viết tắt (không có "Tất cả")
            DoiTuongCb.Items.AddRange(new object[] {
                "SQ",
                "QNCN",
                "HSQ-BS",
                "LĐHĐ",
                "CNVCQP"
            });
            DoiTuongCb.SelectedIndex = -1; // Không chọn mặc định

            // Set CapBacComboBox
            CapBacCb.Items.AddRange(new object[] {
                "Đảng viên",
                "Bí thư",
                "Phó Bí thư",
                "Ủy viên"
            });
            CapBacCb.SelectedIndex = -1;

            // Set TrinhDoComboBox
            TrinhDoCb.Items.AddRange(new object[] {
                "Trung học cơ sở",
                "Trung học phổ thông",
                "Trung cấp",
                "Cao đẳng",
                "Đại học",
                "Thạc sĩ",
                "Tiến sĩ"
            });
            TrinhDoCb.SelectedIndex = -1;

            // Set LoaiDangVienComboBox
            LoaiDangVienCb.Items.AddRange(new object[] {
                "Chính thức",
                "Dự bị"
            });
            LoaiDangVienCb.SelectedIndex = -1;

            // Set TrangThaiComboBox
            TrangThaiCb.Items.AddRange(new object[] {
                "Đang hoạt động",
                "Không hoạt động"
            });
            TrangThaiCb.SelectedIndex = 0; // Mặc định chọn "Đang hoạt động"
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
            col.Items.Add("Xem hồ sơ");

            // Cải thiện giao diện DataGridView
            DangVienGridView.RowHeadersVisible = false;
            DangVienGridView.BackgroundColor = Color.White;
            DangVienGridView.BorderStyle = BorderStyle.None;
            DangVienGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DangVienGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DangVienGridView.EnableHeadersVisualStyles = false;
            
            // Header styling
            DangVienGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            DangVienGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DangVienGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            DangVienGridView.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            DangVienGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DangVienGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DangVienGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 53, 69);
            DangVienGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            DangVienGridView.ColumnHeadersHeight = 60;
            DangVienGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            // Row styling
            DangVienGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DangVienGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            DangVienGridView.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            DangVienGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 200, 200);
            DangVienGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            DangVienGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            DangVienGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 200, 200);
            DangVienGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            DangVienGridView.RowTemplate.Height = 50;
            DangVienGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // Tắt auto size để tối ưu hiệu suất
            
            // Allow column resizing
            DangVienGridView.AllowUserToResizeColumns = true;
            DangVienGridView.ReadOnly = false; // Allow individual column ReadOnly settings
            DangVienGridView.EditMode = DataGridViewEditMode.EditOnEnter; // Allow clicking to edit ComboBoxes

            // Set tất cả các cột là ReadOnly trừ cột ChucNang
            DangVienGridView.CellBeginEdit += DangVienGridView_CellBeginEdit;

            DangVienGridView.EditingControlShowing += DangVienGridView_EditingControlShowing;
            DangVienGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả hàng khi click
            
            // Handle DataError to prevent dialog from showing
            DangVienGridView.DataError += (s, e) =>
            {
                e.ThrowException = false;
            };
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
            
            // Set tất cả các cột là ReadOnly trừ cột ChucNang
            foreach (DataGridViewColumn column in DangVienGridView.Columns)
            {
                if (column.Name != "ChucNang")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void SoluongCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHienThi();
        }

        private void DangVienGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Chỉ cho phép edit cột ChucNang (ComboBox), các cột khác không cho phép edit
            if (DangVienGridView.Columns[e.ColumnIndex].Name != "ChucNang")
            {
                e.Cancel = true;
            }
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
                            // Kiểm tra quyền Update
                            if (!AuthorizationHelper.HasPermission("DangVien", "Update", dangVienDetails.DonViID))
                            {
                                MessageBox.Show("Bạn không có quyền sửa thông tin đảng viên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
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

                    case "Xem hồ sơ":
                        var taiLieuList = _taiLieuHoSoService.GetByDangVienID(dangVienID);
                        if (taiLieuList == null || taiLieuList.Count == 0)
                        {
                            MessageBox.Show("Đảng viên này chưa có tài liệu hồ sơ nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Nếu có nhiều file, hiển thị dialog để chọn
                        if (taiLieuList.Count == 1)
                        {
                            // Chỉ có 1 file, mở trực tiếp
                            var filePath = taiLieuList[0].DuongDan;
                            if (!FileHelper.OpenFile(filePath))
                            {
                                MessageBox.Show("Không thể mở file. Vui lòng kiểm tra lại đường dẫn file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Có nhiều file, hiển thị form chọn file
                            using (var formChonFile = new FormChonTaiLieu(taiLieuList))
                            {
                                if (formChonFile.ShowDialog() == DialogResult.OK)
                                {
                                    var selectedFile = formChonFile.SelectedTaiLieu;
                                    if (selectedFile != null)
                                    {
                                        if (!FileHelper.OpenFile(selectedFile.DuongDan))
                                        {
                                            MessageBox.Show("Không thể mở file. Vui lòng kiểm tra lại đường dẫn file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền Create
            if (!AuthorizationHelper.HasPermission("DangVien", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm hồ sơ đảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Kiểm tra xem có đảng viên nào được chọn không
                if (DangVienGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đảng viên cần thêm hồ sơ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra nếu chọn nhiều hàng
                if (DangVienGridView.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Vui lòng chỉ chọn một đảng viên để thêm hồ sơ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = DangVienGridView.SelectedRows[0];
                var idObj = selectedRow.Cells["dangVienIDDataGridViewTextBoxColumn"].Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int dangVienID))
                {
                    MessageBox.Show("Không thể xác định đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin đảng viên
                var dangVien = _dangVienService.GetById(dangVienID);
                if (dangVien == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở dialog chọn file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Tất cả các file|*.*|PDF Files|*.pdf|Word Documents|*.doc;*.docx|Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Lưu file vào thư mục Server/HoSoDangVien
                            string relativePath = FileHelper.SaveHoSoDangVienFile(openFileDialog.FileName);

                            // Lấy thông tin file
                            FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                            string tenFile = fileInfo.Name;
                            string loaiFile = fileInfo.Extension.TrimStart('.').ToUpper();
                            long kichThuoc = fileInfo.Length;

                            // Lưu thông tin vào database
                            var result = _taiLieuHoSoService.Insert(
                                dangVienID,
                                tenFile,
                                relativePath,
                                loaiFile,
                                kichThuoc,
                                Environment.UserName
                            );

                            if (!string.IsNullOrEmpty(result.error))
                            {
                                MessageBox.Show($"Lỗi khi lưu thông tin file: {result.error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show("Thêm hồ sơ đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi thêm hồ sơ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hồ sơ đảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TimKiemBtn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            try
            {
                string searchText = TimTb.Text?.Trim();
                
                // Nếu không có từ khóa, load tất cả
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadData();
                    return;
                }

                // Lấy các filter từ ComboBox
                string doiTuong = GetSelectedValue(DoiTuongCb);
                string capBac = GetSelectedValue(CapBacCb);
                string trinhDo = GetSelectedValue(TrinhDoCb);
                string loaiDangVien = GetSelectedValue(LoaiDangVienCb);
                bool? trangThai = GetTrangThaiValue();

                // Phân tích và tìm kiếm thông minh
                var searchParams = AnalyzeSearchText(searchText);

                // Nếu tìm kiếm theo số điện thoại hoặc số thẻ đảng viên, cần filter sau
                bool needPostFilter = !string.IsNullOrEmpty(searchParams.SoDienThoai) || 
                                     !string.IsNullOrEmpty(searchParams.SoTheDangVien);

                // Ưu tiên sử dụng giá trị từ ComboBox, nếu không có thì dùng từ searchParams
                string finalCapBac = !string.IsNullOrEmpty(capBac) ? capBac : searchParams.CapBac;
                string finalTrinhDo = !string.IsNullOrEmpty(trinhDo) ? trinhDo : searchParams.TrinhDo;

                // Gọi service với các tham số đã phân tích
                _danhSachDangVien = _dangVienService.GetAll(
                    donViID: null,
                    hoTen: searchParams.HoTen,
                    soCCCD: searchParams.SoCCCD,
                    loaiDangVien: loaiDangVien,
                    doiTuong: doiTuong,
                    capBac: finalCapBac,
                    chucVu: searchParams.ChucVu,
                    queQuan: searchParams.QueQuan,
                    trinhDo: finalTrinhDo,
                    trangThai: trangThai ?? true // Mặc định là true nếu không chọn
                );

                // Filter thêm nếu cần (số điện thoại, số thẻ đảng viên)
                if (needPostFilter)
                {
                    _danhSachDangVien = _danhSachDangVien.Where(dv =>
                    {
                        bool match = true;
                        
                        if (!string.IsNullOrEmpty(searchParams.SoDienThoai))
                        {
                            string phone = dv.SoDienThoai?.Replace(" ", "").Replace("-", "").Replace(".", "") ?? "";
                            match = match && phone.Contains(searchParams.SoDienThoai);
                        }
                        
                        if (!string.IsNullOrEmpty(searchParams.SoTheDangVien))
                        {
                            match = match && (dv.SoTheDangVien?.Contains(searchParams.SoTheDangVien) ?? false);
                        }
                        
                        return match;
                    }).ToList();
                }

                CapNhatHienThi();

                // Hiển thị kết quả
                if (_danhSachDangVien.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy đảng viên nào với từ khóa: \"{searchText}\"", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Có thể hiển thị số lượng kết quả ở đây nếu cần
                    // MessageBox.Show($"Tìm thấy {_danhSachDangVien.Count} đảng viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Phân tích từ khóa tìm kiếm để xác định loại dữ liệu
        /// </summary>
        private SearchParams AnalyzeSearchText(string searchText)
        {
            var result = new SearchParams();

            // Loại bỏ khoảng trắng thừa
            searchText = searchText.Trim();

            // Kiểm tra nếu là số CCCD (12 chữ số)
            if (System.Text.RegularExpressions.Regex.IsMatch(searchText, @"^\d{12}$"))
            {
                result.SoCCCD = searchText;
                return result;
            }

            // Kiểm tra nếu là số điện thoại (10-11 chữ số, có thể có dấu cách hoặc dấu gạch ngang)
            string phonePattern = searchText.Replace(" ", "").Replace("-", "").Replace(".", "");
            if (System.Text.RegularExpressions.Regex.IsMatch(phonePattern, @"^0\d{9,10}$"))
            {
                // Tìm kiếm theo số điện thoại (cần filter sau khi lấy dữ liệu)
                result.SoDienThoai = phonePattern;
                return result;
            }

            // Kiểm tra nếu là số thẻ đảng viên (có thể có chữ và số)
            if (System.Text.RegularExpressions.Regex.IsMatch(searchText, @"^\d{6,12}$"))
            {
                result.SoTheDangVien = searchText;
                return result;
            }

            // Nếu chứa từ khóa "cấp bậc" hoặc "cb:"
            if (searchText.ToLower().Contains("cấp bậc:") || searchText.ToLower().StartsWith("cb:"))
            {
                var parts = searchText.Split(new[] { ':', '：' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    result.CapBac = parts[parts.Length - 1].Trim();
                    return result;
                }
            }

            // Nếu chứa từ khóa "chức vụ" hoặc "cv:"
            if (searchText.ToLower().Contains("chức vụ:") || searchText.ToLower().StartsWith("cv:"))
            {
                var parts = searchText.Split(new[] { ':', '：' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    result.ChucVu = parts[parts.Length - 1].Trim();
                    return result;
                }
            }

            // Nếu chứa từ khóa "quê quán" hoặc "qq:"
            if (searchText.ToLower().Contains("quê quán:") || searchText.ToLower().StartsWith("qq:"))
            {
                var parts = searchText.Split(new[] { ':', '：' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    result.QueQuan = parts[parts.Length - 1].Trim();
                    return result;
                }
            }

            // Nếu chứa từ khóa "trình độ" hoặc "td:"
            if (searchText.ToLower().Contains("trình độ:") || searchText.ToLower().StartsWith("td:"))
            {
                var parts = searchText.Split(new[] { ':', '：' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    result.TrinhDo = parts[parts.Length - 1].Trim();
                    return result;
                }
            }

            // Mặc định: tìm kiếm theo tên (có thể tìm trong nhiều trường)
            result.HoTen = searchText;
            result.SoCCCD = searchText; // Cũng tìm trong CCCD nếu có
            result.ChucVu = searchText; // Cũng tìm trong chức vụ
            result.CapBac = searchText; // Cũng tìm trong cấp bậc
            result.QueQuan = searchText; // Cũng tìm trong quê quán

            return result;
        }

        /// <summary>
        /// Class để lưu các tham số tìm kiếm
        /// </summary>
        private class SearchParams
        {
            public string HoTen { get; set; }
            public string SoCCCD { get; set; }
            public string SoDienThoai { get; set; }
            public string SoTheDangVien { get; set; }
            public string CapBac { get; set; }
            public string ChucVu { get; set; }
            public string QueQuan { get; set; }
            public string TrinhDo { get; set; }
        }

        private void TimTb_TextChanged(object sender, EventArgs e)
        {
            // Tìm kiếm real-time khi người dùng gõ (với delay để tránh query quá nhiều)
            // Có thể bật/tắt tính năng này tùy ý
            // PerformSearch();
        }

        private void ThemBtn_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền Create
            if (!AuthorizationHelper.HasPermission("DangVien", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm đảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Kiểm tra quyền Delete
            if (!AuthorizationHelper.HasPermission("DangVien", "Delete"))
            {
                MessageBox.Show("Bạn không có quyền xóa đảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
        /// Set tên đơn vị đi vào TextBox DonViDi (read-only) và lưu DonViID vào Tag
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

                // Tìm TextBox DonViDi trong form
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

        private void BindDonViComboBoxes(Control parent, List<DonViSimplified> donViData)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    // Chỉ bind ComboBox DonViDen (Đơn vị đến), không bind DonViDi nữa vì nó là TextBox read-only
                    // DonViID vẫn được bind cho các form khác
                    if (comboBox.Name == "DonViID" || comboBox.Name == "DonViDen")
                {
                    comboBox.DataSource = donViData;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";
                    }
                    // DonViDi không còn là ComboBox nữa, bỏ qua
                }

                if (control.HasChildren)
                    BindDonViComboBoxes(control, donViData);
            }
        }

        /// <summary>
        /// Lấy giá trị được chọn từ ComboBox (trả về null nếu không chọn)
        /// </summary>
        private string GetSelectedValue(MetroFramework.Controls.MetroComboBox comboBox)
        {
            if (comboBox.SelectedItem == null || comboBox.SelectedIndex == -1) return null;
            return comboBox.SelectedItem.ToString();
        }

        /// <summary>
        /// Lấy giá trị boolean từ ComboBox trạng thái
        /// </summary>
        private bool? GetTrangThaiValue()
        {
            if (TrangThaiCb.SelectedItem == null || TrangThaiCb.SelectedIndex == -1) return null;
            string selected = TrangThaiCb.SelectedItem.ToString();
            if (selected == "Đang hoạt động") return true;
            if (selected == "Không hoạt động") return false;
            return null;
        }

        private void DoiTuongCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformFilter();
        }

        private void CapBacCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformFilter();
        }

        private void TrinhDoCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformFilter();
        }

        private void LoaiDangVienCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformFilter();
        }

        private void TrangThaiCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformFilter();
        }

        /// <summary>
        /// Thực hiện lọc khi thay đổi ComboBox
        /// </summary>
        private void PerformFilter()
        {
            try
            {
                string searchText = TimTb.Text?.Trim();
                
                // Lấy các filter từ ComboBox
                string doiTuong = GetSelectedValue(DoiTuongCb);
                string capBac = GetSelectedValue(CapBacCb);
                string trinhDo = GetSelectedValue(TrinhDoCb);
                string loaiDangVien = GetSelectedValue(LoaiDangVienCb);
                bool? trangThai = GetTrangThaiValue();

                // Nếu có từ khóa tìm kiếm, gọi PerformSearch
                if (!string.IsNullOrEmpty(searchText))
                {
                    PerformSearch();
                    return;
                }

                // Nếu không có từ khóa, chỉ lọc theo ComboBox
                bool hasFilter = !string.IsNullOrEmpty(doiTuong) || 
                                !string.IsNullOrEmpty(capBac) || 
                                !string.IsNullOrEmpty(trinhDo) || 
                                !string.IsNullOrEmpty(loaiDangVien) ||
                                trangThai.HasValue;

                if (hasFilter)
                {
                    _danhSachDangVien = _dangVienService.GetAll(
                        donViID: null,
                        hoTen: null,
                        soCCCD: null,
                        loaiDangVien: loaiDangVien,
                        doiTuong: doiTuong,
                        capBac: capBac,
                        chucVu: null,
                        queQuan: null,
                        trinhDo: trinhDo,
                        trangThai: trangThai
                    );
                    CapNhatHienThi();
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền Create (chuyển sinh hoạt đảng)
            if (!AuthorizationHelper.HasPermission("ChuyenSinhHoatDang", "Create"))
            {
                MessageBox.Show("Bạn không có quyền chuyển sinh hoạt đảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Lấy row được chọn - ưu tiên SelectedRows, nếu không có thì lấy CurrentRow
                DataGridViewRow selectedRow = null;
                if (DangVienGridView.SelectedRows.Count > 0)
                {
                    selectedRow = DangVienGridView.SelectedRows[0];
                }
                else if (DangVienGridView.CurrentRow != null && DangVienGridView.CurrentRow.Index >= 0)
                {
                    selectedRow = DangVienGridView.CurrentRow;
                }

                if (selectedRow == null)
                {
                    MessageBox.Show("Vui lòng chọn đảng viên cần chuyển sinh hoạt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var idObj = selectedRow.Cells["dangVienIDDataGridViewTextBoxColumn"].Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int dangVienID))
                {
                    MessageBox.Show("Không thể xác định đảng viên! Vui lòng chọn một đảng viên từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin đảng viên
                var dangVien = _dangVienService.GetById(dangVienID);
                if (dangVien == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo form thêm chuyển sinh hoạt với đảng viên đã chọn
                var chuyenSinhHoat = new Models.ChuyenSinhHoatDang
                {
                    DangVienID = dangVienID,
                    DonViDi = dangVien.DonViID, // Đơn vị hiện tại của đảng viên
                    NgayChuyen = DateTime.Now,
                    TrangThai = "Chờ duyệt",
                    NguoiTao = Environment.UserName
                };

                FormThem formThem = new FormThem(typeof(Models.ChuyenSinhHoatDang));
                
                // Bind đơn vị cho ComboBox trước
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formThem, donViData);
                
                // Load dữ liệu vào form sau khi bind ComboBox
                formThem.LoadData(chuyenSinhHoat);
                
                // Set tên đảng viên vào TextBox DangVienID (read-only)
                SetDangVienNameToTextBox(formThem, dangVien.HoTen, dangVienID);
                
                // Set tên đơn vị đi vào TextBox DonViDi (read-only) từ thông tin đảng viên
                SetDonViDiNameToTextBox(formThem, dangVien.DonViID);

                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    var newChuyenSinhHoat = formThem.GetData() as Models.ChuyenSinhHoatDang;
                    
                    // Đảm bảo DangVienID và DonViDi được set đúng (từ Tag của TextBox hoặc từ giá trị gốc)
                    // FormHelper đã xử lý việc lấy từ Tag nếu TextBox là read-only
                    // Nhưng để đảm bảo, ta vẫn set lại từ giá trị gốc
                    newChuyenSinhHoat.DangVienID = dangVienID;
                    newChuyenSinhHoat.DonViDi = dangVien.DonViID; // Đơn vị đi luôn lấy từ thông tin đảng viên
                    
                    // Xử lý file quyết định: nếu FileQuyetDinh là đường dẫn file hợp lệ, lưu file
                    if (!string.IsNullOrWhiteSpace(newChuyenSinhHoat.FileQuyetDinh))
                    {
                        // Kiểm tra xem có phải là đường dẫn file đầy đủ không (người dùng vừa chọn file mới)
                        if (File.Exists(newChuyenSinhHoat.FileQuyetDinh))
                        {
                            try
                            {
                                // Copy file vào thư mục Server/ChuyenSinhHoat
                                string relativePath = FileHelper.SaveChuyenSinhHoatFile(newChuyenSinhHoat.FileQuyetDinh);
                                newChuyenSinhHoat.FileQuyetDinh = relativePath;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi khi lưu file quyết định: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        // Nếu không phải đường dẫn file hợp lệ, có thể là đường dẫn tương đối đã được lưu trước đó, giữ nguyên
                    }
                    
                    newChuyenSinhHoat.NguoiTao = Environment.UserName;
                    
                    var (id, error) = _chuyenSinhHoatDangService.Insert(newChuyenSinhHoat);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Thêm chuyển sinh hoạt đảng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Cập nhật DonViID của đảng viên sang DonViDen (nếu cần)
                    // Note: Theo requirement, việc cập nhật DonViID của đảng viên có thể được thực hiện sau khi duyệt
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chuyển sinh hoạt đảng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuatBtn_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền Export
            if (!AuthorizationHelper.HasPermission("DangVien", "Export"))
            {
                MessageBox.Show("Bạn không có quyền xuất dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_danhSachDangVien == null || _danhSachDangVien.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files|*.xlsx";
                    saveDialog.FileName = $"DanhSachDangVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    saveDialog.DefaultExt = "xlsx";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(_danhSachDangVien, saveDialog.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất danh sách đảng viên ra Excel theo format yêu cầu
        /// </summary>
        private void ExportToExcel(List<DangVienDTO> danhSachDangVien, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                // Group by DonViID để tạo sheet riêng cho mỗi đơn vị
                var groupedByDonVi = danhSachDangVien
                    .Where(dv => dv.TrangThai == true) // Chỉ lấy đảng viên đang hoạt động
                    .GroupBy(dv => new { dv.DonViID, dv.TenDonVi })
                    .OrderBy(g => g.Key.TenDonVi);

                foreach (var group in groupedByDonVi)
                {
                    // Tạo sheet với tên đơn vị (giới hạn 31 ký tự cho tên sheet Excel)
                    string sheetName = group.Key.TenDonVi ?? $"DonVi_{group.Key.DonViID}";
                    if (sheetName.Length > 31)
                        sheetName = sheetName.Substring(0, 31);
                    
                    // Xử lý ký tự không hợp lệ trong tên sheet
                    sheetName = sheetName.Replace(":", "").Replace("\\", "").Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");

                    var worksheet = workbook.Worksheets.Add(sheetName);

                    // Tạo header row
                    CreateHeaderRow(worksheet);

                    // Populate data
                    int row = 2; // Bắt đầu từ row 2 (row 1 là header)
                    foreach (var dangVien in group.OrderBy(dv => dv.HoTen))
                    {
                        PopulateDangVienRow(worksheet, row, dangVien);
                        row++;
                    }

                    // Auto-fit columns và set width
                    AutoFitColumns(worksheet);
                }

                // Lưu file
                workbook.SaveAs(filePath);
            }
        }

        /// <summary>
        /// Tạo header row cho Excel
        /// </summary>
        private void CreateHeaderRow(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "Họ và tên\nNgày, tháng, năm sinh\nSố thẻ đảng viên\nSố CCCD\nSố điện thoại";
            worksheet.Cell(1, 2).Value = "Cấp bậc\nChức vụ\nĐơn vị";
            worksheet.Cell(1, 3).Value = "Nhập ngũ\nNgày vào đảng\nChính thức";
            worksheet.Cell(1, 4).Value = "Tuổi đảng\nTuổi đời";
            worksheet.Cell(1, 5).Value = "Đối tượng; Giới tính";
            worksheet.Cell(1, 6).Value = "Quê quán";
            worksheet.Cell(1, 7).Value = "Trình Độ";
            worksheet.Cell(1, 8).Value = "Quá trình công tác";
            worksheet.Cell(1, 9).Value = "Hồ sơ gia đình";
            worksheet.Cell(1, 10).Value = "Khen thưởng";
            worksheet.Cell(1, 11).Value = "Kỷ luật";
            worksheet.Cell(1, 12).Value = "Tài Liệu Hồ sơ Đảng viên";
            worksheet.Cell(1, 13).Value = "Ghi chú";

            // Format header
            var headerRange = worksheet.Range(1, 1, 1, 13);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.FontSize = 10;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            headerRange.Style.Alignment.WrapText = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Set header row height
            worksheet.Row(1).Height = 80;
        }

        /// <summary>
        /// Populate data cho một đảng viên
        /// </summary>
        private void PopulateDangVienRow(IXLWorksheet worksheet, int row, DangVienDTO dangVien)
        {
            // Cột 1: Họ và tên, Ngày sinh, Số thẻ đảng viên, Số CCCD, Số điện thoại
            var col1 = new StringBuilder();
            col1.AppendLine(dangVien.HoTen ?? "");
            col1.AppendLine(dangVien.NgaySinh?.ToString("dd/MM/yyyy") ?? "");
            col1.AppendLine(dangVien.SoTheDangVien ?? "");
            col1.AppendLine(dangVien.SoCCCD ?? "");
            col1.AppendLine(dangVien.SoDienThoai ?? "");
            worksheet.Cell(row, 1).Value = col1.ToString().TrimEnd('\r', '\n');

            // Cột 2: Cấp bậc, Chức vụ, Đơn vị
            var col2 = new StringBuilder();
            col2.AppendLine(dangVien.CapBac ?? "");
            col2.AppendLine(dangVien.ChucVu ?? "");
            col2.AppendLine(dangVien.TenDonVi ?? "");
            worksheet.Cell(row, 2).Value = col2.ToString().TrimEnd('\r', '\n');

            // Cột 3: Nhập ngũ, Ngày vào đảng, Chính thức
            var col3 = new StringBuilder();
            // Note: Nhập ngũ không có trong DangVienDTO, có thể lấy từ QuanNhan nếu cần
            col3.AppendLine(""); // Nhập ngũ - để trống hoặc lấy từ bảng khác
            col3.AppendLine(dangVien.NgayVaoDang?.ToString("dd/MM/yyyy") ?? "");
            col3.AppendLine(dangVien.NgayChinhThuc?.ToString("dd/MM/yyyy") ?? "");
            worksheet.Cell(row, 3).Value = col3.ToString().TrimEnd('\r', '\n');

            // Cột 4: Tuổi đảng, Tuổi đời
            var col4 = new StringBuilder();
            col4.AppendLine(dangVien.TuoiDang?.ToString() ?? "");
            col4.AppendLine(dangVien.TuoiDoi?.ToString() ?? "");
            worksheet.Cell(row, 4).Value = col4.ToString().TrimEnd('\r', '\n');

            // Cột 5: Đối tượng; Giới tính
            worksheet.Cell(row, 5).Value = $"{dangVien.DoiTuong ?? ""}; {dangVien.GioiTinh ?? ""}";

            // Cột 6: Quê quán
            worksheet.Cell(row, 6).Value = dangVien.QueQuan ?? "";

            // Cột 7: Trình Độ
            worksheet.Cell(row, 7).Value = dangVien.TrinhDo ?? "";

            // Cột 8: Quá trình công tác
            worksheet.Cell(row, 8).Value = dangVien.QuaTrinhCongTac ?? "";

            // Cột 9: Hồ sơ gia đình
            worksheet.Cell(row, 9).Value = dangVien.HoSoGiaDinh ?? "";

            // Cột 10: Khen thưởng (lấy từ service)
            var khenThuongList = _khenThuongService.GetByDangVienID(dangVien.DangVienID);
            var col10 = new StringBuilder();
            foreach (var kt in khenThuongList)
            {
                col10.AppendLine(kt.HinhThuc ?? "");
            }
            worksheet.Cell(row, 10).Value = col10.ToString().TrimEnd('\r', '\n');

            // Cột 11: Kỷ luật (lấy từ service)
            var kyLuatList = _kyLuatService.GetByDangVienID(dangVien.DangVienID);
            var col11 = new StringBuilder();
            foreach (var kl in kyLuatList)
            {
                col11.AppendLine(kl.HinhThuc ?? "");
            }
            worksheet.Cell(row, 11).Value = col11.ToString().TrimEnd('\r', '\n');

            // Cột 12: Tài Liệu Hồ sơ Đảng viên
            var taiLieuList = _taiLieuHoSoService.GetByDangVienID(dangVien.DangVienID);
            var col12 = new StringBuilder();
            foreach (var tl in taiLieuList)
            {
                col12.AppendLine(tl.TenFile ?? "");
            }
            worksheet.Cell(row, 12).Value = col12.ToString().TrimEnd('\r', '\n');

            // Cột 13: Ghi chú (có thể lấy từ trường khác nếu có)
            worksheet.Cell(row, 13).Value = "";

            // Format cells: WrapText = true cho tất cả các cell
            var dataRange = worksheet.Range(row, 1, row, 13);
            dataRange.Style.Alignment.WrapText = true;
            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Set row height
            worksheet.Row(row).Height = 80;
        }

        /// <summary>
        /// Auto-fit columns và set width phù hợp
        /// </summary>
        private void AutoFitColumns(IXLWorksheet worksheet)
        {
            // Set column widths
            worksheet.Column(1).Width = 20;  // Cột 1: Thông tin cơ bản
            worksheet.Column(2).Width = 18;  // Cột 2: Cấp bậc, Chức vụ, Đơn vị
            worksheet.Column(3).Width = 18;  // Cột 3: Nhập ngũ, Ngày vào đảng, Chính thức
            worksheet.Column(4).Width = 12;  // Cột 4: Tuổi đảng, Tuổi đời
            worksheet.Column(5).Width = 15;  // Cột 5: Đối tượng; Giới tính
            worksheet.Column(6).Width = 20;  // Cột 6: Quê quán
            worksheet.Column(7).Width = 12;  // Cột 7: Trình Độ
            worksheet.Column(8).Width = 30;  // Cột 8: Quá trình công tác
            worksheet.Column(9).Width = 30;  // Cột 9: Hồ sơ gia đình
            worksheet.Column(10).Width = 20; // Cột 10: Khen thưởng
            worksheet.Column(11).Width = 20; // Cột 11: Kỷ luật
            worksheet.Column(12).Width = 25; // Cột 12: Tài Liệu Hồ sơ Đảng viên
            worksheet.Column(13).Width = 20; // Cột 13: Ghi chú
        }

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canCreate = AuthorizationHelper.HasPermission("DangVien", "Create");
            bool canUpdate = AuthorizationHelper.HasPermission("DangVien", "Update");
            bool canDelete = AuthorizationHelper.HasPermission("DangVien", "Delete");
            bool canExport = AuthorizationHelper.HasPermission("DangVien", "Export");

            // Enable/disable buttons
            if (ThemBtn != null) ThemBtn.Enabled = canCreate;
            if (XoaNhieuBtn != null) XoaNhieuBtn.Enabled = canDelete;
            if (XuatBtn != null) XuatBtn.Enabled = canExport;
            if (button1 != null) button1.Enabled = canCreate; // Thêm hồ sơ đảng viên

            // Disable edit trong DataGridView nếu không có quyền Update
            if (DangVienGridView != null)
            {
                foreach (DataGridViewColumn column in DangVienGridView.Columns)
                {
                    if (column.Name == "ChucNang")
                    {
                        // Chỉ cho phép edit nếu có quyền Update
                        column.ReadOnly = !canUpdate;
                    }
                }
            }
        }
    }
}
