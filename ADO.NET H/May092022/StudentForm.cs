using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ADO.NET_H
{
    public partial class StudentForm : Form
    {
        
        public StudentForm()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Id) from StudentTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
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
            finally
            {
                con.Close();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }
    }
}
