namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class CreateContractCommandHandler
	: IRequestHandler<CreateContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public CreateContractCommandHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
	}
	public async Task<Unit> Handle(CreateContractCommand request, CancellationToken cancellationToken)
	{
        var contractRepository = _unitOfWork.Repository<Contracts>();
		
		var adminRepository = _unitOfWork.Repository<SysUsers>();

        var contractNumberExists = await contractRepository.FindAnyAsync(
                x => x.ContractNumber == request.CreatrContractDTO.ContractNum,
                cancellationToken);
        if (contractNumberExists)
        {
            throw new Exception("رقم العقد موجود");
        }

        var admin = await adminRepository.FindAsync(x => x.IsAdmin, cancellationToken);

        if (request.CreatrContractDTO.HasReference == ContractTypes.Original)
		{
			var purchaseOrderRefExists = await contractRepository.FindAnyAsync(
				x => x.purchase_order_ref == request.CreatrContractDTO.PurchaseOrdNumRef, 
				cancellationToken);

			if (purchaseOrderRefExists)
			{
				throw new Exception("Purchase Order Reference already exists.");
            }

            try
			{
                var maxSerialNumber = await
						contractRepository.FindAllAsync(cancellationToken: cancellationToken);

                var SerialNum =  maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
                var newContract = CreateContract(request , SerialNum);

				newContract.CreatedById = admin.Id;

                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                await contractRepository.AddAsync(newContract);

				if (request.CreatrContractDTO.Attachments is not null && request.CreatrContractDTO.Attachments.Any())
                {
                    await AddAttachments(newContract.Id,
                        request.CreatrContractDTO.Attachments, cancellationToken);
                }

				await _unitOfWork.SaveChangesAsync();
				await _unitOfWork.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw new Exception("An error occurred while creating the contract post.", ex);
			}
		}
		else
		{
			var contractRefRepository = _unitOfWork.Repository<ContractRef>();

            try
			{
				var contractId = Guid.Parse(request.CreatrContractDTO.BaseContractId!);

				// Check base contract exists
				var contractBaseExists = await contractRepository.FindAsync(
					x => x.Id == contractId,
					cancellationToken);

				// Get ContractRefs ONLY for this contract
				var contractRefs = await contractRefRepository.FindAllAsync(
					x => x.ContractId == contractId,
					cancellationToken);

				// Serial logic
				var serialNum = contractRefs.Any()
					? contractRefs.Max(x => x.SerialNumber) + 1
					: 1;

				var newContract = CreateContractRef(request , serialNum);
				newContract.CreatedById = admin.Id;

                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                await contractRefRepository.AddAsync(newContract);

				if (request.CreatrContractDTO.Attachments is not null)
				{
                    await AddContractRefAttachments(newContract.Id,
                        request.CreatrContractDTO.Attachments, cancellationToken);
                }

				await _unitOfWork.SaveChangesAsync();
				await _unitOfWork.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw new Exception("An error occurred while creating the contract post.", ex);
			}
		}

		return Unit.Value;
	}
	private Contracts CreateContract(CreateContractCommand request , int SerialNum)
	{
		return new Contracts
		{
			Value = request.CreatrContractDTO.Value,
			SerialNumber = SerialNum,
            ContractNumber = request.CreatrContractDTO.ContractNum,
			Details = request.CreatrContractDTO.Details,
			Notes = request.CreatrContractDTO.Notes,
			Contract_Date = request.CreatrContractDTO.ContractDate,
			WorkTypeId = request.CreatrContractDTO.WorkTypeId,
			purchase_order_ref = request.CreatrContractDTO.PurchaseOrdNumRef,
			ProjectId = request.CreatrContractDTO.ProjectId,
			PersonOrgId = request.CreatrContractDTO.PersonOrgId,
			Currency = (Currency)request.CreatrContractDTO.Currency,
			Department = (Departments)request.CreatrContractDTO.Department,
		    CommercialRegisterNumber = request.CreatrContractDTO.CommercialRegisterNumber,
            OldReferenceNumber = request.CreatrContractDTO.OldRef
        };
	}

	private ContractRef CreateContractRef(CreateContractCommand request, int SerialNum)
	{
		return new ContractRef
		{
			Value = request.CreatrContractDTO.Value,
            SerialNumber = SerialNum,
            ContractNumber = request.CreatrContractDTO.ContractNum,
			Details = request.CreatrContractDTO.Details,
			Notes = request.CreatrContractDTO.Notes,
			//Contract_Date = request.CreatrContractDTO.ContractDate,
			//WorkTypeId = request.CreatrContractDTO.WorkTypeId,
			//purchase_order_ref = request.CreatrContractDTO.PurchaseOrdNumRef,
			//ProjectId = request.CreatrContractDTO.ProjectId,
			//PersonOrgId = request.CreatrContractDTO.PersonOrgId,
			Currency = (Currency)request.CreatrContractDTO.Currency,
			//Department = (Departments)request.CreatrContractDTO.Department,
			ContractId = Guid.Parse(request.CreatrContractDTO.BaseContractId!),
        };
	}
	private async Task AddAttachments(
		Guid contractId,
		List<IFormFile> attachments,
		CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<ContractAttachments>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(
				item, 
				"contracts", 
				cancellationToken);

			var attachment = new ContractAttachments
			{
				ContractID = contractId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
	private async Task AddContractRefAttachments(
		Guid contractId,
		List<IFormFile> attachments,
		CancellationToken cancellationToken)
	{
		var attachmentRepository = _unitOfWork.Repository<ContractAttachments>();

		foreach (var item in attachments)
		{
			var fileName = await _saveAttachment.SaveAttachmentAsync(
				item,
				"contracts",
				cancellationToken);
			var attachment = new ContractAttachments
			{
				ContractRefId = contractId,
				FileName = fileName,
			};
			await attachmentRepository.AddAsync(attachment, cancellationToken);
		}
	}
}
