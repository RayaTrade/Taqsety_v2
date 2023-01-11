using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class MobileNumberVerification :BaseEntitiy
    {
        public string? MobileNumber { get; set; }
        public int Code { get; set; }

    }
}
