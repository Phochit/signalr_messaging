using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitter.Models;
using Hitter.DBML;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Hitter.Controllers
{
    public class ChatController
    {
        public List<DBML.Customer> SelectAllCustomer(string curCustomer)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                return db.Customers.Where(u => u.name != curCustomer).ToList();

            }
        }

        public Models.Customer selectcustomer(string cusName)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data= db.Customers.Where(u => u.name == cusName).FirstOrDefault();

                Models.Customer mycus = new Models.Customer();
                mycus.id = data.id;
                mycus.name = data.name;
                mycus.created_at = data.created_at;

                return mycus;

            }
        }

        public List<DBML.Conversation> ChatwithContact(int contact, Models.Customer currentUser)
        {
            var conversations = new List<Models.Conversation>();
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data = db.Conversations.
                                 Where(c => (c.receiver_id == currentUser.id && c.sender_id == contact) || (c.receiver_id == contact && c.sender_id == currentUser.id))
                                 .OrderBy(c => c.created_at)
                                 .ToList();

                return data;
            }

            
        }

        public int lastchatid(int contact, int currentUser)
        {
            //var conversations = new List<Models.Conversation>();
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data = db.Conversations.
                                 Where(c => (c.receiver_id == currentUser && c.sender_id == contact) || (c.receiver_id == contact && c.sender_id == currentUser))
                                 .OrderBy(c => c.created_at).ToArray().Last();

                return data.id;
            }


        }

        
        public string GetALLConversations(int contact, int currentUser)
        {
            var conversations = new List<Models.Conversation>();
            using (hitterDBDataContext db = new hitterDBDataContext())
            {

                var cmd = db.Conversations.
                                 Where(c => (c.receiver_id == currentUser && c.sender_id == contact) || (c.receiver_id == contact && c.sender_id == currentUser))
                                 .OrderBy(c => c.created_at)
                                 .ToList();

                
                
            

                return JsonConvert.SerializeObject(cmd);
            }
            

        }

        //protected void Application_End(Object sender, EventArgs e)
        //{
        //    // Stop SqlDependency notifications
        //    //SqlDependency.Stop(con);
        //}
        string con1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString;

        public List<Models.Conversation> brocastchat()
        {
                var messages = new List<Models.Conversation>();
            //var cmd = (from a in db.Conversations select a).ToList();
           
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["hitterConnectionString"].ConnectionString);
            SqlDependency.Start(con1);
            using (var mycmd = new SqlCommand(@"select * from Conversations", con))
                {
                    //var mycmd = (SqlCommand)db.GetCommand("select * from Conversations" as IQueryable);
                    SqlDataAdapter da = new SqlDataAdapter(mycmd);
                    var dependency = new SqlDependency(mycmd);
                    dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        messages.Add(item: new Models.Conversation
                        {
                            id = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                            sender_id = int.Parse(ds.Tables[0].Rows[i][1].ToString()),
                            receiver_id = Convert.ToInt32(ds.Tables[0].Rows[i][2]),
                            message = ds.Tables[0].Rows[i][3].ToString(),
                            status=0,
                            created_at =Convert.ToDateTime( ds.Tables[0].Rows[i][5].ToString())

                        });
                    }
                }
            //string formatstring = JsonConvert.SerializeObject(messages);
            return messages;
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table. 
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ChatLisHub.SendMessages();
            }

            //SqlDependency dependency = sender as SqlDependency;
            //if (dependency != null) dependency.OnChange -= Dependency_OnChange;
            ////Recall your SQLDependency setup method here.
            //SetupDependency();
        }


        public int SendMessageSave(Models.Conversation convo)
        {

            using(hitterDBDataContext db=new hitterDBDataContext ())
            {
                DBML.Conversation myconv = new DBML.Conversation();
                myconv.sender_id = convo.sender_id;
                myconv.receiver_id = convo.receiver_id;
                myconv.message = convo.message;
                myconv.status =(int) convo.status;
                myconv.created_at = convo.created_at;

                db.Conversations.InsertOnSubmit(myconv);
                db.SubmitChanges();


                //var dependency = new SqlDependency(db.SubmitChanges());
                //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                
            }

            return lastchatid(convo.receiver_id, convo.sender_id);
        }


    }
}