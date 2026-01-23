namespace CompanyPost.Application.CQRS.Handlers.Commands.PostExternals;
internal sealed class CreatePostExternalHandler :
	IRequestHandler<CreatePostExternalCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	private readonly IEmailServices _emailServices;
	public CreatePostExternalHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment,
		IEmailServices emailServices)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
		_emailServices = emailServices;
	}
	public async Task<Unit> Handle(CreatePostExternalCommand request, CancellationToken cancellationToken)
	{
		var postExternalRepository = _unitOfWork.Repository<PostExternal>();
		var sysUserRepo = _unitOfWork.Repository<SysUsers>();
		var admin = await sysUserRepo.FindAsync(x => x.IsAdmin, cancellationToken);

		if (await postExternalRepository.FindAnyAsync(x => x.DocumentNumber == request.CreatePostExternalDTO.DocumentNumber))
		{
			throw new Exception("Cannot have duplicated Document Number");
		}

		var postExternal = new PostExternal
		{
			SerialNumber = request.CreatePostExternalDTO.SerialNumber,
			DocumentNumber = request.CreatePostExternalDTO.DocumentNumber,
			CompanyId = request.CreatePostExternalDTO.CompanyId,
			PublishedId = request.CreatePostExternalDTO.PublishedId,
			RecievedFromId = request.CreatePostExternalDTO.RecivedFromId,
			Subject = request.CreatePostExternalDTO.Subject,
			WorkTypeId = request.CreatePostExternalDTO.WorkTypeId,
			DocumentDate = request.CreatePostExternalDTO.DocumentDate,
			DeliveryDate = request.CreatePostExternalDTO.DeliveryDate,
			Summary = request.CreatePostExternalDTO.Summary,
			Notes = request.CreatePostExternalDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostExternalDTO.DeliveryMethod,
			//Department = (Departments)request.CreatePostExternalDTO.Department,
			InComingNumber = request.CreatePostExternalDTO.IncomingNumber,
			FollowingPerson = request.CreatePostExternalDTO.FollowingPerson,
			CreatedById = admin.Id,
		};
		var postExternalID = postExternal.Id;
		await _unitOfWork.BeginTransactionAsync();
		try
		{
			await postExternalRepository.AddAsync(postExternal);
			if (request.CreatePostExternalDTO.Attachments is not null)
			{
                await AddAttachments(postExternalID, request.CreatePostExternalDTO.Attachments, cancellationToken);
            }

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();

            var sysUsers = await sysUserRepo.FindAllAsync(
					x => request.CreatePostExternalDTO.SentEmailsTo.Contains(x.Id),
					cancellationToken);

            _ = _emailServices.SendBulkEmailAsync(
					$"متابعة المستند رقم {request.CreatePostExternalDTO.DocumentNumber} في الصادر خارجي",
				request.CreatePostExternalDTO.EmailContent,
                sysUsers.Select(u => u.Email!),
				cancellationToken);

            return Unit.Value;
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
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