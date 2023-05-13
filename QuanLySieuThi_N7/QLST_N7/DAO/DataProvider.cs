using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance 
        { 
            get {if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider()
        {
            
        }

        private String strCnn = ConfigurationManager.ConnectionStrings["qlst"].ConnectionString;

        public DataTable ExecuteQuery(string strQuerry, object[] parameter = null)
        {
            DataTable datatable = new DataTable();

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = strCnn;

                cnn.Open();

                SqlCommand command = new SqlCommand(strQuerry, cnn);
                if(parameter != null)
                {
                    string[] listPara = strQuerry.Split(' ');
                    int i = 0;
                    foreach(string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter dap = new SqlDataAdapter(command);

                dap.Fill(datatable);

                cnn.Close();
            }    
            return datatable;
        }

        public int ExecuteNonQuery(string strQuerry, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = strCnn;

                cnn.Open();

                SqlCommand command = new SqlCommand(strQuerry, cnn);
                if (parameter != null)
                {
                    string[] listPara = strQuerry.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();

                cnn.Close();
            }
            return data;
        }

        public object ExecuteScalar(string strQuerry, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = strCnn;

                cnn.Open();

                SqlCommand command = new SqlCommand(strQuerry, cnn);
                if (parameter != null)
                {
                    string[] listPara = strQuerry.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();

                cnn.Close();
            }
            return data;
        }
    }
}
