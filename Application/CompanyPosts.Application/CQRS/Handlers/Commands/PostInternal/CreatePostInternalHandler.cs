namespace CompanyPost.Application.CQRS.Handlers.Commands.PostInernals;
internal sealed class CreatePostInternalHandler
	: IRequestHandler<CreatePostInternalCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	private readonly IEmailServices _emailServices;
    private readonly IGetCurrentUserTokenService _getCurrentUserService;

    public CreatePostInternalHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment,
		IEmailServices emailServices,
		IGetCurrentUserTokenService getCurrentUserService
		)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment =saveAttachment;
		_emailServices = emailServices;
        _getCurrentUserService = getCurrentUserService;
    }
	public async Task<Unit> Handle(CreatePostInternalCommand request, CancellationToken cancellationToken)
	{
		var postInternalRepository = _unitOfWork.Repository<PostInternal>();
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();

		//var admin = await sysUserRepository.FindAsync(x => x.IsAdmin, cancellationToken);

		var adminId = _getCurrentUserService.UserId;

		if (await postInternalRepository.FindAnyAsync(
			x => x.DocumentNumber == request.CreatePostInternalDTO.DocumentNumber))
        {
            throw new Exception("لا يمكن تكرار رقم المستند");
        }
        var maxSerial = await postInternalRepository.MaxSerialNumber<PostInternal>(cancellationToken);
		var postInternal = new PostInternal
		{
			SerialNumber = maxSerial,
			DocumentNumber = request.CreatePostInternalDTO.DocumentNumber,
			CompanyId = request.CreatePostInternalDTO.CompanyId,
			PublishedId = request.CreatePostInternalDTO.PublishedId,
			RecievedFromId = request.CreatePostInternalDTO.RecivedFromId,
			Subject = request.CreatePostInternalDTO.Subject,
			//WorkTypeId = request.CreatePostInternalDTO.WorkTypeId,
			DocumentDate = request.CreatePostInternalDTO.DocumentDate,
			DeliveryDate = request.CreatePostInternalDTO.DeliveryDate,
			Summary = request.CreatePostInternalDTO.Summary,
			Notes = request.CreatePostInternalDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostInternalDTO.DeliveryMethod,
			PostDocumentTypes = (PostDocumentTypes)request.CreatePostInternalDTO.PostDocumentType,
			CreatedById = adminId,
			InComingNumber = request.CreatePostInternalDTO.InComingNumber,
			FollowingPerson = request.CreatePostInternalDTO.FollowingPerson,
			Status = (Status)request.CreatePostInternalDTO.StatusMethod,
			OldReferenceNumber = request.CreatePostInternalDTO.OldRef,
            AboutWork = request.CreatePostInternalDTO.AboutWork
        };
		var postInternalID = postInternal.Id;
		await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postInternalRepository.AddAsync(postInternal);

			if (request.CreatePostInternalDTO.Attachments is not null &&
				request.CreatePostInternalDTO.Attachments.Any())
			{
				await AddAttachments(postInternalID, request.CreatePostInternalDTO.Attachments, cancellationToken);
            }

			await _unitOfWork.SaveChangesAsync(cancellationToken);
			await _unitOfWork.CommitTransactionAsync(cancellationToken);

            if (request.CreatePostInternalDTO.EmailContent is not null
                     && request.CreatePostInternalDTO.SentEmailsTo is not null)
			{
                var sysUsers = await sysUserRepository.FindAllAsync(
                x => request.CreatePostInternalDTO.SentEmailsTo.Contains(x.Id),
                cancellationToken);

                var createEmailDto = new CreateEmailContentDTO()
                {
                    DocumentNumber = request.CreatePostInternalDTO.DocumentNumber,
                    EmailContent = request.CreatePostInternalDTO.EmailContent,
                    Subject = request.CreatePostInternalDTO.Subject,
                    EmailHeader = $"متابعة المستند رقم {request.CreatePostInternalDTO.DocumentNumber} في  الصادر داخلي"
                };

                string emailContent = _emailServices.CreateEmailContent(createEmailDto);

                await _emailServices.SendBulkEmailAsync(
                        $"متابعة المستند رقم {request.CreatePostInternalDTO.DocumentNumber} في الصادر داخلي",
                    emailContent,
                    sysUsers.Select(u => u.Email!),
                    cancellationToken);
            }

            return Unit.Value;
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
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