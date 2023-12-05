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
            guna2TextBoxStudentId.MaxLength = 5;
            guna2TextBoxStudentNim.MaxLength = 11;
            guna2TextBoxName.MaxLength = 20;
            guna2TextBoxSemester.MaxLength = 1;
            guna2ComboBoxDepID.MaxLength = 5;
            guna2TextBoxDepName.MaxLength = 20;
            DepartmentId();
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
            DepartmentId();
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
            if ((thisYear - bornYear) <= 17 || (thisYear - bornYear) >= 25)
            {
                MessageBox.Show("Age must be between 17 to 25", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(guna2TextBoxSemester.Text, out int semester) || semester < 1 || semester > 8)
            {
                MessageBox.Show("Semester can only be between 1-8", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!guna2RadioButtonGnd.Checked && !guna2RadioButton2Gnd.Checked)
            {
                MessageBox.Show("Please select a gender", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify() && val.ValidateOnlyAlphabet(guna2TextBoxName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxDepName.Text))
            {
                try
                {
                    string gender = guna2RadioButton2Gnd.Checked ? "Male" : "Female";
                    MemoryStream memory = new MemoryStream();
                    guna2PictureBoxPhoto.Image.Save(memory, guna2PictureBoxPhoto.Image.RawFormat);
                    byte[] img = memory.ToArray();
                    studentController.addStudents(
                        Convert.ToInt32(guna2TextBoxStudentId.Text),
                        guna2TextBoxStudentNim.Text,
                        guna2TextBoxName.Text,
                        guna2DateTimePickerDOB.Value,
                        gender,
                        Convert.ToInt32(guna2TextBoxSemester.Text),
                        Convert.ToInt32(guna2ComboBoxDepID.Text),
                        guna2TextBoxDepName.Text,
                        img
                    );
                    MessageBox.Show("Saved Successfully");
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty field", "Add Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2ComboBoxDepID_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            guna2TextBoxStudentId.Clear();
            guna2TextBoxStudentNim.Clear();
            guna2TextBoxName.Clear();
            guna2DateTimePickerDOB.Value=DateTime.Now;
            guna2RadioButton2Gnd.Checked=false;
            guna2RadioButtonGnd.Checked=false;
            guna2ComboBoxDepID.SelectedIndex = 0;
            guna2TextBoxSemester.Clear();
            guna2TextBoxDepName.Clear();
            guna2PictureBoxPhoto.Image=null;
        }

        private void brnUpdate_Click(object sender, EventArgs e)
        {
            int bornYear = guna2DateTimePickerDOB.Value.Year;
            int thisYear = DateTime.Now.Year;
            if ((thisYear - bornYear) <= 17 || (thisYear - bornYear) >= 25)
            {
                MessageBox.Show("Age must be between 17 to 25", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(guna2TextBoxSemester.Text, out int semester) || semester < 1 || semester > 8)
            {
                MessageBox.Show("Semester can only be between 1-8", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!guna2RadioButtonGnd.Checked && !guna2RadioButton2Gnd.Checked)
            {
                MessageBox.Show("Please select a gender", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify() && val.ValidateOnlyAlphabet(guna2TextBoxName.Text) && val.ValidateOnlyAlphabet(guna2TextBoxDepName.Text))
            {
                try
                {
                    string gender = guna2RadioButton2Gnd.Checked ? "Male" : "Female";
                    MemoryStream memori = new MemoryStream();
                    guna2PictureBoxPhoto.Image.Save(memori, guna2PictureBoxPhoto.Image.RawFormat);
                    byte[] img = memori.ToArray();
                    studentController.updateStudents(
                        Convert.ToInt32(guna2TextBoxStudentId.Text),
                        guna2TextBoxStudentNim.Text,               
                        guna2TextBoxName.Text,                    
                        guna2DateTimePickerDOB.Value,              
                        gender,                                    
                        Convert.ToInt32(guna2TextBoxSemester.Text), 
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

        private void brnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int selectedValue = (int)dataGridViewStudent.SelectedRows[0].Cells["StId"].Value;
                studentController.deleteStudents(selectedValue);
                refresh();
            }
        }

        private void guna2TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridViewStudent.DataSource = studentController.searchStudents(guna2TextBoxSearch.Text);
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.ForeColor = Color.White;
        }

        private void brnUpdate_MouseEnter(object sender, EventArgs e)
        {
            brnUpdate.ForeColor = Color.White;
        }

        private void brnDelete_MouseEnter(object sender, EventArgs e)
        {
            brnDelete.ForeColor = Color.White;
        }

        private void btnClear_MouseEnter(object sender, EventArgs e)
        {
            btnClear.ForeColor = Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.ForeColor = Color.Fuchsia;
        }

        private void brnUpdate_MouseLeave(object sender, EventArgs e)
        {
            brnUpdate.ForeColor = Color.Fuchsia;
        }

        private void brnDelete_MouseLeave(object sender, EventArgs e)
        {
            brnDelete.ForeColor = Color.Fuchsia;
        }

        private void btnClear_MouseLeave(object sender, EventArgs e)
        {
            btnClear.ForeColor = Color.Fuchsia;
        }

        private void guna2TextBoxStudentId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;

        }

        private void guna2TextBoxStudentNim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;

        }

        private void guna2TextBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;

        }

        private void guna2TextBoxSemester_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;

        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogSt.Document = printDocumentStudents;
            printPreviewDialogSt.ShowDialog();
        }

        private void printDocumentStudents_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string title = "Data students";
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
            Bitmap btm = new Bitmap(this.dataGridViewStudent.Width, this.dataGridViewStudent.Height);
            dataGridViewStudent.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewStudent.Width, this.dataGridViewStudent.Height));
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110;
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }
    }
}
