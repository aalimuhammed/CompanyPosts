namespace CompanyPost.Infrastructure.Configuration;
internal sealed class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
{
	public void Configure(EntityTypeBuilder<WorkType> builder)
	{
		builder.Property(x => x.Name)
			.HasMaxLength(100)
			.IsRequired();
	}
}