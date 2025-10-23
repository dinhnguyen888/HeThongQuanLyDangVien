using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    // ===== FORM SỬA DỮ LIỆU =====

    public partial class FormSua : Form
    {
        private object _dataObject;
        private Type _dataType;
        private Dictionary<string, Control> _controlsDictionary;
        private Dictionary<string, PropertyInfo> _propertiesDictionary;

        // Constructor cho chế độ chỉnh sửa
        public FormSua(object existingData)
        {
            InitializeComponent();
            _dataObject = existingData;
            _dataType = existingData.GetType();
            InitializeForm();
        }

        private void InitializeForm()
        {
            _controlsDictionary = new Dictionary<string, Control>();
            _propertiesDictionary = new Dictionary<string, PropertyInfo>();

            // Đặt title cho form
            this.Text = $"Chỉnh sửa {_dataType.Name}";

            // Tạo dynamic controls
            TaoDynamicControls();

            // Load dữ liệu vào controls
            LoadDataToControls();
        }

        private void TaoDynamicControls()
        {
            // Lấy tất cả properties
            PropertyInfo[] properties = _dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Phân loại properties theo nhóm sử dụng FormHelper
            List<PropertyInfo> leftGroup, rightGroup, bottomGroup;
            FormHelper.ClassifyProperties(properties, out leftGroup, out rightGroup, out bottomGroup);

            // === MAIN SCROLL PANEL ===
            Panel scrollPanel = new Panel();
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.AutoScroll = true;
            scrollPanel.BackColor = Color.White;

            // Container bên trong scroll panel
            Panel contentPanel = new Panel();
            contentPanel.Location = new Point(0, 0);
            contentPanel.AutoSize = true;
            contentPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            contentPanel.MinimumSize = new Size(960, 0);
            contentPanel.Padding = new Padding(10);

            // === LEFT PANEL (PictureBox, CheckBox, DateTimePicker) ===
            FlowLayoutPanel leftPanel = new FlowLayoutPanel();
            leftPanel.FlowDirection = FlowDirection.TopDown;
            leftPanel.AutoSize = true;
            leftPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftPanel.Location = new Point(10, 10);
            leftPanel.Padding = new Padding(5);
            leftPanel.WrapContents = false;
            leftPanel.Width = 460;

            FormHelper.CreateControlsForPanel(leftPanel, leftGroup, 450, _controlsDictionary, _propertiesDictionary, false, true);

            // === RIGHT PANEL (TextBox, NumericUpDown, ComboBox) ===
            FlowLayoutPanel rightPanel = new FlowLayoutPanel();
            rightPanel.FlowDirection = FlowDirection.TopDown;
            rightPanel.AutoSize = true;
            rightPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightPanel.Location = new Point(480, 10);
            rightPanel.Padding = new Padding(5);
            rightPanel.WrapContents = false;
            rightPanel.Width = 460;

            FormHelper.CreateControlsForPanel(rightPanel, rightGroup, 450, _controlsDictionary, _propertiesDictionary, false, true);

            // Thêm left và right vào content panel trước
            contentPanel.Controls.Add(leftPanel);
            contentPanel.Controls.Add(rightPanel);

            // Force layout để tính toán height
            contentPanel.PerformLayout();
            Application.DoEvents();

            // Bây giờ mới tính toán vị trí Y cho bottom panel
            int leftHeight = leftPanel.Height > 0 ? leftPanel.Height : FormHelper.CalculatePanelHeight(leftGroup);
            int rightHeight = rightPanel.Height > 0 ? rightPanel.Height : FormHelper.CalculatePanelHeight(rightGroup);
            int maxHeight = Math.Max(leftHeight, rightHeight);

            // === BOTTOM PANEL (RichTextBox) ===
            FlowLayoutPanel bottomPanel = new FlowLayoutPanel();
            bottomPanel.FlowDirection = FlowDirection.TopDown;
            bottomPanel.AutoSize = true;
            bottomPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bottomPanel.Location = new Point(10, maxHeight + 30);
            bottomPanel.Padding = new Padding(5);
            bottomPanel.WrapContents = false;
            bottomPanel.Width = 940;

            FormHelper.CreateControlsForPanel(bottomPanel, bottomGroup, 920, _controlsDictionary, _propertiesDictionary, false, true);

            // Thêm bottom panel vào content panel
            contentPanel.Controls.Add(bottomPanel);

            // Thêm content panel vào scroll panel
            scrollPanel.Controls.Add(contentPanel);

            // === BUTTONS ===
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 60;
            buttonPanel.BackColor = Color.FromArgb(240, 240, 240);
            buttonPanel.Padding = new Padding(10);

            Button btnSave = new Button();
            btnSave.Text = "Cập nhật";
            btnSave.Size = new Size(120, 35);
            btnSave.Location = new Point(10, 12);
            btnSave.BackColor = Color.FromArgb(0, 174, 219);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += BtnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            Button btnCancel = new Button();
            btnCancel.Text = "Hủy";
            btnCancel.Size = new Size(120, 35);
            btnCancel.Location = new Point(140, 12);
            btnCancel.BackColor = Color.FromArgb(120, 120, 120);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            buttonPanel.Controls.Add(btnCancel);

            this.Controls.Add(buttonPanel);
            this.Controls.Add(scrollPanel);

            // Thiết lập form
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(800, 600);
        }

   

        private void LoadDataToControls()
        {
            FormHelper.LoadDataToControls(_dataObject, _controlsDictionary, _propertiesDictionary);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs using FormHelper
                if (!FormHelper.ValidateInputs(_controlsDictionary, _propertiesDictionary))
                {
                    return;
                }

                // Save data from controls using FormHelper
                FormHelper.SaveDataFromControls(_dataObject, _controlsDictionary, _propertiesDictionary);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public object GetData()
        {
            return _dataObject;
        }
    }
}
