using Bill.Application.Features.Users.Commands.CreateUser;
using Swashbuckle.AspNetCore.Filters;

namespace Management.Api.SwaggerExamples;

public class CreateUserDtoExample : IExamplesProvider<CreateUserDto>
{
    public CreateUserDto GetExamples()
    {
        return new CreateUserDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email@email.com",
            PersonalIdentificationNumber = "PersonalIdentificationNumber"
        };
    }
}