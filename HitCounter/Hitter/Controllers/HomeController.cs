using Hitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetMessages()
        {
            List<Employee> messages = new List<Employee>();
            Repository r = new Repository();
            messages = r.GetAllMessages();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        public void SaveEmployee(Employee emp)
        {
            EmployeeController con = new EmployeeController();
            con.InsertEmployee(emp);
        }

        public JsonResult GetReceiveMessage()
        {
            List<V2_Conversation> messages = new List<V2_Conversation>();
            V2Repository r = new V2Repository();
            if (Session["myid"] != null && Session["chatterid"]!=null)
            {
                messages = r.GetReceiveMsg(Convert.ToInt32(Session["myid"].ToString()),Convert.ToInt32(Session["chatterid"].ToString()));
                return Json(messages, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetLoginStatus()
        {
            List<LoginStatus> messages = new List<LoginStatus>();
            LoginStatusRepository r = new LoginStatusRepository();
            if (Session["myid"] != null)
            {
                messages = r.GetReceiveMsg(Convert.ToInt32(Session["myid"].ToString()));
                return Json(messages, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAuthCreate()
        {
            List<Customer> messages = new List<Customer>();
            AuthCreateRepository r = new AuthCreateRepository();
           
                messages = r.GetReceiveMsg(Convert.ToInt32(Session["myid"].ToString()));
                return Json(messages, JsonRequestBehavior.AllowGet);
            
        }

    }
}