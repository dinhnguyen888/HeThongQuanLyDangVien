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
    public partial class PageKhenThuongDonVi : UserControl
    {
        private KhenThuongDonViService _khenThuongService;
        private DonViService _donViService;
        private List<DonViSimplified> _danhSachDonVi;
        private DonViSimplified _donViSelected;
        private string _selectedHinhThuc = null;

        // Danh sách hình thức khen thưởng đơn vị theo requirement
        private List<string> _danhHieuThiDua = new List<string>
        {
            "Cờ thi đua của Chính phủ",
            "Cờ thi đua cấp bộ, ngành, tỉnh, đoàn thể trung ương",
            "Tập thể lao động xuất sắc",
            "Đơn vị quyết thắng",
            "Tập thể lao động tiên tiến",
            "Đơn vị tiên tiến"
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

        public PageKhenThuongDonVi()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
        }

        private void InitializeServices()
        {
            _khenThuongService = new KhenThuongDonViService();
            _donViService = new DonViService();
        }

        private void SetupUI()
        {
            SetupDataGridView();
            SetupHinhThucRadioButtons();
            LoadDonViComboBox();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnLuu.Click += BtnLuu_Click;
            linkFileDinhKem.Click += LinkFileDinhKem_Click;
            dgvDonVi.SelectionChanged += DgvDonVi_SelectionChanged;
            btnTimKiem.Click += BtnTimKiem_Click;
        }

        private void LoadData()
        {
            LoadDonViData();
        }

        private void LoadDonViData()
        {
            try
            {
                _danhSachDonVi = _donViService.GetDonViData();
                dgvDonVi.DataSource = _danhSachDonVi;

                if (dgvDonVi.Columns.Count > 0)
                {
                    dgvDonVi.Columns[0].HeaderText = "Mã đơn vị";
                    dgvDonVi.Columns[1].HeaderText = "Tên đơn vị";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDonViComboBox()
        {
            try
            {
                var donViList = _donViService.GetDonViData();
                cboDonVi.DataSource = donViList;
                cboDonVi.DisplayMember = "TenDonVi";
                cboDonVi.ValueMember = "DonViID";
                cboDonVi.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvDonVi.AutoGenerateColumns = true;
            dgvDonVi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonVi.MultiSelect = false;
            dgvDonVi.ReadOnly = true;
        }

        private void SetupHinhThucRadioButtons()
        {
            if (panelHinhThuc == null)
            {
                return;
            }

            try
            {
                panelHinhThuc.Controls.Clear();
            }
            catch
            {
                return;
            }

            Font radioFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            Font labelFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            int yPos = 10;
            int xPos = 10;
            int lineHeight = 28;

            // A. Danh hiệu thi đua đối với tập thể
            Label lblDanhHieu = new Label
            {
                Text = "A. Danh hiệu thi đua đối với tập thể:",
                Font = labelFont,
                Location = new Point(xPos, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblDanhHieu);
            yPos += lineHeight;
            int currentX = xPos + 20;

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

            // B. Hình thức khen thưởng tập thể
            Label lblHinhThuc = new Label
            {
                Text = "B. Hình thức khen thưởng tập thể:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblHinhThuc);
            yPos += lineHeight;

            // 1. Huân chương tập thể
            Label lblHuanChuong = new Label
            {
                Text = "1. Huân chương tập thể:",
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
            currentX = xPos + 20;

            // 2. Huy chương tập thể
            Label lblHuyChuong = new Label
            {
                Text = "2. Huy chương tập thể:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblHuyChuong);
            yPos += lineHeight;

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

            // 3. Danh hiệu vinh dự nhà nước tập thể
            Label lblDanhHieuVinhDu = new Label
            {
                Text = "3. Danh hiệu vinh dự nhà nước tập thể:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblDanhHieuVinhDu);
            yPos += lineHeight;

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

            // 4. Giải thưởng tập thể
            Label lblGiaiThuong = new Label
            {
                Text = "4. Giải thưởng tập thể:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblGiaiThuong);
            yPos += lineHeight;

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

            // 5. Khác
            Label lblKhac = new Label
            {
                Text = "5. Khác:",
                Font = labelFont,
                Location = new Point(currentX, yPos),
                AutoSize = true
            };
            panelHinhThuc.Controls.Add(lblKhac);
            yPos += lineHeight;

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

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                _selectedHinhThuc = rb.Tag?.ToString();
            }
        }

        private void DgvDonVi_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonVi.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dgvDonVi.SelectedRows[0];
                    if (selectedRow.Cells["DonViID"].Value == null)
                    {
                        _donViSelected = null;
                        UpdateSelectedInfo();
                        return;
                    }

                    int donViID = Convert.ToInt32(selectedRow.Cells["DonViID"].Value);
                    _donViSelected = _danhSachDonVi.FirstOrDefault(dv => dv.DonViID == donViID);

                    UpdateSelectedInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn đơn vị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                _donViSelected = null;
                UpdateSelectedInfo();
            }
        }

        private void UpdateSelectedInfo()
        {
            if (_donViSelected != null)
            {
                lblThongTinDonVi.Text = $"Đơn vị: {_donViSelected.TenDonVi}";
                lblThongTinDonVi.ForeColor = Color.FromArgb(0, 174, 219);
                cboDonVi.SelectedValue = _donViSelected.DonViID;
            }
            else
            {
                lblThongTinDonVi.Text = "Chưa chọn đơn vị";
                lblThongTinDonVi.ForeColor = Color.Gray;
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            // Implement search logic if needed
            LoadDonViData();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_donViSelected == null && cboDonVi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int donViID = _donViSelected != null ? _donViSelected.DonViID : Convert.ToInt32(cboDonVi.SelectedValue);

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
                var khenThuong = new KhenThuongDonVi
                {
                    DonViID = donViID,
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

                MessageBox.Show($"Đã lưu khen thưởng đơn vị thành công! ID: {id}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // Sử dụng FileHelper để lưu file - tạm thời dùng KhenThuong folder
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
            cboDonVi.SelectedIndex = -1;
            _donViSelected = null;
            UpdateSelectedInfo();
        }
    }
}

