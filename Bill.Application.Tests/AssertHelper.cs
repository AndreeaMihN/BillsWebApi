using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Domain.Clients;
using Xunit;

namespace Bill.Application.Tests
{
    public static class AssertHelper
    {
        public static bool AssertThatClientIsSameAs(this Client actualClient, Client expectedClient)
        {
            Assert.Multiple(() =>
            {
                Assert.Equal(actualClient.Id, expectedClient.Id);
                Assert.Equal(actualClient.FirstName, expectedClient.FirstName);
                Assert.Equal(actualClient.LastName, expectedClient.LastName);
                Assert.Equal(actualClient.Email, expectedClient.Email);
                Assert.Equal(actualClient.IsActive, expectedClient.IsActive);
                Assert.Equal(actualClient.PersonalIdentificationNumber, expectedClient.PersonalIdentificationNumber);
            });

            return true;
        }

        public static bool AssertThatClientIsSameAs(this CreateClientDto actualClient, CreateClientDto expectedClient)
        {
            Assert.Multiple(() =>
            {
                Assert.Equal(actualClient.FirstName, expectedClient.FirstName);
                Assert.Equal(actualClient.LastName, expectedClient.LastName);
                Assert.Equal(actualClient.Email, expectedClient.Email);
                Assert.Equal(actualClient.PersonalIdentificationNumber, expectedClient.PersonalIdentificationNumber);
            });

            return true;
        }
    }
}