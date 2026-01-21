namespace CompanyPost.Application.CQRS.Handlers.Commands.PostInernals;
internal sealed class CreatePostInternalHandler
	: IRequestHandler<CreatePostInternalCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	private readonly IEmailServices _emailServices;
	public CreatePostInternalHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment,
		IEmailServices emailServices)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment =saveAttachment;
		_emailServices = emailServices;
	}
	public async Task<Unit> Handle(CreatePostInternalCommand request, CancellationToken cancellationToken)
	{
		var postInternalRepository = _unitOfWork.Repository<PostInternal>();
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await sysUserRepository.FindAsync(x => x.IsAdmin, cancellationToken);

        if (await postInternalRepository.FindAnyAsync(x => x.DocumentNumber == request.CreatePostInternalDTO.DocumentNumber))
        {
            throw new Exception("Cannot have duplicated Document Number");
        }

        var postInternal = new PostInternal
		{
			SerialNumber = request.CreatePostInternalDTO.SerialNumber,
			DocumentNumber = request.CreatePostInternalDTO.DocumentNumber,
			CompanyId = request.CreatePostInternalDTO.CompanyId,
			PublishedId = request.CreatePostInternalDTO.PublishedId,
			RecievedFromId = request.CreatePostInternalDTO.RecivedFromId,
			Subject = request.CreatePostInternalDTO.Subject,
			WorkTypeId = request.CreatePostInternalDTO.WorkTypeId,
			DocumentDate = request.CreatePostInternalDTO.DocumentDate,
			DeliveryDate = request.CreatePostInternalDTO.DeliveryDate,
			Summary = request.CreatePostInternalDTO.Summary,
			Notes = request.CreatePostInternalDTO.Notes,
			DeliveryMethods = (DeliveryMethods)request.CreatePostInternalDTO.DeliveryMethod,
			//Department = (Departments)request.CreatePostInternalDTO.Department,
			CreatedById = admin.Id,
            InComingNumber = request.CreatePostInternalDTO.InComingNumber,
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

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();

            var sysUsers = await sysUserRepository.FindAllAsync(
                x => request.CreatePostInternalDTO.SentEmailsTo.Contains(x.Id),
                cancellationToken);

            _ = _emailServices.SendBulkEmailAsync(
					$"متابعة المستند رقم {request.CreatePostInternalDTO.DocumentNumber} في الصادر داخلي",
				request.CreatePostInternalDTO.EmailContent,
				sysUsers.Select(u => u.Email!),
				cancellationToken);

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