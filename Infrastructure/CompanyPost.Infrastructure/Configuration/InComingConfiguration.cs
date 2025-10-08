namespace CompanyPost.Infrastructure.Configuration;
internal sealed class InComingConfiguration : IEntityTypeConfiguration<InComing>
{
	public void Configure(EntityTypeBuilder<InComing> builder)
	{
		builder.Property(x => x.DocumentNumber)
			.HasMaxLength(50)
			.IsRequired();
			
		builder.Property(x => x.SerialNumber)
			.IsRequired();

		builder.Property(x => x.Subject)
			.HasMaxLength(100);

		builder.Property(x => x.DocumentDate)
			.IsRequired();

		builder.Property(x => x.DeliveryDate)
			.IsRequired();

		builder.HasOne(builder => builder.Projects)
			.WithMany(t => t.IncomingProjects)
			.HasForeignKey(builder => builder.ProjectId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.OriginalPublisher)
			.WithMany(t => t.OriginalPublisherInComings)
			.HasForeignKey(builder => builder.OriginalPublisherId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.Publisher)
			.WithMany(t => t.PublishedInComings)
			.HasForeignKey(builder => builder.PublishedId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}