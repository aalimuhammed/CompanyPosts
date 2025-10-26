namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostTransformerConfiguration :
	 IEntityTypeConfiguration<PostTransformer>
{
	public void Configure(EntityTypeBuilder<PostTransformer> builder)
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

		builder.Property(x => x.PostNumber)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(x => x.RecivedByName)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(x => x.FollowingPerson)
			.HasMaxLength(100)
			.IsRequired();

		builder.HasOne(builder => builder.Publisher)
			.WithMany(t => t.PublishedPostTransformers)
			.HasForeignKey(builder => builder.PublishedId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.RecievedFrom)
				.WithMany(t => t.RecievedPostTransformers)
				.HasForeignKey(builder => builder.RecievedFromId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.WorkType)
		.WithMany(t => t.PostTransformers)
		.HasForeignKey(builder => builder.WorkTypeId)
		.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => x.DocumentNumber)
			   .IsUnique();
	}
}