namespace CompanyPost.Domain.Enums;
public enum PostTransformerOptions
{
	[Display(Name = "المشاركة")]
	Sharing = 1,

	[Display(Name = "المتابعة")]
	Followed = 2,

	[Display(Name = "التصعيد")]
	Escalated = 3
}
