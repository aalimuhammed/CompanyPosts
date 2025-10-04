namespace CompanyPost.Application.CQRS.Handlers.Commands.Post;
internal sealed class UpdatePostCommandHandler
	: IRequestHandler<UpdatePostCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ISaveAttachment _saveAttachment;
	private readonly IWebHostEnvironment _environment;
	public UpdatePostCommandHandler(
		IUnitOfWork unitOfWork, 
		ISaveAttachment saveAttachment,
		IWebHostEnvironment environment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
		_environment = environment;
	}
	public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
	{
		//var postRepository = _unitOfWork.Repository<Posts>();
		//var postToUpdate = await postRepository.FindAsync(x => x.Id == request.PostDTO.Id, cancellationToken);
		//if (postToUpdate == null)
		//{
		//	throw new Exception("Record not found");
		//}
		//postToUpdate.Subject = request.PostDTO.Subject;
		//postToUpdate.DocumentNumber = request.PostDTO.DocumentNumber;
		//postToUpdate.SerialNumber = request.PostDTO.SerialNumber;
		//postToUpdate.DateOfPost = request.PostDTO.DateOfPost;
		//postToUpdate.DateOfDelivery = request.PostDTO.DateOfDelivery;
		//postToUpdate.PostHeaderId = request.PostDTO.PostHeaderId;
		//postToUpdate.PostTypeId = request.PostDTO.PostTypeId;
		//postToUpdate.DeliveryMethodId = request.PostDTO.DeliveryMethodId;
		////postToUpdate.DeliveryPersonId = request.PostDTO.DeliveryPersonId;
		//postToUpdate.PostOriginalSenderId = request.PostDTO.PostOriginalSenderId;

		////if (request.PostDTO.AttachmentUrl != null)
		////{
		////	if (!string.IsNullOrEmpty(postToUpdate.Attachment))
		////	{
		////		var oldFilePath = Path.Combine(_environment.WebRootPath, "posts", postToUpdate.Attachment);
		////		if (File.Exists(oldFilePath))
		////		{
		////			File.Delete(oldFilePath);
		////		}
		////	}
		////	var newFileName = await _saveAttachment.SaveAttachmentAsync(request.PostDTO.AttachmentUrl , "posts", cancellationToken);
		////	postToUpdate.Attachment = newFileName;
		////}

		//postRepository.Update(postToUpdate);
		//await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}