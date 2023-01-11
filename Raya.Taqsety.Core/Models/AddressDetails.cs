using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class AddressDetails :BaseEntitiy
    {
        public int BuildingType { get; set; }
        public string?  NearestSign { get; set; }
        public int? yearsOfResidance { get; set; }
        public string? Details { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int? GovernrateId { get; set; }
        [ForeignKey("GovernrateId")]
        public Governrate? Governrate { get; set; }
    }
}
