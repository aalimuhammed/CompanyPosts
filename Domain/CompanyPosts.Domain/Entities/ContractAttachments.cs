namespace CompanyPost.Domain.Entities;
public class ContractAttachments : BaseEntity
{
	public Guid ContractID { get; set; }
	public Contracts Contracts { get; set; }
}