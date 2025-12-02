using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetDocumentsNumbers
{
    internal class GetInComingDocumentsNumbersHandler : GetDocumentNumbersBaseHandler<InComing, GetInComingDocumentsNumbersQuery>
    {
        public GetInComingDocumentsNumbersHandler(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.Id , x => x.DocumentNumber)
        {
        }
    }
}