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
                string psw = txtPassword.Text;
                string confirm = txtConfirm.Text;
            try
            {
                if ((txtUsername.Text == "") || (txtPassword.Text == "") || (txtContact.Text == "") || (txtConfirm.Text == ""))
                {
                    MessageBox.Show("Need SignUp data", "Wrong SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }else if (psw == confirm)
                {
                    signUpController.AddAccount(txtUsername.Text,txtContact.Text, txtPassword.Text, txtConfirm.Text);

                    MessageBox.Show("Saved Successfully");
                    FormLogin login = new FormLogin();
                    this.Hide();
                    login.Show();
                    this.Controls.Clear();
                    this.InitializeComponent();
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
            txtUsername.MaxLength = 10;
            txtContact.MaxLength = 13;
            txtPassword.MaxLength = 10;
            txtConfirm.MaxLength = 10;
            pictureBoxEng.Visible = true;
            pictureBoxInd.Visible = false;
        }

        private void btnSignUp_MouseEnter(object sender, EventArgs e)
        {
            btnSignUp.ForeColor = Color.Black;
        }

        private void btnSignUp_MouseLeave(object sender, EventArgs e)
        {
            btnSignUp.ForeColor = Color.Lime;
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2ToggleSwitchMode_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitchMode.Checked == true)
            {
                lblSignUp.ForeColor = Color.FromArgb(34, 36, 49);
                this.BackColor = Color.FromArgb(34, 36, 49);
                guna2PanelRig.BackColor = Color.Azure;
                guna2PanelRight.BackColor = Color.Azure;
                lblUsername.ForeColor = Color.FromArgb(34, 36, 49);
                labelPsw.ForeColor = Color.FromArgb(34, 36, 49);
                labelContact.ForeColor = Color.FromArgb(34, 36, 49);
                labelConfirm.ForeColor = Color.FromArgb(34, 36, 49);
                txtUsername.FillColor = Color.Azure;
                txtPassword.FillColor = Color.Azure;
                txtContact.FillColor = Color.Azure;
                txtConfirm.FillColor = Color.Azure;
                guna2PanelLinewhitecnt.BackColor = Color.Black;
                guna2PanelLineWhitePs.BackColor = Color.Black;
                guna2PanelLineWhiteCPsw.BackColor = Color.Black;
                guna2PanelLineWhiteUsr.BackColor = Color.Black;
                txtUsername.ForeColor = Color.Black;
                txtPassword.ForeColor = Color.Black;
                txtContact.ForeColor = Color.Black;
                txtConfirm.ForeColor = Color.Black;
            }
            else
            {
                guna2PanelRig.BackColor = Color.FromArgb(30, 30, 50);
                guna2PanelRight.BackColor = Color.FromArgb(30, 30, 50);
                lblUsername.ForeColor = Color.White;
                labelPsw.ForeColor = Color.White;
                labelContact.ForeColor = Color.White;
                labelConfirm.ForeColor = Color.White;
                txtUsername.FillColor = Color.FromArgb(30, 30, 50);
                txtPassword.FillColor = Color.FromArgb(30, 30, 50);
                txtContact.FillColor = Color.FromArgb(30, 30, 50);
                txtConfirm.FillColor = Color.FromArgb(30, 30, 50);
                lblSignUp.ForeColor = Color.White;
                guna2PanelLinewhitecnt.BackColor = Color.White;
                guna2PanelLineWhitePs.BackColor = Color.White;
                guna2PanelLineWhiteCPsw.ForeColor = Color.White;
                guna2PanelLineWhiteUsr.BackColor = Color.White;
                txtUsername.ForeColor = Color.White;
                txtPassword.ForeColor = Color.White;
                txtContact.ForeColor = Color.White;
                txtConfirm.ForeColor = Color.White;
            }
        }

        private void pictureBoxInd_Click(object sender, EventArgs e)
        {
            if (pictureBoxInd.Visible == true)
            {
                pictureBoxInd.Visible = false;
                pictureBoxEng.Visible = true;
                lblSignUp.Text = "Sign Up";
                lblUsername.Text = "Username";
                labelPsw.Text = "Password";
                labelContact.Text = "Contact";
                labelConfirm.Text = "Confirm Password";
                btnSignUp.Text = "Sign Up";
                labelLightMode.Text = "Lignt Mode";
                linkLabelLogin.Text = "Already have account";
                labelLng.Text = "English";
            }
        }

        private void pictureBoxEng_Click(object sender, EventArgs e)
        {
            if (pictureBoxEng.Visible == true)
            {
                pictureBoxInd.Visible = true;
                pictureBoxEng.Visible = false;
                lblSignUp.Text = "Daftar";
                lblUsername.Text = "Nama Pengguna";
                labelPsw.Text = "Kata Sandi";
                labelContact.Text = "Telepon";
                labelConfirm.Text = "Konfirmasi Kata Sandi";
                btnSignUp.Text = "Daftar";
                labelLightMode.Text = "Mode Terang";
                linkLabelLogin.Text = "Sudah punya akun";
                labelLng.Text = "Indonesia";
            }
        }
    }
}
