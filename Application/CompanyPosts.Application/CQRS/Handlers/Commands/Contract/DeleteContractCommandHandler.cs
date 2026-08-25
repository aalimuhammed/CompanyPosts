namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class DeleteContractCommandHandler
	: IRequestHandler<DeleteContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _fileService;
	public DeleteContractCommandHandler(
		IUnitOfWork unitOfWork, 
		IFileService fileService)
	{
		_unitOfWork = unitOfWork;
		_fileService = fileService;
	}
	public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();

        Expression<Func<Contracts, object>>[] includes = { c => c.ContractAttachments};

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
		try
		{
            var contractsToDelete = await contractRepository.FindWithIncludeAsync(
            x => x.Id == request.Id, includes ,cancellationToken);

            if (contractsToDelete == null)
            {
                throw new Exception("Not Found Row");
            }

            var contractToDelete = contractsToDelete.FirstOrDefault();

            if (contractToDelete == null) {
                throw new KeyNotFoundException($"Contract with Id {request.Id} not found.");
            }

            if (contractToDelete != null && contractToDelete.ContractAttachments.Any())
            {
                foreach (var attachment in contractToDelete.ContractAttachments)
                {
                    if (!string.IsNullOrEmpty(attachment.FileName))
                    {
                        _fileService.DeleteFile("contracts", attachment.FileName);
                    }
                }
            }

            contractRepository.Delete(contractToDelete!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Unit.Value;
        }
		catch (Exception)
		{
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
        
	}
}