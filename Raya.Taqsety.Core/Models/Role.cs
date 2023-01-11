using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raya.Taqsety.Core.Models
{
	
        public class Role : IdentityRole<int>
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public override int Id { get; set; }
            public DateTime CreationDate { get; set; }
            public int CreatedBy { get; set; }
            public DateTime ModificationDate { get; set; }
            public int ModifiedBy { get; set; }
            public bool IsDeleted { get; set; }
        }
    
}

