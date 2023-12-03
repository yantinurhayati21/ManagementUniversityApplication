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
    internal class CoursesController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectCourses()
        {
            DataTable dataCourses = new DataTable();
            try
            {
                string selectCourses = "SELECT * FROM Courses";
                da = new MySqlConnector.MySqlDataAdapter(selectCourses, GetConn());
                da.Fill(dataCourses);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataCourses;
        }

        public void addCourses(int CId, string CName, int CPrice, string CRoom, int CLrId, string CLrName)
        {
            string add = "insert into Courses values (@CId, @CName, @CPrice, @CRoom, @CLrId, @CLrName)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@CId", MySqlConnector.MySqlDbType.Int32).Value = CId;
                cmd.Parameters.Add("@CName", MySqlConnector.MySqlDbType.VarChar).Value = CName;
                cmd.Parameters.Add("@CPrice", MySqlConnector.MySqlDbType.Int32).Value = CPrice;
                cmd.Parameters.Add("@CRoom", MySqlConnector.MySqlDbType.VarChar).Value = CRoom;
                cmd.Parameters.Add("@CLrId", MySqlConnector.MySqlDbType.Int32).Value = CLrId;
                cmd.Parameters.Add("@CLrName", MySqlConnector.MySqlDbType.VarChar).Value = CLrName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public void updateCourses(int CId, string CName, decimal CPrice, string CRoom, int CLrId, string CLrName)
        {
            string update = "update Courses set CName=@CName, CPrice=@CPrice, CRoom=@CRoom, CLrId=@CLrId, CLrName=@CLrName where CId=" + CId;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@CId", MySqlConnector.MySqlDbType.Int32).Value = CId;
                cmd.Parameters.Add("@CName", MySqlConnector.MySqlDbType.VarChar).Value = CName;
                cmd.Parameters.Add("@CPrice", MySqlConnector.MySqlDbType.Decimal).Value = CPrice;
                cmd.Parameters.Add("@CRoom", MySqlConnector.MySqlDbType.VarChar).Value = CRoom;
                cmd.Parameters.Add("@CLrId", MySqlConnector.MySqlDbType.Int32).Value = CLrId;
                cmd.Parameters.Add("@CLrName", MySqlConnector.MySqlDbType.VarChar).Value = CLrName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteCourses(int CId)
        {
            string delete = "delete from Courses where CId = @CId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@CId", MySqlConnector.MySqlDbType.Int32).Value = CId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchCourses(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Courses where concat(CId, CName, CPrice, CRoom, CLrId, CLrName)like '%" + search + "%'", koneksi.GetConn());
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
