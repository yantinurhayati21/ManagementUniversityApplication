using Guna.UI2.WinForms;
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
    public partial class FormFees : Form
    {
        private Connection conn;
        private FeesController feesController;
        private ValidationController val;
        public FormFees()
        {
            conn = new Connection();
            feesController = new FeesController();
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

        private void pictureBoxSalary_Click(object sender, EventArgs e)
        {
            FormSalary formSalary = new FormSalary();
            formSalary.Show();
            this.Close();
        }

        private void btnPay_MouseEnter(object sender, EventArgs e)
        {
            btnPay.ForeColor = Color.White;
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            btnReset.ForeColor = Color.White;
        }

        private void btnPay_MouseLeave(object sender, EventArgs e)
        {
            btnPay.ForeColor = Color.Fuchsia;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.ForeColor = Color.Fuchsia;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFeesId.Clear();
            txtStudentName.Clear();
            guna2TextBoxDepName.Clear();
            txtFeesPeriod.Clear();
            txtPayAmount.Clear();
            guna2ComboBoxStuID.SelectedIndex = 0;
            guna2DateTimePickerPayDate.Value = DateTime.Now;
        }

        private void dataGridViewFees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFeesId.Text = dataGridViewFees.CurrentRow.Cells[0].Value.ToString();
            guna2ComboBoxStuID.Text = dataGridViewFees.CurrentRow.Cells[1].Value.ToString();
            txtStudentName.Text = dataGridViewFees.CurrentRow.Cells[2].Value.ToString();
            guna2ComboBoxDepId.Text = dataGridViewFees.CurrentRow.Cells[3].Value.ToString();
            guna2TextBoxDepName.Text = dataGridViewFees.CurrentRow.Cells[4].Value.ToString();
            txtFeesPeriod.Text = dataGridViewFees.CurrentRow.Cells[5].Value.ToString();
            txtPayAmount.Text = dataGridViewFees.CurrentRow.Cells[6].Value.ToString();
            guna2DateTimePickerPayDate.Value = (DateTime)dataGridViewFees.CurrentRow.Cells[7].Value;  
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewFees.DataSource = feesController.selectFees();
            dataGridViewFees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtFeesId.MaxLength = 5;
            guna2ComboBoxStuID.MaxLength = 5;
            txtStudentName.MaxLength = 20;
            guna2TextBoxDepName.MaxLength = 5;
            txtFeesPeriod.MaxLength = 20;
            txtPayAmount.MaxLength = 5;
            StudentId();
            DepartId();
        }

        private void StudentId()
        {
            DataTable data = new DataTable();
            string lecturerId = "SELECT StId FROM Students";
            conn.cmd = new MySqlCommand(lecturerId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("StId", typeof(string));
            data.Load(conn.dr);
            guna2ComboBoxStuID.ValueMember = "StId";
            guna2ComboBoxStuID.DataSource = data;
        }

        private void StudName()
        {
            string stuname = "SELECT * FROM Students WHERE StId = @StId";

            conn.cmd = new MySqlCommand(stuname, conn.GetConn());
            conn.cmd.Parameters.AddWithValue("@StId", guna2ComboBoxStuID.SelectedValue);

            DataTable datast = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(datast);

            if (datast.Rows.Count > 0)
            {
                txtStudentName.Text = datast.Rows[0]["StName"].ToString();
            }
            else
            {
                txtStudentName.Text = string.Empty;
            }
        }

        private void DepartId()
        {
            DataTable data = new DataTable();
            string lecturerId = "SELECT DepId FROM Department";
            conn.cmd = new MySqlCommand(lecturerId, conn.GetConn());
            conn.dr = conn.cmd.ExecuteReader();
            data.Columns.Add("DepId", typeof(string));
            data.Load(conn.dr);
            guna2ComboBoxDepId.ValueMember = "DepId";
            guna2ComboBoxDepId.DataSource = data;
        }

        private void DepartName()
        {
            string stuname = "SELECT * FROM Department WHERE DepId = @DepId";

            conn.cmd = new MySqlCommand(stuname, conn.GetConn());
            conn.cmd.Parameters.AddWithValue("@DepId", guna2ComboBoxDepId.SelectedValue);

            DataTable datast = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(conn.cmd);
            da.Fill(datast);

            if (datast.Rows.Count > 0)
            {
                guna2TextBoxDepName.Text = datast.Rows[0]["DepName"].ToString();
            }
            else
            {
                txtStudentName.Text = string.Empty;
            }
        }
        private void guna2ComboBoxStuID_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudName();
        }

        private void FormFees_Load(object sender, EventArgs e)
        {
            refresh();       
            StudentId();
            DepartId();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(txtStudentName.Text))
            {
                try
                {
                    feesController.addFees(
                        txtFeesId.Text,
                        guna2ComboBoxStuID.Text,
                        txtStudentName.Text,
                        guna2ComboBoxDepId.Text,
                        guna2TextBoxDepName.Text,
                        Convert.ToInt32(txtFeesPeriod.Text),
                        Convert.ToInt32(txtPayAmount.Text),
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

        private void txtStudentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtFeesPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtPayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void printDocumentFees_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string title = "Data Fees";
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
            Bitmap btm = new Bitmap(this.dataGridViewFees.Width, this.dataGridViewFees.Height);
            dataGridViewFees.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewFees.Width, this.dataGridViewFees.Height));
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110;
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogFees.Document = printDocumentFees;
            printPreviewDialogFees.ShowDialog();
        }

        private void pictureBoxSearch_Click(object sender, EventArgs e)
        {
            dataGridViewFees.DataSource = feesController.searchFees(guna2TextBoxSearch.Text);
        }

        private void guna2ComboBoxDepId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepartName();
        }
    }
}
