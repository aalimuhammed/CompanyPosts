namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class DeleteContractCommandHandler
	: IRequestHandler<DeleteContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _environment;
	public DeleteContractCommandHandler(
		IUnitOfWork unitOfWork, IWebHostEnvironment environment)
	{
		_unitOfWork = unitOfWork;
		_environment = environment;
	}
	public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();
		var contractToDelete = await contractRepository.FindAsync(x => x.Id == request.Id, cancellationToken);
		if (contractToDelete == null)
		{
			throw new Exception("Record not found");
		}
		//if (contractToDelete.Attachments != null)
		//{
		//	if (!string.IsNullOrEmpty(contractToDelete.Attachments))
		//	{
		//		var oldFilePath = Path.Combine(_environment.WebRootPath, "contracts", contractToDelete.Attachments);
		//		if (File.Exists(oldFilePath))
		//		{
		//			File.Delete(oldFilePath);
		//		}
		//	}
		//}
		contractRepository.Delete(contractToDelete);
		await _unitOfWork.SaveChangesAsync();
		return Unit.Value;
	}
}