using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDangVien.Attributes
{
    /// <summary>
    /// Enum định nghĩa các loại control
    /// </summary>
    public enum ControlInputType
    {
        TextBox,
        RichTextBox,
        ComboBox,
        DateTimePicker,
        CheckBox,
        PictureBox,
        NumericUpDown
    }

    // Attribute để chỉ định kiểu control
    [AttributeUsage(AttributeTargets.Property)]
    public class ControlTypeAttribute : Attribute
    {
        public ControlInputType InputType { get; set; }
        public ControlTypeAttribute(ControlInputType type)
        {
            InputType = type;
        }
    }

    // Attribute để cung cấp dữ liệu cho ComboBox
    [AttributeUsage(AttributeTargets.Property)]
    public class ComboBoxDataAttribute : Attribute
    {
        public object[] Items { get; set; }
        public ComboBoxDataAttribute(params object[] items)
        {
            Items = items;
        }
    }

    // Attribute để chỉ định tên hiển thị
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; set; }
        public DisplayNameAttribute(string name)
        {
            Name = name;
        }
    }

    // Attribute để đánh dấu trường bắt buộc
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public bool IsRequired { get; set; }
        public RequiredAttribute(bool isRequired = true)
        {
            IsRequired = isRequired;
        }
    }

    // Attribute để đánh dấu trường chỉ đọc (không cho edit)
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyFieldAttribute : Attribute
    {
        public bool IsReadOnly { get; set; }
        public ReadOnlyFieldAttribute(bool isReadOnly = true)
        {
            IsReadOnly = isReadOnly;
        }
    }

}
