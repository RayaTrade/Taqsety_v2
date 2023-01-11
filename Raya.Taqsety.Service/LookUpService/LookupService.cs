using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.LookupRepository;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.LookUpService
{
    public class LookupService<T> : ILookupService<T> where T : BaseEntityWithDualLang
    {
        private readonly ILookupRepository<T> _lookupRepository;
        public LookupService(ILookupRepository<T> lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }
        public async Task<List<LookupModel>> GetAll(string langId)
        {
            var allEntities = await _lookupRepository.GetAll();
            List<LookupModel> lookupModels = new List<LookupModel>();
            if (langId.ToLower().Contains("ar"))
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
