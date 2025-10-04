namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class CreateContractCommandHandler
	: IRequestHandler<CreateContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _environment;
	public CreateContractCommandHandler(
		IUnitOfWork unitOfWork,
		IWebHostEnvironment environment)
	{
		_unitOfWork = unitOfWork;
		_environment=environment;
	}
	public async Task<Unit> Handle(CreateContractCommand request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();
		
		var fileName = await SaveAttachmentAsync(request.CreatrContractDTO.attachments, cancellationToken);

		var newContract = CreateContract(request, fileName);

		await contractRepository.AddAsync(newContract);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
	private async Task<string> SaveAttachmentAsync(IFormFile attachment, CancellationToken cancellationToken)
	{
		if (attachment == null || attachment.Length == 0)
			throw new Exception("Attachment is required and must not be empty.");

		var uploadsPath = Path.Combine(_environment.WebRootPath, "contracts");
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
	private Contracts CreateContract(CreateContractCommand request, string fileName)
	{
		return new Contracts
		{
			Value = request.CreatrContractDTO.value,
			ContractNumber = request.CreatrContractDTO.contract_num,
			CreatedById = Guid.Parse("97b46533-ed0c-46dd-87c2-2ca396ee629e"),
			Details = request.CreatrContractDTO.details,
			Notes = request.CreatrContractDTO.notes,
			//Attachments = fileName,
			Contract_Date = request.CreatrContractDTO.contract_date,
			working = request.CreatrContractDTO.working,
			purchase_order_ref = request.CreatrContractDTO.purchase_order_ref,
			ProjectId = request.CreatrContractDTO.project_id,
			PersonOrgId = request.CreatrContractDTO.person_org_id,
			Currency = ParseCurrency(request.CreatrContractDTO.currency)
		};
	}
	private static Currency ParseCurrency(string currencyId)
	{
		var cleanedCurrency = Regex.Replace(currencyId, @"\s+", "");

		if (Enum.TryParse(cleanedCurrency, true, out Currency status))
		{
			return status;
		}
		throw new Exception($"Invalid currency: {currencyId}");
	}
}
