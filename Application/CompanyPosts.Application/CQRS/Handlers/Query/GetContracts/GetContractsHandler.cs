using CompanyPost.Application.DTO;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetContracts;
internal sealed class GetContractsHandler
	: IRequestHandler<GetContractsQuery, IEnumerable<ContractResponeDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetContractsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<ContractResponeDTO>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();

		var includes = new List<Expression<Func<Contracts, object>>>
			 {
			      contract => contract.Projects,
				  contract => contract.CreatedBy,
				  contract => contract.PersonOrgs,
				  contract => contract.ContractAttachments
			 };

		var contracts = await contractRepository.FindWithIncludeAsync(null, includes, cancellationToken);

		var contractDTOs = contracts.Select(c => new ContractResponeDTO(
			c.Id,
			c.Value,
			c.ContractNumber,
			c.Details,
			c.Notes,
			c.Contract_Date ,
			c.Projects.Name,
			c.CreatedBy.UserName,
			c.Currency.ToString(),
			c.purchase_order_ref,
			c.PersonOrgs.Name,
            c.ContractAttachments != null && c.ContractAttachments.Any()
               ? c.ContractAttachments.Select(a => new AttachmentDTO(a.Id, a.FileName!, $"/contracts/{a.FileName}")).ToList()
              : new List<AttachmentDTO>()));

		return contractDTOs;
	}
}