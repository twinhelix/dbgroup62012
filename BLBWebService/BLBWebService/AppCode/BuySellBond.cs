using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace BLBWebService.AppCode
{
    public class BuySellBond
    {
        private string _id;
        private SqlConnection _con;

        public BuySellBond(string id)
        {
            _id = id;
            this._con = new SqlConnection("Server=.;Database=BLBData;Integrated Security=SSPI;");
        }

        public bool BuyBond(string cusip, int qty)
        {
            // Sanity check
            if (qty <= 0)
            {
                return false;
            }

            // Check if cusip is a valid bond
            string queryStringCheck = "SELECT CUSIP, QTY, Ask FROM Bonds WHERE CUSIP = @CUSIP";
            SqlCommand command = new SqlCommand(queryStringCheck, _con);
            command.Parameters.AddWithValue("@CUSIP", cusip);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds, "bonds");
            bool validBond = ds.Tables[0].AsEnumerable().Any(row => cusip == row.Field<String>("CUSIP")
                && qty <= row.Field<int>("QTY"));
            
            if (validBond)
            {
                float price = (float)Convert.ToDouble(ds.Tables[0].Rows[0]["Ask"]);
                float totalSpend = price * qty;

                float currentCash = getCustomerCash();
                if (totalSpend > currentCash)
                {
                    return false;
                }

                currentCash = currentCash - totalSpend;

                updateCustomerCash(currentCash);

                int currentMarketQty = Convert.ToInt32(ds.Tables[0].Rows[0]["QTY"]);
                updateBondsDatabase(cusip, (currentMarketQty - qty));


                // Updates User's Portfolio:

                string queryStringInsert = "";
                // Checks if user already have bond
                int existingQty = checkQuantity(cusip);
                if (existingQty > 0)
                {
                    qty += existingQty;
                    queryStringInsert = "UPDATE CustomerPortfolio SET Price = @Price, Qty = @Qty WHERE CUSIP = @Cusip AND CustomerID = @CustomerID";
                }
                else
                {
                    queryStringInsert = "INSERT INTO CustomerPortfolio (CUSIP, CustomerID, Price, Qty) VALUES (@CUSIP, @CustomerID, @Price, @Qty)";
                }
                     
                command = new SqlCommand(queryStringInsert, _con);

                command.Parameters.AddWithValue("@CUSIP", cusip);
                command.Parameters.AddWithValue("@CustomerID", _id);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Qty", qty);
                _con.Open();
                var result = command.ExecuteNonQuery();
                _con.Close();

                if (result != -1)
                {
                    InsertIntoTransactionsTable(new Transaction());
                    return true;
                }
            }
            return false;
        }

        public bool SellBond(string cusip, int qty)
        {
            // Check to see if user has said bond
            int currentQty = checkQuantity(cusip);
            if (currentQty < qty || currentQty <= 0)
            {
                return false;
            }

            // Get bond characteristics from the market
            string queryStringCheck = "SELECT CUSIP, Bid FROM Bonds WHERE CUSIP = @CUSIP";
            SqlCommand command = new SqlCommand(queryStringCheck, _con);
            command.Parameters.AddWithValue("@CUSIP", cusip);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds, "bonds");
            bool validBond = ds.Tables[0].AsEnumerable().Any(row => cusip == row.Field<String>("CUSIP"));

            // Bond is a valid in the market
            if (validBond)
            {
                // Get the price of the bond (Bid Price)
                float bidPrice = (float)Convert.ToDouble(ds.Tables[0].Rows[0]["Bid"]);
                float sellAmount = bidPrice * qty;

                // Update the customer's cash holdings
                updateCustomerCash(sellAmount + getCustomerCash());

                // Update the customer's bond holdings

                string queryString = "";


                int remainingQty = currentQty - qty;
                if (remainingQty == 0)
                {
                    // No more holdings of said bond. Delete from holdings table
                    queryString = "DELETE FROM CustomerPortfolio WHERE CUSIP = @Cusip AND CustomerID = @CustomerID";
                }
                else
                {
                    // Still remaining bonds, update quantity
                    queryString = "UPDATE CustomerPortfolio SET Qty = @Qty WHERE CUSIP = @Cusip AND CustomerID = @CustomerID";
                }

                // TODO: Determine whether to put into market or not
                command = new SqlCommand(queryString, _con);

                command.Parameters.AddWithValue("@CUSIP", cusip);
                command.Parameters.AddWithValue("@CustomerID", _id);

                _con.Open();
                var result = command.ExecuteNonQuery();
                _con.Close();

                if (result != -1)
                {
                    InsertIntoTransactionsTable(new Transaction());
                    return true;
                }
            }
            return false;
        }

        private void updateBondsDatabase(string cusip, int qty)
        {
            String queryStringUpdate = "UPDATE Bonds SET QTY = @QTY WHERE CUSIP = @CUSIP";

            SqlCommand command = new SqlCommand(queryStringUpdate, _con);

            command.Parameters.AddWithValue("@QTY", qty);
            command.Parameters.AddWithValue("@CUSIP", cusip);

            _con.Open();
            var result = command.ExecuteNonQuery();
            _con.Close();
        }

        private void updateCustomerCash(float currentCash)
        {
            String queryStringUpdate = "UPDATE Customer SET Cash = @Cash WHERE CustomerID = @CustomerID";

            SqlCommand command = new SqlCommand(queryStringUpdate, _con);

            command.Parameters.AddWithValue("@CustomerID", _id);
            command.Parameters.AddWithValue("@Cash", currentCash);

            _con.Open();
            var result = command.ExecuteNonQuery();
            _con.Close();
        }

        private int checkQuantity(string cusip)
        {
            int existingQty = 0;

            string queryStringCheck = "SELECT * FROM CustomerPortfolio WHERE CUSIP = @CUSIP AND CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(queryStringCheck, _con);
            command.Parameters.AddWithValue("@CUSIP", cusip);
            command.Parameters.AddWithValue("@CustomerID", _id);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds, "bonds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                existingQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]);
            }

            return existingQty;
        }

        private float getCustomerCash()
        {
            string queryStringCheck = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(queryStringCheck, _con);
            command.Parameters.AddWithValue("@CustomerID", _id);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds, "bonds");

            if (ds.Tables[0].Rows.Count > 0)
            {
                float cash = (float)Convert.ToDouble(ds.Tables[0].Rows[0]["Cash"]);
                return cash;
            }
            return 0;
        }

        private void InsertIntoTransactionsTable(Transaction t)
        {

        }
    }

    public class Transaction
    {
        public Transaction()
        {

        }
    }
}