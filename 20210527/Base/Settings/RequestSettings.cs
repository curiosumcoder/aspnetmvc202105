using System.Web.Mvc;

namespace Northwind.Store.UI.Web.Internet.Settings
{
    public class RequestSettings
    {
        Controller controller = null;
        public RequestSettings(Controller c)
        {
            controller = c;
        }

        public string Message
        {
            get
            {
                return (string)controller.TempData[nameof(Message)]; ;
            }
            set
            {
                controller.TempData[nameof(Message)] = value;
            }
        } 
    }
}