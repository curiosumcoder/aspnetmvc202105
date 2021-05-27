using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MyChatHub
{
    public class EchoHub : Hub
    {
        public void Hello()
        {
            //Clients.All.hello();

            Groups.Add(Context.ConnectionId, "g1");
            Clients.Group("g1").hello();
        }
    }
}