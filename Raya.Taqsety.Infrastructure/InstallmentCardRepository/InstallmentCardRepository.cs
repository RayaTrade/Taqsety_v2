using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.InstallmentCardRepository
{
    public class InstallmentCardRepository :  Repository<InstallmentCard>,IInstallmentCardRepository
    {
        private readonly ApplicationDbContext _context;
        public InstallmentCardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<InstallmentCard>> GetAllCards(List<int> statusIds,int userId)
        {
            return await _context.InstallmentCards
                 .Include(x => x.Status)
                 .Include(x => x.Grantor).ThenInclude(x => x.Relation)
                 .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.Governrate)
                 .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.City)
                 .Include(x => x.Customer).ThenInclude(x => x.JobDetails)
                 .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.Governrate)
                 .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.City)
                 .Where(x=> statusIds.Contains((int)x.StatusId) && x.ModifiedBy != userId) 
                 .ToListAsync();
        }

        public async Task<InstallmentCard?> GetInstallmentById(int installmentCardId)
        {
            return await _context.InstallmentCards
                 .Include(x=>x.Customer).ThenInclude(x=>x.JobDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == installmentCardId && !x.IsDeleted);
        }

        public async Task<InstallmentCard?> GetInstallmentByMobileNumber(string mobileNumber)
        {
            return await _context.InstallmentCards
                 .Include(x=>x.Status)
                 .Include(x => x.Grantor).ThenInclude(x=>x.Relation)
                 .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x=>x.Governrate)
                 .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x=>x.City)
                 .Include(x => x.Customer).ThenInclude(x => x.JobDetails)
                 .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x=>x.Governrate)
                 .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x=>x.City)
                 .FirstOrDefaultAsync(x => x.Customer.MobileNumber == mobileNumber && !x.IsDeleted);
        }

        public async Task<InstallmentCard?> GetInstallmentByNationalId(long nationalId)
        {
            return await _context.InstallmentCards
                .Include(x => x.Status)
                .Include(x => x.Grantor).ThenInclude(x => x.Relation)
                .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.Governrate)
                .Include(x => x.Grantor).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.City)
                .Include(x => x.Customer).ThenInclude(x => x.JobDetails)
                .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.Governrate)
                .Include(x => x.Customer).ThenInclude(x => x.AddressDetails).ThenInclude(x => x.City)
                .FirstOrDefaultAsync(x => x.Customer.NationalId == nationalId && !x.IsDeleted);
        }

        public async Task<InstallmentCard?> GetInstallmentCardByCustomerId(int customerId)
        {
            return await _context.InstallmentCards
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && !x.IsDeleted);
        }
    }
}
