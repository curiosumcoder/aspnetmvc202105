using System.Web;

namespace Northwind.Store.UI.Web.Internet.Settings
{
    public class ApplicationSettings
    {
        public static string WelcomeMessage
        {
            get
            {
                return (string)HttpContext.Current.Application[nameof(WelcomeMessage)];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[nameof(WelcomeMessage)] = value;
                HttpContext.Current.Application.UnLock();
            }
        }  
    }
}