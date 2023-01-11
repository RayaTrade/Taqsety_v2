using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.CustomerRepository
{
    public class CustomerRepository :Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly InstallmentContext _InstallmentContext;
        public CustomerRepository(
            ApplicationDbContext applicationDbContext,
            InstallmentContext installmentContext
            
            ):base(applicationDbContext)
        {
            _Context = applicationDbContext;
            _InstallmentContext = installmentContext;
        }

        public async Task<bool> CheckIfBlockedCustomer(string CustomerName)
        {
            return await _InstallmentContext.BlockedCustomers
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.CustFullName.ToLower()== CustomerName.ToLower()) != null;
        }

        public async Task<bool> CheckIfDuplicatedMobileNumber(string mobileNumber)
        {
            return await _Context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber && !x.IsDeleted) != null;
        }

        public async Task<bool> CheckIfDuplicatedNationaId(int nationalId)
        {
            return await _Context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NationalId == nationalId && !x.IsDeleted) != null;
        }

        public async Task<bool> CheckIflockedNationalId(string NationalId)
        {
            return await _InstallmentContext.BlockedCustomers
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.CustNationalId == NationalId) != null;
        }

        public Task<List<Club?>> GetAllCustomerClubs(int cutomerId)
        {
            return _Context.CustomersClubs.AsNoTracking()
                 .Include(x => x.Club)
                 .Where(x => x.CustomerId == cutomerId && !x.IsDeleted)
                 .Select(x => x.Club).ToListAsync();
        }

        public async Task<Customer?> GetCustomerByMobileNumber(string mobileNumber)
        {
            return await _Context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber && !x.IsDeleted);
        }

        public async Task<Customer?> GetCustomerByNationalId(long nationalId)
        {
            return await _Context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NationalId == nationalId && !x.IsDeleted);
        }

        public async Task<Grantor?> GetGuarntorById(int guarntorId)
        {
            return await _Context.InstallmentCards
                .AsNoTracking()
                .Include(x => x.Grantor)
                .ThenInclude(x => x.AddressDetails)
                .Select(x => x.Grantor)
                .FirstOrDefaultAsync(x => x.Id == guarntorId);
        }
    }
}
