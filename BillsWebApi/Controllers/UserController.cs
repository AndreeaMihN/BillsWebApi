using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillsWebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        _logger.LogInformation("GET GetUsersAsync was performed");
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    {
        _logger.LogInformation("POST CreateUserAsync was performed");
        var result = await _mediator.Send(new CreateUserCommand(createUserDto));

        return Ok(result);
    }

}