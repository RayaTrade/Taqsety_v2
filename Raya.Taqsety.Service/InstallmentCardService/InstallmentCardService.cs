using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Raya.Taqsety.Core.Enums;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.CustomerRepository;
using Raya.Taqsety.Infrastructure.InstallmentCardRepository;
using Raya.Taqsety.Infrastructure.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Service.AttachmentService;
using Raya.Taqsety.Service.CustomerService;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;


namespace Raya.Taqsety.Service.InstallmentCardService
{
    public class InstallmentCardService : InstallmentCardRepository, IInstallmentCardService
    {
        private readonly IInstallmentCardRepository _installmentCardRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<AddressDetails> _addressDetailsRepository;
        private readonly IRepository<Grantor> _grantorRepository;
        private readonly IRepository<JobDetails> _jobDetailsRepository;
        private readonly IRepository<CustomerClub> _CustomerClubRepository;
        private readonly IRepository<Club> _ClubRepository;
        private readonly IRepository<AttachmentType> _attachmentTypeRepository;
        private readonly IAttachmentServie _attachmentService;
        private readonly IMapper _mapper;
        private readonly InstallmentContext _installmentContext;

        public InstallmentCardService(IInstallmentCardRepository installmentCardRepository,
            ApplicationDbContext applicationDbContext,
            ICustomerService customerService,
            IRepository<AddressDetails> addresDetailsRepo,
            IRepository<Grantor> grantorRepository,
            IRepository<JobDetails> jobDetailsRepository,
            IMapper mapper,
            InstallmentContext installmentContext,
            IAttachmentServie attachmentService,
            IRepository<CustomerClub> customerClubRepository,
            IRepository<Club> clubRepository,
            IRepository<AttachmentType> attachmentTypeRepository

            ) : base(applicationDbContext)
        {
            _installmentCardRepository = installmentCardRepository;
            _customerService = customerService;
            _addressDetailsRepository = addresDetailsRepo;
            _grantorRepository = grantorRepository;
            _jobDetailsRepository = jobDetailsRepository;
            _mapper = mapper;
            _attachmentService = attachmentService;
            _CustomerClubRepository = customerClubRepository;
            _ClubRepository = clubRepository;
            _attachmentTypeRepository = attachmentTypeRepository;
            _installmentContext = installmentContext;

        }


        public async Task<ResultModel> CreateInstallmentCard(InstallmentCardDto installmentCardDto)
        {
            ResultModel? insertedCustomerId = await InsertCustomerData(installmentCardDto);
            if (insertedCustomerId.Succeded)
            {
                installmentCardDto.CustomerId = (int)insertedCustomerId.Content;
                bool IsAttachmentInserted = await InsertCustomerAttachments(installmentCardDto);
                if (!IsAttachmentInserted)
                {
                    var removeAllAddedFileToThisCustomer = await _attachmentService.HardDeleteAllAttachmentByCustomerId(installmentCardDto.CustomerId);
                    if (!removeAllAddedFileToThisCustomer)
                        return new ResultModel { Succeded = false, Message = "Somthing Went Wrong With the attachments", Content = "" };
                    return new ResultModel { Succeded = false, Message = "Attachments RollBack Failed", Content = "" };
                }
                ResultModel insertedGrantorData = null;
                if (installmentCardDto.GrantorNationalId != null)
                    insertedGrantorData = await InsertGrantorDate(installmentCardDto);

                if (insertedGrantorData == null || insertedGrantorData.Succeded || installmentCardDto.GrantorNationalId == 0)
                {
                    var installmentCardToInsert = CalCulatedInstallmentCard(installmentCardDto);
                    installmentCardToInsert.GrantorId = (insertedGrantorData == null) ? null : (int?)insertedGrantorData.Content;
                    var insertedInstallmentCard = await _installmentCardRepository.InsertAsync(installmentCardToInsert);
                    if (insertedInstallmentCard != null)
                    {
                        //installment creditlimit 
                        //await insertInstallmentCustomerCreditLimit(installmentCardDto);
                        return new ResultModel { Succeded = true, Message = "Card Created Succfully", Content = installmentCardToInsert };

                    }
                }
                else
                    return new ResultModel { Succeded = false, Message = insertedGrantorData.Message, Content = "" };

            }
            else
                return new ResultModel { Succeded = false, Message = insertedCustomerId.Message, Content = "" };
            return new ResultModel { Succeded = false, Message = "Somthing Went Wrong Please Try Again", Content = "" };
        }
        public async Task<bool> insertInstallmentCustomerCreditLimit(InstallmentCardDto installmentCardDto)
        {

            _installmentContext.CreditLimitRequests.Add(new CreditLimitRequest()
            {
                ClCustomerName = installmentCardDto.CustomerName,
                ClStatus = 6,
                ClNationalId = installmentCardDto.CustomerNationalId.ToString(),
                ClCreatedDate = DateTime.Now,
                ClIsCorporate = false,
                ClCreditLimit = installmentCardDto.ApprovedCreditLimit,
                ClMobileNumber = installmentCardDto.CustomerMobileNumber,
                ClCreditLimitMonthly = installmentCardDto.ApprovedMonthlyCreditLimit,
                ClCreditLimitMonths = installmentCardDto.ApprovedMaxMonths,
                ClSalary = installmentCardDto.Salary,
                ClExpiredDate= DateTime.Now.AddDays(365),
                ClUserIdOutSource = 99999,
                
            });
            return await _installmentContext.SaveChangesAsync() > 0;
        }

