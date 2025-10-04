namespace CompanyPost.Application.DTO.Response;
public record PostResponseDTO(
	Guid Id,
	string subject, 
	string document_number, 
	string serial_number, 
	DateTime date_of_post, 
	DateTime date_of_delivery, 
	string post_header, 
	string post_type,
	string created_by,
	string delivery_method, 
	//string delivery_person, 
	string post_original_sender,
	string project_name);