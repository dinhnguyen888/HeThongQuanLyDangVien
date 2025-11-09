using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using QuanLyDangVien.DTOs;
using QuanLyDangVien.Helper;

namespace QuanLyDangVien.Pages
{
    public partial class PageDanhSachKhenThuongKyLuat : UserControl
    {
        private KhenThuongCaNhanService _khenThuongCaNhanService;
        private KhenThuongDonViService _khenThuongDonViService;
        private KyLuatCaNhanService _kyLuatCaNhanService;
        private KyLuatToChucService _kyLuatToChucService;
        private DonViService _donViService;
        private DangVienService _dangVienService;
        private string _loaiHienTai = "Khen thưởng"; // "Khen thưởng" hoặc "Kỷ luật"
        private string _loaiDoiTuong = "CaNhan"; // "CaNhan" hoặc "DonVi"

        public PageDanhSachKhenThuongKyLuat()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            LoadData();
        }

        private void InitializeServices()
        {
            _khenThuongCaNhanService = new KhenThuongCaNhanService();
            _khenThuongDonViService = new KhenThuongDonViService();
            _kyLuatCaNhanService = new KyLuatCaNhanService();
            _kyLuatToChucService = new KyLuatToChucService();
            _donViService = new DonViService();
            _dangVienService = new DangVienService();
        }

        private void SetupUI()
        {
            SetupDataGridView();
            SetupLoaiDoiTuongComboBox();
            SetupRadioButtons();
            LoadDonViComboBox();
            SetupEvents();
        }

