using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Application.Features.Clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IMediator mediator, ILogger<ClientController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetClientsAsync()
    {
        _logger.LogInformation("GET GetClientsAsync was performed");
        var clients = await _mediator.Send(new GetClientsQuery());
        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult> CreateClientAsync([FromBody] CreateClientDto createClientDto)
    {
        _logger.LogInformation("POST CreateClientAsync was performed");
        var result = await _mediator.Send(new CreateClientCommand(createClientDto));

        return Ok(result);
    }
}