using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hitter.DBML;

namespace Hitter.Controllers
{
    public class PushController : Controller
    {
        // GET: Push
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMessages()
        {
            List<Models.Conversation> messages = new List<Models.Conversation>();
            ChatController r = new ChatController();
            messages = r.brocastchat();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}