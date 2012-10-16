using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BLBUserInterface
{
    public partial class BondSearchPage : System.Web.UI.Page
    {
        private BLBService.BLBService service;
        private BLBService.SearchType type;

        protected void Page_Load(object sender, EventArgs e)
        {

            service = new BLBService.BLBService();
            LabelResults.Visible = false;
            if (!Page.IsPostBack)
            {
                //Please check if you are binding checkbox controls here. 
                //If not bring them in here


                DropDownSearch.DataSource = Enum.GetValues(typeof(BLBService.SearchType));
                DropDownSearch.DataBind();
            }

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                type = BLBService.SearchType.CUSIP;
                string selectedString = DropDownSearch.SelectedValue;
                if (selectedString != null)
                {
                    type = (BLBService.SearchType)Enum.Parse(typeof(BLBService.SearchType), selectedString, true);
                }
                String search = TextSearchString.Text;

                DataSet ds = service.Search(type, search);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    LabelResults.Visible = true;
                    GVSearchResult.DataSource = null;
                    GVSearchResult.DataBind();
                }
                else
                {
                    LabelResults.Visible = false;
                    GVSearchResult.DataSource = ds.Tables[0];
                    GVSearchResult.DataBind();
                }
            }
            else
            {
                GVSearchResult.DataSource = null;
                GVSearchResult.DataBind();
            }
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