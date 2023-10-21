using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Domain.Repositories.Base
{
    public interface IRepository<T> where T : class
    {

        /// Get all items of an entity by asynchronous method
        Task<IList<T>> GetAllAsync();

        IEnumerable<T> GetAll();
        /// Fin one item of an entity synchronous method
        //T Find(params object[] keyValues);


        /// Find one item of an entity by asynchronous method
        //Task FindAsync(params object[] keyValues);

        /// Find one item of an entity by asynchronous method
        Task<T> FindAsync(int entityId);

        /// Insert item into an entity by asynchronous method
        Task<int> InsertAsync(T entity, bool saveChanges = true);


        /// Insert multiple items into an entity by asynchronous method
        Task<int> InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true);

        /// Update one item from an entity by asynchronous method
        Task<int> UpdateAsync(T entity, bool saveChanges = true);
        /// Remove one item from an entity by asynchronous method
        Task<int> DeleteAsync(T entity, bool softDelete, bool saveChanges = true);

        /// Remove multiple items from an entity by asynchronous method
        Task DeleteRangeAsync(IEnumerable<T> entities, bool softDelete, bool saveChanges = true);
        Task<int> DeleteAsyncById(int id, bool softDelete = false, bool saveChanges = true);
    }
}
