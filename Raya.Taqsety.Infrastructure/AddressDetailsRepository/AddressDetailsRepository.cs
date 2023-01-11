using System;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;

namespace Raya.Taqsety.Infrastructure.AddressDetailsRepository
{
	public class AddressDetailsRepository :  Repository<AddressDetails> , IAddressDetailsRepository
    {
		public AddressDetailsRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
		{

		}


	}
}

