using Bill.Domain.Users;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;

namespace Bill.Infrastructure.Domain.Users;

public class UserCommandRepository : IUserCommandRepository
{
    private readonly IUserContext _context;
    private readonly ILogger<UserCommandRepository> _logger;

    public UserCommandRepository(IUserContext context, ILogger<UserCommandRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(User client)
    {
        if (client == null) throw new ArgumentNullException("client");

        try
        {
            await _context.Users.InsertOneAsync(client);
        }
        catch (Exception ex)
        {
            _logger.LogError("CreateAsync failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new user {@ex}", ex);
        }
    }

    //public async Task RemoveAsync(string id)
    //{
    //    await _context.Users.DeleteOneAsync(client => client.Id == id);
    //    return;
    //}

    //public async Task UpdateAsync(User updatedUser)
    //{
    //    await _context.Users.ReplaceOneAsync(client => client.Id == updatedUser.Id, updatedUser);
    //    return;
    //}
}