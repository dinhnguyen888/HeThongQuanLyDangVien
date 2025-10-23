using MetroFramework.Controls;
using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
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
        private List<DangVien> _danhSachDangVien;

        public UserControlQuanLyDangVien()
        {
            InitializeComponent();
            _dangVienService = new DangVienService();
            _danhSachDangVien = new List<DangVien>();
            
            LoadData();
            SetupUI();
        }

        private void SetupUI()
        {
            if (string.IsNullOrEmpty(TimTb.Text))
            {
                TimTb.Text = " ";
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
                BindDataToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid()
        {
            DangVienGridView.DataSource = _danhSachDangVien;
        }

        /// <summary>
        /// Refresh dữ liệu đơn vị cho tất cả ComboBox có DonViID
        /// </summary>
        public void RefreshDonViData(Form form)
        {
            try
            {
                if (form == null)
                    return;

                BindDonViComboBoxes(form);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi refresh dữ liệu đơn vị: {ex.Message}");
            }
        }

        /// <summary>
        /// Duyệt qua tất cả controls trong form để tìm ComboBox DonViID và bind dữ liệu
        /// </summary>
        private void BindDonViComboBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name == "DonViID")
                {
                    // Lấy dữ liệu đơn vị từ service
                    var donViData = DonViService.GetDonViData();

                    comboBox.DataSource = donViData;
                    comboBox.DisplayMember = "TenDonVi";
                    comboBox.ValueMember = "DonViID";
                }

                // Đệ quy duyệt vào các control con
                if (control.HasChildren)
                {
                    BindDonViComboBoxes(control);
                }
            }
        }


        /// <summary>
        /// Recursively refresh ComboBoxes trong control và các child controls
        /// </summary>
        /// 

        /// <summary>
        /// Duyệt qua tất cả controls trong form để tìm ComboBox DonViID và bind dữ liệu
        /// </summary>
 
        //private void RefreshComboBoxesInControl(Form parent)
        //{
        //    foreach (Control control in parent.Controls)
        //    {
        //        if (control is ComboBox comboBox)
        //        {
        //            // Kiểm tra xem ComboBox này có tên "DonViID" không
        //            if (comboBox.Name == "DonViID")
        //            {
        //                // using DonViSwervice to get data
        //                var donViData = DonViService.GetDonViData();
        //                // phân tích donviDât thanh DonViID và TenDonVi
        //                comboBox.DataSource = donViData;
        //                comboBox.DisplayMember = "TenDonVi";
        //                comboBox.ValueMember = "DonViID";

        //            }
        //        }
                
        //        // Recursively check child controls
        //        if (control.HasChildren)
        //        {
        //            RefreshComboBoxesInControl(control);
        //        }
        //    }
        //}

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
            if (comboBox == null) return;
            if (comboBox.SelectedItem == null)
                return;

            string selectedAction = comboBox.SelectedItem.ToString();

            // Lấy dòng hiện tại
            var row = DangVienGridView.CurrentRow;
            if (row == null) return;

            // Lấy ID đảng viên
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
                            formXem.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Sửa thông tin":
                        var dangVienSua = _dangVienService.GetById(dangVienID);
                        if (dangVienSua != null)
                        {
                            FormSua formSua = new FormSua(dangVienSua);
                            if (formSua.ShowDialog() == DialogResult.OK)
                            {
                                // Reload data sau khi sửa
                                LoadData();
                                MessageBox.Show("Cập nhật thông tin đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "Xóa đảng viên":
                        if (MessageBox.Show($"Bạn có chắc muốn xóa đảng viên {dangVienID}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            bool result = _dangVienService.Delete(dangVienID, "admin"); // TODO: Lấy từ session
                            if (result)
                            {
                                LoadData();
                                MessageBox.Show("Xóa đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Xóa đảng viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Sau khi xử lý xong thì reset chọn trong ComboBox
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

                // Tìm kiếm theo họ tên hoặc CCCD
                _danhSachDangVien = _dangVienService.GetAll(hoTen: searchText);
                if (_danhSachDangVien.Count == 0)
                {
                    // Nếu không tìm thấy theo họ tên, thử tìm theo CCCD
                    _danhSachDangVien = _dangVienService.GetAll(soCCCD: searchText);
                }

                BindDataToGrid();
                
                if (_danhSachDangVien.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy đảng viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

                // Gọi hàm refresh trước khi hiển thị form
                RefreshDonViData(form);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Thêm đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SoluongCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Implement pagination if needed
        }

        private void XoaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DangVienGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đảng viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = DangVienGridView.SelectedRows[0];
                var id = selectedRow.Cells["dangVienIDDataGridViewTextBoxColumn"].Value?.ToString();
                
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int dangVienID))
                {
                    MessageBox.Show("Không thể xác định đảng viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa đảng viên đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bool result = _dangVienService.Delete(dangVienID, "admin"); // TODO: Lấy từ session
                    if (result)
                    {
                        LoadData();
                        MessageBox.Show("Xóa đảng viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa đảng viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa đảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangVienGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SoluongCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}