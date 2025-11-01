namespace CompanyPost.Infrastructure.Data;
public class CompanyPostDbContext : DbContext
{
	public CompanyPostDbContext(DbContextOptions<CompanyPostDbContext> options) 
		: base(options)
	{
	}
	public DbSet<PostInternal> PostInternals { get; set; }
	public DbSet<PostInternalAttachment> PostInternalAttachments { get; set; }
	public DbSet<PostExternal> PostExternals { get; set; }
	public DbSet<PostExternalAttachment> PostExternalAttachments { get; set; }
	public DbSet<PostTransformer> PostTransformers { get; set; }
	public DbSet<PostTransformerAttachment> PostTransformerAttachments { get; set; }
	public DbSet<Contracts> Contracts { get; set; }
	public DbSet<SysUsers> SysUsers { get; set; }
	public DbSet<Company> Companies { get; set; }
	public DbSet<Publisher> Publishers { get; set; }
	public DbSet<WorkType> WorkTypes { get; set; }
	public DbSet<InComingResponsibleEmployee> inComingResponsibleEmployees { get; set; }
	public DbSet<SysUsersCompany> SysUsersCompanies { get; set; }
	public DbSet<BridgeUsers> BridgeUsers { get; set; }
	public DbSet<ContractRef> ContractRefs { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyPostDbContext).Assembly);
		modelBuilder.Ignore<PostBaseEntity>();
		base.OnModelCreating(modelBuilder);
	}
}