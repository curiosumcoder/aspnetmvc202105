using System;
using System.Web;

namespace Northwind.Store.UI.Web.Intranet.Custom
{
    public class CustomHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("<h1>MyHandler</h1>");
            context.Response.Write($"HttpHandler processed on {DateTime.Now}");
        }
    }
}