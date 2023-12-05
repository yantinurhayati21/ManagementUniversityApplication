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
    public partial class FormLearning : Form
    {
        private Connection conn;
        private LearningController learningController;
        private ValidationController val;
        public FormLearning()
        {
            conn = new Connection();
            learningController = new LearningController();
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
            FormStudents student = new FormStudents();
            student.Show();
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
            FormLecturer lecturer = new FormLecturer();
            lecturer.Show();
            this.Close();
        }

        private void pictureBoxCourses_Click(object sender, EventArgs e)
        {
            FormCourses courses = new FormCourses();
            courses.Show();
            this.Close();
        }

        private void pictureBoxFees_Click(object sender, EventArgs e)
        {
            FormFees fees = new FormFees();
            fees.Show();
            this.Close();
        }

        private void pictureBoxSalary_Click(object sender, EventArgs e)
        {
            FormSalary salary = new FormSalary();
            salary.Show();
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

        private void FormLearning_Load(object sender, EventArgs e)
        {
            refresh();
            guna2TextBoxLearnID.MaxLength = 5;
            guna2ComboBoxStuID.MaxLength = 5;
            guna2TextBoxStuName.MaxLength = 20;
            guna2ComboBoxCID.MaxLength = 5;
            guna2TextBoxCName.MaxLength = 20;
            guna2TextBoxCRoom.MaxLength = 3;
            guna2ComboBoxLecID.MaxLength = 5;
            guna2TextBoxLecturerName.MaxLength = 20;
            guna2TextBoxDuration.MaxLength = 5;
            StudentId();
            CourseId();
            LectureId();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewLearning.DataSource = learningController.selectLearning();
            dataGridViewLearning.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            StudentId();
            CourseId();
            LectureId();
        }

        private void StudentId()
        {
            DataTable data = new DataTable();
            string studentId = "SELECT StId FROM Students";
            conn.cmd = new MySqlCommand(studentId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("StId", typeof(Int32));
            data.Load(conn.dr);
            guna2ComboBoxStuID.ValueMember = "StId";
            guna2ComboBoxStuID.DataSource = data;
        }

        private void StudName()
        {
            string pelname = "SELECT * FROM Students WHERE StId = " + guna2ComboBoxStuID.SelectedValue;
            conn.cmd = new MySqlCommand(pelname, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            foreach (DataRow dr in data.Rows)
            {
                guna2TextBoxStuName.Text = dr["StName"].ToString();
            }
        }

        private void CourseId()
        {
            DataTable data = new DataTable();
            string courseId = "SELECT CId FROM Courses";
            conn.cmd = new MySqlCommand(courseId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("CId", typeof(Int32));
            data.Load(conn.dr);
            guna2ComboBoxCID.ValueMember = "CId";
            guna2ComboBoxCID.DataSource = data;
        }

        private void CourseName()
        {
            string pelname = "SELECT * FROM Courses WHERE CId = " + guna2ComboBoxCID.SelectedValue;
            conn.cmd = new MySqlCommand(pelname, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            foreach (DataRow dr in data.Rows)
            {
                guna2TextBoxCName.Text = dr["CName"].ToString();
                guna2TextBoxCRoom.Text = dr["CRoom"].ToString();
            }
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
                guna2TextBoxLecturerName.Text = dr["LrName"].ToString();
            }
        }

        private void guna2ComboBoxStuID_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudName();
        }

        private void guna2ComboBoxCID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CourseName();
        }

        private void guna2ComboBoxLecID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LecName();
        }

        private void dataGridViewLearning_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBoxLearnID.Text = dataGridViewLearning.CurrentRow.Cells[0].Value.ToString();
            guna2ComboBoxStuID.Text = dataGridViewLearning.CurrentRow.Cells[1].Value.ToString();
            guna2TextBoxStuName.Text = dataGridViewLearning.CurrentRow.Cells[2].Value.ToString();
            guna2ComboBoxCID.Text = dataGridViewLearning.CurrentRow.Cells[3].Value.ToString();
            guna2TextBoxCName.Text = dataGridViewLearning.CurrentRow.Cells[4].Value.ToString();
            guna2TextBoxCRoom.Text = dataGridViewLearning.CurrentRow.Cells[5].Value.ToString();
            guna2DateTimePickerClass.Value = (DateTime)dataGridViewLearning.CurrentRow.Cells[6].Value;
            guna2ComboBoxLecID.Text = dataGridViewLearning.CurrentRow.Cells[7].Value.ToString();
            guna2TextBoxLecturerName.Text = dataGridViewLearning.CurrentRow.Cells[8].Value.ToString();
            guna2TextBoxDuration.Text = dataGridViewLearning.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(guna2TextBoxLecturerName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxStuName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxCName.Text) && val.ValidateAlphabetAndNumber(guna2TextBoxCRoom.Text))
            {
                try
                {                  
                    learningController.addLearning(
                        Convert.ToInt32(guna2TextBoxLearnID.Text),
                        Convert.ToInt32(guna2ComboBoxStuID.Text),
                        guna2TextBoxStuName.Text,
                        Convert.ToInt32(guna2ComboBoxCID.Text),
                        guna2TextBoxCName.Text,
                        guna2TextBoxCRoom.Text,
                        guna2DateTimePickerClass.Value,
                        Convert.ToInt32(guna2ComboBoxLecID.Text),
                        guna2TextBoxLecturerName.Text,
                        Convert.ToInt32(guna2TextBoxDuration.Text)
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
            if (val.ValidateOnlyAlphabet(guna2TextBoxLecturerName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxStuName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxCName.Text) && val.ValidateAlphabetAndNumber(guna2TextBoxCRoom.Text))
            {
                try
                {
                    learningController.updateLearning(
                        Convert.ToInt32(guna2TextBoxLearnID.Text),
                        Convert.ToInt32(guna2ComboBoxStuID.Text),
                        guna2TextBoxStuName.Text,
                        Convert.ToInt32(guna2ComboBoxCID.Text),
                        guna2TextBoxCName.Text,
                        guna2TextBoxCRoom.Text,
                        guna2DateTimePickerClass.Value,
                        Convert.ToInt32(guna2ComboBoxLecID.Text),
                        guna2TextBoxLecturerName.Text,
                        Convert.ToInt32(guna2TextBoxDuration.Text)
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
                MessageBox.Show("Empty field", "Add Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int selectedValue = (int)dataGridViewLearning.SelectedRows[0].Cells["LrnId"].Value;
                learningController.deleteLearning(selectedValue);
                refresh();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            guna2TextBoxLearnID.Clear();
            guna2TextBoxStuName.Clear();
            guna2TextBoxCName.Clear();
            guna2TextBoxCRoom.Clear();
            guna2DateTimePickerClass.Value = DateTime.Now;
            guna2TextBoxLecturerName.Clear();
            guna2TextBoxDuration.Clear();
            guna2ComboBoxCID.SelectedIndex = 0;
            guna2ComboBoxLecID.SelectedIndex = 0;
            guna2ComboBoxStuID.SelectedIndex = 0;
        }

        private void guna2TextBoxLearnID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxStuName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxCName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxLecturerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridViewLearning.DataSource = learningController.searchLearning(guna2TextBoxSearch.Text);
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogLearn.Document = printDocumentLearn;
            printPreviewDialogLearn.ShowDialog();
        }

        private void printDocumentLearn_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string title = "Data Learning";
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
            Bitmap btm = new Bitmap(this.dataGridViewLearning.Width, this.dataGridViewLearning.Height);
            dataGridViewLearning.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewLearning.Width, this.dataGridViewLearning.Height));
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110;
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }
    }
}
