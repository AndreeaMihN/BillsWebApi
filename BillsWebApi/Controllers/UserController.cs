using Bill.Application.Features.Users.Queries.GetUsers;
using Bill.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BillsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsersAsync()
    {
        _logger.LogInformation("GET GetUsersAsync was performed");
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    //[HttpPost]
    //public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    //{
    //    _logger.LogInformation("POST CreateUserAsync was performed");
    //    var result = await _mediator.Send(new CreateUserCommand(createUserDto));

    //    return Ok(result);
    //}

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] User user)
    {
        _logger.LogInformation("POST CreateUserAsync started");

        ApplicationUser appUser = new ApplicationUser
        {
            UserName = user.FirstName + '.' + user.LastName,
            Email = user.Email
        };

        try
        {
            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create User failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new user {@ex}", ex);
        }

    }
}