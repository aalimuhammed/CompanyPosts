namespace CompanyPost.Domain.Enums
{
	public enum ContractTypes
	{
		[Display(Name = "اساسي")]
		Original = 1,
		[Display(Name = "ملحق")]
		HasReference = 2
	}
}
