namespace CompanyPost.Application.DTO.Response;
public record ContractResponeDTO(
	Guid id,
	string value,
	string contract_number,
	string details,
	string notes,
	DateTime contract_date,
	string project_name,
	string created_by,
	string currency ,
	string working ,
	string purchase_order_ref,
	string contractor);
