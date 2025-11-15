namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractDocumentById
{
    internal sealed class GetContractDocumentByIdHandler : IRequestHandler<GetContractDocumentByIdQuery, GetContractByIdResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContractDocumentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetContractByIdResponseDTO> Handle(GetContractDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var reopsitory = _unitOfWork.Repository<Contracts>();
            var contract = await reopsitory.FindAsync(x => x.Id == request.Id);
            if (contract is null)
            {
                throw new Exception("Contract not found");
            }
            var response = new GetContractByIdResponseDTO(
                Id: contract.Id,
                ContractNumber: contract.ContractNumber,
                ContractValue: contract.Value,
                ContractDate: contract.Contract_Date.ToString("yyyy-MM-dd"),
                Currency: (int)contract.Currency,
                Details: contract.Details,
                notes: contract.Notes,
                PurchaseOrderRef: contract.purchase_order_ref,
                SupplierId: contract.PersonOrgId,
                ProjectId: contract.ProjectId,
                Department: (int)contract.Department);

            return response;
        }
    }
}