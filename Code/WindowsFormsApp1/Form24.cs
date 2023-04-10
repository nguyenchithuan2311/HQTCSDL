using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form24 : Form
    {
        public static DateTime NgayGiaHan;
        public Form24()
        {
            InitializeComponent();
            setup();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void setup()
        {
            dateTimePicker3.Value = Form8.ngayhethanhopdong;
            dateTimePicker3.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value< dateTimePicker3.Value)
            {
                MessageBox.Show("Ngày gia hạn phải sau ngày hết hợp đồng");
                
            }
            else { NgayGiaHan = dateTimePicker1.Value;
                this.Close();
            }
        }
    }
}
