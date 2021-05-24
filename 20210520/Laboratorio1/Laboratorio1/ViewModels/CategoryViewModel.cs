using Northwind.Store.Model;
using System.Collections.Generic;

namespace Laboratorio1.ViewModels
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}