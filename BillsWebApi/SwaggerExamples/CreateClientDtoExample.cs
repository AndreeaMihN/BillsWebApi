using Bill.Application.Features.Clients.Commands.CreateClient;
using Swashbuckle.AspNetCore.Filters;

namespace Management.Api.SwaggerExamples;

public class CreateClientDtoExample : IExamplesProvider<CreateClientDto>
{
    public CreateClientDto GetExamples()
    {
        return new CreateClientDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email@email.com",
            PersonalIdentificationNumber = "PersonalIdentificationNumber"
        };
    }
}