using Northwind.Store.Model;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1.Helpers.HTML
{
    public static class ProductHelper
    {
        public static HtmlString ProductDetail(Product p)
        {
            HtmlString result = new HtmlString("");

            if (p != null)
            {
                result = new HtmlString($@"<tr>
                    <td>{p.ProductName}</td>
                    <td>{p.QuantityPerUnit}</td>
                    <td>{p.UnitPrice}</td>
                    <td>
                        <a href=""/Home/Details/{p.ProductID}"">Details</a>
                    </td></tr>");
            }

            return result;
        }

        public static HtmlString ProductDetail2(Product p)
        {
            HtmlString result = new HtmlString("");

            if (p != null)
            {
                TagBuilder tb = new TagBuilder("tr");

                TagBuilder tbProductName = new TagBuilder("td");
                tbProductName.InnerHtml = p.ProductName;
                tb.InnerHtml += tbProductName;

                TagBuilder tbQuantityUnit = new TagBuilder("td");
                tbQuantityUnit.InnerHtml = p.QuantityPerUnit;
                tb.InnerHtml += tbQuantityUnit;

                TagBuilder tbUnitPrice = new TagBuilder("td");
                tbUnitPrice.InnerHtml = p.UnitPrice.ToString();
                tb.InnerHtml += tbUnitPrice;

                TagBuilder td = new TagBuilder("td");

                TagBuilder tbLink = new TagBuilder("a");
                tbLink.MergeAttribute("href", $"/Home/Details/{p.ProductID}");
                tbLink.InnerHtml = "Details";

                td.InnerHtml += tbLink;

                tb.InnerHtml += td;

                result = new HtmlString(tb.ToString());
            }

            return result;
        }
    }
}
