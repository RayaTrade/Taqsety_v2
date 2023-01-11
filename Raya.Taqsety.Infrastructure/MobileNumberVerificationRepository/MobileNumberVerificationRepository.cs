using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.MobileNumberVerificationRepository
{
    public class MobileNumberVerificationRepository : IMobileNumberVerificationRepository
    {
        private readonly ApplicationDbContext _context;
        public MobileNumberVerificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MobileNumberVerification?> GetMobileNumberVerificationMobileNumber(string mobileNumber)
        {
            return await _context.MobileNumberVerifications
                 .AsNoTracking()
                 .OrderByDescending(x=>x.Id)
                 .FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber);
        }

        public async Task<MobileNumberVerification?> InsertMobileNumberOTB(MobileNumberVerification mobileNumberVerification)
        {
           await _context.MobileNumberVerifications.AddAsync(mobileNumberVerification);
           var result =await  _context.SaveChangesAsync();
            if(result >0)
                return mobileNumberVerification;
            return null;

        }
    }
}
