using System;
using AutoMapper;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.JobDetailsRepository;
using Raya.Taqsety.Service.DTOs;

namespace Raya.Taqsety.Service.JobDetailsService
{
    public class JobDetailsService : IJobDetailsService
    {
        private readonly IJobDetailsRepository _jobDetailsRepository;
        private readonly IMapper _mapper;
        public JobDetailsService(IJobDetailsRepository jobDetailsRepository , IMapper mapper)
        {
            _jobDetailsRepository = jobDetailsRepository;
            _mapper = mapper;
        }
        public async Task<JobDetailsDTO?> GetJobDetailsById(int id)
        {
            var jobDetails =await _jobDetailsRepository.Get(id);
            return _mapper.Map<JobDetailsDTO>(jobDetails);
        }

        public async Task<JobDetailsDTO?> UpdateJobDetails(JobDetailsDTO jobDetailsDTO)
        {
            var toUpdateJobDetails = _mapper.Map<JobDetails>(jobDetailsDTO);
            var updatedJobDetails = await _jobDetailsRepository.UpdateAsync(toUpdateJobDetails);
            var updatedJobDetailsDto = _mapper.Map<JobDetailsDTO>(updatedJobDetails);
            return updatedJobDetailsDto;
        }
    }
}

