using ManagementUniversityApplication.Controller;
using ManagementUniversityApplication.View;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManagementUniversityApplication
{
    public partial class FormLogin : Form
    {
        GetDataController getDataController;
        public FormLogin()
        {
            getDataController = new GetDataController();
            InitializeComponent();
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.ForeColor = Color.Lime;
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.ForeColor= Color.Black;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if ((txtUserName.Text == "") || (txtPassword.Text == ""))
            {
                MessageBox.Show("Need login data", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string name = txtUserName.Text;
                string pass = txtPassword.Text;
                DataTable table = getDataController.getList(new MySqlCommand
                    ("select * from Admin where username = '" + name + "' and pass='" + pass + "'"));

                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("Login Success");
                    Dashboard dsb = new Dashboard();
                    this.Hide();
                    dsb.Show();
                }
                else
                {
                    PanelInvalidUser.Visible = true;
                    PanelInvalidPass.Visible = true;
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            PanelInvalidUser.Visible = false;
            PanelInvalidPass.Visible = false;
            pictureBoxOpen.Visible = false;
            pictureBoxClose.Visible = true;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if(txtUserName.Text=="Enter Username")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.White;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Enter Username";
                txtUserName.ForeColor = Color.Gray;
                
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.White;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;

            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            PanelInvalidUser.Visible = false;
            PanelInvalidPass.Visible = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            PanelInvalidUser.Visible = false;
            PanelInvalidPass.Visible = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            pictureBoxOpen.Visible = true;
            pictureBoxClose.Visible = false;
            txtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBoxOpen_Click(object sender, EventArgs e)
        {
            pictureBoxOpen.Visible = false;
            pictureBoxClose.Visible = true;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.ForeColor = Color.Black;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.ForeColor = Color.Crimson;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormSignUp signUp = new FormSignUp();
            this.Hide();
            signUp.Show();
        }
    }
}
