using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectEmployeeSM.datagridActions
{
    public partial class DeleteEmployee : Form
    {
        public DeleteEmployee()
        {
            InitializeComponent();
        }
        APIclass api = new APIclass();
        List<Dictionary<string, string>> employeeList = new List<Dictionary<string, string>>();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            APIclass api = new APIclass();
            var selectedDepartment = comboBox1.SelectedIndex + 1;
            List<Dictionary<string, string>> filteredEmployee = api.getEmployeeByDeptId(selectedDepartment.ToString());
            if (filteredEmployee == null)
            {
                MessageBox.Show("Department has no employee");
                return;
            }

            //for display employee with firstname + lastname
            List<string> displayedEmployee = new List<string>();
            foreach (var i in filteredEmployee)
            {
                string fullname = i["FirstName"] + " " + i["LastName"];
                displayedEmployee.Add(fullname);
            }
            comboBox2.DataSource = displayedEmployee;

            //Prep the list of dict for employee and it id
            employeeList = filteredEmployee;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void DeleteEmployee_Load(object sender, EventArgs e)
        {
            APIclass api = new APIclass();
            List<string> queriedDept = api.getDeptData();
            comboBox1.DataSource = queriedDept;
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> prepEmployeeForDb = new Dictionary<string, string>();
            string employeeIdForUpdate = null;

            //Checking whether the client select the employee or not
            if (comboBox1.SelectedValue.ToString() == "" && comboBox2.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Failed to validate your input, Please select all field before submit");
                return;
            }

            //the below operation will get an employee id that been selected in comboBox and store on employeeIdForUpdate
            var getSelectedEmployee = comboBox2.SelectedValue;
            foreach (var element in employeeList)
            {
                string prepEmployee = element["FirstName"] + " " + element["LastName"];
                if (prepEmployee == getSelectedEmployee.ToString())
                {
                    employeeIdForUpdate = element["EmployeeID"];
                }
            }
            if (employeeIdForUpdate == null)
            {
                MessageBox.Show("Failed to delete employee: Catching 1");
            }

            //Now we get all employee data
            Dictionary<string, string> selectedEmployeeInfo = api.getSingleEmployee(employeeIdForUpdate);
            if (selectedEmployeeInfo == null)
            {
                MessageBox.Show("Failed to delete employee: catching 2");
                return;
            }

            prepEmployeeForDb["EmployeeID"] = employeeIdForUpdate;

            //Now we send the dict of prep employee to api
            bool updateResult = api.deleteEmployee(prepEmployeeForDb);
            if (updateResult == false)
            {
                MessageBox.Show("Failed to delete Employee: catching 3 (last)");
                return;
            }
            MessageBox.Show("Employee Deleted Successfully");
        }
    }
}
