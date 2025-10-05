namespace CompanyPost.Infrastructure.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity , IEntity
{
	private readonly CompanyPostDbContext _context;
	private readonly DbSet<T> _dbSet;
	public GenericRepository(CompanyPostDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}
	public async Task AddAsync(T entity , CancellationToken cancellationToken = default)
	{
		await _dbSet.AddAsync(entity , cancellationToken);
	}
	public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
	{
		return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
	}
	public void Update(T entity) => _dbSet.Update(entity);
	public void Delete(T entity) => _dbSet.Remove(entity);
	public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, 
		CancellationToken cancellationToken = default)
	{
		if (predicate != null)
		return await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
		
		return await _dbSet.FirstOrDefaultAsync(cancellationToken);
	}
	public async Task<IEnumerable<T>> FindWithIncludeAsync(
	   Expression<Func<T, bool>> predicate = null,
	   List<Expression<Func<T, object>>> includes = null,
	   CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = _context.Set<T>();

		if (predicate != null)
		{
			query = query.Where(predicate);
		}

		if (includes != null)
		{
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
		}

		return await query.ToListAsync(cancellationToken);
	}
	public async Task<IReadOnlyList<T>> FindAllAsync(
		Expression<Func<T, bool>> predicate = null,
		CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = _dbSet;
		if (predicate != null)
			query = query.Where(predicate);

		return await query.ToListAsync(cancellationToken);
	}
}