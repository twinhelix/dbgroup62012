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
        public SqlBean()
        {
        }

        public DataSet GetBonds()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection("Server=.;Database=BLBData;Integrated Security=SSPI;"); 
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Bonds ORDER BY MaturityDate", con);
            da.Fill(ds, "Fills");
            return ds;
        }
    }
}