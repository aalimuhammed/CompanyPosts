using CompanyPost.Application.Extension;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractAttached
{
    internal sealed class GetContractAttachedDataHandler : IRequestHandler<GetContractAttachedDataQuery, ContractAttachedDataResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContractAttachedDataHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ContractAttachedDataResponseDTO> Handle(GetContractAttachedDataQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<Contracts>();

            var includes = new List<Expression<Func<Contracts, object>>>
                     {
                         p => p.Projects
                     };

            var contract = await repository.FindWithIncludeAsync(
                x => x.Id == request.Id , 
                includes , cancellationToken);

            if (contract is null)
                throw new Exception("Contract not found");

            var contractItem = contract.FirstOrDefault()!;

            var contractDTOs = new ContractAttachedDataResponseDTO(
                        contractItem.Id,
                        contractItem.Projects.Name,
                        contractItem.Department.GetDisplayName(),
                        contractItem.purchase_order_ref!);

            return contractDTOs;
        }
    }
}