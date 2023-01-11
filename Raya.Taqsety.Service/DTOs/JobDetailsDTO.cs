using System;
namespace Raya.Taqsety.Service.DTOs
{
	public class JobDetailsDTO
	{
        public int Id { get; set; }
        public string? JobAddress { get; set; }
        public string? JobTitle { get; set; }
        public int? Salary { get; set; }
        public bool? IsPrivateSector { get; set; }
        public DateTime? StartingDate { get; set; }
    }
}

