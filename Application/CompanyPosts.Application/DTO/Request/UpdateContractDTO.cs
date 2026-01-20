namespace CompanyPost.Application.DTO.Request;
public record UpdateContractDTO(
	Guid Id,
	double Value,
	string Details,
	string ContractNum,
	DateTime ContractDate,
	Guid PersonOrgId,
	string Working,
	string? Notes,
	Guid ProjectId,
	string Currency,
	string PurchaseOrdeRef,
	IFormFile? Attachments);