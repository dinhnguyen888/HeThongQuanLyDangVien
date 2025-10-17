using MetroFramework.Controls;
using QuanLyDangVien.Models;
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
        public UserControlQuanLyDangVien()
        {
            InitializeComponent();
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

            // Giả sử có cột MaDangVien hoặc ID để định danh
            var id = row.Cells["dangVienIDDataGridViewTextBoxColumn"].Value?.ToString();

            switch (selectedAction)
            {
                case "Xem chi tiết":
                    MessageBox.Show($"Xem chi tiết đảng viên {id}");
                    // TODO: Mở form chi tiết hoặc hiển thị thông tin
                    break;

                case "Sửa thông tin":
                    MessageBox.Show($"Sửa thông tin đảng viên {id}");
                    // TODO: Mở form sửa thông tin
                    break;

                case "Xóa đảng viên":
                    if (MessageBox.Show($"Bạn có chắc muốn xóa đảng viên {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // TODO: Thực hiện xóa đảng viên khỏi DB
                        MessageBox.Show($"Đã xóa đảng viên {id}");
                    }
                    break;
            }

            // Sau khi xử lý xong thì reset chọn trong ComboBox (nếu muốn)
            comboBox.SelectedIndex = -1;
        }


        private void TimKiemBtn_Click(object sender, EventArgs e)
        {

        }

        private void ThemBtn_Click(object sender, EventArgs e)
        {
            FormThaoTacDulieu form = new FormThaoTacDulieu(typeof(DangVien));
            form.Show();
        }

        private void SoluongCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void XoaBtn_Click(object sender, EventArgs e)
        {

        }

        private void DangVienGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}