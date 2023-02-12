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
        public async Task<ActionResult> GetClients()
        {
            var clients = await _mediator.Send(new GetClientsQuery());
            return Ok(clients);
        }
    }
}
