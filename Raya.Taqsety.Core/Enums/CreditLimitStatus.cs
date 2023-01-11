using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Enums
{
    public enum CreditLimitStatus
    {
        Waiting_for_operation_Confirmation = 1,
        Waiting_for_1st_Approval = 2,
        Waiting_For_Final_Approvel = 3,
        Approved = 4,
        Rejected = 5,
    }
}
