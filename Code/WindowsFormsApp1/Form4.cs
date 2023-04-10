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
    public partial class Form4 : Form
    {
        Thread t;
        public Form4()
        {
            InitializeComponent();
            LoadData();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(danhsachtaikhoan);
            t.Start();
        }
        private void danhsachtaikhoan(object obj)
        {
            Application.Run(new Form13());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select IDTaiKhoan,Username,Password,isNV,isQuanTri from TaiKhoan";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID Tài Khoản";
            dataGridView1.Columns[1].HeaderText = "Tên Đăng Nhập";
            dataGridView1.Columns[2].HeaderText = "Mật khẩu";
            dataGridView1.Columns[3].HeaderText = "NV";
            dataGridView1.Columns[4].HeaderText = "QT";

            dataGridView1.DefaultCellStyle.Font= new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 300;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            Connection.Disconnect();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["UserName"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
            if(dataGridView1.CurrentRow.Cells["IDTaiKhoan"].Value.ToString().IndexOf("QT")>=0)
            {
                textBox5.Text = "Quản trị";
            }
            else if (dataGridView1.CurrentRow.Cells["IDTaiKhoan"].Value.ToString().IndexOf("NV") >= 0)
            {
                textBox5.Text = "Nhân viên";
            }
            else if (dataGridView1.CurrentRow.Cells["IDTaiKhoan"].Value.ToString().IndexOf("TX") >= 0)
            {
                textBox5.Text = "Tài xế";
            }
            else if (dataGridView1.CurrentRow.Cells["IDTaiKhoan"].Value.ToString().IndexOf("KH") >= 0)
            {
                textBox5.Text = "Khách hàng";
            }
            else if (dataGridView1.CurrentRow.Cells["IDTaiKhoan"].Value.ToString().IndexOf("DT") >= 0)
            {
                textBox5.Text = "Đối tác";
            }

            if (textBox5.Text == "Đối tác" || textBox5.Text == "Tài xế" || textBox5.Text == "Khách hàng")
            {
                button2.Enabled = false;
            }
            else 
            {
                button2.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox5.Text == "Quản trị" || textBox5.Text == "Nhân viên")
            {
                string sql = "delete TaiKhoan where idTaiKhoan='"+ dataGridView1.CurrentRow.Cells["idTaiKhoan"].Value.ToString() +"'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            Form5 form = new Form5();
            form.ShowDialog();
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "Quản trị" || textBox5.Text == "Nhân viên")
            {
               Form15 form = new Form15();
               form.ShowDialog();//Nhân viên
            }
            else if (textBox5.Text == "Khách hàng")
            {
                Form23 form = new Form23();
                form.ShowDialog();//Khách hàng
            }
            else if (textBox5.Text == "Đối tác")
            {
                Form6 form = new Form6();
                form.ShowDialog();//Đối tác
            }
            else if (textBox5.Text == "Tài xế")
            {
                Form12 form = new Form12();
                form.ShowDialog();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(taikhoan);
            t.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void taikhoan(object obj)
        {
            Application.Run(new Form25());
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
