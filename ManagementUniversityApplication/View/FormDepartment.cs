using ManagementUniversityApplication.Controller;
using ManagementUniversityApplication.Model;
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
    public partial class FormDepartment : Form
    {
        private Connection conn;
        private DepartmentController departmentController;
        private ValidationController val;
        public FormDepartment()
        {
            conn = new Connection(); 
            departmentController = new DepartmentController();
            val = new ValidationController();
            InitializeComponent();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewDepartment.DataSource = departmentController.selectDepartment();
            dataGridViewDepartment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtDepartmentId.MaxLength = 5;
            txtDepartmentName.MaxLength = 20;
            txtDepartmentNmDekan.MaxLength = 20;
            txtDepartmentDesc.MaxLength = 30;
        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            refresh(); 
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {

            if(val.ValidateOnlyAlphabet(txtDepartmentName.Text) && val.ValidateOnlyAlphabet(txtDepartmentNmDekan.Text) && val.ValidateAlphabetAndNumber(txtDepartmentDesc.Text))
            {
                try
                {
                    departmentController.addDepartment(Convert.ToInt32(txtDepartmentId.Text), txtDepartmentName.Text, txtDepartmentNmDekan.Text, txtDepartmentDesc.Text);
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

        private void brnUpdate_Click(object sender, EventArgs e)
        {
            if (val.ValidateOnlyAlphabet(txtDepartmentName.Text) && val.ValidateOnlyAlphabet(txtDepartmentNmDekan.Text) && val.ValidateAlphabetAndNumber(txtDepartmentDesc.Text))
            {
                try
                {
                    departmentController.updateDepartment(Convert.ToInt32(txtDepartmentId.Text), txtDepartmentName.Text, txtDepartmentNmDekan.Text, txtDepartmentDesc.Text);
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
                int selectedValue = (int)dataGridViewDepartment.SelectedRows[0].Cells["DepId"].Value;
                departmentController.deleteDepartment(selectedValue);
                refresh();
            }
        }

        private void txtDepartmentId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtDepartmentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void txtDepartmentNmDekan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDepartmentId.Clear();
            txtDepartmentName.Clear();
            txtDepartmentNmDekan.Clear();
            txtDepartmentDesc.Clear();
        }

        private void pictureBoxSearch_Click(object sender, EventArgs e)
        {
            dataGridViewDepartment.DataSource = departmentController.searchDepartment(guna2TextBoxSearch.Text);
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialogDepartment.Document = printDocumentDepartment;
            printPreviewDialogDepartment.ShowDialog();
        }

        private void printDocumentDepartment_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Judul Data Department
            string title = "Data Department";
            Font titleFont = new Font("Arial", 32, FontStyle.Bold);

            // Warna judul
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                // Menggambar judul di tengah atas halaman
                SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                float titleX = (e.PageBounds.Width - titleSize.Width) / 2;
                float titleY = 15;
                e.Graphics.DrawString(title, titleFont, brush, titleX, titleY);
            }

            // Menambahkan garis sebagai pemisah
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(pen, new Point(50, 90), new Point(e.PageBounds.Width - 50, 90));
            }

            // Menggambar data dari DataGridView
            Bitmap btm = new Bitmap(this.dataGridViewDepartment.Width, this.dataGridViewDepartment.Height);
            dataGridViewDepartment.DrawToBitmap(btm, new Rectangle(0, 0, this.dataGridViewDepartment.Width, this.dataGridViewDepartment.Height));

            // Menggambar DataGridView di tengah halaman
            float dataGridViewX = (e.PageBounds.Width - btm.Width) / 2;
            float dataGridViewY = 110; // Mulai dari 110 (dibawah garis pemisah)
            e.Graphics.DrawImage(btm, dataGridViewX, dataGridViewY);

            // Menambahkan teks pada pictureBoxPrint
            e.Graphics.DrawString(pictureBoxPrint.Text, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(310, 50));
        }

        private void dataGridViewDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDepartmentId.Text = dataGridViewDepartment.CurrentRow.Cells[0].Value.ToString();
            txtDepartmentName.Text = dataGridViewDepartment.CurrentRow.Cells[1].Value.ToString();
            txtDepartmentNmDekan.Text = dataGridViewDepartment.CurrentRow.Cells[2].Value.ToString();
            txtDepartmentDesc.Text = dataGridViewDepartment.CurrentRow.Cells[3].Value.ToString();
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

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            Dashboard dsb = new Dashboard();
            dsb.Show();
            this.Close();
        }

        private void pictureBoxStudent_Click(object sender, EventArgs e)
        {
            FormStudents st = new FormStudents();
            st.Show();
            this.Close();
        }

        private void pictureBoxCourses_Click(object sender, EventArgs e)
        {
            FormCourses cs = new FormCourses();
            cs.Show();
            this.Close();
        }

        private void pictureBoxLecturer_Click(object sender, EventArgs e)
        {
            FormLecturer lr = new FormLecturer();
            lr.Show();
            this.Close();
        }

        private void pictureBoxLearning_Click(object sender, EventArgs e)
        {
            FormLearning lrn = new FormLearning();
            lrn.Show();
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
            FormSalary sl = new FormSalary();
            sl.Show();
            this.Close();
        }

        private void txtDepartmentDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}
