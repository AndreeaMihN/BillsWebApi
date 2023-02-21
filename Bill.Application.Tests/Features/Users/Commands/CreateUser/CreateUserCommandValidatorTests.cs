using Bill.Application.Common;
using Bill.Application.Features.Users.Commands.CreateUser;
using Xunit;

namespace Bill.Application.Tests.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator createUserCommandValidator;

    public CreateUserCommandValidatorTests()
    {
        createUserCommandValidator = new CreateUserCommandValidator();
    }

    [Fact]
    public void CreateUserValidator_ValidUser_ReturnSuccess()
    {
        var validUser = new CreateUserDto()
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@email.com",
            PersonalIdentificationNumber = "Test"
        };

        Assert.True(createUserCommandValidator.Validate(validUser).IsValid);
    }

    [Theory]
    [InlineData(null, null, null, null)]
    [InlineData("", "", "", "")]
    [InlineData(ValidatorsHelper.MaxLength256, ValidatorsHelper.MaxLength256, ValidatorsHelper.EmailWith256Characters, ValidatorsHelper.MaxLength256)]
    public void CreateUserValidator_InvalidUser_ReturnBadRequest(string firstName, string lastName, string email, string personalIdentificationNumber)
    {
        var validUser = new CreateUserDto()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PersonalIdentificationNumber = personalIdentificationNumber
        };

        Assert.False(createUserCommandValidator.Validate(validUser).IsValid);
    }

    [Fact]
    public void CreateUserValidator_InvalidEmailUser_ReturnBadRequest()
    {
        var validUser = new CreateUserDto()
        {
            FirstName = "Test",
            LastName = "Test",
            Email = ValidatorsHelper.InvalidEmail,
            PersonalIdentificationNumber = "Test"
        };

        Assert.False(createUserCommandValidator.Validate(validUser).IsValid);
    }
}