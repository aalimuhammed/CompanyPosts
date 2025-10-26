namespace CompanyPost.Application.CQRS.Handlers.Query.GetFollowingPersons;
internal sealed class GetFollowingPersonsHandler
	: IRequestHandler<GetFollowingPersonsQuery, IEnumerable<FollowingPersonsDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetFollowingPersonsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<FollowingPersonsDTO>> Handle(GetFollowingPersonsQuery request, CancellationToken cancellationToken)
	{
		var followingPersonRepository = _unitOfWork.Repository<SysUsers>();
		var followingPersons = await followingPersonRepository.FindAllAsync(null , cancellationToken);

		var followingPersonDTO = followingPersons.Select(po => new FollowingPersonsDTO(po.Id , po.UserName));

		return followingPersonDTO;
	}
}