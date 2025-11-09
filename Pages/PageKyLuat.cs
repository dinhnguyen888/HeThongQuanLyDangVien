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
    public partial class PageKyLuat : UserControl
    {
        private KyLuatCaNhanService _kyLuatService;
        private DangVienService _dangVienService;
        private List<DangVienDTO> _danhSachDangVien;
        private DangVienDTO _dangVienSelected;

        // Hình thức kỷ luật cho đảng viên chính thức
        private List<string> _hinhThucChinhThuc = new List<string>
        {
            "Khiển trách",
            "Cảnh cáo",
            "Cách chức",
            "Khai trừ"
        };

        // Hình thức kỷ luật cho đảng viên dự bị
        private List<string> _hinhThucDuBi = new List<string>
        {
            "Khiển trách",
            "Cảnh cáo"
        };

        public PageKyLuat()
        {
            InitializeComponent();
            this.Load += (s, e) =>
            {
                InitializeServices();
                SetupUI();
                LoadData();
            };
        }

        private void InitializeServices()
        {
            _kyLuatService = new KyLuatCaNhanService();
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
            // Sẽ được cập nhật dựa trên loại đảng viên được chọn
            panelHinhThuc.Controls.Clear();
        }

        private string _selectedHinhThuc = null;

        private void UpdateHinhThucRadioButtons()
        {
            panelHinhThuc.Controls.Clear();
            _selectedHinhThuc = null;
            
            if (_dangVienSelected == null)
            {
                return;
            }

            List<string> hinhThucList;
            if (_dangVienSelected.LoaiDangVien == "Chính thức")
            {
                hinhThucList = _hinhThucChinhThuc;
                lblLoaiDangVien.Text = "Đảng viên chính thức";
            }
            else if (_dangVienSelected.LoaiDangVien == "Dự bị")
            {
                hinhThucList = _hinhThucDuBi;
                lblLoaiDangVien.Text = "Đảng viên dự bị";
            }
            else
            {
                return;
            }

            Font radioFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            int yPos = 10;
            int xPos = 10;
            int lineHeight = 35;

            foreach (var item in hinhThucList)
            {
                RadioButton rb = new RadioButton
                {
                    Text = item,
                    Font = radioFont,
                    Location = new Point(xPos, yPos),
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
                UpdateHinhThucRadioButtons();
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
            if (_dangVienSelected == null)
            {
                MessageBox.Show("Vui lòng chọn đảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_selectedHinhThuc))
            {
                MessageBox.Show("Vui lòng chọn hình thức kỷ luật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var kyLuat = new KyLuatCaNhan
                {
                    DangVienID = _dangVienSelected.DangVienID,
                    HinhThuc = _selectedHinhThuc,
                    Ngay = dtpNgay.Value,
                    SoQuyetDinh = txtSoQuyetDinh.Text.Trim(),
                    CapQuyetDinh = txtCapQuyetDinh.Text.Trim(),
                    NoiDung = rtxtNoiDung.Text,
                    GhiChu = rtxtGhiChu.Text,
                    FileDinhKem = lblFileDinhKem.Text == "Chưa chọn file" ? null : lblFileDinhKem.Text,
                    NguoiTao = "System" // TODO: Lấy từ user hiện tại
                };

                var (id, error) = _kyLuatService.Insert(kyLuat);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show($"Lỗi khi lưu: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Đã lưu kỷ luật thành công! ID: {id}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        string relativePath = FileHelper.SaveKyLuatFile(openFileDialog.FileName);
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
            rtxtGhiChu.Clear();
            lblFileDinhKem.Text = "Chưa chọn file";
            lblFileDinhKem.ForeColor = Color.Gray;
        }
    }
}
