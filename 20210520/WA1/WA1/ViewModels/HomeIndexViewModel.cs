using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WA1.Models;

namespace WA1.ViewModels
{
    public class HomeIndexViewModel
    {
        public string Filtro { get; set; }
        public List<Product> Productos { get; set; }
    }
}