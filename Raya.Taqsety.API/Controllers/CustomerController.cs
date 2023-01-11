using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.AttachmentService;
using Raya.Taqsety.Service.CustomerService;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.InstallmentCardService;
using Raya.Taqsety.Service.Models;

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IInstallmentCardService _installmentCardService;
        private readonly IAttachmentServie _attachmentService;
        public CustomerController(ICustomerService customerService, IInstallmentCardService installmentCardService,IAttachmentServie attachmentServie)
        {
            _customerService= customerService;
            _installmentCardService = installmentCardService;
            _attachmentService = attachmentServie;
        }
        [HttpGet]
        [Route("CheckIfNationalIdUsedBefore")]
        public async Task<JsonResult> CheckIfNationalIdUsedBefore([FromBody] int nationalId)
        {
            var bNationalisLreadyUsed = await _customerService.CheckIfNationalIdUsedBefore(nationalId);
            if (!bNationalisLreadyUsed)
                return new JsonResult(new { Succeded = true, Message = "", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "This NationalId already used Before", Content = "" });
        }

        [HttpGet]
        [Route("CheckIfMobileNumberUsedBefore")]
        public async Task<JsonResult> CheckIfMobileNumberUsedBefore(string mobileNumber)
        {
            var bNationalisLreadyUsed = await _customerService.CheckIfMobileNumberUsedBefore(mobileNumber);
            if (!bNationalisLreadyUsed)
                return new JsonResult(new { Succeded = true, Message = "", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "This Mobile Number already used Before", Content = "" });
        }
        [HttpGet]
        [Route("isValidNationalId")]
        public JsonResult IsValidNationalId(string nationalId)
        {
            var isValid = _installmentCardService.IsValidNationalId(long.Parse(nationalId));
            if (isValid)
                return new JsonResult(new { Succeded = true, Message = "Valid National Id", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "This National Id is not valid", Content = "" });

        }
        [HttpPut]
        [Route("UpdateCustomerMaritialStatus")]
        public async Task<JsonResult> UpdateCustomerMaritialStatus(string installmentCardId, int maritialStatus)
        {
            ResultModel isUpdatedResult = await _installmentCardService
                                                   .UpdateCustomerMaritialStatus(Convert.ToInt32(installmentCardId), maritialStatus);
            if (isUpdatedResult.Succeded)
                return new JsonResult(new { Succeded = true, Message = isUpdatedResult.Message, Content = "" });
            return new JsonResult(new { Succeded = false, Message = isUpdatedResult.Message, Content = "" });
        }

        [HttpGet]
        [Route("GetAllCustomerAttachments")]
        public async Task<JsonResult> GetAllCustomerAttachments(int customerId)
        {
            var attachments = await _attachmentService.GetAllCustomerAttachments(customerId);
            if (attachments != null)
                return new JsonResult(new { Succeded = true, Message = "", Content = attachments });
            return new JsonResult(new { Succeded = false, Message = "No data Found", Content = "" });
        }
        [HttpGet]
        [Route("CheckIfBlockedCustomer")]
        public async Task<JsonResult> CheckIfBlockedCustomer(string CustomerName)
        {
            var isBlockedCustomer = await _customerService.CheckIfBlockedCustomerName(CustomerName); 
            if (isBlockedCustomer)
                return new JsonResult(new { Succeded = true, Message = "Customer is blocked", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "valid Customer", Content = "" });
        } 
        [HttpGet]
        [Route("CheckIfBlockedNationalId")]
        public async Task<JsonResult> CheckIfBlockedNationalId(string NationalId)
        {
            var isBlockedNationalId = await _customerService.CheckIfBlockedNationalId(NationalId); 
            if (isBlockedNationalId)
                return new JsonResult(new { Succeded = true, Message = "national id is blocked", Content = "" });
            return new JsonResult(new { Succeded = false, Message = "valid nationalId", Content = "" });
        }

        [HttpPut]
        [Route("UpdateCustomerAttachments")]
        public async Task<JsonResult> UpdateCustomerAttachments([FromBody]CustomerAttachmentsDTO customerAttachmentsDTO)
        {
            ResultModel updatedAttachments = await _attachmentService.UpdateCustomerAttachments(customerAttachmentsDTO);
            if (updatedAttachments.Succeded)
                return new JsonResult(new { Succeded = true, Message = updatedAttachments.Message, Content = updatedAttachments.Content });
            return new JsonResult(new { Succeded = false, Message = updatedAttachments.Message, Content = "" });
        }
        [HttpGet]
        [Route("GetGuarntorData")]
        public async Task<JsonResult> GetGuarntorData(int guarntorId)
        {
            var guarntor = await _customerService.GetGuarntorData(guarntorId);
            if (guarntor != null)
                return new JsonResult(new { Succeded = true, Message = "Guarntor Data", Content = guarntor });
            return new JsonResult(new { Succeded = false, Message = "valid Guarntor", Content = "" });
        }
        [HttpPut]
        [Route("UpdateGuarntorData")]
        public async Task<JsonResult> UpdateGuarntorData(GuarntorDTO guarntorDTO)
        {
            GuarntorDTO? guarntor = await _customerService.UpdateGurantorData(guarntorDTO);
            if (guarntor != null)
                return new JsonResult(new { Succeded = true, Message = "Guarntor is Updated", Content = guarntor });
            return new JsonResult(new { Succeded = false, Message = "invalid Guarntor", Content = "" });
        }
    }
}
    