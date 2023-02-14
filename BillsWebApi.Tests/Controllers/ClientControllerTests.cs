using AutoMapper;
using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Domain.Clients;
using Bill.Domain.Clients.Requests;
using Bill.Domain.Clients.Responses;
using Bill.Domain.CommonModels;
using Bill.Domain.Repositories;
using BillsWebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace BillsWebApi.Tests.Controllers
{
    public class ClientControllerTests
    {
        private IMediator mediator;
        private ILogger<ClientController> logger;
        private ClientController controller;
        private IBillUnitOfWork billUnitOfWork;
        private CreateClientHandler createClientHandler;
       // private readonly IMapper mapper;

        private Client client;
        private ClientRequest clientRequest;
        private ClientResponse clientResponse;

        private static readonly Guid Id = Guid.NewGuid();
        private static readonly Guid PersonalIdentificationNumber = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            var billUnitOfWork = Substitute.For<IBillUnitOfWork>();
            //var mapper = Substitute.For<Mapper>();
            //var createClientHandler = new CreateClientHandler(billUnitOfWork, mapper);

            client = new Client
            {
                Id = Id.ToString(),
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                IsActive = true,
                PersonalIdentificationNumber = PersonalIdentificationNumber.ToString()
            };

            clientRequest = new ClientRequest
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                PersonalIdentificationNumber = Guid.NewGuid().ToString()
            };

            clientResponse = new ClientResponse
            {
                ResponseCode = ResponseCodes.Success,
                DetailedResponseCode = DetailedResponseCodes.SuccessCodes.Success,
                Client = client
            };

            mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<CreateClientCommand>).Returns(clientResponse);

            logger = Substitute.For<ILogger<ClientController>>();

            controller = new ClientController(mediator, logger);
        }

        //[Test]
        //public async Task CreateClientSuccessfully()
        //{
        //    //var request = new ClientRequest
        //    //{
        //    //    FirstName = client.FirstName,
        //    //    LastName = client.LastName,
        //    //    Email = client.Email,
        //    //    PersonalIdentificationNumber = client.PersonalIdentificationNumber
        //    //};

            

        //    var response = await controller.Create(clientRequest);

        //    await mediator.Received(1).Send(
        //        Arg.Is<CreateClientCommand>(c => c.AssertThatClientIsSameAs(clientRequest)));
                

        //    //var clientResponse = await createClientHandler.Handle(new CreateClientCommand(clientRequest), new CancellationToken());
        //    //var actualResult = await GetActualResult(handler);


        //    var result = response as CreatedResult;

        //    Assert.That(result, Is.Not.Null, "Result should not be null");


        //    var actualClientResponse = result.Value as ClientResponse;

        //    Assert.That(actualClientResponse, Is.Not.Null, "ClientResponse should not be null");

        //    //actualClientResponse.AssertThatResponseCodesAreSuccessful();
        //    //actualClientResponse.Client.AssertThatClientIsSameAs(request);
        //}
    }
}