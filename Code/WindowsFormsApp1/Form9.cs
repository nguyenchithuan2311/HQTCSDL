using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        Thread t;
        public Form9()
        {
            InitializeComponent();
            LoadData();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select IDChiNhanh,DiaChi,TenChiNhanh,TGHD,TTCH from ChiNhanh where IDDoiTac='"+Form6.idDoiTac+"'";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID chi nhánh";
            dataGridView1.Columns[1].HeaderText = "Địa Chỉ";
            dataGridView1.Columns[2].HeaderText = "Tên Chi Nhánh";
            dataGridView1.Columns[3].HeaderText = "Thời gian mở cửa";
            dataGridView1.Columns[4].HeaderText = "Tình trạng cửa hàng";
            
            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 250;
            dataGridView1.Columns[4].Width = 250;


            dataGridView1.AllowUserToAddRows = false;
            
            Connection.Disconnect();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||dateTimePicker2.Text==""||textBox4.Text==""||comboBox1.Text=="")
            {
                MessageBox.Show("Nhập đầy đủ để thêm");
            }
            else
            {
                Connection.Connect();
                string sql = "select TOP 1 IDChiNhanh from chinhanh order by IDChiNhanh desc;";
                string buffer = Connection.GetFieldValues(sql);
                int temp = int.Parse(buffer) + 1;
                buffer = temp.ToString();
                
                sql = "insert Chinhanh values ('" + buffer + "','" + Form6.idDoiTac + "','" + textBox4.Text + "','" + textBox1.Text + "','"+dateTimePicker2.Value.ToString("hh:mm:ss")+"',N'"+comboBox1.Text+"')";
                Connection.RunSQL(sql);
                sql = "update Doitac set SLChiNhanh=SLChiNhanh+1 where Iddoitac='" + Form6.idDoiTac + "'";
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            } 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["TTCH"].Value.ToString() != "Mở cửa" && dataGridView1.CurrentRow.Cells["TTCH"].Value.ToString() != "Tạm nghỉ")
            {
                MessageBox.Show("TTCH không phù hợp");
            }
            else
            {
                Connection.Connect();
                string sql = "update ChiNhanh set Diachi=N'" + dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString() + "',TenChiNhanh=N'" + dataGridView1.CurrentRow.Cells["TenChiNhanh"].Value.ToString() + "',TGHD='" + dataGridView1.CurrentRow.Cells["TGHD"].Value.ToString() + "',TTCH=N'" + dataGridView1.CurrentRow.Cells["TTCH"].Value.ToString() + "'where idChiNhanh='"+ dataGridView1.CurrentRow.Cells["idChiNhanh"].Value.ToString() + "'";
                Connection.RunSQL(sql);
                Connection.Disconnect();
                LoadData();
            }
        }
        private bool isValidTime(string thetime)
        {
            Regex checkTime = new Regex(@"^(?:[01]?[0-9]2[0-3]):[0-5][0-9]$");
            return checkTime.IsMatch(thetime);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection.Connect();
            string sql = "delete ChiNhanh where IDChiNhanh='" + dataGridView1.CurrentRow.Cells["idChiNhanh"].Value.ToString() + "'";
            Connection.RunSQL(sql);
            sql = "update Doitac set SLChiNhanh=SLChiNhanh-1 where Iddoitac='" + Form6.idDoiTac + "'";
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadData();
        }
    }
}
