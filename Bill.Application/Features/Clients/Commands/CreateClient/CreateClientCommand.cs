using Bill.Domain.Clients.Requests;
using Bill.Domain.Clients.Responses;
using MediatR;

namespace Bill.Application.Features.Clients.Commands.CreateClient
{
    public record CreateClientCommand(ClientRequest clientRequest) : IRequest<ClientResponse>;
}