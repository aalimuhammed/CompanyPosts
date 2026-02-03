namespace CompanyPost.Application.CQRS.Handlers.Commands.PostTransformers;
internal sealed class CreatePostTransformerHandler
	: IRequestHandler<CreatePostTransformerCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	private readonly IEmailServices _emailServices;
	public CreatePostTransformerHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment,
		IEmailServices emailServices)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
		_emailServices = emailServices;
	}
	public async Task<Unit> Handle(CreatePostTransformerCommand request, CancellationToken cancellationToken)
	{
		var postTransofrmerRepository = _unitOfWork.Repository<PostTransformer>();

        if (await postTransofrmerRepository.FindAnyAsync(
            x => x.DocumentNumber == request.CreatePostTransofrmerDTO.DocumentNumber, cancellationToken))
        {
            throw new Exception("Cannot have duplicated Document Number");
        }

        var systUserRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await systUserRepository.FindAsync(x => x.IsAdmin, cancellationToken);
		
        var maxSerial = await postTransofrmerRepository.MaxSerialNumber<PostTransformer>(cancellationToken);

		var postTransformer = new PostTransformer
		{
			SerialNumber = maxSerial,
			PostNumber = request.CreatePostTransofrmerDTO.PostNumber,
			DocumentNumber = request.CreatePostTransofrmerDTO.DocumentNumber,
			CompanyId = request.CreatePostTransofrmerDTO.CompanyId,
			PublishedId = request.CreatePostTransofrmerDTO.PublishedId,
			RecievedFromId = request.CreatePostTransofrmerDTO.RecivedFromId,
			Subject = request.CreatePostTransofrmerDTO.Subject,
			WorkTypeId = request.CreatePostTransofrmerDTO.WorkTypeId,
			DocumentDate = request.CreatePostTransofrmerDTO.DocumentDate,
			DeliveryDate = request.CreatePostTransofrmerDTO.DeliveryDate,
			Summary = request.CreatePostTransofrmerDTO.Summary,
			Notes = request.CreatePostTransofrmerDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostTransofrmerDTO.DeliveryMethod,
			PostDocumentTypes = (PostDocumentTypes)request.CreatePostTransofrmerDTO.PostDocumentType,
			IncomingNumber = request.CreatePostTransofrmerDTO.IncomingNumber,
			RecivedByName = request.CreatePostTransofrmerDTO.RecivedName,
			FollowingPerson = request.CreatePostTransofrmerDTO.FollowingPerson,
			CreatedById = admin.Id,
			InComingNumber = request.CreatePostTransofrmerDTO.IncomingNumber,
			Status = (Status)request.CreatePostTransofrmerDTO.StatusMethod,
            OldReferenceNumber = request.CreatePostTransofrmerDTO.OldRef,
			DocumentType = (DocumentType)request.CreatePostTransofrmerDTO.DocumentType,
		};
		var postExternalID = postTransformer.Id;
		await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postTransofrmerRepository.AddAsync(postTransformer);

			if (request.CreatePostTransofrmerDTO.Attachments != null &&
				request.CreatePostTransofrmerDTO.Attachments.Any())
                await AddAttachments(postExternalID, request.CreatePostTransofrmerDTO.Attachments, cancellationToken);

			var sysUsers = await systUserRepository.FindAllAsync(
				x => request.CreatePostTransofrmerDTO.SentEmailsTo.Contains(x.Id),
				cancellationToken);


			await _unitOfWork.SaveChangesAsync(cancellationToken);
			await _unitOfWork.CommitTransactionAsync();

			_ = _emailServices.SendBulkEmailAsync(
					$"متابعة المستند رقم {request.CreatePostTransofrmerDTO.DocumentNumber} في الصادر المحول",
				request.CreatePostTransofrmerDTO.EmailContent,
				sysUsers.Select(u => u.Email!),
				cancellationToken);

			return Unit.Value;
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
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