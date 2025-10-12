namespace CompanyPost.Infrastructure.Configuration;
internal sealed class InComingResponsibleEmployeeConfiguration
	 : IEntityTypeConfiguration<InComingResponsibleEmployee>
{
	public void Configure(EntityTypeBuilder<InComingResponsibleEmployee> builder)
	{
		builder
			.HasKey(inr => new { inr.EmployeeId , inr.InComingId , inr.Id });

		builder
			.HasOne(bc => bc.Employees)
			.WithMany(b => b.inComingResponsibleEmployees)
			.HasForeignKey(bc => bc.EmployeeId);

		builder
			.HasOne(bc => bc.InComing)
			.WithMany(c => c.inComingResponsibleEmployees)
			.HasForeignKey(bc => bc.InComingId);
	}
}
