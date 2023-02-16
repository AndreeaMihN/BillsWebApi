using Bill.Domain.Clients;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public record GetClientsQuery : IRequest<List<Client>>;
}
