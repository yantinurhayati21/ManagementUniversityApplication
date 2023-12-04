using ManagementUniversityApplication.Model;
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

namespace ManagementUniversityApplication.View
{
    public partial class Dashboard : Form
    {
        private Connection conn;
        public Dashboard()
        {
            conn = new Connection();
            InitializeComponent();
            SumFinances();
            SumSalaries();
            SumStudents();
            SumDepartement();
            SumLecturer();
            SumCourses();
        }

        private void pictureBoxSudents_Click(object sender, EventArgs e)
        {
            FormStudents student = new FormStudents();
            student.Show();
            this.Hide();
        }

        private void pictureBoxDepartment_Click(object sender, EventArgs e)
        {
            FormDepartment department = new FormDepartment();
            department.Show();
            this.Hide();
        }

        private void pictureBoxLecturer_Click(object sender, EventArgs e)
        {
            FormLecturer formLecturer = new FormLecturer();
            formLecturer.Show();
            this.Hide();
        }

        private void pictureBoxCourses_Click(object sender, EventArgs e)
        {
            FormCourses formCourses = new FormCourses();
            formCourses.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FormLearning formLearning = new FormLearning();
            formLearning.Show();
            this.Hide();
        }

        private void pictureBoxFees_Click(object sender, EventArgs e)
        {
            FormFees formFees = new FormFees();
            formFees.Show();
            this.Hide();
        }

        private void pictureBoxSalary_Click(object sender, EventArgs e)
        {
            FormSalary formSalary = new FormSalary();
            formSalary.Show();
            this.Hide();
        }

        private void pictureBoxLogout_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void SumFinances()
        {
            string sum = "SELECT SUM(FAmount) FROM Fees";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);           
            labelFinances.Text = "Rp " + data.Rows[0][0].ToString();
        }

        private void SumSalaries()
        {
            string sum = "SELECT SUM(SLrSalary) FROM Salary";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            labelSalaries.Text = "Rp " + data.Rows[0][0].ToString();
        }

        private void SumStudents()
        {
            string sum = "SELECT Count(*) FROM Students";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            labelStud.Text = data.Rows[0][0].ToString();
        }

        private void SumLecturer()
        {
            string sum = "SELECT Count(*) FROM Lecturer";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            labelLectur.Text = data.Rows[0][0].ToString();
        }

        private void SumDepartement()
        {
            string sum = "SELECT Count(*) FROM Department";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            labelDepart.Text = data.Rows[0][0].ToString();
        }

        private void SumCourses()
        {
            string sum = "SELECT Count(*) FROM Courses";
            conn.cmd = new MySqlCommand(sum, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            labelCours.Text = data.Rows[0][0].ToString();
        }

    }
}
