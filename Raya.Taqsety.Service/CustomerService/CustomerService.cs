using AutoMapper;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.AddressDetailsRepository;
using Raya.Taqsety.Infrastructure.CustomerRepository;
using Raya.Taqsety.Infrastructure.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.CustomerService
{
    public class CustomerService : CustomerRepository, ICustomerService  
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IAddressDetailsRepository _addressDetailsRepository;
        private readonly IRepository<Grantor> _grantorRepository;
        public CustomerService(ICustomerRepository customerRepository ,
            ApplicationDbContext applicationDbContext ,
            IMapper mapper,
            IAddressDetailsRepository addressDetailsRepository ,
            IRepository<Grantor> repository,
            InstallmentContext installmentContext)
            :base(applicationDbContext , installmentContext)
        {
            _customerRepository= customerRepository;
            _mapper = mapper;
            _addressDetailsRepository = addressDetailsRepository;
            _grantorRepository = repository;
        }

        public async Task<bool> CheckIfBlockedNationalId(string nationalId)
        {
           return await _customerRepository.CheckIflockedNationalId(nationalId);
        }
      

        public async Task<bool> CheckIfMobileNumberUsedBefore(string mobileNumber)
        {
            return await _customerRepository.CheckIfDuplicatedMobileNumber(mobileNumber);
        }

        public async Task<bool> CheckIfNationalIdUsedBefore(int nationalId)
        {
           return await _customerRepository.CheckIfDuplicatedNationaId(nationalId);
        }

        public async Task<List<Club>> GetAllClubsByCustomerId(int cutomerId)
        {
            return await _customerRepository.GetAllCustomerClubs(cutomerId);
        }

        public async Task<bool> CheckIfBlockedCustomerName(string CustomerName)
        {
            return await _customerRepository.CheckIfBlockedCustomer(CustomerName);

        }

        public async Task<GuarntorDTO?> GetGuarntorData(int guarntorId)
        {
            var guarntorData = await _customerRepository.GetGuarntorById(guarntorId);
            var guarntorModel = _mapper.Map<GuarntorDTO>(guarntorData);
            return guarntorModel;
        }

        public async Task<GuarntorDTO?> UpdateGurantorData(GuarntorDTO guarntorDTO)
        {
            var guarntorAddress = _mapper.Map<AddressDetails>(guarntorDTO); 
            var updateGuarntorAddress = await _addressDetailsRepository.UpdateAsync(guarntorAddress);
            if (updateGuarntorAddress != null)
            {
                Grantor grantorToUPdate = new Grantor()
                {
                    Id = (int)guarntorDTO.Id,
                    AddressDetailsId = updateGuarntorAddress.Id,
                    MobileNumber = guarntorDTO.MobileNumber,
                    NationalId = guarntorDTO.NationalId,
                    Name = guarntorDTO.Name,
                    RelationId = guarntorDTO.RelationId
                };
                var grantorUpdated = await _grantorRepository.UpdateAsync(grantorToUPdate);
                var guarntorUpdatedModel = _mapper.Map<GuarntorDTO>(grantorUpdated); 

                if (guarntorUpdatedModel != null)
                    return guarntorUpdatedModel;
            }
            return null;
        }
    }
}
