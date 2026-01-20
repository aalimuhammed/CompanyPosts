namespace CompanyPost.Application.DTO.Response;
public record PostStatisticsDTO(
    int postInternal, 
    int postExternal, 
    int postTransformer,
    int inComing, 
    int contract,
    int purchaseOrder ,
    double contractSumEgp,
    double contractSumUsd,
    double contractSumSar,
    double contractSumEur,
    double purchaseOrderEgp,
    double purchaseOrderUsd,
    double purchaseOrderSar,
    double purchaseOrderEur);