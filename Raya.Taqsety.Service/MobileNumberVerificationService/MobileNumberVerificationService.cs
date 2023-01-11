using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.MobileNumberVerificationRepository;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.MobileNumberVerificationService
{
    public class MobileNumberVerificationService : IMobileNumberVerificationService
    {
        private readonly IMobileNumberVerificationRepository _mobileNumberVerificationRepository;
        public MobileNumberVerificationService(IMobileNumberVerificationRepository mobileNumberVerificationRepository)
        {
            _mobileNumberVerificationRepository = mobileNumberVerificationRepository;
        }

        public async Task<int> GetMobileOTBCode(string mobileNumber)
        {
            var result = await _mobileNumberVerificationRepository.GetMobileNumberVerificationMobileNumber(mobileNumber);
            if (result != null)
                return result.Code;
            return 0;
        }

        private static int GenerateRandomOTB()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new();
            return _rdm.Next(_min, _max);
        }

        public async Task<ResultModel> VerifyOTB(string mobileNumber, int code)
        {
            var sentCode = await _mobileNumberVerificationRepository.GetMobileNumberVerificationMobileNumber(mobileNumber);
            if(sentCode != null && sentCode.CreationDate.Value.AddSeconds(40) <= DateTime.Now)
            {
                return new ResultModel { Succeded = false, Message = "Code Expired", Content = "" };
            }
            if(sentCode!=null && code == sentCode.Code)
            {
              return   new ResultModel { Succeded = true, Message = "Code is right", Content = "" };
            }
            return new ResultModel { Succeded = false, Message = "Code is wrong", Content = "" };
        }

        public async Task<bool> SendOTB(string mobileNumber,string lang)
        {
            var code = GenerateRandomOTB();
            MobileNumberVerification mobileVerificationToInsert = new()
            {
                MobileNumber =  mobileNumber,
                Code = code,
                CreationDate = DateTime.Now,
            };
            var isMobileVerificationInserted = await _mobileNumberVerificationRepository.InsertMobileNumberOTB(mobileVerificationToInsert);
            if (isMobileVerificationInserted != null)
            {
                var isSMSSent = await SendSMS(mobileNumber, code,lang);
                return isSMSSent;
            }
            return false;
        }
        private static async Task<bool> SendSMS(string mobileNumber, int code, string lang)
        {
            string message;
            if (lang.ToLower().Contains("ar"))
                message = $"رقم تحقق RayaShop الخاص بك هو {code} أستخدم هذا الرقم للتحقق من تسجيل دخولك";
            else
                message = $"Your RayaShop Code is {code} . use This number to validate your login";

            HttpClient client = new();
            var targetUri = $"www.rayadistribution.com/RayaSMS/RayaSMSService.asmx/SendSMS";
            var builder = new UriBuilder(targetUri);
            builder.Query = $"UserName=RayaDist&Password=Raya@2016Dist&SenderName=Raya&MobileNumber={mobileNumber}&SMSText={message}";
            var url = builder.ToString();
            var response = await client.GetAsync(url);
            return (response.IsSuccessStatusCode);
        }
    }
}
