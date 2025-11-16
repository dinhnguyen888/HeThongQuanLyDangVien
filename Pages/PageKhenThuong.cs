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
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using QuanLyDangVien.DTOs;

namespace QuanLyDangVien.Pages
{
    public partial class PageKhenThuong : UserControl
    {
        private KhenThuongCaNhanService _khenThuongService;
        private DangVienService _dangVienService;
        private List<DangVienDTO> _danhSachDangVien;
        private DangVienDTO _dangVienSelected;

        // Danh sách hình thức khen thưởng
        private List<string> _danhHieuThiDua = new List<string>
        {
            "Chiến sĩ thi đua toàn quốc",
            "Chiến sĩ thi đua cấp bộ, ngành, tỉnh, đoàn thể trung ương",
            "Chiến sĩ thi đua cơ sở",
            "Lao động tiên tiến",
            "Chiến sĩ tiên tiến"
        };

        private List<string> _huanChuong = new List<string>
        {
            "Huân chương Sao vàng",
            "Huân chương Hồ Chí Minh",
            "Huân chương Độc lập hạng nhất",
            "Huân chương Độc lập hạng nhì",
            "Huân chương Độc lập hạng ba",
            "Huân chương Quân công hạng nhất",
            "Huân chương Quân công hạng nhì",
            "Huân chương Quân công hạng ba",
            "Huân chương Lao động hạng nhất",
            "Huân chương Lao động hạng nhì",
            "Huân chương Lao động hạng ba",
            "Huân chương Bảo vệ Tổ quốc hạng nhất",
            "Huân chương Bảo vệ Tổ quốc hạng nhì",
            "Huân chương Bảo vệ Tổ quốc hạng ba",
            "Huân chương Chiến công hạng nhất",
            "Huân chương Chiến công hạng nhì",
            "Huân chương Chiến công hạng ba",
            "Huân chương Đại đoàn kết dân tộc",
            "Huân chương Dũng cảm",
            "Huân chương Hữu nghị"
        };

        private List<string> _huyChuong = new List<string>
        {
            "Huy chương Quân kỳ quyết thắng",
            "Huy chương Vì an ninh Tổ quốc",
            "Huy chương Chiến sĩ vẻ vang hạng nhất",
            "Huy chương Chiến sĩ vẻ vang hạng nhì",
            "Huy chương Chiến sĩ vẻ vang hạng ba",
            "Huy chương Hữu nghị"
        };

        private List<string> _danhHieuVinhDu = new List<string>
        {
            "Anh hùng Lực lượng vũ trang nhân dân",
            "Anh hùng Lao động"
        };

        private List<string> _giaiThuong = new List<string>
        {
            "Giải thưởng Hồ Chí Minh",
            "Giải thưởng nhà nước"
        };

        private List<string> _khac = new List<string>
        {
            "Kỷ niệm chương",
            "Huy hiệu",
            "Bằng khen của Thủ tướng Chính phủ",
            "Bằng khen cấp bộ, ngành, tỉnh, đoàn thể trung ương",
            "Giấy khen"
        };

        public PageKhenThuong()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
            ApplyPermissions();
        }

        private void InitializeServices()
        {
            _khenThuongService = new KhenThuongCaNhanService();
            _dangVienService = new DangVienService();
        }

        private void SetupUI()
        {
            SetupDataGridView();
            SetupHinhThucRadioButtons();
            LoadDonViComboBox();
            SetupEvents();
        }