        private void SetupDataGridView()
        {
            dgvDanhSach.AutoGenerateColumns = false;
            dgvDanhSach.AllowUserToAddRows = false;
            dgvDanhSach.AllowUserToDeleteRows = false;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.MultiSelect = false;
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SetupLoaiDoiTuongComboBox()
        {
            try
            {
                var loaiDoiTuongList = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("CaNhan", "Đảng viên"),
                    new KeyValuePair<string, string>("DonVi", "Đơn vị")
                };

                cboLoaiDoiTuong.DataSource = loaiDoiTuongList;
                cboLoaiDoiTuong.DisplayMember = "Value";
                cboLoaiDoiTuong.ValueMember = "Key";
                cboLoaiDoiTuong.SelectedValue = "CaNhan";
                _loaiDoiTuong = "CaNhan";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thiết lập ComboBox loại đối tượng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupRadioButtons()
        {
            radioKhenThuong.Checked = true;
            radioKyLuat.Checked = false;
            radioTheoNam.Checked = false;
        }

        private void LoadDonViComboBox()
        {
            try
            {
                var donViList = _donViService.GetDonViData();
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

        private void SetupEvents()
        {
            radioKhenThuong.CheckedChanged += RadioKhenThuong_CheckedChanged;
            radioKyLuat.CheckedChanged += RadioKyLuat_CheckedChanged;
            cboLoaiDoiTuong.SelectedValueChanged += CboLoaiDoiTuong_SelectedValueChanged;
            radioTheoNam.CheckedChanged += RadioTheoNam_CheckedChanged;
            btnTimKiem.Click += BtnTimKiem_Click;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            btnSua.Click += BtnSua_Click;
            btnSuaFile.Click += BtnSuaFile_Click;
            btnXoa.Click += BtnXoa_Click;
            dgvDanhSach.CellDoubleClick += DgvDanhSach_CellDoubleClick;
            dgvDanhSach.KeyDown += DgvDanhSach_KeyDown;
        }

        private void LoadData()
        {
            try
            {
                if (_loaiHienTai == "Khen thưởng")
                {
                    LoadKhenThuongData();
                }
                else
                {
                    LoadKyLuatData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhenThuongData()
        {
            try
            {
                dgvDanhSach.DataSource = null;
                dgvDanhSach.Columns.Clear();
                
                if (_loaiDoiTuong == "CaNhan")
                {
                    var data = _khenThuongCaNhanService.GetAll();
                    
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "KhenThuongCaNhanID",
                        HeaderText = "ID",
                        DataPropertyName = "KhenThuongCaNhanID",
                        Visible = false
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TenDangVien",
                        HeaderText = "Họ tên",
                        DataPropertyName = "TenDangVien",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "HinhThuc",
                        HeaderText = "Hình thức",
                        DataPropertyName = "HinhThuc",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 25
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Ngay",
                        HeaderText = "Ngày",
                        DataPropertyName = "Ngay",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 12
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "SoQuyetDinh",
                        HeaderText = "Số quyết định",
                        DataPropertyName = "SoQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 15
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "CapQuyetDinh",
                        HeaderText = "Cấp quyết định",
                        DataPropertyName = "CapQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 18
                    });

                    dgvDanhSach.DataSource = data;
                }
                else
                {
                    var data = _khenThuongDonViService.GetAll();
                    
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "KhenThuongDonViID",
                        HeaderText = "ID",
                        DataPropertyName = "KhenThuongDonViID",
                        Visible = false
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TenDonVi",
                        HeaderText = "Đơn vị",
                        DataPropertyName = "TenDonVi",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "HinhThuc",
                        HeaderText = "Hình thức",
                        DataPropertyName = "HinhThuc",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 25
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Ngay",
                        HeaderText = "Ngày",
                        DataPropertyName = "Ngay",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 12
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "SoQuyetDinh",
                        HeaderText = "Số quyết định",
                        DataPropertyName = "SoQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 15
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "CapQuyetDinh",
                        HeaderText = "Cấp quyết định",
                        DataPropertyName = "CapQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 18
                    });

                    dgvDanhSach.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu khen thưởng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKyLuatData()
        {
            try
            {
                dgvDanhSach.DataSource = null;
                dgvDanhSach.Columns.Clear();
                
                if (_loaiDoiTuong == "CaNhan")
                {
                    var data = _kyLuatCaNhanService.GetAll();
                    
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "KyLuatCaNhanID",
                        HeaderText = "ID",
                        DataPropertyName = "KyLuatCaNhanID",
                        Visible = false
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TenDangVien",
                        HeaderText = "Họ tên",
                        DataPropertyName = "TenDangVien",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "HinhThuc",
                        HeaderText = "Hình thức",
                        DataPropertyName = "HinhThuc",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Ngay",
                        HeaderText = "Ngày",
                        DataPropertyName = "Ngay",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 12
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "SoQuyetDinh",
                        HeaderText = "Số quyết định",
                        DataPropertyName = "SoQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 15
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "CapQuyetDinh",
                        HeaderText = "Cấp quyết định",
                        DataPropertyName = "CapQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 18
                    });

                    dgvDanhSach.DataSource = data;
                }
                else
                {
                    var data = _kyLuatToChucService.GetAll();
                    
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "KyLuatToChucID",
                        HeaderText = "ID",
                        DataPropertyName = "KyLuatToChucID",
                        Visible = false
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "TenDonVi",
                        HeaderText = "Đơn vị",
                        DataPropertyName = "TenDonVi",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "HinhThuc",
                        HeaderText = "Hình thức",
                        DataPropertyName = "HinhThuc",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 20
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Ngay",
                        HeaderText = "Ngày",
                        DataPropertyName = "Ngay",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 12
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "SoQuyetDinh",
                        HeaderText = "Số quyết định",
                        DataPropertyName = "SoQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 15
                    });
                    dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "CapQuyetDinh",
                        HeaderText = "Cấp quyết định",
                        DataPropertyName = "CapQuyetDinh",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 18
                    });

                    dgvDanhSach.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu kỷ luật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RadioKhenThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (radioKhenThuong.Checked)
            {
                _loaiHienTai = "Khen thưởng";
                LoadData();
            }
        }

        private void RadioKyLuat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioKyLuat.Checked)
            {
                _loaiHienTai = "Kỷ luật";
                LoadData();
            }
        }

