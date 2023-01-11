using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.InstallmentCardRepository
{
    public interface IInstallmentCardRepository :IRepository<InstallmentCard>
    {
        Task<List<InstallmentCard>> GetAllCards(List<int> StatusIds,int userId);
        Task<InstallmentCard?> GetInstallmentById(int installmentCardId);
        Task<InstallmentCard?> GetInstallmentByMobileNumber(string mobileNumber);
        Task<InstallmentCard?> GetInstallmentByNationalId(long nationalId);
        Task<InstallmentCard?> GetInstallmentCardByCustomerId(int customerId);
    }
}
