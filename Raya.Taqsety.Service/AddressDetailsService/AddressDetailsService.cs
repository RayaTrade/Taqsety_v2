using System;
using AutoMapper;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.AddressDetailsRepository;
using Raya.Taqsety.Service.DTOs;

namespace Raya.Taqsety.Service.AddressDetailsService
{
	public class AddressDetailsService :IAddressDetailsService
	{
		private readonly IAddressDetailsRepository _addressDetailsRepository;
		private readonly IMapper _mapper;
		public AddressDetailsService(IAddressDetailsRepository addressDetailsRepository,IMapper mapper) 
		{
			_addressDetailsRepository = addressDetailsRepository;
			_mapper = mapper;
		}

        public async Task<AddressDetailsDTO?> GetAddressDetailsById(int id)
        {
			var addressDetails = await _addressDetailsRepository.Get(id);
			var addressDto = _mapper.Map<AddressDetailsDTO>(addressDetails);
			return addressDto;
        }

        public async Task<AddressDetailsDTO?> UpdateAddressDetails(AddressDetailsDTO addressDetailsDto)
        {
			var toUpdateAddressDetails = _mapper.Map<AddressDetails>(addressDetailsDto);
			var updatedAddresDetails = await _addressDetailsRepository.UpdateAsync(toUpdateAddressDetails);
			return _mapper.Map<AddressDetailsDTO>(updatedAddresDetails);
        }
    }
}

