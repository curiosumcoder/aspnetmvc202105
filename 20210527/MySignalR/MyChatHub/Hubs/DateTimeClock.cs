using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MyChatHub.Hubs
{
    public class DateTimeClock
    {
        private readonly static Lazy<DateTimeClock> _instance = new Lazy<DateTimeClock>(
            () => new DateTimeClock(GlobalHost.ConnectionManager.GetHubContext<DateTimeHub>().Clients));
        public static DateTimeClock Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private readonly Timer _timer;

        private DateTimeClock(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            // Se dispara el timer comenzando despúes de 5 segundos, y a partir de allí cada 1 segundo
            _timer = new Timer(UpdateDateTime, null, TimeSpan.FromMilliseconds(5000), TimeSpan.FromMilliseconds(1000));
        }

        private void UpdateDateTime(object state)
        {
            // Aquí la lógica que se desea implementar cada vez que se efectúa la actualización
            DateTime dt = DateTime.Now;

            BroadcastDateTime(dt);
        }

        private void BroadcastDateTime(DateTime dt)
        {
            // update es el método que debe estar disponible en el 
            // código de JavaScript del cliente
            Clients.All.update(dt.ToString());
        }
    }
}