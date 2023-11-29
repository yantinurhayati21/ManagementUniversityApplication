using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace ManagementUniversityApplication.Model
{
    internal class Connection
    {
        public MySqlCommand cmd;
        public DataSet ds;
        public MySqlDataAdapter da;
        public MySqlDataReader dr;
        public MySqlConnection GetConn()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;user=root;database=university";

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed" + ex.Message);
            }
            return conn;
        }
    }
}
