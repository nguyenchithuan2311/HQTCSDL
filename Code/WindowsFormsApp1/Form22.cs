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
    
    public partial class Form22 : Form
    {
        Thread t;
        public Form22()
        {
            InitializeComponent();
            init();

        }
        private void init()
        {
            string sql = "select tenmon from mon where idmon='" + Form21.idmon.Trim() + "'";
            Connection.Connect();
            textBox1.Text=Connection.GetFieldValues(sql);
            Connection.Disconnect();
            textBox1.ReadOnly = true;
          
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(dathang);
            t.Start();
        }
        private void dathang()
        {
            Application.Run(new Form21());
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox6.Text==""||textBox7.Text==""||comboBox1.Text=="")
            {
                MessageBox.Show("Nhập đầy đủ để đặt hàng");
            }
            else
            {
                string sql = "select count(iddonhang) from donhang";
                Connection.Connect();
                string buffer1 = (int.Parse(Connection.GetFieldValues(sql))+1).ToString();
                sql="select masohopdong from hopdong where iddoitac='"+Form20.iddoitac+"'";
                string x = "DH" + buffer1;
                
                string buffer2 = Connection.GetFieldValues(sql);
                
                sql = "insert Donhang values ('"+x+"','" + buffer2 + "','" + Form18.idkhachhang + "',null,N'" + comboBox1.Text + "','"+DateTime.Now.ToString("yyyy-MM-dd")+"',N'"+textBox6.Text+"','"+textBox3.Text+ "','"+textBox4.Text+"',N'Đang giao',N'Đã tiếp nhận')";

                Connection.RunSQL(sql);
                sql = "insert qldonhang values('" + Form21.idmon.Trim() + "','" + x + "','" + textBox7.Text + "','" + textBox4.Text + "')";
                MessageBox.Show(sql);
                Connection.RunSQL(sql);
                Connection.Disconnect();
                MessageBox.Show("Đặt hàng thành công");
            }    
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                string sql = "select giamon from mon where idmon='" + Form21.idmon.Trim() + "'";
                Connection.Connect();
                textBox4.Text = (int.Parse(textBox7.Text) * float.Parse(Connection.GetFieldValues(sql))).ToString();
                Connection.Disconnect();
                textBox3.Text = (0.1 * (int.Parse(textBox4.Text))).ToString();
            }
        }
    }
}
