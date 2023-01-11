using System;
using Raya.Taqsety.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raya.Taqsety.Service.DTOs
{
	public class GuarntorDTO
	{
        public int? Id { get; set; } //latest
        public int? RelationId { get; set; }
        public string? Name { get; set; }
        public long? NationalId { get; set; }
        public string? MobileNumber { get; set; }
        //address details
        public int? AddressDetailsId { get; set; }
        public int? GovernrateId { get; set; }
        public int? CityId { get; set; }
        public string? AddressDetails { get; set; }
        public string? NearestLandMark { get; set; }

    }
}

