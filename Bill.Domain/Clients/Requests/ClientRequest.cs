using AutoMapper;

namespace Bill.Domain.Clients.Requests;

[AutoMap(typeof(Client), ReverseMap = true)]
public record ClientRequest
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string PersonalIdentificationNumber { get; init; }

    public ClientRequest()
    {
    }

    public ClientRequest(ClientRequest clientRequest)
    {
        FirstName = clientRequest.FirstName;
        LastName = clientRequest.LastName;
        PersonalIdentificationNumber = clientRequest.PersonalIdentificationNumber;
    }
}