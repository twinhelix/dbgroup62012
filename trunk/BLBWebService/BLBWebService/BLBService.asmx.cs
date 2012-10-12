using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BLBWebService.AppCode;

namespace BLBWebService
{
    /// <summary>
    /// Summary description for BLBService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BLBService : System.Web.Services.WebService
    {

        SqlBean sb = new SqlBean();
        SearchEngine se = new SearchEngine();

        [WebMethod]
        public DataSet GetBonds()
        {
            return sb.GetBonds();
        }

        [WebMethod]
        public bool Login(string username, string password)
        {
            return sb.Authenticate(username,password);
        }

        [WebMethod]
        public DataSet Search(SearchType type, string searchString)
        {
            return se.SearchBond(type, searchString);
        }

        [WebMethod]
        public DataSet GetCustomerPortfolio(string customerID)
        {
            CustomerPortfolio cp = new CustomerPortfolio(customerID);
            return cp.GetPortfolio();
        }
    }
}
