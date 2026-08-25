namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractRefMaxSerialNumber
{
	internal sealed class GetContractRefMaxSerialNumberHandler : IRequestHandler<GetContractRefMaxSerialNumberQuery, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetContractRefMaxSerialNumberHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<int> Handle(GetContractRefMaxSerialNumberQuery request, CancellationToken cancellationToken)
		{
			var contractRefRepository = _unitOfWork.Repository<ContractRef>();

			var contractRefs = await contractRefRepository.FindAllAsync(
					x => x.ContractId == request.contractId,
					cancellationToken); 
			
			var serialNum = contractRefs.Any()
					? contractRefs.Max(x => x.SerialNumber) + 1
					: 1;
			return serialNum;
		}
	}
}
