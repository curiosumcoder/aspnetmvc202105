using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Northwind.Store.UI.Web.Intranet.Startup))]
namespace Northwind.Store.UI.Web.Intranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
