using System;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;

namespace Raya.Taqsety.Infrastructure.JobDetailsRepository
{
    public class JobDetailsRepository : Repository<JobDetails>, IJobDetailsRepository  
    {
        public JobDetailsRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
       
    }
}

