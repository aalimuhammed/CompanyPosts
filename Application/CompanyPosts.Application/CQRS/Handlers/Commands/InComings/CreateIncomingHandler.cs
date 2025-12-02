using CompanyPost.Application.CQRS.Commands.InComing;

namespace CompanyPost.Application.CQRS.Handlers.Commands.InComings;
internal sealed class CreateIncomingHandler
	: IRequestHandler<CreateIncomingCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public CreateIncomingHandler(
		IUnitOfWork unitOfWork ,
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
	}
	public async Task<Unit> Handle(CreateIncomingCommand request, CancellationToken cancellationToken)
	{
		var incomingRepository = _unitOfWork.Repository<InComing>();
		var adminRepository = _unitOfWork.Repository<SysUsers>();
		var admin = await adminRepository.FindAsync(x => x.IsAdmin, cancellationToken);

        if (await incomingRepository.FindAnyAsync(x => x.DocumentNumber == request.createIncomingDTO.DocumentNumber))
        {
            throw new Exception("Cannot have duplicated Document Number");
        }

        var incoming = new InComing
		{
			SerialNumber = request.createIncomingDTO.SerialNumber,
			DocumentNumber = request.createIncomingDTO.DocumentNumber,
			Subject = request.createIncomingDTO.Subject,
			DocumentDate = request.createIncomingDTO.DocumentDate,
			DeliveryDate = request.createIncomingDTO.DeliveryDate,
			Summary = request.createIncomingDTO.Summary,
			DeliveryMethods = (DeliveryMethods)request.createIncomingDTO.DeliveryMethod,
			ProjectId = request.createIncomingDTO.ProjectId,
			SaveDate = request.createIncomingDTO.SaveDate,
			DocumentType = (DocumentType)request.createIncomingDTO.DocumentType,
			//Department = (Departments)request.createIncomingDTO.Department,
			OriginalPublisherId = request.createIncomingDTO.OriginalPublisherId,
			PublishedId = request.createIncomingDTO.PublishedId,
			WorkTypeId = request.createIncomingDTO.WorkTypeId,
			CreatedById = admin.Id,
			InComingNumber = request.createIncomingDTO.InComingNumber,
        };

		var inComingId = incoming.Id;
		await _unitOfWork.BeginTransactionAsync();
		try
		{
			await incomingRepository.AddAsync(incoming);

			if (request.createIncomingDTO.Attachments != null)
			{
                await AddAttachments(inComingId,
                    request.createIncomingDTO.Attachments, cancellationToken);
            }

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();
			return Unit.Value;
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
			throw new Exception("An error occurred while creating the incoming post.", ex);
		}
	}
	private async Task AddAttachments(
		Guid incomingId,
		List<IFormFile> attachments,
		CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<InComingAttachments>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(item, "incomings", cancellationToken);
			var attachment = new InComingAttachments
			{
				IncomingId = incomingId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
}