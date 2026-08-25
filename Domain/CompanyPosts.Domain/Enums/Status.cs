namespace CompanyPost.Domain.Enums;
public enum Status
{
    [Display(Name = "مكتمل")]
    Completed = 1 ,

    [Display(Name = "قيد التنفيذ")]
    InProgress = 2 ,

    [Display(Name = "مرفوض")]
    Rejected = 3 ,

    [Display(Name = "معتمد")]
    Approved = 4 ,

    [Display(Name = "قيد المراجعة")]
    UnderRevision = 5,

    [Display (Name = "لا شئ")]
    Nothing = 6
}