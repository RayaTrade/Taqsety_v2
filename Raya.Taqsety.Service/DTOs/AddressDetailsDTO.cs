using System;
using Raya.Taqsety.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raya.Taqsety.Service.DTOs
{
	public class AddressDetailsDTO
	{
        public int? Id { get; set; }
        public int? BuildingType { get; set; }
        public string? NearestSign { get; set; }
        public string? Details { get; set; }
        public int? CityId { get; set; }
        public int? GovernrateId { get; set; }
        public int? yearsOfResidance { get; set; }
    }
}

