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
    public partial class Form19 : Form
    {
        Thread t;
        public Form19()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["diachigiaohang"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["tongphi"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["hinhthucthanhtoan"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["ngaytao"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["trangthaivanchuyen"].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells["trangthaidonhang"].Value.ToString();
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select idkh from khachhang where idTaiKhoan='" + Form2.idTaiKhoan + "'";
            string buffer = Connection.GetFieldValues(sql);
            sql = "Select iddonhang,diachigiaohang,hinhthucthanhtoan,ngaytao,trangthaivanchuyen,trangthaidonhang,(phisanpham+phivanchuyen)as tongphi from donhang where idkh='" +buffer+ "'";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
            dataGridView1.Columns[1].HeaderText = "Địa chỉ giao hàng";
            dataGridView1.Columns[2].HeaderText = "Hình thức thanh toán";
            dataGridView1.Columns[3].HeaderText = "Ngày mua";
            dataGridView1.Columns[4].HeaderText = "Tình trạng vận chuyển";
            dataGridView1.Columns[5].HeaderText = "Tình trạng đơn hàng";
            dataGridView1.Columns[6].HeaderText = "Tổng phí";


            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

            Connection.Disconnect();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

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

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_TaiKhoan);
            t.Start();
        }
        private void open_TaiKhoan()
        {
            Application.Run(new Form18());
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["trangthaidonhang"].Value.ToString()== "Đã tiếp nhận")
            {
                string sql = "delete qldonhang where iddonhang='" + dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString() + "'";
                Connection.Connect();
                Connection.RunSQL(sql);
                sql = "delete donhang where iddonhang='" + dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString() + "'";
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            }
        }
    }
}
