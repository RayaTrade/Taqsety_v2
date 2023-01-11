using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class BaseEntityWithDualLang :BaseEntitiy
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
