namespace CompanyPost.Application.Abstraction
{
    public class ExcelColumn<T>
    {
        public string ColumnName { get; set; } = string.Empty;

        public Func<T, object?> ColumnValue { get; set; } = default!;
    }
}
