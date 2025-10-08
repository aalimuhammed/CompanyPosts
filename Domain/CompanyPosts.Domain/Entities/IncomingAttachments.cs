namespace CompanyPost.Domain.Entities;
public class IncomingAttachments : BaseEntity
{
	public Guid IncomingId { get; set; }
	public InComing Incoming { get; set; } = null!;
	public string FileName { get; set; } = null!;
}