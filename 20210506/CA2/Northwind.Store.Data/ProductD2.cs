using Northwind.Store.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public partial class ProductD
    {
        public Product Get(int id)
        {
            var products = List();

            return products.Single(p => p.Id == id);
        }
    }
}
