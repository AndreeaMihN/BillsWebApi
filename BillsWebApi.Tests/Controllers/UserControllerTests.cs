using AutoFixture;
using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Domain.Users;
using BillsWebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BillsWebApi.Tests.Controllers;

public class UserControllerTests
{
    private readonly UserController controller;
    private readonly ILogger<UserController> logger;
    private readonly Mock<IMediator> mockMediator;
    private readonly Mock<UserManager<ApplicationUser>> mockUserManager;
    private readonly Mock<RoleManager<ApplicationRole>> mockRoleManager;
    private readonly Mock<SignInManager<ApplicationUser>> mockSignInManager;
    private readonly Mock<ILogger<UserController>> mockLogger;
    private readonly Fixture fixture;

    public UserControllerTests()
    {
        mockMediator = new Mock<IMediator>();
        mockUserManager = new Mock<UserManager<ApplicationUser>>();
        mockLogger = new Mock<ILogger<UserController>>();
        controller = new UserController(mockMediator.Object, mockLogger.Object, mockUserManager.Object, mockRoleManager.Object, mockSignInManager.Object);
        fixture = new Fixture();
    }

    [Fact]
    public async void CreateUserAsync_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var clientDto = fixture.Create<CreateUserDto>();

        mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await controller.CreateUserAsync(clientDto);

        // Assert
        var viewResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(viewResult);

        var boolResult = Assert.IsType<bool>(viewResult.Value);
        Assert.True(boolResult);

        mockMediator.Verify(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}