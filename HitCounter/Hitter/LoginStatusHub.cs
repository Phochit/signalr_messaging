using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter
{
    [HubName("loginStatusHub")]
    public class LoginStatusHub : Hub
    {
        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<LoginStatusHub>();
            context.Clients.All.updateMessages();
        }
    }
}