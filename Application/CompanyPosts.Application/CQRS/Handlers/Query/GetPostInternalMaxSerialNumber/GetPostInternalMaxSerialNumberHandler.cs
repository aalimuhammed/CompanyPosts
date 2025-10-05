namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostInternalMaxSerialNumber;
internal sealed class GetPostExternalMaxSerialNumberHandler
	: IRequestHandler<GetPostInternalMaxSerialNumberQuery, int>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetPostExternalMaxSerialNumberHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<int> Handle(GetPostInternalMaxSerialNumberQuery request, CancellationToken cancellationToken)
	{
		var postInternalRepository = _unitOfWork.Repository<PostInternal>();
		var maxSerialNumber = await
			postInternalRepository.FindAllAsync(cancellationToken: cancellationToken);
		return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
	}
}