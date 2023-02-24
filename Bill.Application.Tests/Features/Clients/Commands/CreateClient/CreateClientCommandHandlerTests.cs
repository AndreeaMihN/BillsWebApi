using AutoFixture;
using AutoMapper;
using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bill.Application.Tests.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandlerTests
{
    private readonly CreateClientHandler createClientHandler;
    private readonly Mock<IBillUnitOfWork> mockBillUnitOfWork;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ILogger<CreateClientHandler>> mockLogger;
    private readonly Fixture fixture;
    private readonly CreateClientCommand command;
    private readonly Client entityClient;

    public CreateClientCommandHandlerTests()
    {
        mockMapper = new Mock<IMapper>();
        mockLogger = new Mock<ILogger<CreateClientHandler>>();
        mockBillUnitOfWork = new Mock<IBillUnitOfWork>();
        createClientHandler = new CreateClientHandler(mockBillUnitOfWork.Object, mockMapper.Object, mockLogger.Object);

        fixture = new Fixture();
        command = fixture.Create<CreateClientCommand>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
        entityClient = fixture.Create<Client>();
    }

    [Fact]
    public async void CreateClientHandler_ReturnTrue()
    {
        // Arrange
        mockMapper.Setup(mapper => mapper.Map<Client>(It.IsAny<CreateClientDto>())).Returns(entityClient);
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())); mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>()));

        // Act
        var result = await createClientHandler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        mockMapper.Verify(mapper => mapper.Map<Client>(It.Is<CreateClientDto>(x => x.Email == command.createClientDto.Email)), Times.Once);
        mockBillUnitOfWork.Verify(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.Is<Client>(x => x.AssertThatClientIsSameAs(entityClient))), Times.Once);

        Assert.True(result);
    }

    [Fact]
    public async void CreateClientHandler_ReturnException()
    {
        // Arrange
        mockMapper.Setup(mapper => mapper.Map<Client>(It.IsAny<CreateClientDto>())).Returns(entityClient);
        mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientCommandRepository.CreateAsync(It.IsAny<Client>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await createClientHandler.Handle(command, It.IsAny<CancellationToken>()));

        mockMapper.Verify(mapper => mapper.Map<Client>(It.Is<CreateClientDto>(x => x.AssertThatClientIsSameAs(command.createClientDto))), Times.Once);
    }
}