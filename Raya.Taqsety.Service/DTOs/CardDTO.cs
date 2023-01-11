using System;
using Raya.Taqsety.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raya.Taqsety.Service.DTOs
{
	public class CardDTO
	{
        public int? Id { get; set; }
        public string? Name { get; set; }
        public long? NationalId { get; set; }
        public string? MobileNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? JobDetailsId { get; set; }
        public int? AddressDetailsId { get; set; }
        public int? ApprovedCreditLimit { get; set; }
        public int? ApprovedMaxMonths { get; set; }
        public int? ApprovedMonthlyCreditLimit { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ValidateToDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? GrantorId { get; set; }
        public int? StatusId { get; set; }

    }
}

