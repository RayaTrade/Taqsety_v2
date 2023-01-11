using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Taqsety.Infrastructure.AttachmentRepository
{
    public interface IAttachmentRepository :IRepository<Attachment>
    {
        Task<List<Attachment>> GetAllCustomerAttachments(int customerId);


    }
}
