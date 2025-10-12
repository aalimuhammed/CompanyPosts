namespace CompanyPost.Domain.Enums;
public enum Currency
{
	[Display (Name = "دولار")]
	USD = 1,
	[Display (Name = "يورو")]
	EUR = 2,
	[Display (Name = "جنيه مصري")]
	EGP = 3,
	[Display (Name = "ريال سعودي")]
	SAR = 4
}