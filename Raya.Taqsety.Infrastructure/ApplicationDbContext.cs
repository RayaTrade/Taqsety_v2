using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure
{
    public class ApplicationDbContext :IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MobileNumberVerification>().HasIndex(p => new { p.MobileNumber });

            modelBuilder.Entity<AttachmentType>().HasData(
                new AttachmentType { Id = 1, EnglishName = "Front NationalId",ArabicName = "الصورة الأمامية للبطاقة" },
                new AttachmentType { Id = 2, EnglishName = "Back NationalId",ArabicName = "الصورة الخلفية للبطاقة" },
                new AttachmentType { Id = 3, EnglishName = "Medical Card",ArabicName = " صورة بطاقة التامين الصحي " },
                new AttachmentType { Id = 4, EnglishName = "Car License" ,ArabicName =  "صورة رخصة السيارة"},
                new AttachmentType { Id = 5, EnglishName = "Club Card",ArabicName = "صورة كارنيه عضوية النادي" }
                );

            //authontication data seed
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.EmailConfirmed)
                                               .Ignore(c => c.TwoFactorEnabled)
                                               .Ignore(c => c.PhoneNumberConfirmed);


            modelBuilder.Entity<User>().ToTable("Users");


            modelBuilder.Entity<Role>().ToTable("Roles")
                .HasData(new Role { Id = 1, Name = "Admin", NormalizedName = "AdMIN" },
                         new Role { Id = 2, Name = "User", NormalizedName = "USER" });


            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            //authontication data seed

          

        }


        //public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<City> Cities{ get; set; }
        //public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<Governrate> Governrates{ get; set; }
        public virtual DbSet<Customer> Customers{ get; set; }
        public virtual DbSet<InstallmentCard> InstallmentCards{ get; set; }
        public virtual DbSet<Grantor> Grantors{ get; set; }
        public virtual DbSet<Question> Questions{ get; set; }
        public virtual DbSet<Answer> Answers{ get; set; }
        public virtual DbSet<JobDetails> JobDetails{ get; set; }
        public virtual DbSet<JobType> JobTypes{ get; set; }
        public virtual DbSet<Attachment> Attachments{ get; set; }
        public virtual DbSet<Status> Statuses{ get; set; }
        public virtual DbSet<GrantorRelation> GrantorRelations{ get; set; }
        public virtual DbSet<CustomerClub> CustomersClubs{ get; set; }
        public virtual DbSet<Club> Clubs{ get; set; }
        public virtual DbSet<University> Universities{ get; set; }
        public virtual DbSet<Relation> Relations{ get; set; }
        public virtual DbSet<MobileNumberVerification> MobileNumberVerifications{ get; set; }
        public virtual DbSet<AttachmentType> AttachmentTypes{ get; set; }
















    }
}
