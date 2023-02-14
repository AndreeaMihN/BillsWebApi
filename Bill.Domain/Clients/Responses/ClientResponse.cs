using Bill.Domain.CommonModels;

namespace Bill.Domain.Clients.Responses
{
    public record ClientResponse : ResponseRecord
    {
        public Client Client { get; init; } = new();
    }
}