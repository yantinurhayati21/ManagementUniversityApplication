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
    internal class FeesController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectFees()
        {
            DataTable dataFees = new DataTable();
            try
            {
                string selectFees = "SELECT * FROM Fees";
                da = new MySqlConnector.MySqlDataAdapter(selectFees, GetConn());
                da.Fill(dataFees);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataFees;
        }

        public void addFees(string FId, string FStId, string FStName, string FDepId, string FDepName, int FPeriod, int FAmount, DateTime PayDate)
        {
            string add = "insert into Fees values (@FId, @FStId, @FStName, @FDepId,@FDepName, @FPeriod, @FAmount, @PayDate)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@FId", MySqlConnector.MySqlDbType.VarChar).Value = FId;
                cmd.Parameters.Add("@FStId", MySqlConnector.MySqlDbType.VarChar).Value = FStId;
                cmd.Parameters.Add("@FStName", MySqlConnector.MySqlDbType.VarChar).Value = FStName;
                cmd.Parameters.Add("@FDepId", MySqlConnector.MySqlDbType.VarChar).Value = FDepId;
                cmd.Parameters.Add("@FDepName", MySqlConnector.MySqlDbType.VarChar).Value = FDepName;
                cmd.Parameters.Add("@FPeriod", MySqlConnector.MySqlDbType.Int32).Value = FPeriod;
                cmd.Parameters.Add("@FAmount", MySqlConnector.MySqlDbType.Int32).Value = FAmount;
                cmd.Parameters.Add("@PayDate", MySqlConnector.MySqlDbType.VarChar).Value = PayDate;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public DataTable searchFees(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Fees where concat(FId, FStId, FStName, FDepId, FPeriod, FDepName, FAmount, PayDate)like '%" + search + "%'", koneksi.GetConn());
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
