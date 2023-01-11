using System;
using Raya.Taqsety.Service.DTOs;

namespace Raya.Taqsety.Service.JobDetailsService
{
	public interface IJobDetailsService
	{
		Task<JobDetailsDTO?> GetJobDetailsById(int id);
		Task<JobDetailsDTO?> UpdateJobDetails(JobDetailsDTO jobDetailsDTO);
	}
}

