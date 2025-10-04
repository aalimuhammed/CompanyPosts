namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostExternalConfiguration : IEntityTypeConfiguration<PostExternal>
{
	public void Configure(EntityTypeBuilder<PostExternal> builder)
	{
		//builder.HasKey(x => x.Id);

		builder.Property(x => x.DocumentNumber)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(x => x.SerialNumber)
			.IsRequired();

		builder.Property(x => x.Subject)
			.HasMaxLength(100);

		builder.Property(x => x.AboutWork)
			.HasMaxLength(50);

		builder.Property(x => x.DocumentDate)
			.IsRequired();

		builder.Property(x => x.DeliveryTime)
			.IsRequired();
	}
}