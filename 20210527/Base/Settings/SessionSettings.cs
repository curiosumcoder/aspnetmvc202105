using Northwind.Store.Model;
using System.Collections.Generic;
using System.Web;

namespace Northwind.Store.UI.Web.Internet.Settings
{
    public class SessionSettings
    {
        public static string Message
        {
            get
            {
                return (string)HttpContext.Current.Session[nameof(Message)];
            }
            set
            {
                HttpContext.Current.Session[nameof(Message)] = value;
            }
        }

        public static List<Product> Cart
        {
            get
            {
                if (HttpContext.Current.Session[nameof(Cart)] == null)
                {
                    HttpContext.Current.Session[nameof(Cart)] = new List<Product>();
                }

                return (List<Product>)HttpContext.Current.Session[nameof(Cart)];
            }
            set
            {
                HttpContext.Current.Session[nameof(Cart)] = value;
            }
        }
    }
}