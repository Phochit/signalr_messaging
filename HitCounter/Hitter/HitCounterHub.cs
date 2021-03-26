using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Hitter
{
    [HubName("hitCounter")]
    public class HitCounterHub : Hub
    {
        static int _hitcount = 0;

        public void RecordHit()
        {
            _hitcount += 1;
            Clients.All.onRecordHit(_hitcount);
        }
    }
}