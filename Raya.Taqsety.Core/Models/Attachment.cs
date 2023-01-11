using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class Attachment :BaseEntitiy
    {
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public string AttachmentName { get; set; }
        public int? AttachmentTypeId { get; set; }
        [ForeignKey("AttachmentTypeId")]
        public AttachmentType? AttachmentType{ get; set; }

    }
}
