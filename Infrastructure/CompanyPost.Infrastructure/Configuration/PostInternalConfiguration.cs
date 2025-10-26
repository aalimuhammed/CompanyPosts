namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostInternalConfiguration : IEntityTypeConfiguration<PostInternal>
{
	public void Configure(EntityTypeBuilder<PostInternal> builder)
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

		builder.HasOne(builder => builder.Publisher)
				.WithMany(t => t.PublishedPostInternals)
				.HasForeignKey(builder => builder.PublishedId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.RecievedFrom)
				.WithMany(t => t.RecievedPostInternals)
				.HasForeignKey(builder => builder.RecievedFromId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.WorkType)
		.WithMany(t => t.PostInternals)
		.HasForeignKey(builder => builder.WorkTypeId)
		.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => x.DocumentNumber)
			   .IsUnique();
	}
}