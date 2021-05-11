using NW.Data;
using NW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW.UI.CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                var pr = db.Products.Single(p => p.ProductID == 1);
                Console.WriteLine($"{pr.UnitPrice} - {pr.ProductName}");

                // Change Tracking
                pr.ProductName += " M";
                pr.UnitPrice += 1;

                var pNuevo = new Product() { ProductName = "Sandías" };
                pNuevo.UnitPrice = 777;
                db.Products.Add(pNuevo);

                var pEliminar = db.Products.Single(p => p.ProductID == 81);
                db.Products.Remove(pEliminar);

                db.SaveChanges();

                var pEliminar2 = db.Products.Where(p => p.UnitPrice > 1000);
                foreach (var pr2 in pEliminar2)
                {
                    db.Products.Remove(pr2);
                }

                var pr3 = db.Products.Single(p => p.ProductID == 1 && p.ProductName == "ABC");

                var hayChayotes = db.Products.Any(p => p.ProductName == "Chayote");
                if (hayChayotes)
                {

                }
            }

            Console.ReadLine();
        }
    }
}
