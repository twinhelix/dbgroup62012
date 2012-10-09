using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BLBService.WebService ws = new BLBService.WebService();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        int total = ws.addMethod(Convert.ToInt32(txtOp1.Text), Convert.ToInt32(txtOp2.Text));
        txtTotal.Text = total.ToString();
    }
}