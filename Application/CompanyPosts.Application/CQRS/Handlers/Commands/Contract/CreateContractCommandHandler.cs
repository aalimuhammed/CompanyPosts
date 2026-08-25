namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class CreateContractCommandHandler
	: IRequestHandler<CreateContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
    private readonly IEmailServices _emailServices;
    public CreateContractCommandHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment,
        IEmailServices emailServices)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
		_emailServices = emailServices;
	}
	public async Task<Unit> Handle(CreateContractCommand request, CancellationToken cancellationToken)
	{
        var contractRepository = _unitOfWork.Repository<Contracts>();
		
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();

        var contractNumberExists = await contractRepository.FindAnyAsync(
                x => x.ContractNumber == request.CreateContractDTO.ContractNum,
                cancellationToken);

        if (contractNumberExists)
        {
            throw new Exception("رقم العقد موجود");
        }

        var admin = await sysUserRepository.FindAsync(x => x.IsAdmin, cancellationToken);

        if (request.CreateContractDTO.HasReference == ContractTypes.Original)
		{
            try
			{
                var maxSerialNumber = await contractRepository.FindAllAsync(cancellationToken: cancellationToken);

                var SerialNum =  maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
                var newContract = CreateContract(request , SerialNum);

				newContract.CreatedById = admin.Id;

                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                await contractRepository.AddAsync(newContract);

				if (request.CreateContractDTO.Attachments is not null && request.CreateContractDTO.Attachments.Any())
                {
                    await AddAttachments(newContract.Id,
                        request.CreateContractDTO.Attachments, cancellationToken);
                }

				await _unitOfWork.SaveChangesAsync(cancellationToken);
				await _unitOfWork.CommitTransactionAsync(cancellationToken);

                if (request.CreateContractDTO.EmailContent is not null &&
                   request.CreateContractDTO.SentEmailsTo is not null)
                {
					var sysUsers = await sysUserRepository.FindAllAsync(
						x => request.CreateContractDTO.SentEmailsTo.Contains(x.Id),
						cancellationToken);

                    var createEmailDto = new CreateEmailContentDTO()
                    {
                        DocumentNumber = request.CreateContractDTO.ContractNum,
                        EmailContent = request.CreateContractDTO.EmailContent,
                        Subject =$"متابعة العقد رقم {request.CreateContractDTO.ContractNum}",
                        EmailHeader = $"متابعة العقد رقم {request.CreateContractDTO.ContractNum}"
                    };

                    string emailContent = _emailServices.CreateEmailContent(createEmailDto);

                    await _emailServices.SendBulkEmailAsync(
                            $"متابعة المستند رقم {request.CreateContractDTO.ContractNum} في  العقد",
                        emailContent,
                        sysUsers.Select(u => u.Email!),
                        cancellationToken);
                }
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
				var contractId = Guid.Parse(request.CreateContractDTO.BaseContractId!);

				var originalContract = await contractRepository.FindWithIncludeFirstOrDefaultAsync
					(x => x.Id == contractId);

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

				originalContract.HasReference = true;

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                await contractRefRepository.AddAsync(newContract);
				contractRepository.Update(originalContract);

				if (request.CreateContractDTO.Attachments is not null)
				{
                    await AddContractRefAttachments(newContract.Id,
                        request.CreateContractDTO.Attachments, cancellationToken);
                }

				await _unitOfWork.SaveChangesAsync(cancellationToken);
				await _unitOfWork.CommitTransactionAsync(cancellationToken);

                if (request.CreateContractDTO.EmailContent is not null &&
					request.CreateContractDTO.SentEmailsTo is not null)
                {
                    var sysUsers = await sysUserRepository.FindAllAsync(
                        x => request.CreateContractDTO.SentEmailsTo.Contains(x.Id),
                        cancellationToken);

                    var createEmailDto = new CreateEmailContentDTO()
                    {
                        DocumentNumber = request.CreateContractDTO.ContractNum,
                        EmailContent = request.CreateContractDTO.EmailContent,
                        Subject = $"متابعة ملحق العقد رقم {request.CreateContractDTO.ContractNum}",
                        EmailHeader = $"متابعة ملحق العقد رقم {request.CreateContractDTO.ContractNum}"
                    };

                    string emailContent = _emailServices.CreateEmailContent(createEmailDto);

                    await _emailServices.SendBulkEmailAsync(
                            $"متابعة ملحق العقد رقم {request.CreateContractDTO.ContractNum}",
                        emailContent,
                        sysUsers.Select(u => u.Email!),
                        cancellationToken);
                }
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
			Value = request.CreateContractDTO.Value,
			SerialNumber = SerialNum,
            ContractNumber = request.CreateContractDTO.ContractNum,
			Details = request.CreateContractDTO.Details,
			Notes = request.CreateContractDTO.Notes,
			Contract_Date = request.CreateContractDTO.ContractDate,
			WorkTypeId = request.CreateContractDTO.WorkTypeId,
			purchase_order_ref = request.CreateContractDTO.PurchaseOrdNumRef,
			ProjectId = request.CreateContractDTO.ProjectId,
			PersonOrgId = request.CreateContractDTO.PersonOrgId,
			Currency = (Currency)request.CreateContractDTO.Currency,
			Department = (Departments)request.CreateContractDTO.Department,
		    CommercialRegisterNumber = request.CreateContractDTO.CommercialRegisterNumber,
            OldReferenceNumber = request.CreateContractDTO.OldRef , 
			DateOfReceipt = request.CreateContractDTO.DateOfReceipt,
			ApprovalDeliveryDate = request.CreateContractDTO.ApprovalDeliveryDate,
        };
	}
	private ContractRef CreateContractRef(CreateContractCommand request, int SerialNum)
	{
		return new ContractRef
		{
			Value = request.CreateContractDTO.Value,
            SerialNumber = SerialNum,
            ContractNumber = request.CreateContractDTO.ContractNum,
			Details = request.CreateContractDTO.Details,
			Notes = request.CreateContractDTO.Notes,
			Contract_Date = request.CreateContractDTO.ContractDate,
			//WorkTypeId = request.CreatrContractDTO.WorkTypeId,
			//purchase_order_ref = request.CreatrContractDTO.PurchaseOrdNumRef,
			//ProjectId = request.CreatrContractDTO.ProjectId,
			PersonOrgId = request.CreateContractDTO.PersonOrgId,
			Currency = (Currency)request.CreateContractDTO.Currency,
			//Department = (Departments)request.CreatrContractDTO.Department,
			ContractId = Guid.Parse(request.CreateContractDTO.BaseContractId!),
            DateOfReceipt = request.CreateContractDTO.DateOfReceipt,
            ApprovalDeliveryDate = request.CreateContractDTO.ApprovalDeliveryDate,
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