using CompanyPost.Domain.Interface;

namespace CompanyPost.Application.Abstraction;
public interface ICurrencyRepository<T> : IGenericRepository<T>
where T : BaseEntity, IHasCurrencyAndValue
{
    Task<decimal> SumForCurrency(Currency currency, CancellationToken cancellationToken = default);
}