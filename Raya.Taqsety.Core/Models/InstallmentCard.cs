using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class InstallmentCard :BaseEntitiy
    {
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int ApprovedCreditLimit { get; set; }
        public int ApprovedMaxMonths { get; set; }
        public int ApprovedMonthlyCreditLimit { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidateToDate { get; set; }
        public DateTime?  IssueDate { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }
        public int? GrantorId { get; set; }
        [ForeignKey("GrantorId")]
        public Grantor? Grantor { get; set; }

    }
}
