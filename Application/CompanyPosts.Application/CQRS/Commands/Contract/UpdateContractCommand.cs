namespace CompanyPost.Application.CQRS.Commands.Contract;
public record UpdateContractCommand(
    Guid Id,
    UpdateContractDTO UpdateContractDTO) : IRequest<Unit>;