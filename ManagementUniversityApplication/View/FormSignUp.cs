using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementUniversityApplication.View
{
    public partial class FormSignUp : Form
    {
        public FormSignUp()
        {
            InitializeComponent();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Enter Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.White;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Enter Username";
                txtUsername.ForeColor = Color.Gray;

            }
        }

        private void txtContact_Enter(object sender, EventArgs e)
        {
            if (txtContact.Text == "Enter Contact")
            {
                txtContact.Text = "";
                txtContact.ForeColor = Color.White;
            }
        }

        private void txtContact_Leave(object sender, EventArgs e)
        {
            if (txtContact.Text == "")
            {
                txtContact.Text = "Enter Contact";
                txtContact.ForeColor = Color.Gray;

            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.White;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Enter Password";
                txtPassword.ForeColor = Color.Gray;

            }
        }

        private void txtConfirm_Enter(object sender, EventArgs e)
        {
            if (txtConfirm.Text == "Enter Confirm Password")
            {
                txtConfirm.Text = "";
                txtConfirm.ForeColor = Color.White;
            }
        }

        private void txtConfirm_Leave(object sender, EventArgs e)
        {
            if (txtConfirm.Text == "")
            {
                txtConfirm.Text = "Enter Confirm Password";
                txtConfirm.ForeColor = Color.Gray;

            }
        }
    }
}
