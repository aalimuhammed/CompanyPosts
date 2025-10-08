namespace CompanyPost.Application.Abstraction;
public interface IGenericRepository<T> where T : BaseEntity , IEntity
{
	Task<T> FindAsync(Expression<Func<T, bool>>? predicate , CancellationToken cancellationToken = default);
	Task<IReadOnlyList<T>> FindAllAsync(
		Expression<Func<T, bool>>? predicate = null,
		CancellationToken cancellationToken = default);
	Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default );
	Task<IEnumerable<T>> FindWithIncludeAsync(
	   Expression<Func<T, bool>>? predicate = null,
	   List<Expression<Func<T, object>>>? includes = null,
	   CancellationToken cancellationToken = default);
	   
	Task AddAsync(T entity , CancellationToken cancellationToken = default);
	void Update(T entity);
	void Delete(T entity);
}