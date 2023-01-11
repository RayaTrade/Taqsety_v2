using AutoMapper;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.DTOs;
using System.Data;
using System.Text.RegularExpressions;

namespace Raya.Taqsety.Service.Configuration
{
    public class MapperConfig :Profile
    {
        public MapperConfig()
        {
            //CreateMap<Customer, InstallmentCardDto>()
            //    .ForMember(x => x.CustomerName, c => c.MapFrom(x => x.Name))
            //    .ForMember(x => x.CustomerNationalId, c => c.MapFrom(x => x.NationalId))
            //    .ForMember(x => x.CustomerMobileNumber, c => c.MapFrom(x => x.MobileNumber))
            //    .ForMember(x => x.CustomerMartialStatus, c => c.MapFrom(x => x.MartialStatus))
            //    .ForMember(x => x.CustomerUniversityId.Id, c => c.MapFrom(x => x.UniversityId))
            //    //.ForMember(x => x.CustomerUniversityId.Name, c => c.MapFrom(x => x.University.EnglishName))
            //    .ForMember(x => x.CustomerMartialStatus.Id, c => c.MapFrom(x => x.MartialStatus)).ReverseMap();
            
            CreateMap<InstallmentCardDto, Customer>()
                .ForMember(x => x.Name, c => c.MapFrom(x => x.CustomerName))
                .ForMember(x => x.NationalId, c => c.MapFrom(x => x.CustomerNationalId))
                .ForMember(x => x.MobileNumber, c => c.MapFrom(x => x.CustomerMobileNumber))
                .ForMember(x => x.MartialStatus, c => c.MapFrom(x => x.CustomerMartialStatus.Id))
                .ForMember(x => x.UniversityId, c => c.MapFrom(x => x.CustomerUniversityId.Id))
                //.ForMember(x => x.HasMedicalinsurance, c => c.MapFrom(x => x.CustomerHasMedicalInsurance))
                //.ForMember(x => x.HasCar, c => c.MapFrom(x => x.CustomerHasCar))
                .ForMember(x => x.MartialStatus, c => c.MapFrom(x => x.CustomerMartialStatus.Id)).ReverseMap();

            //CreateMap<AddressDetails, InstallmentCardDto>()
            //   .ForMember(x => x.CustomerBuildingType.Id, c => c.MapFrom(x => x.BuildingType))
            //   .ForMember(x => x.CustomerAddressDetails, c => c.MapFrom(x => x.Details))
            //   .ForMember(x => x.CustomerCityId.Id, c => c.MapFrom(x => x.CityId))
            //   .ForMember(x => x.CustomerGovernrateId.Id, c => c.MapFrom(x => x.GovernrateId));
            
            CreateMap<InstallmentCardDto, AddressDetails>()
               .ForMember(x => x.BuildingType, c => c.MapFrom(x => x.CustomerBuildingType.Id))
               .ForMember(x => x.Details, c => c.MapFrom(x => x.CustomerAddressDetails))
               .ForMember(x => x.CityId, c => c.MapFrom(x => x.CustomerCityId.Id))
               .ForMember(x => x.NearestSign, c => c.MapFrom(x => x.CustomerAddressNearestLandmark))
               .ForMember(x => x.GovernrateId, c => c.MapFrom(x => x.CustomerGovernrateId.Id));  
            
            //CreateMap<InstallmentCardDto, AddressDetails>()
            //   .ForMember(x => x.BuildingType, c => c.MapFrom(x => x.GrantorBuildingType.Id))
            //   .ForMember(x => x.Details, c => c.MapFrom(x => x.GrantorAddressDetails))
            //   .ForMember(x => x.CityId, c => c.MapFrom(x => x.GrantorCityId.Id))
            //   .ForMember(x => x.GovernrateId, c => c.MapFrom(x => x.GrantorGovernrateId.Id));

            //CreateMap<InstallmentCardDto,AddressDetails>()
            //  .ForMember(x => x.CustomerBuildingType.Id, c => c.MapFrom(x => x.BuildingType))
            //  .ForMember(x => x.CustomerAddressDetails, c => c.MapFrom(x => x.Details))
            //  .ForMember(x => x.CustomerCityId.Id, c => c.MapFrom(x => x.CityId))
            //  .ForMember(x => x.CustomerGovernrateId, c => c.MapFrom(x => x.GovernrateId))
            //  //.ForMember(x => x.CustomerAddressNearestLandmark, c => c.MapFrom(x => x.GrantorAddressNearestLandmark))
            //  .ForMember(x => x.CustomerAddressNearestLandmark, c => c.MapFrom(x => x.NearestSign));


            //CreateMap<GrantorAddressDetailsModel, InstallmentCardDto>()
            //   .ForMember(x => x.GrantorBuildingType.Id, c => c.MapFrom(x => x.BuildingType))
            //   .ForMember(x => x.GrantorAddressDetails, c => c.MapFrom(x => x.Details))
            //   .ForMember(x => x.GrantorCityId.Id, c => c.MapFrom(x => x.CityId))
            //   .ForMember(x => x.GrantorAddressNearestLandmark, c => c.MapFrom(x => x.NearestSign))
            //   .ForMember(x => x.GrantorGovernrateId.Id, c => c.MapFrom(x => x.GovernrateId)).ReverseMap(); 
            
            CreateMap<InstallmentCardDto, GrantorAddressDetailsModel>()
               .ForMember(x => x.BuildingType, c => c.MapFrom(x => x.GrantorBuildingType.Id))
               .ForMember(x => x.Details, c => c.MapFrom(x => x.GrantorAddressDetails))
               .ForMember(x => x.CityId, c => c.MapFrom(x => x.GrantorCityId.Id))
               .ForMember(x => x.NearestSign, c => c.MapFrom(x => x.GrantorNearestSign))
               .ForMember(x => x.GovernrateId, c => c.MapFrom(x => x.GrantorGovernrateId.Id)).ReverseMap();


            //CreateMap<Grantor, InstallmentCardDto>()
            //   .ForMember(x => x.GrantorMobileNumber, c => c.MapFrom(x => x.MobileNumber))
            //   .ForMember(x => x.GrantorNationalId, c => c.MapFrom(x => x.NationalId))
            //   .ForMember(x => x.GrantorName, c => c.MapFrom(x => x.Name))
            //   .ForMember(x => x.GrantorRelationId.Id, c => c.MapFrom(x => x.RelationId)).ReverseMap();
            CreateMap<InstallmentCardDto, Grantor>()
               .ForMember(x => x.MobileNumber, c => c.MapFrom(x => x.GrantorMobileNumber))
               .ForMember(x => x.NationalId, c => c.MapFrom(x => x.GrantorNationalId))
               .ForMember(x => x.Name, c => c.MapFrom(x => x.GrantorName))
               .ForMember(x => x.RelationId, c => c.MapFrom(x => x.GrantorRelationId.Id)).ReverseMap();

            //CreateMap<JobDetails, InstallmentCardDto>()
            //   .ForMember(x => x.JobAddress, c => c.MapFrom(x => x.JobAddress))
            //   .ForMember(x => x.Salary, c => c.MapFrom(x => x.Salary))
            //   .ForMember(x => x.JobTypeId.Id, c => c.MapFrom(x => x.JobTypeId))
            //   .ForMember(x => x.IsPrivateSector, c => c.MapFrom(x => x.IsPrivateSector)).ReverseMap(); 


            CreateMap<InstallmentCardDto, JobDetails>()
               .ForMember(x => x.JobAddress, c => c.MapFrom(x => x.JobAddress))
               .ForMember(x => x.Salary, c => c.MapFrom(x => x.Salary))
               .ForMember(x => x.JobTypeId, c => c.MapFrom(x => x.JobTypeId.Id))
               .ForMember(x => x.JobTitle, c => c.MapFrom(x => x.CustomerJobTitle))
               .ForMember(x => x.StartingDate, c => c.MapFrom(x => x.JobStartingDate))
               .ForMember(x => x.IsPrivateSector, c => c.MapFrom(x => x.IsPrivateSector)).ReverseMap();
            //CreateMap<JobDetails, InstallmentCardDto>()
            //   .ForMember(x => x.JobAddress, c => c.MapFrom(x => x.JobAddress))
            //   .ForMember(x => x.Salary, c => c.MapFrom(x => x.Salary))
            //   .ForMember(x => x.JobTypeId.Id, c => c.MapFrom(x => x.JobTypeId))
            //   .ForMember(x => x.IsPrivateSector, c => c.MapFrom(x => x.IsPrivateSector)).ReverseMap();  

            CreateMap<InstallmentCardDto, InstallmentCard>()
               .ForMember(x => x.CustomerId, c => c.MapFrom(x => x.CustomerId))
               .ForMember(x => x.GrantorId, c => c.MapFrom(x => x.GrantorId))
               .ForMember(x => x.ApprovedCreditLimit, c => c.MapFrom(x => x.ApprovedCreditLimit))
               .ForMember(x => x.ApprovedMonthlyCreditLimit, c => c.MapFrom(x => x.ApprovedMonthlyCreditLimit))
               .ForMember(x => x.IsActive, c => c.MapFrom(x => x.IsActive))
               .ForMember(x => x.IssueDate, c => c.MapFrom(x => x.IssueDate))
               .ForMember(x => x.ValidateToDate, c => c.MapFrom(x => x.ValidateToDate))
              // .ForMember(x => x.StatusId, c => c.MapFrom(x => x.StatusId.Id))
               .ForMember(x => x.ApprovedMaxMonths, c => c.MapFrom(x => x.ApprovedMaxMonths)).ReverseMap();


            //CreateMap<InstallmentCard, InstallmentCardDto>()
            //  .ForMember(x => x.CustomerId, c => c.MapFrom(x => x.CustomerId))
            //  .ForMember(x => x.GrantorId, c => c.MapFrom(x => x.GrantorId))
            //  .ForMember(x => x.ApprovedCreditLimit, c => c.MapFrom(x => x.ApprovedCreditLimit))
            //  .ForMember(x => x.ApprovedMonthlyCreditLimit, c => c.MapFrom(x => x.ApprovedMonthlyCreditLimit))
            //  .ForMember(x => x.IsActive, c => c.MapFrom(x => x.IsActive))
            //  .ForMember(x => x.IssueDate, c => c.MapFrom(x => x.IssueDate))
            //  .ForMember(x => x.ValidateToDate, c => c.MapFrom(x => x.ValidateToDate))
            //  .ForMember(x => x.StatusId.Id, c => c.MapFrom(x => x.StatusId))
            //  .ForMember(x => x.ApprovedMaxMonths, c => c.MapFrom(x => x.ApprovedMaxMonths));


            CreateMap<InstallmentCard, InstallmentCardDto>()
              .ForMember(x => x.CustomerId, c => c.MapFrom(x => x.CustomerId))
              .ForMember(x => x.GrantorId, c => c.MapFrom(x => x.GrantorId))
              .ForMember(x => x.ApprovedCreditLimit, c => c.MapFrom(x => x.ApprovedCreditLimit))
              .ForMember(x => x.ApprovedMonthlyCreditLimit, c => c.MapFrom(x => x.ApprovedMonthlyCreditLimit))
              .ForMember(x => x.IsActive, c => c.MapFrom(x => x.IsActive))
              .ForMember(x => x.IssueDate, c => c.MapFrom(x => x.IssueDate))
              .ForMember(x => x.ValidateToDate, c => c.MapFrom(x => x.ValidateToDate))
              //.ForMember(x => x.StatusId, c => c.MapFrom(x => x.StatusId))
              .ForMember(x => x.ApprovedMaxMonths, c => c.MapFrom(x => x.ApprovedMaxMonths)).ReverseMap();

            CreateMap<AddressDetails, GrantorAddressDetailsModel>().ReverseMap();

            CreateMap<AddressDetails, AddressDetailsDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Grantor, GuarntorDTO>() 
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.Name, c => c.MapFrom(x => x.Name))
                .ForMember(x => x.MobileNumber, c => c.MapFrom(x => x.MobileNumber))
                .ForMember(x => x.NationalId, c => c.MapFrom(x => x.NationalId))
                .ForMember(x => x.GovernrateId, c => c.MapFrom(x => x.AddressDetails.GovernrateId))
                .ForMember(x => x.CityId, c => c.MapFrom(x => x.AddressDetails.CityId))
                .ForMember(x => x.AddressDetails, c => c.MapFrom(x => x.AddressDetails.Details))
                .ForMember(x => x.NearestLandMark, c => c.MapFrom(x => x.AddressDetails.NearestSign))
                .ReverseMap();


