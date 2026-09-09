namespace CompanyPost.Application.DTO;
public record ContractExcelRowModel(
    string? Project,
    string ContractNum,
    string SerialNum,
    string? WorkType,
    string ContractDate,
    string? Department,
    string? PurchaseOrderRef,
    string Currency,
    string? PersonOrg,
    string? Type,
    string CreatedBy,
    string CreatedAt,
    double Value,
    DateTime? ApprovalDeliveryDate,
    DateTime? DateOfReceipt
);