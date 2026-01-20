using CompanyPost.Domain.Enums;

namespace CompanyPost.Infrastructure.Repository;
internal class CurrencyRepository<T>
    : GenericRepository<T>, ICurrencyRepository<T>
    where T : BaseEntity, IHasCurrencyAndValue
{
    public CurrencyRepository(CompanyPostDbContext context) : base(context) { }
    public async Task<double> SumForCurrency(Currency currency, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(x => x.Currency == currency)
            .SumAsync(x => x.Value, cancellationToken);
    }
}