        private async Task<bool> InsertCustomerAttachments(InstallmentCardDto installmentCardDto)
        {
            if (installmentCardDto.Attachments != null && installmentCardDto.Attachments.Count() > 0)
            {
                List<AttachmentDTO> attachments = installmentCardDto.Attachments;
                foreach (var item in attachments)
                {
                    var isAttachmentInserted =  _attachmentService.UploadFile(item.Name,item.ImageBase64);
                    if (string.IsNullOrEmpty(isAttachmentInserted))
                        return false;
                    else
                    {
                        await _attachmentService.InsertAttachment(new Attachment()
                        {
                            CustomerId = installmentCardDto.CustomerId,
                            AttachmentTypeId = item.AttachmentTypeId,
                            AttachmentName = isAttachmentInserted
                        });
                    }

                }
                return true;
            }
            return true;
        }
        private InstallmentCard CalCulatedInstallmentCard(InstallmentCardDto installmentCardDto)
        {
            installmentCardDto.ApprovedMonthlyCreditLimit = Convert.ToInt32(installmentCardDto.Salary * 0.35);
            installmentCardDto.ApprovedCreditLimit = Convert.ToInt32(installmentCardDto.ApprovedMonthlyCreditLimit * 48);
            installmentCardDto.ValidateToDate = DateTime.Now;
            installmentCardDto.IssueDate = DateTime.Now.AddYears(1);
            installmentCardDto.IsActive = false;
            installmentCardDto.ApprovedMaxMonths = 12;

            // var installmentCardToInsert = _mapper.Map<InstallmentCard>(installmentCardDto);

            return new InstallmentCard()
            {
                CustomerId = installmentCardDto.CustomerId,
                GrantorId = installmentCardDto.GrantorId,
                ApprovedCreditLimit = installmentCardDto.ApprovedCreditLimit,
                ApprovedMaxMonths = installmentCardDto.ApprovedMaxMonths,
                ApprovedMonthlyCreditLimit = installmentCardDto.ApprovedMonthlyCreditLimit,
                StatusId =null,
                IsActive = installmentCardDto.IsActive,
                IssueDate = installmentCardDto.IssueDate,
                ValidateToDate = installmentCardDto.ValidateToDate
            };

        }

        private InstallmentCard CalCulatedInstallmentCard(InstallmentCard installmentCardDto)
        {
            installmentCardDto.ApprovedMonthlyCreditLimit = Convert.ToInt32(installmentCardDto.Customer.JobDetails.Salary * 0.35);
            installmentCardDto.ApprovedCreditLimit = Convert.ToInt32(installmentCardDto.ApprovedMonthlyCreditLimit * 48);
            installmentCardDto.ValidateToDate = DateTime.Now;
            installmentCardDto.IssueDate = DateTime.Now.AddYears(1);
            installmentCardDto.IsActive = false;
            installmentCardDto.ApprovedMaxMonths = 12;
            var installmentCardToInsert = _mapper.Map<InstallmentCard>(installmentCardDto);
            return installmentCardToInsert;

        }

