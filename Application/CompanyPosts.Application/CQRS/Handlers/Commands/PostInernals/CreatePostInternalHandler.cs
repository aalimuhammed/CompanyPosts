namespace CompanyPost.Application.CQRS.Handlers.Commands.PostInernal;
internal sealed class CreatePostInternalHandler
	: IRequestHandler<CreatePostInternalCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public CreatePostInternalHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment =saveAttachment;
	}
	public async Task<Unit> Handle(CreatePostInternalCommand request, CancellationToken cancellationToken)
	{
		var postInternalRepository = _unitOfWork.Repository<PostInternal>();
		var adminRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await adminRepository.FindAsync(predicate: null, cancellationToken);

		var postInternal = new PostInternal
		{
			SerialNumber = request.CreatePostInternalDTO.SerialNumber,
			DocumentNumber = request.CreatePostInternalDTO.DocumentNumber,
			CompanyId = request.CreatePostInternalDTO.CompanyId,
			PublishedId = request.CreatePostInternalDTO.PublishedId,
			RecievedFromId = request.CreatePostInternalDTO.RecivedFromId,
			Subject = request.CreatePostInternalDTO.Subject,
			AboutWork = request.CreatePostInternalDTO.Working,
			DocumentDate = request.CreatePostInternalDTO.DocumentDate,
			DeliveryDate = request.CreatePostInternalDTO.DeliveryDate,
			Summary = request.CreatePostInternalDTO.Summary,
			Notes = request.CreatePostInternalDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostInternalDTO.DeliveryMethod,
			CreatedById = admin.Id,
		};
		var postInternalID = postInternal.Id;
		using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postInternalRepository.AddAsync(postInternal);
			await AddAttachments(postInternalID, request.CreatePostInternalDTO.Attachments, cancellationToken);

			await _unitOfWork.SaveChangesAsync();
			transaction.Commit();
			return Unit.Value;
		}
		catch (Exception ex)
		{
			transaction.Rollback();
			throw new Exception("An error occurred while creating the internal post.", ex);
		}
	}
	private async Task AddAttachments(
		Guid postInternalId , 
		List<IFormFile> attachments ,
		CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<PostInternalAttachment>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(item, "posts", cancellationToken);
			var attachment = new PostInternalAttachment
			{
				PostInternalId = postInternalId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
}