namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostTransformerMaxSerialNumber;
internal sealed class GetPostTransformerMaxSerialNumberHandler
	: IRequestHandler<GetPostTransformerMaxSerialNumberQuery, int>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetPostTransformerMaxSerialNumberHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<int> Handle(GetPostTransformerMaxSerialNumberQuery request, CancellationToken cancellationToken)
	{
		var postTrasnformerRepository = _unitOfWork.Repository<PostTransformer>();
		var maxSerialNumber = await
			postTrasnformerRepository.FindAllAsync(cancellationToken: cancellationToken);
		return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
	}
}
