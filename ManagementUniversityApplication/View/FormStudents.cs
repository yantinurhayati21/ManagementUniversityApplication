using ManagementUniversityApplication.Controller;
using ManagementUniversityApplication.Model;
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
    public partial class FormStudents : Form
    {
        private Connection conn;
        private StudentController studentController;
        private ValidationController val;
        public FormStudents()
        {
            conn = new Connection();
            studentController = new StudentController();
            val = new ValidationController();
            InitializeComponent();
        }
        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewStudent.DataSource = studentController.selectStudents();
            dataGridViewStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
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

        private void pictureBoxSalary_Click(object sender, EventArgs e)
        {
            FormSalary formSalary = new FormSalary();
            formSalary.Show();
            this.Close();
        }

        private void FormStudents_Load(object sender, EventArgs e)
        {
            refresh();
            guna2TextBoxStudentId.MaxLength = 5;
            guna2TextBoxStudentNim.MaxLength = 12;
            guna2TextBoxName.MaxLength = 20;
            guna2TextBoxSemester.MaxLength = 1;
            guna2ComboBoxDepID.MaxLength = 5;
            guna2TextBoxDepName.MaxLength = 20;
        }

        private void dataGridViewStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBoxStudentId.Text = dataGridViewStudent.CurrentRow.Cells[0].Value.ToString();
            guna2TextBoxStudentNim.Text = dataGridViewStudent.CurrentRow.Cells[1].Value.ToString();
            guna2TextBoxName.Text = dataGridViewStudent.CurrentRow.Cells[2].Value.ToString();
            guna2DateTimePickerDOB.Value = (DateTime)dataGridViewStudent.CurrentRow.Cells[3].Value;
            string gender = dataGridViewStudent.CurrentRow.Cells[4].Value.ToString();
            if (gender == "Male")
            {
                guna2RadioButtonGnd.Checked = true;
            }
            else if (gender == "Female")
            {
                guna2RadioButton2Gnd.Checked = true;
            }
            guna2TextBoxSemester.Text = dataGridViewStudent.CurrentRow.Cells[5].Value.ToString();
            guna2ComboBoxDepID.Text = dataGridViewStudent.CurrentRow.Cells[6].Value.ToString();
            guna2TextBoxDepName.Text = dataGridViewStudent.CurrentRow.Cells[7].Value.ToString();
            if (dataGridViewStudent.CurrentRow.Cells[8].Value is byte[] img)
            {
                try
                {
                    MemoryStream imageStream = new MemoryStream(img);
                    guna2PictureBoxPhoto.Image = Image.FromStream(imageStream);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error loading image: " + ex.Message);
                    guna2PictureBoxPhoto.Image = null;
                }

            }
        }

        private void guna2RadioButtonGnd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(guna2TextBoxName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxDepName .Text))
            {
                try
                {
                    MemoryStream memori = new MemoryStream();
                    guna2PictureBoxPhoto.Image.Save(memori, guna2PictureBoxPhoto.Image.RawFormat);
                    byte[] img = memori.ToArray();
                    studentController.addStudents(Convert.ToInt32(guna2TextBoxStudentId.Text), guna2TextBoxStudentNim.Text, guna2TextBoxName.Text, guna2DateTimePickerDOB.Value, guna2RadioButton2Gnd.Text, Convert.ToInt32(guna2TextBoxSemester.Text), Convert.ToInt32(guna2ComboBoxDepID.Text), guna2TextBoxDepName.Text, img);
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
    }
}
