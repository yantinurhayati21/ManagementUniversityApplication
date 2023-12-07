using ManagementUniversityApplication.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementUniversityApplication.Controller
{
    internal class SalaryController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectSalary()
        {
            DataTable dataSalary = new DataTable();
            try
            {
                string selectSalary = "SELECT * FROM Salary";
                da = new MySqlConnector.MySqlDataAdapter(selectSalary, GetConn());
                da.Fill(dataSalary);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataSalary;
        }

        public void addSalary(string SId, string SLrId, string SLrName, int SLrSalary, int SPeriod, DateTime SPDate)
        {
            string add = "insert into Salary values (@SId, @SLrId, @SLrName, @SLrSalary, @SPeriod, @FAmount, @SPDate)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@SId", MySqlConnector.MySqlDbType.VarChar).Value = SId;
                cmd.Parameters.Add("@SLrId", MySqlConnector.MySqlDbType.VarChar).Value = SLrId;
                cmd.Parameters.Add("@SLrName", MySqlConnector.MySqlDbType.VarChar).Value = SLrName;
                cmd.Parameters.Add("@SLrSalary", MySqlConnector.MySqlDbType.Decimal).Value = SLrSalary;
                cmd.Parameters.Add("@SPeriod", MySqlConnector.MySqlDbType.Int32).Value = SPeriod;
                cmd.Parameters.Add("@SPDate", MySqlConnector.MySqlDbType.VarChar).Value = SPDate;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public DataTable searchSalary(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Salary where concat(SId, SLrId, SLrName, SLrSalary, SPeriod, SPDate)like '%" + search + "%'", koneksi.GetConn());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return table;
        }
    }
}
