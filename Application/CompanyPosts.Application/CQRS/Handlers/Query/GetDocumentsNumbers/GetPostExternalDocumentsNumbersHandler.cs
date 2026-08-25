using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetDocumentsNumbers
{
    internal class GetPostExternalDocumentsNumbersHandler : GetDocumentNumbersBaseHandler<PostExternal , GetPostExternalDocumentNumbersQuery>
    {
        public GetPostExternalDocumentsNumbersHandler(IUnitOfWork unitOfWork) 
            : base(unitOfWork , x => x.Id , x => x.DocumentNumber)
        {
            
        }
    }
}