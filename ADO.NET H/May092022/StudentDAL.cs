using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ADO.NET_H.May092022
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public int SaveStudent()
        {

        }
    }
}
