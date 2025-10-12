namespace CompanyPost.Domain.Entities;
public class ContractAttachments : BaseEntity
{
	public Guid ContractID { get; set; }
	public string FileName { get; set; } = null!;
	public Contracts Contracts { get; set; } = null!;
}