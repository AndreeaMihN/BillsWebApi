using Bill.Domain.Users;
using Bill.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bill.Infrastructure.Contexts;

public interface IUserContext
{
    IMongoCollection<User> Users { get; }
}

public class UserContext : IUserContext
{
    private readonly IMongoDatabase db;

    public UserContext(IOptions<UserConfiguration> options)
    {
        var user = new MongoClient(options.Value.ConnectionString);
        db = user.GetDatabase(options.Value.Database);
    }

    public IMongoCollection<User> Users => db.GetCollection<User>("Users");
}