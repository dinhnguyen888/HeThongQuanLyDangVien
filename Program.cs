using QuanLyDangVien.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Kết nối thất bại!\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            Application.EnableVisualStyles();
           
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HeThongQuanLyDangVien());
        }


      
    }
}
