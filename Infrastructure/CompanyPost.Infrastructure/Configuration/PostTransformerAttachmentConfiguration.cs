namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostTransformerAttachmentConfiguration :
	IEntityTypeConfiguration<PostTransformerAttachment>
{
	public void Configure(EntityTypeBuilder<PostTransformerAttachment> builder)
	{
		builder.HasOne(builder => builder.PostTransformer)
				.WithMany(t => t.Attachments)
				.HasForeignKey(builder => builder.PostTransformerId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.Property(x => x.FileName)
			.HasMaxLength(100)
			.IsRequired();
	}
}