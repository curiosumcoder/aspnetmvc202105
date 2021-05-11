using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Shell.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Data.NorthwindContext())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine(p.ProductName);
                }
            }
        }
    }
}
