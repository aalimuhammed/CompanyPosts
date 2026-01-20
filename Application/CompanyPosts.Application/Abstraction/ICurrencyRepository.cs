using CompanyPost.Domain.Interface;

namespace CompanyPost.Application.Abstraction;
public interface ICurrencyRepository<T> : IGenericRepository<T>
where T : BaseEntity, IHasCurrencyAndValue
{
    Task<double> SumForCurrency(Currency currency, CancellationToken cancellationToken = default);
}