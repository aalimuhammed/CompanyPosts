using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace CompanyPost.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
	private bool _disposed = false;
	protected IServiceProvider ServiceProvider { get; }
	protected CompanyPostDbContext context { get; }
	public UnitOfWork(IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;
		context = ServiceProvider.GetService<CompanyPostDbContext>()!;
	}
	public async Task<IDbTransaction> BeginTransactionAsync()
	{
		var transaction = await context.Database.BeginTransactionAsync();
		return transaction.GetDbTransaction();
	}
	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await context.SaveChangesAsync(cancellationToken);
	}
	public IGenericRepository<T> Repository<T>() 
		where T : BaseEntity, IEntity
	{
		var repository = ServiceProvider.GetService<IGenericRepository<T>>()
		?? throw new InvalidOperationException("Repository is not registered in DI container");

		return repository;
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				context.Dispose();
			}
			_disposed = true;
		}
	}
}