using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using QuanLyDangVien.Services;
using QuanLyDangVien.Models;
using QuanLyDangVien.DTOs;

namespace QuanLyDangVien.UserControls
{
    public partial class UserControlDanhSachQuanNhan : UserControl
    {
        private QuanNhanService _quanNhanService;
        private DonViService _donViService;
        private List<QuanNhanDTO> _quanNhanList;
        private List<DonViSimplified> _donViList;

        public UserControlDanhSachQuanNhan()
        {
            InitializeComponent();
            _quanNhanService = new QuanNhanService();
            _donViService = new DonViService();
            InitializeData();
        }

        private void InitializeData()
        {
            LoadDonViComboBox();
            LoadQuanNhanData(); // SetupDataGridView sẽ được gọi trong LoadQuanNhanData
        }

        private void LoadDonViComboBox()
        {
            try
            {
                // Sử dụng GetDonViData() để lấy danh sách đơn giản cho ComboBox
                _donViList = _donViService.GetDonViData();
                
                if (_donViList == null || _donViList.Count == 0)
                {
                    // Nếu không có dữ liệu, tạo một item mặc định
                    _donViList = new List<DonViSimplified>
                    {
                        new DonViSimplified { DonViID = 0, TenDonVi = "Chưa có đơn vị nào" }
                    };
                }
                
                // Đảm bảo ComboBox được reset trước khi bind
                cboDonVi.DataSource = null;
                cboDonVi.Items.Clear();
                
                // Đảm bảo ComboBox ở chế độ DropDownList và FormattingEnabled = true
                cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
                cboDonVi.FormattingEnabled = true;
                
                // Bind dữ liệu
                cboDonVi.DataSource = _donViList;
                cboDonVi.DisplayMember = "TenDonVi";
                cboDonVi.ValueMember = "DonViID";
                
                // Refresh để đảm bảo dữ liệu được hiển thị
                cboDonVi.Refresh();
                cboDonVi.SelectedIndex = -1;
                
                // Debug: Kiểm tra xem dữ liệu đã được load chưa
                System.Diagnostics.Debug.WriteLine($"Đã load {_donViList.Count} đơn vị vào ComboBox");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadDonViComboBox: {ex.Message}");
                // Tạo danh sách rỗng để tránh lỗi
                _donViList = new List<DonViSimplified>();
            }
        }

        private void LoadQuanNhanData()
        {
            try
            {
                // Xử lý SelectedValue có thể trả về DonViSimplified object
                int? donViID = null;
                if (cboDonVi.SelectedValue != null)
                {
                    if (cboDonVi.SelectedValue is DonViSimplified donVi)
                    {
                        donViID = donVi.DonViID > 0 ? (int?)donVi.DonViID : null;
                    }
                    else if (cboDonVi.SelectedValue is int)
                    {
                        donViID = (int?)cboDonVi.SelectedValue;
                    }
                    else if (int.TryParse(cboDonVi.SelectedValue.ToString(), out int id))
                    {
                        donViID = id > 0 ? (int?)id : null;
                    }
                }
                string hoTen = txtHoTen.Text.Trim();
                string soCCCD = txtSoCCCD.Text.Trim();
                string capBac = cboCapBac.SelectedItem?.ToString();
                string chucVu = txtChucVu.Text.Trim();

                _quanNhanList = _quanNhanService.GetAll(donViID, hoTen, soCCCD, capBac, chucVu, true);
                
                // Setup DataGridView trước khi bind
                SetupDataGridView();
                
                // Bind data
                dgvQuanNhan.DataSource = _quanNhanList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu quân nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                // Remove existing event handlers if any (tránh duplicate)
                dgvQuanNhan.CellFormatting -= DgvQuanNhan_CellFormatting;
                dgvQuanNhan.DataBindingComplete -= DgvQuanNhan_DataBindingComplete;

                // Clear existing columns
                dgvQuanNhan.Columns.Clear();

                // Configure DataGridView
                dgvQuanNhan.AutoGenerateColumns = false;
                dgvQuanNhan.AllowUserToAddRows = false;
                dgvQuanNhan.AllowUserToDeleteRows = false;
                dgvQuanNhan.ReadOnly = true;
                dgvQuanNhan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvQuanNhan.MultiSelect = false;
                dgvQuanNhan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvQuanNhan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvQuanNhan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dgvQuanNhan.DefaultCellStyle.Font = new Font("Arial", 9);
                
                // Tăng chiều cao hàng mặc định và thêm padding
                dgvQuanNhan.RowTemplate.Height = 120; // Tăng từ 100 lên 120
                dgvQuanNhan.DefaultCellStyle.Padding = new Padding(5, 8, 5, 8); // Tăng padding
                dgvQuanNhan.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 8, 5, 8);

                // Column for TT (ID) - hiển thị ở đầu tiên
                dgvQuanNhan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "QuanNhanID",
                    DataPropertyName = "QuanNhanID",
                    HeaderText = "TT",
                    Width = 60,
                    MinimumWidth = 50,
                    Visible = true,
                    DisplayIndex = 0 // Đặt ở vị trí đầu tiên
                });

