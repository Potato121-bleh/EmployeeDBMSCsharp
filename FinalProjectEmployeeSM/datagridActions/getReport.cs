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
    public partial class getReport : Form
    {
        public getReport()
        {
            InitializeComponent();
        }

        APIclass api = new APIclass();
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void TotalEmployee_label_Click(object sender, EventArgs e)
        {

        }

        private void getReport_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> reportData = api.getReport();
            if (reportData == null) {
                MessageBox.Show("Failed to generate Report: getReport_Load Error 1");
                return;
            }

            label5.Text = reportData["totalDept"];
            label7.Text = reportData["totalSalary"];
            TotalEmployee_label.Text = reportData["totalEmp"];

        }
    }
}
