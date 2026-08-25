namespace CompanyPost.Application.CQRS.Commands;
public record CreateContractCommand(CreateContractDTO CreateContractDTO) 
    : IRequest<Unit>;