        private void CboLoaiDoiTuong_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboLoaiDoiTuong.SelectedValue != null)
            {
                _loaiDoiTuong = cboLoaiDoiTuong.SelectedValue.ToString();
                
                // Chỉ hiển thị ComboBox lọc theo đơn vị khi chọn "Đơn vị"
                if (_loaiDoiTuong == "DonVi")
                {
                    cboLocTheo.Visible = true;
                    lblLocTheoDonVi.Visible = true;
                }
                else
                {
                    cboLocTheo.Visible = false;
                    lblLocTheoDonVi.Visible = false;
                    cboLocTheo.SelectedIndex = -1;
                }
                
                LoadData();
            }
        }

        private void RadioTheoNam_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTheoNam.Checked)
            {
                FilterData();
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            FilterData();
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadData();
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

                // TODO: Implement filter logic based on radio buttons
                // Hiện tại chỉ reload data
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvDanhSach.SelectedRows[0];
                int id = 0;

                if (_loaiHienTai == "Khen thưởng")
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KhenThuongCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuong = _khenThuongCaNhanService.GetById(id);
                        if (khenThuong == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Convert DTO to Model
                        var khenThuongModel = new KhenThuongCaNhan
                        {
                            KhenThuongCaNhanID = khenThuong.KhenThuongCaNhanID,
                            DangVienID = khenThuong.DangVienID,
                            HinhThuc = khenThuong.HinhThuc,
                            Ngay = khenThuong.Ngay,
                            SoQuyetDinh = khenThuong.SoQuyetDinh,
                            CapQuyetDinh = khenThuong.CapQuyetDinh,
                            NoiDung = khenThuong.NoiDung,
                            FileDinhKem = khenThuong.FileDinhKem,
                            NgayTao = khenThuong.NgayTao,
                            NguoiTao = khenThuong.NguoiTao
                        };

                        var formSua = new FormSua(khenThuongModel);
                        var dangVienList = _dangVienService.GetAll();
                        RefreshDangVienData(formSua, dangVienList);
                        var donViList = _donViService.GetDonViData();
                        RefreshDonViData(formSua, donViList);
                        
                        // Chuyển HinhThuc và DangVienID thành TextBox read-only
                        ConvertComboBoxToReadOnlyTextBox(formSua, "HinhThuc", khenThuong.HinhThuc);
                        var dangVien = dangVienList.FirstOrDefault(d => d.DangVienID == khenThuong.DangVienID);
                        ConvertComboBoxToReadOnlyTextBox(formSua, "DangVienID", dangVien != null ? dangVien.HoTen : khenThuong.DangVienID.ToString());

                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedKhenThuong = (KhenThuongCaNhan)formSua.GetData();
                            var (success, error) = _khenThuongCaNhanService.Update(updatedKhenThuong);
                            if (success)
                            {
                                MessageBox.Show("Cập nhật khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KhenThuongDonViID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuong = _khenThuongDonViService.GetById(id);
                        if (khenThuong == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var khenThuongModel = new KhenThuongDonVi
                        {
                            KhenThuongDonViID = khenThuong.KhenThuongDonViID,
                            DonViID = khenThuong.DonViID,
                            HinhThuc = khenThuong.HinhThuc,
                            Ngay = khenThuong.Ngay,
                            SoQuyetDinh = khenThuong.SoQuyetDinh,
                            CapQuyetDinh = khenThuong.CapQuyetDinh,
                            NoiDung = khenThuong.NoiDung,
                            FileDinhKem = khenThuong.FileDinhKem,
                            NgayTao = khenThuong.NgayTao,
                            NguoiTao = khenThuong.NguoiTao
                        };

                        var formSua = new FormSua(khenThuongModel);
                        var donViList = _donViService.GetDonViData();
                        RefreshDonViData(formSua, donViList);
                        
                        // Chuyển HinhThuc thành TextBox read-only
                        ConvertComboBoxToReadOnlyTextBox(formSua, "HinhThuc", khenThuong.HinhThuc);
                        var donVi = donViList.FirstOrDefault(d => d.DonViID == khenThuong.DonViID);
                        ConvertComboBoxToReadOnlyTextBox(formSua, "DonViID", donVi != null ? donVi.TenDonVi : khenThuong.DonViID.ToString());

                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedKhenThuong = (KhenThuongDonVi)formSua.GetData();
                            var (success, error) = _khenThuongDonViService.Update(updatedKhenThuong);
                            if (success)
                            {
                                MessageBox.Show("Cập nhật khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KyLuatCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuat = _kyLuatCaNhanService.GetById(id);
                        if (kyLuat == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var kyLuatModel = new KyLuatCaNhan
                        {
                            KyLuatCaNhanID = kyLuat.KyLuatCaNhanID,
                            DangVienID = kyLuat.DangVienID,
                            HinhThuc = kyLuat.HinhThuc,
                            Ngay = kyLuat.Ngay,
                            SoQuyetDinh = kyLuat.SoQuyetDinh,
                            CapQuyetDinh = kyLuat.CapQuyetDinh,
                            NoiDung = kyLuat.NoiDung,
                            GhiChu = kyLuat.GhiChu,
                            FileDinhKem = kyLuat.FileDinhKem,
                            NgayTao = kyLuat.NgayTao,
                            NguoiTao = kyLuat.NguoiTao
                        };

                        var formSua = new FormSua(kyLuatModel);
                        var dangVienList = _dangVienService.GetAll();
                        RefreshDangVienData(formSua, dangVienList);
                        var donViList = _donViService.GetDonViData();
                        RefreshDonViData(formSua, donViList);
                        
                        // Chuyển HinhThuc và DangVienID thành TextBox read-only
                        ConvertComboBoxToReadOnlyTextBox(formSua, "HinhThuc", kyLuat.HinhThuc);
                        var dangVien = dangVienList.FirstOrDefault(d => d.DangVienID == kyLuat.DangVienID);
                        ConvertComboBoxToReadOnlyTextBox(formSua, "DangVienID", dangVien != null ? dangVien.HoTen : kyLuat.DangVienID.ToString());

                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedKyLuat = (KyLuatCaNhan)formSua.GetData();
                            var (success, error) = _kyLuatCaNhanService.Update(updatedKyLuat);
                            if (success)
                            {
                                MessageBox.Show("Cập nhật kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KyLuatToChucID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuat = _kyLuatToChucService.GetById(id);
                        if (kyLuat == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var kyLuatModel = new KyLuatToChuc
                        {
                            KyLuatToChucID = kyLuat.KyLuatToChucID,
                            DonViID = kyLuat.DonViID,
                            HinhThuc = kyLuat.HinhThuc,
                            Ngay = kyLuat.Ngay,
                            SoQuyetDinh = kyLuat.SoQuyetDinh,
                            CapQuyetDinh = kyLuat.CapQuyetDinh,
                            NoiDung = kyLuat.NoiDung,
                            GhiChu = kyLuat.GhiChu,
                            FileDinhKem = kyLuat.FileDinhKem,
                            NgayTao = kyLuat.NgayTao,
                            NguoiTao = kyLuat.NguoiTao
                        };

                        var formSua = new FormSua(kyLuatModel);
                        var donViList = _donViService.GetDonViData();
                        RefreshDonViData(formSua, donViList);
                        
                        // Chuyển HinhThuc thành TextBox read-only
                        ConvertComboBoxToReadOnlyTextBox(formSua, "HinhThuc", kyLuat.HinhThuc);
                        var donVi = donViList.FirstOrDefault(d => d.DonViID == kyLuat.DonViID);
                        ConvertComboBoxToReadOnlyTextBox(formSua, "DonViID", donVi != null ? donVi.TenDonVi : kyLuat.DonViID.ToString());

                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            var updatedKyLuat = (KyLuatToChuc)formSua.GetData();
                            var (success, error) = _kyLuatToChucService.Update(updatedKyLuat);
                            if (success)
                            {
                                MessageBox.Show("Cập nhật kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var selectedRow = dgvDanhSach.SelectedRows[0];
                int id = 0;
                bool success = false;
                string error = null;

                if (_loaiHienTai == "Khen thưởng")
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KhenThuongCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        (success, error) = _khenThuongCaNhanService.Delete(id);
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KhenThuongDonViID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        (success, error) = _khenThuongDonViService.Delete(id);
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Xóa khen thưởng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi xóa: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KyLuatCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        (success, error) = _kyLuatCaNhanService.Delete(id);
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KyLuatToChucID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        (success, error) = _kyLuatToChucService.Delete(id);
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Xóa kỷ luật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi xóa: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDanhSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                XemChiTiet();
            }
        }

        private void XemChiTiet()
        {
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                if (dgvDanhSach.Rows.Count > 0)
                {
                    dgvDanhSach.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                var selectedRow = dgvDanhSach.SelectedRows[0];
                int id = 0;
                object dataObject = null;

                if (_loaiHienTai == "Khen thưởng")
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KhenThuongCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuongDTO = _khenThuongCaNhanService.GetById(id);
                        if (khenThuongDTO == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Convert DTO to Model
                        dataObject = new KhenThuongCaNhan
                        {
                            KhenThuongCaNhanID = khenThuongDTO.KhenThuongCaNhanID,
                            DangVienID = khenThuongDTO.DangVienID,
                            HinhThuc = khenThuongDTO.HinhThuc,
                            Ngay = khenThuongDTO.Ngay,
                            SoQuyetDinh = khenThuongDTO.SoQuyetDinh,
                            CapQuyetDinh = khenThuongDTO.CapQuyetDinh,
                            NoiDung = khenThuongDTO.NoiDung,
                            FileDinhKem = khenThuongDTO.FileDinhKem,
                            NgayTao = khenThuongDTO.NgayTao,
                            NguoiTao = khenThuongDTO.NguoiTao
                        };
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KhenThuongDonViID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuongDTO = _khenThuongDonViService.GetById(id);
                        if (khenThuongDTO == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dataObject = new KhenThuongDonVi
                        {
                            KhenThuongDonViID = khenThuongDTO.KhenThuongDonViID,
                            DonViID = khenThuongDTO.DonViID,
                            HinhThuc = khenThuongDTO.HinhThuc,
                            Ngay = khenThuongDTO.Ngay,
                            SoQuyetDinh = khenThuongDTO.SoQuyetDinh,
                            CapQuyetDinh = khenThuongDTO.CapQuyetDinh,
                            NoiDung = khenThuongDTO.NoiDung,
                            FileDinhKem = khenThuongDTO.FileDinhKem,
                            NgayTao = khenThuongDTO.NgayTao,
                            NguoiTao = khenThuongDTO.NguoiTao
                        };
                    }
                }
                else
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KyLuatCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuatDTO = _kyLuatCaNhanService.GetById(id);
                        if (kyLuatDTO == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dataObject = new KyLuatCaNhan
                        {
                            KyLuatCaNhanID = kyLuatDTO.KyLuatCaNhanID,
                            DangVienID = kyLuatDTO.DangVienID,
                            HinhThuc = kyLuatDTO.HinhThuc,
                            Ngay = kyLuatDTO.Ngay,
                            SoQuyetDinh = kyLuatDTO.SoQuyetDinh,
                            CapQuyetDinh = kyLuatDTO.CapQuyetDinh,
                            NoiDung = kyLuatDTO.NoiDung,
                            GhiChu = kyLuatDTO.GhiChu,
                            FileDinhKem = kyLuatDTO.FileDinhKem,
                            NgayTao = kyLuatDTO.NgayTao,
                            NguoiTao = kyLuatDTO.NguoiTao
                        };
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KyLuatToChucID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuatDTO = _kyLuatToChucService.GetById(id);
                        if (kyLuatDTO == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dataObject = new KyLuatToChuc
                        {
                            KyLuatToChucID = kyLuatDTO.KyLuatToChucID,
                            DonViID = kyLuatDTO.DonViID,
                            HinhThuc = kyLuatDTO.HinhThuc,
                            Ngay = kyLuatDTO.Ngay,
                            SoQuyetDinh = kyLuatDTO.SoQuyetDinh,
                            CapQuyetDinh = kyLuatDTO.CapQuyetDinh,
                            NoiDung = kyLuatDTO.NoiDung,
                            GhiChu = kyLuatDTO.GhiChu,
                            FileDinhKem = kyLuatDTO.FileDinhKem,
                            NgayTao = kyLuatDTO.NgayTao,
                            NguoiTao = kyLuatDTO.NguoiTao
                        };
                    }
                }

                if (dataObject != null)
                {
                    var formXemChiTiet = new FormXemChiTiet(dataObject);
                    formXemChiTiet.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDanhSach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvDanhSach.SelectedRows.Count > 0)
            {
                BtnXoa_Click(sender, e);
            }
        }

        private void RefreshDangVienData(Form form, List<DangVienDTO> dangVienList)
        {
            if (form == null) return;
            BindDangVienComboBoxes(form, dangVienList);
        }

        private void BindDangVienComboBoxes(Control parent, List<DangVienDTO> dangVienList)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DangVienID")
                {
                    comboBox.DataSource = dangVienList;
                    comboBox.DisplayMember = "HoTen";
                    comboBox.ValueMember = "DangVienID";
                }

                if (control.HasChildren)
                    BindDangVienComboBoxes(control, dangVienList);
            }
        }

        private void RefreshDonViData(Form form, List<DonViSimplified> donViList)
        {
            if (form == null) return;
            BindDonViComboBoxes(form, donViList);
        }

        private void BindDonViComboBoxes(Control parent, List<DonViSimplified> donViList)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                {
                    comboBox.DataSource = donViList;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";
                }

                if (control.HasChildren)
                    BindDonViComboBoxes(control, donViList);
            }
        }

        /// <summary>
        /// Chuyển ComboBox thành TextBox read-only và đánh dấu để không lưu giá trị từ control này
        /// </summary>
        private void ConvertComboBoxToReadOnlyTextBox(Form form, string controlName, string displayValue)
        {
            if (form == null) return;
            
            Control targetControl = FindControlByName(form, controlName);
            if (targetControl is ComboBox comboBox)
            {
                // Lưu giá trị gốc vào Tag để có thể khôi phục sau
                object originalValue = comboBox.SelectedValue ?? comboBox.SelectedItem;
                
                // Tạo TextBox mới
                TextBox textBox = new TextBox();
                textBox.Name = controlName;
                textBox.Text = displayValue ?? "";
                textBox.ReadOnly = true;
                textBox.BackColor = Color.FromArgb(245, 245, 245);
                textBox.Location = comboBox.Location;
                textBox.Size = comboBox.Size;
                textBox.Font = comboBox.Font;
                textBox.TabIndex = comboBox.TabIndex;
                // Lưu giá trị gốc vào Tag để FormHelper có thể sử dụng
                textBox.Tag = originalValue;

                // Thay thế ComboBox bằng TextBox
                Control parent = comboBox.Parent;
                if (parent != null)
                {
                    int index = parent.Controls.IndexOf(comboBox);
                    parent.Controls.Remove(comboBox);
                    parent.Controls.Add(textBox);
                    parent.Controls.SetChildIndex(textBox, index);
                }

                // Cập nhật controlsDictionary trong FormSua
                if (form is FormSua formSua)
                {
                    formSua.UpdateControlInDictionary(controlName, textBox);
                }
            }
        }

        /// <summary>
        /// Tìm control theo tên trong form
        /// </summary>
        private Control FindControlByName(Control parent, string name)
        {
            if (parent.Name == name)
                return parent;

            foreach (Control control in parent.Controls)
            {
                if (control.Name == name)
                    return control;

                if (control.HasChildren)
                {
                    Control found = FindControlByName(control, name);
                    if (found != null)
                        return found;
                }
            }

            return null;
        }

        private void BtnSuaFile_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa file đính kèm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvDanhSach.SelectedRows[0];
                int id = 0;
                string oldFileDinhKem = null;

                // Lấy thông tin record và file cũ
                if (_loaiHienTai == "Khen thưởng")
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KhenThuongCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuong = _khenThuongCaNhanService.GetById(id);
                        if (khenThuong == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        oldFileDinhKem = khenThuong.FileDinhKem;

                        // Mở dialog chọn file
                        var openFileDialog = new OpenFileDialog
                        {
                            Filter = "Tất cả các file (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                            Title = "Chọn file đính kèm mới"
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Lưu file mới
                            string newFileDinhKem = FileHelper.SaveKhenThuongFile(openFileDialog.FileName);

                            // Cập nhật database
                            var khenThuongModel = new KhenThuongCaNhan
                            {
                                KhenThuongCaNhanID = khenThuong.KhenThuongCaNhanID,
                                DangVienID = khenThuong.DangVienID,
                                HinhThuc = khenThuong.HinhThuc,
                                Ngay = khenThuong.Ngay,
                                SoQuyetDinh = khenThuong.SoQuyetDinh,
                                CapQuyetDinh = khenThuong.CapQuyetDinh,
                                NoiDung = khenThuong.NoiDung,
                                FileDinhKem = newFileDinhKem,
                                NgayTao = khenThuong.NgayTao,
                                NguoiTao = khenThuong.NguoiTao
                            };

                            var (success, error) = _khenThuongCaNhanService.Update(khenThuongModel);
                            if (success)
                            {
                                // Xóa file cũ nếu có
                                if (!string.IsNullOrWhiteSpace(oldFileDinhKem))
                                {
                                    FileHelper.DeleteFile(oldFileDinhKem);
                                }
                                MessageBox.Show("Cập nhật file đính kèm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                // Xóa file mới nếu update thất bại
                                FileHelper.DeleteFile(newFileDinhKem);
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KhenThuongDonViID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var khenThuong = _khenThuongDonViService.GetById(id);
                        if (khenThuong == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin khen thưởng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        oldFileDinhKem = khenThuong.FileDinhKem;

                        var openFileDialog = new OpenFileDialog
                        {
                            Filter = "Tất cả các file (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                            Title = "Chọn file đính kèm mới"
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string newFileDinhKem = FileHelper.SaveKhenThuongFile(openFileDialog.FileName);

                            var khenThuongModel = new KhenThuongDonVi
                            {
                                KhenThuongDonViID = khenThuong.KhenThuongDonViID,
                                DonViID = khenThuong.DonViID,
                                HinhThuc = khenThuong.HinhThuc,
                                Ngay = khenThuong.Ngay,
                                SoQuyetDinh = khenThuong.SoQuyetDinh,
                                CapQuyetDinh = khenThuong.CapQuyetDinh,
                                NoiDung = khenThuong.NoiDung,
                                FileDinhKem = newFileDinhKem,
                                NgayTao = khenThuong.NgayTao,
                                NguoiTao = khenThuong.NguoiTao
                            };

                            var (success, error) = _khenThuongDonViService.Update(khenThuongModel);
                            if (success)
                            {
                                if (!string.IsNullOrWhiteSpace(oldFileDinhKem))
                                {
                                    FileHelper.DeleteFile(oldFileDinhKem);
                                }
                                MessageBox.Show("Cập nhật file đính kèm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                FileHelper.DeleteFile(newFileDinhKem);
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    if (_loaiDoiTuong == "CaNhan")
                    {
                        var idValue = selectedRow.Cells["KyLuatCaNhanID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuat = _kyLuatCaNhanService.GetById(id);
                        if (kyLuat == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        oldFileDinhKem = kyLuat.FileDinhKem;

                        var openFileDialog = new OpenFileDialog
                        {
                            Filter = "Tất cả các file (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                            Title = "Chọn file đính kèm mới"
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string newFileDinhKem = FileHelper.SaveKyLuatFile(openFileDialog.FileName);

                            var kyLuatModel = new KyLuatCaNhan
                            {
                                KyLuatCaNhanID = kyLuat.KyLuatCaNhanID,
                                DangVienID = kyLuat.DangVienID,
                                HinhThuc = kyLuat.HinhThuc,
                                Ngay = kyLuat.Ngay,
                                SoQuyetDinh = kyLuat.SoQuyetDinh,
                                CapQuyetDinh = kyLuat.CapQuyetDinh,
                                NoiDung = kyLuat.NoiDung,
                                GhiChu = kyLuat.GhiChu,
                                FileDinhKem = newFileDinhKem,
                                NgayTao = kyLuat.NgayTao,
                                NguoiTao = kyLuat.NguoiTao
                            };

                            var (success, error) = _kyLuatCaNhanService.Update(kyLuatModel);
                            if (success)
                            {
                                if (!string.IsNullOrWhiteSpace(oldFileDinhKem))
                                {
                                    FileHelper.DeleteFile(oldFileDinhKem);
                                }
                                MessageBox.Show("Cập nhật file đính kèm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                FileHelper.DeleteFile(newFileDinhKem);
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        var idValue = selectedRow.Cells["KyLuatToChucID"]?.Value;
                        if (idValue == null)
                        {
                            MessageBox.Show("Không tìm thấy ID bản ghi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        id = Convert.ToInt32(idValue);
                        var kyLuat = _kyLuatToChucService.GetById(id);
                        if (kyLuat == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin kỷ luật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        oldFileDinhKem = kyLuat.FileDinhKem;

                        var openFileDialog = new OpenFileDialog
                        {
                            Filter = "Tất cả các file (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                            Title = "Chọn file đính kèm mới"
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string newFileDinhKem = FileHelper.SaveKyLuatFile(openFileDialog.FileName);

                            var kyLuatModel = new KyLuatToChuc
                            {
                                KyLuatToChucID = kyLuat.KyLuatToChucID,
                                DonViID = kyLuat.DonViID,
                                HinhThuc = kyLuat.HinhThuc,
                                Ngay = kyLuat.Ngay,
                                SoQuyetDinh = kyLuat.SoQuyetDinh,
                                CapQuyetDinh = kyLuat.CapQuyetDinh,
                                NoiDung = kyLuat.NoiDung,
                                GhiChu = kyLuat.GhiChu,
                                FileDinhKem = newFileDinhKem,
                                NgayTao = kyLuat.NgayTao,
                                NguoiTao = kyLuat.NguoiTao
                            };

                            var (success, error) = _kyLuatToChucService.Update(kyLuatModel);
                            if (success)
                            {
                                if (!string.IsNullOrWhiteSpace(oldFileDinhKem))
                                {
                                    FileHelper.DeleteFile(oldFileDinhKem);
                                }
                                MessageBox.Show("Cập nhật file đính kèm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                FileHelper.DeleteFile(newFileDinhKem);
                                MessageBox.Show($"Lỗi khi cập nhật: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa file đính kèm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
