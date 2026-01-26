namespace CompanyPost.Application.DTO.Response;
public record PostStatisticsDTO(
    int postInternal, 
    int postExternal, 
    int postTransformer,
    int inComing, 
    int contract,
    int purchaseOrder ,
    string contractSumEgp,
    string contractSumUsd,
    string contractSumSar,
    string contractSumEur,
    string purchaseOrderEgp,
    string purchaseOrderUsd,
    string purchaseOrderSar,
    string purchaseOrderEur);