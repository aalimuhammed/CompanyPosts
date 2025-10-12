namespace CompanyPost.Application.CQRS.Handlers.Query.GetSuppliers;
internal sealed class GetSuppliersHandler
	: IRequestHandler<GetSuppliersQuery, IEnumerable<SupplierDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetSuppliersHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<SupplierDTO>> Handle(
		GetSuppliersQuery request, 
		CancellationToken cancellationToken)
	{
		var publisherRepository = _unitOfWork.Repository<Publisher>();

		var suppliers = await publisherRepository.FindAllAsync(
			x => x.IsSupplierOrSubContractor, cancellationToken);

		var supplierDTO = suppliers.Select(x => new SupplierDTO (x.Id, x.Name ));
		return supplierDTO;
	}
}