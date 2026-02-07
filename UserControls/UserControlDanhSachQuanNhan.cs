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
using QuanLyDangVien.Helper;
using MetroFramework.Controls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using WinFormsControl = System.Windows.Forms.Control;
using DrawingColor = System.Drawing.Color;
using DrawingFont = System.Drawing.Font;

namespace QuanLyDangVien.UserControls
{
    public partial class UserControlDanhSachQuanNhan : UserControl
    {
        private QuanNhanService _quanNhanService;
        private DonViService _donViService;
        private List<QuanNhanDTO> _quanNhanList;

        public UserControlDanhSachQuanNhan()
        {
            InitializeComponent();
            _quanNhanService = new QuanNhanService();
            _donViService = new DonViService();
            InitializeData();
            ApplyPermissions();
        }

        private void InitializeData()
        {
            LoadQuanNhanData(); // SetupDataGridView sẽ được gọi trong LoadQuanNhanData
        }

        private void LoadQuanNhanData()
        {
            try
            {
                // Tìm DonViID từ tên đơn vị nhập vào
                int? donViID = null;
                string tenDonVi = txtDonVi.Text.Trim();
                if (!string.IsNullOrEmpty(tenDonVi))
                {
                    // Tìm đơn vị theo tên (tìm chính xác hoặc chứa, không phân biệt hoa thường)
                    var donViList = _donViService.GetDonViCapThapNhat();
                    string tenDonViLower = tenDonVi.ToLower();
                    var donVi = donViList.FirstOrDefault(d => 
                        d.TenDonVi != null && (
                            d.TenDonVi.ToLower().Equals(tenDonViLower) ||
                            d.TenDonVi.ToLower().Contains(tenDonViLower)));
                    
                    if (donVi != null && donVi.DonViID > 0)
                    {
                        donViID = donVi.DonViID;
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
                dgvQuanNhan.RowHeadersVisible = false;
                dgvQuanNhan.BackgroundColor = DrawingColor.White;
                dgvQuanNhan.BorderStyle = BorderStyle.None;
                dgvQuanNhan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvQuanNhan.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvQuanNhan.EnableHeadersVisualStyles = false;
                dgvQuanNhan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvQuanNhan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                
                // Header styling - màu xanh lục
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.BackColor = DrawingColor.FromArgb(40, 167, 69); // Màu xanh lục
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.ForeColor = DrawingColor.White;
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.Font = new DrawingFont("Segoe UI", 9F, FontStyle.Bold);
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.SelectionBackColor = DrawingColor.FromArgb(40, 167, 69);
                dgvQuanNhan.ColumnHeadersDefaultCellStyle.SelectionForeColor = DrawingColor.White;
                dgvQuanNhan.ColumnHeadersHeight = 100; // Tăng chiều cao để hiển thị đầy đủ headerText nhiều dòng
                dgvQuanNhan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                
                // Row styling
                dgvQuanNhan.DefaultCellStyle.Font = new DrawingFont("Segoe UI", 9F);
                dgvQuanNhan.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
                dgvQuanNhan.DefaultCellStyle.SelectionBackColor = DrawingColor.FromArgb(200, 255, 200); // Màu xanh lục nhạt khi chọn
                dgvQuanNhan.DefaultCellStyle.SelectionForeColor = DrawingColor.Black;
                dgvQuanNhan.AlternatingRowsDefaultCellStyle.BackColor = DrawingColor.FromArgb(245, 245, 245);
                dgvQuanNhan.AlternatingRowsDefaultCellStyle.SelectionBackColor = DrawingColor.FromArgb(200, 255, 200);
                dgvQuanNhan.AlternatingRowsDefaultCellStyle.SelectionForeColor = DrawingColor.Black;
                
                // Tăng chiều cao hàng mặc định và thêm padding
                dgvQuanNhan.RowTemplate.Height = 60;
                dgvQuanNhan.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
                
                // Allow column resizing
                dgvQuanNhan.AllowUserToResizeColumns = true;
                
                // Handle DataError to prevent dialog from showing
                dgvQuanNhan.DataError += (s, e) =>
                {
                    e.ThrowException = false;
                };

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
                    HeaderText = "Họ tên\nNgày sinh\nSHSQ\nBHYT\nCCCD",
                    Width = 200,
                    MinimumWidth = 180
                };
                dgvQuanNhan.Columns.Add(colThongTinCoBan);

                // 2. Cấp bậc / Chức vụ / Đơn vị
                var colCapBacChucVu = new MultiLineDataGridViewColumn
                {
                    Name = "CapBacChucVu",
                    HeaderText = "Cấp bậc\nChức vụ\nĐơn vị",
                    Width = 150,
                    MinimumWidth = 130
                };
                dgvQuanNhan.Columns.Add(colCapBacChucVu);

                // 3. Nhập ngũ / Ngày vào đảng / Số thẻ Đảng / Đoàn
                var colThongTinDang = new MultiLineDataGridViewColumn
                {
                    Name = "ThongTinDang",
                    HeaderText = "Nhập ngũ\nVào đảng\nSố thẻ Đảng\nĐoàn",
                    Width = 180,
                    MinimumWidth = 160
                };
                dgvQuanNhan.Columns.Add(colThongTinDang);

                // 4. Dân tộc / Tôn giáo
                var colDanTocTonGiao = new MultiLineDataGridViewColumn
                {
                    Name = "DanTocTonGiao",
                    HeaderText = "Dân tộc\nTôn giáo",
                    Width = 120,
                    MinimumWidth = 100
                };
                dgvQuanNhan.Columns.Add(colDanTocTonGiao);

                // 5. Sức khỏe / Nhóm máu
                var colSucKhoeNhomMau = new MultiLineDataGridViewColumn
                {
                    Name = "SucKhoeNhomMau",
                    HeaderText = "Sức khỏe\nNhóm máu",
                    Width = 120,
                    MinimumWidth = 100
                };
                dgvQuanNhan.Columns.Add(colSucKhoeNhomMau);

                // 6. Họ tên cha năm sinh / Họ tên mẹ năm sinh / Họ tên vợ, con năm sinh
                var colGiaDinh = new MultiLineDataGridViewColumn
                {
                    Name = "GiaDinh",
                    HeaderText = "Cha\nMẹ\nVợ (Con)",
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
                    HeaderText = "Quê quán\nNơi ở",
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
            txtDonVi.Clear();
            txtHoTen.Clear();
            txtSoCCCD.Clear();
            cboCapBac.SelectedIndex = -1;
            txtChucVu.Clear();
            LoadQuanNhanData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("QuanNhan", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm quân nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (!AuthorizationHelper.HasPermission("QuanNhan", "Update"))
            {
                MessageBox.Show("Bạn không có quyền sửa quân nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (!AuthorizationHelper.HasPermission("QuanNhan", "Delete"))
            {
                MessageBox.Show("Bạn không có quyền xóa quân nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (!AuthorizationHelper.HasPermission("QuanNhan", "Export"))
            {
                MessageBox.Show("Bạn không có quyền xuất dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (dgvQuanNhan.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"DanhSachQuanNhan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(saveFileDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất dữ liệu từ DataGridView ra Excel
        /// </summary>
        private void ExportToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh sách quân nhân");

                // Đếm số cột visible
                int visibleColCount = dgvQuanNhan.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible);

                // Tiêu đề
                worksheet.Cell(1, 1).Value = "DANH SÁCH QUÂN NHÂN";
                var titleRange = worksheet.Range(1, 1, 1, visibleColCount);
                titleRange.Merge();
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.FontSize = 16;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Row(1).Height = 30;

                // Header row
                int headerRow = 3;
                int headerColIndex = 0;
                for (int col = 0; col < dgvQuanNhan.Columns.Count; col++)
                {
                    var column = dgvQuanNhan.Columns[col];
                    if (column.Visible)
                    {
                        worksheet.Cell(headerRow, headerColIndex + 1).Value = column.HeaderText;
                        headerColIndex++;
                    }
                }

                // Style header
                var headerRange = worksheet.Range(headerRow, 1, headerRow, visibleColCount);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Font.FontSize = 10;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                headerRange.Style.Alignment.WrapText = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Row(headerRow).Height = 80;

                // Data rows
                int dataRow = headerRow + 1;
                foreach (DataGridViewRow row in dgvQuanNhan.Rows)
                {
                    if (row.IsNewRow) continue;

                    int colIndex = 0;
                    for (int col = 0; col < dgvQuanNhan.Columns.Count; col++)
                    {
                        var column = dgvQuanNhan.Columns[col];
                        if (column.Visible)
                        {
                            // Lấy giá trị đã format từ cell (hỗ trợ MultiLineDataGridViewCell)
                            var cell = row.Cells[col];
                            string cellValue = "";
                            if (cell.FormattedValue != null)
                            {
                                cellValue = cell.FormattedValue.ToString();
                            }
                            else if (cell.Value != null)
                            {
                                cellValue = cell.Value.ToString();
                            }
                            worksheet.Cell(dataRow, colIndex + 1).Value = cellValue;
                            colIndex++;
                        }
                    }

                    // Style data row
                    var dataRange = worksheet.Range(dataRow, 1, dataRow, visibleColCount);
                    dataRange.Style.Alignment.WrapText = true;
                    dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Row(dataRow).Height = 80;

                    dataRow++;
                }

                // Auto-fit columns
                int colNum = 1;
                for (int col = 0; col < dgvQuanNhan.Columns.Count; col++)
                {
                    var column = dgvQuanNhan.Columns[col];
                    if (column.Visible)
                    {
                        worksheet.Column(colNum).Width = Math.Max(10, Math.Min(30, column.Width / 7.0));
                        colNum++;
                    }
                }

                workbook.SaveAs(filePath);
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
            BindDonViComboBoxes((WinFormsControl)form, donViData);
        }

        /// <summary>
        /// Bind DonVi data vào tất cả ComboBox có tên "DonViID" trong form
        /// </summary>
        private void BindDonViComboBoxes(WinFormsControl parent, List<DonViSimplified> donViData)
        {
            foreach (WinFormsControl control in parent.Controls)
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

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canCreate = AuthorizationHelper.HasPermission("QuanNhan", "Create");
            bool canUpdate = AuthorizationHelper.HasPermission("QuanNhan", "Update");
            bool canDelete = AuthorizationHelper.HasPermission("QuanNhan", "Delete");
            bool canExport = AuthorizationHelper.HasPermission("QuanNhan", "Export");

            if (btnThem != null) btnThem.Enabled = canCreate;
            if (btnSua != null) btnSua.Enabled = canUpdate;
            if (btnXoa != null) btnXoa.Enabled = canDelete;
            if (btnXuatExcel != null) btnXuatExcel.Enabled = canExport;
        }
    }
}
