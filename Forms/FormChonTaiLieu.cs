using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    public partial class FormChonTaiLieu : Form
    {
        private List<TaiLieuHoSoModel> _taiLieuList;
        public TaiLieuHoSoModel SelectedTaiLieu { get; private set; }

        public FormChonTaiLieu(List<TaiLieuHoSoModel> taiLieuList)
        {
            InitializeComponent();
            _taiLieuList = taiLieuList ?? new List<TaiLieuHoSoModel>();
            SetupDataGridView();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dgvTaiLieu.AutoGenerateColumns = false;
            dgvTaiLieu.AllowUserToAddRows = false;
            dgvTaiLieu.ReadOnly = true;
            dgvTaiLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiLieu.MultiSelect = false;

            dgvTaiLieu.Columns.Clear();

            dgvTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenFile",
                DataPropertyName = "TenFile",
                HeaderText = "Tên file",
                Width = 300
            });

            dgvTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiFile",
                DataPropertyName = "LoaiFile",
                HeaderText = "Loại file",
                Width = 100
            });

            dgvTaiLieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Ngày tạo",
                Width = 150
            });

            dgvTaiLieu.CellDoubleClick += DgvTaiLieu_CellDoubleClick;
        }

        private void LoadData()
        {
            dgvTaiLieu.DataSource = _taiLieuList;
        }

        private void DgvTaiLieu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _taiLieuList.Count)
            {
                SelectedTaiLieu = _taiLieuList[e.RowIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvTaiLieu.SelectedRows.Count > 0)
            {
                var selectedIndex = dgvTaiLieu.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < _taiLieuList.Count)
                {
                    SelectedTaiLieu = _taiLieuList[selectedIndex];
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
