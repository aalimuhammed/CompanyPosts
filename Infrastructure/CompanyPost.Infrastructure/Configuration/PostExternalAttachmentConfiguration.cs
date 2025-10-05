namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostExternalAttachmentConfiguration : 
	IEntityTypeConfiguration<PostExternalAttachment>
{
	public void Configure(EntityTypeBuilder<PostExternalAttachment> builder)
	{
		builder.HasOne(builder => builder.PostExternal)
				.WithMany(t => t.Attachments)
				.HasForeignKey(builder => builder.PostExternalId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.FileName)
			.HasMaxLength(100)
			.IsRequired();
	}
}