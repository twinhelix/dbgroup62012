using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLBWebService.AppCode
{
    public class CustomerPortfolio
    {
        private string _customerID;
        private SqlConnection _con;

        public CustomerPortfolio(string customerID)
        {
            _customerID = customerID;
            this._con = new SqlConnection("Server=.;Database=BLBData;Integrated Security=SSPI;");
        }

        public DataSet GetPortfolio()
        {
            DataSet ds = new DataSet();



            String queryString = @"SELECT cp.CUSIP, b.Name, cp.Price, cp.Qty, b.Rating, b.Coupon, 
                            b.Yield, b.YTM, b.MaturityDate, b.ParValue, b.Bid, b.Ask, b.Issuer, b.Time
                            FROM Bonds b
                            INNER JOIN CustomerPortfolio cp
                            ON b.CUSIP = cp.CUSIP
                            WHERE cp.CustomerID = @CustomerID";

            SqlCommand command = new SqlCommand(queryString, _con);
            command.Parameters.AddWithValue("@CustomerID", _customerID);
            SqlDataAdapter da = new SqlDataAdapter(command);


            da.Fill(ds, "Fills");
            return ds;

        }
    }
}