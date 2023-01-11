using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class Grantor :BaseEntitiy
    {
        public int? RelationId { get; set; }
        [ForeignKey("RelationId")]
        public Relation? Relation { get; set; }
        public string? Name { get; set; }
        public long? NationalId { get; set; }
        public string? MobileNumber { get; set; }
        public int? AddressDetailsId { get; set; }
        [ForeignKey("AddressDetailsId")]
        public AddressDetails? AddressDetails { get; set; }

    }
}
