namespace CompanyPost.Domain.Base;
public abstract class PostBaseEntity : BaseEntity, IDocumentEntity
{
	public int SerialNumber { get; set; }
	public string DocumentNumber { get; set; } = null!;
	public Guid CompanyId { get; set; }
	public Company Company { get; set; } = null!;
	public Guid PublishedId { get; set; }
	public Publisher Publisher { get; set; } = null!;
    public Guid RecievedFromId { get; set; }
    public Publisher RecievedFrom { get; set; } = null!;
    public Guid WorkTypeId { get; set; }
    public WorkType WorkType { get; set; } = null!;
    public string? Subject { get; set; } = null!;
	public string? AboutWork { get; set; } = null!;
	public DateTime DocumentDate { get; set; }
	public DateTime DeliveryDate { get; set; }
	public string? Summary { get; set; }
	public string? Notes { get; set; }
	public DeliveryMethods DeliveryMethods { get; set; }
	public Guid CreatedById { get; set; }
	public SysUsers CreatedBy { get; set; } = null!;
	public string? OldReferenceNumber { get; set; } = string.Empty;
	public PostDocumentTypes PostDocumentTypes { get; set; }
    public string? InComingNumber { get; set; }
	public Status Status {  get; set; }
	public string? FollowingPerson { get; set; }
}