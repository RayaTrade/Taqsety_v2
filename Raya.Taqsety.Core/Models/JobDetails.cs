using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class JobDetails :BaseEntitiy
    {
        public string JobAddress { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public bool IsPrivateSector { get; set; }
        public DateTime? StartingDate { get; set; }
        public int JobTypeId { get; set; }
    }
}
