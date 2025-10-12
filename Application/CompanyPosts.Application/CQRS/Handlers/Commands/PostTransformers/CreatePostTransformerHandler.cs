namespace CompanyPost.Application.CQRS.Handlers.Commands.PostTransformers;
internal sealed class CreatePostTransformerHandler
	: IRequestHandler<CreatePostTransformerCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public CreatePostTransformerHandler(
		IUnitOfWork unitOfWork, 
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
	}
	public async Task<Unit> Handle(CreatePostTransformerCommand request, CancellationToken cancellationToken)
	{
		var postTransofrmerRepository = _unitOfWork.Repository<PostTransformer>();
		var adminRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await adminRepository.FindAsync(predicate: null, cancellationToken);

		var postTransformer = new PostTransformer
		{
			SerialNumber = request.CreatePostTransofrmerDTO.SerialNumber,
			PostNumber = request.CreatePostTransofrmerDTO.PostNumber,
			DocumentNumber = request.CreatePostTransofrmerDTO.DocumentNumber,
			CompanyId = request.CreatePostTransofrmerDTO.CompanyId,
			PublishedId = request.CreatePostTransofrmerDTO.PublishedId,
			RecievedFromId = request.CreatePostTransofrmerDTO.RecivedFromId,
			Subject = request.CreatePostTransofrmerDTO.Subject,
			AboutWork = request.CreatePostTransofrmerDTO.Working,
			DocumentDate = request.CreatePostTransofrmerDTO.DocumentDate,
			DeliveryDate = request.CreatePostTransofrmerDTO.DeliveryDate,
			Summary = request.CreatePostTransofrmerDTO.Summary,
			Notes = request.CreatePostTransofrmerDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostTransofrmerDTO.DeliveryMethod,
			DocumentType = (DocumentType)request.CreatePostTransofrmerDTO.DocumentType,
			IncomingNumber = request.CreatePostTransofrmerDTO.IncomingNumber,
			RecivedByName = request.CreatePostTransofrmerDTO.RecivedName,
			FollowingPerson = request.CreatePostTransofrmerDTO.FollowingPerson,
			CreatedById = admin.Id,
		};
		var postExternalID = postTransformer.Id;
		using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postTransofrmerRepository.AddAsync(postTransformer);
			await AddAttachments(postExternalID, request.CreatePostTransofrmerDTO.Attachments, cancellationToken);

			await _unitOfWork.SaveChangesAsync();
			transaction.Commit();
			return Unit.Value;
		}
		catch (Exception ex)
		{
			transaction.Rollback();
			throw new Exception("An error occurred while creating the transformer post.", ex);
		}
	}
	private async Task AddAttachments(
	Guid postExternalId,
	List<IFormFile> attachments,
	CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<PostTransformerAttachment>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(item, "posts", cancellationToken);
			var attachment = new PostTransformerAttachment
			{
				PostTransformerId = postExternalId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
}
