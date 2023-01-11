using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.InstallmentCardRepository;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.InstallmentCardService
{
    public interface IInstallmentCardService :IInstallmentCardRepository
    {
        //Task<InstallmentCard?> GetInstallmentCardByCustomerId(int customerId);
        Task<ResultModel> CreateInstallmentCard(InstallmentCardDto installmentCardDto);
        Task<InstallmentCardDto?> GetInstallmentCardByMobileNumber(string mobileNumber,string lang);
        Task<ResultModel> UpdateCustomerName(int installmentCardId, string newCustomerFullName);
        bool IsValidNationalId(long nationalIdToValidate);
        Task<InstallmentCardDto?> GetInstallmentCardByNationalId(long nationalId, string lang);
        Task<ResultModel> UpdateCustomerMaritialStatus(int v, int newmaritialStatus);
        Task<ResultModel> GetAllInstallmentCards();
        Task<CardDTO> UpdateInstallmentCard(CardDTO installmentCardDto);
        Task<List<CardDTO>> GetAllCards(int roleId,int userId);
        Task<InstallmentCard> UpdateInstallmentStatus(int installmentCardId, int newStatusId,int userId);
    }
}
