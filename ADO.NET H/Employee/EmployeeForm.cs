using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_H.Employee
{
    public partial class EmployeeForm : Form
    {
        EmployeeDAL empdal = new EmployeeDAL();
        public EmployeeForm()
        {
            InitializeComponent();
        }
        public void ClearAll()
        {
            txtId.Clear();
            txtName.Clear();
            txtDesignation.Clear();
            txtSalary.Clear();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            { 
                EmployeeClass empobj = new EmployeeClass();
                empobj.Id = Convert.ToInt32(txtId.Text);
                empobj.Name = txtName.Text;
                empobj.Designation = txtDesignation.Text;
                empobj.Salary = Convert.ToInt32(txtSalary.Text);
                int res = empdal.SaveEmployee(empobj);
                if (res == 1)
                {
                        MessageBox.Show("Record Inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ClearAll();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeClass empobj = new EmployeeClass();
                empobj.Id = Convert.ToInt32(txtId.Text);
                empobj.Name = txtName.Text;
                empobj.Designation = txtDesignation.Text;
                empobj.Salary = Convert.ToInt32(txtSalary.Text);
                int res = empdal.UpdateEmployee(empobj);
                if (res == 1)
                    MessageBox.Show("Record Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeeClass empobj = empdal.SearchEmployee(Convert.ToInt32(txtId.Text));
            if (empobj.Id > 0)
            {
                txtName.Text = empobj.Name;
                txtDesignation.Text = empobj.Designation;
                txtSalary.Text = empobj.Salary.ToString();
            }
            else
            {
                MessageBox.Show("No Record Found");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = empdal.Delete(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("Record Deleted");
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable table = empdal.ShowAll();
            EmployeeDataGridView.DataSource = table;
        }
    }
}
