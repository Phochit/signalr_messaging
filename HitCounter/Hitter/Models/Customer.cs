using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter.Models
{
    public class Customer
    {
        public Customer()
        {
            this.created_at = DateTime.UtcNow;
        }

        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
    }
}