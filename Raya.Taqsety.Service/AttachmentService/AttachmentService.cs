using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.AttachmentRepository;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.AttachmentService
{
    public class AttachmentService : IAttachmentServie
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public AttachmentService(ApplicationDbContext applicationDbContext,
            IWebHostEnvironment hostingEnvironment,
            IAttachmentRepository attachmentRepository,
            IMapper mapper
            ) 
        {
            _hostingEnv= hostingEnvironment;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> HardDeleteAllAttachmentByCustomerId(int customerId)
        {
            var customerAttachments = await _attachmentRepository.GetAllCustomerAttachments(customerId);
            foreach (var item in customerAttachments)
            {
                var isDeleted = await _attachmentRepository.HardDeleteAsync(item);
                if (!isDeleted)
                    return false;
            }
            return true;
        }

        public string UploadFile(string fileName,string bas64)
        {
            var fileInBytes = Convert.FromBase64String(bas64);
            var newFileName = string.Concat($"{DateTime.Now.Ticks}__", fileName);
            string url = $"{_hostingEnv.WebRootPath}\\Uploads\\Images\\{newFileName}";
            try
            {
                DrawImageFromBase64(bas64, url);
                //await File.WriteAllBytesAsync(url, fileInBytes);
                return newFileName;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public void DrawImageFromBase64(string base64, string filename)  // Drawing image from Base64 string.
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }//tested

        public Task<Core.Models.Attachment?> InsertAttachment(Core.Models.Attachment attachment)
        {
            return _attachmentRepository.InsertAsync(attachment);
        }   

        public async Task<List<AttachmentDTO>> GetAllCustomerAttachments(int customerId)
        {
            var attachments = await _attachmentRepository.GetAllCustomerAttachments(customerId);
            List<AttachmentDTO> attachmentDTOs = new();
            foreach (var item in attachments)
            {
                AttachmentDTO attachmentToShow = new();
                attachmentToShow.AttachmentTypeId =(int) item.AttachmentTypeId;
                attachmentToShow.Id = item.Id;
                attachmentToShow.ImageBase64 = GetAttachmentsBase64(item.AttachmentName);
                attachmentDTOs.Add(attachmentToShow);
            }
            return attachmentDTOs;
        }

        private string? GetAttachmentsBase64(string name)
        {
            var filebyteArr = File.ReadAllBytes($"{_hostingEnv.WebRootPath}\\Uploads\\Images\\{name}");
            var base64String = Convert.ToBase64String(filebyteArr);

            return "data:image/" + name.Split(".")[1] +";base64," + base64String; //to add src="data:image/png;base64, before the base 64 string
        }

        public async Task<ResultModel> UpdateCustomerAttachments(CustomerAttachmentsDTO customerAttachmentsDTO)
        {
            Core.Models.Attachment? isFileUpdated = null;
            var allCustomerAttachments = await _attachmentRepository.GetAllCustomerAttachments((int)customerAttachmentsDTO.CustomerId);

            foreach (var newAttachment in customerAttachmentsDTO.Attachments)
            {
                var fileToDelete = allCustomerAttachments.FirstOrDefault(x => x.AttachmentTypeId == newAttachment.AttachmentTypeId);//get the old file
                if(fileToDelete !=null)
                DeleteFile(fileToDelete.AttachmentName);//delete the old file

                var newfileNeme = UploadFile(newAttachment.Name, newAttachment.ImageBase64);//upload the new file

                isFileUpdated = await _attachmentRepository.UpdateAsync(new Core.Models.Attachment() //update the database record with the new file name
                {
                    CustomerId = customerAttachmentsDTO.CustomerId,
                    AttachmentName = newfileNeme,
                    Id = (int)newAttachment.Id,
                    AttachmentTypeId = newAttachment.AttachmentTypeId
                });

            }
            if (isFileUpdated != null)
                return new ResultModel { Succeded = true, Message = "The file Updated succefully", Content = "" };
            return new ResultModel { Succeded = false, Message = "somthing went wrong please try again", Content = "" };

        }

        private void DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(Path.Combine($"{_hostingEnv.WebRootPath}\\Uploads\\Images\\", fileName)))// Check if file exists with its full path   
                {
                    File.Delete(Path.Combine($"{_hostingEnv.WebRootPath}\\Uploads\\Images\\", fileName));// If file found, delete it    
                }
            }
            catch (IOException ioExp)
            {
               
            }
        }
    }
}
