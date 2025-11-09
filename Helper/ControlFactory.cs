using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QuanLyDangVien.Attributes;
namespace QuanLyDangVien.Helper
{
    /// <summary>
    /// Factory class để tạo các control động cho form
    /// </summary>
    public static class ControlFactory
    {
        /// <summary>
        /// Tạo control dựa trên PropertyInfo và attribute
        /// </summary>
        public static Control CreateControl(PropertyInfo property, bool isReadOnly = false)
        {
            var controlTypeAttr = property.GetCustomAttribute<ControlTypeAttribute>();

            if (controlTypeAttr != null)
            {
                switch (controlTypeAttr.InputType)
                {
                    case ControlInputType.RichTextBox:
                        return CreateRichTextBox();

                    case ControlInputType.ComboBox:
                        return CreateComboBox(property);

                    case ControlInputType.PictureBox:
                        return CreatePictureBoxControl(property);

                    case ControlInputType.NumericUpDown:
                        return CreateNumericUpDown();

                    case ControlInputType.CheckBox:
                        return CreateCheckBox();

                    case ControlInputType.DateTimePicker:
                        return CreateDateTimePicker();

                    case ControlInputType.FileDialog:
                        return CreateFileDialogControl(property, isReadOnly);

                    default:
                        return CreateTextBox();
                }
            }

            return CreateControlByDataType(property);
        }

        /// <summary>
        /// Tạo TextBox
        /// </summary>
        public static TextBox CreateTextBox()
        {
            TextBox txt = new TextBox();
            txt.Font = new Font("Segoe UI", 10);
            return txt;
        }

        /// <summary>
        /// Tạo CheckBox
        /// </summary>
        public static CheckBox CreateCheckBox()
        {
            CheckBox cb = new CheckBox();
            cb.Font = new Font("Segoe UI", 10);
            cb.AutoSize = true;
            return cb;
        }

        /// <summary>
        /// Tạo RichTextBox
        /// </summary>
        public static RichTextBox CreateRichTextBox()
        {
            RichTextBox rtb = new RichTextBox();
            rtb.BorderStyle = BorderStyle.FixedSingle;
            rtb.Font = new Font("Segoe UI", 10);
            return rtb;
        }

        /// <summary>
        /// Tạo ComboBox với dữ liệu
        /// </summary>
        public static ComboBox CreateComboBox(PropertyInfo property)
        {
            ComboBox cb = new ComboBox();
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Standard;
            cb.Font = new Font("Segoe UI", 10);

            var comboDataAttr = property.GetCustomAttribute<ComboBoxDataAttribute>();
            if (comboDataAttr != null && comboDataAttr.Items != null)
            {
                // Xử lý ComboBoxItem để bind đúng cách
                var dataList = comboDataAttr.Items.Select(item => new { Key = item.Key, Value = item.Value }).ToList();
                cb.DisplayMember = "Value";
                cb.ValueMember = "Key";
                cb.DataSource = dataList;
            }
            else if (property.PropertyType.IsEnum ||
                     (Nullable.GetUnderlyingType(property.PropertyType)?.IsEnum ?? false))
            {
                Type enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                cb.DataSource = Enum.GetValues(enumType);
            }

            return cb;
        }

        /// <summary>
        /// Tạo PictureBox với các nút điều khiển
        /// </summary>
        public static Panel CreatePictureBoxControl(PropertyInfo property)
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
            btnChonAnh.Click += (s, e) => SelectImage(pictureBox);
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

        /// <summary>
        /// Xử lý chọn ảnh
        /// </summary>
        private static void SelectImage(PictureBox pictureBox)
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

        /// <summary>
        /// Tạo NumericUpDown
        /// </summary>
        public static NumericUpDown CreateNumericUpDown()
        {
            NumericUpDown nud = new NumericUpDown();
            nud.Maximum = 1000000;
            nud.Minimum = 0;
            nud.Font = new Font("Segoe UI", 10);
            nud.DecimalPlaces = 0; // Chỉ cho phép số nguyên
            nud.AllowDrop = false;
            return nud;
        }

        /// <summary>
        /// Tạo DateTimePicker
        /// </summary>
        public static DateTimePicker CreateDateTimePicker()
        {
            DateTimePicker dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
            dtp.Font = new Font("Segoe UI", 10);
            return dtp;
        }

        /// <summary>
        /// Tạo FileDialog control (LinkLabel với OpenFileDialog)
        /// </summary>
        public static Panel CreateFileDialogControl(PropertyInfo property, bool isReadOnly = false)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.None;
            panel.AutoSize = true;
            // Lưu thông tin read-only vào Tag của panel
            panel.Tag = isReadOnly;

            LinkLabel linkLabel = new LinkLabel();
            linkLabel.Text = "Chưa chọn file";
            linkLabel.Location = new Point(0, 0);
            linkLabel.AutoSize = true;
            linkLabel.Font = new Font("Segoe UI", 10);
            linkLabel.ForeColor = Color.Gray;
            linkLabel.LinkColor = Color.FromArgb(0, 174, 219);
            linkLabel.VisitedLinkColor = Color.FromArgb(0, 174, 219);
            linkLabel.Name = property.Name;
            
