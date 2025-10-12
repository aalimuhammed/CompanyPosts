namespace CompanyPost.Domain.Enums;
public enum DeliveryMethods
{
	[Display (Name = "يدويا")]
	Manually = 1,
	[Display (Name = "إيميل")]
	Email = 2,
	[Display (Name = "فاكس")]
	Fax = 3,
}