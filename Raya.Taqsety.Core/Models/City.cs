using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class City :BaseEntityWithDualLang
    {
        public int? GovernrateId { get; set; }
        [ForeignKey("GovernrateId")]
        public Governrate? Governrate { get; set; }
    }
}
