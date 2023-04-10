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
    public partial class Form13 : Form
    {
        Thread t;
        public Form13()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString();
            textBox2.Text= dataGridView1.CurrentRow.Cells["tenkh"].Value.ToString();
            textBox3.Text= dataGridView1.CurrentRow.Cells["sdtkh"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["ngaytao"].Value.ToString();
            textBox5.Text= dataGridView1.CurrentRow.Cells["phivanchuyen"].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells["hinhthucthanhtoan"].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells["diachigiaohang"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["phisanpham"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["tongphi"].Value.ToString();
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select dh.iddonHang,dh.ngaytao,dh.phivanchuyen,dh.hinhthucthanhtoan,dh.phisanpham,dh.diachigiaohang,kh.tenKH,kh.SDTKH,(dh.phisanpham+dh.phivanchuyen) as tongphi from DonHang dh, khachhang kh where dh.idTaixe is null and dh.idkh=kh.idkh";
            MessageBox.Show(sql);
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
            dataGridView1.Columns[1].HeaderText = "ngày tạo";
            dataGridView1.Columns[2].HeaderText = "phí vận chuyển";
            dataGridView1.Columns[3].HeaderText = "hình thức thanh toán";
            dataGridView1.Columns[4].HeaderText = "phí sản phẩm";
            dataGridView1.Columns[5].HeaderText = "Điạ chi giao hang";
            dataGridView1.Columns[6].HeaderText = "Tên KH";
            dataGridView1.Columns[7].HeaderText = "SĐT khách hàng";
            dataGridView1.Columns[8].HeaderText = "Tổng phí";


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
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            

            Connection.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql="update donhang set IDtaixe='"+Form12.idtaixe+"'where iddonhang='"+dataGridView1.CurrentRow.Cells["iddonhang"].Value.ToString()+"'";
            Connection.Connect();
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadData();
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
