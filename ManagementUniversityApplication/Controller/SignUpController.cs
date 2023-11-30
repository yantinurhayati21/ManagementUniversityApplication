using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementUniversityApplication.Controller
{
    internal class SignUpController:Model.Connection
    {
        public void AddAccount(string user, string contact, string pass, string confirm)
        {
            string add = "insert into Admin values (" + " @username, @contact, @pass, @confirm)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@username", MySqlConnector.MySqlDbType.VarChar).Value = user;
                cmd.Parameters.Add("@contact", MySqlConnector.MySqlDbType.VarChar).Value = contact;
                cmd.Parameters.Add("@pass", MySqlConnector.MySqlDbType.VarChar).Value = pass;
                cmd.Parameters.Add("@confirm", MySqlConnector.MySqlDbType.VarChar).Value = confirm;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Account Failed!! " + ex.Message);
            }
        }
    }
}
