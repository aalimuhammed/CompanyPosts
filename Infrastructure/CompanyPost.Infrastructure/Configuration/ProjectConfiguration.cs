namespace CompanyPost.Infrastructure.Configuration;
internal class ProjectConfiguration : IEntityTypeConfiguration<Projects>
{
	public void Configure(EntityTypeBuilder<Projects> builder)
	{
		//builder.HasKey(builder => builder.Id);

		builder.Property(builder => builder.Name)
			.HasMaxLength(100)
			.IsRequired();
	}
}