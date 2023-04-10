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
    public partial class Form11 : Form
    {
        Thread t;
        private string masohopdong;
        public Form11()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[0].HeaderText == "ID đơn hàng")
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["idmon"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["idchinhanh"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["tenkh"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["sdtkh"].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells["tenTX"].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells["sdttx"].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells["trangthaidonhang"].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells["trangthaivanchuyen"].Value.ToString();
                textBox11.Text = dataGridView1.CurrentRow.Cells["tongphi"].Value.ToString();
            }
            
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select Masohopdong from Hopdong where iddoitac='" + Form6.idDoiTac + "'";
            masohopdong = Connection.GetFieldValues(sql).Trim();

            sql = "Select dh.idDonHang,qldh.idmon,qltd.idchinhanh,kh.tenkh,kh.sdtkh,tx.tenTX,tx.sdttx,dh.trangthaivanchuyen,dh.trangthaidonhang,(dh.Phivanchuyen+dh.Phisanpham) as tongphi from DonHang dh,QLdonhang qldh,QLTHucdon qltd,khachhang kh,taixe tx where dh.MaSoHopDong='" + masohopdong + "' and dh.idkh=kh.idkh and dh.idtaixe=tx.idtaixe and qldh.iddonhang=dh.iddonhang and qltd.idmon=qldh.idmon ";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
            dataGridView1.Columns[1].HeaderText = "ID món";
            dataGridView1.Columns[2].HeaderText = "ID chi nhánh";
            dataGridView1.Columns[3].HeaderText = "Tên khách hàng";
            dataGridView1.Columns[4].HeaderText = "SDT khách hàng";
            dataGridView1.Columns[5].HeaderText = "Tên tài xế";
            dataGridView1.Columns[6].HeaderText = "SDT tài xế";
            dataGridView1.Columns[7].HeaderText = "Trạng thái vận chuyển";
            dataGridView1.Columns[8].HeaderText = "Trạng thái đơn hàng";
            dataGridView1.Columns[9].HeaderText = "Tổng tiền";

            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.Columns[7].Width = 200;
            dataGridView1.Columns[8].Width = 200;
            dataGridView1.Columns[9].Width = 200;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

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

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "Đã tiếp nhận" && textBox8.Text != "Đã xử lý")
            {
                MessageBox.Show("Chỉ nhận nhập đã tiếp nhận hoặc đã xử lý");
            }
            else
            {
                string sql = "update donhang set trangthaidonhang=N'" + textBox8.Text + "'where iddonhang='" + dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString() + "'";
                Connection.Connect();
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Connection.Connect();
            string sql = "select Ngaytao,count(*) from donhang where masohopdong='" + masohopdong + "' group by (ngaytao)";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);
            dataGridView1.Columns[0].HeaderText = "Ngày";
            dataGridView1.Columns[1].HeaderText = "số đơn";

            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 500;
            dataGridView1.Columns[1].Width = 500;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            Connection.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Connect();
            string sql = "select format(ngaytao,'MM-yyyy'),count(*) from donhang where masohopdong='" + masohopdong + "' group by (format(ngaytao,'MM-yyyy'))";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);
            dataGridView1.Columns[0].HeaderText = "Tháng";
            dataGridView1.Columns[1].HeaderText = "số đơn";

            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 500;
            dataGridView1.Columns[1].Width = 500;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            Connection.Disconnect();
        }
    }
}
