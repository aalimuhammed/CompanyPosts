using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.Base
{
    internal class GetDocumentNumbersBaseHandler<TEntity, TQuery>
     : IRequestHandler<TQuery, IEnumerable<PostDocumentNumbersDTO>>
     where TEntity : BaseEntity
     where TQuery : IRequest<IEnumerable<PostDocumentNumbersDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Func<TEntity, Guid> _idSelector;
        private readonly Func<TEntity, string> _documentNumberSelector;

        public GetDocumentNumbersBaseHandler(
            IUnitOfWork unitOfWork,
            Func<TEntity, Guid> idSelector,
            Func<TEntity, string> documentNumberSelector)
        {
            _unitOfWork = unitOfWork;
            _idSelector = idSelector;
            _documentNumberSelector = documentNumberSelector;
        }
        public async Task<IEnumerable<PostDocumentNumbersDTO>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<TEntity>();
            var entities = await repository.ListAllAsync(cancellationToken);

            var documents = entities.Select(x => new PostDocumentNumbersDTO(_idSelector(x), _documentNumberSelector(x)));

            return documents;
        }
    }
}