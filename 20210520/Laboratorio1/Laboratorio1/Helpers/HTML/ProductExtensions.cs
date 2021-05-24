using Northwind.Store.Model;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1.Helpers.HTML
{
    public static class ProductExtensions
    {
        public static HtmlString ProductDetail(this HtmlHelper helper, Product p)
        {
            return ProductHelper.ProductDetail2(p);
        }
    }
}
