using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.ExcelConfigurations;
public static class IncomingExcelColumns
{
    public static List<ExcelColumn<PostDocumentsDTO>>Columns =>
        new()
        {
            new()
            {
                ColumnName = "الرقم التسلسلي",
                ColumnValue = x => x.SerialNumber
            },
            new()
            {
                ColumnName = "رقم المستند",
                ColumnValue = x => x.DocumentNumber
            },
            new()
            {
                ColumnName = "تاريخ المستند",
                ColumnValue = x => x.DocumentDate
            },
            new()
            {
                ColumnName = "تاريخ التسليم",
                ColumnValue = x => x.DeliveryDate
            },
            new()
            {
                ColumnName = "الموضوع",
                ColumnValue = x => x.Subject
            },
            new()
            {
                ColumnName = "الملخص",
                ColumnValue = x => x.Summary
            },
            new()
            {
                ColumnName = "ملاحظات",
                ColumnValue = x => x.Notes
            },
            new()
            {
                ColumnName = "أنشأ بواسطة",
                ColumnValue = x => x.CreatedBy
            },
            new()
            {
                ColumnName = "اسم الناشر",
                ColumnValue = x => x.PublishedName
            },
            new()
            {
                ColumnName = "طريقة التسليم",
                ColumnValue = x => x.DeliveryMethod
            },
            new()
            {
                ColumnName = "الشركة",
                ColumnValue = x => x.CompanyName
            },
            new()
            {
                ColumnName = "مُستلم من",
                ColumnValue = x => x.ReceivedFromName
            }
        };
}