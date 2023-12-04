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

        public void addFees(int FId, int FStId, string FStName, int FDepId, int FPeriod, int FAmount, DateTime PayDate)
        {
            string add = "insert into Fees values (@FId, @FStId, @FStName, @FDepId, @FPeriod, @FAmount, @PayDate)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@FId", MySqlConnector.MySqlDbType.Int32).Value = FId;
                cmd.Parameters.Add("@FStId", MySqlConnector.MySqlDbType.Int32).Value = FStId;
                cmd.Parameters.Add("@FStName", MySqlConnector.MySqlDbType.VarChar).Value = FStName;
                cmd.Parameters.Add("@FDepId", MySqlConnector.MySqlDbType.Int32).Value = FDepId;
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

        public void updateFees(int FId, int FStId, string FStName, int FDepId, string FPeriod, string FAmount, DateTime PayDate)
        {
            string update = "update Fees set FStId=@FStId, FStName=@FStName, FDepId=@FDepId, FPeriod=@FPeriod, FAmount=@FAmount, PayDate=@PayDate where FId=" + FId;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@FId", MySqlConnector.MySqlDbType.Int32).Value = FId;
                cmd.Parameters.Add("@FStId", MySqlConnector.MySqlDbType.Int32).Value = FStId;
                cmd.Parameters.Add("@FStName", MySqlConnector.MySqlDbType.VarChar).Value = FStName;
                cmd.Parameters.Add("@FDepId", MySqlConnector.MySqlDbType.Int32).Value = FDepId;
                cmd.Parameters.Add("@FPeriod", MySqlConnector.MySqlDbType.VarChar).Value = FPeriod;
                cmd.Parameters.Add("@FAmount", MySqlConnector.MySqlDbType.VarChar).Value = FAmount;
                cmd.Parameters.Add("@PayDate", MySqlConnector.MySqlDbType.VarChar).Value = PayDate;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteFees(int FId)
        {
            string delete = "delete from Fees where FId = @FId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@FId", MySqlConnector.MySqlDbType.Int32).Value = FId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchFees(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Fees where concat(FId, FStId, FStName, FDepId, FPeriod, FAmount, PayDate)like '%" + search + "%'", koneksi.GetConn());
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
