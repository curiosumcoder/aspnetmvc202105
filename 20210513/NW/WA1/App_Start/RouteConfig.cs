using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WA1
{
    public class RouteConfig
    {
        /// <summary>
        /// mep.go.cr/estudiantes/10123046/calificaciones
        /// mep.go.cr/estudiantes/10123046/expediente
        /// /
        /// {controller}/{action}/{id}
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
