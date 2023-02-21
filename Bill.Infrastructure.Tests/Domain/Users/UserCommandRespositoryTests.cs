using AutoFixture;
using Bill.Domain.Users;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Users;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Bill.Infrastructure.Tests.Domain.Users;

public class UserCommandRespositoryTests
{
    private readonly UserCommandRepository clientCommandRepository;
    private readonly Mock<IUserContext> mockUserContext;
    private readonly Mock<ILogger<UserCommandRepository>> mockLogger;
    private readonly Fixture fixture = new Fixture();

    public UserCommandRespositoryTests()
    {
        mockUserContext = new Mock<IUserContext>();
        mockLogger = new Mock<ILogger<UserCommandRepository>>();
        clientCommandRepository = new UserCommandRepository(mockUserContext.Object, mockLogger.Object);
    }

    [Fact]
    public async void CreateAsync_ValidUser_SuccessfullyInserted()
    {
        // Arrange
        var client = fixture.Create<User>();

        mockUserContext.Setup(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

        // Act
        await clientCommandRepository.CreateAsync(client);

        // Assert
        mockUserContext.Verify(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async void CreateAsync_NullUser_ThrowsException()
    {
        // Arrange
        mockUserContext.Setup(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await clientCommandRepository.CreateAsync(null));

        mockUserContext.Verify(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async void CreateAsync_ValidUser_ThrowsException()
    {
        // Arrange
        var client = fixture.Create<User>();

        mockUserContext.Setup(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await clientCommandRepository.CreateAsync(client));

        mockUserContext.Verify(context => context.Users.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}