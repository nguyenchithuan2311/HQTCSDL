using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form25 : Form
    {
        Thread t;
        public Form25()
        {
            InitializeComponent();
            init();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void init()
        {
            Connection.Connect();
            string sql = "select username from taikhoan where idtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox1.Text = Connection.GetFieldValues(sql);
            Connection.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
            }
            else if (textBox3.Text != textBox2.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp");
            }
            else
            {
                string sql = "update taikhoan set Password='" + textBox3.Text + "' where idtaikhoan='" + Form2.idTaiKhoan + "'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(danhsachtaikhoan);
            t.Start();
        }
        private void danhsachtaikhoan(object obj)
        {
            Application.Run(new Form4());
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_DangNhap);
            t.Start();
        }
        private void open_DangNhap(object obj)
        {
            Application.Run(new Form2());
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
