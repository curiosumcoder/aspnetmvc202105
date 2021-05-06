using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Model
{
    /// <summary>
    /// Modelo de producto.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identificador del producto.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre completo del producto.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Precio con decimales.
        /// </summary>
        public decimal Price { get; set; }

        public Product()
        {
        }

        public Product(int id)
        {
            this.Id = id;
        }
    }
}