        private void LoadDonViComboBox()
        {
            try
            {
                var donViService = new DonViService();
                var donViList = donViService.GetDonViData();
                cboLocTheo.DataSource = donViList;
                cboLocTheo.DisplayMember = "TenDonVi";
                cboLocTheo.ValueMember = "DonViID";
                cboLocTheo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvDangVien.AutoGenerateColumns = false;
            dgvDangVien.AllowUserToAddRows = false;
            dgvDangVien.AllowUserToDeleteRows = false;
            dgvDangVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDangVien.MultiSelect = false;
            dgvDangVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDangVien.Columns.Clear();
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DangVienID",
                HeaderText = "ID",
                DataPropertyName = "DangVienID",
                Visible = false
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 25
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GioiTinh",
                HeaderText = "Giới tính",
                DataPropertyName = "GioiTinh",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 10
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoCCCD",
                HeaderText = "Số CCCD",
                DataPropertyName = "SoCCCD",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoTheDangVien",
                HeaderText = "Số thẻ Đảng viên",
                DataPropertyName = "SoTheDangVien",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiDangVien",
                HeaderText = "Loại",
                DataPropertyName = "LoaiDangVien",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 12
            });
            dgvDangVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDonVi",
                HeaderText = "Đơn vị",
                DataPropertyName = "TenDonVi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 23
            });
        }

        private void SetupHinhThucRadioButtons()
        {
            panelHinhThuc.Controls.Clear();
            
            Font radioFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            Font labelFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            int yPos = 10;
            int xPos = 10;
            int maxWidth = panelHinhThuc.Width - 30;
            int currentX = xPos;
            int lineHeight = 28;
            
            // Danh hiệu thi đua
            Label lblDanhHieu = new Label
            {
                Text = "A. Danh hiệu thi đua:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblDanhHieu);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _danhHieuThiDua)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
            
            yPos += 10;
            currentX = xPos;
            
            // Huân chương
            Label lblHuanChuong = new Label
            {
                Text = "B. Huân chương:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblHuanChuong);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _huanChuong)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
            
            yPos += 10;
            currentX = xPos;
            
            // Huy chương
            Label lblHuyChuong = new Label
            {
                Text = "C. Huy chương:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblHuyChuong);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _huyChuong)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
            
            yPos += 10;
            currentX = xPos;
            
            // Danh hiệu vinh dự
            Label lblDanhHieuVinhDu = new Label
            {
                Text = "D. Danh hiệu vinh dự nhà nước:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblDanhHieuVinhDu);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _danhHieuVinhDu)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
            
            yPos += 10;
            currentX = xPos;
            
            // Giải thưởng
            Label lblGiaiThuong = new Label
            {
                Text = "E. Giải thưởng:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblGiaiThuong);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _giaiThuong)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
            
            yPos += 10;
            currentX = xPos;
            
            // Khác
            Label lblKhac = new Label
            {
                Text = "F. Khác:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblKhac);
            yPos += lineHeight;
            currentX = xPos + 20;
            
            foreach (var item in _khac)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(currentX, yPos),
                    AutoSize = true,
                    Tag = item
                };
                rb.CheckedChanged += RadioButton_CheckedChanged;
                panelHinhThuc.Controls.Add(rb);
                yPos += lineHeight;
            }
        }
        
        private string _selectedHinhThuc = null;
        
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                _selectedHinhThuc = rb.Tag?.ToString();
            }
        }

        private void SetupEvents()
        {
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvDangVien.SelectionChanged += DgvDangVien_SelectionChanged;
            dgvDangVien.CellDoubleClick += DgvDangVien_CellDoubleClick;
            btnLuu.Click += BtnLuu_Click;
            linkFileDinhKem.Click += LinkFileDinhKem_Click;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
        }

        private void LoadData()
        {
            try
            {
                _danhSachDangVien = _dangVienService.GetAll(trangThai: true);
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            dgvDangVien.DataSource = _danhSachDangVien;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            FilterData();
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                RefreshDataGridView();
            }
        }

        private void FilterData()
        {
            try
            {
                string keyword = txtTimKiem.Text?.Trim().ToLower() ?? "";
                int? donViID = null;

                if (cboLocTheo.SelectedValue != null)
                {
                    if (cboLocTheo.SelectedValue is DonViSimplified donVi)
                        donViID = donVi.DonViID;
                    else if (cboLocTheo.SelectedValue is int id)
                        donViID = id;
                }

                _danhSachDangVien = _dangVienService.GetAll(donViID: donViID, trangThai: true);

                if (!string.IsNullOrEmpty(keyword))
                {
                    _danhSachDangVien = _danhSachDangVien.Where(dv =>
                        (dv.HoTen?.ToLower().Contains(keyword) ?? false) ||
                        (dv.SoCCCD?.ToLower().Contains(keyword) ?? false) ||
                        (dv.SoTheDangVien?.ToLower().Contains(keyword) ?? false) ||
                        (dv.TenDonVi?.ToLower().Contains(keyword) ?? false)
                    ).ToList();
                }

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDangVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDangVien.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDangVien.SelectedRows[0];
                int dangVienID = Convert.ToInt32(selectedRow.Cells["DangVienID"].Value);
                _dangVienSelected = _danhSachDangVien.FirstOrDefault(dv => dv.DangVienID == dangVienID);
                UpdateSelectedInfo();
            }
        }

        private void DgvDangVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DgvDangVien_SelectionChanged(sender, e);
            }
        }

        private void UpdateSelectedInfo()
        {
            if (_dangVienSelected != null)
            {
                lblThongTinDangVien.Text = $"Đã chọn: {_dangVienSelected.HoTen} - {_dangVienSelected.TenDonVi}";
                lblThongTinDangVien.ForeColor = Color.FromArgb(0, 174, 219);
            }
            else
            {
                lblThongTinDangVien.Text = "Chưa chọn đảng viên";
                lblThongTinDangVien.ForeColor = Color.Gray;
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("KhenThuong", "Create"))
            {
                MessageBox.Show("Bạn không có quyền thêm khen thưởng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_dangVienSelected == null)
            {
                MessageBox.Show("Vui lòng chọn đảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_selectedHinhThuc))
            {
                MessageBox.Show("Vui lòng chọn hình thức khen thưởng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoQuyetDinh.Text))
            {
                MessageBox.Show("Vui lòng nhập số quyết định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoQuyetDinh.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCapQuyetDinh.Text))
            {
                MessageBox.Show("Vui lòng nhập cấp quyết định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapQuyetDinh.Focus();
                return;
            }

            try
            {
                var khenThuong = new KhenThuongCaNhan
                {
                    DangVienID = _dangVienSelected.DangVienID,
                    HinhThuc = _selectedHinhThuc,
                    Ngay = dtpNgay.Value,
                    SoQuyetDinh = txtSoQuyetDinh.Text.Trim(),
                    CapQuyetDinh = txtCapQuyetDinh.Text.Trim(),
                    NoiDung = rtxtNoiDung.Text,
                    FileDinhKem = lblFileDinhKem.Text == "Chưa chọn file" ? null : lblFileDinhKem.Text,
                    NguoiTao = "System" // TODO: Lấy từ user hiện tại
                };

                var (id, error) = _khenThuongService.Insert(khenThuong);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show($"Lỗi khi lưu: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Đã lưu khen thưởng thành công! ID: {id}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkFileDinhKem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tất cả files (*.*)|*.*|Ảnh (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|PDF (*.pdf)|*.pdf|Word (*.doc;*.docx)|*.doc;*.docx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Sử dụng FileHelper để lưu file giống cách SaveInforHelper lưu data
                        string relativePath = FileHelper.SaveKhenThuongFile(openFileDialog.FileName);
                        lblFileDinhKem.Text = relativePath;
                        lblFileDinhKem.ForeColor = Color.FromArgb(0, 174, 219);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ClearForm()
        {
            // Uncheck all radio buttons
            foreach (Control ctrl in panelHinhThuc.Controls)
            {
                if (ctrl is RadioButton rb)
                {
                    rb.Checked = false;
                }
            }
            _selectedHinhThuc = null;
            dtpNgay.Value = DateTime.Now;
            txtSoQuyetDinh.Clear();
            txtCapQuyetDinh.Clear();
            rtxtNoiDung.Clear();
            lblFileDinhKem.Text = "Chưa chọn file";
            lblFileDinhKem.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canCreate = AuthorizationHelper.HasPermission("KhenThuong", "Create");
            if (btnLuu != null) btnLuu.Enabled = canCreate;
        }
    }
}
