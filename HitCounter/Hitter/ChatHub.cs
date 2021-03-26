using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Hitter
{
    [HubName("chatMessage")]
    public class ChatHub : Hub
    {
        public void SendMessage(string user, string message)
        {
            Clients.All.Send(user, message);
        }
    }
}