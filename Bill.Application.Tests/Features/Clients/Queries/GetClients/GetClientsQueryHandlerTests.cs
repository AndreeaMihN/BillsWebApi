using AutoFixture;
using Bill.Application.Features.Clients.Queries.GetClients;
using Bill.Application.Tests.Mocks;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace Bill.Application.Tests.Features.Clients.Queries.GetClients
{
    public class GetClientsQueryHandlerTests
    {
        private readonly GetClientsHandler getClientsHandler;
        private readonly Mock<IBillUnitOfWork> mockBillUnitOfWork;
        //private readonly Mock<IMapper> mockMapper;
        private readonly Fixture fixture;
        private readonly GetClientsQuery getClientsQuery;
        private readonly List<Client> clients;

        public GetClientsQueryHandlerTests()
        {
            //mockMapper = new Mock<IMapper>();
            mockBillUnitOfWork = new Mock<IBillUnitOfWork>();
            getClientsHandler = new GetClientsHandler(mockBillUnitOfWork.Object);

            fixture = new Fixture();
            getClientsQuery = fixture.Create<GetClientsQuery>();
            clients = fixture.Create<List<Client>>();
        }

        [Fact]
        public async void GetClientsHandler_ReturnValue()
        {
            //Arrange
            mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientReadOnlyRepository.GetAllClients()).ReturnsAsync(ClientMocks.GetClientList());

            //Act
            var result = await getClientsHandler.Handle(getClientsQuery, It.IsAny<CancellationToken>());

            //Assert
            mockBillUnitOfWork.Verify(billUnitOfWork => billUnitOfWork.ClientReadOnlyRepository.GetAllClients(), Times.Once);
            result.ShouldBeOfType<List<Client>>();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public async void GetClientsHandler_ReturnException()
        {
            //Arrange
            mockBillUnitOfWork.Setup(billUnitOfWork => billUnitOfWork.ClientReadOnlyRepository.GetAllClients()).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await getClientsHandler.Handle(getClientsQuery, It.IsAny<CancellationToken>()));
        }
    }
}
