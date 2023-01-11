using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Raya.Taqsety.Infrastructure.Models;

public partial class InstallmentContext : DbContext
{
    public InstallmentContext()
    {
    }

    public InstallmentContext(DbContextOptions<InstallmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlockedCustomer> BlockedCustomers { get; set; }

    public virtual DbSet<CreditLimitRequest> CreditLimitRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.0.9,34300;Database=Installment;User ID=RayaReportsUser2017;password=wp-rd-reportsP@ss;integrated security=false;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlockedCustomer>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustFullName)
                .HasMaxLength(500)
                .HasColumnName("Cust_FullName");
            entity.Property(e => e.CustNationalId)
                .HasMaxLength(50)
                .HasColumnName("Cust_NationalID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Reason).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CreditLimitRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UsersCreditLimit");

            entity.HasIndex(e => e.ClNationalId, "NonClusteredIndex-20220707-002048");

            entity.HasIndex(e => e.ClMobileNumber, "NonClusteredIndex-20220707-002100");

            entity.HasIndex(e => e.ClCorporateId, "NonClusteredIndex-20220707-002116");

            entity.Property(e => e.ClApprovedDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_ApprovedDate");
            entity.Property(e => e.ClCorporateId).HasColumnName("CL_CorporateID");
            entity.Property(e => e.ClCreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CL_CreatedDate");
            entity.Property(e => e.ClCreditLimit)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CL_CreditLimit");
            entity.Property(e => e.ClCreditLimitMonthly)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CL_CreditLimit_Monthly");
            entity.Property(e => e.ClCreditLimitMonths).HasColumnName("CL_CreditLimit_Months");
            entity.Property(e => e.ClCreditTeam1stAction)
                .HasMaxLength(50)
                .HasColumnName("CL_CreditTeam_1stAction");
            entity.Property(e => e.ClCreditTeam1stActionDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_CreditTeam_1stActionDate");
            entity.Property(e => e.ClCreditTeam2ndAction)
                .HasMaxLength(50)
                .HasColumnName("CL_CreditTeam_2ndAction");
            entity.Property(e => e.ClCreditTeam2ndActionDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_CreditTeam_2ndActionDate");
            entity.Property(e => e.ClCustomerDegree)
                .HasMaxLength(250)
                .HasColumnName("CL_Customer_Degree");
            entity.Property(e => e.ClCustomerEmployer)
                .HasMaxLength(250)
                .HasColumnName("CL_Customer_Employer");
            entity.Property(e => e.ClCustomerName)
                .HasMaxLength(200)
                .HasColumnName("CL_CustomerName");
            entity.Property(e => e.ClExpiredDate)
                .HasColumnType("date")
                .HasColumnName("CL_Expired_Date");
            entity.Property(e => e.ClExpiredWeek).HasColumnName("CL_Expired_Week");
            entity.Property(e => e.ClFileName)
                .HasMaxLength(700)
                .HasColumnName("CL_FileName");
            entity.Property(e => e.ClHrId)
                .HasMaxLength(10)
                .HasColumnName("CL_HrId");
            entity.Property(e => e.ClIsCorporate)
                .HasDefaultValueSql("((1))")
                .HasColumnName("CL_IsCorporate");
            entity.Property(e => e.ClIscoreDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_IscoreDate");
            entity.Property(e => e.ClIscoreErrors)
                .HasMaxLength(500)
                .HasColumnName("CL_IscoreErrors");
            entity.Property(e => e.ClIscoreNumber)
                .HasMaxLength(50)
                .HasColumnName("CL_IscoreNumber");
            entity.Property(e => e.ClIscoreReason)
                .HasMaxLength(500)
                .HasColumnName("CL_IscoreReason");
            entity.Property(e => e.ClIscoreResult)
                .HasMaxLength(250)
                .HasColumnName("CL_IscoreResult");
            entity.Property(e => e.ClMobileNumber)
                .HasMaxLength(16)
                .HasColumnName("CL_MobileNumber");
            entity.Property(e => e.ClNationalId)
                .HasMaxLength(14)
                .HasColumnName("CL_NationalId");
            entity.Property(e => e.ClRejectedDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_RejectedDate");
            entity.Property(e => e.ClSalary)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CL_Salary");
            entity.Property(e => e.ClStatus).HasColumnName("CL_Status");
            entity.Property(e => e.ClTotalCreditCards)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CL_TotalCreditCards");
            entity.Property(e => e.ClTotalLoans)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CL_TotalLoans");
            entity.Property(e => e.ClUnderProcessDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_UnderProcessDate");
            entity.Property(e => e.ClUserIdOutSource).HasColumnName("CL_UserIdOutSource");
            entity.Property(e => e.ClUsername).HasColumnName("CL_Username");
            entity.Property(e => e.ClWaitingHrapprovalDate)
                .HasColumnType("datetime")
                .HasColumnName("CL_WaitingHRApprovalDate");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy).HasColumnName("update_by");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
