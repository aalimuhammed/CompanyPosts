using  CompanyPost.Application.DTO;
namespace CompanyPost.Application.ExcelConfigurations;

public static class ContractExcelColumns
{
    public static List<ExcelColumn<ContractExcelRowModel>> Columns =>
        new()
        {
            new()
            {
                ColumnName = "المشروع",
                ColumnValue = x => x.Project
            },
            new()
            {
                ColumnName = "رقم العقد",
                ColumnValue = x => x.ContractNum
            },
            new()
            {
                ColumnName = "الرقم التسلسلي",
                ColumnValue = x => x.SerialNum
            },
            new()
            {
                ColumnName = "نوع الأعمال",
                ColumnValue = x => x.WorkType
            },
            new()
            {
                ColumnName = "تاريخ العقد",
                ColumnValue = x => x.ContractDate
            },
            new()
            {
                ColumnName = "الإدارة",
                ColumnValue = x => x.Department
            },
            new()
            {
                ColumnName = "أمر الشراء",
                ColumnValue = x => x.PurchaseOrderRef
            },
            new()
            {
                ColumnName = "العملة",
                ColumnValue = x => x.Currency
            },
            new()
            {
                ColumnName = "الجهة",
                ColumnValue = x => x.PersonOrg
            },
            new()
            {
                ColumnName = "النوع",
                ColumnValue = x => x.Type
            },
            new()
            {
                ColumnName = "أنشأ بواسطة",
                ColumnValue = x => x.CreatedBy
            },
            new()
            {
                ColumnName = "تاريخ الإنشاء",
                ColumnValue = x => x.CreatedAt
            },
            new()
            {
                ColumnName = "تاريخ التسليم",
                ColumnValue = x => x.ApprovalDeliveryDate
            },
            new()
            {
                ColumnName = "تاريخ الاستلام",
                ColumnValue = x => x.DateOfReceipt
            }
        };
}