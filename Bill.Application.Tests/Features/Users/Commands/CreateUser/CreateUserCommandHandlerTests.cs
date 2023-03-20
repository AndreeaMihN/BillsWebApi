using AutoFixture;
using AutoMapper;
using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Domain.Users;
using Bill.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bill.Application.Tests.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly CreateUserHandler createUserHandler;
    private readonly Mock<IBillUnitOfWork> mockBillUnitOfWork;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ILogger<CreateUserHandler>> mockLogger;
    private readonly Fixture fixture;
    private readonly CreateUserCommand command;
    private readonly User entityUser;

    public CreateUserCommandHandlerTests()
    {
        mockMapper = new Mock<IMapper>();
        mockLogger = new Mock<ILogger<CreateUserHandler>>();
        mockBillUnitOfWork = new Mock<IBillUnitOfWork>();
        createUserHandler = new CreateUserHandler(mockBillUnitOfWork.Object, mockMapper.Object, mockLogger.Object);

        fixture = new Fixture();
        command = fixture.Create<CreateUserCommand>();
        entityUser = fixture.Create<User>();
    }

    [Fact]
    public async void CreateUserHandler_ReturnTrue()
    {
        // Arrange
        mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<CreateUserDto>())).Returns(entityUser);
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.UserCommandRepository.CreateAsync(It.IsAny<User>()));

        // Act
        var result = await createUserHandler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        mockMapper.Verify(mapper => mapper.Map<User>(It.Is<CreateUserDto>(x => x.Email == command.createUserDto.Email)), Times.Once);
        mockBillUnitOfWork.Verify(billUnitOfWork => billUnitOfWork.UserCommandRepository.CreateAsync(It.Is<User>(x => x.AssertThatUserIsSameAs(entityUser))), Times.Once);

        Assert.True(result);
    }

    [Fact]
    public async void CreateUserHandler_ReturnException()
    {
        // Arrange
        mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<CreateUserDto>())).Returns(entityUser);
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.UserCommandRepository.CreateAsync(It.IsAny<User>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await createUserHandler.Handle(command, It.IsAny<CancellationToken>()));

        mockMapper.Verify(mapper => mapper.Map<User>(It.Is<CreateUserDto>(x => x.AssertThatUserIsSameAs(command.createUserDto))), Times.Once);
    }
}