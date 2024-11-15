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
    public partial class createEmployee : Form
    {
        public createEmployee()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //APIclass api = new APIclass();
            //List<string> deptList = api.getDeptData();

        }
        private void createEmployee_Load(object sender, EventArgs e)
        {
            

            APIclass api = new APIclass();
            List<string> deptList = api.getDeptData();
            
            if (deptList == null)
            {
                MessageBox.Show("failed to retreive data from database");
                return;
            }
            int numberofList = deptList.Count;

            foreach(string element in deptList)
            {
                comboBox2.Items.Add(element);
            }
            
            
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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

        private void createEmployee_btn_Click(object sender, EventArgs e)
        {

            //Data validation
            if (firstName_textBox.Text == "" || LastName_textBox.Text == "" || DOB_textBox.Text == "" || Address_textBox.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please insert all field to continue");
                return;
            }

            APIclass api = new APIclass();
            int selectedDept = comboBox2.SelectedIndex;
            if (selectedDept == -1)
            {
                MessageBox.Show("Please select which department of new employee");
                return;
            }

            
            selectedDept += 1;
            bool ok = api.PostEmployeeRequest(firstName_textBox.Text, LastName_textBox.Text, DOB_textBox.Text, Address_textBox.Text, selectedDept, int.Parse(textBox5.Text));
            if (!ok) {
                MessageBox.Show("Failed to create new employee, please try again");
                return ;
            }
            MessageBox.Show("Employee Created Successfully");
        }
    }
}
