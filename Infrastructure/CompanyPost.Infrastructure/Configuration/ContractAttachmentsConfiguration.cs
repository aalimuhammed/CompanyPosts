namespace CompanyPost.Infrastructure.Configuration;
internal sealed class ContractAttachmentsConfiguration : IEntityTypeConfiguration<ContractAttachments>
{
	public void Configure(EntityTypeBuilder<ContractAttachments> builder)
	{
		builder.HasOne(builder => builder.Contracts)
				.WithMany(t => t.ContractAttachments)
				.HasForeignKey(builder => builder.ContractID)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(builder => builder.ContractRef)
				.WithMany(t => t.ContractAttachments)
				.HasForeignKey(builder => builder.ContractRefId)
				.OnDelete(DeleteBehavior.Restrict);
	}
}