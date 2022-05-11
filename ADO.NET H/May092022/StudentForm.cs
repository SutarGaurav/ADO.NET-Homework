using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ADO.NET_H.May092022
{
    public partial class StudentForm : Form
    {
        StudentDAL StudentDAL = new StudentDAL();
        public StudentForm()
        {
            InitializeComponent();
        }
        public void ClearAll()
        {
            txtRollNo.Clear();
            txtName.Clear();
            txtBranch.Clear();
            txtPercentage.Clear();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                    txtRollNo.Text = "1";
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtRollNo.Text = id.ToString();
                }
                txtRollNo.Enabled = false;
                txtName.Clear();
                txtBranch.Clear();
                txtPercentage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StudentClass student = new StudentClass();
                student.RollNo = int.Parse(txtRollNo.Text);
                student.Name = txtName.Text;
                student.Branch = txtBranch.Text;
                student.Percentage = float.Parse(txtPercentage.Text);
                int res = StudentDAL.SaveStudent(student);
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
                StudentClass StudentClass = new StudentClass();
                StudentClass.RollNo = Convert.ToInt32(txtRollNo.Text);
                StudentClass.Name = txtName.Text;
                StudentClass.Branch = txtBranch.Text;
                StudentClass.Percentage = float.Parse(txtPercentage.Text);
                int res = StudentDAL.UpdateStudent(StudentClass);
                if (res == 1)
                    MessageBox.Show("Record Updated");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

             
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            StudentClass StudentClass = StudentDAL.SearchStudent(Convert.ToInt32(txtRollNo));
            if(StudentClass.RollNo > 0)
            {
                txtName.Text = StudentClass.Name;
                txtBranch.Text = StudentClass.Branch;
                txtPercentage.Text = StudentClass.Percentage.ToString();
            }
            else
            {
                MessageBox.Show("No Record Found");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = StudentDAL.Delete(Convert.ToInt32(txtRollNo));
            if (res == 1)
                MessageBox.Show("Record Deleted");
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable table =
        }
    }
}
