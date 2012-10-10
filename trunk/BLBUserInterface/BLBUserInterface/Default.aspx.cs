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
    }
}