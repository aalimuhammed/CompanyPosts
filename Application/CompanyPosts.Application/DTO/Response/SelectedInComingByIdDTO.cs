namespace CompanyPost.Application.DTO.Response
{
    public record SelectedInComingByIdDTO(
        string DocumentNumber,
        string Subject,
        string Summary,
        string Notes,
        string OldReferenceNumber,
        string InComingNumber,
        Guid PublishedArea,
        Guid? ProjectId,
       // Guid RecivedId,
        DateTime DocumentDate,
        DateTime DeliveryDate,
        int DeliveryMethod,
        int DocumentType,
		int PostDocumentType ,
        int Status,
        List<AttachmentDTO> Attachments);
}