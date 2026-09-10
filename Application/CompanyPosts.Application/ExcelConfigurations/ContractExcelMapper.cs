using CompanyPost.Application.DTO;
using CompanyPost.Application.DTO.Response;


namespace CompanyPost.Application.Excel;

public static class ContractExcelMapper
{
    public static IEnumerable<ContractExcelRowModel> Map(
        IEnumerable<ContractReportResponseDTO> contracts)
    {
        foreach (var contract in contracts)
        {
            // العقد الأساسي
            yield return new ContractExcelRowModel(
                contract.Project,
                contract.ContractNum,
                contract.SerialNum,
                contract.WorkType,
                contract.ContractDate,
                contract.Department,
                contract.PurchaseOrderRef,
                contract.Currency,
                contract.PersonOrg,
                "أساسي",
                contract.CreatedBy,
                contract.CreatedAt,
                contract.Value,
                contract.ApprovalDeliveryDate,
                contract.DateOfReceipt
            );

            // الملاحق
            foreach (var reference in contract.References)
            {
                yield return new ContractExcelRowModel(
                    contract.Project,
                    reference.ContractNumber,
                    reference.DisplayNumber,
                    contract.WorkType,
                    reference.ContractDate,
                    contract.Department,
                    null,
                    reference.Currency,
                    null,
                    "ملحق",
                    reference.CreatedBy,
                    null,
                    reference.Value,
                    null,
                    null
                );
            }
        }
    }
}