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
    internal class LearningController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectLearning()
        {
            DataTable dataLearning = new DataTable();
            try
            {
                string selectLearning = "SELECT * FROM Learning";
                da = new MySqlConnector.MySqlDataAdapter(selectLearning, GetConn());
                da.Fill(dataLearning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataLearning;
        } 

        public void addLearning(int LrnId, int LrnStId, string LrnStName, int LrnCId, string LrCName, string LrCRoom, DateTime LrnTimes, int LrnLrId,string LrnLrName, int LrnDuration)
        {
            string add = "insert into Learning values (@LrnId, @LrnStId, @LrnStName, @LrnCId, @LrCName, @LrCRoom, @LrnTimes, @LrnLrId, @LrnLrName, @LrnDuration)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@LrnId", MySqlConnector.MySqlDbType.Int32).Value = LrnId;
                cmd.Parameters.Add("@LrnStId", MySqlConnector.MySqlDbType.Int32).Value = LrnStId;
                cmd.Parameters.Add("@LrnStName", MySqlConnector.MySqlDbType.VarChar).Value = LrnStName;
                cmd.Parameters.Add("@LrnCId", MySqlConnector.MySqlDbType.Int32).Value = LrnCId;
                cmd.Parameters.Add("@LrCName", MySqlConnector.MySqlDbType.VarChar).Value = LrCName;
                cmd.Parameters.Add("@LrCRoom", MySqlConnector.MySqlDbType.VarChar).Value = LrCRoom;
                cmd.Parameters.Add("@LrnTimes", MySqlConnector.MySqlDbType.VarChar).Value = LrnTimes;
                cmd.Parameters.Add("@LrnLrId", MySqlConnector.MySqlDbType.Int32).Value = LrnLrId;
                cmd.Parameters.Add("@LrnLrName", MySqlConnector.MySqlDbType.VarChar).Value = LrnLrName;
                cmd.Parameters.Add("@LrnDuration", MySqlConnector.MySqlDbType.Int32).Value = LrnDuration;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public void updateLearning(int LrnId, int LrnStId, string LrnStName, int LrnCId, string LrCName, string LrCRoom, DateTime LrnTimes, int LrnLrId, string LrnLrName, int LrnDuration)
        {
            string update = "update Learning set LrnStId=@LrnStId, LrnStName=@LrnStName, LrnCId=@LrnCId, LrCName=@LrCName, LrCRoom=@LrCRoom, LrnTimes=@LrnTimes, LrnLrId=@LrnLrId, LrnLrName=@LrnLrName, LrnDuration=@LrnDuration where LrnId=" + LrnId;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@LrnId", MySqlConnector.MySqlDbType.Int32).Value = LrnId;
                cmd.Parameters.Add("@LrnStId", MySqlConnector.MySqlDbType.Int32).Value = LrnStId;
                cmd.Parameters.Add("@LrnStName", MySqlConnector.MySqlDbType.VarChar).Value = LrnStName;
                cmd.Parameters.Add("@LrnCId", MySqlConnector.MySqlDbType.Int32).Value = LrnCId;
                cmd.Parameters.Add("@LrCName", MySqlConnector.MySqlDbType.VarChar).Value = LrCName;
                cmd.Parameters.Add("@LrCRoom", MySqlConnector.MySqlDbType.VarChar).Value = LrCRoom;
                cmd.Parameters.Add("@LrnTimes", MySqlConnector.MySqlDbType.VarChar).Value = LrnTimes;
                cmd.Parameters.Add("@LrnLrId", MySqlConnector.MySqlDbType.Int32).Value = LrnLrId;
                cmd.Parameters.Add("@LrnLrName", MySqlConnector.MySqlDbType.VarChar).Value = LrnLrName;
                cmd.Parameters.Add("@LrnDuration", MySqlConnector.MySqlDbType.Int32).Value = LrnDuration;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteLearning(int LrnId)
        {
            string delete = "delete from Learning where LrnId = @LrnId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@LrnId", MySqlConnector.MySqlDbType.Int32).Value = LrnId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchLearning(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Learning where concat(LrnId, LrnStId, LrnStName, LrnCId, LrCName, LrCRoom, LrnTimes, LrnLrId, LrnLrName, LrnDuration)like '%" + search + "%'", koneksi.GetConn());
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
