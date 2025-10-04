namespace CompanyPost.Infrastructure.Configuration;
internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		//builder.HasKey(t => t.Id);

		builder.Property(builder => builder.Name)
				.HasMaxLength(100)
				.IsRequired();

		builder.Property(builder => builder.CompanyCode)
				.HasMaxLength(20)
				.IsRequired();

		builder.HasIndex(t => t.CompanyCode);
	}
}