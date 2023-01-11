using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.AttachmentRepository;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.AttachmentService
{
    public interface IAttachmentServie 
    {
        string UploadFile(string fileName, string bas64);
        Task<bool> HardDeleteAllAttachmentByCustomerId(int customerId);
        Task<Attachment?> InsertAttachment(Attachment attachment);
        Task<List<AttachmentDTO>> GetAllCustomerAttachments(int customerId);
        Task<ResultModel> UpdateCustomerAttachments(CustomerAttachmentsDTO  customerAttachmentsDTO);
        //Task<List<Attachment>> GetAllRequiredCustomerAttachments(int customerId);
    }   
}
