using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.AddressDetailsService;
using Raya.Taqsety.Service.DTOs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressDetailsController : ControllerBase
    {
        private readonly IAddressDetailsService _addressDetailsService;
        public AddressDetailsController(IAddressDetailsService addresDetailsService)
        {
            _addressDetailsService = addresDetailsService;
        }
        [HttpGet]
        [Route("GetAddressDetailsById")]
        public async Task<JsonResult> GetAddressDetailsById(string id)
        {
            var addressDetails = await _addressDetailsService.GetAddressDetailsById(int.Parse( id));
            if(addressDetails != null)
                return new JsonResult(new { Succeded = true, Message = "", Content = addressDetails });
            return new JsonResult(new { Succeded = false, Message = "This Address details Not Found", Content = "" });

        }
        [HttpPut]
        [Route("UpdateAddressDetails")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<JsonResult> UpdateAddressDetails([FromBody] AddressDetailsDTO addressDetailsDTO)
        {
          // var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var updatedAddress = await _addressDetailsService.UpdateAddressDetails(addressDetailsDTO);
            if(updatedAddress != null)
                return new JsonResult(new { Succeded = true, Message = "updated successfully", Content = updatedAddress });
            return new JsonResult(new { Succeded = false, Message = "We didn't update the address somthing went wrong", Content = "" });

        }

    }
}
