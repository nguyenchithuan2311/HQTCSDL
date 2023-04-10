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
    public partial class Form14 : Form
    {
        Thread t;
        public Form14()
        {
            InitializeComponent();
            init();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void init()
        {
            string sql = "select iddonhang, trangthaivanchuyen, phivanchuyen from donhang where idtaixe='" + Form12.idtaixe + "' ";
            Connection.Connect();
            dataGridView1.DataSource = Connection.GetDataToTable(sql);
            

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
            dataGridView1.Columns[1].HeaderText = "trạng thái vận chuyển";
            dataGridView1.Columns[2].HeaderText = "phí vận chuyển";

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            Connection.Disconnect();

        }
        private void loadData()
        {
            float sum = 0;
            for (int i=0;i< dataGridView1.RowCount;i=i+1)
            {
                
                sum = sum + float.Parse(dataGridView1.Rows[i].Cells["phivanchuyen"].Value.ToString());
                
            }
            textBox4.Text = sum.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox6.Text=="" && textBox1.Text != "")
            {
                Connection.Connect();
                
                string buffer1 = ""+textBox1.Text+ "/01/01";
                string buffer2 = "" + textBox1.Text + "/12/31";
                DateTime dt1 = Convert.ToDateTime(buffer1);
                DateTime dt2 = Convert.ToDateTime(buffer2);
                string sql = "Select dh.iddonHang,dh.phivanchuyen from DonHang dh where idtaixe='" + Form12.idtaixe + "' and ngaytao<='"+dt2.ToString("yyyy-MM-dd")+"' and ngaytao>='"+ dt1.ToString("yyyy-MM-dd") + "'";
                MessageBox.Show(sql);
                Connection.RunSQL(sql);

                dataGridView1.DataSource = Connection.GetDataToTable(sql);

                dataGridView1.Font = new Font("Time New Roman", 13);
                dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
                dataGridView1.Columns[1].HeaderText = "phí vận chuyển";


                dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
                dataGridView1.Columns[0].Width = 600;
                dataGridView1.Columns[1].Width = 600;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;


                Connection.Disconnect();
                loadData();
            }
            else if(textBox6.Text != "" && textBox1.Text != "")
            {
                Connection.Connect();

                string buffer1 = "" + textBox1.Text + "/" + textBox6.Text + "/01";
                string buffer2 = "" + textBox1.Text + "/" + textBox6.Text + "/31";
                DateTime dt1 = Convert.ToDateTime(buffer1);
                DateTime dt2 = Convert.ToDateTime(buffer2);
                string sql = "Select dh.iddonHang,dh.phivanchuyen from DonHang dh where idtaixe='" + Form12.idtaixe + "' and ngaytao<='" + dt2.ToString("yyyy-MM-dd") + "' and ngaytao>='" + dt1.ToString("yyyy-MM-dd") + "'";
                MessageBox.Show(sql);
                Connection.RunSQL(sql);

                dataGridView1.DataSource = Connection.GetDataToTable(sql);

                dataGridView1.Font = new Font("Time New Roman", 13);
                dataGridView1.Columns[0].HeaderText = "ID đơn hàng";
                dataGridView1.Columns[1].HeaderText = "phí vận hcuyển";


                dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
                dataGridView1.Columns[0].Width = 600;
                dataGridView1.Columns[1].Width = 600;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;


                Connection.Disconnect();
                loadData();
            }    
                
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_DSDonHang);
            t.Start();
        }
        private void open_DSDonHang(object obj)
        {
            Application.Run(new Form13());
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text!="" && dataGridView1.Columns[1].HeaderText == "trạng thái vận chuyển")
            {
                string sql="update donhang set trangthaivanchuyen=N'"+comboBox1.Text+ "'where iddonhang='"+dataGridView1.CurrentRow.Cells["iddonHang"].Value.ToString()+"'";
                Connection.Connect();
                
                Connection.RunSQL(sql);
                Connection.Disconnect();
                init();
            }    
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
