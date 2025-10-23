using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QuanLyDangVien.Helper;
using System.Data;

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
        public ComboBoxItem[] Items { get; }

        public ComboBoxDataAttribute(params object[] items)
        {
            // Cho phép truyền vào theo cặp key-value
            if (items.Length % 2 != 0)
                throw new ArgumentException("Phải truyền theo cặp key - value");

            var list = new List<ComboBoxItem>();
            for (int i = 0; i < items.Length; i += 2)
            {
                list.Add(new ComboBoxItem(items[i], items[i + 1]));
            }

            Items = list.ToArray();
        }
    }


    public class ComboBoxItem
    {
        public object Key { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(object key, object value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString() => Value?.ToString() ?? "";
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
