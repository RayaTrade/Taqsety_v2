using System;
using Raya.Taqsety.Infrastructure.AddressDetailsRepository;
using Raya.Taqsety.Service.DTOs;

namespace Raya.Taqsety.Service.AddressDetailsService
{
	public interface IAddressDetailsService 
	{
		Task<AddressDetailsDTO?> GetAddressDetailsById(int id);
		Task<AddressDetailsDTO?> UpdateAddressDetails(AddressDetailsDTO addressDetails);
	}
}

