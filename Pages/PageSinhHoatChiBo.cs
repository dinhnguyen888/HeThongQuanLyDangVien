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
using QuanLyDangVien.Helper;
using QuanLyDangVien.DTOs;

namespace QuanLyDangVien.Pages
{
    public partial class PageSinhHoatChiBo : UserControl
    {
        private SinhHoatChiBoService _sinhHoatService;
        private DonViService _donViService;
        private DangVienService _dangVienService;
        private List<SinhHoatChiBo> _danhSachSinhHoat;
        private SinhHoatChiBo _sinhHoatHienTai;

        public PageSinhHoatChiBo()
        {
            InitializeComponent();
            InitializeServices();
            LoadData();
            SetupDataGridView();
        }

        private void InitializeServices()
        {
            _sinhHoatService = new SinhHoatChiBoService();
            _donViService = new DonViService();
            _dangVienService = new DangVienService();
        }

        private void LoadData()
        {
            try
            {
                _danhSachSinhHoat = _sinhHoatService.GetSinhHoatChiBoList();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Thêm các cột
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SinhHoatID",
                HeaderText = "ID",
                DataPropertyName = "SinhHoatID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TieuDe",
                HeaderText = "Tiêu đề",
                DataPropertyName = "TieuDe",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgaySinhHoat",
                HeaderText = "Ngày sinh hoạt",
                DataPropertyName = "NgaySinhHoat",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaDiem",
                HeaderText = "Địa điểm",
                DataPropertyName = "DiaDiem",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 20
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ChuTri",
                HeaderText = "Chủ trì",
                DataPropertyName = "ChuTri",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 12
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuongThamGia",
                HeaderText = "Số lượng tham gia",
                DataPropertyName = "SoLuongThamGia",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 8
            });
        }

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = _danhSachSinhHoat;
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            // Tạo lịch sinh hoạt
            try
            {
                var formThem = new FormThem(typeof(SinhHoatChiBo));
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formThem, donViData);
                
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    var newSinhHoat = (SinhHoatChiBo)formThem.GetData();
                    var sinhHoatID = _sinhHoatService.AddSinhHoatChiBo(newSinhHoat);
                    MessageBox.Show($"Đã tạo lịch sinh hoạt thành công! ID: {sinhHoatID}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo lịch sinh hoạt: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            // Điểm danh
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn buổi sinh hoạt để điểm danh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
                
                // Sử dụng FormSua để chỉnh sửa điểm danh
                var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                var formSua = new FormSua(sinhHoat);
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formSua, donViData);
                
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedSinhHoat = (SinhHoatChiBo)formSua.GetData();
                    _sinhHoatService.UpdateSinhHoatChiBo(updatedSinhHoat);
                    MessageBox.Show("Đã cập nhật điểm danh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi điểm danh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void metroButton3_Click(object sender, EventArgs e)
        {
            // Thêm nội dung cho buổi sinh hoạt
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn buổi sinh hoạt để thêm nội dung!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
                
                // Sử dụng FormSua để chỉnh sửa nội dung sinh hoạt
                var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                var formSua = new FormSua(sinhHoat);
                var donViData = _donViService.GetDonViData();
                RefreshDonViData(formSua, donViData);
                
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    var updatedSinhHoat = (SinhHoatChiBo)formSua.GetData();
                    _sinhHoatService.UpdateSinhHoatChiBo(updatedSinhHoat);
                    MessageBox.Show("Đã cập nhật nội dung sinh hoạt thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nội dung: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            // Upload file nghị quyết
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn buổi sinh hoạt để upload file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
                
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Word Documents (*.doc;*.docx)|*.doc;*.docx|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*",
                    Title = "Chọn file nghị quyết"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // TODO: Implement file upload logic
                    MessageBox.Show($"Đã chọn file: {openFileDialog.FileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi upload file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xem chi tiết khi double click
            if (e.RowIndex >= 0)
            {
                try
                {
                    var selectedRow = dataGridView1.Rows[e.RowIndex];
                    var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
                    
                    var sinhHoat = _sinhHoatService.GetSinhHoatChiBoById(sinhHoatID);
                    var formXemChiTiet = new FormXemChiTiet(sinhHoat);
                    formXemChiTiet.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Xóa khi nhấn Delete
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa buổi sinh hoạt này?", 
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        var selectedRow = dataGridView1.SelectedRows[0];
                        var sinhHoatID = Convert.ToInt32(selectedRow.Cells["SinhHoatID"].Value);
                        
                        _sinhHoatService.DeleteSinhHoatChiBo(sinhHoatID);
                        MessageBox.Show("Đã xóa buổi sinh hoạt thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Reload data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Refresh dữ liệu đơn vị cho form
        /// </summary>
        public void RefreshDonViData(Form form, List<DonViSimplified> donViData)
        {
            if (form == null) return;
            BindDonViComboBoxes(form, donViData);
        }

        /// <summary>
        /// Bind dữ liệu đơn vị vào các ComboBox trong form
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
