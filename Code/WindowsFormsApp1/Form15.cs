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
    public partial class Form15 : Form
    {
        Thread t;
        public Form15()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            string sql="select username from taikhoan where idtaikhoan='"+Form2.idTaiKhoan+"'";
            Connection.Connect();
            textBox1.Text = Connection.GetFieldValues(sql).Trim();
            Connection.Disconnect();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ");
            }
            else if(textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng");
            }
            else
            {
                string sql = "update Taikhoan set Password='" + textBox4.Text + "'where idtaikhoan='"+Form2.idTaiKhoan+"'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();

            }    
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(Hopdongdaduyet);
            t.Start();
        }

        private void Hopdongdaduyet(object obj)
        {
            Application.Run(new Form17());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(Hopdongchuaduyet);
            t.Start();
        }
        private void Hopdongchuaduyet(object obj)
        {
            Application.Run(new Form16());
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_DangNhap);
            t.Start();
        }
        private void open_DangNhap(object obj)
        {
            Application.Run(new Form2());
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
