using Northwind.Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public interface IMaintenanceD<T, K>
    {
        void Create(T m);
        T Read(K key);
        Task<List<T>> ReadAsync(PageFilter pf = null);
        void Update(T m);
        void Delete(K key);
    }
}
