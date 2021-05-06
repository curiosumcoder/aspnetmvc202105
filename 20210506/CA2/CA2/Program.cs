using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Store.Data;
using Northwind.Store.Model;

namespace CA2
{
    class Program
    {
        static void Main(string[] args)
        {
            var pD = new ProductD();
            var products = pD.List();

            foreach (var p in products)
            {
                string s = string.Format("{0}, {1}, {2}", p.Id, p.Name, p.Price);
                Console.WriteLine(s);
                Console.WriteLine("{0}, {1}, {2}", p.Id, p.Name, p.Price);
                Console.WriteLine($"{p.Id}, {p.Name}, {p.Price}");
            }

            var product = pD.Get(5);
            Console.WriteLine($"\n\n{product.Id}, {product.Name}, {product.Price}");
        }
    }
}
