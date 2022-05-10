using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace ADO.NET_H
{
    public partial class EmployeeForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeForm()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server = LAPTOP-C6NB9IB2\SQLEXPRESS; database = TQTraining; Integrated Security = True");
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
            try
            {
                string qry = "select max(Id) from EmployeeTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                    txtId.Text = "1";
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtId.Text = id.ToString();
                }
                txtId.Enabled = false;  //Manual change restricted.
                txtName.Clear();
                txtDesignation.Clear();
                txtSalary.Clear();
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
            try
            {
                string qry = "insert into EmployeeTable values (@id, @name, @designation,@salary)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Inserted");
                    txtId.Enabled = true;
                    ClearAll();
                }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update EmployeeTable set Name = @name, Designation = @designation Salary = @salary where Id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                    MessageBox.Show("Record Updated");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from EmployeeTable where Id= @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString();
                        txtDesignation.Text = dr["Designation"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                    }
                }
                else
                    MessageBox.Show("Record not found");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from EmployeeTable where Id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                    MessageBox.Show("Record Deleted");
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from EmployeeTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    EmployeeDataGridView.DataSource = table;
                }
                else
                    MessageBox.Show("No record to display");
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

        private void EmployeeDataGridView_Click(object sender, EventArgs e)
        {
            txtId.Text = EmployeeDataGridView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = EmployeeDataGridView.CurrentRow.Cells[1].Value.ToString();
            txtDesignation.Text = EmployeeDataGridView.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = EmployeeDataGridView.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
