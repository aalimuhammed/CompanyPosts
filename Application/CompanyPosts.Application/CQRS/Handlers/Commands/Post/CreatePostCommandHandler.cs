namespace CompanyPost.Application.CQRS.Handlers.Commands.Post;
internal sealed class CreatePostCommandHandler 
	: IRequestHandler<CreatePostCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _environment;
	public CreatePostCommandHandler(
		IUnitOfWork unitOfWork , 
		IWebHostEnvironment webHostEnvironment)
	{
		_unitOfWork = unitOfWork;
		_environment = webHostEnvironment;
	}
	public async Task<Unit> Handle(
		CreatePostCommand request, 
		CancellationToken cancellationToken)
	{
		//var postRepository = _unitOfWork.Repository<Posts>();

		//var fileName = await SaveAttachmentAsync(request.CreatePostDTO.attachment, cancellationToken);
		//var entity = CreatePosts(request , fileName);

		//await postRepository.AddAsync(entity, cancellationToken);
		//await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
	private async Task<string> SaveAttachmentAsync(IFormFile attachment, CancellationToken cancellationToken)
	{
		if (attachment == null || attachment.Length == 0)
			throw new Exception("Attachment is required and must not be empty.");

		var uploadsPath = Path.Combine(_environment.WebRootPath, "posts");
		if (!Directory.Exists(uploadsPath))
			Directory.CreateDirectory(uploadsPath);

		var fileName = Guid.NewGuid().ToString() + Path.GetExtension(attachment.FileName);
		var filePath = Path.Combine(uploadsPath, fileName);

		try
		{
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await attachment.CopyToAsync(stream, cancellationToken);
			}

			if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
				throw new Exception("File upload failed. Attachment was not saved properly.");

			return fileName;
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while uploading the attachment.", ex);
		}
	}
	//private Posts CreatePosts(CreatePostCommand request , string fileName)
	//{
	//	return new Posts
	//	{
	//		Subject = request.CreatePostDTO.subject,
	//		//Attachment = fileName,
	//		DocumentNumber = request.CreatePostDTO.document_number,
	//		SerialNumber = request.CreatePostDTO.serial_number,
	//		DateOfDelivery = request.CreatePostDTO.date_of_delivery,
	//		DateOfPost = request.CreatePostDTO.date_of_post,
	//		PostOriginalSenderId = request.CreatePostDTO.post_original_sender_id,
	//		DeliveryMethodId = request.CreatePostDTO.delivery_method_id,
	//		//DeliveryPersonId = request.CreatePostDTO.post_original_sender_id,
	//		CreatedById = Guid.Parse("97b46533-ed0c-46dd-87c2-2ca396ee629e"),
	//		PostHeaderId = request.CreatePostDTO.post_header_id,
	//		PostTypeId = request.CreatePostDTO.post_type_id,
	//		ProjectId = request.CreatePostDTO.project_id,
	//	};
	//}
}