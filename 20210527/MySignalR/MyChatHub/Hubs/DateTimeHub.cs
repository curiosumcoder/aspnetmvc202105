using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MyChatHub.Hubs
{
    public class DateTimeHub : Hub
    {
        DateTimeClock dtc = DateTimeClock.Instance;
    }
}