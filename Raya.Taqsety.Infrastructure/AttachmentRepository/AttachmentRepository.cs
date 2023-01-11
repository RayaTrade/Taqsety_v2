using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.AttachmentRepository
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AttachmentRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<List<Attachment>> GetAllCustomerAttachments(int customerId)
        {
            return await _context.Attachments
                .AsNoTracking()
                .Where(x => x.CustomerId == customerId)
                .Include(x=>x.AttachmentType)
                .ToListAsync();
        }
        
    }
}
