namespace CompanyPost.Application.DTO.Response
{
    public record SelectedInComingByIdDTO(
        string DocumentNumber,
        string Subject,
        string Summary,
        string Notes,
        Guid PublishedArea,
        Guid WorkTypeId,
        DateTime DocumentDate,
        DateTime DeliveryDate,
        DateTime SavingDate,
        int DeliveryMethod,
       // Guid ProjectId,
        int DocumentType,
		int PostDocumentType);
}