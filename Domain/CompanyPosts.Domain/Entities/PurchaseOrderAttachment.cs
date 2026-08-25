namespace CompanyPost.Domain.Entities
{
    public class PurchaseOrderAttachment : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
