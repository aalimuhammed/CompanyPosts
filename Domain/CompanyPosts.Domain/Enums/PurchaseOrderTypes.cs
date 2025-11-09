namespace CompanyPost.Domain.Enums
{
    public enum PurchaseOrderTypes
    {

        [Display(Name = "توريد")]
        Supply = 1,

        [Display(Name = "توريد/تركيب")]
        SupplyOrInstallation = 2
    }
}