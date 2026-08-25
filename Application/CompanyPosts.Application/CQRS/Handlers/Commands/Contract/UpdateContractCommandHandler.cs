using CompanyPost.Application.Helpers;

namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class UpdateContractCommandHandler
    : IRequestHandler<UpdateContractCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly AttachmentsHelper _attachmentsHelper;
    public UpdateContractCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
    }
    public async Task<Unit> Handle(
     UpdateContractCommand request,
     CancellationToken cancellationToken)
    {
        var contractRepo = _unitOfWork.Repository<Contracts>();

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var dto = request.UpdateContractDTO;

            var hasNewFiles = dto.Attachments?.Any() == true;
            var idsToDelete = dto.AttachmentIdsToDelete ?? new List<Guid>();
            var hasDeletions = idsToDelete.Any();
            var needsAttachmentsLoaded = hasNewFiles || hasDeletions;

            var contract = await contractRepo.GetByIdAsyncWithAttachmentIncluded(
                             request.Id,
                             needsAttachmentsLoaded,
                             x => x.ContractAttachments,
                             cancellationToken);

            if (contract is null)
                throw new Exception("Contract Record not found");

            MapContract(contract, dto);

            if (hasDeletions)
                DeleteSelectedAttachments(contract.ContractAttachments, idsToDelete);

            if (hasNewFiles)
                await AddAttachmentsAsync(contract.Id, dto.Attachments!, cancellationToken);

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

        contract.Department = Enum.TryParse(dto.department, true, out Departments department)
            ? department
            : throw new ArgumentException($"Invalid department: {dto.department}");
    }

    // Deletes only the attachments whose Id is in idsToDelete — the DB rows
    // are removed via the repository AND the files are deleted from disk.
    // Everything else in the collection is left untouched.
    private void DeleteSelectedAttachments(
        ICollection<ContractAttachments> attachments,
        List<Guid> idsToDelete)
    {
        if (!attachments.Any())
            return;

        var attachmentRepo = _unitOfWork.Repository<ContractAttachments>();

        var toRemove = attachments
            .Where(a => idsToDelete.Contains(a.Id))
            .ToList();

        foreach (var attachment in toRemove)
        {
            if (!string.IsNullOrWhiteSpace(attachment.FileName))
                _fileService.DeleteFile("contracts", attachment.FileName);

            attachmentRepo.Delete(attachment);
            attachments.Remove(attachment);
        }
    }

    // Appends new files without touching existing attachments.
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