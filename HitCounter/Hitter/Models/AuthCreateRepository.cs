using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hitter.Models
{
    public class AuthCreateRepository
    {
        
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString);
        public List<Models.Customer> GetReceiveMsg(int id)
        {
            var messages = new List<Models.Customer>();

            using (var cmd = new SqlCommand(@"SELECT [id],[name] FROM [dbo].[Customers] WHERE ([id]!=" + id + ")", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    messages.Add(item: new Models.Customer
                    {
                        id = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        name = ds.Tables[0].Rows[i][1].ToString()
                    });
                }

            }
            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table. 
        {
            if (e.Type == SqlNotificationType.Change)
            {
                AuthHub.SendMessages();
            }
        }

    }
}