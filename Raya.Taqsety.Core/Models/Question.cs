using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Core.Models
{
    public class Question :BaseEntityWithDualLang
    {
        public int? JobTypeId { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType? JobType  { get; set; }

        public int? NextQuestionId { get; set; }
        [ForeignKey("NextQuestionId")]
        public Question NextQuestion { get; set; }


    }
}
