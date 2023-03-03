using Bill.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bill.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        //private readonly SymmetricSecurityKey _key;
        //private readonly UserManager<ApplicationUser> _userManger;
        //private readonly IUserContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            //_userManger = userManger;
            _configuration = configuration;
            // _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
        }

        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        //public async Task<string> CreateToken(ApplicationUser user)
        //{
        //    var claims = new List<Claim>{
        //        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        //    };

        //    var roles = await _userManger.GetRolesAsync(user);
        //    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        //    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.Now.AddHours(1),
        //        SigningCredentials = creds
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}