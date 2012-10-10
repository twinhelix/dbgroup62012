using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLBWebService.AppCode
{
    public class SqlBean
    {
        private SqlConnection _con;

        public SqlBean()
        {
            this._con = new SqlConnection("Server=.;Database=BLBData;Integrated Security=SSPI;"); 
        }

        public DataSet GetBonds()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Bonds ORDER BY MaturityDate", _con);
            da.Fill(ds, "Fills");
            return ds;
        }

        public bool Authenticate(string username, string password)
        {
            string query = "SELECT password FROM Credentials WHERE username=\'" + username + "\'";
            SqlDataAdapter da = new SqlDataAdapter(query, _con);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "Fills");
            dt = ds.Tables["Fills"];
            if (dt.Rows.Count == 0)
                return false;
            foreach (DataRow dr in dt.Rows)
            {
                string pwd = dr[0].ToString();
                if (pwd.Equals(password))
                    return true;
            }
            return false;
        }
    }
}