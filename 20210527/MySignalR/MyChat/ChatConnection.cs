using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace MyChat
{
    public class ChatConnection : PersistentConnection
    {
        private static int _conns = 0;

        protected override async Task OnConnected(IRequest request, string connectionId)
        {
            Interlocked.Increment(ref _conns);
            await Connection.Send(connectionId, "¡Bienvenido a MyChat! " + connectionId);
            await Connection.Broadcast("Ingresó " + connectionId + ". Visitantes: " + _conns);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = connectionId + ">> " + data;
            return Connection.Broadcast(message);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref _conns);
            return Connection.Broadcast(connectionId + " salió. Visitantes: " + _conns);
        }
    }
}