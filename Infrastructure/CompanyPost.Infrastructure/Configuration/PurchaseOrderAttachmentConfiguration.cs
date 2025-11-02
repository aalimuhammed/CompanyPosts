namespace CompanyPost.Infrastructure.Configuration
{
    internal sealed class PurchaseOrderAttachmentConfiguration
         : IEntityTypeConfiguration<PurchaseOrderAttachment>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderAttachment> builder)
        {
            builder.HasOne(builder => builder.PurchaseOrder)
                    .WithMany(t => t.PurchaseOrderAttachments)
                    .HasForeignKey(builder => builder.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.FileName)
                    .HasMaxLength(100)
                    .IsRequired();
        }
    }
}