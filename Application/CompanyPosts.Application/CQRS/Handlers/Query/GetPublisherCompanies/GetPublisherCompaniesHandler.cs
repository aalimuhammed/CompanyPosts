namespace CompanyPost.Application.CQRS.Handlers.Query.GetPublisherCompanies
{
    internal class GetPublisherCompaniesHandler : IRequestHandler<GetPublisherCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPublisherCompaniesHandler(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CompanyDto>> Handle(GetPublisherCompaniesQuery request, CancellationToken cancellationToken)
        {
            var publisherRepository = _unitOfWork.Repository<Publisher>();
            var allCompanies = await publisherRepository.FindAllAsync(x => x.IsCompany, cancellationToken);
            var companiesDTO = allCompanies.Select(d => new CompanyDto(d.Id, d.Name));
            return companiesDTO;
        }
    }
}