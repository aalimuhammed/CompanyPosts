using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.DTO.Response
{
    public record SelectedPostTransformerByIdDTO(
    string DocumentNumber,
    string Subject,
    string Summary,
    string Notes,
    string? oldReferenceNumber,
    string? inComingNumber,
    Guid CompanyId,
    Guid PublisherId,
    Guid DeliveredId,
    Guid? WorkTypeId,
    DateTime DocumentDate,
    DateTime DeliveryDate,
    int DeliveryMethod,
    int? Status,
    string PostNumber, 
    int PostDocumentType,
    int DocumentType,
    List<AttachmentDTO> Attachments
            ) : SelectedPostByIdDTO(
                DocumentNumber,
                Subject,
                Summary,
                Notes,
                oldReferenceNumber,
                inComingNumber,
                CompanyId,
                PublisherId,
                DeliveredId,
                WorkTypeId,
                DocumentDate,
                DeliveryDate,
                DeliveryMethod,
                Status,
				PostDocumentType,
                Attachments
			);
}