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
    internal class LecturerController:Model.Connection
    {
        Connection koneksi = new Connection();
        public DataTable selectLecturers()
        {
            DataTable dataLecturers = new DataTable();
            try
            {
                string selectLecturers = "SELECT * FROM Lecturer";
                da = new MySqlConnector.MySqlDataAdapter(selectLecturers, GetConn());
                da.Fill(dataLecturers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataLecturers;
        }

        public void addLecturers(int LrId, string LrName, string LrQual, DateTime LrDOB, string LrGen, int LrSalary, int LrDepId, string LrDepName, byte[] LrPhoto)
        {
            string add = "insert into Lecturer values (@LrId, @LrName, @LrQual, @LrDOB, @LrGen, @LrSalary, @LrDepId, @LrDepName, @LrPhoto)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@LrId", MySqlConnector.MySqlDbType.Int32).Value = LrId;
                cmd.Parameters.Add("@LrName", MySqlConnector.MySqlDbType.VarChar).Value = LrName;
                cmd.Parameters.Add("@LrQual", MySqlConnector.MySqlDbType.VarChar).Value = LrQual;
                cmd.Parameters.Add("@LrDOB", MySqlConnector.MySqlDbType.VarChar).Value = LrDOB;
                cmd.Parameters.Add("@LrGen", MySqlConnector.MySqlDbType.VarChar).Value = LrGen;
                cmd.Parameters.Add("@LrSalary", MySqlConnector.MySqlDbType.Int32).Value = LrSalary;
                cmd.Parameters.Add("@LrDepId", MySqlConnector.MySqlDbType.Int32).Value = LrDepId;
                cmd.Parameters.Add("@LrDepName", MySqlConnector.MySqlDbType.VarChar).Value = LrDepName;
                cmd.Parameters.Add("@LrPhoto", MySqlConnector.MySqlDbType.Blob).Value = LrPhoto;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Data Failed" + ex.Message);
            }

        }

        public void updateLecturers(int LrId, string LrName, string LrQual, DateTime LrDOB, string LrGen, decimal LrSalary, int LrDepId, string LrDepName, byte[] LrPhoto)
        {
            string update = "update Lecturer set LrName=@LrName, LrQual=@LrQual, LrDOB=@LrDOB, LrGen=@LrGen, LrSalary=@LrSalary, LrDepId=@LrDepId, LrDepName=@LrDepName, LrPhoto=@LrPhoto where LrId=" + LrId;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(update, GetConn());
                cmd.Parameters.Add("@LrId", MySqlConnector.MySqlDbType.Int32).Value = LrId;
                cmd.Parameters.Add("@LrName", MySqlConnector.MySqlDbType.VarChar).Value = LrName;
                cmd.Parameters.Add("@LrQual", MySqlConnector.MySqlDbType.VarChar).Value = LrQual;
                cmd.Parameters.Add("@LrDOB", MySqlConnector.MySqlDbType.VarChar).Value = LrDOB;
                cmd.Parameters.Add("@LrGen", MySqlConnector.MySqlDbType.VarChar).Value = LrGen;
                cmd.Parameters.Add("@LrSalary", MySqlConnector.MySqlDbType.Decimal).Value = LrSalary;
                cmd.Parameters.Add("@LrDepId", MySqlConnector.MySqlDbType.Int32).Value = LrDepId;
                cmd.Parameters.Add("@LrDepName", MySqlConnector.MySqlDbType.VarChar).Value = LrDepName;
                cmd.Parameters.Add("@LrPhoto", MySqlConnector.MySqlDbType.Blob).Value = LrPhoto;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update data Failed" + ex.Message);
            }

        }

        public void deleteLecturers(int LrId)
        {
            string delete = "delete from Lecturer where LrId = @LrId";

            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@LrId", MySqlConnector.MySqlDbType.Int32).Value = LrId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete data Failed" + ex.Message);
            }

        }

        public DataTable searchLecturers(string search)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = new MySqlCommand
                    ("select * from Lecturer where concat(LrId, LrName, LrQual, LrDOB, LrGen, LrSalary, LrDepId, LrDepName)like '%" + search + "%'", koneksi.GetConn());
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
