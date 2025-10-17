using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms; // nhớ thêm dòng này để dùng MessageBox

namespace QuanLyDangVien.Helper
{
    public static class DbHelper
    {
        private static readonly string connectionString;

        static DbHelper()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new Exception("Không tìm thấy hoặc chuỗi kết nối rỗng trong App.config (key: DbConnection).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo DbHelper:\n" + ex.Message,
                    "Database Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                throw; // ném lại để dừng chương trình, tránh lỗi ẩn
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
