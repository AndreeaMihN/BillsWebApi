using AutoFixture;
using Bill.Domain.Users;
using Bill.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using Bill.Application.Features.Users.Queries.GetUsers;

namespace Bill.Application.Tests.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandlerTests
{
    private readonly GetUsersHandler getUsersHandler;
    private readonly Mock<IBillUnitOfWork> mockBillUnitOfWork;
    private readonly Mock<ILogger<GetUsersHandler>> mockLogger;
    private readonly Fixture fixture;
    private readonly GetUsersQuery getUsersQuery;
    private readonly List<User> users;

    public GetUsersQueryHandlerTests()
    {
        mockBillUnitOfWork = new Mock<IBillUnitOfWork>();
        mockLogger = new Mock<ILogger<GetUsersHandler>>();
        getUsersHandler = new GetUsersHandler(mockBillUnitOfWork.Object, mockLogger.Object);

        fixture = new Fixture();
        getUsersQuery = fixture.Create<GetUsersQuery>();
        users = fixture.Create<List<User>>();
    }

    [Fact]
    public async void GetUsersHandler_ReturnValue()
    {
        //Arrange
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.UserReadOnlyRepository.GetAllUsers()).ReturnsAsync(users);

        //Act
        var result = await getUsersHandler.Handle(getUsersQuery, It.IsAny<CancellationToken>());

        //Assert
        mockBillUnitOfWork.Verify(billUnitOfWork => billUnitOfWork.UserReadOnlyRepository.GetAllUsers(), Times.Once);
        result.ShouldBeOfType<List<User>>();
        result.Count.ShouldBe(3);
    }

    [Fact]
    public async void GetUsersHandler_ReturnException()
    {
        //Arrange
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.UserReadOnlyRepository.GetAllUsers()).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await getUsersHandler.Handle(getUsersQuery, It.IsAny<CancellationToken>()));
    }
}