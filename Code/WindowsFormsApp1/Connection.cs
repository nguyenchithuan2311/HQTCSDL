using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace WindowsFormsApp1
{
    internal class Connection
    {
        private static string exactly_server_name = @"Data Source=DESKTOP-9QUAAMR;Initial Catalog=QL_DH_GH;Integrated Security=True";
        public static SqlConnection Con;
        public static void Connect()
        {
            Con = new SqlConnection();
            Con.ConnectionString = exactly_server_name;
            Con.Open();

            //Kiểm tra kết nối
            if (Con.State == ConnectionState.Open)
            {
                MessageBox.Show("Kết nối DB thành công");
            }
            else MessageBox.Show("Không thể kết nối với DB");
        }
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                //Đóng kết nối
                Con.Close();

                //Giải phóng tài nguyên
                Con.Dispose();
                Con = null;

                //Kiểm tra kết nối
                //MessageBox.Show("Đóng Kết nối DB thành công");
            }
        }
        public static DataTable GetDataToTable(string sql) //Lấy dữ liệu đổ vào bảng
        {
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand();

            //Kết nối cơ sở dữ liệu
            dap.SelectCommand.Connection = Connection.Con;
            dap.SelectCommand.CommandText = sql;

            DataTable table = new DataTable();
            dap.Fill(table);
            return table;
        }
        public static void RunSQL(string sql) // chạy câu lệnh sql
        {
            SqlCommand cmd = new SqlCommand();

            //Gán kết nối
            cmd.Connection = Con;

            //Gán lệnh SQL
            cmd.CommandText = sql;

            //Thực hiện câu lệnh SQL
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //Giải phóng bộ nhớ
            cmd.Dispose();
            cmd = null;
        }
        public static string GetFieldValues(string sql) // lấy dữ liệu từ câu lệnh sql
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }

    }
}
