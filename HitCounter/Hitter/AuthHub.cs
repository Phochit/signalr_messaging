using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter
{
    [HubName("authHub")]
    public class AuthHub : Hub
    {
        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AuthHub>();
            context.Clients.All.updateMessages();
        }
    }
}