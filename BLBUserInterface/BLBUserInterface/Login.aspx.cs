using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BLBUserInterface
{
    public partial class Login : System.Web.UI.Page
    {
        private BLBService.BLBService service = new BLBService.BLBService();
        
        protected void Page_Load(object sender, EventArgs e)
        {
       
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            bool result = service.Login(TextBoxUsername.Text, TextBoxPassword.Text);
            if (result)
            {
                TextBoxUsername.Text = "Hi";
            }
        }
    }
}