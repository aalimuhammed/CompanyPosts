namespace CompanyPost.Domain.Entities;
public class Contracts : BaseEntity, ISharedContractProperty , IHasCurrencyAndValue
{
	public int SerialNumber { get; set; }
	public double Value { get; set; }
	public string? Details { get; set; } = string.Empty;
	public string ContractNumber { get; set; } = string.Empty;
	public string? Notes { get; set; }
	public bool HasReference { get; set; }
	public DateTime Contract_Date { get; set; }
	public string? purchase_order_ref { get; set; } = string.Empty;
	public Guid ProjectId { get; set; }
	public Publisher Projects { get; set; } = null!;
	public Guid PersonOrgId { get; set; }
	public Publisher PersonOrgs { get; set; } = null!;
	public Currency Currency { get; set; }
	public Guid CreatedById { get; set; }
	public SysUsers CreatedBy { get; set; } = null!;
	public Departments Department { get; set; }
	public Guid WorkTypeId { get; set; }
	public WorkType WorkType { get; set; } = null!;
	public string? CommercialRegisterNumber { get; set; } = string.Empty;
	public string? OldReferenceNumber { get; set; } = string.Empty;
    public ICollection<ContractAttachments> ContractAttachments { get; set; } = new List<ContractAttachments>();
	public ICollection<ContractRef> ContractRefs { get; set; } = new List<ContractRef>();
    public Status Status { get ; set; }
}