namespace CompanyPost.Infrastructure.Configuration
{
    internal sealed class PurchaseOrderConfiguration
         : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.Property(builder => builder.Details)
            .HasMaxLength(100)
            .IsRequired(false);

            builder.Property(builder => builder.Notes)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(builder => builder.PurchaseOrderNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(builder => builder.Value)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(builder => builder.PurchaseOrder_Date)
                .IsRequired(false);

            builder.HasOne(builder => builder.Projects)
                .WithMany(t => t.PurchaseOrderProjects)
                .HasForeignKey(builder => builder.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(builder => builder.CreatedBy)
                .WithMany(t => t.PurchaseOrdersCreatedBy)
                .HasForeignKey(builder => builder.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(builder => builder.PersonOrgs)
                .WithMany(t => t.PurchaseOrdersPersonOrgs)
                .HasForeignKey(builder => builder.PersonOrgId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(builder => builder.WorkType)
                    .WithMany(t => t.PurchaseOrdersWorkTypes)
                    .HasForeignKey(builder => builder.WorkTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(builder => builder.Currency)
                .HasConversion<int>()
                .IsRequired(true);

            builder.HasIndex(builder => builder.PurchaseOrderNumber)
                .IsUnique();
        }
    }
}
