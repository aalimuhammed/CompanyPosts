using CompanyPost.Domain.Result;

namespace CompanyPost.Infrastructure.Repository;
internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity , IEntity
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
	public async Task<bool> FindAnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
		return await _dbSet.Where(predicate).AnyAsync(cancellationToken);
	}
	public void Update(T entity) => _dbSet.Update(entity);
	public void Delete(T entity) => _dbSet.Remove(entity);
	public async Task<T?> FindAsync(
		Expression<Func<T, bool>> predicate, 
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
	public async Task<PaginatedResult<T>> GetPagedAsync(
		int pageNumber, 
		int pageSize, 
		Expression<Func<T, bool>>? filter = null,
		List<Expression<Func<T, object>>> includes = null,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
		CancellationToken cancellationToken = default)
	{
		if (pageNumber <= 0) pageNumber = 1;
		if (pageSize <= 0) pageSize = 50;

		const int maxPageSize = 500;
		if (pageSize > maxPageSize) pageSize = maxPageSize;

		var query = _context.Set<T>().AsQueryable();

		if (includes != null)
		{
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
		}

		if (filter != null)
			query = query.Where(filter);

		var totalCount = await query.CountAsync(cancellationToken);

		if (orderBy != null)
			query = orderBy(query);

		var items = await query
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

		return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
	}
}