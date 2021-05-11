using NW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine(p.ProductName);
                }
            }

            Console.ReadLine();
        }
    }
}
