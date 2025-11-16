using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyDangVien
{
    public partial class UserControlQuanTriHeThong : UserControl
    {
        private NguoiDungService _nguoiDungService;
        private AuditLogService _auditLogService;
        private SystemConfigService _systemConfigService;
        private DonViService _donViService;

        // Tab Quản lý người dùng
        private DataGridView dgvNguoiDung;
        private Button btnThemNguoiDung;
        private Button btnSuaNguoiDung;
        private Button btnXoaNguoiDung;
        private Button btnResetPassword;

        // Tab Audit Log
        private DataGridView dgvAuditLog;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private ComboBox cboAction;
        private Button btnFilterAuditLog;

        // Tab Cấu hình
        private Button btnBackup;
        private Button btnRestore;
        private Button btnDangXuat;
        private NumericUpDown nudBackupInterval;
        private Label lblBackupInterval;

        // Buttons để switch giữa các tab
        private Button btnQuanLyNguoiDung;
        private Button btnAuditLog;
        private Button btnCauHinh;

        public UserControlQuanTriHeThong()
        {
            InitializeComponent();
            _nguoiDungService = new NguoiDungService();
            _auditLogService = new AuditLogService();
            _systemConfigService = new SystemConfigService();
            _donViService = new DonViService();

            InitializeButtons();
            InitializeQuanLyNguoiDung();
            InitializeAuditLog();
            InitializeCauHinh();
            
            // Hiển thị tab đầu tiên
            ShowQuanLyNguoiDung();
            
            // Apply permissions after all controls are initialized
            ApplyPermissions();
        }

        private void InitializeButtons()
        {
            panelButtons.Controls.Clear();
            panelButtons.BackColor = Color.FromArgb(248, 249, 250);
            panelButtons.Padding = new Padding(10, 10, 10, 0);

            btnQuanLyNguoiDung = new Button();
            btnQuanLyNguoiDung.Text = "Quản lý người dùng";
            btnQuanLyNguoiDung.Size = new Size(200, 40);
            btnQuanLyNguoiDung.Location = new Point(10, 10);
            btnQuanLyNguoiDung.BackColor = Color.FromArgb(220, 53, 69);
            btnQuanLyNguoiDung.ForeColor = Color.White;
            btnQuanLyNguoiDung.FlatStyle = FlatStyle.Flat;
            btnQuanLyNguoiDung.FlatAppearance.BorderSize = 0;
            btnQuanLyNguoiDung.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnQuanLyNguoiDung.Click += (s, e) => ShowQuanLyNguoiDung();
            panelButtons.Controls.Add(btnQuanLyNguoiDung);

            btnAuditLog = new Button();
            btnAuditLog.Text = "Nhật ký hệ thống";
            btnAuditLog.Size = new Size(200, 40);
            btnAuditLog.Location = new Point(220, 10);
            btnAuditLog.BackColor = Color.FromArgb(108, 117, 125);
            btnAuditLog.ForeColor = Color.White;
            btnAuditLog.FlatStyle = FlatStyle.Flat;
            btnAuditLog.FlatAppearance.BorderSize = 0;
            btnAuditLog.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnAuditLog.Click += (s, e) => ShowAuditLog();
            panelButtons.Controls.Add(btnAuditLog);

            btnCauHinh = new Button();
            btnCauHinh.Text = "Cấu hình hệ thống";
            btnCauHinh.Size = new Size(200, 40);
            btnCauHinh.Location = new Point(430, 10);
            btnCauHinh.BackColor = Color.FromArgb(108, 117, 125);
            btnCauHinh.ForeColor = Color.White;
            btnCauHinh.FlatStyle = FlatStyle.Flat;
            btnCauHinh.FlatAppearance.BorderSize = 0;
            btnCauHinh.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnCauHinh.Click += (s, e) => ShowCauHinh();
            panelButtons.Controls.Add(btnCauHinh);
        }

        private void ShowQuanLyNguoiDung()
        {
            SetActiveButton(btnQuanLyNguoiDung);
            panelContent.Controls.Clear();
            panelContent.Controls.Add(panelQuanLyNguoiDung);
        }

        private void ShowAuditLog()
        {
            SetActiveButton(btnAuditLog);
            panelContent.Controls.Clear();
            panelContent.Controls.Add(panelAuditLog);
        }

        private void ShowCauHinh()
        {
            SetActiveButton(btnCauHinh);
            panelContent.Controls.Clear();
            panelContent.Controls.Add(panelCauHinh);
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons
            btnQuanLyNguoiDung.BackColor = Color.FromArgb(108, 117, 125);
            btnAuditLog.BackColor = Color.FromArgb(108, 117, 125);
            btnCauHinh.BackColor = Color.FromArgb(108, 117, 125);

            // Set active button
            activeButton.BackColor = Color.FromArgb(220, 53, 69);
        }

        #region Tab Quản lý người dùng

        private void InitializeQuanLyNguoiDung()
        {
            panelQuanLyNguoiDung.Controls.Clear();
            panelQuanLyNguoiDung.Padding = new Padding(10);

            // Header panel
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.BackColor = Color.FromArgb(220, 53, 69);

            Label lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ NGƯỜI DÙNG";
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(10, 15);
            headerPanel.Controls.Add(lblTitle);

            // Buttons panel
            Panel buttonsPanel = new Panel();
            buttonsPanel.Dock = DockStyle.Top;
            buttonsPanel.Height = 50;
            buttonsPanel.Padding = new Padding(10, 10, 10, 10);

            btnThemNguoiDung = new Button();
            btnThemNguoiDung.Text = "Thêm người dùng";
            btnThemNguoiDung.Size = new Size(150, 35);
            btnThemNguoiDung.Location = new Point(10, 5);
            btnThemNguoiDung.BackColor = Color.FromArgb(40, 167, 69);
            btnThemNguoiDung.ForeColor = Color.White;
            btnThemNguoiDung.FlatStyle = FlatStyle.Flat;
            btnThemNguoiDung.FlatAppearance.BorderSize = 0;
            btnThemNguoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnThemNguoiDung.Click += BtnThemNguoiDung_Click;
            buttonsPanel.Controls.Add(btnThemNguoiDung);

            btnSuaNguoiDung = new Button();
            btnSuaNguoiDung.Text = "Sửa";
            btnSuaNguoiDung.Size = new Size(100, 35);
            btnSuaNguoiDung.Location = new Point(170, 5);
            btnSuaNguoiDung.BackColor = Color.FromArgb(255, 193, 7);
            btnSuaNguoiDung.ForeColor = Color.White;
            btnSuaNguoiDung.FlatStyle = FlatStyle.Flat;
            btnSuaNguoiDung.FlatAppearance.BorderSize = 0;
            btnSuaNguoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnSuaNguoiDung.Click += BtnSuaNguoiDung_Click;
            buttonsPanel.Controls.Add(btnSuaNguoiDung);

            btnXoaNguoiDung = new Button();
            btnXoaNguoiDung.Text = "Xóa";
            btnXoaNguoiDung.Size = new Size(100, 35);
            btnXoaNguoiDung.Location = new Point(280, 5);
            btnXoaNguoiDung.BackColor = Color.FromArgb(220, 53, 69);
            btnXoaNguoiDung.ForeColor = Color.White;
            btnXoaNguoiDung.FlatStyle = FlatStyle.Flat;
            btnXoaNguoiDung.FlatAppearance.BorderSize = 0;
            btnXoaNguoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnXoaNguoiDung.Click += BtnXoaNguoiDung_Click;
            buttonsPanel.Controls.Add(btnXoaNguoiDung);

            btnResetPassword = new Button();
            btnResetPassword.Text = "Đặt lại mật khẩu";
            btnResetPassword.Size = new Size(150, 35);
            btnResetPassword.Location = new Point(390, 5);
            btnResetPassword.BackColor = Color.FromArgb(23, 162, 184);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnResetPassword.Click += BtnResetPassword_Click;
            buttonsPanel.Controls.Add(btnResetPassword);

            // DataGridView
            dgvNguoiDung = new DataGridView();
            dgvNguoiDung.Dock = DockStyle.Fill;
            dgvNguoiDung.AutoGenerateColumns = false;
            dgvNguoiDung.AllowUserToAddRows = false;
            dgvNguoiDung.ReadOnly = true; // Bỏ edit cell
            dgvNguoiDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguoiDung.MultiSelect = false;
            SetupDataGridViewStyle(dgvNguoiDung);

            panelQuanLyNguoiDung.Controls.Add(dgvNguoiDung);
            panelQuanLyNguoiDung.Controls.Add(buttonsPanel);
            panelQuanLyNguoiDung.Controls.Add(headerPanel);

            LoadNguoiDung();
        }

        private void LoadNguoiDung()
        {
            try
            {
                var users = _nguoiDungService.GetAll();
                dgvNguoiDung.DataSource = users;
                SetupNguoiDungColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupNguoiDungColumns()
        {
            dgvNguoiDung.Columns.Clear();

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NguoiDungID",
                HeaderText = "ID",
                DataPropertyName = "NguoiDungID",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 60,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDangNhap",
                HeaderText = "Tên đăng nhập",
                DataPropertyName = "TenDangNhap",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "VaiTro",
                HeaderText = "Vai trò",
                DataPropertyName = "VaiTro",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 100,
                ReadOnly = true
            });
        }

        private void BtnThemNguoiDung_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền thêm người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FormThem form = new FormThem(typeof(NguoiDung)))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var nguoiDung = form.GetData() as NguoiDung;
                    if (nguoiDung != null)
                    {
                        // Lấy mật khẩu từ form nếu có, nếu không thì mới hỏi
                        string matKhau = nguoiDung.MatKhau;
                        if (string.IsNullOrEmpty(matKhau))
                        {
                            matKhau = ShowInputDialog("Nhập mật khẩu:", "Mật khẩu");
                        }
                        
                        if (!string.IsNullOrEmpty(matKhau))
                        {
                            try
                            {
                                int id = _nguoiDungService.Insert(nguoiDung, matKhau);
                                _auditLogService.LogAction("Insert", "NguoiDung", id);
                                LoadNguoiDung();
                                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void BtnSuaNguoiDung_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền sửa người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = dgvNguoiDung.SelectedRows[0].DataBoundItem as NguoiDung;
            if (selectedUser != null)
            {
                using (FormSua form = new FormSua(selectedUser))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var updated = form.GetData() as NguoiDung;
                        if (updated != null)
                        {
                            try
                            {
                                _nguoiDungService.Update(updated);
                                _auditLogService.LogAction("Update", "NguoiDung", updated.NguoiDungID);
                                LoadNguoiDung();
                                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void BtnXoaNguoiDung_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền xóa người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = dgvNguoiDung.SelectedRows[0].DataBoundItem as NguoiDung;
            if (selectedUser != null)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{selectedUser.TenDangNhap}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _nguoiDungService.Delete(selectedUser.NguoiDungID);
                        _auditLogService.LogAction("Delete", "NguoiDung", selectedUser.NguoiDungID);
                        LoadNguoiDung();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền đặt lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = dgvNguoiDung.SelectedRows[0].DataBoundItem as NguoiDung;
            if (selectedUser != null)
            {
                string newPassword = ShowInputDialog("Nhập mật khẩu mới:", "Đặt lại mật khẩu");
                if (!string.IsNullOrEmpty(newPassword))
                {
                    try
                    {
                        _nguoiDungService.ResetPassword(selectedUser.NguoiDungID, newPassword);
                        _auditLogService.LogAction("Update", "NguoiDung", selectedUser.NguoiDungID, null, "Password reset");
                        MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Tab Audit Log

        private void InitializeAuditLog()
        {
            panelAuditLog.Controls.Clear();
            panelAuditLog.Padding = new Padding(10);

            // Header
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.BackColor = Color.FromArgb(220, 53, 69);

            Label lblTitle = new Label();
            lblTitle.Text = "NHẬT KÝ HỆ THỐNG";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(10, 12);
            headerPanel.Controls.Add(lblTitle);

            // Filter panel
            Panel filterPanel = new Panel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 60;
            filterPanel.Padding = new Padding(10);

            Label lblTuNgay = new Label();
            lblTuNgay.Text = "Từ ngày:";
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(10, 15);
            lblTuNgay.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(lblTuNgay);

            dtpTuNgay = new DateTimePicker();
            dtpTuNgay.Location = new Point(80, 12);
            dtpTuNgay.Size = new Size(150, 25);
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpTuNgay.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(dtpTuNgay);

            Label lblDenNgay = new Label();
            lblDenNgay.Text = "Đến ngày:";
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(250, 15);
            lblDenNgay.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(lblDenNgay);

            dtpDenNgay = new DateTimePicker();
            dtpDenNgay.Location = new Point(320, 12);
            dtpDenNgay.Size = new Size(150, 25);
            dtpDenNgay.Value = DateTime.Now;
            dtpDenNgay.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(dtpDenNgay);

            Label lblAction = new Label();
            lblAction.Text = "Hành động:";
            lblAction.AutoSize = true;
            lblAction.Location = new Point(490, 15);
            lblAction.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(lblAction);

            cboAction = new ComboBox();
            cboAction.Location = new Point(570, 12);
            cboAction.Size = new Size(120, 25);
            cboAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAction.Items.AddRange(new[] { "Tất cả", "Insert", "Update", "Delete", "View" });
            cboAction.SelectedIndex = 0;
            cboAction.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            filterPanel.Controls.Add(cboAction);

            btnFilterAuditLog = new Button();
            btnFilterAuditLog.Text = "Lọc";
            btnFilterAuditLog.Location = new Point(700, 10);
            btnFilterAuditLog.Size = new Size(80, 30);
            btnFilterAuditLog.BackColor = Color.FromArgb(40, 167, 69);
            btnFilterAuditLog.ForeColor = Color.White;
            btnFilterAuditLog.FlatStyle = FlatStyle.Flat;
            btnFilterAuditLog.FlatAppearance.BorderSize = 0;
            btnFilterAuditLog.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnFilterAuditLog.Click += BtnFilterAuditLog_Click;
            filterPanel.Controls.Add(btnFilterAuditLog);

            // DataGridView
            dgvAuditLog = new DataGridView();
            dgvAuditLog.Dock = DockStyle.Fill;
            dgvAuditLog.AutoGenerateColumns = true;
            dgvAuditLog.AllowUserToAddRows = false;
            dgvAuditLog.ReadOnly = true;
            dgvAuditLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill width
            SetupDataGridViewStyle(dgvAuditLog);

            panelAuditLog.Controls.Add(dgvAuditLog);
            panelAuditLog.Controls.Add(filterPanel);
            panelAuditLog.Controls.Add(headerPanel);

            LoadAuditLog();
        }

        private void LoadAuditLog()
        {
            try
            {
                DateTime? tuNgay = dtpTuNgay?.Value.Date;
                DateTime? denNgay = dtpDenNgay?.Value.Date.AddDays(1).AddSeconds(-1);
                string action = cboAction?.SelectedItem?.ToString();
                if (action == "Tất cả") action = null;

                var logs = _auditLogService.GetAll(tuNgay, denNgay, action, null);
                dgvAuditLog.DataSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải nhật ký: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFilterAuditLog_Click(object sender, EventArgs e)
        {
            LoadAuditLog();
        }

        #endregion

        #region Tab Cấu hình

        private void InitializeCauHinh()
        {
            panelCauHinh.Controls.Clear();
            panelCauHinh.Padding = new Padding(20);

            // Header
            Label lblTitle = new Label();
            lblTitle.Text = "CẤU HÌNH HỆ THỐNG";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            lblTitle.ForeColor = Color.FromArgb(220, 53, 69);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);
            panelCauHinh.Controls.Add(lblTitle);

            // Backup section
            GroupBox grpBackup = new GroupBox();
            grpBackup.Text = "Sao lưu dữ liệu";
            grpBackup.Location = new Point(20, 60);
            grpBackup.Size = new Size(600, 150);
            grpBackup.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            lblBackupInterval = new Label();
            lblBackupInterval.Text = "Tần suất sao lưu (ngày):";
            lblBackupInterval.Location = new Point(20, 30);
            lblBackupInterval.AutoSize = true;
            lblBackupInterval.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grpBackup.Controls.Add(lblBackupInterval);

            nudBackupInterval = new NumericUpDown();
            nudBackupInterval.Location = new Point(200, 28);
            nudBackupInterval.Size = new Size(120, 30);
            nudBackupInterval.Minimum = 1;
            nudBackupInterval.Maximum = 30;
            nudBackupInterval.Value = _systemConfigService.GetBackupIntervalDays();
            nudBackupInterval.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            nudBackupInterval.TextAlign = HorizontalAlignment.Center;
            nudBackupInterval.DecimalPlaces = 0;
            nudBackupInterval.ThousandsSeparator = false;
            grpBackup.Controls.Add(nudBackupInterval);

            btnBackup = new Button();
            btnBackup.Text = "Sao lưu ngay";
            btnBackup.Location = new Point(20, 70);
            btnBackup.Size = new Size(150, 40);
            btnBackup.BackColor = Color.FromArgb(40, 167, 69);
            btnBackup.ForeColor = Color.White;
            btnBackup.FlatStyle = FlatStyle.Flat;
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnBackup.Click += BtnBackup_Click;
            grpBackup.Controls.Add(btnBackup);

            btnRestore = new Button();
            btnRestore.Text = "Khôi phục";
            btnRestore.Location = new Point(190, 70);
            btnRestore.Size = new Size(150, 40);
            btnRestore.BackColor = Color.FromArgb(255, 193, 7);
            btnRestore.ForeColor = Color.White;
            btnRestore.FlatStyle = FlatStyle.Flat;
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnRestore.Click += BtnRestore_Click;
            grpBackup.Controls.Add(btnRestore);

            Button btnSaveBackupInterval = new Button();
            btnSaveBackupInterval.Text = "Lưu tần suất";
            btnSaveBackupInterval.Location = new Point(330, 25);
            btnSaveBackupInterval.Size = new Size(120, 30);
            btnSaveBackupInterval.BackColor = Color.FromArgb(23, 162, 184);
            btnSaveBackupInterval.ForeColor = Color.White;
            btnSaveBackupInterval.FlatStyle = FlatStyle.Flat;
            btnSaveBackupInterval.FlatAppearance.BorderSize = 0;
            btnSaveBackupInterval.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnSaveBackupInterval.Click += (s, e) =>
            {
                _systemConfigService.SetBackupIntervalDays((int)nudBackupInterval.Value);
                MessageBox.Show("Đã lưu tần suất sao lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            grpBackup.Controls.Add(btnSaveBackupInterval);

            panelCauHinh.Controls.Add(grpBackup);

            // Đăng xuất section
            GroupBox grpDangXuat = new GroupBox();
            grpDangXuat.Text = "Đăng xuất";
            grpDangXuat.Location = new Point(20, 230);
            grpDangXuat.Size = new Size(600, 100);
            grpDangXuat.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            btnDangXuat = new Button();
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.Location = new Point(20, 30);
            btnDangXuat.Size = new Size(200, 50);
            btnDangXuat.BackColor = Color.FromArgb(220, 53, 69);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnDangXuat.Click += BtnDangXuat_Click;
            grpDangXuat.Controls.Add(btnDangXuat);

            panelCauHinh.Controls.Add(grpDangXuat);
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền sao lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string backupFolder = @"C:\QuanLyDangVien\Server\Backup";
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                string backupFileName = $"QuanLyDangVien_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string backupPath = Path.Combine(backupFolder, backupFileName);

                string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Không tìm thấy chuỗi kết nối database!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string backupQuery = $"BACKUP DATABASE QuanLyDangVien TO DISK = '{backupPath}' WITH FORMAT, INIT, NAME = 'QuanLyDangVien-Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    using (SqlCommand cmd = new SqlCommand(backupQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                _auditLogService.LogAction("Backup", "Database", null, null, $"Backup to {backupPath}");
                MessageBox.Show($"Sao lưu thành công!\nĐường dẫn: {backupPath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sao lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                MessageBox.Show("Chỉ Admin mới có quyền khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Backup files (*.bak)|*.bak";
                dlg.InitialDirectory = @"C:\QuanLyDangVien\Server\Backup";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn khôi phục? Tất cả dữ liệu hiện tại sẽ bị ghi đè!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"]?.ConnectionString;
                            if (string.IsNullOrEmpty(connectionString))
                            {
                                MessageBox.Show("Không tìm thấy chuỗi kết nối trong App.config!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                conn.Open();
                                // Set database to single user mode
                                string setSingleUser = "ALTER DATABASE QuanLyDangVien SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                                using (SqlCommand cmd = new SqlCommand(setSingleUser, conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                // Restore database
                                string restoreQuery = $"RESTORE DATABASE QuanLyDangVien FROM DISK = '{dlg.FileName}' WITH REPLACE";
                                using (SqlCommand cmd = new SqlCommand(restoreQuery, conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                // Set database back to multi user mode
                                string setMultiUser = "ALTER DATABASE QuanLyDangVien SET MULTI_USER";
                                using (SqlCommand cmd = new SqlCommand(setMultiUser, conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            _auditLogService.LogAction("Restore", "Database", null, null, $"Restore from {dlg.FileName}");
                            MessageBox.Show("Khôi phục thành công! Vui lòng khởi động lại ứng dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Restart();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi khôi phục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveInforHelper.ClearSavedInfo();
                Application.Restart();
            }
        }

        #endregion

        #region Helper Methods

        private void SetupDataGridViewStyle(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ReadOnly = true; // Bỏ edit cell
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill width

            // Header styling - màu đỏ đẹp
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 10, 10, 10);
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersHeight = 60;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row styling
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.RowsDefaultCellStyle.Padding = new Padding(10, 10, 10, 10);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 200, 200);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 200, 200);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 45;
        }

        private void ApplyPermissions()
        {
            bool isAdmin = AuthorizationHelper.IsAdmin();
            
            // Tab Quản lý người dùng
            if (btnThemNguoiDung != null) btnThemNguoiDung.Enabled = isAdmin;
            if (btnSuaNguoiDung != null) btnSuaNguoiDung.Enabled = isAdmin;
            if (btnXoaNguoiDung != null) btnXoaNguoiDung.Enabled = isAdmin;
            if (btnResetPassword != null) btnResetPassword.Enabled = isAdmin;
            
            // Tab Cấu hình
            if (btnBackup != null) btnBackup.Enabled = isAdmin;
            if (btnRestore != null) btnRestore.Enabled = isAdmin;
        }

        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 400;
            prompt.Height = 150;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 350 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350, UseSystemPasswordChar = true };
            Button confirmation = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Hủy", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        #endregion
    }
}
