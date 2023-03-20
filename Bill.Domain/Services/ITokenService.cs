using Bill.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bill.Domain.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(List<Claim> claims);
    }
}
