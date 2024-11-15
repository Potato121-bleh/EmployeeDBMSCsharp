using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectEmployeeSM
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        APIclass api = new APIclass();
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {

            APIclass api = new APIclass();
            DataTable dataGridresult = api.getDataForDataGrid();
            if (dataGridresult == null)
            {
                MessageBox.Show("Failed to retrieve data from database");
                return;
            }

            dataGridresult.Columns[0].ColumnName = "Employee_ID";
            dataGridresult.Columns[1].ColumnName = "FirstName";
            dataGridresult.Columns[2].ColumnName = "LastName";
            dataGridresult.Columns[3].ColumnName = "DOB";
            dataGridresult.Columns[4].ColumnName = "Address";
            dataGridresult.Columns[5].ColumnName = "Salary (Currency $)";
            //employeeData.Columns[0].ColumnName = "Employee ID";

            //dataGridView1 = new DataGridView();
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = dataGridresult;
        }

        private void create_btn_Click(object sender, EventArgs e)
        {
            datagridActions.createEmployee createEmployee = new datagridActions.createEmployee();
            createEmployee.ShowDialog();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            datagridActions.updateEmployee updateEmployee = new datagridActions.updateEmployee();
            updateEmployee.ShowDialog();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void getData_btn_Click(object sender, EventArgs e)
        {
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            datagridActions.DeleteEmployee deleteEmployee = new datagridActions.DeleteEmployee();
            deleteEmployee.ShowDialog();
        }

        private void report_btn_Click(object sender, EventArgs e)
        {
            datagridActions.getReport getReportPage = new datagridActions.getReport();
            getReportPage.ShowDialog();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            DialogResult confirmationResult = MessageBox.Show("Are you sure to clear all Employee Data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        
            if (confirmationResult == DialogResult.Yes)
            {
                bool clearResult = api.clearData();
                if (!clearResult) {
                    MessageBox.Show("Failed to clear Employee Data or No data found");
                }
                //dataGridView1.Refresh();
                dataGridView1.DataSource = null;
                MessageBox.Show("All Employee Data Deleted Successfully");
            }
            else
            {
                //Do nth
            }

        }

        private void refreshData_btn_Click(object sender, EventArgs e)
        {
            APIclass api = new APIclass();
            DataTable dataGridresult = api.getDataForDataGrid();
            if (dataGridresult == null)
            {
                MessageBox.Show("Failed to retrieve data from database");
                return;
            }

            dataGridresult.Columns[0].ColumnName = "Employee_ID";
            dataGridresult.Columns[1].ColumnName = "FirstName";
            dataGridresult.Columns[2].ColumnName = "LastName";
            dataGridresult.Columns[3].ColumnName = "DOB";
            dataGridresult.Columns[4].ColumnName = "Address";
            dataGridresult.Columns[5].ColumnName = "Salary (Currency $)";
            //employeeData.Columns[0].ColumnName = "Employee ID";

            //dataGridView1 = new DataGridView();
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = dataGridresult;
        }
    }
}
