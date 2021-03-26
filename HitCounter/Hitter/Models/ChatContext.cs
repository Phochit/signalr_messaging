using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Hitter.Models
{
    public class ChatContext:DbContext
    {
        public ChatContext() : base("hitterConnectionString")
        {
        }

        public static ChatContext Create()
        {
            return new ChatContext();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<V2_Conversation> v2_Conversations{ get; set; }
        public DbSet<LoginStatus> LoginStatuses { get; set; }
    }
}