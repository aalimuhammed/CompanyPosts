namespace CompanyPost.Domain.Entities;
public class InComing : BaseEntity , IDocumentEntity , IHasSharedProperty
{
	public int SerialNumber { get; set; }
	public string DocumentNumber { get; set; } = null!;
	public string? Subject { get; set; }
	public DateTime DocumentDate { get; set; }
	public DateTime DeliveryDate { get; set; }
	public string? Summary { get; set; }
	public string? Notes { get; set; }
	public DeliveryMethods DeliveryMethods { get; set; }
	public DocumentType DocumentType { get; set; }
	public Guid ProjectId { get; set; }
	public Publisher Projects { get; set; }  = null!;
	public Guid OriginalPublisherId { get; set; }
	public Publisher OriginalPublisher { get; set; } = null!;
	public DateTime SaveDate { get; set; }
	public Guid PublishedId { get; set; }
	public Publisher Publisher { get; set; } = null!;
	public Guid CreatedById { get; set; }
	public SysUsers CreatedBy { get; set; } = null!;
	public Departments Department { get; set; }
	public ICollection<InComingAttachments> IncomingAttachments { get; set; } = new List<InComingAttachments>();
	public ICollection<InComingResponsibleEmployee> inComingResponsibleEmployees = new List<InComingResponsibleEmployee>();
}