using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.InstallmentCardService;
using Raya.Taqsety.Service.Models;

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentCardController : ControllerBase
    {
        private readonly IInstallmentCardService _InstallmentCardService;
        public InstallmentCardController(IInstallmentCardService installmentCardService)
        {
            _InstallmentCardService = installmentCardService;
        }
        [HttpGet]
        [Route("GetInstallmentCardByMobileNumber")]
        public async Task<IActionResult> GetInstallmentCardByMobileNumber(string mobileNumber,string lang)
        {
            InstallmentCardDto? installmentCard = await _InstallmentCardService.GetInstallmentCardByMobileNumber(mobileNumber,lang);
            if (installmentCard != null)
                return new JsonResult(new { Succeded = true, Message = $"", Content = installmentCard });
            return new JsonResult(new { Succeded = false, Message = $"No Data Were Found", Content = "" });
        }

        [HttpGet]
        [Route("GetAllInstallmentCards")]
        public async Task<IActionResult> GetAllInstallmentCards()
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var roleId = int.Parse(this.User.Claims.First(i => i.Type == "RoleId").Value);
            List<CardDTO> installmentCard = await _InstallmentCardService.GetAllCards(roleId, userId);
            if (installmentCard != null)
                return new JsonResult(new { Succeded = true, Message = $"", Content = installmentCard });
            return new JsonResult(new { Succeded = false, Message = $"No Data Were Found", Content = "" });
        }
        [HttpPost]
        [Route("InsertInstallmentCard")]
        public async Task<IActionResult> InsertInstallmentCard([FromBody] InstallmentCardDto installmentCardModel)
        {
            ResultModel InstallmentCardCreationResult = await _InstallmentCardService.CreateInstallmentCard(installmentCardModel);
            if (InstallmentCardCreationResult.Succeded)
                return new JsonResult(new { Succeded = true, Message = InstallmentCardCreationResult.Message, Content = "" });
            return new JsonResult(new { Succeded = false, Message = InstallmentCardCreationResult.Message, Content = "" });
        }
        [HttpPut]
        [Route("UpdateCustomerFullName")]
        public async Task<JsonResult> UpdateCustomerFullName(string installmentCardId, string newCustomerFullName)
        {
            ResultModel isUpdatedResult = await _InstallmentCardService
                                                   .UpdateCustomerName(Convert.ToInt32(installmentCardId), newCustomerFullName);
            if (isUpdatedResult.Succeded)
                return new JsonResult(new { Succeded = true, Message = isUpdatedResult.Message, Content = "" });
            return new JsonResult(new { Succeded = false, Message = isUpdatedResult.Message, Content = "" });
        }
        [HttpGet]
        [Route("GetInstallmetnCardWithNationalId")]
        public async Task<JsonResult> GetInstallmetnCardWithNationalId(string nationalId,string lang)
        {
            var installmentCard = await _InstallmentCardService.GetInstallmentCardByNationalId(long.Parse( nationalId), lang);
            if (installmentCard != null)
                return new JsonResult(new { Succeded = true, Message = $"", Content = installmentCard });
            return new JsonResult(new { Succeded = false, Message = $"No Data Were Found", Content = "" });
        }
        [HttpPut]
        [Route("UpdateInstallmentCard")]
        public async Task<JsonResult> UpdateInstallmentCard([FromBody] CardDTO cardDTO)
        {
            var installmentCard = await _InstallmentCardService.UpdateInstallmentCard(cardDTO);
            if (installmentCard != null)
                return new JsonResult(new { Succeded = true, Message = "updated successfully", Content = installmentCard });
            return new JsonResult(new { Succeded = false, Message = "No Data Updated", Content = "" });
        }
        [HttpPut]
        [Route("ChangeInstallmentCardStatus")]
        public async Task<JsonResult> UpdateInstallmentCardStatus(int installmentCardId,int newStatusId)
        {
            int userId = 1;
            if (newStatusId != 1)
                 userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var updatedInstallmentCard = await _InstallmentCardService.UpdateInstallmentStatus(installmentCardId,newStatusId, userId);
            if (updatedInstallmentCard != null)
                return new JsonResult(new { Succeded = true, Message = "The Status Is Updated", Content = updatedInstallmentCard });
            return new JsonResult(new { Succeded = false, Message = "No Data Updated", Content = "" });
        }
        
    }   
}
