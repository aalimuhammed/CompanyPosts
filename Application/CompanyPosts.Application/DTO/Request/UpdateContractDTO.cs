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
	string department,
    string PurchaseOrdeRef,
	string oldReferenceNumber,
    Guid WorkTypeId,
	List<IFormFile>? Attachments ,
    List<Guid>? AttachmentIdsToDelete);