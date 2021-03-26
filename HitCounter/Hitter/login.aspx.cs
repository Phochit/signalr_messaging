using Hitter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hitter.Models;

namespace Hitter
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void sendchat_ServerClick(object sender, EventArgs e)
        {
            AuthController auth = new AuthController();

            auth.Login(username.Value);
            Customer cu = new Customer();
            cu= auth.GetCustomer(username.Value);
            LoginStatusController lcon = new LoginStatusController();
            Models.LoginStatus lg = new Models.LoginStatus();
            lg.loginid = cu.id;
            lg.LastLoginTime = DateTime.UtcNow.AddMinutes(390);
            lg.loginstatus = 1;
            lcon.InsertLgStatus(lg);

            Session["customer"] =username.Value;
            Session["myid"] = cu.id;
            Response.Redirect("Chat.aspx");
        }
    }
}