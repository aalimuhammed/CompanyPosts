namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractsNumbers
{
	internal sealed class GetContractsNumbersHandler
		: IRequestHandler<GetContractsNumbersQuery, IEnumerable<ContractNumberDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetContractsNumbersHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<ContractNumberDTO>> Handle(GetContractsNumbersQuery request, CancellationToken cancellationToken)
		{
			var contractRepository = _unitOfWork.Repository<Contracts>();
			var contracts = await contractRepository.FindAllAsync(cancellationToken: cancellationToken);
			var contractNumberDTOs = contracts.Select(c => new ContractNumberDTO(c.Id, c.ContractNumber));
			return contractNumberDTOs;
		}
	}
}