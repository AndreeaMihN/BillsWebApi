using Bill.Domain.CommonModels;

namespace Bill.Domain.Clients.Requests
{
    public record SearchClientRequest
    {
        public string? Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public string PersonalIdentificationNumber { get; init; }

        public bool IsActive { get; init; }

        public int Skip { get; init; }
        public int Take { get; init; } = Constants.DefaultNumberOfSearchResults;
    }
}