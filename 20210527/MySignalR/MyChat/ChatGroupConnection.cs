using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace MyChat
{
    public class ChatGroupConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {            
            string grupo = request.QueryString["group"];
            var json = JsonConvert.SerializeObject(new GroupMessage() { message = "Bienvenido al chat en grupo " +  grupo});

            this.Groups.Add(connectionId, grupo);

            return Connection.Send(connectionId, json);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            GroupMessage gm = (GroupMessage)JsonConvert.DeserializeObject(data, typeof(GroupMessage));

            var json = JsonConvert.SerializeObject(gm);

            return this.Groups.Send(gm.groupName, json);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            string grupo = request.QueryString["group"];

            return this.Groups.Remove(connectionId, grupo);
        }

        protected override IList<string> OnRejoiningGroups(IRequest request, IList<string> groups, string connectionId)
        {
            return groups;
        }
    }

    public class GroupMessage
    {
        public string groupName { get; set; }
        public string message { get; set; }
    }
}