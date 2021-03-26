using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hitter.Controllers;

namespace Hitter
{
    public partial class hitter : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["customer"] != null)
                {
                    logout.Visible = true;
                }
                else
                {
                    logout.Visible = false;
                }
            }


        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            LoginStatusController con = new LoginStatusController();
            con.Logout(Convert.ToInt32(Session["myid"]));
            Session.Clear();
            Response.Redirect("login.aspx");
        }
    }
}