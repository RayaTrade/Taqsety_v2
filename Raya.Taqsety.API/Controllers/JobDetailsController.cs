using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.JobDetailsService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    public class JobDetailsController : Controller
    {
        private readonly IJobDetailsService _jobDetailsService;
        public JobDetailsController(IJobDetailsService jobDetailsService)
        {
            _jobDetailsService = jobDetailsService;
        }

        [HttpGet]
        [Route("GetJobDetailsById")]
        public async Task<JsonResult> GetJobDetailsById(string id)
        {
            var jobDetails = await _jobDetailsService.GetJobDetailsById(int.Parse(id));
            if (jobDetails != null)
                return new JsonResult(new { Succeded = true, Message = "", Content = jobDetails });
            return new JsonResult(new { Succeded = false, Message = "This job details Not Found", Content = "" });
        }
        [HttpPut]
        [Route("UpdateJobDetails")]
        public async Task<JsonResult> UpdateJobDetails([FromBody] JobDetailsDTO jobDetailsDTO)
        {
            var jobDetails = await _jobDetailsService.UpdateJobDetails(jobDetailsDTO);
            if (jobDetails != null)
                return new JsonResult(new { Succeded = true, Message = "updated successfully", Content = jobDetails });
            return new JsonResult(new { Succeded = false, Message = "This job details wan not updated", Content = "" });
        }
    }
}

