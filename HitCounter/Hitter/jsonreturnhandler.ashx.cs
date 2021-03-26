using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter
{
    /// <summary>
    /// Summary description for jsonreturnhandler
    /// </summary>
    public class jsonreturnhandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}