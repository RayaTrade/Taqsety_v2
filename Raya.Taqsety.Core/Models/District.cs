using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class District : BaseEntityWithDualLang
    {
        public int? CityId{ get; set; }
        [ForeignKey("CityId")]
        public City? City { get; set; }
    }
}
