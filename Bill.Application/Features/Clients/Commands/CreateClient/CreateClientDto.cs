using AutoMapper;
using Bill.Domain.Clients;

namespace Bill.Application.Features.Clients.Commands.CreateClient;

[AutoMap(typeof(Client), ReverseMap = true)]
public class CreateClientDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PersonalIdentificationNumber { get; set; }

    public CreateClientDto()
    {
    }

    public CreateClientDto(CreateClientDto createClientDto)
    {
        FirstName = createClientDto.FirstName;
        LastName = createClientDto.LastName;
        PersonalIdentificationNumber = createClientDto.PersonalIdentificationNumber;
    }
}