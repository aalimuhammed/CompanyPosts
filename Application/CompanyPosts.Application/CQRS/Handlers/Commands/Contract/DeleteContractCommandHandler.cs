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
		var contractAttachmentRepository = _unitOfWork.Repository<ContractAttachments>();

		var includes = new List<Expression<Func<Contracts, object>>>
				 {
						x => x.ContractAttachments,
				 };

		var contractToDelete = await contractRepository.FindWithIncludeAsync(
			x => x.Id == request.Id, includes, cancellationToken);

		if (contractToDelete == null || !contractToDelete.Any())
		{
			throw new Exception("Contract not found");
		}

		var contract = contractToDelete.FirstOrDefault() 
			?? throw new Exception("Not Found Row");

		if (contract.ContractAttachments != null && contract.ContractAttachments.Any())
		{
			foreach (var attachment in contract.ContractAttachments)
			{
				if (!string.IsNullOrEmpty(attachment.FileName))
				{
					_fileService.DeleteFile("contracts" , attachment.FileName);
					contractAttachmentRepository.Delete(attachment);
				}
			}
		}
		contractRepository.Delete(contract);
		await _unitOfWork.SaveChangesAsync();
		return Unit.Value;
	}
}