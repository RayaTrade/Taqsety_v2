using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.MobileNumberVerificationRepository
{
    public interface IMobileNumberVerificationRepository
    {
        Task<MobileNumberVerification?> GetMobileNumberVerificationMobileNumber(string mobileNumber);
        Task<MobileNumberVerification?> InsertMobileNumberOTB(MobileNumberVerification mobileNumberVerification);
    }
}
