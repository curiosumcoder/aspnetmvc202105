using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Northwind.Store.Model
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public class ProductMetadata
        {
            [Display(Name = "Nombre de Producto")]
            [Required(ErrorMessage = "El {0} es requerido.")]
            public string ProductName { get; set; }

            [Required(ErrorMessage = "El ID de proveedor es requerido.")]
            public Nullable<int> SupplierID { get; set; }

            [Required(ErrorMessage = "El ID de categoría es requerido.")]
            public Nullable<int> CategoryID { get; set; }

            [JsonIgnore]
            public virtual Category Category { get; set; }

            [JsonIgnore]
            public virtual ICollection<OrderDetail> OrderDetails { get; set; }

            [JsonIgnore]
            public virtual Supplier Supplier { get; set; }
        }
    }
}
