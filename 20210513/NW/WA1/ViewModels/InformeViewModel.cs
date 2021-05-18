using NW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WA1.ViewModels
{
    public class InformeViewModel
    {
        public Customer Cliente { get; set; }
        public List<Order> Ordenes { get; set; }
    }
}