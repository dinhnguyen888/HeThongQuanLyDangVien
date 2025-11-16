using QuanLyDangVien.Attributes;
using QuanLyDangVien.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace QuanLyDangVien.Helper
{
    /// <summary>
    /// Helper class chứa các hàm tiện ích cho dynamic forms
    /// </summary>
    public static class FormHelper
    {
        /// <summary>
        /// Lấy display name từ property name hoặc DisplayNameAttribute
        /// </summary>
        public static string GetDisplayName(PropertyInfo property)
        {
            var displayNameAttr = property.GetCustomAttribute<QuanLyDangVien.Attributes.DisplayNameAttribute>();
            if (displayNameAttr != null)
            {
                return displayNameAttr.Name;
            }
            
            // Convert PascalCase to spaced text
            return System.Text.RegularExpressions.Regex.Replace(property.Name, "([A-Z])", " $1").Trim();
        }

        /// <summary>
        /// Convert string value sang kiểu dữ liệu target
        /// </summary>
        public static object ConvertValue(string value, Type targetType)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (Nullable.GetUnderlyingType(targetType) != null)
                    return null;
                if (targetType == typeof(string))
                    return string.Empty;
                throw new ArgumentException("Giá trị không được để trống");
            }

            Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType == typeof(int))
                return int.Parse(value);
            else if (underlyingType == typeof(long))
                return long.Parse(value);
            else if (underlyingType == typeof(decimal))
                return decimal.Parse(value);
            else if (underlyingType == typeof(double))
                return double.Parse(value);
            else if (underlyingType == typeof(float))
                return float.Parse(value);
            else if (underlyingType == typeof(DateTime))
                return DateTime.Parse(value);
            else
                return value;
        }

        /// <summary>
        /// Validate các input controls dựa trên RequiredAttribute
        /// </summary>
        public static bool ValidateInputs(Dictionary<string, Control> controlsDictionary, 
            Dictionary<string, PropertyInfo> propertiesDictionary)
        {
            foreach (var kvp in controlsDictionary)
            {
                string propertyName = kvp.Key;
                Control control = kvp.Value;
                PropertyInfo property = propertiesDictionary[propertyName];

                var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttr != null && requiredAttr.IsRequired)
                {
                    bool isEmpty = false;

                    if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                        isEmpty = true;
                    else if (control is RichTextBox richTextBox && string.IsNullOrWhiteSpace(richTextBox.Text))
                        isEmpty = true;
                    else if (control is ComboBox comboBox && comboBox.SelectedIndex == -1)
                        isEmpty = true;

                    if (isEmpty)
                    {
                        string displayName = GetDisplayName(property);
                        MessageBox.Show($"Vui lòng nhập {displayName}!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        control.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Load dữ liệu từ object vào controls
        /// </summary>
        public static void LoadDataToControls(object dataObject, Dictionary<string, Control> controlsDictionary,
            Dictionary<string, PropertyInfo> propertiesDictionary)
        {
            foreach (var kvp in controlsDictionary)
            {
                string propertyName = kvp.Key;
                Control control = kvp.Value;
                PropertyInfo property = propertiesDictionary[propertyName];

                object value = property.GetValue(dataObject);
                if (value == null) continue;

                if (control is TextBox textBox)
                {
                    textBox.Text = value.ToString();
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Text = value.ToString();
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = (bool)value;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = (DateTime)value;
                }
                else if (control is ComboBox comboBox)
                {
                    // Đặt về -1 trước để tránh tự chọn item đầu
                    comboBox.SelectedIndex = -1;
                    // Nếu ComboBox có ValueMember (bind từ ControlFactory)
                    if (!string.IsNullOrEmpty(comboBox.ValueMember))
                    {
                        comboBox.SelectedValue = value;
                        // Nếu chưa match, thử tìm thủ công trong DataSource theo ValueMember
                        if (comboBox.SelectedIndex == -1 && comboBox.DataSource is System.Collections.IEnumerable list)
                        {
                            int index = -1;
                            int i = 0;
                            string target = Convert.ToString(value);
                            foreach (var item in list)
                            {
                                var prop = item.GetType().GetProperty(comboBox.ValueMember);
                                if (prop != null)
                                {
                                    var val = prop.GetValue(item, null);
                                    if (string.Equals(Convert.ToString(val), target, StringComparison.OrdinalIgnoreCase))
                                    {
                                        index = i;
                                        break;
                                    }
                                }
                                i++;
                            }
                            if (index >= 0 && index < comboBox.Items.Count)
                            {
                                comboBox.SelectedIndex = index;
                            }
                        }
                    }
                    else
                    {
                        comboBox.SelectedItem = value;
                    }
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    // Xử lý nullable int - nếu value null thì set về 0
                    if (value == null)
                    {
                        numericUpDown.Value = 0;
                    }
                    else
                    {
                        // Convert sang int trước rồi sang decimal
                        int intValue = Convert.ToInt32(value);
                        numericUpDown.Value = intValue;
                    }
                }
                else if (control is Panel panel)
                {
                    // Kiểm tra nếu là PictureBox
                    PictureBox pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();
                    if (pictureBox != null)
                    {
                        if (value is byte[] imageBytes && imageBytes != null && imageBytes.Length > 0)
                        {
                            try
                            {
                                // Dispose ảnh cũ trước khi load ảnh mới
                                if (pictureBox.Image != null)
                                {
                                    var oldImage = pictureBox.Image;
                                    pictureBox.Image = null;
                                    oldImage.Dispose();
                                }

                                using (var ms = new MemoryStream(imageBytes))
                                {
                                    pictureBox.Image = Image.FromStream(ms);
                                }
                                pictureBox.Tag = imageBytes; // Lưu byte[] vào Tag luôn
                            }
                            catch (Exception ex)
                            {
                                // Nếu không load được ảnh, clear ảnh và log lỗi
                                pictureBox.Image = null;
                                pictureBox.Tag = null;
                                System.Diagnostics.Debug.WriteLine($"Lỗi load ảnh: {ex.Message}");
                            }
                        }
                        else
                        {
                            // Nếu không có ảnh hoặc ảnh rỗng, clear ảnh và tag
                            if (pictureBox.Image != null)
                            {
                                var oldImage = pictureBox.Image;
                                pictureBox.Image = null;
                                oldImage.Dispose();
                            }
                            pictureBox.Tag = null;
                        }
                    }
                    // Kiểm tra nếu là FileDialog (LinkLabel)
                    else
                    {
                        LinkLabel linkLabel = panel.Controls.OfType<LinkLabel>().FirstOrDefault();
                        if (linkLabel != null && value is string filePath)
                        {
                            // Lấy thông tin read-only từ Tag của panel
                            bool isReadOnlyMode = panel.Tag != null && panel.Tag is bool && (bool)panel.Tag;
                            
                            if (!string.IsNullOrWhiteSpace(filePath))
                            {
                                // Nếu là đường dẫn tương đối (Server\...), hiển thị tên file
                                // Nếu là đường dẫn đầy đủ, hiển thị tên file
                                string fileName = Path.GetFileName(filePath);
                                if (string.IsNullOrEmpty(fileName))
                                {
                                    fileName = filePath;
                                }
                                
                                linkLabel.Text = fileName;
                                
                                // Lưu đường dẫn và thông tin read-only vào Tag (sử dụng anonymous object)
                                linkLabel.Tag = new { FilePath = filePath, IsReadOnly = isReadOnlyMode };
                                linkLabel.ForeColor = Color.FromArgb(0, 174, 219);
                                linkLabel.LinkColor = Color.FromArgb(0, 174, 219);
                            }
                            else
                            {
                                linkLabel.Text = "Chưa chọn file";
                                linkLabel.Tag = new { FilePath = (string)null, IsReadOnly = isReadOnlyMode };
                                linkLabel.ForeColor = Color.Gray;
                            }
                        }
                    }
                }

            }
        }
        

        /// <summary>
        /// Lưu dữ liệu từ controls vào object
        /// </summary>
        public static void SaveDataFromControls(object dataObject, Dictionary<string, Control> controlsDictionary,
            Dictionary<string, PropertyInfo> propertiesDictionary)
        {
            foreach (var kvp in controlsDictionary)
            {
                string propertyName = kvp.Key;
                Control control = kvp.Value;
                PropertyInfo property = propertiesDictionary[propertyName];

                if (!property.CanWrite) continue;
                
                // KIỂM TRA TRƯỜNG READONLY - GIỮ NGUYÊN GIÁ TRỊ GỐC
                var readOnlyAttr = property.GetCustomAttribute<ReadOnlyFieldAttribute>();
                if (readOnlyAttr != null && readOnlyAttr.IsReadOnly)
                {
                    // Bỏ qua việc cập nhật từ control, giữ nguyên giá trị hiện tại
                    continue;
                }

                object value = null;

                try
                {
                    if (control is TextBox textBox)
                    {
                        // Nếu TextBox có Tag và là read-only, sử dụng giá trị từ Tag (từ ComboBox đã bị thay thế hoặc DangVienID)
                        if (textBox.ReadOnly && textBox.Tag != null)
                        {
                            // Lấy giá trị từ Tag và convert sang đúng kiểu của property
                            object tagValue = textBox.Tag;
                            if (tagValue != null)
                            {
                                // Nếu kiểu của Tag khớp với property type, sử dụng trực tiếp
                                if (property.PropertyType.IsAssignableFrom(tagValue.GetType()))
                                {
                                    value = tagValue;
                                }
                                else
                                {
                                    // Convert sang đúng kiểu
                                    value = ConvertValue(tagValue.ToString(), property.PropertyType);
                                }
                            }
                        }
                        // Xử lý trường hợp string rỗng cho các trường không bắt buộc
                        else if (string.IsNullOrWhiteSpace(textBox.Text) && property.PropertyType == typeof(string))
                        {
                            var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                            if (requiredAttr == null || !requiredAttr.IsRequired)
                            {
                                value = null; // Trường không bắt buộc có thể null
                            }
                            else
                            {
                                value = ConvertValue(textBox.Text, property.PropertyType);
                            }
                        }
                        else
                        {
                            value = ConvertValue(textBox.Text, property.PropertyType);
                        }
                    }
                    else if (control is RichTextBox richTextBox)
                    {
                        // Tương tự cho RichTextBox
                        if (string.IsNullOrWhiteSpace(richTextBox.Text))
                        {
                            value = null;
                        }
                        else
                        {
                            value = richTextBox.Text;
                        }
                    }
                    else if (control is CheckBox checkBox)
                    {
                        value = checkBox.Checked;
                    }
                    else if (control is DateTimePicker dateTimePicker)
                    {
                        value = dateTimePicker.Value;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        // Kiểm tra nếu ComboBox có ValueMember (được bind từ ControlFactory)
                        if (!string.IsNullOrEmpty(comboBox.ValueMember))
                        {
                            if (comboBox.SelectedIndex == -1)
                            {
                                // Nếu không chọn gì, set null hoặc empty string tùy theo property type
                                if (property.PropertyType == typeof(string))
                                {
                                    value = null;
                                }
                                else
                                {
                                    value = null;
                                }
                            }
                            else
                            {
                                value = comboBox.SelectedValue;
                            }
                        }
                        else
                        {
                            if (comboBox.SelectedIndex == -1)
                            {
                                value = null;
                            }
                            else
                            {
                                value = comboBox.SelectedItem;
                            }
                        }
                    }
                    else if (control is NumericUpDown numericUpDown)
                    {
                        // Xử lý nullable int - nếu value = 0 thì có thể là null
                        if (property.PropertyType == typeof(int?))
                        {
                            if (numericUpDown.Value == 0)
                            {
                                value = null; // Cho phép null nếu không bắt buộc
                            }
                            else
                            {
                                // Convert decimal sang int trước
                                int intValue = (int)numericUpDown.Value;
                                value = (int?)intValue; // Cast sang int? trực tiếp
                            }
                        }
                        else if (property.PropertyType == typeof(long?))
                        {
                            if (numericUpDown.Value == 0)
                            {
                                value = null; // Cho phép null nếu không bắt buộc
                            }
                            else
                            {
                                // Convert decimal sang long trước
                                long longValue = (long)numericUpDown.Value;
                                value = (long?)longValue; // Cast sang long? trực tiếp
                            }
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            // Convert decimal sang int trước
                            int intValue = (int)numericUpDown.Value;
                            value = intValue;
                        }
                        else if (property.PropertyType == typeof(long))
                        {
                            // Convert decimal sang long trước
                            long longValue = (long)numericUpDown.Value;
                            value = longValue;
                        }
                        else
                        {
                            // Fallback cho các kiểu khác
                            int intValue = (int)numericUpDown.Value;
                            value = Convert.ChangeType(intValue, property.PropertyType);
                        }
                    }
                    else if (control is Panel panel)
                    {
                        // Kiểm tra nếu là PictureBox
                        PictureBox pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();
                        if (pictureBox != null && pictureBox.Tag != null)
                        {
                            if (pictureBox.Tag is byte[] bytes)
                            {
                                value = bytes;
                            }
                            else if (pictureBox.Tag is string path && File.Exists(path))
                            {
                                value = File.ReadAllBytes(path);
                            }
                            else
                            {
                                value = new byte[0]; // Mảng byte rỗng thay vì null
                            }
                        }
                        // Kiểm tra nếu là FileDialog (LinkLabel)
                        else
                        {
                            LinkLabel linkLabel = panel.Controls.OfType<LinkLabel>().FirstOrDefault();
                            if (linkLabel != null)
                            {
                                // Lấy đường dẫn file từ Tag
                                if (linkLabel.Tag != null)
                                {
                                    // Kiểm tra xem Tag có phải là anonymous object với FilePath không
                                    var filePathProperty = linkLabel.Tag.GetType().GetProperty("FilePath");
                                    if (filePathProperty != null)
                                    {
                                        string filePath = filePathProperty.GetValue(linkLabel.Tag) as string;
                                        value = filePath; // Lưu đường dẫn file (sẽ được xử lý bởi service để copy file)
                                    }
                                    // Tương thích ngược: nếu Tag là string
                                    else if (linkLabel.Tag is string filePathStr)
                                    {
                                        value = filePathStr;
                                    }
                                    else
                                    {
                                        value = null; // Không có file
                                    }
                                }
                                else
                                {
                                    value = null; // Không có file
                                }
                            }
                            else
                            {
                                value = null; // Không có file cho FileDialog
                            }
                        }
                    }


                    property.SetValue(dataObject, value);
                }
                catch (Exception ex)
                {
                    string displayName = GetDisplayName(property);
                    throw new Exception($"Lỗi tại trường {displayName}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Tính toán chiều cao panel dựa trên danh sách properties
        /// </summary>
        public static int CalculatePanelHeight(List<PropertyInfo> properties)
        {
            int totalHeight = 10; // padding top

            foreach (PropertyInfo property in properties)
            {
                ControlInputType inputType = ControlFactory.DetermineInputType(property);

                // Tính chiều cao dựa trên loại control
                if (inputType == ControlInputType.RichTextBox)
                {
                    totalHeight += 460; // RichTextBox height + label
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

        /// <summary>
        /// Phân loại properties theo nhóm (left, right, bottom)
        /// </summary>
        public static void ClassifyProperties(PropertyInfo[] properties, 
            out List<PropertyInfo> leftGroup, 
            out List<PropertyInfo> rightGroup, 
            out List<PropertyInfo> bottomGroup)
        {
            leftGroup = new List<PropertyInfo>();
            rightGroup = new List<PropertyInfo>();
            bottomGroup = new List<PropertyInfo>();

            // Tách riêng PictureBox để đặt đầu tiên
            List<PropertyInfo> pictureBoxList = new List<PropertyInfo>();
            List<PropertyInfo> otherLeftList = new List<PropertyInfo>();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanWrite) continue;

                ControlInputType inputType = ControlFactory.DetermineInputType(property);

                // Phân loại vào nhóm
                switch (inputType)
                {
                    case ControlInputType.PictureBox:
                        pictureBoxList.Add(property);
                        break;

                    case ControlInputType.CheckBox:
                    case ControlInputType.DateTimePicker:
                    case ControlInputType.FileDialog:
                        otherLeftList.Add(property);
                        break;

                    case ControlInputType.RichTextBox:
                        bottomGroup.Add(property);
                        break;

                    default: // TextBox, NumericUpDown, ComboBox
                        rightGroup.Add(property);
                        break;
                }
            }

            // Sắp xếp lại leftGroup: PictureBox đứng đầu, sau đó là các controls khác
            leftGroup.AddRange(pictureBoxList);
            leftGroup.AddRange(otherLeftList);
        }

        /// <summary>
        /// Tạo các controls cho panel
        /// </summary>
        public static void CreateControlsForPanel(FlowLayoutPanel panel, List<PropertyInfo> properties, 
            int controlWidth, Dictionary<string, Control> controlsDictionary, 
            Dictionary<string, PropertyInfo> propertiesDictionary, bool isReadOnly = false, bool isEditMode = false)
        {
            foreach (PropertyInfo property in properties)
            {
                // Filter properties theo mode:
                // - Form Thêm/Sửa (isReadOnly = false): Ẩn TenCapTren, DanhSachCapDuoi
                // - Form Xem chi tiết (isReadOnly = true): Ẩn CapTrenID
                if (isReadOnly)
                {
                    // Form xem chi tiết: Ẩn CapTrenID
                    if (property.Name == "CapTrenID")
                        continue;
                }
                else
                {
                    // Form Thêm/Sửa: Ẩn TenCapTren và DanhSachCapDuoi
                    if (property.Name == "TenCapTren" || property.Name == "DanhSachCapDuoi")
                        continue;
                }

                // Kiểm tra ReadOnly attribute (chỉ áp dụng khi isEditMode = true)
                var readOnlyAttr = property.GetCustomAttribute<ReadOnlyFieldAttribute>();
                bool fieldIsReadOnly = isReadOnly || (readOnlyAttr != null && readOnlyAttr.IsReadOnly && isEditMode);

                // Lấy display name
                string displayName = GetDisplayName(property);

                // Kiểm tra required (không hiển thị * nếu là readonly mode)
                var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                bool isRequired = !isReadOnly && requiredAttr != null && requiredAttr.IsRequired;

                // Container cho mỗi field
                Panel fieldContainer = new Panel();
                fieldContainer.AutoSize = true;
                fieldContainer.Padding = new Padding(5);
                fieldContainer.Width = controlWidth;

                // Tạo label với khả năng tự động mở rộng và wrap text
                Label label = new Label();
                string labelText = displayName + (isRequired ? " *" : "") + ":";
                label.Text = labelText;
                label.Location = new Point(0, 5);
                label.AutoSize = true; // Tự động tính toán chiều cao
                label.MaximumSize = new Size(controlWidth - 10, 0); // Cho phép wrap text khi quá dài
                label.TextAlign = ContentAlignment.TopLeft;
                label.Font = new Font("Segoe UI", 9, isReadOnly ? FontStyle.Bold : FontStyle.Regular);
                if (isRequired)
                {
                    label.ForeColor = Color.FromArgb(209, 17, 65);
                }
                fieldContainer.Controls.Add(label);
                
                // Force layout để label tính toán chiều cao chính xác
                label.PerformLayout();
                Application.DoEvents();

                // Tính toán vị trí Y cho input control dựa trên chiều cao thực tế của label
                int labelHeight = label.Height > 0 ? label.Height : 23; // Fallback nếu chưa tính được
                int inputControlTop = 5 + labelHeight + 5; // 5px top padding + label height + 5px spacing

                // Tạo control (truyền thông tin read-only cho FileDialog)
                Control inputControl = ControlFactory.CreateControl(property, fieldIsReadOnly);
                inputControl.Location = new Point(0, inputControlTop);
                inputControl.Name = property.Name;

                // Set kích thước
                if (inputControl is RichTextBox)
                {
                    inputControl.Size = new Size(controlWidth - 10, 400);
                    fieldContainer.Height = inputControlTop + 400 + 5; // inputControlTop + RichTextBox height + bottom padding
                }
                else if (inputControl is Panel) // PictureBox container hoặc FileDialog
                {
                    // Kiểm tra xem có phải là PictureBox không
                    PictureBox pictureBox = inputControl.Controls.OfType<PictureBox>().FirstOrDefault();
                    if (pictureBox != null)
                    {
                        inputControl.Size = new Size(controlWidth - 10, 200);
                        fieldContainer.Height = inputControlTop + 200 + 5; // inputControlTop + PictureBox height + bottom padding
                    }
                    else
                    {
                        // FileDialog - chỉ cần chiều cao vừa đủ
                        inputControl.Size = new Size(controlWidth - 10, 30);
                        fieldContainer.Height = inputControlTop + 30 + 5; // inputControlTop + FileDialog height + bottom padding
                    }
                }
                else
                {
                    inputControl.Size = new Size(controlWidth - 10, 25);
                    fieldContainer.Height = inputControlTop + 25 + 5; // inputControlTop + control height + bottom padding
                }

                // Set ReadOnly/Disabled
                if (fieldIsReadOnly)
                {
                    if (inputControl is TextBox textBox)
                    {
                        textBox.ReadOnly = true;
                        if (isReadOnly) textBox.BackColor = Color.FromArgb(245, 245, 245);
                    }
                    else if (inputControl is RichTextBox richTextBox)
                    {
                        richTextBox.ReadOnly = true;
                        if (isReadOnly) richTextBox.BackColor = Color.FromArgb(245, 245, 245);
                    }
                    else if (inputControl is Panel inputPanel)
                    {
                        // Kiểm tra nếu là PictureBox
                        PictureBox pictureBox = inputPanel.Controls.OfType<PictureBox>().FirstOrDefault();
                        if (pictureBox != null && isReadOnly)
                        {
                            // Ẩn các button chọn/xóa ảnh trong view-only mode
                            foreach (Control ctrl in inputPanel.Controls)
                            {
                                if (ctrl is Button)
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                        // Kiểm tra nếu là FileDialog (LinkLabel)
                        // Trong chế độ xem, FileDialog link vẫn có thể click để mở file
                        // Không cần disable vì logic trong ControlFactory đã xử lý việc mở file
                    }
                    else
                    {
                        inputControl.Enabled = false;
                    }
                }

                fieldContainer.Controls.Add(inputControl);

                // Lưu vào dictionary
                controlsDictionary[property.Name] = inputControl;
                propertiesDictionary[property.Name] = property;

                // Bind ComboBox cho CapTrenID với dữ liệu từ DonViService
                if (inputControl is ComboBox comboBox && property.Name == "CapTrenID")
                {
                    try
                    {
                        var donViService = new DonViService();
                        var donViList = donViService.GetDonViData();
                        
                        // Thêm option "Không có" (null)
                        var dataList = new List<object> { new { Key = (int?)null, Value = "(Không có)" } };
                        dataList.AddRange(donViList.Select(dv => new { Key = (int?)dv.DonViID, Value = dv.TenDonVi }));
                        
                        comboBox.DisplayMember = "Value";
                        comboBox.ValueMember = "Key";
                        comboBox.DataSource = dataList;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi khi bind ComboBox cho {property.Name}: {ex.Message}");
                    }
                }

                panel.Controls.Add(fieldContainer);
            }
        }
    }
}
