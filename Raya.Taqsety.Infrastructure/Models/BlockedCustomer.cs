using System;
using System.Collections.Generic;

namespace Raya.Taqsety.Infrastructure.Models;

public partial class BlockedCustomer
{
    public int Id { get; set; }

    public string? CustNationalId { get; set; }

    public string? CustFullName { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }
}
