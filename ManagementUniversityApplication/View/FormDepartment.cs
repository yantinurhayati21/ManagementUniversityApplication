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
        private ValidationController validationController;
        public FormDepartment()
        {
            conn = new Connection();
            departmentController = new DepartmentController();
            validationController = new ValidationController();
            InitializeComponent();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewDepartment.DataSource = departmentController.selectDepartment();
            dataGridViewDepartment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridViewDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDepartmentId.Text = dataGridViewDepartment.CurrentRow.Cells[0].Value.ToString();
            txtDepartmentName.Text = dataGridViewDepartment.CurrentRow.Cells[1].Value.ToString();
            txtDepartmentNmDekan.Text = dataGridViewDepartment.CurrentRow.Cells[2].Value.ToString();
            txtDepartmentDesc.Text = dataGridViewDepartment.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            departmentController.addDepartment(Convert.ToInt32(txtDepartmentId.Text), txtDepartmentName.Text, txtDepartmentNmDekan.Text, txtDepartmentDesc.Text);
            refresh();
        }

        private void brnUpdate_Click(object sender, EventArgs e)
        {
            departmentController.updateDepartment(Convert.ToInt32(txtDepartmentId.Text), txtDepartmentName.Text, txtDepartmentNmDekan.Text, txtDepartmentDesc.Text);
            refresh();
        }

        private void brnDelete_Click(object sender, EventArgs e)
        {
            int selectedValue = (int)dataGridViewDepartment.SelectedRows[0].Cells["DepId"].Value;
            departmentController.deleteDepartment(selectedValue);
            refresh();
        }
    }
}
