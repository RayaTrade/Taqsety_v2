using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.CityRepository;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.LookUpService
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepo;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepo = cityRepository;
        }
        public async Task<List<LookupModel>> GetAllGouvernrateCities(int gouvernrateId, string lang)
        {
            var allEntities = await _cityRepo.GetAllGouvernrateCities(gouvernrateId);
            List<LookupModel> lookupModels = new();
            if (lang.ToLower().Contains("ar"))
            {
                foreach (var entity in allEntities)
                {
                    lookupModels.Add(new LookupModel
                    {
                        Id = entity.Id,
                        Name = entity.ArabicName
                    });
                }
            }
            else
            {
                foreach (var entity in allEntities)
                {
                    lookupModels.Add(new LookupModel
                    {
                        Id = entity.Id,
                        Name = entity.EnglishName
                    });
                }
            }
            return lookupModels;
        }

    }
}
