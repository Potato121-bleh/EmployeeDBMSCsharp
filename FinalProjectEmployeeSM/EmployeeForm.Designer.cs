namespace FinalProjectEmployeeSM
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataSet1 = new FinalProjectEmployeeSM.DataSet1();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.create_btn = new System.Windows.Forms.Button();
            this.update_btn = new System.Windows.Forms.Button();
            this.clear_btn = new System.Windows.Forms.Button();
            this.delete_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.report_btn = new System.Windows.Forms.Button();
            this.refreshData_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSet1BindingSource
            // 
            this.dataSet1BindingSource.DataSource = this.dataSet1;
            this.dataSet1BindingSource.Position = 0;
            // 
            // create_btn
            // 
            this.create_btn.Location = new System.Drawing.Point(638, 29);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(109, 48);
            this.create_btn.TabIndex = 2;
            this.create_btn.Text = "Create";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.create_btn_Click);
            // 
            // update_btn
            // 
            this.update_btn.Location = new System.Drawing.Point(638, 97);
            this.update_btn.Name = "update_btn";
            this.update_btn.Size = new System.Drawing.Size(109, 48);
            this.update_btn.TabIndex = 3;
            this.update_btn.Text = "Update";
            this.update_btn.UseVisualStyleBackColor = true;
            this.update_btn.Click += new System.EventHandler(this.update_btn_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.Location = new System.Drawing.Point(638, 294);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(109, 48);
            this.clear_btn.TabIndex = 5;
            this.clear_btn.Text = "Clear";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // delete_btn
            // 
            this.delete_btn.Location = new System.Drawing.Point(638, 162);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(109, 48);
            this.delete_btn.TabIndex = 4;
            this.delete_btn.Text = "Delete";
            this.delete_btn.UseVisualStyleBackColor = true;
            this.delete_btn.Click += new System.EventHandler(this.delete_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(548, 322);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // report_btn
            // 
            this.report_btn.Location = new System.Drawing.Point(638, 227);
            this.report_btn.Name = "report_btn";
            this.report_btn.Size = new System.Drawing.Size(109, 48);
            this.report_btn.TabIndex = 7;
            this.report_btn.Text = "Report";
            this.report_btn.UseVisualStyleBackColor = true;
            this.report_btn.Click += new System.EventHandler(this.report_btn_Click);
            // 
            // refreshData_btn
            // 
            this.refreshData_btn.Location = new System.Drawing.Point(248, 385);
            this.refreshData_btn.Name = "refreshData_btn";
            this.refreshData_btn.Size = new System.Drawing.Size(168, 48);
            this.refreshData_btn.TabIndex = 8;
            this.refreshData_btn.Text = "Refresh Data";
            this.refreshData_btn.UseVisualStyleBackColor = true;
            this.refreshData_btn.Click += new System.EventHandler(this.refreshData_btn_Click);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 464);
            this.Controls.Add(this.refreshData_btn);
            this.Controls.Add(this.report_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.delete_btn);
            this.Controls.Add(this.update_btn);
            this.Controls.Add(this.create_btn);
            this.Name = "EmployeeForm";
            this.Text = "EmployeeForm";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Button create_btn;
        private System.Windows.Forms.Button update_btn;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Button delete_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button report_btn;
        private System.Windows.Forms.Button refreshData_btn;
    }
}