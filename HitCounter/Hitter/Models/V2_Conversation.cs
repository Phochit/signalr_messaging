using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter.Models
{
    public class V2_Conversation
    {
        public int id { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
    }
}