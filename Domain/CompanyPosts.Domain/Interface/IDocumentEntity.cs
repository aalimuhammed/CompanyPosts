namespace CompanyPost.Domain.Interface;
public interface IDocumentEntity
{
    public int SerialNumber { get; set; }
    public string DocumentNumber { get; set; }
	public string? Subject { get; set; }
	public DateTime DocumentDate { get; set; }
	public DateTime DeliveryDate { get; set; }
	public string? Summary { get; set; }
	public string? Notes { get; set; }
	public DeliveryMethods DeliveryMethods { get; set; }
	public Guid PublishedId { get; set; }
	public Publisher Publisher { get; set; }
	public Guid CreatedById { get; set; }
	public SysUsers CreatedBy { get; set; }
	public string? OldReferenceNumber { get; set; }
    public string? InComingNumber { get; set; }
	public Status Status { get; set; }
}