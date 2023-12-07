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
    internal class DepartmentController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectDepartment()
        {
            DataTable dataDepartment = new DataTable();
            try
            {
                string selectDepartment = "SELECT * FROM Department";
                da = new MySqlConnector.MySqlDataAdapter(selectDepartment, GetConn());
                da.Fill(dataDepartment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataDepartment;
        }

        public void addDepartment(string DepId, string DepName, string DepNmDekan, string DepDescription)
        {
            string add = "insert into Department values (@DepId, @DepName, @DepNmDekan, @DepDescription)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@DepId", MySqlConnector.MySqlDbType.VarChar).Value = DepId;
                cmd.Parameters.Add("@DepName", MySqlConnector.MySqlDbType.VarChar).Value = DepName;
                cmd.Parameters.Add("@DepNmDekan", MySqlConnector.MySqlDbType.VarChar).Value = DepNmDekan;
                cmd.Parameters.Add("@DepDescription", MySqlConnector.MySqlDbType.VarChar).Value = DepDescription;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public void updateDepartment(string DepId, string DepName, string DepNmDekan, string DepDescription)
        {
            string update = "update Department set DepName=@DepName, DepNmDekan=@DepNmDekan, DepDescription=@DepDescription where DepId=@DepId";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@DepId", MySqlConnector.MySqlDbType.VarChar).Value = DepId;
                cmd.Parameters.Add("@DepName", MySqlConnector.MySqlDbType.VarChar).Value = DepName;
                cmd.Parameters.Add("@DepNmDekan", MySqlConnector.MySqlDbType.VarChar).Value = DepNmDekan;
                cmd.Parameters.Add("@DepDescription", MySqlConnector.MySqlDbType.VarChar).Value = DepDescription;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteDepartment(string DepId)
        {
            string delete = "delete from Department where DepId = @DepId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@DepId", MySqlConnector.MySqlDbType.VarChar).Value = DepId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchDepartment(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Department where concat(DepId, DepName, DepNmDekan, DepDescription)like '%" + search + "%'", koneksi.GetConn());
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
