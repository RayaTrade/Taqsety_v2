using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.LookupRepository
{
    public interface ILookupRepository<T> where T : BaseEntityWithDualLang
    {
        Task<List<T>> GetAll();
        //Task<T?> Get(int id);
        //Task<T?> InsertAsync(T entity);
        //Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> func);
        //Task<T?> UpdateAsync(T entity);
        //Task<int> DeleteAsync(T entity);
        //Task<int> HardDeleteAsync(T entity);
        //Task<int> SaveChangesAsync();
    }
}
