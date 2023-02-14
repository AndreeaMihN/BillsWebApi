using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Application.Features.Clients.Queries.GetClients;
using Bill.Domain.Clients.Requests;
using Bill.Domain.Clients.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BillsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ClientController> logger;

        public ClientController(IMediator mediator, ILogger<ClientController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all clients.")]
        [ProducesResponseType(typeof(SearchClientResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Search()
        {
            logger.LogInformation("Search clients operation started");

            var searchClientResponse = await mediator.Send(new GetClientsQuery());

            logger.LogInformation("Search clients operation finished successfully with result {@searchClientResponse}.", searchClientResponse);

            return Ok(searchClientResponse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] ClientRequest clientRequest)
        {
            logger.LogInformation("Create client request is received: {@createClientRequest}", clientRequest);

            var clientResponse = await mediator.Send(new CreateClientCommand(clientRequest));

            logger.LogInformation("A new client {@createdClient} is created, {@clientnId}",
            clientResponse.Client, clientResponse.Client.Id);

            return Ok(clientResponse);
        }
    }
}