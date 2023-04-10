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
   
    public partial class Form18 : Form
    {
        public static string idkhachhang;
        Thread t;
        public Form18()
        {
            InitializeComponent();
            LoadData();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            DataTable table = new DataTable();
            DataRow x;
           
            string sql = "Select tenKH,sdtkh,diachikh,emailkh from khachhang where idtaikhoan='" + Form2.idTaiKhoan + "'";
            Connection.Connect();
            table = Connection.GetDataToTable(sql);
            sql = "Select Username from TaiKhoan where idtaikhoan='" + Form2.idTaiKhoan + "'";
            textBox1.Text = Connection.GetFieldValues(sql).Trim();
            sql= "Select idkh from khachhang where idtaikhoan='" + Form2.idTaiKhoan + "'";
            idkhachhang=Connection.GetFieldValues(sql).Trim();
            Connection.Disconnect();
            if (table.Rows.Count != 0)
            {
                x = table.Rows[0];
                textBox6.Text=x["tenKH"].ToString();
                textBox5.Text = x["diachiKH"].ToString();
                textBox3.Text = x["SDTKH"].ToString();
                textBox7.Text = x["EmailKH"].ToString();
            }
            

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
            }
            else if(textBox2.Text!= textBox4.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng");
            }
            else
            {
                string sql = "update Taikhoan set Password='" + textBox4.Text + "'where idtaikhoan='" + Form2.idTaiKhoan + "'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
            }    
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox6.Text==""|| textBox5.Text==""|| textBox3.Text=="" ||textBox7.Text=="" )
            {
                MessageBox.Show("Vui lòng không để trống để cập nhật");
            }
            else
            {
                string sql = "update Khachhang set tenKH=N'"+ textBox6.Text + "',SDTKH='"+ textBox3.Text + "',DiachiKH=N'"+ textBox5.Text + "',EmailKH='"+ textBox7.Text + "'where idtaikhoan='" + Form2.idTaiKhoan + "' ";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            }    
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(dathang);
            t.Start();
        }
        private void dathang()
        {
            Application.Run(new Form20());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(donhangcuatoi);
            t.Start();
        }
        private void donhangcuatoi()
        {
            Application.Run(new Form19());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_Click(object sender, EventArgs e)
        {

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
