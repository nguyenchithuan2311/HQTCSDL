using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        Thread t;
        public Form10()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select M.idMon,M.TenMon, M.GiaMon,qltd.mieutamon,qltd.tinhtrangmon,qltd.tuychonchomon from Mon M,QLThucDon qltd where qltd.IDDoiTac='" + Form6.idDoiTac + "' and m.idMon=qltd.idmon";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID món";
            dataGridView1.Columns[1].HeaderText = "Tên món";
            dataGridView1.Columns[2].HeaderText = "Giá món";
            dataGridView1.Columns[3].HeaderText = "miêu tả món";
            dataGridView1.Columns[4].HeaderText = "Tình trạng món";
            dataGridView1.Columns[5].HeaderText = "Tùy chọn cho món";

            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.AllowUserToAddRows = false;

            Connection.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Connect();
            if (textBox1.Text==""||textBox2.Text==""||textBox4.Text==""|| comboBox1.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ để thêm");
            }
            else if(Connection.GetFieldValues("select idChiNhanh from chinhanh where idChiNhanh='"+ textBox6.Text + "' and idDoiTac='"+ Form6.idDoiTac + "'") =="")
            {
                MessageBox.Show("ID chi nhánh không đúng vui lòng xem lại");
            }    
            else
            {
                string sql = "select TOP 1 IDMon from QLThucdon order by IDmon desc";
                string buffer = Connection.GetFieldValues(sql);
                if (buffer == "")
                {
                    buffer = "1";
                }
                else
                {
                    int temp = int.Parse(buffer) + 1;
                    buffer = temp.ToString();
                }
             
                sql = "insert Mon values ('" + buffer + "',N'" + textBox4.Text + "','" + 5 + "','" + int.Parse(textBox2.Text) + "')";
                Connection.RunSQL(sql);
                sql = "insert QLthucdon values ('" + Form6.idDoiTac + "','" + buffer + "','" + textBox6.Text + "',N'" + textBox1.Text + "',N'" + comboBox1.Text + "',N'" + textBox5.Text + "')";
                Connection.RunSQL(sql);
                LoadData();
            }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "delete QLthucdon where idMon='" + dataGridView1.CurrentRow.Cells["IDMon"].Value.ToString() + "'";
            Connection.Connect();
            Connection.RunSQL(sql);
            sql = "delete Mon where idMon='" + dataGridView1.CurrentRow.Cells["IDMon"].Value.ToString() + "'";
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadData();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "update Mon set tenmon=N'" + dataGridView1.CurrentRow.Cells["TenMon"].Value.ToString() + "',Giamon='" + dataGridView1.CurrentRow.Cells["giaMon"].Value.ToString() + "' where idMon='" + dataGridView1.CurrentRow.Cells["IDMon"].Value.ToString() + "'";
            Connection.Connect();
            Connection.RunSQL(sql);
            sql = "update QLthucdon set mieutamon=N'" + dataGridView1.CurrentRow.Cells["Mieutamon"].Value.ToString() + "',TinhTrangMon=N'" + dataGridView1.CurrentRow.Cells["tinhtrangmon"].Value.ToString() + "',Tuychonchomon=N'" + dataGridView1.CurrentRow.Cells["tuychonchomon"].Value.ToString() + "' where idMon='" + dataGridView1.CurrentRow.Cells["IDMon"].Value.ToString() + "'";
            Connection.RunSQL(sql);
            Connection.Disconnect();
            LoadData();
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
