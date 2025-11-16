using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Services;
using QuanLyDangVien.DTOs;
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
            
            // Debug: Kiểm tra dữ liệu đầu vào
            if (existingData == null)
            {
                System.Diagnostics.Debug.WriteLine("Error: existingData is null in FormSua constructor");
                throw new ArgumentNullException(nameof(existingData), "Dữ liệu đầu vào không được null");
            }
            
            _dataObject = existingData;
            _dataType = existingData.GetType();
            
            System.Diagnostics.Debug.WriteLine($"FormSua initialized with object type: {_dataType.Name}");
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            _controlsDictionary = new Dictionary<string, Control>();
            _propertiesDictionary = new Dictionary<string, PropertyInfo>();

            // Đặt title cho form - đẹp hơn
            string displayName = _dataType.Name;
            if (displayName == "NguoiDung") displayName = "Người dùng";
            else if (displayName == "DangVien") displayName = "Đảng viên";
            else if (displayName == "QuanNhan") displayName = "Quân nhân";
            else if (displayName == "DonVi") displayName = "Đơn vị";
            this.Text = $"Chỉnh sửa {displayName}";

            // Tạo dynamic controls
            TaoDynamicControls();

            // Load dữ liệu vào controls
            LoadDataToControls();
            // Thêm TextBox "Giá trị cũ" trước các ComboBox
            AddOldValueTextBoxesForComboBoxes();
            
            // Setup DonVi ComboBoxes nếu là DangVien
            if (_dataType.Name == "DangVien")
            {
                SetupDonViComboBoxes();
            }
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

        /// <summary>
        /// Thêm TextBox read-only "Giá trị cũ: {Data}" trước ComboBox
        /// </summary>
        private void AddOldValueTextBoxesForComboBoxes()
        {
            try
            {
                foreach (var kvp in _controlsDictionary)
                {
                    string propertyName = kvp.Key;
                    Control inputControl = kvp.Value;

                    var property = _propertiesDictionary.ContainsKey(propertyName)
                        ? _propertiesDictionary[propertyName]
                        : null;
                    if (property == null) continue;

                    var comboBox = inputControl as ComboBox;
                    if (comboBox == null) continue;

                    object originalValue = property.GetValue(_dataObject);
                    string displayText = string.Empty;

                    if (originalValue != null)
                    {
                        if (!string.IsNullOrEmpty(comboBox.ValueMember) &&
                            !string.IsNullOrEmpty(comboBox.DisplayMember) &&
                            comboBox.DataSource is System.Collections.IEnumerable list)
                        {
                            foreach (var item in list)
                            {
                                var valProp = item.GetType().GetProperty(comboBox.ValueMember);
                                var dispProp = item.GetType().GetProperty(comboBox.DisplayMember);
                                if (valProp != null && dispProp != null)
                                {
                                    var val = valProp.GetValue(item, null);
                                    if (string.Equals(Convert.ToString(val), Convert.ToString(originalValue), StringComparison.OrdinalIgnoreCase))
                                    {
                                        displayText = Convert.ToString(dispProp.GetValue(item, null));
                                        break;
                                    }
                                }
                            }
                        }
                        if (string.IsNullOrWhiteSpace(displayText))
                        {
                            displayText = Convert.ToString(originalValue);
                        }
                    }

                    var parent = comboBox.Parent;
                    if (parent != null)
                    {
                        int comboBoxX = comboBox.Left;
                        int comboBoxY = comboBox.Top;
                        int comboBoxWidth = comboBox.Width;
                        int comboBoxHeight = comboBox.Height;

                        // Tạo TextBox read-only hiển thị "Giá trị cũ: {Data}" với cùng chiều cao ComboBox
                        TextBox txtOldValue = new TextBox
                        {
                            ReadOnly = true,
                            Text = $"Giá trị cũ: {displayText}",
                            Font = new Font("Arial", 12, FontStyle.Regular),
                            BackColor = Color.FromArgb(240, 240, 240), // Màu nền xám nhạt để thể hiện read-only
                            BorderStyle = BorderStyle.FixedSingle,
                            Width = 250, // Độ rộng cố định
                            Height = comboBoxHeight  // Cùng chiều cao với ComboBox
                        };

                        // Đặt vị trí TextBox (bên trái ComboBox, cùng vị trí Y)
                        txtOldValue.Location = new Point(comboBoxX, comboBoxY);
                        parent.Controls.Add(txtOldValue);

                        // Di chuyển ComboBox sang phải và điều chỉnh độ rộng
                        int spacing = 10; // Khoảng cách giữa TextBox và ComboBox
                        int comboBoxNewWidth = comboBoxWidth - txtOldValue.Width - spacing;

                        // Đảm bảo ComboBox không quá nhỏ
                        if (comboBoxNewWidth < 150)
                        {
                            comboBoxNewWidth = 150;
                        }

                        comboBox.Left = comboBoxX + txtOldValue.Width + spacing;
                        comboBox.Width = comboBoxNewWidth;
                    }

                    // Thử set SelectedValue cho ComboBox dựa trên giá trị từ TextBox
                    if (!string.IsNullOrWhiteSpace(displayText))
                    {
                        TrySelectComboBoxValue(comboBox, property, displayText);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddOldValueTextBoxesForComboBoxes error: {ex.Message}");
            }
        }

        /// <summary>
        /// Thử chọn giá trị trong ComboBox dựa trên displayText từ TextBox "Giá trị cũ"
        /// </summary>
        private void TrySelectComboBoxValue(ComboBox comboBox, PropertyInfo property, string displayText)
        {
            try
            {
                // Lấy ComboBoxDataAttribute từ property
                var comboDataAttr = property.GetCustomAttribute<ComboBoxDataAttribute>();
                if (comboDataAttr != null && comboDataAttr.Items != null)
                {
                    // Ưu tiên: So sánh displayText với Value của các ComboBoxItem (giá trị hiển thị)
                    foreach (var item in comboDataAttr.Items)
                    {
                        string itemValue = Convert.ToString(item.Value);
                        if (string.Equals(itemValue, displayText, StringComparison.OrdinalIgnoreCase))
                        {
                            // Tìm thấy match với Value, set SelectedValue = Key
                            try
                            {
                                comboBox.SelectedValue = item.Key;
                                System.Diagnostics.Debug.WriteLine($"Đã set SelectedValue = {item.Key} cho ComboBox {property.Name} (match với Value: {itemValue})");
                                return; // Thành công, thoát
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Lỗi khi set SelectedValue: {ex.Message}");
                            }
                        }
                    }

                    // Nếu không match với Value, thử so sánh với Key (trường hợp database lưu Key)
                    foreach (var item in comboDataAttr.Items)
                    {
                        string itemKey = Convert.ToString(item.Key);
                        if (string.Equals(itemKey, displayText, StringComparison.OrdinalIgnoreCase))
                        {
                            // Tìm thấy match với Key, set SelectedValue = Key
                            try
                            {
                                comboBox.SelectedValue = item.Key;
                                System.Diagnostics.Debug.WriteLine($"Đã set SelectedValue = {item.Key} cho ComboBox {property.Name} (match với Key: {itemKey})");
                                return; // Thành công, thoát
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Lỗi khi set SelectedValue: {ex.Message}");
                            }
                        }
                    }
                }
                else if (property.PropertyType.IsEnum ||
                         (Nullable.GetUnderlyingType(property.PropertyType)?.IsEnum ?? false))
                {
                    // Xử lý cho Enum
                    Type enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    try
                    {
                        // Thử parse displayText thành enum value
                        object enumValue = Enum.Parse(enumType, displayText, true);
                        comboBox.SelectedValue = enumValue;
                        System.Diagnostics.Debug.WriteLine($"Đã set SelectedValue = {enumValue} cho Enum ComboBox {property.Name}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Không thể parse enum từ displayText: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TrySelectComboBoxValue error: {ex.Message}");
            }
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

                // Kiểm tra _dataObject có null không
                if (_dataObject == null)
                {
                    MessageBox.Show("Lỗi: Đối tượng dữ liệu không tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save data from controls using FormHelper
                FormHelper.SaveDataFromControls(_dataObject, _controlsDictionary, _propertiesDictionary);

                // Debug: Kiểm tra dữ liệu sau khi save
                System.Diagnostics.Debug.WriteLine($"Data saved successfully. Object type: {_dataObject.GetType().Name}");

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
            // Kiểm tra _dataObject có null không
            if (_dataObject == null)
            {
                System.Diagnostics.Debug.WriteLine("Warning: _dataObject is null in GetData()");
                return null;
            }

            // Debug: Log thông tin về object
            System.Diagnostics.Debug.WriteLine($"GetData() returning object of type: {_dataObject.GetType().Name}");
            
            return _dataObject;
        }

        /// <summary>
        /// Setup DonVi ComboBoxes với logic cascade
        /// </summary>
        private void SetupDonViComboBoxes()
        {
            try
            {
                var donViService = new DonViService();
                var allDonVi = donViService.GetAll();

                // DonViCap1: các DonVi không có CapTrenID
                if (_controlsDictionary.ContainsKey("DonViCap1"))
                {
                    var cboDonViCap1 = _controlsDictionary["DonViCap1"] as ComboBox;
                    if (cboDonViCap1 != null)
                    {
                        var donViCap1List = allDonVi.Where(dv => !dv.CapTrenID.HasValue)
                            .Select(dv => new { Key = dv.TenDonVi, Value = dv.TenDonVi })
                            .ToList();
                        cboDonViCap1.DataSource = donViCap1List;
                        cboDonViCap1.DisplayMember = "Value";
                        cboDonViCap1.ValueMember = "Key";
                        cboDonViCap1.SelectedIndexChanged += CboDonViCap1_SelectedIndexChanged;
                        
                        // Load giá trị hiện tại nếu có (từ DonViCap1 property hoặc tính từ DonViID)
                        var property = _propertiesDictionary.ContainsKey("DonViCap1") 
                            ? _propertiesDictionary["DonViCap1"] : null;
                        string currentDonViCap1 = null;
                        if (property != null)
                        {
                            currentDonViCap1 = property.GetValue(_dataObject) as string;
                        }
                        
                        // Nếu không có DonViCap1, tính từ DonViID
                        if (string.IsNullOrEmpty(currentDonViCap1))
                        {
                            var donViIDProperty = _propertiesDictionary.ContainsKey("DonViID") 
                                ? _propertiesDictionary["DonViID"] : null;
                            if (donViIDProperty != null)
                            {
                                var currentDonViID = Convert.ToInt32(donViIDProperty.GetValue(_dataObject) ?? 0);
                                if (currentDonViID > 0)
                                {
                                    // Tính DonViCap1 từ DonViID
                                    var currentDonVi = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonViID);
                                    if (currentDonVi != null && currentDonVi.CapTrenID.HasValue)
                                    {
                                        var donViCap2 = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonVi.CapTrenID.Value);
                                        if (donViCap2 != null && donViCap2.CapTrenID.HasValue)
                                        {
                                            var donViCap1 = allDonVi.FirstOrDefault(dv => dv.DonViID == donViCap2.CapTrenID.Value);
                                            if (donViCap1 != null)
                                            {
                                                currentDonViCap1 = donViCap1.TenDonVi;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (!string.IsNullOrEmpty(currentDonViCap1))
                        {
                            cboDonViCap1.SelectedValue = currentDonViCap1;
                        }
                    }
                }

                // DonViCap2: sẽ được load khi chọn DonViCap1
                if (_controlsDictionary.ContainsKey("DonViCap2"))
                {
                    var cboDonViCap2 = _controlsDictionary["DonViCap2"] as ComboBox;
                    if (cboDonViCap2 != null)
                    {
                        cboDonViCap2.SelectedIndexChanged += CboDonViCap2_SelectedIndexChanged;
                        
                        // Load giá trị hiện tại nếu có (từ DonViCap2 property hoặc tính từ DonViID)
                        var property = _propertiesDictionary.ContainsKey("DonViCap2") 
                            ? _propertiesDictionary["DonViCap2"] : null;
                        string currentDonViCap2 = null;
                        if (property != null)
                        {
                            currentDonViCap2 = property.GetValue(_dataObject) as string;
                        }
                        
                        // Nếu không có DonViCap2, tính từ DonViID
                        if (string.IsNullOrEmpty(currentDonViCap2))
                        {
                            var donViIDProperty = _propertiesDictionary.ContainsKey("DonViID") 
                                ? _propertiesDictionary["DonViID"] : null;
                            if (donViIDProperty != null)
                            {
                                var currentDonViID = Convert.ToInt32(donViIDProperty.GetValue(_dataObject) ?? 0);
                                if (currentDonViID > 0)
                                {
                                    // Tính DonViCap2 từ DonViID
                                    var currentDonVi = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonViID);
                                    if (currentDonVi != null && currentDonVi.CapTrenID.HasValue)
                                    {
                                        var donViCap2 = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonVi.CapTrenID.Value);
                                        if (donViCap2 != null)
                                        {
                                            currentDonViCap2 = donViCap2.TenDonVi;
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (!string.IsNullOrEmpty(currentDonViCap2))
                        {
                            // Cần load DonViCap2 list trước
                            LoadDonViCap2List(currentDonViCap2);
                        }
                    }
                }

                // DonViID: sẽ được load khi chọn DonViCap2
                if (_controlsDictionary.ContainsKey("DonViID"))
                {
                    var cboDonViID = _controlsDictionary["DonViID"] as ComboBox;
                    if (cboDonViID != null)
                    {
                        // Load giá trị hiện tại nếu có
                        var property = _propertiesDictionary.ContainsKey("DonViID") 
                            ? _propertiesDictionary["DonViID"] : null;
                        if (property != null)
                        {
                            var currentDonViID = property.GetValue(_dataObject);
                            if (currentDonViID != null && Convert.ToInt32(currentDonViID) > 0)
                            {
                                // Cần load DonViID list trước
                                LoadDonViIDList(Convert.ToInt32(currentDonViID));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi setup DonVi ComboBoxes: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDonViCap2List(string selectedDonViCap2)
        {
            try
            {
                var donViService = new DonViService();
                var allDonVi = donViService.GetAll();
                
                // Tìm DonViCap2
                var donViCap2 = allDonVi.FirstOrDefault(dv => dv.TenDonVi == selectedDonViCap2);
                if (donViCap2 != null && donViCap2.CapTrenID.HasValue)
                {
                    // Tìm DonViCap1 từ DonViCap2
                    var donViCap1 = allDonVi.FirstOrDefault(dv => dv.DonViID == donViCap2.CapTrenID.Value);
                    if (donViCap1 != null && _controlsDictionary.ContainsKey("DonViCap1"))
                    {
                        var cboDonViCap1 = _controlsDictionary["DonViCap1"] as ComboBox;
                        if (cboDonViCap1 != null)
                        {
                            // Tạm thời tắt event để tránh trigger cascade
                            cboDonViCap1.SelectedIndexChanged -= CboDonViCap1_SelectedIndexChanged;
                            cboDonViCap1.SelectedValue = donViCap1.TenDonVi;
                            cboDonViCap1.SelectedIndexChanged += CboDonViCap1_SelectedIndexChanged;
                            
                            // Load DonViCap2 list từ DonViCap1
                            var donViCap2List = allDonVi.Where(dv => dv.CapTrenID == donViCap1.DonViID)
                                .Select(dv => new { Key = dv.TenDonVi, Value = dv.TenDonVi })
                                .ToList();
                            
                            if (_controlsDictionary.ContainsKey("DonViCap2"))
                            {
                                var cboDonViCap2 = _controlsDictionary["DonViCap2"] as ComboBox;
                                if (cboDonViCap2 != null)
                                {
                                    cboDonViCap2.DataSource = donViCap2List;
                                    cboDonViCap2.DisplayMember = "Value";
                                    cboDonViCap2.ValueMember = "Key";
                                    cboDonViCap2.Enabled = donViCap2List.Count > 0;
                                    cboDonViCap2.SelectedValue = selectedDonViCap2;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load DonViCap2 list: {ex.Message}");
            }
        }

        private void LoadDonViIDList(int currentDonViID)
        {
            try
            {
                var donViService = new DonViService();
                var allDonVi = donViService.GetAll();
                
                var currentDonVi = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonViID);
                if (currentDonVi != null && currentDonVi.CapTrenID.HasValue)
                {
                    // Tìm DonViCap2 (cấp trên của DonViID hiện tại)
                    var donViCap2 = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonVi.CapTrenID.Value);
                    if (donViCap2 != null)
                    {
                        // Load DonViCap2 list trước
                        LoadDonViCap2List(donViCap2.TenDonVi);
                        
                        // Load DonViID list từ DonViCap2
                        if (_controlsDictionary.ContainsKey("DonViID"))
                        {
                            var cboDonViID = _controlsDictionary["DonViID"] as ComboBox;
                            if (cboDonViID != null)
                            {
                                var donViIDList = allDonVi.Where(dv => dv.CapTrenID == donViCap2.DonViID)
                                    .Select(dv => new { Key = dv.DonViID, Value = dv.TenDonVi })
                                    .ToList();
                                cboDonViID.DataSource = donViIDList;
                                cboDonViID.DisplayMember = "Value";
                                cboDonViID.ValueMember = "Key";
                                cboDonViID.Enabled = donViIDList.Count > 0;
                                cboDonViID.SelectedValue = currentDonViID;
                            }
                        }
                    }
                }
                else if (currentDonVi != null && !currentDonVi.CapTrenID.HasValue)
                {
                    // Nếu DonViID hiện tại không có CapTrenID, có thể nó là DonViCap1
                    // Trong trường hợp này, không có DonViCap2 và DonViID sẽ là chính nó
                    if (_controlsDictionary.ContainsKey("DonViID"))
                    {
                        var cboDonViID = _controlsDictionary["DonViID"] as ComboBox;
                        if (cboDonViID != null)
                        {
                            var donViIDList = new[] { new { Key = currentDonViID, Value = currentDonVi.TenDonVi } }.ToList();
                            cboDonViID.DataSource = donViIDList;
                            cboDonViID.DisplayMember = "Value";
                            cboDonViID.ValueMember = "Key";
                            cboDonViID.Enabled = true;
                            cboDonViID.SelectedValue = currentDonViID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load DonViID list: {ex.Message}");
            }
        }

        private void CboDonViCap1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cboDonViCap1 = sender as ComboBox;
                if (cboDonViCap1 == null || cboDonViCap1.SelectedValue == null) return;

                string selectedDonViCap1 = cboDonViCap1.SelectedValue.ToString();
                var donViService = new DonViService();
                var allDonVi = donViService.GetAll();

                // Tìm DonViID của DonViCap1 đã chọn
                var donViCap1 = allDonVi.FirstOrDefault(dv => dv.TenDonVi == selectedDonViCap1);
                if (donViCap1 == null) return;

                // Load DonViCap2: các DonVi có CapTrenID = DonViCap1.DonViID
                if (_controlsDictionary.ContainsKey("DonViCap2"))
                {
                    var cboDonViCap2 = _controlsDictionary["DonViCap2"] as ComboBox;
                    if (cboDonViCap2 != null)
                    {
                        var donViCap2List = allDonVi.Where(dv => dv.CapTrenID == donViCap1.DonViID)
                            .Select(dv => new { Key = dv.TenDonVi, Value = dv.TenDonVi })
                            .ToList();
                        cboDonViCap2.DataSource = donViCap2List;
                        cboDonViCap2.DisplayMember = "Value";
                        cboDonViCap2.ValueMember = "Key";
                        cboDonViCap2.Enabled = donViCap2List.Count > 0;
                        cboDonViCap2.SelectedIndex = -1;
                    }
                }

                // Reset DonViID
                if (_controlsDictionary.ContainsKey("DonViID"))
                {
                    var cboDonViID = _controlsDictionary["DonViID"] as ComboBox;
                    if (cboDonViID != null)
                    {
                        cboDonViID.DataSource = null;
                        cboDonViID.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load DonViCap2: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboDonViCap2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cboDonViCap2 = sender as ComboBox;
                if (cboDonViCap2 == null || cboDonViCap2.SelectedValue == null) return;

                string selectedDonViCap2 = cboDonViCap2.SelectedValue.ToString();
                var donViService = new DonViService();
                var allDonVi = donViService.GetAll();

                // Tìm DonViID của DonViCap2 đã chọn
                var donViCap2 = allDonVi.FirstOrDefault(dv => dv.TenDonVi == selectedDonViCap2);
                if (donViCap2 == null) return;

                // Load DonViID: các DonVi có CapTrenID = DonViCap2.DonViID
                if (_controlsDictionary.ContainsKey("DonViID"))
                {
                    var cboDonViID = _controlsDictionary["DonViID"] as ComboBox;
                    if (cboDonViID != null)
                    {
                        var donViIDList = allDonVi.Where(dv => dv.CapTrenID == donViCap2.DonViID)
                            .Select(dv => new { Key = dv.DonViID, Value = dv.TenDonVi })
                            .ToList();
                        cboDonViID.DataSource = donViIDList;
                        cboDonViID.DisplayMember = "Value";
                        cboDonViID.ValueMember = "Key";
                        cboDonViID.Enabled = donViIDList.Count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load DonViID: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật control trong controlsDictionary (dùng khi thay thế ComboBox bằng TextBox)
        /// </summary>
        public void UpdateControlInDictionary(string controlName, Control newControl)
        {
            if (_controlsDictionary.ContainsKey(controlName))
            {
                _controlsDictionary[controlName] = newControl;
            }
        }
    }
}
