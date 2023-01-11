using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.DTOs
{
    public class GrantorAddressDetailsModel
    {
        public int Id { get; set; }
        public int BuildingType { get; set; }
        public string Details { get; set; }
        public string NearestSign { get; set; }
        //public int? DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? GovernrateId { get; set; }
    }
}
