namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PublisherConfiguration :
	IEntityTypeConfiguration<Publisher>
{
	public void Configure(EntityTypeBuilder<Publisher> builder)
	{
		builder.Property(x => x.Name)
			.HasMaxLength(100)
			.IsRequired();
	}
}
