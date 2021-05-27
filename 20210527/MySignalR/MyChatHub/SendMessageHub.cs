using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace MyChatHub
{
    public class SendMessageHub : Hub
    {
        public void Send(Message m)
        {
            Clients.All.received(m);
        }
    }

    public class Message
    {
        [JsonProperty("left")]
        public int priority { get; set; }
        public string message { get; set; }
    }
}