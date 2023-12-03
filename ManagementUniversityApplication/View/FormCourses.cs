using ManagementUniversityApplication.Controller;
using ManagementUniversityApplication.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementUniversityApplication.View
{
    public partial class FormCourses : Form
    {
        private Connection conn;
        private CoursesController courseController;
        private ValidationController val;
        public FormCourses()
        {
            conn = new Connection();
            courseController = new CoursesController();
            val = new ValidationController();
            InitializeComponent();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewCourses.DataSource = courseController.selectCourses();
            dataGridViewCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LecturerId();
        }

        private void LecturerId()
        {
            DataTable data = new DataTable();
            string lecturerId = "SELECT LrId FROM Lecturer";
            conn.cmd = new MySqlCommand(lecturerId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("LrId", typeof(Int32));
            data.Load(conn.dr);
            comboBoxLrId.ValueMember = "LrId";
            comboBoxLrId.DataSource = data;
        }

        private void LecName()
        {
            string pelname = "SELECT * FROM Lecturer WHERE LrId = " + comboBoxLrId.SelectedValue;
            conn.cmd = new MySqlCommand(pelname, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            foreach (DataRow dr in data.Rows)
            {
                txtLecturerName.Text = dr["LrName"].ToString();
            }
        }

        private void comboBoxLrId_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void pictureBoxSalary_Click(object sender, EventArgs e)
        {
            FormSalary formSalary = new FormSalary();
            formSalary.Show();
            this.Close();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.ForeColor = Color.White;
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            btnUpdate.ForeColor = Color.White;
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.ForeColor = Color.White;
        }

        private void btnClear_MouseEnter(object sender, EventArgs e)
        {
            btnClear.ForeColor = Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.ForeColor = Color.Fuchsia;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnUpdate.ForeColor = Color.Fuchsia;
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.ForeColor = Color.Fuchsia;
        }

        private void btnClear_MouseLeave(object sender, EventArgs e)
        {
            btnClear.ForeColor = Color.Fuchsia;
        }

        private void comboBoxLrId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LecName();
        }

        private void FormCourses_Load(object sender, EventArgs e)
        {
            refresh();
            txtCoursesId.MaxLength = 5;
            txtCoursesName.MaxLength = 20;
            txtCoursesPrice.MaxLength = 20;
            comboBoxLrId.MaxLength = 10;
            txtLecturerName.MaxLength = 5;
            LecturerId();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(txtLecturerName.Text) && val.ValidateOnlyAlphabet(txtCoursesName.Text) && val.ValidateAlphabetAndNumber(guna2TextBoxRoom.Text))
            {
                try
                {
                    courseController.addCourses(Convert.ToInt32(txtCoursesId.Text),
                        txtCoursesName.Text,                    
                        Convert.ToInt32(txtCoursesPrice.Text),
                        guna2TextBoxRoom.Text,
                        Convert.ToInt32(comboBoxLrId.Text),
                        txtLecturerName.Text
                    );
                    MessageBox.Show("Saved Succesfully");
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Add Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(txtLecturerName.Text) && val.ValidateOnlyAlphabet(txtCoursesName.Text))
            {
                try
                {
                    courseController.updateCourses(Convert.ToInt32(txtCoursesId.Text),
                        txtCoursesName.Text,
                        Convert.ToDecimal(txtCoursesPrice.Text),
                        guna2TextBoxRoom.Text,
                        Convert.ToInt32(comboBoxLrId.Text),
                        txtLecturerName.Text
                    );
                    MessageBox.Show("Update Succesfully");
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Update Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedValue = (int)dataGridViewCourses.SelectedRows[0].Cells["CId"].Value;
            courseController.deleteCourses(selectedValue);
            refresh();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCoursesId.Clear();
            txtCoursesName.Clear();
            txtCoursesPrice.Clear();
            guna2TextBoxRoom.Clear();
            txtLecturerName.Clear();          
        }

        private void dataGridViewCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCoursesId.Text = dataGridViewCourses.CurrentRow.Cells[0].Value.ToString();
            txtCoursesName.Text = dataGridViewCourses.CurrentRow.Cells[1].Value.ToString();
            txtCoursesPrice.Text = dataGridViewCourses.CurrentRow.Cells[2].Value.ToString();
            guna2TextBoxRoom.Text = dataGridViewCourses.CurrentRow.Cells[3].Value.ToString();
            comboBoxLrId.Text = dataGridViewCourses.CurrentRow.Cells[4].Value.ToString();
            txtLecturerName.Text = dataGridViewCourses.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
