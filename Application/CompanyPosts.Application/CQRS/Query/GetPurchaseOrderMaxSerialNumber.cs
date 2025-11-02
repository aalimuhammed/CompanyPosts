namespace CompanyPost.Application.CQRS.Query
{
    public record GetPurchaseOrderMaxSerialNumberQuery : IRequest<int>;
}