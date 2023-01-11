using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.DTOs
{
    public class AttachmentDTO
    {
       
        public int? Id { get; set; }
        public int? AttachmentTypeId { get; set; }
        public string? Name { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
