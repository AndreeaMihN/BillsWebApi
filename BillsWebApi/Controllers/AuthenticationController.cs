using Bill.Domain.DTOs;
using Bill.Domain.Requests;
using Bill.Domain.Responses;
using Bill.Domain.Services;
using Bill.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BillsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _configuration;

    public AuthenticationController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        ILogger<UserController> logger,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _logger = logger;
        _configuration = configuration;
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

            var result = await _roleManager.CreateAsync(role);

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
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest registerRequest)
    {
        _logger.LogInformation("Register user by idenity started");

        if (registerRequest.Password != registerRequest.ConfirmPassword)
        {
            return BadRequest(new { ErrorMessage = "Password does not match confirm password." });
        }

        try
        {
            if (await UserExists(registerRequest.Email)) return BadRequest("Email is taken");

            var appUser = new ApplicationUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email
            };

            var authClaims = new List<Claim>
                {
                    new Claim("id", appUser.Id.ToString()),
                    new Claim(ClaimTypes.Email, appUser.Email),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var result = await _userManager.CreateAsync(appUser, registerRequest.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                UserName = appUser.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateAccessToken(authClaims)),
                Email = appUser.Email
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Register User failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new user {@ex}", ex);
        }
    }

    //[HttpPost("login")]
    //[AllowAnonymous]
    //public async Task<ActionResult<UserDto>> LoginAndreea(LoginDto loginDto)
    //{
    //    _logger.LogInformation("Login");
    //    try
    //    {
    //        var user = _userManager.Users.SingleOrDefault(x => x.Email == loginDto.Email.ToLower());
    //        ApplicationUser appUser = await _userManager.FindByEmailAsync(loginDto.Email);
    //        if (appUser != null)
    //        {
    //            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
    //            if (!result.Succeeded) return Unauthorized();
    //            return new UserDto
    //            {
    //                UserName = appUser.UserName,
    //                Token = await _tokenService.CreateToken(appUser),
    //                Email = appUser.Email
    //            };
    //        }
    //        return Unauthorized("Invalid username");
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError("Login failed, {@exception}", ex);
    //        throw new Exception("Failed to login {@ex}", ex);
    //    }
    //}

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
    {
        if (loginModel is null)
        {
            return BadRequest("Invalid client request");
        }

        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var accessToken = _tokenService.GenerateAccessToken(authClaims);

            return Ok(new AuthenticationUserResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                AccessTokenExpirationTime = accessToken.ValidTo
            });
        }
        return Unauthorized();
    }
}