using AutoFixture;
using Bill.Application.Features.Clients.Commands.CreateClient;
using BillsWebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BillsWebApi.Tests.Controllers
{
    public class ClientControllerTests
    {
        private readonly ClientController controller;
        private readonly ILogger<ClientController> logger;
        private readonly Mock<IMediator> mockMediator;
        private readonly Mock<ILogger<ClientController>> mockLogger;
        private readonly Fixture fixture;

        public ClientControllerTests()
        {
            mockMediator = new Mock<IMediator>();
            controller = new ClientController(mockMediator.Object, mockLogger.Object);
            fixture = new Fixture();
        }

        [Fact]
        public async void CreateClientAsync_ValidClient_ReturnsOkResult()
        {
            // Arrange
            var clientDto = fixture.Create<CreateClientDto>();

            mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await controller.CreateClientAsync(clientDto);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(viewResult);

            var boolResult = Assert.IsType<bool>(viewResult.Value);
            Assert.True(boolResult);

            mockMediator.Verify(mediator => mediator.Send(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}