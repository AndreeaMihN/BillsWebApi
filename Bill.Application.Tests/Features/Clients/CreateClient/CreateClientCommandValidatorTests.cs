using Bill.Application.Common;
using Bill.Application.Features.Clients.Commands.CreateClient;
using Xunit;

namespace Bill.Application.Tests.Features.Clients.CreateClient
{
    public class CreateClientCommandValidatorTests
    {
        private readonly CreateClientCommandValidator createClientCommandValidator;

        public CreateClientCommandValidatorTests()
        {
            createClientCommandValidator = new CreateClientCommandValidator();
        }

        [Fact]
        public void CreateClientValidator_ValidClient_ReturnSuccess()
        {
            var validClient = new CreateClientDto()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                PersonalIdentificationNumber = "Test"
            };

            Assert.True(createClientCommandValidator.Validate(validClient).IsValid);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        [InlineData(ValidatorsHelper.MaxLength256, ValidatorsHelper.MaxLength256, ValidatorsHelper.EmailWith256Characters, ValidatorsHelper.MaxLength256)]
        public void CreateClientValidator_InvalidClient_ReturnBadRequest(string firstName, string lastName, string email, string personalIdentificationNumber)
        {
            var validClient = new CreateClientDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PersonalIdentificationNumber = personalIdentificationNumber
            };

            Assert.False(createClientCommandValidator.Validate(validClient).IsValid);
        }

        [Fact]
        public void CreateClientValidator_InvalidEmailClient_ReturnSuccess()
        {
            var validClient = new CreateClientDto()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = ValidatorsHelper.InvalidEmail,
                PersonalIdentificationNumber = "Test"
            };

            Assert.False(createClientCommandValidator.Validate(validClient).IsValid);
        }
    }
}