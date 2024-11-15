using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProjectEmployeeSM.datagridActions
{
    public partial class updateEmployee : Form
    {
        public updateEmployee()
        {
            InitializeComponent();
        }
        List<Dictionary<string, string>> employeeList = new List<Dictionary<string, string>>();
        private void department_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            APIclass api = new APIclass();
            var selectedDepartment = department_comboBox.SelectedIndex + 1;
            List<Dictionary<string, string>> filteredEmployee = api.getEmployeeByDeptId(selectedDepartment.ToString());
            if (filteredEmployee == null)
            {
                MessageBox.Show("Department has no employee");
                return;
            }

            //for display employee with firstname + lastname
            List<string> displayedEmployee = new List<string>();
            foreach (var i in filteredEmployee) {
                string fullname = i["FirstName"] + " " + i["LastName"];
                displayedEmployee.Add(fullname);
            }
            Employee_comboBox.DataSource = displayedEmployee;

            //Prep the list of dict for employee and it id
            employeeList = filteredEmployee;

        }

        private void Employee_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void updateEmployee_Load(object sender, EventArgs e)
        {
            APIclass api = new APIclass();
            List<string> queriedDept = api.getDeptData();
            department_comboBox.DataSource = queriedDept;
        }


        private void firstName_textBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LastName_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DOB_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Address_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Salary_textBox_TextChanged(object sender, EventArgs e)
        {

        }
        APIclass api = new APIclass();
        private void updateEmp_btn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> prepEmployeeForDb = new Dictionary<string, string>();
            string employeeIdForUpdate = null;

            //Validate user input, THEY have to input BOTH department & employee to continue
            if (department_comboBox.SelectedValue.ToString() == "" && Employee_comboBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Failed to validate your input, Please select all field before submit");
                return;
            }

            //the below operation will get an employee id that been selected in comboBox and store on employeeIdForUpdate
            var getSelectedEmployee = Employee_comboBox.SelectedValue;
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
                MessageBox.Show("Failed to update employee: Catching 1");
            }

            //Now we get all employee data
            Dictionary<string, string> selectedEmployeeInfo = api.getSingleEmployee(employeeIdForUpdate);
            if (selectedEmployeeInfo == null)
            {
                MessageBox.Show("Failed to update employee: catching 2");
                return;
            }

            prepEmployeeForDb["EmployeeID"] = employeeIdForUpdate;
            prepEmployeeForDb["FirstName"] = string.IsNullOrEmpty(firstName_textBox.Text) ? selectedEmployeeInfo["FirstName"] : firstName_textBox.Text;
            prepEmployeeForDb["LastName"] = string.IsNullOrEmpty(LastName_textBox.Text) ? selectedEmployeeInfo["LastName"] : LastName_textBox.Text;
            prepEmployeeForDb["DOB"] = string.IsNullOrEmpty(DOB_textBox.Text) ? selectedEmployeeInfo["DOB"] : DOB_textBox.Text;
            prepEmployeeForDb["Address"] = string.IsNullOrEmpty(Address_textBox.Text) ? selectedEmployeeInfo["Address"] : Address_textBox.Text;
            prepEmployeeForDb["Salary"] = string.IsNullOrEmpty(Salary_textBox.Text) ? selectedEmployeeInfo["Salary"] : Salary_textBox.Text;

            //Now we send the dict of prep employee to api
            bool updateResult = api.updateEmployee(prepEmployeeForDb);
            if (updateResult == false)
            {
                MessageBox.Show("Failed to update Employee: catching 3 (last)");
                return;
            }
            MessageBox.Show("Employee Updated Successfully");
        }
    }
}
