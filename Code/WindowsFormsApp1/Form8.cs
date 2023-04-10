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
    public partial class Form8 : Form
    {
        Thread t;
        public static DateTime ngayhethanhopdong;
        public Form8()
        {
            InitializeComponent();
            LoadData();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            dateTimePicker1.Enabled= false;
            dateTimePicker2.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            Connection.Connect();
            MessageBox.Show(Form6.idDoiTac);
            string sql = "Select dt.MaSoThue,dt.NguoiDaiDien,hd.trangthaiduyet,dt.SLChiNhanh,hd.NgayBatDau,hd.NgayKetThuc from DoiTac dt,HopDong hd where dt.idDoiTac='"+Form6.idDoiTac+ "' and hd.iddoitac=dt.idDoiTac";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "Mã số thuế";
            dataGridView1.Columns[1].HeaderText = "Người đại diện";
            dataGridView1.Columns[2].HeaderText = "Duyệt hay Chưa";
            dataGridView1.Columns[3].HeaderText = "Số Chi Nhánh";
            dataGridView1.Columns[4].HeaderText = "Ngày lập";
            dataGridView1.Columns[5].HeaderText = "Ngày kết thúc";


            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 135;
            dataGridView1.Columns[3].Width = 145;
            dataGridView1.Columns[4].Width = 145;
            dataGridView1.Columns[5].Width = 145;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            Connection.Disconnect();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["MaSoThue"].Value.ToString().Trim();
            textBox2.Text = dataGridView1.CurrentRow.Cells["SLChiNhanh"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["NguoiDaiDien"].Value.ToString();
            if(dataGridView1.CurrentRow.Cells["TrangThaiDuyet"].Value.ToString()=="Y")
            {
                textBox3.Text = "Đã duyệt";
            }
            else
            {
                textBox3.Text = "Chưa duyệt";
            }
            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells["NgayBatDau"].Value;
            dateTimePicker2.Value = (DateTime)dataGridView1.CurrentRow.Cells["NgayKetThuc"].Value;
            
            textBox6.Text = (dateTimePicker2.Value - DateTime.Now).ToString("dd");
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ngayhethanhopdong = (DateTime)dataGridView1.CurrentRow.Cells["NgayKetThuc"].Value;
            Form24 temp = new Form24();
            temp.ShowDialog();
            Connection.Connect();
            string sql = "Update HopDong set NgayKetThuc='"+Form24.NgayGiaHan.ToString("yyyy-MM-dd")+"'where iddoitac='"+Form6.idDoiTac+"'";
            MessageBox.Show(sql);
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_ChiNhanh);
            t.Start();
        }
        private void open_ChiNhanh(object obj)
        {
            Application.Run(new Form9());
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
        private void open_ADDHopDong(object obj)
        {
            Application.Run(new Form7());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
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

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_TaiKhoan);
            t.Start();
        }
        private void open_TaiKhoan(object obj)
        {
            Application.Run(new Form6());
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
    }
}
