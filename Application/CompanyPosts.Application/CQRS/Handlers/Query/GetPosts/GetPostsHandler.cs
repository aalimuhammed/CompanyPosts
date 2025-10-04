namespace CompanyPost.Application.CQRS.Handlers.Query.GetPosts;
internal sealed class GetPostsHandler
	: IRequestHandler<GetPostsQuery, IEnumerable<PostResponseDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetPostsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<PostResponseDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
	{
		//var postRepository = _unitOfWork.Repository<Posts>();

		//var includes = new List<Expression<Func<Posts, object>>>
		//	 {
		//		 post => post.DeliveryMethods ,
		//		 post => post.PostHeaders ,
		//		 post => post.PostTypes ,
		//		// post => post.DeliveryPersons ,
		//		 post => post.PostOriginalSenders , 
		//		 post => post.CreatedBy
		//	 };

		//var posts = await postRepository.FindWithIncludeAsync(null ,includes , cancellationToken);

		//var postDTOs = posts.Select(p => new PostResponseDTO(
		//	p.Id,
		//	p.Subject,
		//	p.DocumentNumber,
		//	p.SerialNumber,
		//	p.DateOfPost,
		//	p.DateOfDelivery,
		//	p.PostHeaders.Name,
		//	p.PostTypes.Type,
		//	p.CreatedBy.username,
		//	p.DeliveryMethods.Name,
		//	//p.DeliveryPersons.Name,
		//	p.PostOriginalSenders.Name,
		//	""
		//	//p.Attachment != null ? $"/uploads/{p.Attachment}" : null
		//));
		return null;
	}
}