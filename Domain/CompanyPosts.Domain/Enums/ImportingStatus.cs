namespace CompanyPost.Domain.Enums;
public enum ImportingStatus
{
   [Display(Name = "بالكامل")]
   Completed = 1,

   [Display (Name = "جزئي")]
   Partial = 2,

   [Display (Name = "ملغاة")]
   Cancelled = 3
}