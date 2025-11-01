namespace CompanyPost.Domain.Entities;
public class WorkType : BaseEntity, IHasName
{
	public string Name { get; set; } = null!;
	public string Code { get; set; } = null!;
	public ICollection<Contracts> Contracts = new List<Contracts>();
	public ICollection<ContractRef> ContractRefs = new List<ContractRef>();
	public ICollection<InComing> InComings = new List<InComing>();
	public ICollection<PostExternal> PostExternals = new List<PostExternal>();
	public ICollection<PostInternal> PostInternals = new List<PostInternal>();
	public ICollection<PostTransformer> PostTransformers = new List<PostTransformer>();
}