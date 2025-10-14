namespace CompanyPost.Infrastructure.Configuration;
internal sealed class InComingAttachmentConfiguration
	: IEntityTypeConfiguration<InComingAttachments>
{
	public void Configure(EntityTypeBuilder<InComingAttachments> builder)
	{
		builder.HasOne(builder => builder.Incoming)
				.WithMany(t => t.IncomingAttachments)
				.HasForeignKey(builder => builder.IncomingId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.FileName)
			.HasMaxLength(100)
			.IsRequired();
	}
}
