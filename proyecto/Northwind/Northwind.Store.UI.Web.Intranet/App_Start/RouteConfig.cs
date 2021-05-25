using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace Northwind.Store.UI.Web.Intranet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Agregar para al soporte de múltiples Áreas
            // using System.Web.Compilation;
            // namespaces: new[] { $"{BuildManager.GetGlobalAsaxType().BaseType.Assembly.GetName().Name}.Controllers" }
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { $"{BuildManager.GetGlobalAsaxType().BaseType.Assembly.GetName().Name}.Controllers" }
            );
        }
    }
}
