using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;

namespace QuanLyDangVien.UserControls
{
    /// <summary>
    /// Custom DataGridViewCell để hiển thị nhiều dòng trong một ô
    /// </summary>
    public class MultiLineDataGridViewCell : DataGridViewTextBoxCell
    {
        public override Type ValueType => typeof(string);

        public override Type FormattedValueType => typeof(string);

        // Override để xử lý tốt hơn khi DataGridView tính toán kích thước
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, 
            int rowIndex, Size constraintSize)
        {
            // Tăng chiều cao tối thiểu để hàng cao hơn
            return new Size(100, 100);
        }

        // Override phương thức này để tính toán kích thước thực tế
        public Size GetActualPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, 
            int rowIndex, Size constraintSize)
        {
            // Kiểm tra các tham số đầu vào
            if (graphics == null || cellStyle == null)
            {
                return new Size(100, 100);
            }

            // Kiểm tra rowIndex hợp lệ - DataGridView có thể gọi với rowIndex = -1
            if (rowIndex < 0)
            {
                return new Size(100, 100); // Kích thước mặc định cho header hoặc tính toán layout
            }

            // Kiểm tra DataGridView và row hợp lệ
            if (DataGridView == null || rowIndex >= DataGridView.Rows.Count)
            {
                return new Size(100, 100);
            }

            string text = Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(text))
                return new Size(100, 100);

            try
            {
                Font font = cellStyle.Font ?? DataGridView.DefaultCellStyle.Font ?? new Font("Segoe UI", 9F);
                
                // Tính toán kích thước dựa trên số dòng
                string[] lines = text.Split('\n');
                
                // Tính toán kích thước cho từng dòng để có kết quả chính xác hơn
                float maxLineWidth = 0;
                float totalHeight = 0;
                
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        SizeF lineSize = graphics.MeasureString(line, font);
                        maxLineWidth = Math.Max(maxLineWidth, lineSize.Width);
                        totalHeight += lineSize.Height;
                    }
                    else
                    {
                        // Dòng trống vẫn chiếm chiều cao
                        totalHeight += font.Height;
                    }
                }
                
                // Đảm bảo kích thước tối thiểu và hợp lý - tăng chiều cao tối thiểu
                int width = Math.Max(80, Math.Min(500, (int)Math.Ceiling(maxLineWidth) + 6));
                int height = Math.Max(100, Math.Min(300, (int)Math.Ceiling(totalHeight) + 16)); // Tăng padding từ 6 lên 16
                
                return new Size(width, height);
            }
            catch (Exception ex)
            {
                // Log lỗi để debug (có thể bỏ sau khi fix xong)
                System.Diagnostics.Debug.WriteLine($"GetActualPreferredSize error: {ex.Message}");
                return new Size(100, 100);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, 
            int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, 
            string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, 
            DataGridViewPaintParts paintParts)
        {
            try
            {
                // Kiểm tra cellBounds hợp lệ
                if (cellBounds.Width <= 0 || cellBounds.Height <= 0)
                    return;

                // Vẽ background
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    Color backColor = cellStyle?.BackColor ?? Color.White;
                    if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                    {
                        backColor = cellStyle?.SelectionBackColor ?? Color.LightBlue;
                    }
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        graphics.FillRectangle(brush, cellBounds);
                    }
                }

                // Vẽ border
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                // Vẽ nội dung
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
                {
                    string text = formattedValue?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(text))
                    {
                        Color foreColor = cellStyle?.ForeColor ?? Color.Black;
                        if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                        {
                            foreColor = cellStyle?.SelectionForeColor ?? Color.Black;
                        }

                        using (SolidBrush brush = new SolidBrush(foreColor))
                        {
                            Font font = cellStyle?.Font ?? DataGridView?.DefaultCellStyle?.Font ?? new Font("Segoe UI", 9F);
                            StringFormat format = new StringFormat
                            {
                                Alignment = StringAlignment.Near,
                                LineAlignment = StringAlignment.Near,
                                FormatFlags = StringFormatFlags.NoWrap
                            };

                            // Tính toán kích thước để hiển thị nhiều dòng - tăng padding
                            Rectangle textBounds = new Rectangle(
                                cellBounds.X + 5,
                                cellBounds.Y + 8,
                                Math.Max(0, cellBounds.Width - 10),
                                Math.Max(0, cellBounds.Height - 16)
                            );

                            if (textBounds.Width > 0 && textBounds.Height > 0)
                            {
                                graphics.DrawString(text, font, brush, textBounds, format);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi vẽ để tránh crash
            }
        }

    }

    /// <summary>
    /// Custom DataGridViewColumn cho MultiLineCell
    /// </summary>
    public class MultiLineDataGridViewColumn : DataGridViewColumn
    {
        public MultiLineDataGridViewColumn()
        {
            CellTemplate = new MultiLineDataGridViewCell();
        }
    }
}
