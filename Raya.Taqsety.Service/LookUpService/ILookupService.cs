using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.LookupRepository;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.LookUpService
{
    public interface ILookupService<T> where T :BaseEntityWithDualLang 
    {
        Task<List<LookupModel>> GetAll(string langId);
    }
}
