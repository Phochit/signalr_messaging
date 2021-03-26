using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter.Models
{
    public class LoginStatus
    {
        public int id { get; set; }

        public int loginid { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int loginstatus { get; set; }
    }
}