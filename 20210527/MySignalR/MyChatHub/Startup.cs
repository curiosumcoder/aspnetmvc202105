using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MyChatHub.Startup))]
namespace MyChatHub
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Map hubs to "/signalr" by default
            app.MapSignalR();

            //app.MapSignalR("/realtime", new HubConfiguration());
        }
    }
}