using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.CustomerRepository;
using Raya.Taqsety.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.CustomerService
{
    public interface ICustomerService :ICustomerRepository
    {
        Task<bool> CheckIfNationalIdUsedBefore(int nationalId);
        Task<bool> CheckIfBlockedNationalId(string nationalId);
        Task<bool> CheckIfBlockedCustomerName(string CustomerName);
        Task<bool> CheckIfMobileNumberUsedBefore(string mobileNumber);
        Task<List<Club>> GetAllClubsByCustomerId(int cutomerId);
        Task<GuarntorDTO?> GetGuarntorData(int guarntorId);
        Task<GuarntorDTO?> UpdateGurantorData(GuarntorDTO guarntorDTO);
    }
}
