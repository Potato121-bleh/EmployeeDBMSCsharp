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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            APIclass api = new APIclass();
            EmployeeForm employeeForm = new EmployeeForm();
            string usernameZ = textBox1.Text;
            string passwordZ = textBox2.Text;
            string autheticationErr = api.checkAuth(usernameZ, passwordZ);
            if (autheticationErr != null) {
                MessageBox.Show(autheticationErr);
                return;
            }
            MessageBox.Show("Login successfully");
            employeeForm.ShowDialog();
            

            
        }
    }
}
