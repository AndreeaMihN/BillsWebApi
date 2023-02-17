using MediatR;

namespace Bill.Application.Features.Clients.Commands.CreateClient;

public record CreateClientCommand(CreateClientDto createClientDto) : IRequest<bool>;