                // 1. Họ tên / Ngày tháng năm sinh / SHSQ / Số thẻ BHYT / Số CCCD
                var colThongTinCoBan = new MultiLineDataGridViewColumn
                {
                    Name = "ThongTinCoBan",
                    HeaderText = "Họ tên / Ngày sinh / SHSQ / BHYT / CCCD",
                    Width = 200,
                    MinimumWidth = 180
                };
                dgvQuanNhan.Columns.Add(colThongTinCoBan);

                // 2. Cấp bậc / Chức vụ / Đơn vị
                var colCapBacChucVu = new MultiLineDataGridViewColumn
                {
                    Name = "CapBacChucVu",
                    HeaderText = "Cấp bậc / Chức vụ / Đơn vị",
                    Width = 150,
                    MinimumWidth = 130
                };
                dgvQuanNhan.Columns.Add(colCapBacChucVu);

                // 3. Nhập ngũ / Ngày vào đảng / Số thẻ Đảng / Đoàn
                var colThongTinDang = new MultiLineDataGridViewColumn
                {
                    Name = "ThongTinDang",
                    HeaderText = "Nhập ngũ / Vào đảng / Số thẻ Đảng / Đoàn",
                    Width = 180,
                    MinimumWidth = 160
                };
                dgvQuanNhan.Columns.Add(colThongTinDang);

                // 4. Dân tộc / Tôn giáo
                var colDanTocTonGiao = new MultiLineDataGridViewColumn
                {
                    Name = "DanTocTonGiao",
                    HeaderText = "Dân tộc / Tôn giáo",
                    Width = 120,
                    MinimumWidth = 100
                };
                dgvQuanNhan.Columns.Add(colDanTocTonGiao);

                // 5. Sức khỏe / Nhóm máu
                var colSucKhoeNhomMau = new MultiLineDataGridViewColumn
                {
                    Name = "SucKhoeNhomMau",
                    HeaderText = "Sức khỏe / Nhóm máu",
                    Width = 120,
                    MinimumWidth = 100
                };
                dgvQuanNhan.Columns.Add(colSucKhoeNhomMau);

                // 6. Họ tên cha năm sinh / Họ tên mẹ năm sinh / Họ tên vợ, con năm sinh
                var colGiaDinh = new MultiLineDataGridViewColumn
                {
                    Name = "GiaDinh",
                    HeaderText = "Cha / Mẹ / Vợ (Con)",
                    Width = 200,
                    MinimumWidth = 180
                };
                dgvQuanNhan.Columns.Add(colGiaDinh);

