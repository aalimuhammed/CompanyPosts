namespace CompanyPost.Infrastructure.Configuration;
internal sealed class InComingAttachmentConfiguration
	: IEntityTypeConfiguration<IncomingAttachments>
{
	public void Configure(EntityTypeBuilder<IncomingAttachments> builder)
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
