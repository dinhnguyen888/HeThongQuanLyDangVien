using QuanLyDangVien.Attributes;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Services;
using QuanLyDangVien.Models;
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
    // ===== FORM XEM CHI TIẾT DỮ LIỆU =====

    public partial class FormXemChiTiet : Form
    {
        private object _dataObject;
        private Type _dataType;
        private Dictionary<string, Control> _controlsDictionary;
        private Dictionary<string, PropertyInfo> _propertiesDictionary;

        // Constructor cho chế độ xem chi tiết
        public FormXemChiTiet(object existingData)
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
            this.Text = $"Xem chi tiết {_dataType.Name}";

            // Tạo dynamic controls
            TaoDynamicControls();

            // Load dữ liệu vào controls
            LoadDataToControls();
            
            // Chuyển DonViCap1 và DonViCap2 sang TextBox nếu là DangVien
            if (_dataType.Name == "DangVien")
            {
                ConvertDonViCapToTextBox();
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

            FormHelper.CreateControlsForPanel(leftPanel, leftGroup, 450, _controlsDictionary, _propertiesDictionary, true);

            // === RIGHT PANEL (TextBox, NumericUpDown, ComboBox) ===
            FlowLayoutPanel rightPanel = new FlowLayoutPanel();
            rightPanel.FlowDirection = FlowDirection.TopDown;
            rightPanel.AutoSize = true;
            rightPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightPanel.Location = new Point(480, 10);
            rightPanel.Padding = new Padding(5);
            rightPanel.WrapContents = false;
            rightPanel.Width = 460;

            FormHelper.CreateControlsForPanel(rightPanel, rightGroup, 450, _controlsDictionary, _propertiesDictionary, true);

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

            FormHelper.CreateControlsForPanel(bottomPanel, bottomGroup, 920, _controlsDictionary, _propertiesDictionary, true);

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

            Button btnClose = new Button();
            btnClose.Text = "Đóng";
            btnClose.Size = new Size(120, 35);
            btnClose.Location = new Point(10, 12);
            btnClose.BackColor = Color.FromArgb(120, 120, 120);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => { this.Close(); };
            buttonPanel.Controls.Add(btnClose);

            this.Controls.Add(buttonPanel);
            this.Controls.Add(scrollPanel);

            // Thiết lập form
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(800, 600);
        }
     
        /// <summary>
        /// Đảm bảo DonViCap1 và DonViCap2 hiển thị đúng giá trị (từ property hoặc tính từ DonViID)
        /// </summary>
        private void ConvertDonViCapToTextBox()
        {
            try
            {
                // Nếu là DangVienDTO, tính DonViCap1 và DonViCap2 từ DonViID nếu chưa có
                if (_dataObject.GetType().Name == "DangVienDTO")
                {
                    var donViIDProperty = _propertiesDictionary.ContainsKey("DonViID") 
                        ? _propertiesDictionary["DonViID"] : null;
                    var donViCap1Property = _propertiesDictionary.ContainsKey("DonViCap1") 
                        ? _propertiesDictionary["DonViCap1"] : null;
                    var donViCap2Property = _propertiesDictionary.ContainsKey("DonViCap2") 
                        ? _propertiesDictionary["DonViCap2"] : null;
                    
                    if (donViIDProperty != null)
                    {
                        var currentDonViID = Convert.ToInt32(donViIDProperty.GetValue(_dataObject) ?? 0);
                        if (currentDonViID > 0)
                        {
                            var donViService = new DonViService();
                            var allDonVi = donViService.GetAll();
                            var currentDonVi = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonViID);
                            
                            if (currentDonVi != null && currentDonVi.CapTrenID.HasValue)
                            {
                                var donViCap2 = allDonVi.FirstOrDefault(dv => dv.DonViID == currentDonVi.CapTrenID.Value);
                                if (donViCap2 != null && donViCap2.CapTrenID.HasValue)
                                {
                                    var donViCap1 = allDonVi.FirstOrDefault(dv => dv.DonViID == donViCap2.CapTrenID.Value);
                                    
                                    // Set DonViCap1 nếu chưa có
                                    if (donViCap1Property != null && donViCap1 != null)
                                    {
                                        var currentDonViCap1 = donViCap1Property.GetValue(_dataObject) as string;
                                        if (string.IsNullOrEmpty(currentDonViCap1))
                                        {
                                            donViCap1Property.SetValue(_dataObject, donViCap1.TenDonVi);
                                        }
                                    }
                                    
                                    // Set DonViCap2 nếu chưa có
                                    if (donViCap2Property != null)
                                    {
                                        var currentDonViCap2 = donViCap2Property.GetValue(_dataObject) as string;
                                        if (string.IsNullOrEmpty(currentDonViCap2))
                                        {
                                            donViCap2Property.SetValue(_dataObject, donViCap2.TenDonVi);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                // Đảm bảo TextBox hiển thị đúng giá trị
                if (_controlsDictionary.ContainsKey("DonViCap1"))
                {
                    var control = _controlsDictionary["DonViCap1"];
                    if (control is TextBox textBox)
                    {
                        var property = _propertiesDictionary.ContainsKey("DonViCap1") 
                            ? _propertiesDictionary["DonViCap1"] : null;
                        if (property != null)
                        {
                            var value = property.GetValue(_dataObject);
                            textBox.Text = value != null ? value.ToString() : "";
                        }
                    }
                }
                
                if (_controlsDictionary.ContainsKey("DonViCap2"))
                {
                    var control = _controlsDictionary["DonViCap2"];
                    if (control is TextBox textBox)
                    {
                        var property = _propertiesDictionary.ContainsKey("DonViCap2") 
                            ? _propertiesDictionary["DonViCap2"] : null;
                        if (property != null)
                        {
                            var value = property.GetValue(_dataObject);
                            textBox.Text = value != null ? value.ToString() : "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xử lý DonViCap: {ex.Message}");
            }
        }

        private void LoadDataToControls()
        {
            FormHelper.LoadDataToControls(_dataObject, _controlsDictionary, _propertiesDictionary);
            
            // Nếu là DonVi, load danh sách đơn vị cấp dưới
            if (_dataObject is DonVi donVi && donVi.DonViID > 0)
            {
                try
                {
                    var donViService = new DonViService();
                    var allDonVi = donViService.GetAll();
                    
                    // Lấy danh sách đơn vị cấp dưới (có CapTrenID = DonViID hiện tại)
                    var capDuoiList = allDonVi.Where(dv => dv.CapTrenID == donVi.DonViID).ToList();
                    
                    if (capDuoiList.Any())
                    {
                        string danhSachCapDuoi = string.Join(", ", capDuoiList.Select(dv => dv.TenDonVi));
                        donVi.DanhSachCapDuoi = danhSachCapDuoi;
                        
                        // Cập nhật lại control nếu có
                        if (_controlsDictionary.ContainsKey("DanhSachCapDuoi"))
                        {
                            var control = _controlsDictionary["DanhSachCapDuoi"];
                            if (control is TextBox textBox)
                            {
                                textBox.Text = danhSachCapDuoi;
                            }
                        }
                    }
                    else
                    {
                        donVi.DanhSachCapDuoi = "(Không có)";
                        if (_controlsDictionary.ContainsKey("DanhSachCapDuoi"))
                        {
                            var control = _controlsDictionary["DanhSachCapDuoi"];
                            if (control is TextBox textBox)
                            {
                                textBox.Text = "(Không có)";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi load danh sách đơn vị cấp dưới: {ex.Message}");
                }
            }
        }

        public object GetData()
        {
            return _dataObject;
        }
    }
}