                // 7. Nghề nghiệp
                dgvQuanNhan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NgheNghiepChaMe",
                    DataPropertyName = "NgheNghiepChaMe",
                    HeaderText = "Nghề nghiệp",
                    Width = 100,
                    MinimumWidth = 80
                });

                // 8. Mấy anh chị em
                dgvQuanNhan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "MayAnhChiEm",
                    DataPropertyName = "MayAnhChiEm",
                    HeaderText = "Anh chị em",
                    Width = 50,
                    MinimumWidth = 40
                });

                // 9. Quê quán / Nơi ở hiện nay
                var colQueQuanNoiO = new MultiLineDataGridViewColumn
                {
                    Name = "QueQuanNoiO",
                    HeaderText = "Quê quán / Nơi ở",
                    Width = 200,
                    MinimumWidth = 180
                };
                dgvQuanNhan.Columns.Add(colQueQuanNoiO);

                // 10. Khi cần báo tin
                dgvQuanNhan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "KhiCanBaoTin",
                    DataPropertyName = "KhiCanBaoTin",
                    HeaderText = "Khi cần báo tin",
                    Width = 150,
                    MinimumWidth = 130
                });

                // 11. Ghi chú
                var colGhiChu = new MultiLineDataGridViewColumn
                {
                    Name = "GhiChu",
                    DataPropertyName = "GhiChu",
                    HeaderText = "Ghi chú",
                    Width = 200,
                    MinimumWidth = 180
                };
                dgvQuanNhan.Columns.Add(colGhiChu);

                // Handle CellFormatting to populate merged columns
                dgvQuanNhan.CellFormatting += DgvQuanNhan_CellFormatting;
                
                // Đảm bảo cột TT luôn hiển thị và ở vị trí đầu tiên sau khi bind data
                dgvQuanNhan.DataBindingComplete += DgvQuanNhan_DataBindingComplete;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SetupDataGridView error: {ex.Message}");
            }
        }

        private void DgvQuanNhan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Đảm bảo cột TT luôn hiển thị và ở vị trí đầu tiên
            if (dgvQuanNhan.Columns["QuanNhanID"] != null)
            {
                var colTT = dgvQuanNhan.Columns["QuanNhanID"];
                colTT.Visible = true;
                colTT.HeaderText = "TT";
                colTT.DisplayIndex = 0;
                if (colTT.Width < 50)
                {
                    colTT.Width = 60;
                }
            }
        }

        private void DgvQuanNhan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _quanNhanList?.Count)
                return;

            var quanNhan = _quanNhanList[e.RowIndex];
            var column = dgvQuanNhan.Columns[e.ColumnIndex];

            try
            {
                switch (column.Name)
                {
                    case "ThongTinCoBan":
                        e.Value = FormatThongTinCoBan(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "CapBacChucVu":
                        e.Value = FormatCapBacChucVu(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "ThongTinDang":
                        e.Value = FormatThongTinDang(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "DanTocTonGiao":
                        e.Value = FormatDanTocTonGiao(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "SucKhoeNhomMau":
                        e.Value = FormatSucKhoeNhomMau(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "GiaDinh":
                        e.Value = FormatGiaDinh(quanNhan);
                        e.FormattingApplied = true;
                        break;

                    case "QueQuanNoiO":
                        e.Value = FormatQueQuanNoiO(quanNhan);
                        e.FormattingApplied = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CellFormatting error for {column.Name}: {ex.Message}");
            }
        }

        private string FormatThongTinCoBan(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.HoTen))
                lines.Add($"Họ tên: {qn.HoTen}");
            if (qn.NgaySinh.HasValue)
                lines.Add($"Ngày sinh: {qn.NgaySinh.Value:dd/MM/yyyy}");
            if (!string.IsNullOrEmpty(qn.SHSQ))
                lines.Add($"SHSQ: {qn.SHSQ}");
            if (!string.IsNullOrEmpty(qn.SoTheBHYT))
                lines.Add($"BHYT: {qn.SoTheBHYT}");
            if (!string.IsNullOrEmpty(qn.SoCCCD))
                lines.Add($"CCCD: {qn.SoCCCD}");
            return string.Join("\n", lines);
        }

        private string FormatCapBacChucVu(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.CapBac))
                lines.Add($"Cấp bậc: {qn.CapBac}");
            if (!string.IsNullOrEmpty(qn.ChucVu))
                lines.Add($"Chức vụ: {qn.ChucVu}");
            if (!string.IsNullOrEmpty(qn.TenDonVi))
                lines.Add($"Đơn vị: {qn.TenDonVi}");
            return string.Join("\n", lines);
        }

        private string FormatThongTinDang(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (qn.NhapNgu.HasValue)
                lines.Add($"Nhập ngũ: {qn.NhapNgu.Value:dd/MM/yyyy}");
            if (qn.NgayVaoDang.HasValue)
                lines.Add($"Vào Đảng: {qn.NgayVaoDang.Value:dd/MM/yyyy}");
            if (!string.IsNullOrEmpty(qn.SoTheDang))
                lines.Add($"Số thẻ Đảng: {qn.SoTheDang}");
            if (!string.IsNullOrEmpty(qn.Doan))
                lines.Add($"Đoàn: {qn.Doan}");
            return string.Join("\n", lines);
        }

        private string FormatDanTocTonGiao(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.DanToc))
                lines.Add($"Dân tộc: {qn.DanToc}");
            if (!string.IsNullOrEmpty(qn.TonGiao))
                lines.Add($"Tôn giáo: {qn.TonGiao}");
            return string.Join("\n", lines);
        }

        private string FormatSucKhoeNhomMau(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.SucKhoe))
                lines.Add($"Sức khỏe: {qn.SucKhoe}");
            if (!string.IsNullOrEmpty(qn.NhomMau))
                lines.Add($"Nhóm máu: {qn.NhomMau}");
            return string.Join("\n", lines);
        }

        private string FormatGiaDinh(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.HoTenChaNamSinh))
                lines.Add($"Cha: {qn.HoTenChaNamSinh}");
            if (!string.IsNullOrEmpty(qn.HoTenMeNamSinh))
                lines.Add($"Mẹ: {qn.HoTenMeNamSinh}");
            if (!string.IsNullOrEmpty(qn.HoTenVoConNamSinh))
                lines.Add($"Vợ (Con): {qn.HoTenVoConNamSinh}");
            return string.Join("\n", lines);
        }

        private string FormatQueQuanNoiO(QuanNhanDTO qn)
        {
            var lines = new List<string>();
            if (!string.IsNullOrEmpty(qn.QueQuan))
                lines.Add($"Quê quán: {qn.QueQuan}");
            if (!string.IsNullOrEmpty(qn.NoiO))
                lines.Add($"Nơi ở: {qn.NoiO}");
            return string.Join("\n", lines);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadQuanNhanData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboDonVi.SelectedIndex = -1;
            txtHoTen.Clear();
            txtSoCCCD.Clear();
            cboCapBac.SelectedIndex = -1;
            txtChucVu.Clear();
            LoadQuanNhanData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var formThem = new FormThem(typeof(QuanNhan));
                
                // Load danh sách đơn vị vào form
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formThem, donViData);
                
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    var quanNhan = (QuanNhan)formThem.GetData();
                    quanNhan.NguoiTao = Environment.UserName; // Hoặc lấy từ session
                    
                    var (id, error) = _quanNhanService.Insert(quanNhan);
                    if (string.IsNullOrEmpty(error))
                    {
                        LoadQuanNhanData();
                        MessageBox.Show("Thêm quân nhân thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi thêm quân nhân: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm quân nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQuanNhan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quân nhân cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedQuanNhan = (QuanNhanDTO)dgvQuanNhan.SelectedRows[0].DataBoundItem;
                
                // Lấy dữ liệu đầy đủ từ database
                var quanNhanFull = _quanNhanService.GetById(selectedQuanNhan.QuanNhanID);
                if (quanNhanFull == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin quân nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert DTO to Model
                var quanNhanModel = new QuanNhan
                {
                    QuanNhanID = quanNhanFull.QuanNhanID,
                    DonViID = quanNhanFull.DonViID,
                    HoTen = quanNhanFull.HoTen,
                    NgaySinh = quanNhanFull.NgaySinh,
                    SHSQ = quanNhanFull.SHSQ,
                    SoTheBHYT = quanNhanFull.SoTheBHYT,
                    SoCCCD = quanNhanFull.SoCCCD,
                    CapBac = quanNhanFull.CapBac,
                    ChucVu = quanNhanFull.ChucVu,
                    NhapNgu = quanNhanFull.NhapNgu,
                    NgayVaoDang = quanNhanFull.NgayVaoDang,
                    SoTheDang = quanNhanFull.SoTheDang,
                    Doan = quanNhanFull.Doan,
                    DanToc = quanNhanFull.DanToc,
                    TonGiao = quanNhanFull.TonGiao,
                    SucKhoe = quanNhanFull.SucKhoe,
                    NhomMau = quanNhanFull.NhomMau,
                    HoTenChaNamSinh = quanNhanFull.HoTenChaNamSinh,
                    HoTenMeNamSinh = quanNhanFull.HoTenMeNamSinh,
                    HoTenVoConNamSinh = quanNhanFull.HoTenVoConNamSinh,
                    NgheNghiepChaMe = quanNhanFull.NgheNghiepChaMe,
                    MayAnhChiEm = quanNhanFull.MayAnhChiEm,
                    QueQuan = quanNhanFull.QueQuan,
                    NoiO = quanNhanFull.NoiO,
                    KhiCanBaoTin = quanNhanFull.KhiCanBaoTin,
                    GhiChu = quanNhanFull.GhiChu,
                    AnhDaiDien = quanNhanFull.AnhDaiDien,
                    TrangThai = quanNhanFull.TrangThai,
                    NgayTao = quanNhanFull.NgayTao,
                    NguoiTao = quanNhanFull.NguoiTao
                };

                var formSua = new FormSua(quanNhanModel);
                
                // Load danh sách đơn vị vào form
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formSua, donViData);
                
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedQuanNhan = (QuanNhan)formSua.GetData();
                    updatedQuanNhan.NguoiTao = Environment.UserName; // Hoặc lấy từ session
                    
                    var (success, error) = _quanNhanService.Update(updatedQuanNhan);
                    if (success)
                    {
                        LoadQuanNhanData();
                        MessageBox.Show("Sửa thông tin quân nhân thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi sửa quân nhân: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa quân nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvQuanNhan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quân nhân cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedQuanNhan = (QuanNhanDTO)dgvQuanNhan.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa quân nhân '{selectedQuanNhan.HoTen}'?", 
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var (success, error) = _quanNhanService.Delete(selectedQuanNhan.QuanNhanID);
                    if (success)
                    {
                        LoadQuanNhanData();
                        MessageBox.Show("Xóa quân nhân thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi xóa quân nhân: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa quân nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvQuanNhan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quân nhân cần xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedQuanNhan = (QuanNhanDTO)dgvQuanNhan.SelectedRows[0].DataBoundItem;
                
                // Lấy dữ liệu đầy đủ từ database
                var quanNhanFull = _quanNhanService.GetById(selectedQuanNhan.QuanNhanID);
                if (quanNhanFull == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin quân nhân!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert DTO to Model
                var quanNhanModel = new QuanNhan
                {
                    QuanNhanID = quanNhanFull.QuanNhanID,
                    DonViID = quanNhanFull.DonViID,
                    HoTen = quanNhanFull.HoTen,
                    NgaySinh = quanNhanFull.NgaySinh,
                    SHSQ = quanNhanFull.SHSQ,
                    SoTheBHYT = quanNhanFull.SoTheBHYT,
                    SoCCCD = quanNhanFull.SoCCCD,
                    CapBac = quanNhanFull.CapBac,
                    ChucVu = quanNhanFull.ChucVu,
                    NhapNgu = quanNhanFull.NhapNgu,
                    NgayVaoDang = quanNhanFull.NgayVaoDang,
                    SoTheDang = quanNhanFull.SoTheDang,
                    Doan = quanNhanFull.Doan,
                    DanToc = quanNhanFull.DanToc,
                    TonGiao = quanNhanFull.TonGiao,
                    SucKhoe = quanNhanFull.SucKhoe,
                    NhomMau = quanNhanFull.NhomMau,
                    HoTenChaNamSinh = quanNhanFull.HoTenChaNamSinh,
                    HoTenMeNamSinh = quanNhanFull.HoTenMeNamSinh,
                    HoTenVoConNamSinh = quanNhanFull.HoTenVoConNamSinh,
                    NgheNghiepChaMe = quanNhanFull.NgheNghiepChaMe,
                    MayAnhChiEm = quanNhanFull.MayAnhChiEm,
                    QueQuan = quanNhanFull.QueQuan,
                    NoiO = quanNhanFull.NoiO,
                    KhiCanBaoTin = quanNhanFull.KhiCanBaoTin,
                    GhiChu = quanNhanFull.GhiChu,
                    AnhDaiDien = quanNhanFull.AnhDaiDien,
                    TrangThai = quanNhanFull.TrangThai,
                    NgayTao = quanNhanFull.NgayTao,
                    NguoiTao = quanNhanFull.NguoiTao
                };

                var formChiTiet = new FormXemChiTiet(quanNhanModel);
                formChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xem chi tiết quân nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_quanNhanList == null || _quanNhanList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"DanhSachQuanNhan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = _quanNhanService.ExportToExcel(_quanNhanList, saveFileDialog.FileName);
                    MessageBox.Show(result, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_quanNhanList == null || _quanNhanList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = _quanNhanService.PrintList(_quanNhanList);
                MessageBox.Show(result, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi in danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvQuanNhan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnXemChiTiet_Click(sender, e);
            }
        }

        /// <summary>
        /// Refresh DonVi data vào form (giống UserControlQuanLyDangVien)
        /// </summary>
        public void RefreshDonViData(Form form, List<DonViSimplified> donViData)
        {
            if (form == null) return;
            BindDonViComboBoxes(form, donViData);
        }

        /// <summary>
        /// Bind DonVi data vào tất cả ComboBox có tên "DonViID" trong form
        /// </summary>
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
    }
}
