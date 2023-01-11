using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.CityRepository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<City>> GetAllGouvernrateCities(int gouvernrateId)
        {
            return await _context.Cities
                .AsNoTracking()
                .Where(x => x.GovernrateId == gouvernrateId && !x.IsDeleted)
                .ToListAsync();
        }
    }
}
