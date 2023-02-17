using AutoFixture;
using Bill.Domain.Clients;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Clients;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Bill.Infrastructure.Tests.Domain.Clients
{
    public class ClientCommandRespositoryTests
    {
        private readonly ClientCommandRepository clientCommandRepository;
        private readonly Mock<IClientContext> mockClientContext;
        private readonly Mock<ILogger<ClientCommandRepository>> mockLogger;
        private readonly Fixture fixture = new Fixture();

        public ClientCommandRespositoryTests()
        {
            mockClientContext = new Mock<IClientContext>();
            mockLogger = new Mock<ILogger<ClientCommandRepository>>();
            clientCommandRepository = new ClientCommandRepository(mockClientContext.Object, mockLogger.Object);
        }

        [Fact]
        public async void CreateAsync_ValidClient_SuccessfullyInserted()
        {
            // Arrange
            var client = fixture.Create<Client>();

            mockClientContext.Setup(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            // Act
            await clientCommandRepository.CreateAsync(client);

            // Assert
            mockClientContext.Verify(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void CreateAsync_NullClient_ThrowsException()
        {
            // Arrange
            mockClientContext.Setup(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await clientCommandRepository.CreateAsync(null));

            mockClientContext.Verify(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void CreateAsync_ValidClient_ThrowsException()
        {
            // Arrange
            var client = fixture.Create<Client>();

            mockClientContext.Setup(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await clientCommandRepository.CreateAsync(client));

            mockClientContext.Verify(context => context.Clients.InsertOneAsync(It.IsAny<Client>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}