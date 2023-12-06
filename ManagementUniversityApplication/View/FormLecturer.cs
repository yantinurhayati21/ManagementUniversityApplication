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
    public partial class FormLecturer : Form
    {
        private Connection conn;
        private LecturerController lecturerController;
        private ValidationController val;
        public FormLecturer()
        {
            conn = new Connection();
            lecturerController = new LecturerController();
            val = new ValidationController();
            InitializeComponent();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewLecturer.DataSource = lecturerController.selectLecturers();
            dataGridViewLecturer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2TextBoxLecturerId.MaxLength = 5;
            guna2TextBoxLecturerName.MaxLength = 20;
            guna2TextBoxLecturerQual.MaxLength = 20;
            guna2TextBoxSalary.MaxLength = 10;
            guna2ComboBoxDepID.MaxLength = 5;
            guna2TextBoxDepName.MaxLength = 20;
            DepartmentId();
        }

        private void DepartmentId()
        {
            DataTable data = new DataTable();
            string departmentId = "SELECT DepId FROM Department";
            conn.cmd = new MySqlCommand(departmentId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("DepId", typeof(Int32));
            data.Load(conn.dr);
            guna2ComboBoxDepID.ValueMember = "DepId";
            guna2ComboBoxDepID.DataSource = data;
        }

        private void DepName()
        {
            string pelname = "SELECT * FROM Department WHERE DepId = " + guna2ComboBoxDepID.SelectedValue;
            conn.cmd = new MySqlCommand(pelname, conn.GetConn());
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(data);
            foreach (DataRow dr in data.Rows)
            {
                guna2TextBoxDepName.Text = dr["DepName"].ToString();
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

        private void pictureBoxCourses_Click(object sender, EventArgs e)
        {
            FormCourses courses = new FormCourses();
            courses.Show();
            this.Close();
        }

        private void pictureBoxLearning_Click(object sender, EventArgs e)
        {
            FormLearning learning = new FormLearning();
            learning.Show();
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

        private void dataGridViewLecturer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBoxLecturerId.Text = dataGridViewLecturer.CurrentRow.Cells[0].Value.ToString();
            guna2TextBoxLecturerName.Text = dataGridViewLecturer.CurrentRow.Cells[1].Value.ToString();
            guna2TextBoxLecturerQual.Text = dataGridViewLecturer.CurrentRow.Cells[2].Value.ToString();
            guna2DateTimePickerDOB.Value = (DateTime)dataGridViewLecturer.CurrentRow.Cells[3].Value;
            string gender = dataGridViewLecturer.CurrentRow.Cells[4].Value.ToString();
            if (gender == "Male")
            {
                guna2RadioButtonGnd.Checked = true;
            }
            else if (gender == "Female")
            {
                guna2RadioButton2Gnd.Checked = true;
            }
            guna2TextBoxSalary.Text = dataGridViewLecturer.CurrentRow.Cells[5].Value.ToString();
            guna2ComboBoxDepID.Text = dataGridViewLecturer.CurrentRow.Cells[6].Value.ToString();
            guna2TextBoxDepName.Text = dataGridViewLecturer.CurrentRow.Cells[7].Value.ToString();
            if (dataGridViewLecturer.CurrentRow.Cells[8].Value is byte[] img)
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

        private void guna2ComboBoxDepID_SelectedIndexChanged(object sender, EventArgs e)
        { 
            DepName();
        }

        private void guna2ButtonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif;";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                guna2PictureBoxPhoto.Image = Image.FromFile(opf.FileName);
            }
        }

        bool verify()
        {
            if (guna2PictureBoxPhoto.Image == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int bornYear = guna2DateTimePickerDOB.Value.Year;
            int thisYear = DateTime.Now.Year;
            if ((thisYear - bornYear) <= 26 || (thisYear - bornYear) >= 50)
            {
                MessageBox.Show("Age must be between 26 to 50", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!guna2RadioButtonGnd.Checked && !guna2RadioButton2Gnd.Checked)
            {
                MessageBox.Show("Please select a gender", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify() && val.ValidateOnlyAlphabet(guna2TextBoxLecturerName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxDepName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxLecturerQual.Text))
            {
                try
                {
                    string gender = guna2RadioButton2Gnd.Checked ? "Male" : "Female";
                    MemoryStream memori = new MemoryStream();
                    guna2PictureBoxPhoto.Image.Save(memori, guna2PictureBoxPhoto.Image.RawFormat);
                    byte[] img = memori.ToArray();
                    lecturerController.addLecturers(
                        Convert.ToInt32(guna2TextBoxLecturerId.Text),
                        guna2TextBoxLecturerName.Text,
                        guna2TextBoxLecturerQual.Text,
                        guna2DateTimePickerDOB.Value,
                        gender,
                        Convert.ToInt32(guna2TextBoxSalary.Text),
                        Convert.ToInt32(guna2ComboBoxDepID.Text),
                        guna2TextBoxDepName.Text,
                        img
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
            int bornYear = guna2DateTimePickerDOB.Value.Year;
            int thisYear = DateTime.Now.Year;
            if ((thisYear - bornYear) <= 26 || (thisYear - bornYear) >= 50)
            {
                MessageBox.Show("Age must be between 26 to 50", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!guna2RadioButtonGnd.Checked && !guna2RadioButton2Gnd.Checked)
            {
                MessageBox.Show("Please select a gender", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify() && val.ValidateOnlyAlphabet(guna2TextBoxLecturerName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxDepName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxLecturerQual.Text))
            {
                try
                {
                    string gender = guna2RadioButton2Gnd.Checked ? "Male" : "Female";
                    MemoryStream memori = new MemoryStream();
                    guna2PictureBoxPhoto.Image.Save(memori, guna2PictureBoxPhoto.Image.RawFormat);
                    byte[] img = memori.ToArray();
                    lecturerController.updateLecturers(
                        Convert.ToInt32(guna2TextBoxLecturerId.Text),
                        guna2TextBoxLecturerName.Text,
                        guna2TextBoxLecturerQual.Text,
                        guna2DateTimePickerDOB.Value,
                        gender,
                        Convert.ToDecimal(guna2TextBoxSalary.Text),
                        Convert.ToInt32(guna2ComboBoxDepID.Text),
                        guna2TextBoxDepName.Text,
                        img
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
                int selectedValue = (int)dataGridViewLecturer.SelectedRows[0].Cells["LrId"].Value;
                lecturerController.deleteLecturers(selectedValue);
                refresh();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            guna2TextBoxLecturerId.Clear();
            guna2TextBoxLecturerName.Clear();
            guna2TextBoxLecturerQual.Clear();
            guna2DateTimePickerDOB.Value = DateTime.Now;
            guna2RadioButton2Gnd.Checked = false;
            guna2RadioButtonGnd.Checked = false;
            guna2ComboBoxDepID.SelectedIndex = 0;
            guna2TextBoxSalary.Clear();
            guna2TextBoxDepName.Clear();
            guna2PictureBoxPhoto.Image = null;
        }

        private void guna2TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridViewLecturer.DataSource = lecturerController.searchLecturers(guna2TextBoxSearch.Text);
        }

        private void FormLecturer_Load(object sender, EventArgs e)
        {
            refresh();
            DepartmentId();
        }

        private void guna2TextBoxLecturerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxLecturerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxLecturerQual_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void guna2TextBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogLec.Document = printDocumentLec;
            printPreviewDialogLec.ShowDialog();
        }

        private void printDocumentLec_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string title = "Data Lecturers";
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
            Bitmap btm = new Bitmap(this.dataGridViewLecturer.Width, this.dataGridViewLecturer.Height);
            dataGridViewLecturer.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewLecturer.Width, this.dataGridViewLecturer.Height));
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110;
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }
    }
}
