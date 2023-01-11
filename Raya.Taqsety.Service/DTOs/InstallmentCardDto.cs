using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.DTOs
{
    public class InstallmentCardDto
    {
        public int? Id { get; set; }
        //customerData
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long CustomerNationalId { get; set; }
        public string CustomerMobileNumber { get; set; }
        public bool CustomerHasCar { get; set; }
        public bool CustomerHasMedicalInsurance { get; set; }
        public LookupModel? CustomerUniversityId { get; set; }
        public LookupModel? CustomerMartialStatus { get; set; }
        public List<LookupModel?>? ClubsIds { get; set; }

        //customerData
        //customer address details
        public int CustomerAddressDetailsId { get; set; }
        public string CustomerAddressDetails { get; set; }
        public string CustomerAddressNearestLandmark { get; set; }
        public LookupModel CustomerBuildingType { get; set; }
        public LookupModel? CustomerCityId { get; set; }
        public LookupModel? CustomerGovernrateId { get; set; }
        //customer address details

        // customer attachments

        public List<AttachmentDTO>? Attachments { get; set; }
        public List<string?>? AttachmentsOnDelivery { get; set; }
        // customer attachments

        //customer job Details
        public int CustomerJobDetailsId { get; set; }
        public string? CustomerJobTitle { get; set; }
        public DateTime? JobStartingDate { get; set; }
        public int? yearsOfResidance { get; set; }
            
        public string JobAddress { get; set; }
        public int Salary { get; set; }
        public bool IsPrivateSector { get; set; }
        public string? IsPrivateSectorDisplayName { get; set; }
        public LookupModel JobTypeId { get; set; }

        //customer job Details

        //grantor info
        public int? GrantorId { get; set; }
        public string? GrantorMobileNumber { get; set; }
        public long? GrantorNationalId { get; set; }
        public string? GrantorName { get; set; }
        public LookupModel? GrantorRelationId { get; set; }

        //grantor info

        //grantor address details
        public int? GrantorAddressDetailsId { get; set; }
        public string? GrantorAddressDetails { get; set; }
        public string? GrantorAddressNearestLandmark { get; set; }
        public LookupModel? GrantorBuildingType { get; set; }
        //public int? GrantorDistrictId { get; set; }
        public LookupModel? GrantorCityId { get; set; }
        public LookupModel? GrantorGovernrateId { get; set; }
        public string? GrantorNearestSign { get; set; }

        //grantor address details

        //Installment Card Info
        public int ApprovedCreditLimit { get; set; }
        public int ApprovedMaxMonths { get; set; }
        public int ApprovedMonthlyCreditLimit { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidateToDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public LookupModel? StatusId { get; set; }
    }
}
