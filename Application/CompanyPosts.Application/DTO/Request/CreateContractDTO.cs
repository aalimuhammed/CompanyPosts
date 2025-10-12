namespace CompanyPost.Application.DTO.Request;
public record CreateContractDTO(
	string Value ,
	string Details,
	string ContractNum,
	DateTime ContractDate,
	int SerialNumber,
	Guid PersonOrgId, 
	string Working,	
	string? Notes,
	Guid ProjectId,
	int Currency,
	string PurchaseOrdNumRef , 
	List<IFormFile> Attachments);
