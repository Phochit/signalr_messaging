using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hitter.Controllers;
using Hitter.Models;

namespace Hitter
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void sendchat_ServerClick(object sender, EventArgs e)
        {

            AuthController auth = new AuthController();
            auth.Login(username.Value);
            Session["customer"] = username.Value;

        }

       
    }
}