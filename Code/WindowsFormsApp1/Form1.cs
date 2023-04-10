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
    public partial class Form1 : Form
    {
        string tendangnhap;
        string matkhau;
          public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }    
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tendangnhap = textBox1.Text.Trim().ToString();
            matkhau = textBox2.Text.Trim().ToString();
            string sql = "select tk.Password from TaiKhoan tk where tk.Password= '" + matkhau + "' ";
            Connection.Connect();
            if (tendangnhap=="")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
            }
            else if (matkhau == "")
            {
                MessageBox.Show("Vui lòng nhập tên mật khẩu");
            }
            else if(matkhau!= textBox3.Text.Trim().ToString())
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp với mật khẩu");
            }    
            else if (Connection.GetFieldValues(sql).Count()!=0)
            {
                MessageBox.Show("Mật khẩu đã trùng");
            }  
            else
            {
                if(checkBox1.Checked == true)
                {
                    sql = "select count(*) from TaiKhoan tk where tk.idTaiKhoan LIKE 'DT%'";
                    string buffer = Connection.GetFieldValues(sql);
                    int temp = int.Parse(buffer) + 1;
                    buffer = "DT"+temp.ToString();
                    sql = "insert into TaiKhoan values ('"+ buffer + "','"+tendangnhap+"','"+matkhau+"',0,0)";
                    Connection.RunSQL(sql);
                }
                else if (checkBox2.Checked == true)
                {
                    sql = "select count(*) from TaiKhoan tk where tk.idTaiKhoan LIKE 'KH%'";
                    string buffer = Connection.GetFieldValues(sql);
                    int temp = int.Parse(buffer) + 1;
                    buffer = "KH" + temp.ToString();
                    sql = "insert into TaiKhoan values ('" + buffer + "','" + tendangnhap + "','" + matkhau + "',0,0)";
                    
                    Connection.RunSQL(sql);
                }
                else if (checkBox3.Checked == true)
                {
                    sql = "select count(*) from TaiKhoan tk where tk.idTaiKhoan LIKE 'TX%'";
                    string buffer = Connection.GetFieldValues(sql);
                    int temp = int.Parse(buffer) + 1;
                    buffer = "TX" + temp.ToString();
                    sql = "insert into TaiKhoan values ('" + buffer + "','" + tendangnhap + "','" + matkhau + "',0,1)";
                    
                    Connection.RunSQL(sql);
                }
                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Connection.Disconnect();
            }    
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }
    }
}
