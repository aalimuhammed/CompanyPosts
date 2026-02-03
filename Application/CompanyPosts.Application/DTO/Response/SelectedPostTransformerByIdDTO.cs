using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.DTO.Response
{
    public record SelectedPostTransformerByIdDTO(
    string DocumentNumber,
    string Subject,
    string Summary,
    string Notes,
    Guid CompanyId,
    Guid PublisherId,
    Guid DeliveredId,
    Guid WorkTypeId,
    DateTime DocumentDate,
    DateTime DeliveryDate,
    int DeliveryMethod,
    string InComingNumber,  
    string PostNumber, 
    string RecivedByName ,
    int PostDocumentType,
    int DocumentType
			) : SelectedPostByIdDTO(
                DocumentNumber,
                Subject,
                Summary,
                Notes,
                CompanyId,
                PublisherId,
                DeliveredId,
                WorkTypeId,
                DocumentDate,
                DeliveryDate,
                DeliveryMethod,
				PostDocumentType
			);
}