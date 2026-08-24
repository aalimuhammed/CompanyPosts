namespace CompanyPost.Domain.Entities;
public class InComing : BaseEntity, IDocumentEntity
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
	public Guid? ProjectId { get; set; }
	public Publisher Projects { get; set; }  = null!;
	//public Guid OriginalPublisherId { get; set; }
	//public Publisher OriginalPublisher { get; set; } = null!;
	//public DateTime SaveDate { get; set; }
	public Guid PublishedId { get; set; }
	public Publisher Publisher { get; set; } = null!;
	public Guid CreatedById { get; set; }
	public SysUsers CreatedBy { get; set; } = null!;
	public Guid? WorkTypeId { get; set; }
	public WorkType? WorkType { get; set; }
    public ICollection<InComingAttachments> IncomingAttachments { get; set; } = new List<InComingAttachments>();
    public string? OldReferenceNumber { get ; set ; }
	public PostDocumentTypes PostDocumentTypes { get; set; }

    public ICollection<InComingResponsibleEmployee> inComingResponsibleEmployees = new List<InComingResponsibleEmployee>();
    public string? InComingNumber { get; set; }
    public Status Status { get; set; }
	public string? OriginalSender { get; set; } = string.Empty;
    public string? AboutWork { get ; set; }
}