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
            txtCoursesId.MaxLength = 5;
            txtCoursesName.MaxLength = 20;
            txtCoursesPrice.MaxLength = 20;
            guna2TextBoxRoom.MaxLength = 3;
            comboBoxLrId.MaxLength = 10;
            txtLecturerName.MaxLength = 5;
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int selectedValue = (int)dataGridViewCourses.SelectedRows[0].Cells["CId"].Value;
                courseController.deleteCourses(selectedValue);
                refresh();
            }     
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCoursesId.Clear();
            txtCoursesName.Clear();
            txtCoursesPrice.Clear();
            guna2TextBoxRoom.Clear();
            txtLecturerName.Clear();
            comboBoxLrId.SelectedIndex = 0;
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

        private void txtCoursesId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtCoursesName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtCoursesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void pictureBoxSearch_Click(object sender, EventArgs e)
        {
            dataGridViewCourses.DataSource = courseController.searchCourses(guna2TextBoxSearch.Text);
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogCourses.Document = printDocumentCourses;
            printPreviewDialogCourses.ShowDialog();
        }

        private void printDocumentCourses_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string title = "Data Courses";
            Font titleFont = new Font("Arial", 32, FontStyle.Bold);
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                float titleX = (e.PageBounds.Width - titleSize.Width) / 2;
                float titleY = 15;
                e.Graphics.DrawString(title, titleFont, brush, titleX, titleY);
            }
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(pen, new Point(50, 90), new Point(e.PageBounds.Width - 50, 90));
            }
            Bitmap btm = new Bitmap(this.dataGridViewCourses.Width, this.dataGridViewCourses.Height);
            dataGridViewCourses.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewCourses.Width, this.dataGridViewCourses.Height));
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110;
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }
    }
}
