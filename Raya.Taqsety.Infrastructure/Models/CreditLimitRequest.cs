using System;
using System.Collections.Generic;

namespace Raya.Taqsety.Infrastructure.Models;

public partial class CreditLimitRequest
{
    public int Id { get; set; }

    public string? ClNationalId { get; set; }

    public string? ClCustomerName { get; set; }

    public string? ClMobileNumber { get; set; }

    public double? ClCreditLimit { get; set; }

    public double? ClSalary { get; set; }

    public string? ClHrId { get; set; }

    public int? ClStatus { get; set; }

    public DateTime? ClCreatedDate { get; set; }

    public DateTime? ClApprovedDate { get; set; }

    public DateTime? ClRejectedDate { get; set; }

    public bool? ClIsCorporate { get; set; }

    public int? ClUsername { get; set; }

    public string? ClFileName { get; set; }

    public string? ClIscoreResult { get; set; }

    public string? ClIscoreNumber { get; set; }

    public string? ClIscoreReason { get; set; }

    public DateTime? ClIscoreDate { get; set; }

    public string? ClIscoreErrors { get; set; }

    public string? ClCreditTeam1stAction { get; set; }

    public DateTime? ClCreditTeam1stActionDate { get; set; }

    public string? ClCreditTeam2ndAction { get; set; }

    public DateTime? ClCreditTeam2ndActionDate { get; set; }

    public DateTime? ClUnderProcessDate { get; set; }

    public DateTime? ClWaitingHrapprovalDate { get; set; }

    public decimal? ClTotalCreditCards { get; set; }

    public decimal? ClTotalLoans { get; set; }

    public int? ClCorporateId { get; set; }

    public string? ClCustomerEmployer { get; set; }

    public string? ClCustomerDegree { get; set; }

    public int? ClUserIdOutSource { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? UpdateBy { get; set; }

    public decimal? ClCreditLimitMonthly { get; set; }

    public int? ClCreditLimitMonths { get; set; }

    public int? ClExpiredWeek { get; set; }

    public DateTime? ClExpiredDate { get; set; }
}
