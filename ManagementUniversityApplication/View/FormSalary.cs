using ManagementUniversityApplication.Controller;
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
    public partial class FormSalary : Form
    {
        private Connection conn;
        private SalaryController salaryController;
        private ValidationController val;
        public FormSalary()
        {
            conn = new Connection();
            salaryController = new SalaryController();
            val = new ValidationController();
            InitializeComponent();
        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void pictureBoxStudent_Click(object sender, EventArgs e)
        {
            FormStudents students = new FormStudents();
            students.Show(); 
            this.Close();
        }

        private void pictureBoxDepartment_Click(object sender, EventArgs e)
        {
            FormDepartment department = new FormDepartment();
            department.Show(); 
            this.Close();
        }

        private void pictureBoxLecturer_Click(object sender, EventArgs e)
        {
            FormLecturer formLecturer = new FormLecturer();
            formLecturer.Show();
            this.Close();
        }

        private void pictureBoxCourses_Click(object sender, EventArgs e)
        {
            FormCourses formCourses = new FormCourses();
            formCourses.Show();
            this.Close();
        }

        private void pictureBoxLearning_Click(object sender, EventArgs e)
        {
            FormLearning formLearning = new FormLearning();
            formLearning.Show();
            this.Close();
        }

        private void pictureBoxFees_Click(object sender, EventArgs e)
        {
            FormFees formFees = new FormFees();
            formFees.Show();
            this.Close();
        }

        private void btnPay_MouseEnter(object sender, EventArgs e)
        {
            btnPay.ForeColor = Color.White;
        }

        private void btnPay_MouseLeave(object sender, EventArgs e)
        {
            btnPay.ForeColor = Color.Fuchsia;
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.ForeColor = Color.Fuchsia;
        }

        private void dataGridViewSalary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSalaryId.Text = dataGridViewSalary.CurrentRow.Cells[0].Value.ToString();
            guna2ComboBoxLecID.Text = dataGridViewSalary.CurrentRow.Cells[1].Value.ToString();
            txtLectureName.Text = dataGridViewSalary.CurrentRow.Cells[2].Value.ToString();
            txtLectureSalary.Text = dataGridViewSalary.CurrentRow.Cells[3].Value.ToString();
            txtPeriodSalary.Text = dataGridViewSalary.CurrentRow.Cells[4].Value.ToString();
            guna2DateTimePickerPayDate.Value = (DateTime)dataGridViewSalary.CurrentRow.Cells[5].Value;
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewSalary.DataSource = salaryController.selectSalary();
            dataGridViewSalary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LectureId();
        }

        private void LectureId()
        {
            DataTable data = new DataTable();
            string lectureId = "SELECT LrId FROM Lecturer";
            conn.cmd = new MySqlCommand(lectureId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("LrId", typeof(Int32));
            data.Load(conn.dr);
            guna2ComboBoxLecID.ValueMember = "LrId";
            guna2ComboBoxLecID.DataSource = data;
        }

        private void LecName()
        {
            string pelname = "SELECT * FROM Lecturer WHERE LrId = " + guna2ComboBoxLecID.SelectedValue;
            conn.cmd = new MySqlCommand(pelname, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            foreach (DataRow dr in data.Rows)
            {
                txtLectureName.Text = dr["LrName"].ToString();
                txtLectureSalary.Text = dr["LrSalary"].ToString();
            }
        }

        private void FormSalary_Load(object sender, EventArgs e)
        {
            refresh();
            txtSalaryId.MaxLength = 5;
            guna2ComboBoxLecID.MaxLength = 5;
            txtLectureName.MaxLength = 20;
            txtLectureSalary.MaxLength = 5;
            txtPeriodSalary.MaxLength = 20;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(txtLectureName.Text))
            {
                try
                {
                    salaryController.addSalary(
                        Convert.ToInt32(txtSalaryId.Text),
                        Convert.ToInt32(guna2ComboBoxLecID.Text),
                        txtLectureName.Text,
                        Convert.ToInt32(txtLectureSalary.Text),
                        Convert.ToInt32(txtPeriodSalary.Text),
                        guna2DateTimePickerPayDate.Value
                    );
                    MessageBox.Show("Pay Succesfully");
                    refresh();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Pay Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSalaryId.Clear();
            txtLectureName.Clear();
            txtLectureSalary.Clear();
            txtPeriodSalary.Clear();
            guna2DateTimePickerPayDate.Value = DateTime.Now;
        }

        private void guna2ComboBoxLecID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LecName();
        }
    }
}
