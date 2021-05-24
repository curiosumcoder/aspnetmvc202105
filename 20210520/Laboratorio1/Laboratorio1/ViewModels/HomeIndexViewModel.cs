using System.Collections.Generic;

namespace Laboratorio1.ViewModels
{
    public class HomeIndexViewModel
    {
        public string Filter { get; set; } = "";
        public List<CategoryViewModel> Items { get; set; }
    }
}