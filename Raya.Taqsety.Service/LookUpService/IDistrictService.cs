using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.LookUpService
{
    public interface IDistrictService
    {
        Task<List<LookupModel>> GetAllCityDistricts(int cityId, string lang);
    }
}
