namespace CompanyPost.Application.DTO.Request;
public record CreateContractDTO(
	string Value ,
	string Details,
	string ContractNum,
	DateTime ContractDate,
	Guid PersonOrgId,
	Guid WorkTypeId,
	string? Notes,
	Guid ProjectId,
	int Currency,
	int? Department,
	string? PurchaseOrdNumRef ,
	ContractTypes HasReference,
	string? BaseContractId,
    List<IFormFile>? Attachments);
