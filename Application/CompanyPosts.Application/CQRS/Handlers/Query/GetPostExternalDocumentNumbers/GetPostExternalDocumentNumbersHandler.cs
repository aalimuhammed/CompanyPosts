using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostExternalDocumentNumbers
{
    internal class GetPostExternalDocumentNumbersHandler : IRequestHandler<GetPostExternalDocumentNumbersQuery, IEnumerable<PostDocumentNumbersDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostExternalDocumentNumbersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PostDocumentNumbersDTO>> Handle(
            GetPostExternalDocumentNumbersQuery request, 
            CancellationToken cancellationToken)
        {
            var postExternalRepository = _unitOfWork.Repository<PostExternal>();

            var postExternals = await postExternalRepository.FindAllAsync(null,cancellationToken);

            var postExternalDocuments = postExternals.Select(po => new PostDocumentNumbersDTO(
                po.Id,
                po.DocumentNumber
            ));

            return postExternalDocuments;
        }
    }
}