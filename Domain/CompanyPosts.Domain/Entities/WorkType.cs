namespace CompanyPost.Domain.Entities;
public class WorkType : BaseEntity, IHasName
{
	public string Name { get; set; } = null!;
	public string Code { get; set; } = null!;
	public ICollection<Contracts> Contracts { get; set; } = new List<Contracts>();
	public ICollection<ContractRef> ContractRefs { get; set; } = new List<ContractRef>();
	public ICollection<InComing> InComings { get; set; } = new List<InComing>();
	public ICollection<PostExternal> PostExternals { get; set; } = new List<PostExternal>();
	public ICollection<PostInternal> PostInternals { get; set; } = new List<PostInternal>();
	public ICollection<PostTransformer> PostTransformers { get; set; } = new List<PostTransformer>();
	public ICollection<PurchaseOrder> PurchaseOrdersWorkTypes { get; set; } = new List<PurchaseOrder>();
}