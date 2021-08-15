using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.DAL
{
    class DBHelper
    {
        private static DBHelper _Instance;
        SqlConnection cnn;
        public static DBHelper Instance
        {

            get
            {
                if (_Instance == null)
                {
                    string s = @"Data Source=LAPTOP-ALGLTSVF\SQLEXPRESS;Initial Catalog=QuanLiNhaHang;Integrated Security=True";
                    _Instance = new DBHelper(s);
                };
                return _Instance;
            }
            private set { }
        }

        private DBHelper(string s)
        {
            cnn = new SqlConnection(s);
        }
        public bool ExecuteNonQuery(string query)
        {
            bool kq;
            SqlCommand cmd = new SqlCommand(query, cnn);
            cnn.Open();
            int n=cmd.ExecuteNonQuery();
            if (n > 0)
            {
                kq = true;
            }
            else
            {
                kq = false;
            }
            cnn.Close();
            return kq;
        }
        public DataTable ExecuteQuery(string query)
        {
            
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                da.Fill(data);
                cnn.Close();
                return data;
            

        }
    
    //View Stored Procedure
    public static DataTable ViewStoredProc(string procName, int SoHD)
        {
            string s = @"Data Source=LAPTOP-ALGLTSVF\SQLEXPRESS;Initial Catalog=QuanLiNhaHang;Integrated Security=True";
            DataTable dt = new DataTable();
            SqlConnection connect = new SqlConnection(s);
            SqlCommand command = connect.CreateCommand();
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SoHD", SoHD);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            connect.Close();
            return dt;
        }
    }
}

