namespace CompanyPost.Infrastructure.Configuration
{
	internal sealed class ContractRefConfiguration
		: IEntityTypeConfiguration<ContractRef>
	{
		public void Configure(EntityTypeBuilder<ContractRef> builder)
		{
			builder.Property(builder => builder.SerialNumber)
				   .IsRequired();

			builder.HasOne(builder => builder.Contract)
					.WithMany(t => t.ContractRefs)
					.HasForeignKey(builder => builder.ContractId)
					.OnDelete(DeleteBehavior.Cascade);

			builder.Property(builder => builder.Details)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(builder => builder.Notes)
				.HasMaxLength(100);

			//builder.Property(builder => builder.purchase_order_ref)
			//	.HasMaxLength(100)
			//	.IsRequired();

			builder.Property(builder => builder.ContractNumber)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(builder => builder.Value)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(builder => builder.Contract_Date)
				.IsRequired();

			//builder.HasOne(builder => builder.Projects)
			//	.WithMany(t => t.ContractRefProjects)
			//	.HasForeignKey(builder => builder.ProjectId)
			//	.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(builder => builder.CreatedBy)
				.WithMany(t => t.ContractRefs)
				.HasForeignKey(builder => builder.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			//builder.HasOne(builder => builder.PersonOrgs)
			//	.WithMany(t => t.ContractRefPersonOrgs)
			//	.HasForeignKey(builder => builder.PersonOrgId)
			//	.OnDelete(DeleteBehavior.Restrict);

			//builder.HasOne(builder => builder.WorkType)
			//		.WithMany(t => t.ContractRefs)
			//		.HasForeignKey(builder => builder.WorkTypeId)
			//		.OnDelete(DeleteBehavior.Restrict);

			builder.Property(builder => builder.Currency)
				.HasConversion<int>()
				.IsRequired();

			builder.HasIndex(builder => builder.ContractNumber)
				.IsUnique();

			//builder.HasIndex(builder => builder.purchase_order_ref)
			//	.IsUnique();
		}
	}
}