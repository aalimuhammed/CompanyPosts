using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CompanyPost.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
	private bool _disposed = false;
	protected IServiceProvider ServiceProvider { get; }
	protected CompanyPostDbContext context { get; }
	private IDbContextTransaction? _currentTransaction;
	public UnitOfWork(IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;
		context = ServiceProvider.GetService<CompanyPostDbContext>()!;
	}
	public async Task<IDbTransaction> BeginTransactionAsync()
	{
		if (_currentTransaction != null)
		{
			throw new InvalidOperationException("A transaction is already in progress.");
		}

		_currentTransaction = await context.Database.BeginTransactionAsync();
		return _currentTransaction.GetDbTransaction();
	}
	public async Task CommitTransactionAsync()
	{
		if (_currentTransaction == null)
			throw new InvalidOperationException("No transaction started.");

		await _currentTransaction.CommitAsync();
		await _currentTransaction.DisposeAsync();
		_currentTransaction = null;
	}
	public async Task RollbackTransactionAsync()
	{
		if (_currentTransaction == null)
			throw new InvalidOperationException("No transaction started.");

		await _currentTransaction.RollbackAsync();
		await _currentTransaction.DisposeAsync();
		_currentTransaction = null;
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