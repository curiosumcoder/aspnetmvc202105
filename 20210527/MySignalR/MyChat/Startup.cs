using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<ChatConnection>("/chat");
            app.MapSignalR<EchoConnection>("/echo");
            app.MapSignalR<ChatGroupConnection>("/groupchat");
        }
    }
}