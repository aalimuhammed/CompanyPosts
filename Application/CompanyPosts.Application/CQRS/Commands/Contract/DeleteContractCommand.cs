namespace CompanyPost.Application.CQRS.Commands.Contract;
public record DeleteContractCommand(Guid Id) : IRequest<Unit>;