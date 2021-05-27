using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Store.Model;
using System.IO;
using System.Linq.Dynamic.Core;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public class ProductD : IDisposable, IMaintenanceD<Product, int>
    {
        readonly NorthwindContext db = new NorthwindContext();

        public void Create(Product p)
        {
            db.Products.Add(p);
            db.SaveChanges();
        }

        public Product Read(int id)
        {
            return db.Products.Include("Category").Include("Supplier").SingleOrDefault(p => p.ProductID == id);
        }

        public Product Read(string name)
        {
            Product result = null;

            result = db.Products.Include("Category").Include("Supplier").
                Where(c => c.ProductName.IndexOf(name,
                    StringComparison.CurrentCultureIgnoreCase) > -1).
                    SingleOrDefault();

            return result;
        }

        public async Task<List<Product>> ReadAsync(PageFilter pf = null)
        {
            List<Product> result = new List<Product>();

            if (pf != null)
            {
                pf.Count = await db.Products.CountAsync();
                result = await db.Products.Include(p => p.Category).Include(p => p.Supplier).
                    OrderBy(pf.Sorting).
                    Skip(--pf.Page * pf.PageSize).
                    Take(pf.PageSize).ToListAsync();
            }
            else
            {
                result = await db.Products.ToListAsync();
            }

            return result;
        }

        public void Update(Product p)
        {
            db.Products.Attach(p);
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Database.ExecuteSqlCommand(
                "delete from products where productid = @ProductID",
                new System.Data.SqlClient.SqlParameter(
                    "@ProductID", System.Data.SqlDbType.Int) { Value = id });
        }

        public List<Product> Search(string filter)
        {
            List<Product> result = new List<Product>();

            result = db.Products.Include("Category").Include("Supplier").
                Where(c => c.ProductName.IndexOf(filter,
                    StringComparison.CurrentCultureIgnoreCase) > -1 && 
                    !string.IsNullOrEmpty(filter)).ToList();

            return result;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
