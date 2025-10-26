using CompanyPost.Domain.Interface;
using System.Data;
namespace CompanyPost.Application.Abstraction;
public interface IUnitOfWork : IDisposable
{
	IGenericRepository<T> Repository<T>() where T : BaseEntity, IEntity;
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitTransactionAsync(CancellationToken cancellationToken = default);
	Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}