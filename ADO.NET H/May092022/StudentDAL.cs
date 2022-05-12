using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ADO.NET_H.May092022
{
    public class StudentDAL
    {
        StudentClass StudentClass = new StudentClass();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public object AddNewStudent(StudentClass StudentClass)
        {
            string qry = "select max(RollNo) from StudentTable";
            cmd = new SqlCommand(qry, con);
            con.Open();
            object res = cmd.ExecuteScalar();
            con.Close();
            return res;            
        }
        public int SaveStudent(StudentClass StudentClass)
        {
            string qry = "insert into StudentTable values (@rollno, @name, @branch, @percentage)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", StudentClass.RollNo);
            cmd.Parameters.AddWithValue("@name", StudentClass.Name);
            cmd.Parameters.AddWithValue("@branch", StudentClass.Branch);
            cmd.Parameters.AddWithValue("@percentage", StudentClass.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        } //reference to btnSave_Click
        public int UpdateStudent(StudentClass StudentClass)
        {
            string qry = "select max(RollNo) from StudentTable";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", StudentClass.RollNo);
            cmd.Parameters.AddWithValue("@name", StudentClass.Name);
            cmd.Parameters.AddWithValue("@branch", StudentClass.Branch);
            cmd.Parameters.AddWithValue("@percentage", StudentClass.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public StudentClass SearchStudent(int rollNo)
        {
            StudentClass StudentClass = new StudentClass(); 
            string qry = "select * from StudentTable where RollNo=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollNo);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    StudentClass.RollNo = Convert.ToInt32(dr["RollNo"]);
                    StudentClass.Name = dr["Name"].ToString();
                    StudentClass.Branch = dr["Branch"].ToString();
                    StudentClass.Percentage = Convert.ToSingle(dr["Percentage"]);
                }
            }
            con.Close();
            return StudentClass;
        }
        public int Delete(int RollNo)
        {
            string qry = "delete from StudentTable where RollNo=@RollNo";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", RollNo);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public DataTable ShowAllStudent()
        {
            DataTable table = new DataTable();
            string qry = "select * from StudentTable";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            return table;
        }
    }
}
