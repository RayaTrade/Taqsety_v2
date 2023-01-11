using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.LookupRepository
{
    public class LookupRepository<T> : ILookupRepository<T> where T : BaseEntityWithDualLang
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        public LookupRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await entities.AsNoTracking().ToListAsync();
        }
    }
}
