using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Hitter
{
    [HubName("chatListMessage")]
    public class ChatLisHub : Hub
    {
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatLisHub>();
            context.Clients.All.updateMessages();
        }
    }
}