using Bill.Domain.Users;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bill.Infrastructure.Domain.Users;

public class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly IUserContext _context;
    private readonly ILogger<UserReadOnlyRepository> _logger;

    public UserReadOnlyRepository(IUserContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        List<User> clients = new List<User>();
        try
        {
            return await _context.Users.Find(new BsonDocument()).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("GetAllUsers failed, {@exception}", ex);
            throw new Exception("Failed to get list with all users from DB {@ex}", ex);
        }
    }

    //public async Task<User> GetAsync(string id)
    //{
    //    return await _context.Users.Find(client => client.Id == id).FirstOrDefaultAsync();
    //}
}