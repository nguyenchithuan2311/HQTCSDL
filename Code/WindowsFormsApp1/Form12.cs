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
    public partial class Form12 : Form
    {
        public static string idtaixe;
        Thread t;
        public Form12()
        {
            InitializeComponent();
            initData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void initData()
        {
            string sql = "select username from taikhoan where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            Connection.Connect();
            textBox1.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select Tentx from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox6.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select cmnd from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox5.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select DiachiTX from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox4.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select SDTTX from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox8.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select Bienso from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox12.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select stktx from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox10.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select EmailTX from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox7.Text = Connection.GetFieldValues(sql).Trim();
            sql = "select idtaixe from taixe where IDtaikhoan='" + Form2.idTaiKhoan + "'";
            idtaixe= Connection.GetFieldValues(sql).Trim();
            Connection.Disconnect();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            if(textBox6.Text==""||textBox5.Text==""||textBox4.Text ==""||textBox8.Text ==""||textBox12.Text ==""||textBox10.Text==""||textBox7.Text =="")
            {
                MessageBox.Show("Vui lòng không để trống thông tin");
            }
            else 
            { 
                string sql="update taixe set tenTX=N'"+textBox6.Text.Trim()+ "',Bienso='"+textBox12.Text.Trim()+"',STKTX='" + textBox10.Text.Trim() + "',SDTTX='"+ textBox8.Text.Trim() + "',EmailTX='"+ textBox7.Text.Trim() + "' where idtaixe='" + idtaixe+"'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
                initData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("Vui lòng nhập để thay đổi mật khẩu");
            }
            else if(textBox2.Text!=textBox3.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp");
            }
            else
            {
                string sql = "update taikhoan setpassword='"+textBox2.Text.Trim()+"'where idtaikhoan='"+Form2.idTaiKhoan+"'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
            }    
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_DSDonHang);
            t.Start();
        }

        private void open_DSDonHang(object obj)
        {
            Application.Run(new Form13());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_Thongke);
            t.Start();
        }
        private void open_Thongke(object obj)
        {
            Application.Run(new Form14());
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

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    
}
