using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class CustomerClub :BaseEntitiy
    {
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int? ClubId { get; set; }
        [ForeignKey("ClubId")]
        public Club? Club { get; set; }
    }
}
