using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Domain.Clients;
using Bill.Domain.Clients.Requests;

namespace BillsWebApi.Tests
{
    public static class AssertHelper
    {
        public static bool AssertThatClientIsSameAs(this CreateClientCommand createClientCommand, ClientRequest expectedClient)
        {
            var actualClient = createClientCommand.clientRequest;

            Assert.Multiple((() =>
            {
                Assert.That(actualClient.FirstName, Is.EqualTo(expectedClient.FirstName), $"{nameof(Client.FirstName)} did not match");
                Assert.That(actualClient.LastName, Is.EqualTo(expectedClient.LastName), $"{nameof(Client.LastName)} did not match");
                Assert.That(actualClient.Email, Is.EqualTo(expectedClient.Email), $"{nameof(Client.Email)} did not match");
                Assert.That(actualClient.PersonalIdentificationNumber, Is.EqualTo(expectedClient.PersonalIdentificationNumber), $"{nameof(Client.PersonalIdentificationNumber)} did not match");
            }));

            return true;
        }
    }
}