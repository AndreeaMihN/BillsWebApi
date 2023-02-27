using Bill.Domain.Users;

namespace Bill.Domain.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}
