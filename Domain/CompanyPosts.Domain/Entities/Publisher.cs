namespace CompanyPost.Domain.Entities;
public class Publisher : BaseEntity , IHasName
{
	public string Name { get; set; } = null!;
	public bool IsDepartment { get; set; } = false;
	public bool IsProject { get; set; } = false;
	public bool IsSupplierOrSubContractor { get; set; } = false;
	public ICollection<PostInternal> PublishedPostInternals { get; set; } = new List<PostInternal>();
	public ICollection<PostInternal> RecievedPostInternals { get; set; } = new List<PostInternal>();
	public ICollection<PostExternal> PublishedPostExternals { get; set; } = new List<PostExternal>();
	public ICollection<PostExternal> RecievedPostExternals { get; set; } = new List<PostExternal>();
	public ICollection<PostTransformer> PublishedPostTransformers { get; set; } = new List<PostTransformer>();
	public ICollection<PostTransformer> RecievedPostTransformers { get; set; } = new List<PostTransformer>();
}