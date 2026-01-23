namespace CompanyPost.Domain.Entities
{
    public class PurchaseOrder : BaseEntity, IHasSharedProperty , IHasCurrencyAndValue
    {
        public int SerialNumber { get; set; }
        public Departments Department { get ; set; }
        public double Value { get; set; }
        public string? Details { get; set; } = string.Empty;
        public string PurchaseOrderNumber { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime? PurchaseOrder_Date { get; set; }
        public Guid ProjectId { get; set; }
        public Publisher Projects { get; set; } = null!;
        public Guid PersonOrgId { get; set; }
        public Publisher PersonOrgs { get; set; } = null!;
        public Currency Currency { get; set; }
        public Guid CreatedById { get; set; }
        public SysUsers CreatedBy { get; set; } = null!;
        public Guid? WorkTypeId { get; set; }
        public WorkType? WorkType { get; set; } = null!;
        public string? OldReferenceNumber { get; set; } = string.Empty;
        public ICollection<PurchaseOrderAttachment> PurchaseOrderAttachments { get; set; } = new List<PurchaseOrderAttachment>();
        public Status Status { get; set; }
        public NatureOfWorks NatureOfWorks { get; set; }
        public double? CheckValue { get; set; }
        public string? CommericalRegisterId { get; set; }
        public ImportingStatus? ImportingStatus { get; set; }
    }
}