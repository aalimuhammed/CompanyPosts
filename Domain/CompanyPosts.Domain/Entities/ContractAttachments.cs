namespace CompanyPost.Domain.Entities;
public class ContractAttachments : BaseEntity
{
	public Guid? ContractID { get; set; }
	public Contracts? Contracts { get; set; } = null!;

	public Guid? ContractRefId { get; set; } 
	public ContractRef? ContractRef { get; set; }
	public string FileName { get; set; } = null!;
}