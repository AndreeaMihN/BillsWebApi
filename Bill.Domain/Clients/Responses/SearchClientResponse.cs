using Bill.Domain.CommonModels;

namespace Bill.Domain.Clients.Responses
{
    public record SearchClientResponse : ResponseRecord
    {
        public IReadOnlyCollection<Client> Clients { get; init; } = new List<Client>();

        public int TotalCount { get; init; }
    }
}