            CreateMap<AddressDetails, GuarntorDTO>() //latest
                .ForMember(x => x.AddressDetailsId, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.GovernrateId, c => c.MapFrom(x => x.GovernrateId))
                .ForMember(x => x.CityId, c => c.MapFrom(x => x.CityId))
                .ForMember(x => x.NearestLandMark, c => c.MapFrom(x => x.NearestSign))
                .ForMember(x => x.AddressDetails, c => c.MapFrom(x => x.Details))
                .ReverseMap();


            CreateMap<JobDetails, JobDetailsDTO>().ReverseMap();
            CreateMap<InstallmentCard, CardDTO>()
              .ForMember(x => x.Id, c => c.MapFrom(x => x.Id))
              .ForMember(x => x.AddressDetailsId, c => c.MapFrom(x => x.Customer.AddressDetails.Id))
              .ForMember(x => x.JobDetailsId, c => c.MapFrom(x => x.Customer.JobDetails.Id))
              .ForMember(x => x.CustomerId, c => c.MapFrom(x => x.Customer.Id))
              .ForMember(x => x.GrantorId, c => c.MapFrom(x => x.Grantor.Id))
              .ForMember(x => x.IssueDate, c => c.MapFrom(x => x.IssueDate))
              .ForMember(x => x.ValidateToDate, c => c.MapFrom(x => x.ValidateToDate))
              .ForMember(x => x.IsActive, c => c.MapFrom(x => x.IsActive))
              .ForMember(x => x.Name, c => c.MapFrom(x => x.Customer.Name))
              .ForMember(x => x.NationalId, c => c.MapFrom(x => x.Customer.NationalId))
              .ForMember(x => x.MobileNumber, c => c.MapFrom(x => x.Customer.MobileNumber))
              .ReverseMap();
              








        }
    }
}
