using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetDocumentsNumbers
{
    internal class GetPostTransformerDocumentsNumbersHandler : GetDocumentNumbersBaseHandler<PostTransformer, GetPostTransformerDocumentsNumbersQuery>
    {
        public GetPostTransformerDocumentsNumbersHandler(IUnitOfWork unitOfWork) 
            : base(unitOfWork, x => x.Id, x => x.DocumentNumber)
        {
        }
    }
}