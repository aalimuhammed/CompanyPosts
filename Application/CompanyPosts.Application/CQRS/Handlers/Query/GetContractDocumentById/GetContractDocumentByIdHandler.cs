using CompanyPost.Application.DTO;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractDocumentById
{
    internal sealed class GetContractDocumentByIdHandler : 
        IRequestHandler<GetContractDocumentByIdQuery, GetContractByIdResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContractDocumentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetContractByIdResponseDTO> Handle(
            GetContractDocumentByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<Contracts>();

            var includes = new List<Expression<Func<Contracts, object>>>
                    {
                        post => post.ContractAttachments
                    };

            var contract = await repository.FindWithIncludeFirstOrDefaultAsync(
                p => p.Id == request.Id,
                includes, 
                cancellationToken);

            var contractrefRepository = _unitOfWork.Repository<ContractRef>();

            var contractrefIncludes = new List<Expression<Func<ContractRef, object>>>
                    {
                        post => post.Contract,
                        post => post.ContractAttachments
                    };

            var contractRef = await contractrefRepository.FindWithIncludeFirstOrDefaultAsync(
                p => p.Id == request.Id,
                contractrefIncludes,
                cancellationToken);

            if (contractRef is null && contract is null)
            {
                throw new Exception("Contract not found");
            }

            if (contract != null)
            {
                return MapToDto(
                    contract.Id,
                    contract.ContractNumber,
                    contract.Value,
                    contract.Contract_Date,
                    (int)contract.Currency,
                    contract.Details,
                    contract.Notes,
                    contract.purchase_order_ref,
                    contract.PersonOrgId,
                    contract.ProjectId,
                    contract.WorkTypeId,
                    contract.OldReferenceNumber,
                    (int)contract.Department ,
                    contract.ContractAttachments);
            }

            if (contractRef != null)
            {
                return MapToDto(
                    contractRef.Id,
                    contractRef.ContractNumber,
                    contractRef.Value,
                    contractRef.Contract_Date,
                    (int)contractRef.Currency,
                    contractRef.Details,
                    contractRef.Notes , 
                    contractRef.Contract.purchase_order_ref , 
                    contractRef.Contract.PersonOrgId,
                    contractRef.Contract.ProjectId,
                    contractRef.Contract.WorkTypeId,
                    contractRef.Contract.OldReferenceNumber,
                    (int)contractRef.Contract.Department,
                    contractRef.ContractAttachments);
            }

            throw new Exception("Contract not found");
        }

        private static GetContractByIdResponseDTO MapToDto(
            Guid id,
            string contractNumber,
            double value,
            DateTime contractDate,
            int currency,
            string details,
            string notes,
            string purchaseOrderRef,
            Guid supplierId,
            Guid projectId,
            Guid workTypeId,
            string oldReferenceNumber,
            int department , 
            ICollection<ContractAttachments> contractAttachments)
        {
            return new GetContractByIdResponseDTO(
                Id: id,
                ContractNumber: contractNumber,
                ContractValue: value,
                ContractDate: contractDate.ToString("yyyy-MM-dd"),
                Currency: currency,
                Details: details,
                notes: notes,
                PurchaseOrderRef: purchaseOrderRef,
                SupplierId: supplierId,
                ProjectId: projectId,
                WorkTypeId: workTypeId,
                OldReferenceNumber: oldReferenceNumber,
                Department: department ,
                Attachments: contractAttachments != null && contractAttachments.Any()
               ? contractAttachments.Select(a => new AttachmentDTO(a.Id, a.FileName!, $"/contracts/{a.FileName}")).ToList()
              : new List<AttachmentDTO>());
        }
    }
}