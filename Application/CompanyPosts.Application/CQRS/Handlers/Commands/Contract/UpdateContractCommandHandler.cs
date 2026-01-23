namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class UpdateContractCommandHandler
	: IRequestHandler<UpdateContractCommand, Unit>
{
	    private readonly IUnitOfWork _unitOfWork;
	    private readonly IFileService _fileService;
	    public UpdateContractCommandHandler(
		    IUnitOfWork unitOfWork,
		    IFileService fileService)
	    {
		    _unitOfWork = unitOfWork;
            _fileService = fileService;
	    }
        public async Task<Unit> Handle(
         UpdateContractCommand request,
         CancellationToken cancellationToken)
        {
            var contractRepo = _unitOfWork.Repository<Contracts>();

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var contract = await GetContractAsync(
                    contractRepo,
                    request.Id,
                    request.UpdateContractDTO.Attachments?.Any() == true,
                    cancellationToken);

                if (contract is null)
                    throw new Exception("Contract Record not found");

                MapContract(contract, request.UpdateContractDTO);

                if (request.UpdateContractDTO.Attachments?.Any() == true)
                {
                    await ReplaceAttachmentsAsync(
                        contract,
                        request.UpdateContractDTO.Attachments!,
                        cancellationToken);
                }

                contractRepo.Update(contract);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Unit.Value;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        #region Private Helpers
        private static async Task<Contracts?> GetContractAsync(
            IGenericRepository<Contracts> contractRepo,
            Guid contractId,
            bool includeAttachments,
            CancellationToken cancellationToken)
        {
            if (!includeAttachments)
            {
                return await contractRepo.FindAsync(
                    x => x.Id == contractId,
                    cancellationToken);
            }

            Expression<Func<Contracts, object>>[] includes =
            {
                c => c.ContractAttachments
            };

            return (await contractRepo.FindWithIncludeAsync(
                    x => x.Id == contractId,
                    includes,
                    cancellationToken))
                .FirstOrDefault();
        }
        private static void MapContract(
            Contracts contract,
            UpdateContractDTO dto)
        {
            contract.Value = dto.Value;
            contract.ContractNumber = dto.ContractNum;
            contract.Details = dto.Details;
            contract.Notes = dto.Notes;
            contract.Contract_Date = dto.ContractDate;
            contract.ProjectId = dto.ProjectId;
            contract.purchase_order_ref = dto.PurchaseOrdeRef;
            contract.PersonOrgId = dto.PersonOrgId;
            contract.WorkTypeId = dto.WorkTypeId;

            contract.Currency = Enum.TryParse(dto.Currency, true, out Currency currency)
                ? currency
                : throw new ArgumentException($"Invalid currency: {dto.Currency}");
        }
        private async Task ReplaceAttachmentsAsync(
            Contracts contract,
            List<IFormFile> newAttachments,
            CancellationToken cancellationToken)
        {
            DeleteExistingAttachments(contract);
            await AddAttachmentsAsync(contract.Id, newAttachments, cancellationToken);
        }
        private void DeleteExistingAttachments(Contracts contract)
        {
            if (!contract.ContractAttachments.Any())
                return;

            foreach (var attachment in contract.ContractAttachments)
            {
                if (!string.IsNullOrWhiteSpace(attachment.FileName))
                {
                    _fileService.DeleteFile("contracts", attachment.FileName);
                }
            }
        }
        private async Task AddAttachmentsAsync(
            Guid contractId,
            List<IFormFile> attachments,
            CancellationToken cancellationToken)
        {
            var attachmentRepo = _unitOfWork.Repository<ContractAttachments>();

            foreach (var file in attachments)
            {
                var fileName = await _fileService.SaveAttachmentAsync(
                    file,
                    "contracts",
                    cancellationToken);

                await attachmentRepo.AddAsync(new ContractAttachments
                {
                    ContractID = contractId,
                    FileName = fileName
                }, cancellationToken);
            }
        }
        #endregion
}
