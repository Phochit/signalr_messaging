using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Hitter.Models
{
    public class V2Repository
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString);
        public List<V2_Conversation> GetReceiveMsg(int myid,int sid)
        {
            var messages = new List<V2_Conversation>();
            using (var cmd = new SqlCommand(@"SELECT [id],[sender_id],[receiver_id],[message],[created_at] FROM [dbo].[V2_Conversation] 
                                            WHERE ([receiver_id]=" + myid+" AND [sender_id]="+sid+") OR " +
                                            "([receiver_id]=" + sid + " AND [sender_id]=" + myid + ") " +
                                            "ORDER BY [created_at]", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //DateTime dt1 = DateTime.ParseExact(ds.Tables[0].Rows[i][4].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    messages.Add(item: new V2_Conversation
                    {
                        id = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        sender_id = int.Parse(ds.Tables[0].Rows[i][1].ToString()),
                        receiver_id = Convert.ToInt32(ds.Tables[0].Rows[i][2]),
                        message = ds.Tables[0].Rows[i][3].ToString(),
                        //status =Convert.ToInt32( ds.Tables[0].Rows[i][4].ToString()),
                        created_at = Convert.ToDateTime( ds.Tables[0].Rows[i][4].ToString())
                });
                }
            }
            return messages;
        }

        public List<LoginStatus> GetReceiveLgsMsg(int myid)
        {
            var messages = new List<LoginStatus>();

            using (var cmd = new SqlCommand(@"SELECT [id],[loginid],[LastLoginTime],[loginstatus] FROM [dbo].[LoginStatus] 
                                            WHERE ([loginid]!=" + myid + ")", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //DateTime dt1 = DateTime.ParseExact(ds.Tables[0].Rows[i][4].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    messages.Add(item: new LoginStatus
                    {
                        id = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        loginid = int.Parse(ds.Tables[0].Rows[i][1].ToString()),
                        LastLoginTime = Convert.ToDateTime(ds.Tables[0].Rows[i][2]),
                        loginstatus = Convert.ToInt32(ds.Tables[0].Rows[i][3].ToString())
                    });
                }

            }
            return messages;
        }

        public List<Models.Customer> GetReceiveLoginMsg(int id)
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
                MyHub2.SendMessages();
            }
        }
    }
}