        private async Task<ResultModel> InsertGrantorDate(InstallmentCardDto installmentCardDto)
        {
            if (installmentCardDto.GrantorNationalId != 0)
            {
                var grantorAddressDetails = _mapper.Map<GrantorAddressDetailsModel>(installmentCardDto);
                var grantorAddressDetailsToInsert = _mapper.Map<AddressDetails>(grantorAddressDetails);
                var insertedGrantorAddressDetails = await _addressDetailsRepository.InsertAsync(grantorAddressDetailsToInsert);
                if (insertedGrantorAddressDetails == null)
                    return new ResultModel { Succeded = false, Message = "Somthing Went Wrong with Grantor Address Data", Content = "" };

                var gratntorToInsert = _mapper.Map<Grantor>(installmentCardDto);
                gratntorToInsert.AddressDetailsId = insertedGrantorAddressDetails.Id;
                installmentCardDto.GrantorAddressDetailsId = insertedGrantorAddressDetails.Id;
                var insertedGrantor = await _grantorRepository.InsertAsync(gratntorToInsert);
                if (insertedGrantor != null)
                {
                    return new ResultModel { Succeded = true, Message = "", Content = insertedGrantor.Id };
                }
                else
                {
                    var isGrantorAddressDetailsRemoved = await _addressDetailsRepository.HardDeleteAsync(grantorAddressDetailsToInsert);
                    if (isGrantorAddressDetailsRemoved)
                    {
                        return new ResultModel { Succeded = true, Message = "Grantor AddressDetails RollBack Failed", Content = "" };
                    }
                    return new ResultModel { Succeded = true, Message = "Somthing Went Wrong With Grantor Data", Content = "" };
                }
            }
            return new ResultModel { Succeded = true, Message = "", Content = null };
        }

        private async Task<ResultModel> InsertCustomerData(InstallmentCardDto installmentCardDto)
        {

            var addressDetails = _mapper.Map<AddressDetails>(installmentCardDto);
            var insertAddressDetails = await _addressDetailsRepository.InsertAsync(addressDetails);
            if (insertAddressDetails != null)
            {
                var customerToInsert = _mapper.Map<Customer>(installmentCardDto);
                customerToInsert.AddressDetailsId = insertAddressDetails.Id;
                installmentCardDto.CustomerJobDetailsId = insertAddressDetails.Id;
                var jobDetails = _mapper.Map<JobDetails>(installmentCardDto);
                var insertedJobDetails = await _jobDetailsRepository.InsertAsync(jobDetails);
                if (insertedJobDetails == null)
                    return new ResultModel { Succeded = false, Message = "Somthing went wrong While adding job details", Content = "" };



                var isValidCustomer = await IsValidCustomer(customerToInsert);
                if (!isValidCustomer.Succeded)
                {
                    //await _addressDetailsRepository.HardDeleteAsync(insertAddressDetails);
                    //await _jobDetailsRepository.HardDeleteAsync(insertedJobDetails);
                    return new ResultModel { Succeded = false, Message = isValidCustomer.Message, Content = "" };
                }

                customerToInsert.JobDetailsId = insertedJobDetails.Id;
                var insertedCustomer = await _customerService.InsertAsync(customerToInsert);
                if (installmentCardDto.ClubsIds != null && installmentCardDto.ClubsIds.Count() > 0)
                {
                    var isClubsInserted = await InsertClubsToCustomer(installmentCardDto.ClubsIds.Select(x => x.Id).ToList(), insertedCustomer.Id);
                }
                if (insertedCustomer != null)
                    return new ResultModel { Succeded = true, Message = "Customer Created Succefully", Content = insertedCustomer.Id };

                //here

                else
                {
                    var isAddressDetailRemoved = await _addressDetailsRepository.HardDeleteAsync(addressDetails);
                    var isJobDetailsRemoved = await _jobDetailsRepository.HardDeleteAsync(jobDetails);
                    if (isAddressDetailRemoved && isJobDetailsRemoved)
                        return new ResultModel { Succeded = false, Message = "Roll Back Failed", Content = null };
                    return new ResultModel { Succeded = false, Message = "Somthing Went Wrong Please Try Again", Content = null };
                }
            }
            else
                return new ResultModel { Succeded = false, Message = "Somthing Went Wrong With Customer Address Details", Content = null };
        }
        private async Task<List<CustomerClub>?> InsertClubsToCustomer(List<int> clubsIds, int CutomerId)
        {
            List<CustomerClub> toInsertList = new() { };
            foreach (var item in clubsIds)
            {
                toInsertList.Add(new()
                {
                    CustomerId = CutomerId,
                    ClubId = item,
                    CreationDate = DateTime.Now,
                });
            }
            var isCustomerCulbInserted = await _CustomerClubRepository.InsertBulkAsync(toInsertList);
            return isCustomerCulbInserted;
        }

