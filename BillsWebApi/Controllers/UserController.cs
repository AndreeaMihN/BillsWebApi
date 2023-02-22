using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Application.Features.Users.Queries.GetUsers;
using Bill.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BillsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IMediator mediator, ILogger<UserController> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _mediator = mediator;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        _logger.LogInformation("GET GetUsersAsync was performed");
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    {
        _logger.LogInformation("POST CreateUserAsync was performed");
        var result = await _mediator.Send(new CreateUserCommand(createUserDto));

        return Ok(result);
    }

    [HttpPost("create-identity-user")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        _logger.LogInformation("Create user by idenity started");

        try
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.FirstName + '.' + user.LastName,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create User failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new user {@ex}", ex);
        }
    }
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([Required] string name)
    {
        _logger.LogInformation("Create role started");

        try
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole { Name = name });
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create Role failed, {@exception}", ex);
            throw new Exception("Failed to create role {@ex}", ex);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password)
    {
        _logger.LogInformation("Login");
        try
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
                return Ok(result);
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError("Login failed, {@exception}", ex);
            throw new Exception("Failed to login {@ex}", ex);
        }
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        _logger.LogInformation("Logout");
        try
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Logout failed, {@exception}", ex);
            throw new Exception("Failed to logout {@ex}", ex);
        }
    }

}