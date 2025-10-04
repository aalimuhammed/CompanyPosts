namespace CompanyPost.Infrastructure.Configuration;
internal sealed class PersonOrgConfiguration : IEntityTypeConfiguration<PersonOrg>
{
	public void Configure(EntityTypeBuilder<PersonOrg> builder)
	{
		//builder.HasKey(builder => builder.Id);

		//builder.HasAlternateKey(builder => builder.SAP_BP);

		builder.Property(builder => builder.Name)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(builder => builder.Address)
			.HasMaxLength(100);

		builder.Property(builder => builder.Phone)
			.HasMaxLength(20);

		builder.Property(builder => builder.Email)
			.HasMaxLength(100);

		builder.Property(builder => builder.Contact_Person)
			.HasMaxLength(100);
	}
}