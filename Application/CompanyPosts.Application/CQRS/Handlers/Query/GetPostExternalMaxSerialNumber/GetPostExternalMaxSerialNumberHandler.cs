namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostExternalMaxSerialNumber;
internal sealed class GetPostExternalMaxSerialNumberHandler
	: IRequestHandler<GetPostExternalMaxSerialNumberQuery, int>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetPostExternalMaxSerialNumberHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<int> Handle(
		GetPostExternalMaxSerialNumberQuery request, 
		CancellationToken cancellationToken)
	{
		var postExternalRepository = _unitOfWork.Repository<PostExternal>();
		var maxSerialNumber = await
			postExternalRepository.FindAllAsync(cancellationToken: cancellationToken);
		return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
	}
}