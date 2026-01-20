namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract;
internal sealed class UpdateContractCommandHandler
	: IRequestHandler<UpdateContractCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _saveAttachment;
	public UpdateContractCommandHandler(
		IUnitOfWork unitOfWork,
		IFileService saveAttachment)
	{
		_unitOfWork = unitOfWork;
		_saveAttachment = saveAttachment;
	}
	public async Task<Unit> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();
		var contractToUpdate = await contractRepository.FindAsync(x => x.Id == request.UpdateContractDTO.Id, cancellationToken);
		if (contractToUpdate == null)
		{
			throw new Exception("Record not found");
		}
		contractToUpdate.Value = request.UpdateContractDTO.Value;
		contractToUpdate.ContractNumber = request.UpdateContractDTO.ContractNum;
		contractToUpdate.Details = request.UpdateContractDTO.Details;
		contractToUpdate.Notes = request.UpdateContractDTO.Notes;
		contractToUpdate.Contract_Date = request.UpdateContractDTO.ContractDate;
		contractToUpdate.ProjectId = request.UpdateContractDTO.ProjectId;

		contractToUpdate.Currency = Enum.TryParse(request.UpdateContractDTO.Currency, true, out Currency currency)
				 ? currency : throw new ArgumentException($"Invalid currency: {request.UpdateContractDTO.Currency}");

		//contractToUpdate.working = request.UpdateContractDTO.Working;
		contractToUpdate.purchase_order_ref = request.UpdateContractDTO.PurchaseOrdeRef;
		contractToUpdate.PersonOrgId = request.UpdateContractDTO.PersonOrgId;

		if (request.UpdateContractDTO.Attachments != null)
		{
			//if (!string.IsNullOrEmpty(contractToUpdate.Attachments))
			//{
			//	var oldFilePath = Path.Combine(_environment.WebRootPath, "contracts", contractToUpdate.Attachments);
			//	if (File.Exists(oldFilePath))
			//	{
			//		File.Delete(oldFilePath);
			//	}
			//}
			var newFileName = await _saveAttachment.SaveAttachmentAsync(request.UpdateContractDTO.Attachments, "contracts", cancellationToken);
			//contractToUpdate.Attachments = newFileName;
		}
		contractRepository.Update(contractToUpdate);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