        public async Task<ResultModel> IsValidCustomer(Customer customer)
        {
            var isNationaIdUsedBeforeResult = await _customerService.CheckIfMobileNumberUsedBefore(customer.MobileNumber);
            if (isNationaIdUsedBeforeResult)
                return new ResultModel { Succeded = false, Message = "This Mobile number is already Used before", Content = null };

            bool checkIfNationalIdIsValid = IsValidNationalId(customer.NationalId);
            if (!checkIfNationalIdIsValid)
                return new ResultModel { Succeded = false, Message = "This National Id is invalid", Content = null };

            var isMobileNumberUsedBeforeResult = await _customerService.GetCustomerByNationalId(customer.NationalId) != null;
            if (isMobileNumberUsedBeforeResult)
                return new ResultModel { Succeded = false, Message = "This National Id is already Used before", Content = null };

            return new ResultModel { Succeded = true, Message = "Customer Is valid", Content = null };

        }

        public bool IsValidNationalId(long nationalId)
        {
            Regex regex = new(@"(?<BirthMillennium>[23])\x20?(?:(?<BirthYear>[0-9]{2})\x20?(?:(?:(?<BirthMonth>0[13578]|1[02])\x20?(?<BirthDay>0[1-9]|[12][0-9]|3[01]))\x20?|(?:(?<BirthMonth>0[469]|11)\x20?(?<BirthDay>0[1-9]|[12][0-9]|30))\x20?|(?:(?<BirthMonth>02)\x20?(?<BirthDay>0[1-9]|1[0-9]|2[0-8]))\x20?)|(?:(?<BirthYear>04|08|[2468][048]|[13579][26]|(?<=3)00)\x20?(?<BirthMonth>02)\x20?(?<BirthDay>29)\x20?))(?<ProvinceCode>0[1-34]|[12][1-9]|3[1-5]|88)\x20?(?<RegistryDigit>[0-9]{3}(?<GenderDigit>[0-9]))\x20?(?<CheckDigit>[0-9])");
            return regex.IsMatch(nationalId.ToString());
        }

        public async Task<InstallmentCardDto?> GetInstallmentCardByMobileNumber(string mobileNumber, string lang)
        {
            InstallmentCard? installmentCard = await _installmentCardRepository.GetInstallmentByMobileNumber(mobileNumber);
            if (installmentCard == null)
                return null;
            var installmentDto = FillingDataToInstallmentCardDto(installmentCard, lang);
            installmentDto.ClubsIds = await FillingClubsNames(installmentCard.Customer.Id, lang);
            FillingCustomerData(ref installmentDto, installmentCard.Customer, lang);
            if (installmentCard.Grantor != null)
                FillingGrantorData(ref installmentDto, installmentCard.Grantor, lang);

            installmentDto.AttachmentsOnDelivery = await FillingAttachments(installmentCard, lang);
            return installmentDto;
        }

        private async Task<List<string?>>   FillingAttachments(InstallmentCard installmentCard, string lang)
        {
            var CustomerAttachments = await _attachmentService.GetAllCustomerAttachments(installmentCard.Customer.Id);

            var allAttachments = await _attachmentTypeRepository.GetAll();

            //if (!installmentCard.Customer.HasCar)//exclude car licence if does not have car
            //    allAttachments.RemoveAt(3);


            //if (!installmentCard.Customer.HasMedicalinsurance)//exclude medical inssurance if does not have medical
            //    allAttachments.RemoveAt(2);

            List<string> lstAttachmentStillRequired = new();
            foreach (var attachment in allAttachments)
            {
                if (lang.ToLower().Contains("ar"))
                {
                    if (!CustomerAttachments.Any(x => x.AttachmentTypeId == attachment.Id))
                        lstAttachmentStillRequired.Add(attachment.ArabicName);
                }
                else
                {
                    if (!CustomerAttachments.Any(x => x.AttachmentTypeId == attachment.Id))
                        lstAttachmentStillRequired.Add(attachment.EnglishName);
                }
            }
            return lstAttachmentStillRequired;
        }

