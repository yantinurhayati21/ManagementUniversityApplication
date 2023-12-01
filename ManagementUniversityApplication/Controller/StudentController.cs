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
    internal class StudentController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectStudents()
        {
            DataTable dataStudents = new DataTable();
            try
            {
                string selectStudents = "SELECT * FROM Students";
                da = new MySqlConnector.MySqlDataAdapter(selectStudents, GetConn());
                da.Fill(dataStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataStudents;
        }

        public void addStudents(int StId, string StNim, string StName, DateTime StDOB, string StGen, int StSem, int StDepId, string StDepName, byte[] StPhoto)
        {
            string add = "insert into Students values (@StId, @StNim, @StName, @StDOB, @StGen, @StSem, @StDepId, @StDepName, @StPhoto)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@StId", MySqlConnector.MySqlDbType.Int32).Value = StId;
                cmd.Parameters.Add("@StNim", MySqlConnector.MySqlDbType.VarChar).Value = StNim;
                cmd.Parameters.Add("@StName", MySqlConnector.MySqlDbType.VarChar).Value = StName;
                cmd.Parameters.Add("@StDOB", MySqlConnector.MySqlDbType.VarChar).Value = StDOB;
                cmd.Parameters.Add("@StGen", MySqlConnector.MySqlDbType.VarChar).Value = StGen;
                cmd.Parameters.Add("@StSem", MySqlConnector.MySqlDbType.Int32).Value = StSem;
                cmd.Parameters.Add("@StDepId", MySqlConnector.MySqlDbType.Int32).Value = StDepId;
                cmd.Parameters.Add("@StDepName", MySqlConnector.MySqlDbType.VarChar).Value = StDepName;
                cmd.Parameters.Add("@StPhoto", MySqlConnector.MySqlDbType.Blob).Value = StPhoto;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public void updateStudentst(int StId, string StNim, string StName, DateTime StDOB, string StGen, int StSem, int StDepId, string StDepName, byte[] StPhoto)
        {
            string update = "update Students set StNim=@StNim, StName=@StName, StDOB=@StDOB, StGen=@StGen, StSem=@StSem, StDepId=@StDepId, StDepName=@StDepName where StId=" + StId;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@StId", MySqlConnector.MySqlDbType.Int32).Value = StId;
                cmd.Parameters.Add("@StNim", MySqlConnector.MySqlDbType.VarChar).Value = StNim;
                cmd.Parameters.Add("@StName", MySqlConnector.MySqlDbType.VarChar).Value = StName;
                cmd.Parameters.Add("@StDOB", MySqlConnector.MySqlDbType.VarChar).Value = StDOB;
                cmd.Parameters.Add("@StGen", MySqlConnector.MySqlDbType.VarChar).Value = StGen;
                cmd.Parameters.Add("@StSem", MySqlConnector.MySqlDbType.Int32).Value = StSem;
                cmd.Parameters.Add("@StDepId", MySqlConnector.MySqlDbType.Int32).Value = StDepId;
                cmd.Parameters.Add("@StDepName", MySqlConnector.MySqlDbType.VarChar).Value = StDepName;
                cmd.Parameters.Add("@StPhoto", MySqlConnector.MySqlDbType.Blob).Value = StPhoto;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteStudents(int StId)
        {
            string delete = "delete from Students where StId = @StId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@StId", MySqlConnector.MySqlDbType.Int32).Value = StId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchStudents(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Students where concat(StId, StNim, StName, StDOB, StGen, StSem, StDepId, StDepName)like '%" + search + "%'", koneksi.GetConn());
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
