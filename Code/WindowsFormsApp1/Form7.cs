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
using System.Windows.Media.Animation;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        Thread t;
        public Form7()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Tên Quán",175);
            listView1.Columns.Add("Địa chỉ", 175);
            
        }
        private void Adding(string DiaChi,string TenQuan )
        {
            if(DiaChi==""||TenQuan=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên Chi Nhánh và Địa chỉ");
                return;
            }    
            string[] row = { TenQuan,DiaChi};
            ListViewItem item=new ListViewItem(row);
            listView1.Items.Add(item);
        }

        private void Delete()
        {
            listView1.Items.RemoveAt(listView1.SelectedIndices[0]);

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime date = dateTimePicker1.Value;
            DateTime x = DateTime.Now;
            if(textBox1.Text=="")
            {
                MessageBox.Show("Vui lòng nhập số chi nhánh");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập người đại diện");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên quán");
            }
            else if (listView1.Items.Count!= int.Parse(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ chi nhánh");
            }
            else if(date<=x)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu khác");
            }
            else if (date >= dateTimePicker2.Value)
            {
                MessageBox.Show("Vui lòng chọn ngày kết thúc khác");
            }
            Connection.Connect();
            string sql = "select count(*) from HopDong hd where hd.MaSoHopDong LIKE 'HD%'";
            string buffer = Connection.GetFieldValues(sql);
            int temp = int.Parse(buffer) + 1;
            if(temp<10)
            {
                buffer = "DT00"+ temp.ToString();
            }
            else if(temp < 100)
            {
                buffer = "DT0" + temp.ToString();
            }
            else
            {
                buffer = "DT" + temp.ToString();
            }
            sql = "update HopDong set Ngaylap='" + x.ToString("yyyy/MM/dd") + "',tenquan='"+textBox3.Text+ "',nguoidaidien='"+textBox2.Text+"',phikichhoat='"+1000000+ "',phihoahong='"+10+ "',ngaybatdau='"+ dateTimePicker1.Value.ToString("yyyy/MM/dd") + "',ngayketthuc='"+ dateTimePicker2.Value.ToString("yyyy/MM/dd") + "',trangthaiduyet='N' where idtaikhoan='"+Form2.idTaiKhoan+"'";
            Connection.RunSQL(sql);
            //-----------------------
            sql = "update Doitac set slchinhanh='"+ int.Parse(textBox1.Text) + "'";
            Connection.RunSQL(sql);
            sql = "select count(*) from ChiNhanh";
            buffer = Connection.GetFieldValues(sql);
            temp = int.Parse(buffer) + 1;
            sql="delete qlthucdon where iddoitac='"+Form6.idDoiTac+"'";
            Connection.RunSQL(sql);
            sql = "delete chinhanh where iddoitac='" + Form6.idDoiTac + "'";
            Connection.RunSQL(sql);
            for (int i=0;i< int.Parse(textBox1.Text);i=i+1)
            {
                MessageBox.Show(listView1.Items[i].SubItems[i].Text);
                MessageBox.Show(listView1.Items[i].SubItems[i+1].Text);
                sql = "insert ChiNhanh values ('"+temp+"','"+Form6.idDoiTac+"','"+listView1.Items[i].SubItems[i + 1].Text+ "','"+listView1.Items[i].SubItems[i].Text+"','07:00:00',N'Mở cửa')";
                Connection.RunSQL(sql);
                temp = temp + 1;
            } 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Adding(textBox4.Text, textBox5.Text);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

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
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_XemHopDongDaLap);
            t.Start();
        }
        private void open_XemHopDongDaLap(object obj)
        {
            Application.Run(new Form8());
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

    }
}
