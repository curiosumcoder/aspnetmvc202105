using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WA1.Models;
using WA1.ViewModels;

namespace WA1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HomeIndexViewModel vm)
        {           
            using (var db = new NorthwindContext())
            {
                vm.Productos = db.Products.Where(p => p.ProductName.Contains(vm.Filtro)).ToList();
            }

            return View(vm);
        }

        public ActionResult IndexOLD(string filtro = "")
        {
            List<Product> ps = new List<Product>();

            using (var db = new NorthwindContext())
            {
                ps = db.Products.Where(p => p.ProductName.Contains(filtro)).ToList();
            }

            ViewBag.filtro = filtro;
            ViewData["products"] = ps;
            ViewBag.products = ps;

            //return View();
            return View(ps);
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