using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ADO.NET_H.Employee
{

    class EmployeeDAL
    {
        EmployeeClass empobj = new EmployeeClass();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public int SaveEmployee(EmployeeClass empobj)
        {
            string qry = "insert into EmployeeTable values (@id, @name, @designation, @salary)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", empobj.Id);
            cmd.Parameters.AddWithValue("@name", empobj.Name);
            cmd.Parameters.AddWithValue("@designation", empobj.Designation);
            cmd.Parameters.AddWithValue("@salary", empobj.Salary);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int UpdateEmployee(EmployeeClass empobj)
        {
            string qry = "select max(ID) from EmployeeTable";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", empobj.Id);
            cmd.Parameters.AddWithValue("@name", empobj.Name);
            cmd.Parameters.AddWithValue("@designation", empobj.Designation);
            cmd.Parameters.AddWithValue("@salary", empobj.Salary);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public EmployeeClass SearchEmployee(int id)
        {
            EmployeeClass empobj = new EmployeeClass();
            string qry = "select * from EmployeeTable where ID=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    empobj.Id = Convert.ToInt32(dr["ID"]);
                    empobj.Name = dr["Name"].ToString();
                    empobj.Designation = dr["Designation"].ToString();
                    empobj.Salary = Convert.ToInt32(dr["Salary"]);
                }
            }
            con.Close();
            return empobj;
        }
        public int Delete(int Id)
        {
            string qry = "delete from EmployeeTable where ID=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", Id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public DataTable ShowAll()
        {
            DataTable table = new DataTable();
            string qry = "select * from EmployeeTable";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            return table;
        }

    }
}
