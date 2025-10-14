namespace CompanyPost.Infrastructure.Configuration;
internal sealed class SysUsersCompanyConfiguration
	: IEntityTypeConfiguration<SysUsersCompany>
{
	public void Configure(EntityTypeBuilder<SysUsersCompany> builder)
	{
		builder
			.HasKey(inr => new { inr.SysUserId, inr.CompanyId, inr.Id });

		builder
			.HasOne(bc => bc.SysUser)
			.WithMany(b => b.SysUsersCompanies)
			.HasForeignKey(bc => bc.SysUserId);

		builder
			.HasOne(bc => bc.Company)
			.WithMany(c => c.SysUsersCompanies)
			.HasForeignKey(bc => bc.CompanyId);
	}
}