        private static void FillingGrantorData(ref InstallmentCardDto installmentDto, Grantor? grantor, string lang)
        {
            installmentDto.GrantorAddressDetails = grantor.AddressDetails.Details;
            installmentDto.GrantorAddressNearestLandmark = grantor.AddressDetails.NearestSign;
            installmentDto.GrantorAddressDetailsId = grantor.AddressDetails.Id;
            installmentDto.GrantorName = grantor.Name;
            installmentDto.GrantorNationalId = grantor.NationalId;
            installmentDto.GrantorMobileNumber = grantor.MobileNumber;

            if (lang.ToLower().Contains("ar"))
            {
                if (grantor.Relation != null)
                    installmentDto.GrantorRelationId = new LookupModel() { Name = grantor.Relation.ArabicName, Id = grantor.Relation.Id };
                if (grantor.AddressDetails != null)
                    installmentDto.GrantorCityId = new LookupModel() { Name = grantor.AddressDetails.City.ArabicName, Id = grantor.AddressDetails.City.Id };
                if (grantor.AddressDetails != null)
                    installmentDto.GrantorGovernrateId = new LookupModel() { Name = grantor.AddressDetails.Governrate.ArabicName, Id = grantor.AddressDetails.Governrate.Id };

            }
            else
            {
                if (grantor.Relation != null)
                    installmentDto.GrantorRelationId = new LookupModel() { Name = grantor.Relation.EnglishName, Id = grantor.Relation.Id };
                if (grantor.AddressDetails != null)
                    installmentDto.GrantorCityId = new LookupModel() { Name = grantor.AddressDetails.City.EnglishName, Id = grantor.AddressDetails.City.Id };
                if (grantor.AddressDetails != null)
                    installmentDto.GrantorGovernrateId = new LookupModel() { Name = grantor.AddressDetails.Governrate.EnglishName, Id = grantor.AddressDetails.Governrate.Id };
            }
        }

        private static void FillingCustomerData(ref InstallmentCardDto installmentDto, Customer? customer, string lang)
        {
            installmentDto.CustomerAddressDetailsId = customer.AddressDetails.Id;
            installmentDto.CustomerAddressDetails = customer.AddressDetails.Details;
            installmentDto.CustomerAddressNearestLandmark = customer.AddressDetails.NearestSign;
            installmentDto.yearsOfResidance = customer.AddressDetails.yearsOfResidance;
            installmentDto.Salary = customer.JobDetails.Salary;
            installmentDto.JobAddress = customer.JobDetails.JobAddress;
            installmentDto.CustomerJobDetailsId = customer.JobDetails.Id;
            installmentDto.CustomerMobileNumber = customer.MobileNumber;
            installmentDto.CustomerNationalId = customer.NationalId;
            installmentDto.CustomerName = customer.Name;
            installmentDto.CustomerId = customer.Id;
            installmentDto.CustomerJobTitle = customer.JobDetails.JobTitle;
            installmentDto.JobStartingDate = customer.JobDetails.StartingDate;

            if (lang.ToLower().Contains("ar"))
            {
                if (customer.AddressDetails.BuildingType == 1)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "عقار ملك", Id = 1 };
                if (customer.AddressDetails.BuildingType == 2)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "أيجار قديم", Id = 2 };
                if (customer.AddressDetails.BuildingType == 3)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "أيجار جديد", Id = 3 };

