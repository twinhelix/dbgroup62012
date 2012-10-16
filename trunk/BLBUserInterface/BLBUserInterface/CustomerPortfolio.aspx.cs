using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BLBUserInterface
{
    public partial class CustomerPortfolio : System.Web.UI.Page
    {
        private string _customerID;
        private BLBService.BLBService service = new BLBService.BLBService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnGetPortfolio_Click(object sender, EventArgs e)
        {
            _customerID = TextBoxCustomerID.Text;
            LabelTitle.Text = _customerID + "'s Portfolio";
            GetPortfolio();
        }

        private void GetPortfolio()
        {
            if (_customerID != null)
            {
                DataSet ds = service.GetCustomerPortfolio(_customerID);
                GVPortfolio.DataSource = ds.Tables[0];
                GVPortfolio.DataBind();
                

            }
        }

        protected void BTNSell_Click(object sender, EventArgs e)
        {
            String cusip = Request.Form["RadioButton"];
            try
            {
                int quantity = Convert.ToInt32(TBQuantity.Text);
                if (cusip != null)
                {
                    BuyLabel.Text = "Selected Bond: " + cusip;
                }
                else
                {
                    BuyLabel.Text = "Please select bond to Sell!";
                }
            }
            catch (Exception)
            {
                BuyLabel.Text = "Incorrect Quantity";
            }


            // searchengine.SellBond(customerid, cusip, quantity);
        }

    }
}