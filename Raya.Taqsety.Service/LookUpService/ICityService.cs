using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.LookUpService
{
    public interface ICityService 
    {
        Task<List<LookupModel>> GetAllGouvernrateCities(int gouvernrateId, string lang);
    }
}
