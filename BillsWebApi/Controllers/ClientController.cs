using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Application.Features.Clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetClientsAsync()
        {
            var clients = await _mediator.Send(new GetClientsQuery());
            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult> CreateClientAsync([FromBody] CreateClientDto createClientDto)
        {
            var result = await _mediator.Send(new CreateClientCommand(createClientDto));

            return Ok(result);
        }
    }
}