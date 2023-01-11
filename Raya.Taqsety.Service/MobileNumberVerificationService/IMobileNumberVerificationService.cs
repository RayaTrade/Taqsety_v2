using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.MobileNumberVerificationService
{
    public interface IMobileNumberVerificationService
    {
        Task<bool> SendOTB(string mobileNumber,string lang);

        Task<ResultModel> VerifyOTB(string mobileNumber, int code);
    }
}
