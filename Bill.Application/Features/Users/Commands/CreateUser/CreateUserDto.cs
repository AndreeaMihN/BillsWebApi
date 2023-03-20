using AutoMapper;
using Bill.Domain.Users;

namespace Bill.Application.Features.Users.Commands.CreateUser;

[AutoMap(typeof(User), ReverseMap = true)]
public class CreateUserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PersonalIdentificationNumber { get; set; }

    public CreateUserDto()
    {
    }

    public CreateUserDto(CreateUserDto createUserDto)
    {
        FirstName = createUserDto.FirstName;
        LastName = createUserDto.LastName;
        PersonalIdentificationNumber = createUserDto.PersonalIdentificationNumber;
    }
}