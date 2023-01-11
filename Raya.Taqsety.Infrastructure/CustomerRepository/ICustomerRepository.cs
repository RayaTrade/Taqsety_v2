using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.CustomerRepository
{
    public interface ICustomerRepository :IRepository<Customer>
    {
        Task<bool> CheckIfDuplicatedNationaId(int nationalId);
        Task<bool> CheckIfDuplicatedMobileNumber(string mobileNumber);
        Task<bool> CheckIfBlockedCustomer(string CustomerName);
        Task<bool> CheckIflockedNationalId(string NationalId);
        Task<Customer?> GetCustomerByMobileNumber(string mobileNumber);
        Task<Customer?> GetCustomerByNationalId(long nationalId);
        Task<List<Club>> GetAllCustomerClubs(int cutomerId);
        Task<Grantor?> GetGuarntorById(int guarntorId);
    }
}
