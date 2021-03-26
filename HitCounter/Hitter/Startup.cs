using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Hitter.Startup))]

namespace Hitter
{
    public class Startup
    {
        string con = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString;

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            System.Data.SqlClient.SqlDependency.Start(con);
        }
        protected void Application_Start(Object sender,EventArgs e)
        {
            //SqlDependency.Start(con);
        }

        protected void Application_End(Object sender, EventArgs e)
        {
            // Stop SqlDependency notifications
            //SqlDependency.Stop(con);
        }


    }
}
