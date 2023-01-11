using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntitiy
    {

        #region property
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;
        #endregion

        #region Constructor
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }
        #endregion

        public async Task<int> DeleteAsync(T entity)
        {
           
            entity.IsDeleted = true;
            entities.Update(entity);
            int result = await _applicationDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<T?> Get(int Id)
        {
            return await entities
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == Id && !c.IsDeleted);
        }

        public async Task<List<T>> GetAll()
        {
            return await entities
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<T?> InsertAsync(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entities.Add(entity);
            int result = await _applicationDbContext.SaveChangesAsync();
            if (result > 0)
                return entity;
            return null;
        }

        public async Task<bool> HardDeleteAsync(T entity)
        {
            var isItemFound = entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if(isItemFound == null)
                return true;
            entities.Remove(entity);
            int result = await _applicationDbContext.SaveChangesAsync();
            return result >0;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _applicationDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            entities.Update(entity);
            
            var result = await _applicationDbContext.SaveChangesAsync();
            if (result > 0)
                return entity;
            return null;
        }


        public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> func)
        {
            return await entities.FirstOrDefaultAsync<T>(func, cancellationToken: CancellationToken.None);
        }

        public async Task<List<T>?> InsertBulkAsync(List<T> entitiesToInsert)
        {
           entities.AddRangeAsync(entitiesToInsert);
            var result = await SaveChangesAsync();
            if(result >0)
                return entitiesToInsert;
            return null;
        }
    }
}
