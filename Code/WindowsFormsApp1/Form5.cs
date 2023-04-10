using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        Thread t;
        public Form5()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                checkBox3.Checked = false;
            }    
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string taikhoan = textBox1.Text.Trim().ToString();
            string matkhau= textBox2.Text.Trim().ToString();
            Connection.Connect();
            if (matkhau != textBox3.Text.Trim().ToString() )
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp với mật khẩu");
            }
            if(taikhoan=="" || matkhau==""|| textBox3.Text.Trim().ToString()=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ");
            }    
            if (checkBox1.Checked == true)
            {
                string sql = "select count(*) from TaiKhoan tk where tk.idTaiKhoan LIKE 'QT%'";
                string buffer = Connection.GetFieldValues(sql);
                int temp = int.Parse(buffer) + 1;
                buffer = "QT" + temp.ToString();
                sql = "insert into TaiKhoan values ('" + buffer + "','" + taikhoan + "','" + matkhau + "',0,1)";
               Connection.RunSQL(sql);
            }
            else if (checkBox3.Checked == true)
            {
                string sql = "select count(*) from TaiKhoan tk where tk.idTaiKhoan LIKE 'NV%'";
                string buffer = Connection.GetFieldValues(sql);
                int temp = int.Parse(buffer) + 1;
                buffer = "NV" + temp.ToString();
                sql = "insert into TaiKhoan values ('" + buffer + "','" + taikhoan + "','" + matkhau + "',1,0)";
                Connection.RunSQL(sql);
            }
            Connection.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }   
    }
}
