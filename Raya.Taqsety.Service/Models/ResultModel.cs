using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Service.Models
{
    public class ResultModel
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public object? Content { get; set; }

    }
}
