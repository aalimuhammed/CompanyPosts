namespace CompanyPost.Infrastructure.Configuration;
internal sealed class ContractAttachmentsConfiguration : IEntityTypeConfiguration<ContractAttachments>
{
	public void Configure(EntityTypeBuilder<ContractAttachments> builder)
	{
		//builder.HasKey(e => e.Id);

		builder.HasOne(builder => builder.Contracts)
				.WithMany(t => t.ContractAttachments)
				.HasForeignKey(builder => builder.ContractID)
				.OnDelete(DeleteBehavior.Restrict);
	}
}