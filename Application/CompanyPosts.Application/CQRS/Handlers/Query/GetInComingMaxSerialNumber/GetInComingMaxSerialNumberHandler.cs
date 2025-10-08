namespace CompanyPost.Application.CQRS.Handlers.Query.GetInComingMaxSerialNumber;
internal sealed class GetInComingMaxSerialNumberHandler
	: IRequestHandler<GetInComingMaxSerialNumberQuery, int>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetInComingMaxSerialNumberHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<int> Handle(GetInComingMaxSerialNumberQuery request, CancellationToken cancellationToken)
	{
		var InComingRepository = _unitOfWork.Repository<InComing>();

		var maxSerialNumber = await
			InComingRepository.FindAllAsync(cancellationToken: cancellationToken);

		return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
	}
}