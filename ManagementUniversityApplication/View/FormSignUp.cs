using ManagementUniversityApplication.Controller;
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

namespace ManagementUniversityApplication.View
{
    public partial class FormSignUp : Form
    {
        SignUpController signUpController=new SignUpController();
        public FormSignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtUsername.Text == "") || (txtPassword.Text == "") || (txtContact.Text == "") || (txtConfirm.Text == ""))
                {
                    MessageBox.Show("Need SignUp data", "Wrong SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                string psw = txtPassword.Text;
                string confirm = txtConfirm.Text;

                if (psw == confirm)
                {
                    signUpController.AddAccount(txtUsername.Text,txtContact.Text, txtPassword.Text, txtConfirm.Text);

                    MessageBox.Show("Saved Successfully");

                    this.Controls.Clear();
                    this.InitializeComponent();
                    txtUsername.Focus();
                }
                else
                {
                    MessageBox.Show("Password and confirm password do not match");
                    txtConfirm.Clear();
                    txtConfirm.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void FormSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
