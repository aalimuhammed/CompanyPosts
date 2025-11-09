namespace CompanyPost.Application.DTO.Request.Base
{
    public record BaseFilterRequestDTO(
                Guid? ProjectId,
                string? DepartmentId,
                Guid? PublisherId,
                DateTime? StartDate,
                DateTime? EndDate
            );
}