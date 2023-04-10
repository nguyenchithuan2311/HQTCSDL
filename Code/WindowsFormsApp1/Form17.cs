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
    public partial class Form17 : Form
    {
        Thread t;
        public Form17()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells["masohopdong"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["iddoitac"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["tenquan"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["nguoidaidien"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["phihoahong"].Value.ToString();
            textBox5.Text = (Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ngayketthuc"].Value.ToString()) - Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ngaybatdau"].Value.ToString())).ToString("dd");
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["ngaybatdau"].Value.ToString();
            dateTimePicker3.Text= dataGridView1.CurrentRow.Cells["ngayketthuc"].Value.ToString();
            textBox8.Text= (Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ngayketthuc"].Value.ToString()) - DateTime.Now).ToString("dd");
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select Masohopdong, nguoidaidien,iddoitac,tenquan,phihoahong,ngaybatdau,ngayketthuc from Hopdong where trangthaiduyet ='Y'";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "Mã số hợp đồng";
            dataGridView1.Columns[1].HeaderText = "Người đại diện";
            dataGridView1.Columns[2].HeaderText = "Mã đối tác";
            dataGridView1.Columns[3].HeaderText = "Tên quán";
            dataGridView1.Columns[4].HeaderText = "Phí hoa hồng";
            dataGridView1.Columns[5].HeaderText = "Ngày bắt đầu";
            dataGridView1.Columns[6].HeaderText = "Ngày kết thúc";



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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

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

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_Taikhoan);
            t.Start();
        }
        private void open_Taikhoan(object obj)
        {
            Application.Run(new Form12());
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
