using System;
namespace Raya.Taqsety.Service.DTOs
{
	public class CustomerAttachmentsDTO
	{
		public int? CustomerId { get; set; }
		public List<AttachmentDTO?>? Attachments { get; set; }

	}
}

