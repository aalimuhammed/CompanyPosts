namespace CompanyPost.Application.CQRS.Handlers.Commands.PostExternals;
internal sealed class CreatePostExternalHandler :
	IRequestHandler<CreatePostExternalCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public CreatePostExternalHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
	}
	public async Task<Unit> Handle(CreatePostExternalCommand request, CancellationToken cancellationToken)
	{
		var postExternalRepository = _unitOfWork.Repository<PostExternal>();
		var adminRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await adminRepository.FindAsync(predicate: null, cancellationToken);

		var postExternal = new PostExternal
		{
			SerialNumber = request.CreatePostExternalDTO.SerialNumber,
			DocumentNumber = request.CreatePostExternalDTO.DocumentNumber,
			CompanyId = request.CreatePostExternalDTO.CompanyId,
			PublishedId = request.CreatePostExternalDTO.PublishedId,
			ReceivedFromSupplierId = request.CreatePostExternalDTO.RecivedFromId,
			Subject = request.CreatePostExternalDTO.Subject,
			AboutWork = request.CreatePostExternalDTO.Working,
			DocumentDate = request.CreatePostExternalDTO.DocumentDate,
			DeliveryDate = request.CreatePostExternalDTO.DeliveryDate,
			Summary = request.CreatePostExternalDTO.Summary,
			Notes = request.CreatePostExternalDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostExternalDTO.DeliveryMethod,
			IncomingNumber = request.CreatePostExternalDTO.IncomingNumber,
			CreatedById = admin.Id,
		};
		var postExternalID = postExternal.Id;
		using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postExternalRepository.AddAsync(postExternal);
			await AddAttachments(postExternalID, request.CreatePostExternalDTO.Attachments, cancellationToken);

			await _unitOfWork.SaveChangesAsync();
			transaction.Commit();
			return Unit.Value;
		}
		catch (Exception ex)
		{
			transaction.Rollback();
			throw new Exception("An error occurred while creating the external post.", ex);
		}
	}
	private async Task AddAttachments(
		Guid postExternalId,
		List<IFormFile> attachments,
		CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<PostExternalAttachment>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(item, "posts", cancellationToken);
			var attachment = new PostExternalAttachment
			{
				PostExternalId = postExternalId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
}