namespace CompanyPost.Application.CQRS.Handlers.Commands.Post;
internal sealed class DeletePostCommandHandler
	: IRequestHandler<DeletePostCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _environment;
	public DeletePostCommandHandler(
		IUnitOfWork unitOfWork, 
		IWebHostEnvironment environment)
	{
		_unitOfWork = unitOfWork;
		_environment = environment;
	}
	public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
	{
		//var postRepository = _unitOfWork.Repository<Posts>();
		//var postToDelete = await postRepository.FindAsync(x => x.Id == request.Id, cancellationToken);
		//if (postToDelete == null)
		//{
		//	throw new Exception("Record not found");
		//}
		//if (postToDelete.Attachment != null)
		//{
		//	if (!string.IsNullOrEmpty(postToDelete.Attachment))
		//	{
		//		var oldFilePath = Path.Combine(_environment.WebRootPath, "posts", postToDelete.Attachment);
		//		if (File.Exists(oldFilePath))
		//		{
		//			File.Delete(oldFilePath);
		//		}
		//	}
		//}
		//postRepository.Delete(postToDelete);
		//await _unitOfWork.SaveChangesAsync();
		return Unit.Value;
	}
}