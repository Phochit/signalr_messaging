using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Hitter.Models;
using Hitter.Controllers;
using Newtonsoft.Json;
using System.Web.Services;

namespace Hitter
{
    public partial class Chat : System.Web.UI.Page
    {
        public static int hvid;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlControl divControl = this.Master.FindControl("logout") as HtmlControl;
            if (Session["customer"] != null)
            {
                
                divControl.Visible = true;
            }
            else
            {
                divControl.Visible = false;
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ruser"]))
                {
                    int i = Convert.ToInt32(Request.QueryString["ruser"].ToString());
                    Session["chatterid"] = i;
                    GetConversationsList(i, Convert.ToInt32(Session["myid"].ToString()));

                    //hvid = Convert.ToInt32(hv.Value);
                    // purechat();
                }
            }
            else
            {
                //vid = Convert.ToInt32( hv.Value);
            }
        }

        public List<V2_Conversation> GetConversationsList(int receiver, int cus)
        {
            List<V2_Conversation> lst = new List<V2_Conversation>();
            V2Controller con = new V2Controller();
            lst = con.OurMessage(receiver, cus);
             
            return lst;
        }

        [WebMethod]
        public string GetChatAllMessage()
        {
            ChatController con = new ChatController();
            if (Session["myid"] == null && !string.IsNullOrEmpty(Request.QueryString["ruser"]))
            {
                int i = Convert.ToInt32(Request.QueryString["ruser"]);
                int j = Convert.ToInt32(Session["myid"]);
                var data = con.GetALLConversations(i, j);

                return JsonConvert.SerializeObject(data);
            }
            else
                return "";
            
        }

        //public List<Conversation> purechat()
        //{
        //    return lst;
        //}


        public int CustomerCount()
        {
            ChatController con = new ChatController();
            var data = con.SelectAllCustomer(Session["customer"].ToString());
            return data.Count;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> ls = new List<Customer>();
            V2Controller con = new V2Controller();
           return  ls = con.SelectOtherCustomer(Convert.ToInt32(Session["myid"].ToString()));
            
        }

        protected void sendMessage_ServerClick(object sender, EventArgs e)
        {
            //if ( Session["aid"]!=null )
            //{
                //int abc = int.Parse(Session["aid"].ToString());
                int i = Convert.ToInt32(txtChatID.Text.Trim());
                if (!string.IsNullOrEmpty(msg_box.Value))
                {
                    string msg = msg_box.Value.ToString();
                    V2_Conversation conv = new V2_Conversation()
                    {
                        sender_id = Convert.ToInt32(Session["myid"]),
                        receiver_id = i,
                        message = msg,
                        status = 0,
                        created_at = DateTime.UtcNow.AddMinutes(390)
                    };
                    V2Controller con = new V2Controller();
                    con.InsertV2Convo(conv);

                    msg_box.Value = "";

                }

            //}

        }
    }
}