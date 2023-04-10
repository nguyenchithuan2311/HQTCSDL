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
    public partial class Form21 : Form
    {
        Thread t;
        public static string idmon;
        public Form21()
        {
            InitializeComponent();
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
                string sql="select sdtkh,diachikh from khachhang where idtaikhoan='"+Form2.idTaiKhoan+"'";
                Connection.Connect();
                DataTable table = Connection.GetDataToTable(sql);
                DataRow x = table.Rows[0];
                Connection.Disconnect();
                if (x["sdtkh"].ToString()=="" || x["diachikh"].ToString() == "")
                {
                    MessageBox.Show("Vui lòng cập nhật thông tin trước khi đặt hàng");
                }  
                else
                {
                    this.Close();
                    t = new Thread(confirm);
                    t.Start();
                }    
               
        }
        private void confirm()
        {
            Application.Run(new Form22());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(dathang);
            t.Start();
        }
        private void dathang()
        {
            Application.Run(new Form20());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idmon= dataGridView1.CurrentRow.Cells["IDmon"].Value.ToString();
            textBox1.Text= dataGridView1.CurrentRow.Cells["tenmon"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["tinhtrangmon"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["tenchinhanh"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["giamon"].Value.ToString();
        }
        private void LoadData()
        {
            Connection.Connect();
            string sql = "Select m.idmon,tinhtrangmon,TenMon,tenchinhanh,giamon from qlthucdon qltd,mon m,chinhanh cn where qltd.idDoiTac='" + Form20.iddoitac+ "'and qltd.idmon=m.idmon and cn.idchinhanh=qltd.idchinhanh";
            dataGridView1.DataSource = Connection.GetDataToTable(sql);

            dataGridView1.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].HeaderText = "ID món";
            dataGridView1.Columns[1].HeaderText = "Tình trạng món";
            dataGridView1.Columns[2].HeaderText = "Tên món";
            dataGridView1.Columns[3].HeaderText = "Tên chi nhánh";
            dataGridView1.Columns[4].HeaderText = "Giá món";
            


            dataGridView1.DefaultCellStyle.Font = new Font("Time New Roman", 13);
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 250;
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
