namespace CompanyPost.Application.CQRS.Commands.InComing;
public record CreateIncomingCommand(CreateIncomingDTO createIncomingDTO) 
	: IRequest<Unit>;