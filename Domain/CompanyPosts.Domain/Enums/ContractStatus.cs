namespace CompanyPost.Domain.Enums;
public enum ContractStatus
{
	[Display (Name = "مالي")]
	Finance = 1 ,
	[Display (Name = "مورد")]
	Vendor = 2,
	[Display (Name = "مشروع")]
	Project = 3
}