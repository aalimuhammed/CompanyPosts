namespace CompanyPost.Application.CQRS.Query
{
   public record GetPurchaseOrderByFiltersQuery(PurchaseOrderFilterRequestDTO DTO)
        :IRequest<IEnumerable<PurchaseOrdersReportResponseDTO>>
    {
    }
}
