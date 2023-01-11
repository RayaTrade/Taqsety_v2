using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class Customer :BaseEntitiy
    {
        public string? Name { get; set; }
        public long NationalId { get; set; }
        public string? MobileNumber { get; set; }
        public int MartialStatus { get; set; }
        //public bool HasCar { get; set; }
        //public bool HasMedicalinsurance { get; set; }
        public string? MetaData { get; set; }
        public int? UniversityId { get; set; }
        [ForeignKey("UniversityId")]
        public University? University { get; set; }
        public int? AddressDetailsId { get; set; }
        [ForeignKey("AddressDetailsId")]
        public AddressDetails? AddressDetails { get; set; }
        public int? JobDetailsId { get; set; }
        [ForeignKey("JobDetailsId")]
        public JobDetails? JobDetails { get; set; }
     
    }
}
