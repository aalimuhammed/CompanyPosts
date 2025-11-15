namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract
{
    internal sealed class UpdateContractDocumentHandler : IRequestHandler<UpdateContractDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateContractDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateContractDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<Contracts>();
            var contract = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (contract == null)
                throw new Exception($"Contract with ID '{request.Id}' not found.");

            contract.Department = (Departments)request.UpdateContractDocumentDTO.Department ;
            contract.ContractNumber = request.UpdateContractDocumentDTO.ContractNumber;
            contract.Contract_Date = request.UpdateContractDocumentDTO.ContractDate;
            contract.Notes = request.UpdateContractDocumentDTO.Notes;
            contract.Details = request.UpdateContractDocumentDTO.Details;
            contract.Value = request.UpdateContractDocumentDTO.ContractValue;
            contract.PersonOrgId = request.UpdateContractDocumentDTO.SupplierId;
            contract.ProjectId = request.UpdateContractDocumentDTO.ProjectId;
            contract.purchase_order_ref = request.UpdateContractDocumentDTO.PurchaseOrderRef;
            contract.Currency = (Currency) request.UpdateContractDocumentDTO.Currency;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                repository.Update(contract);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return false;
                throw;
            }
        }
    }
}
