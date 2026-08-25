namespace CompanyPost.Application.CQRS.Handlers.Query.GetPurchaseOrderMaxSerialNumber
{
    internal sealed class GetPurchaseOrderMaxSerialNumberHandler
         : IRequestHandler<GetPurchaseOrderMaxSerialNumberQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPurchaseOrderMaxSerialNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(GetPurchaseOrderMaxSerialNumberQuery request, CancellationToken cancellationToken)
        {
            var purchaseOrderRepository = _unitOfWork.Repository<PurchaseOrder>();

            var maxSerialNumber = await
                purchaseOrderRepository.FindAllAsync(cancellationToken: cancellationToken);

            return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
        }
    }
}