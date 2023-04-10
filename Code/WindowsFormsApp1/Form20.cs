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
    
    public partial class Form20 : Form
    {
        Thread t;
        public static string iddoitac;
        public Form20()
        {
            InitializeComponent();
            LoadData();
        }

        private void Form20_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells["Tenquan"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["dckinhdoanh"].Value.ToString();
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select iddoitac,Tenquan,dckinhdoanh from doitac";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID đối tác";
            dataGridView1.Columns[1].HeaderText = "Tên quán";
            dataGridView1.Columns[2].HeaderText = "Địa chỉ";
           


            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 300;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

            Connection.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Vui lòng chọn đối tác");
            }
            else
            {
               iddoitac= dataGridView1.CurrentRow.Cells["iddoitac"].Value.ToString();
                this.Close();
                t = new Thread(dathang);
                t.Start();
            }
        }
        private void dathang()
        {
            Application.Run(new Form21());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(Donhangcuatoi);
            t.Start();
        }
        private void Donhangcuatoi()
        {
            Application.Run(new Form19());
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
