using Bill.Domain.Clients.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Management.Api.SwaggerExamples;

public class CreateClientDtoExample : IExamplesProvider<ClientRequest>
{
    public ClientRequest GetExamples()
    {
        return new ClientRequest
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email@email.com",
            PersonalIdentificationNumber = "PersonalIdentificationNumber"
        };
    }
}