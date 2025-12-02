using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetDocumentsNumbers
{
    internal class GetPostInternalDocumentsNumbersHandler : GetDocumentNumbersBaseHandler<PostInternal, GetPostInternalDocumentNumberQuery>
    {
        public GetPostInternalDocumentsNumbersHandler(IUnitOfWork unitOfWork) 
            : base(unitOfWork, x => x.Id, x => x.DocumentNumber)
        {
        }
    }
}