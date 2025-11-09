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
    public partial class PageKyLuatToChuc : UserControl
    {
        private KyLuatToChucService _kyLuatService;
        private DonViService _donViService;
        private List<DonViSimplified> _danhSachDonVi;
        private DonViSimplified _donViSelected;
        private string _selectedHinhThuc = null;

        // Danh sách hình thức kỷ luật tổ chức đảng
        private List<string> _hinhThucKyLuat = new List<string>
        {
            "Khiển trách",
            "Cảnh cáo",
            "Giải tán"
        };

        public PageKyLuatToChuc()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
        }

        private void InitializeServices()
        {
            _kyLuatService = new KyLuatToChucService();
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
            int yPos = 10;
            int xPos = 10;
            int lineHeight = 35;

            foreach (var item in _hinhThucKyLuat)
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
                var kyLuat = new KyLuatToChuc
                {
                    DonViID = donViID,
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

                MessageBox.Show($"Đã lưu kỷ luật tổ chức đảng thành công! ID: {id}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // Sử dụng FileHelper để lưu file - tạm thời dùng KyLuat folder
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
            cboDonVi.SelectedIndex = -1;
            _donViSelected = null;
            UpdateSelectedInfo();
        }
    }
}

