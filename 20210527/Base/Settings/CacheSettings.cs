using System;
using System.Web;
using System.Web.Caching;

namespace Northwind.Store.UI.Web.Internet.Settings
{
    public class CacheSettings
    {
        public static string Offer
        {
            get { return (string)HttpContext.Current.Cache[nameof(Offer)]; }
            set
            {
                HttpContext.Current.Cache.Add(nameof(Offer),
                    value,
                    null,
                    Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, 30),
                    CacheItemPriority.Default,
                    new CacheItemRemovedCallback(RemovedCallback));

                System.Diagnostics.Debug.WriteLine($"Cache iniciado {DateTime.Now}");
            }
        }

        static void RemovedCallback(String k, Object v, 
            CacheItemRemovedReason r)
        {
            System.Diagnostics.Debug.WriteLine($"{k} {v} {r}");
            System.Diagnostics.Debug.WriteLine($"Cache expirado {DateTime.Now}");
        }
    }
}