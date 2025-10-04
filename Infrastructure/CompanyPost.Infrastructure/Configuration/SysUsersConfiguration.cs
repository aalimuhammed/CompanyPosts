namespace CompanyPost.Infrastructure.Configuration;
internal sealed class SysUsersConfiguration : IEntityTypeConfiguration<SysUsers>
{
	public void Configure(EntityTypeBuilder<SysUsers> builder)
	{
		//builder.HasKey(builder => builder.Id);

		builder.Property(builder => builder.username)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(builder => builder.password)
			.HasMaxLength(255)
			.IsRequired();

		builder.Property(builder => builder.email)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(builder => builder.Name)
			.HasMaxLength(50)
			.IsRequired();
	}
}