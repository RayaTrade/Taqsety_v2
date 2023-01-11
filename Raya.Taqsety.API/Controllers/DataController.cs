using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.CityRepository;
using Raya.Taqsety.Infrastructure.LookupRepository;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Service.LookUpService;
using Raya.Taqsety.Service.MobileNumberVerificationService;

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILookupService<University> _univesityService;
        private readonly ILookupService<Governrate> _governrateService;
        private readonly ILookupService<Status> _statusService;
        private readonly ILookupService<Club> _clubService;
        private readonly ICityService _cityService;
        private readonly IMobileNumberVerificationService _mobileVerificationService;
        public DataController(ILookupService<University> repository,
            ILookupService<Governrate> governrateService,
            ILookupService<Status> statusService,
            ICityService cityRepository,
            IMobileNumberVerificationService mobileVerificationService,
            ILookupService<Club> lookupService
            )
        {
            _univesityService = repository;
            _governrateService = governrateService;
            _cityService = cityRepository;
            _mobileVerificationService = mobileVerificationService;
            _clubService= lookupService;
            _statusService = statusService;
        }
        [HttpGet]
        [Route("GetAllUnivesities")]
        public async Task<JsonResult> GetAllUniversities()
        {
            var langId = "ar";
            var allUnivercities = await _univesityService.GetAll(langId);
            return new JsonResult(new { Succeded = true, Message = "", Content = allUnivercities });

        }

        [HttpGet]
        [Route("GetAllGouvernrates")]
        public async Task<JsonResult> GetAllGouvernrates(string lang)
        {
            //var langId = "ar";
            var allUnivercities = await _governrateService.GetAll(lang);
            return new JsonResult(new { Succeded = true, Message = "", Content = allUnivercities });
        } 
        [HttpGet]
        [Route("GetAllStatuses")]
        public async Task<JsonResult> GetAllStatuses(string lang)
        {
            //var langId = "ar";
            var allStatus = await _statusService.GetAll(lang);
            return new JsonResult(new { Succeded = true, Message = "", Content = allStatus });
        }

        [HttpGet]
        [Route("GetAllGouvernrateCities")]
        public async Task<JsonResult> GetAllGouvernrateCities(int gouvernrteId,string lang)
        {
            //var langId = "ar";
            var allUnivercities = await _cityService.GetAllGouvernrateCities(gouvernrteId, lang);
            return new JsonResult(new { Succeded = true, Message = "", Content = allUnivercities });
        }

        [HttpGet]
        [Route("GetAllClubs")]
        public async Task<JsonResult> GetAllClubs(string langId)
        {
            
            var allUnivercities = await _clubService.GetAll(langId);
            return new JsonResult(new { Succeded = true, Message = "", Content = allUnivercities });
        }

        [HttpGet]
        [Route("SendOTP")]
        public async Task<JsonResult> SendOTP(string mobileNumber, string lang)
        {
            var isOTBSent = await _mobileVerificationService.SendOTB(mobileNumber,lang);
            if (isOTBSent)
                return new JsonResult(new { Succeded = true, Message = "Message Was Sent succefully", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "we didn't send the message somthing went wong", Content = "" });
        }
        [HttpPost]
        [Route("VerifyCode")]
        public async Task<JsonResult> VerifyCode(string mobileNumber, string code)
        {
            var mobileVerificationResult = await _mobileVerificationService.VerifyOTB(mobileNumber,Int32.Parse( code));
            if (!mobileVerificationResult.Succeded)
                return new JsonResult(new { Succeded = false, Message = mobileVerificationResult.Message, Content = "" });
            return new JsonResult(new { Succeded = true, Message = mobileVerificationResult.Message, Content = "" });
        }
    }//from mac
}