                installmentDto.CustomerCityId = new LookupModel() { Name = customer.AddressDetails.City.ArabicName, Id = customer.AddressDetails.City.Id };
                installmentDto.CustomerGovernrateId = new LookupModel() { Name = customer.AddressDetails.Governrate.ArabicName, Id = customer.AddressDetails.Governrate.Id };
                installmentDto.CustomerUniversityId = (customer.UniversityId != null) ? new LookupModel() { Name = customer.University.ArabicName, Id = customer.University.Id } : null;
                installmentDto.CustomerMartialStatus = new LookupModel() { Name = (customer.MartialStatus == 1) ? "أعزب" : "متزوج" ,Id = customer.MartialStatus};
                installmentDto.IsPrivateSectorDisplayName = (customer.JobDetails.IsPrivateSector) ? "قطاع خاص" : "قطاع عام";
            }
            else
            {
                if (customer.AddressDetails.BuildingType == 1)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "Owned", Id = 1 };
                if (customer.AddressDetails.BuildingType == 2)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "Old Rent", Id = 2 };
                if (customer.AddressDetails.BuildingType == 3)
                    installmentDto.CustomerBuildingType = new LookupModel() { Name = "New Rent", Id = 3 };

                installmentDto.CustomerCityId = new LookupModel() { Name = customer.AddressDetails.City.EnglishName, Id = customer.AddressDetails.City.Id };
                installmentDto.CustomerGovernrateId = new LookupModel() { Name = customer.AddressDetails.Governrate.EnglishName, Id = customer.AddressDetails.Governrate.Id };
                installmentDto.CustomerUniversityId = (customer.UniversityId != null) ? new LookupModel() { Name = customer.University.EnglishName, Id = customer.University.Id } : null;
                installmentDto.CustomerMartialStatus = new LookupModel() { Name = (customer.MartialStatus == 1) ? "Married" : "Single" ,Id = customer.MartialStatus};
                installmentDto.IsPrivateSectorDisplayName = (customer.JobDetails.IsPrivateSector) ? "Private Sector" : "Public Sector";


            }

        }

        private static InstallmentCardDto FillingDataToInstallmentCardDto(InstallmentCard installmentCard, string lang)
        {
            InstallmentCardDto installmentCardDto = new()
            {
                Id = installmentCard.Id,
                ApprovedCreditLimit = installmentCard.ApprovedCreditLimit,
                ApprovedMaxMonths = installmentCard.ApprovedMaxMonths,
                ApprovedMonthlyCreditLimit = installmentCard.ApprovedMonthlyCreditLimit,
                IsActive = installmentCard.IsActive,
                ValidateToDate = installmentCard.ValidateToDate,
                IssueDate = installmentCard.IssueDate,
                CustomerId = (int)installmentCard.CustomerId,
                GrantorId = installmentCard.GrantorId,
                
            };
            if (installmentCard.Status != null)
            {

                installmentCardDto.StatusId = new LookupModel()
                {
                    Id = installmentCard.Status.Id,
                    Name = (lang.ToLower().Contains("ar")) ? installmentCard.Status.ArabicName : installmentCard.Status.EnglishName
                };
            }
            return installmentCardDto;
        }


        private async Task<List<LookupModel>?> FillingClubsNames(int cutomerId, string lang)
        {
            var clubs = await _customerService.GetAllClubsByCustomerId(cutomerId);
            List<LookupModel> clubsList = new();
            if (clubs.Count > 0)
            {
                if (lang.ToLower().Contains("ar"))
                    foreach (var clubView in clubs)
                        clubsList.Add(new LookupModel() { Name = clubs.FirstOrDefault(x => x.Id == clubView.Id).ArabicName, Id = clubView.Id });
                else
                    foreach (var clubView in clubs)
                        clubsList.Add(new LookupModel() { Name = clubs.FirstOrDefault(x => x.Id == clubView.Id).EnglishName, Id = clubView.Id });
            }
            else
            {
                if (lang.ToLower().Contains("ar"))
                    clubsList.Add(new LookupModel() { Name = "غير مشترك", Id = 0 });
                else
                    clubsList.Add(new LookupModel() { Name = "No membership", Id = 0 });
            }
            return clubsList;
        }

        public async Task<ResultModel> UpdateCustomerName(int installmentCardId, string newCustomerFullName)
        {
            InstallmentCard? installmentIfExsict = await _installmentCardRepository.GetInstallmentById(installmentCardId);
            if (installmentIfExsict == null)
                return new ResultModel { Succeded = false, Message = "This Installment card does not exsist", Content = null };
            installmentIfExsict.Customer.Name = newCustomerFullName;
            var isUpdated = await _installmentCardRepository.UpdateAsync(installmentIfExsict);
            if (isUpdated != null)
                return new ResultModel { Succeded = true, Message = "Customer Name Updated ", Content = null };
            return new ResultModel { Succeded = false, Message = "Could Not Update Customer Name Somthing went wrong", Content = null };
        }

        public async Task<InstallmentCardDto?> GetInstallmentCardByNationalId(long nationalId, string lang)
        {
            InstallmentCard? installmentCard = await _installmentCardRepository.GetInstallmentByNationalId(nationalId);
            if (installmentCard == null)
                return null;
            var installmentDto = FillingDataToInstallmentCardDto(installmentCard, lang);
            installmentDto.ClubsIds = await FillingClubsNames(installmentCard.Customer.Id, lang);
            FillingCustomerData(ref installmentDto, installmentCard.Customer, lang);
            if (installmentCard.Grantor != null)
                FillingGrantorData(ref installmentDto, installmentCard.Grantor, lang);

            installmentDto.AttachmentsOnDelivery = await FillingAttachments(installmentCard, lang);
            return installmentDto;
        }

        public async Task<ResultModel> UpdateCustomerMaritialStatus(int v, int newmaritialStatus)
        {
            var customerToUpdate = await _installmentCardRepository.GetInstallmentById(v);
            if (customerToUpdate!= null && customerToUpdate.Customer != null)
            {
                customerToUpdate.Customer.MartialStatus = newmaritialStatus;
                var result = await _customerService.UpdateAsync(customerToUpdate.Customer);
                return new ResultModel { Succeded = true, Message = "Customer MariTial Status Updated ", Content = null };

            }
            return new ResultModel { Succeded = false, Message = "Customer MariTial Status didn't updated Please try again", Content = null };

        }

        public async Task<ResultModel> GetAllInstallmentCards()
        {
            var installmentCard = await _installmentCardRepository.GetAll();
            return new ResultModel { Succeded = true, Message = "InstallmentData", Content = installmentCard };
        }

        public async Task<CardDTO> UpdateInstallmentCard(CardDTO installmentCardDto)
        {
            var intallmentCardToUpdate =  _mapper.Map<InstallmentCard>(installmentCardDto);
            var cardToUpdate = await GetInstallmentById(intallmentCardToUpdate.Id);

            cardToUpdate.Customer.Name = intallmentCardToUpdate.Customer.Name;
            cardToUpdate.Customer.NationalId = intallmentCardToUpdate.Customer.NationalId;
            cardToUpdate.Customer.MobileNumber = intallmentCardToUpdate.Customer.MobileNumber;
            cardToUpdate.Customer.JobDetails.Salary = intallmentCardToUpdate.Customer.JobDetails.Salary;

            var result = CalCulatedInstallmentCard(cardToUpdate);

            var UpdatedInstallment = await _installmentCardRepository.UpdateAsync(result);
            var updatedDto = _mapper.Map<CardDTO>(UpdatedInstallment);
            return updatedDto;
        }

        public async Task<List<CardDTO>> GetAllCards(int roleId,int userId)
        {
            List<InstallmentCard> allInstallmentCard = null;
            if(roleId == 1)
                allInstallmentCard = await _installmentCardRepository.GetAllCards(new List<int> { 1,2,3,4,5 },userId); // all ststus
            else if (roleId == 2)//operation team
                allInstallmentCard = await _installmentCardRepository.GetAllCards(new List<int> { 1 },userId);//status Waiting for operation Confirmation
            else if (roleId == 3)//credit team
                allInstallmentCard = await _installmentCardRepository.GetAllCards(new List<int> { 2,3 },userId); //Waiting for 1st Approval && Waiting For Final Approvel

            List<CardDTO> allCards = _mapper.Map<List<CardDTO>>(allInstallmentCard);
            return allCards;
        }

        public async Task<InstallmentCard> UpdateInstallmentStatus(int installmentCardId, int newStatusId, int userId)
        {

            var installmentToUpdate = await _installmentCardRepository.Get(installmentCardId);
            if (installmentToUpdate != null)
            {
                installmentToUpdate.StatusId = newStatusId;
                installmentToUpdate.ModifiedBy = userId;
                installmentToUpdate.ModificationDate = DateTime.Now;

            }
            var isUpdated = await _installmentCardRepository.UpdateAsync(installmentToUpdate);
            if(newStatusId.Equals( (int) CreditLimitStatus.Approved))
            {
               var installmentCardData = await _installmentCardRepository.GetInstallmentById(installmentCardId);
                if (installmentCardData != null)
                {
                        var installmentCardDataDto = new InstallmentCardDto()
                        {
                            ApprovedCreditLimit = installmentCardData.ApprovedCreditLimit,
                            ApprovedMaxMonths = installmentCardData.ApprovedMaxMonths,
                            ApprovedMonthlyCreditLimit = installmentCardData.ApprovedMonthlyCreditLimit,
                            CustomerName = installmentCardData.Customer.Name,
                            CustomerNationalId = installmentCardData.Customer.NationalId,
                            Salary = installmentCardData.Customer.JobDetails.Salary,
                            CustomerMobileNumber = installmentCardData.Customer.MobileNumber
                            

                        };
                             await insertInstallmentCustomerCreditLimit(installmentCardDataDto);
                }
            }
            return isUpdated;
            
        }
    }
}
