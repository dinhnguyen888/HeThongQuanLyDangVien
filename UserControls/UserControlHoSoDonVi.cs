using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangVien
{
    public partial class UserControlHoSoDonVi : UserControl
    {
        public UserControlHoSoDonVi()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(TimTb.Text))
            {
                TimTb.Text = " ";
                TimTb.Text = "";
            }
        }

        private void TimKiemBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
