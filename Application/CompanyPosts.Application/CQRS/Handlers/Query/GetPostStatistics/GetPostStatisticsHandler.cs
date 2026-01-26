namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostStatistics;
internal sealed class GetPostStatisticsHandler : IRequestHandler<GetPostStatisticsQuery, PostStatisticsDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPostStatisticsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<PostStatisticsDTO> Handle(GetPostStatisticsQuery request, CancellationToken cancellationToken)
    {
        var postInternalRepo = _unitOfWork.Repository<PostInternal>();
        var postExternalRepo = _unitOfWork.Repository<PostExternal>();
        var postTransformerRepo = _unitOfWork.Repository<PostTransformer>();

        var contractRepo = _unitOfWork.Repository<Contracts>();
        var contractRefRepo = _unitOfWork.Repository<ContractRef>();

        var purchaseOrderRepo = _unitOfWork.Repository<PurchaseOrder>();

        var inComingRepo = _unitOfWork.Repository<InComing>();

        var contractCurrencyRepo = _unitOfWork.CurrencyRepository<Contracts>();
        var contractRefCurrencyRepo = _unitOfWork.CurrencyRepository<ContractRef>();
        var purchaseOrderCurrencyRepo = _unitOfWork.CurrencyRepository<PurchaseOrder>();

        var postInternalCount = await postInternalRepo.CountAsync(cancellationToken);
        var postExternalCount = await postExternalRepo.CountAsync(cancellationToken);
        var postTransformerCount = await postTransformerRepo.CountAsync(cancellationToken);

        var contractCount = await contractRepo.CountAsync(cancellationToken);

        var purchaseOrderCount = await purchaseOrderRepo.CountAsync(cancellationToken);

        var inComingCount = await inComingRepo.CountAsync(cancellationToken);

        var totalValueContractsUSD = await contractCurrencyRepo.SumForCurrency(Currency.USD, cancellationToken);
        var totalValueContractsEUR = await contractCurrencyRepo.SumForCurrency(Currency.EUR , cancellationToken);
        var totalValueContractsEGP = await contractCurrencyRepo.SumForCurrency(Currency.EGP , cancellationToken);
        var totalValueContractsSAR = await contractCurrencyRepo.SumForCurrency(Currency.SAR , cancellationToken);


        var totalValueContractsRefUSD = await contractRefCurrencyRepo.SumForCurrency(Currency.USD, cancellationToken);
        var totalValueContractsRefEUR = await contractRefCurrencyRepo.SumForCurrency(Currency.EUR, cancellationToken);
        var totalValueContractsRefEGP = await contractRefCurrencyRepo.SumForCurrency(Currency.EGP, cancellationToken);
        var totalValueContractsRefSAR = await contractRefCurrencyRepo.SumForCurrency(Currency.SAR, cancellationToken);

        var totalValueAllContractsUSD = totalValueContractsUSD + totalValueContractsRefUSD;
        var totalValueAllContractsEGP = totalValueContractsEGP + totalValueContractsRefEGP;
        var totalValueAllContractsEUR = totalValueContractsEUR + totalValueContractsRefEUR;
        var totalValueAllContractsSAR = totalValueContractsSAR + totalValueContractsRefSAR;

        var totalValuePurchaseOrderUSD = await purchaseOrderCurrencyRepo.SumForCurrency(Currency.USD, cancellationToken);
        var totalValuePurchaseOrderEUR = await purchaseOrderCurrencyRepo.SumForCurrency(Currency.EUR, cancellationToken);
        var totalValuePurchaseOrderEGP = await purchaseOrderCurrencyRepo.SumForCurrency(Currency.EGP, cancellationToken);
        var totalValuePurchaseOrderSAR = await purchaseOrderCurrencyRepo.SumForCurrency(Currency.SAR, cancellationToken);

        return new PostStatisticsDTO(
                postInternalCount,
                postExternalCount,
                postTransformerCount,
                inComingCount,
                contractCount,
                purchaseOrderCount,

                Format(totalValueAllContractsEGP),
                Format(totalValueAllContractsUSD),
                Format(totalValueAllContractsSAR),
                Format(totalValueAllContractsEUR),
                Format(totalValuePurchaseOrderEGP),
                Format(totalValuePurchaseOrderUSD),
                Format(totalValuePurchaseOrderSAR),
                Format(totalValuePurchaseOrderEUR));
    }
    private string Format(decimal value)
    {
        return value.ToString("N2");
    }
}