using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Store.Model;
using System.Linq.Dynamic.Core;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{

    /// <summary>    
    /// Requiere el paquete de Nuget: System.Linq.Dynamic.Core
    /// </summary>
    public class CategoryD : IDisposable, IMaintenanceD<Category,int>
    {
        readonly NorthwindContext db = new NorthwindContext();

        public void Create(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public Category Read(int key)
        {
            return db.Categories.Find(key);
        }

        public async Task<List<Category>> ReadAsync(PageFilter pf = null)
        {
            List<Category> result = new List<Category>();

            if (pf != null)
            {
                pf.Count = await db.Categories.CountAsync();
                result = await db.Categories.OrderBy(pf.Sorting).Skip(--pf.Page * pf.PageSize). Take(pf.PageSize).ToListAsync();
            }
            else
            {
                result = await db.Categories.ToListAsync();
            }

            return result;
        }

        public void Update(Category c)
        {
            db.Categories.Attach(c);
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int key)
        {
            Category category = db.Categories.Find(key);
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
