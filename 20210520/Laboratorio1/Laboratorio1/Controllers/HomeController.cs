using Laboratorio1.ViewModels;
using Northwind.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio1.Extensions;
using Northwind.Store.Model;

namespace Laboratorio1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HomeIndexViewModel vm)
        {

            using (var db = new NorthwindContext())
            {
                var q = from p in db.Products.Include("Category").ToList()
                        where p.ProductName.IndexOf(vm.Filter, StringComparison.InvariantCultureIgnoreCase) > -1
                        group p by p.Category?.CategoryName ?? "Sin Categoría" into g
                        select new CategoryViewModel()
                        {
                            CategoryName = g.Key,
                            Products = g.ToList()
                        };

                vm.Items = q.ToList();
            }

            return View(vm);
        }

        public ActionResult IndexPartial(int? id)
        {
            var isAjax = Request.IsAjaxRequest();

            if (id != null)
            {
                Product product = null;

                using (var db = new NorthwindContext())
                {
                    product = db.Products.Where(p => p.ProductID == id).SingleOrDefault();

                    if (product != null)
                    {
                        return PartialView("ProductPartial", product);
                    }
                }                
            }

            return HttpNotFound();
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