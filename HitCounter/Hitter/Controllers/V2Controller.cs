using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitter.Models;
using Hitter.DBML;

namespace Hitter.Controllers
{
    public class V2Controller
    {
        public void InsertV2Convo(Models.V2_Conversation v2con)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                DBML.V2_Conversation myconv = new DBML.V2_Conversation();
                myconv.sender_id = v2con.sender_id;
                myconv.receiver_id = v2con.receiver_id;
                myconv.message = v2con.message;
                myconv.status = v2con.status;
                myconv.created_at = v2con.created_at;

                db.V2_Conversations.InsertOnSubmit(myconv);
                db.SubmitChanges();

                //var dependency = new SqlDependency(db.SubmitChanges());
                //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            }
        }

        public List<Models.Customer> SelectOtherCustomer(int id)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data =db.Customers.Where(u => u.id != id).ToList();
                List<Models.Customer> lst = new List<Models.Customer>();
                if (data.Count > 0)
                {
                   
                    foreach (var obj in data)
                    {
                        Models.Customer cu = new Models.Customer();
                        cu.id = obj.id;
                        cu.name = obj.name;
                        cu.created_at = obj.created_at;

                        lst.Add(cu);
                    }

                }
                return lst;
                
            }
        }

        public List<Models.V2_Conversation> OurMessage(int me,int other)
        {
            var conversations = new List<Models.V2_Conversation>();
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data = db.V2_Conversations.
                                 Where(c => (c.receiver_id == me && c.sender_id == other) || (c.receiver_id == other && c.sender_id == me))
                                 .OrderBy(c => c.created_at)
                                 .ToList();

                foreach (var obj in data)
                {
                    Models.V2_Conversation conv = new Models.V2_Conversation();
                    conv.id = obj.id;
                    conv.sender_id = obj.sender_id;
                    conv.receiver_id = obj.receiver_id;
                    conv.message = obj.message;
                    conv.status = obj.status;
                    conv.created_at = obj.created_at;

                    conversations.Add(conv);
                }

                return conversations;
            }
        }
    }
}