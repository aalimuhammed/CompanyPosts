namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostExternalConfiguration : IEntityTypeConfiguration<PostExternal>
{
	public void Configure(EntityTypeBuilder<PostExternal> builder)
	{
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

		builder.Property(x => x.DeliveryDate)
			.IsRequired();

		builder.Property(x => x.IncomingNumber)
				.HasMaxLength(50)
				.IsRequired();

		builder.HasOne(builder => builder.Publisher)
			.WithMany(t => t.PublishedPostExternals)
			.HasForeignKey(builder => builder.PublishedId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.ReceivedFromSupplier)
				.WithMany(t => t.RecievedPostExternals)
				.HasForeignKey(builder => builder.ReceivedFromSupplierId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => x.DocumentNumber)
			   .IsUnique();
	}
}