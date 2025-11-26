namespace CompanyPost.Domain.Enums;
public enum DocumentType
{
	[Display (Name = "إيميل")]
	Email = 1,

	[Display (Name = "مذكرة داخلية")]
	InternalLetter = 2,

	[Display (Name = "طلب شراء")]
    PurchaseRequisition = 3,
}