using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BLBUserInterface
{
    public partial class Default : System.Web.UI.Page
    {
        private BLBService.BLBService service;
        protected void Page_Load(object sender, EventArgs e)
        {
            service = new BLBService.BLBService();
            PopulateBondsTable();
        }

        private void PopulateBondsTable()
        {
            DataSet ds = service.GetBonds();
            GVBonds.DataSource = ds.Tables[0];
            GVBonds.DataBind();
        }

        protected void BTNBuy_Click(object sender, EventArgs e)
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
                    BuyLabel.Text = "Please select bond to buy!";
                }
            }
            catch (Exception)
            {
                BuyLabel.Text = "Incorrect Quantity";
            }


            // searchengine.BuyBond(customerid, cusip, quantity);
        }
    }
}