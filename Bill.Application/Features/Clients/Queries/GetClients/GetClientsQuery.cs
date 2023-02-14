using Bill.Domain.Clients.Responses;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public record GetClientsQuery : IRequest<SearchClientResponse>;
}