namespace CompanyPost.Infrastructure.Configuration;
internal sealed class SysUsersConfiguration : IEntityTypeConfiguration<SysUsers>
{
	public void Configure(EntityTypeBuilder<SysUsers> builder)
	{
		builder.Property(builder => builder.UserName)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(builder => builder.Password)
			.HasMaxLength(255)
			.IsRequired();

		builder.Property(builder => builder.Email)
			.HasMaxLength(100)
			.IsRequired();

        builder.Property(builder => builder.HrCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(builder => builder.Name)
			.HasMaxLength(50)
			.IsRequired();

		//builder.Property(builder => builder.IsPasswordDefault)
		//	.HasDefaultValueSql("1");

        builder.HasOne(builder => builder.Company)
            .WithMany(t => t.SysUsers)
            .HasForeignKey(builder => builder.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}