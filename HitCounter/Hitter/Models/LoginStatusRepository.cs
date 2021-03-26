using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hitter.Models
{

    public class LoginStatusRepository
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString);
        public List<LoginStatus> GetReceiveMsg(int myid)
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

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table. 
        {
            if (e.Type == SqlNotificationType.Change)
            {
                LoginStatusHub.SendMessages();
            }
        }
    }
}