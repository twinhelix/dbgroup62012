using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuyLocalBondLibrary.BOL;
using MySql.Data.MySqlClient; 

namespace BuyLocalBondLibrary.DAL
{
    public class CustomerDA : ICustomerDA
    {
        #region DATA MEMBERS

        private const string SQL_SELECT_CUSTOMER = "SELECT * FROM customer WHERE ID = @ID";

        #endregion

        #region PUBLIC METHODS

        public Customer GetCustomer(int id)
        {
            Customer cust = null;

            MySqlParameter[] parms = new MySqlParameter[] {
                new MySqlParameter("@ID", MySqlDbType.Int16)
            };

            parms[0].Value = id;

            //Execute Query
            using (MySqlDataReader rdr = StockValuationLibrary.DAL.MySqlHelper.ExecuteReader(StockValuationLibrary.DAL.MySqlHelper.SV_CONN_STRING, SQL_SELECT_CUSTOMER, parms))
            {
                if (rdr.Read())
                {
                    cust = ConvertReaderToCustomerObject(rdr);
                }
            }

            return cust;
        }

        #endregion

        #region HELPER METHODS

        private Customer ConvertReaderToCustomerObject(MySqlDataReader rdr)
        {
            Customer cust = new Customer();
            cust.Id = StockValuationLibrary.DAL.MySqlHelper.ConvertReaderToInt(rdr, "ID");
            cust.Name = StockValuationLibrary.DAL.MySqlHelper.ConvertReaderToString(rdr, "NAME");
            cust.PortfolioValue = StockValuationLibrary.DAL.MySqlHelper.ConvertReaderToDecimal(rdr, "PORTFOLIO_VALUE");
            
            return cust;
        }

        private static MySqlParameter[] GetCustomerParameters()
        {
            MySqlParameter[] parms;
            parms = new MySqlParameter[] {
                                            new MySqlParameter("@ID", MySqlDbType.VarChar),
                                            new MySqlParameter("@NAME", MySqlDbType.VarChar)
                							};

            return parms;
        }

        private static void SetCompanyParameters(Customer customer, MySqlParameter[] parms)
        {
            parms[0].Value = customer.Id;
            parms[1].Value = customer.Name;
        }

        #endregion
    }

}
