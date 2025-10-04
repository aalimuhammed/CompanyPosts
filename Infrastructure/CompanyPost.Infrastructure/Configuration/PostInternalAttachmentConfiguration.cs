
namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PostInternalAttachmentConfiguration : IEntityTypeConfiguration<PostInternalAttachment>
{
	public void Configure(EntityTypeBuilder<PostInternalAttachment> builder)
	{
		builder.HasOne(builder => builder.PostInternal)
		.WithMany(t => t.Attachments)
		.HasForeignKey(builder => builder.PostInternalId)
		.OnDelete(DeleteBehavior.Restrict);
	}
}
