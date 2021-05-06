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
        /// <summary>
        /// Retorna la lista completa de products.
        /// </summary>
        /// <returns>Lista genérica (Tipos genéricos)</returns>
        public List<Product> List()
        {
            //int []  a  = { 1, 2, 3};
            //ArrayList a1 = new ArrayList();
            //a1.Add()
            //var val1 = a1[0];

            // Lista genérica
            // Inicializador de colección
            List<Product> result = new List<Product>() {
                new Product() { Id = 4, Name = "Libro 4",Price = 400},
                new Product() { Id = 5, Name = "Libro 5", Price = 500}
            };

            Product p1 = new Product();
            p1.Id = 1;
            p1.Name = "Libro 1";
            p1.Price = 100;

            result.Add(p1);

            // Inferencia de tipos
            // Inicialidor de objetos
            var p2 = new Product()
            {
                Id = 2,
                Name = "Libro 2",
                Price = 220
            };
            result.Add(p2);

            result.Add(new Product()
            {
                Id = 3,
                Name = "Libro 3",
                Price = 300
            });

            return result;
        }
    }
}
