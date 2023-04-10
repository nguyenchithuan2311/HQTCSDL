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
    public partial class Form2 : Form
    {
        string tendangnhap;
        string matkhau;
        static public string idTaiKhoan;
        Thread t;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Connect();
            tendangnhap = textBox1.Text.Trim().ToString();
            matkhau = textBox2.Text.Trim().ToString();
            string sql = "select tk.idTaiKhoan from TaiKhoan tk where tk.Password= '" + matkhau + "' and tk.Username= '" + tendangnhap + "'";
            idTaiKhoan=Connection.GetFieldValues(sql);
            if(idTaiKhoan!="")
            {
                this.Close();
                t = new Thread(open_Form);
                t.Start();
            }
            Connection.Disconnect();        
        }
        private void open_Form(object obj)
        {
            if (idTaiKhoan.IndexOf("KH") >= 0)
            {
                Application.Run(new Form18());
            }
            else if (idTaiKhoan.IndexOf("DT") >= 0)
            {
                Application.Run(new Form6());
            }
            else if (idTaiKhoan.IndexOf("TX") >= 0)
            {
                Application.Run(new Form12());
            }
            else if (idTaiKhoan.IndexOf("NV") >= 0)
            {
                Application.Run(new Form15());
            }
            else if (idTaiKhoan.IndexOf("QT") >= 0)
            {
                Application.Run(new Form25());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
