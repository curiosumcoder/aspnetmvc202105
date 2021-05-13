using NW.Data;
using NW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WA1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string filtro = "")
        {
            List<Product> products = null;

            using (var db = new NorthwindContext())
            {
                products = db.Products.Where(p => p.ProductName.Contains(filtro)).ToList();
            }

            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}