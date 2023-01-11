using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.RepositoryPattern
{
    public interface IRepository<T> where T : BaseEntitiy
    {
        Task<List<T>> GetAll();
        Task<T?> Get(int id);
        Task<T?> InsertAsync(T entity);
        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> func);
        Task<T?> UpdateAsync(T entity);
        Task<List<T>?> InsertBulkAsync(List<T> entitiesToInsert);
        Task<int> DeleteAsync(T entity);
        Task<bool> HardDeleteAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
