using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Northwind.Store.UI.Web.Intranet.Custom
{
    public class CustomHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.context_BeginRequest);
            context.EndRequest += new EventHandler(this.context_EndRequest);
        }
        public void context_EndRequest(object sender, EventArgs e)
        {
            Debug.WriteLine($"End Request called at {DateTime.Now}");
        }
        public void context_BeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine($"Begin request called at {DateTime.Now}");
        }
        public void Dispose()
        {

        }
    }
}