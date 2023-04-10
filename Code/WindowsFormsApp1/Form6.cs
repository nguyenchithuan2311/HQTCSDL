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
    public partial class Form6 : Form
    {
        Thread t;
       
        static public string idDoiTac;
        public Form6()
        {
            InitializeComponent();
            initIdDoiTac();
            LoadInformation();
        }
        private void initIdDoiTac()
        {
            Connection.Connect();
            idDoiTac = Connection.GetFieldValues("Select dt.IDDoiTac  from HopDong hd, Doitac dt where hd.IDDoiTac=dt.IDDoiTac and hd.IDTaiKhoan='" + Form2.idTaiKhoan + "'");
            Connection.Disconnect();
        }
        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label3.Visible == true)
            {
                this.Close();
                t = new Thread(open_ADDHopDong);
                t.Start();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void open_ADDHopDong(object obj)
        {
            Application.Run(new Form7());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Visible == true)
            {
                this.Close();
                t = new Thread(open_HopDongDaLap);
                t.Start();
            }
        }
        private void open_HopDongDaLap(object obj)
        {
            Application.Run(new Form8());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (label2.Visible == true)
            {
                this.Close();
                t = new Thread(open_ChiNhanh);
                t.Start();
            }
        }
        private void open_ChiNhanh(object obj)
        {
            Application.Run(new Form9());
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (label5.Visible == true)
            {
                this.Close();
                t = new Thread(open_Mon);
                t.Start();
            }
        }
        private void open_Mon(object obj)
        {
            Application.Run(new Form10());
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (label6.Visible == true)
            {
                this.Close();
                t = new Thread(open_DonHang);
                t.Start();
            }
        }
        private void open_DonHang(object obj)
        {
            Application.Run(new Form11());
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (label8.Visible == true)
            {
                this.Close();
                t = new Thread(open_DangNhap);
                t.Start();
            }
        }
        private void open_DangNhap(object obj)
        {
            Application.Run(new Form2());
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (label9.Visible == true)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadInformation()
        {
            Connection.Connect();
            string sql = "Select Username  from TaiKhoan where idTaiKhoan='"+ Form2.idTaiKhoan + "'";
            textBox1.Text=Connection.GetFieldValues(sql).Trim();
            sql = "Select TenQuan  from DoiTac where IDDoiTac='" + idDoiTac + "'";
            textBox6.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select NguoiDaiDien  from DoiTac where IDDoiTac='" + idDoiTac + "'";
            textBox4.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select dt.DCKinhDoanh  from DoiTac dt where IDDoiTac='" + idDoiTac + "'";
            textBox9.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select dt.SDTDT  from DoiTac dt where IDDoiTac='" + idDoiTac + "'";
            textBox8.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select dt.MaSoThue  from DoiTac dt where IDDoiTac='" + idDoiTac + "'";
            textBox7.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select dt.SoTaiKhoanDT  from DoiTac dt where IDDoiTac='" + idDoiTac + "'";
            textBox10.Text = Connection.GetFieldValues(sql).Trim();
            sql = "Select dt.NganHang  from DoiTac dt where IDDoiTac='" + idDoiTac + "'";
            textBox11.Text = Connection.GetFieldValues(sql).Trim();
            Connection.Disconnect();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text==""|| textBox5.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                return;
            }
            else if(textBox3.Text!= textBox5.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp");
                return;
            }
            string sql = "update TaiKhoan set Password='" + textBox3.Text + "' where IDTaiKhoan='" + Form2.idTaiKhoan + "'";
            Connection.Connect();
            Connection.RunSQL(sql);
            Connection.Disconnect();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string sql = "update DoiTac set TenQuan='"+ textBox6.Text.ToString()+ "',NguoiDaiDien='"+ textBox4.Text.ToString() + "' ,DcKinhDoanh='"+ textBox9.Text.ToString() + "' ,SDTDT='"+ textBox8.Text.ToString() + "' ,MaSoThue='"+ textBox7.Text.ToString() + "' ,SoTaiKhoanDT='"+ textBox10.Text+ " ',NganHang='"+ textBox11.Text+ "'where IDDoiTac='" + idDoiTac + "'";
            MessageBox.Show(sql);
            Connection.Connect();
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadInformation();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
