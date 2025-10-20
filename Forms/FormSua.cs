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

        private int TinhToanChieuCaoPanel(List<PropertyInfo> properties)
        {
            int totalHeight = 10; // padding top

            foreach (PropertyInfo property in properties)
            {
                var controlTypeAttr = property.GetCustomAttribute<ControlTypeAttribute>();
                Type propertyType = property.PropertyType;
                Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                ControlInputType inputType;
                if (controlTypeAttr != null)
                {
                    inputType = controlTypeAttr.InputType;
                }
                else
                {
                    if (underlyingType == typeof(bool))
                        inputType = ControlInputType.CheckBox;
                    else if (underlyingType == typeof(DateTime))
                        inputType = ControlInputType.DateTimePicker;
                    else if (underlyingType.IsEnum)
                        inputType = ControlInputType.ComboBox;
                    else if (underlyingType == typeof(int) || underlyingType == typeof(long) ||
                             underlyingType == typeof(decimal) || underlyingType == typeof(double))
                        inputType = ControlInputType.NumericUpDown;
                    else
                        inputType = ControlInputType.TextBox;
                }

                // Tính chiều cao dựa trên loại control
                if (inputType == ControlInputType.RichTextBox)
                {
                    totalHeight += 260; // RichTextBox height
                }
                else if (inputType == ControlInputType.PictureBox)
                {
                    totalHeight += 260; // PictureBox container height
                }
                else
                {
                    totalHeight += 65; // Normal control height
                }
            }

            return totalHeight;
        }

        private void TaoControlsChoPanel(FlowLayoutPanel panel, List<PropertyInfo> properties, int controlWidth)
        {
            int labelWidth = 150;

            foreach (PropertyInfo property in properties)
            {
                // Kiểm tra ReadOnly attribute
                var readOnlyAttr = property.GetCustomAttribute<ReadOnlyFieldAttribute>();
                bool isReadOnly = readOnlyAttr != null && readOnlyAttr.IsReadOnly;

                // Lấy display name
                var displayNameAttr = property.GetCustomAttribute<QuanLyDangVien.Attributes.DisplayNameAttribute>();
                string displayName = displayNameAttr != null ? displayNameAttr.Name : GetDisplayName(property.Name);

                // Kiểm tra required
                var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                bool isRequired = requiredAttr != null && requiredAttr.IsRequired;

                // Container cho mỗi field
                Panel fieldContainer = new Panel();
                fieldContainer.AutoSize = true;
                fieldContainer.Padding = new Padding(5);
                fieldContainer.Width = controlWidth;

                // Tạo label
                Label label = new Label();
                label.Text = displayName + (isRequired ? " *" : "") + ":";
                label.Location = new Point(0, 5);
                label.Size = new Size(labelWidth, 23);
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                if (isRequired)
                {
                    label.ForeColor = Color.FromArgb(209, 17, 65);
                }
                fieldContainer.Controls.Add(label);

                // Tạo control
                Control inputControl = TaoControlTheoAttribute(property);
                inputControl.Location = new Point(0, 30);
                inputControl.Name = property.Name;

                // Set kích thước
                if (inputControl is RichTextBox)
                {
                    inputControl.Size = new Size(controlWidth - 10, 400);
                    fieldContainer.Height = 460;
                }
                else if (inputControl is Panel) // PictureBox container
                {
                    inputControl.Size = new Size(controlWidth - 10, 200);
                    fieldContainer.Height = 260;
                }
                else
                {
                    inputControl.Size = new Size(controlWidth - 10, 25);
                    fieldContainer.Height = 65;
                }

                // Set ReadOnly
                if (isReadOnly)
                {
                    if (inputControl is TextBox textBox)
                        textBox.ReadOnly = true;
                    else if (inputControl is RichTextBox richTextBox)
                        richTextBox.ReadOnly = true;
                    else
                        inputControl.Enabled = false;
                }

                fieldContainer.Controls.Add(inputControl);

                // Lưu vào dictionary
                _controlsDictionary[property.Name] = inputControl;
                _propertiesDictionary[property.Name] = property;

                panel.Controls.Add(fieldContainer);
            }
        }

        private Control TaoControlTheoAttribute(PropertyInfo property)
        {
            var controlTypeAttr = property.GetCustomAttribute<ControlTypeAttribute>();

            if (controlTypeAttr != null)
            {
                switch (controlTypeAttr.InputType)
                {
                    case ControlInputType.RichTextBox:
                        return TaoRichTextBox();

                    case ControlInputType.ComboBox:
                        return TaoComboBox(property);

                    case ControlInputType.PictureBox:
                        return TaoPictureBoxControl(property);

                    case ControlInputType.NumericUpDown:
                        return TaoNumericUpDown();

                    case ControlInputType.CheckBox:
                        return TaoCheckBox();

                    case ControlInputType.DateTimePicker:
                        return TaoDateTimePicker();

                    default:
                        return TaoTextBox();
                }
            }

            return TaoControlTheoKieuDuLieu(property);
        }

        private TextBox TaoTextBox()
        {
            TextBox txt = new TextBox();
            txt.Font = new Font("Segoe UI", 10);
            return txt;
        }

        private CheckBox TaoCheckBox()
        {
            CheckBox cb = new CheckBox();
            cb.Font = new Font("Segoe UI", 10);
            cb.AutoSize = true;
            return cb;
        }

        private RichTextBox TaoRichTextBox()
        {
            RichTextBox rtb = new RichTextBox();
            rtb.BorderStyle = BorderStyle.FixedSingle;
            rtb.Font = new Font("Segoe UI", 10);
            return rtb;
        }

        private ComboBox TaoComboBox(PropertyInfo property)
        {
            ComboBox cb = new ComboBox();
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Standard;
            cb.Font = new Font("Segoe UI", 10);

            var comboDataAttr = property.GetCustomAttribute<ComboBoxDataAttribute>();
            if (comboDataAttr != null && comboDataAttr.Items != null)
            {
                cb.Items.AddRange(comboDataAttr.Items);
            }
            else if (property.PropertyType.IsEnum ||
                     (Nullable.GetUnderlyingType(property.PropertyType)?.IsEnum ?? false))
            {
                Type enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                cb.DataSource = Enum.GetValues(enumType);
            }

            return cb;
        }

        private Panel TaoPictureBoxControl(PropertyInfo property)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(200, 150);
            pictureBox.Location = new Point(5, 5);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.LightGray;
            pictureBox.Name = "PictureBox_" + property.Name;
            panel.Controls.Add(pictureBox);

            Button btnChonAnh = new Button();
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.Location = new Point(210, 5);
            btnChonAnh.Size = new Size(100, 30);
            btnChonAnh.BackColor = Color.FromArgb(0, 174, 219);
            btnChonAnh.ForeColor = Color.White;
            btnChonAnh.FlatStyle = FlatStyle.Flat;
            btnChonAnh.Cursor = Cursors.Hand;
            btnChonAnh.Click += (s, e) => ChonAnh(pictureBox);
            panel.Controls.Add(btnChonAnh);

            Button btnXoaAnh = new Button();
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.Location = new Point(210, 40);
            btnXoaAnh.Size = new Size(100, 30);
            btnXoaAnh.BackColor = Color.FromArgb(209, 17, 65);
            btnXoaAnh.ForeColor = Color.White;
            btnXoaAnh.FlatStyle = FlatStyle.Flat;
            btnXoaAnh.Cursor = Cursors.Hand;
            btnXoaAnh.Click += (s, e) => { pictureBox.Image = null; pictureBox.Tag = null; };
            panel.Controls.Add(btnXoaAnh);

            Label lblGuide = new Label();
            lblGuide.Text = "Kích thước khuyến nghị:\n200x150 pixels";
            lblGuide.Location = new Point(210, 80);
            lblGuide.Size = new Size(150, 40);
            lblGuide.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblGuide.ForeColor = Color.Gray;
            panel.Controls.Add(lblGuide);

            return panel;
        }

        private void ChonAnh(PictureBox pictureBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(openFileDialog.FileName);
                        pictureBox.Image = img;
                        pictureBox.Tag = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private NumericUpDown TaoNumericUpDown()
        {
            NumericUpDown nud = new NumericUpDown();
            nud.Maximum = 1000000;
            nud.Minimum = 0;
            nud.Font = new Font("Segoe UI", 10);
            return nud;
        }

        private DateTimePicker TaoDateTimePicker()
        {
            DateTimePicker dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
            dtp.Font = new Font("Segoe UI", 10);
            return dtp;
        }

        private Control TaoControlTheoKieuDuLieu(PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (underlyingType == typeof(bool))
            {
                return TaoCheckBox();
            }
            else if (underlyingType == typeof(DateTime))
            {
                return TaoDateTimePicker();
            }
            else if (underlyingType.IsEnum)
            {
                return TaoComboBox(property);
            }
            else if (underlyingType == typeof(int) || underlyingType == typeof(long) ||
                     underlyingType == typeof(decimal) || underlyingType == typeof(double))
            {
                return TaoNumericUpDown();
            }
            else
            {
                return TaoTextBox();
            }
        }

        private string GetDisplayName(string propertyName)
        {
            return System.Text.RegularExpressions.Regex.Replace(propertyName, "([A-Z])", " $1").Trim();
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