            // Lưu đường dẫn file vào một dictionary riêng để tránh conflict với Tag của panel
            // Sử dụng một đối tượng để lưu cả đường dẫn file và thông tin read-only
            var fileInfo = new { FilePath = (string)null, IsReadOnly = isReadOnly };
            linkLabel.Tag = fileInfo;
            
            // Event click để mở OpenFileDialog hoặc mở file
            linkLabel.Click += (s, e) =>
            {
                // Lấy thông tin từ Tag
                var tagInfo = linkLabel.Tag;
                string existingPath = null;
                bool allowFileSelection = true;
                
                if (tagInfo != null)
                {
                    // Kiểm tra xem Tag có phải là object với FilePath không
                    var filePathProperty = tagInfo.GetType().GetProperty("FilePath");
                    var isReadOnlyProperty = tagInfo.GetType().GetProperty("IsReadOnly");
                    
                    if (filePathProperty != null)
                    {
                        existingPath = filePathProperty.GetValue(tagInfo) as string;
                    }
                    if (isReadOnlyProperty != null)
                    {
                        allowFileSelection = !(bool)isReadOnlyProperty.GetValue(tagInfo);
                    }
                    // Nếu Tag là string (tương thích ngược)
                    else if (tagInfo is string)
                    {
                        existingPath = tagInfo as string;
                        allowFileSelection = true;
                    }
                }
                
                // Nếu đã có file, mở file
                if (!string.IsNullOrWhiteSpace(existingPath))
                {
                    string fullPath = existingPath;
                    
                    // Nếu là đường dẫn tương đối (Server\...), lấy full path
                    if (!Path.IsPathRooted(existingPath))
                    {
                        try
                        {
                            fullPath = FileHelper.GetFullPath(existingPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi lấy đường dẫn file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    
                    // Kiểm tra file có tồn tại không
                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(fullPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }
                    else
                    {
                        // File không tồn tại
                        if (!allowFileSelection)
                        {
                            // Trong chế độ read-only, chỉ thông báo
                            MessageBox.Show("File không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                // Nếu cho phép chọn file mới (không phải read-only mode), mở OpenFileDialog
                if (allowFileSelection)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Tất cả files (*.*)|*.*|PDF (*.pdf)|*.pdf|Word (*.doc;*.docx)|*.doc;*.docx|Ảnh (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.Title = "Chọn file";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Lưu đường dẫn file vào Tag và hiển thị tên file
                            linkLabel.Tag = new { FilePath = openFileDialog.FileName, IsReadOnly = isReadOnly };
                            linkLabel.Text = Path.GetFileName(openFileDialog.FileName);
                            linkLabel.ForeColor = Color.FromArgb(0, 174, 219);
                            linkLabel.LinkColor = Color.FromArgb(0, 174, 219);
                        }
                    }
                }
                else
                {
                    // Trong chế độ read-only và chưa có file, thông báo
                    MessageBox.Show("Chưa có file được đính kèm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            panel.Controls.Add(linkLabel);
            return panel;
        }

        /// <summary>
        /// Tạo control tự động dựa trên kiểu dữ liệu
        /// </summary>
        private static Control CreateControlByDataType(PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (underlyingType == typeof(bool))
            {
                return CreateCheckBox();
            }
            else if (underlyingType == typeof(DateTime))
            {
                return CreateDateTimePicker();
            }
            else if (underlyingType.IsEnum)
            {
                return CreateComboBox(property);
            }
            else if (underlyingType == typeof(int) || underlyingType == typeof(long) ||
                     underlyingType == typeof(decimal) || underlyingType == typeof(double))
            {
                return CreateNumericUpDown();
            }
            else
            {
                return CreateTextBox();
            }
        }

        /// <summary>
        /// Xác định kiểu control từ PropertyInfo
        /// </summary>
        public static ControlInputType DetermineInputType(PropertyInfo property)
        {
            var controlTypeAttr = property.GetCustomAttribute<ControlTypeAttribute>();
            Type propertyType = property.PropertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (controlTypeAttr != null)
            {
                return controlTypeAttr.InputType;
            }
            else
            {
                // Auto-detect type
                if (underlyingType == typeof(bool))
                    return ControlInputType.CheckBox;
                else if (underlyingType == typeof(DateTime))
                    return ControlInputType.DateTimePicker;
                else if (underlyingType.IsEnum)
                    return ControlInputType.ComboBox;
                else if (underlyingType == typeof(int) || underlyingType == typeof(long) ||
                         underlyingType == typeof(decimal) || underlyingType == typeof(double))
                    return ControlInputType.NumericUpDown;
                else
                    return ControlInputType.TextBox;
            }
        }
    }
}
