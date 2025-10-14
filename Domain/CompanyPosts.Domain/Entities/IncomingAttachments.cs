namespace CompanyPost.Domain.Entities;
public class InComingAttachments : BaseEntity
{
	public Guid IncomingId { get; set; }
	public InComing Incoming { get; set; } = null!;
	public string FileName { get; set; } = null!;
}