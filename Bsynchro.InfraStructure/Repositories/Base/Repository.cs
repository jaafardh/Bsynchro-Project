using Bsynchro.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.InfraStructure.Repositories.Base
{
    public class Repository<T, U> : IRepository<T> where T : class where U : DbContext
    {
        //protected U db { get; private set; }
        protected readonly U db;
        protected DbSet<T> Entities => db.Set<T>();

        //DbSet<T> IRepository<T>.Entities => Entities;
        //DbContext IRepository<T>.db => db;


        protected readonly ILogger _logger;

        public Repository(U dbContext, ILogger logger)
        {
            db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// This will affect Submit Changes for ALL Scope DB Context
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("DB Transaction Failed, " + ex.Message);
                throw new Exception("DB Transaction Failed, " + ex.Message);
            }
        }


        public async Task<int> DeleteAsync(T entity, bool softDelete = false, bool saveChanges = true)
        {
            if (!softDelete)
                Entities.Remove(entity);
            else
            {
                var IsDeleted = entity.GetType().GetProperty("IsDeleted");
                if (IsDeleted != null) IsDeleted.SetValue(entity, true);

                var DeletedDate = entity.GetType().GetProperty("DeletedDate");
                if (DeletedDate != null) DeletedDate.SetValue(entity, DateTime.UtcNow);
            }

            if (saveChanges)
            {
                return await db.SaveChangesAsync();
            }
            return -1;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool softDelete = false, bool saveChanges = true)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                if (!softDelete)
                    Entities.RemoveRange(enumerable);
                else
                {
                    foreach (var entity in enumerable)
                        await DeleteAsync(entity, true, false);
                }

            }
            if (saveChanges)
            {
                await db.SaveChangesAsync();
            }
        }
        public async Task<int> DeleteAsyncById(int entityId, bool softDelete = false, bool saveChanges = true)
        {
            T entity = await FindAsync(entityId);
            if (entity != null)
            {
                if (!softDelete)
                    Entities.Remove(entity);
                else
                {
                    var IsDeleted = entity.GetType().GetProperty("IsDeleted");
                    if (IsDeleted != null) IsDeleted.SetValue(entity, true);

                    var DeletedDate = entity.GetType().GetProperty("DeletedDate");
                    if (DeletedDate != null) DeletedDate.SetValue(entity, DateTime.UtcNow);
                }

                if (saveChanges)
                {
                    return await db.SaveChangesAsync();
                }
            }
            return -1;
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.AsNoTracking().AsEnumerable();
        }
        public async Task<IList<T>> GetAllAsync()
        {
            var x = await Entities.AsNoTracking().ToListAsync();
            return x;
        }
        //public T Find(params object[] keyValues)
        //{
        //    return Entities.Find(keyValues);
        //}

        //public virtual async Task<T> FindAsync(params object[] keyValues)
        //{
        //    return await Entities.FindAsync(keyValues);
        //}
        public async Task<T> FindAsync(int entityId)
        {
            return await Entities.FindAsync(entityId);
        }
        public async Task<int> InsertAsync(T entity, bool saveChanges = true)
        {
            db.ChangeTracker.Clear();
            await Entities.AddAsync(entity);
            if (saveChanges)
            {
                return await db.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            await db.AddRangeAsync(entities);
            if (saveChanges)
            {
                return await db.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> UpdateAsync(T entity, bool saveChanges = true)
        {
            db.ChangeTracker.Clear();
            Entities.Update(entity);
            if (saveChanges)
            {
                return await db.SaveChangesAsync();
            }
            return -1;
        }
    }
}
