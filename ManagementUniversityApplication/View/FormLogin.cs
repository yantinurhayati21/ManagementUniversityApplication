using Guna.UI2.WinForms;
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
                    Dashboard dsb = new Dashboard();
                    this.Hide();
                    dsb.Show();
                }
                else
                {
                    PanelInvalidUser.Visible = true;
                    PanelInvalidPass.Visible = true;
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            pictureBoxEng.Visible = true;
            pictureBoxInd.Visible = false;
            PanelInvalidUser.Visible = false;
            PanelInvalidPass.Visible = false;
            pictureBoxOpen.Visible = false;
            txtPassword.UseSystemPasswordChar = true;
            pictureBoxClose.Visible = true;
            txtUserName.MaxLength = 10;
            txtPassword.MaxLength = 10;
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
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
            FormSignUp signUp = new FormSignUp();
            this.Hide();
            signUp.Show();
        }

        private void guna2ToggleSwitchMode_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitchMode.Checked == true)
            {
                lblLogin.ForeColor = Color.FromArgb(34, 36, 49);
                this.BackColor = Color.FromArgb(34, 36, 49);
                guna2PanelLeft.BackColor = Color.Azure;
                PanelLogin.BackColor = Color.Azure;
                lblUsername.ForeColor = Color.FromArgb(34, 36, 49);
                lblPassword.ForeColor = Color.FromArgb(34, 36, 49);
                txtUserName.FillColor = Color.Azure;
                txtPassword.FillColor = Color.Azure;
                guna2PanelLineWhiteUs.BackColor = Color.Black;
                guna2linewhiteps.BackColor = Color.Black;
                txtUserName.ForeColor = Color.Black;
                txtPassword.ForeColor = Color.Black;
            }
            else
            {
                guna2PanelLeft.BackColor = Color.FromArgb(30, 30, 50);
                PanelLogin.BackColor= Color.FromArgb(30, 30, 50);
                lblUsername.ForeColor = Color.White;
                lblPassword.ForeColor = Color.White;
                txtUserName.FillColor = Color.FromArgb(30, 30, 50);
                txtPassword.FillColor = Color.FromArgb(30, 30, 50);
                lblLogin.ForeColor = Color.White;
                guna2PanelLineWhiteUs.BackColor = Color.White;
                guna2linewhiteps.BackColor = Color.White;
                txtUserName.ForeColor = Color.White;
                txtPassword.ForeColor = Color.White;
            }
        }

        private void pictureBoxEng_Click(object sender, EventArgs e)
        {
            if (pictureBoxEng.Visible == true) 
            {
                pictureBoxInd.Visible = true;
                pictureBoxEng.Visible = false;
                lblLogin.Text = "Masuk";
                lblUsername.Text = "Nama Pengguna";
                lblPassword.Text = "Kata Sandi";
                btnExit.Text = "Keluar";
                btnLogin.Text = "Masuk";
                labelLightMode.Text = "Mode Terang";
                linkLabelSignUp.Text = "Buat akun baru";
                labelLng.Text = "Indonesia";
            }
        }

        private void pictureBoxInd_Click(object sender, EventArgs e)
        {
            if (pictureBoxInd.Visible == true)
            {
                pictureBoxInd.Visible = false;
                pictureBoxEng.Visible = true;
                lblLogin.Text = "Login";
                lblUsername.Text = "Username";
                lblPassword.Text = "Password";
                btnExit.Text = "Exit";
                btnLogin.Text = "Login";
                labelLightMode.Text = "Lignt Mode";
                linkLabelSignUp.Text = "Create new account";
                labelLng.Text = "English";
            }
        }
    }
}
