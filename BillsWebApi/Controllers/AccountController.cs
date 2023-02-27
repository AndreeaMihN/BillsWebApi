using Bill.Domain.DTOs;
using Bill.Domain.Services;
using Bill.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BillsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<UserController> _logger;


    public AccountController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        ILogger<UserController> logger
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _logger = logger;
    }
    //private async Task<bool> UserExists(string username)
    //{
    //    return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    //}

    private async Task<bool> UserExists(string username)
    {
        return _userManager.Users.Any(x => x.UserName == username.ToLower());
    }

    [HttpPost("create-role")]
    public async Task<ActionResult<ApplicationRole>> CreateRole(string name)
    {
        try
        {
            var role = new ApplicationRole() { Name = name };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return role;
        }
        catch (Exception ex)
        {
            _logger.LogError("Create Role failed, {@exception}", ex);
            throw new Exception("Failed to create role {@ex}", ex);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Create([FromBody] User user)
    {
        _logger.LogInformation("Create user by idenity started");

        try
        {
            if (await UserExists(user.Email)) return BadRequest("Email is taken");
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(appUser, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);
            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(appUser),
                Email = user.Email
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Create User failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new user {@ex}", ex);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        _logger.LogInformation("Login");
        try
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Email == loginDto.Email.ToLower());
            ApplicationUser appUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (appUser != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
                if (!result.Succeeded) return Unauthorized();
                return new UserDto
                {
                    UserName = appUser.UserName,
                    Token = await _tokenService.CreateToken(appUser),
                    Email = appUser.Email
                };
            }
            return Unauthorized("Invalid username");
        }
        catch (Exception ex)
        {
            _logger.LogError("Login failed, {@exception}", ex);
            throw new Exception("Failed to login {@ex}", ex);
        }
    }

    //[Authorize]
    //public async Task<IActionResult> Logout()
    //{
    //    _logger.LogInformation("Logout");
    //    try
    //    {
    //        await _signInManager.SignOutAsync();
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError("Logout failed, {@exception}", ex);
    //        throw new Exception("Failed to logout {@ex}", ex);
    //    }
    //}


}
