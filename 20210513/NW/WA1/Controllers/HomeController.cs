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
        /// <summary>
        /// GET Home/Index
        /// GET Home
        /// GET /
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ActionResult Index(string filtro = "")
        {
            List<Product> products = null;

            using (var db = new NorthwindContext())
            {
                products = db.Products.Where(p => p.ProductName.Contains(filtro)).ToList();
            }

            ViewBag.datos = "";
            ViewData["datos"] = "";

            return View(products);
        }
    
        /// <summary>
        /// GET Home/Index/123
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index2(int? id)
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET About
        /// </summary>
        /// <returns></returns>
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