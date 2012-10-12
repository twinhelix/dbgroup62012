using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLBWebService.AppCode
{
    public class SearchEngine
    {
        private SqlConnection _con;
        public SearchEngine()
        {
            this._con = new SqlConnection("Server=.;Database=BLBData;Integrated Security=SSPI;");
        }

        public DataSet SearchBond(SearchType type, String searchString)
        {
            DataSet ds = new DataSet();

            String queryString = "SELECT * FROM Bonds WHERE ";
            String queryAttr = "";
            object searchObject = null;
            switch (type)
            {
                case SearchType.CUSIP:
                    queryAttr = "@cusip";
                    queryString = queryString + "CUSIP = " + queryAttr;
                    break;

                case SearchType.NAME:
                    queryAttr = "@name";
                    queryString = queryString + "Name = " + queryAttr;
                    break;

                case SearchType.YIELD:
                    queryAttr = "@Yield";
                    queryString = queryString + "Yield = " + queryAttr;
                    try
                    {
                        float yield = (float)Convert.ToDouble(searchString);
                        searchObject = yield;
                    }
                    catch (Exception)
                    {
                        return ds;
                    }
                    break;

                case SearchType.RATING:
                    queryAttr = "@rating";
                    queryString = queryString + "Rating = " + queryAttr;
                    break;

                case SearchType.ISSUER:
                    queryAttr = "@Issuer";
                    queryString = queryString + "Issuer = " + queryAttr;
                    break;
            }

            // If query is not yield (a float), cast string to object
            if (searchObject == null)
            {
                searchObject = searchString;
            }

            SqlCommand command = new SqlCommand(queryString, _con);
            command.Parameters.AddWithValue(queryAttr, searchObject);

            SqlDataAdapter da = new SqlDataAdapter(command);

            da.Fill(ds, "Bonds");

            return ds;

        }



    }
}