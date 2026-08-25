namespace CompanyPost.Domain.Interface;
public interface IHasCurrencyAndValue
{
    Currency Currency { get; set; }
    double Value { get; set; }
}
