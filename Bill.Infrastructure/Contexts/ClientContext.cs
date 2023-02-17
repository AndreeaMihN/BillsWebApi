using Bill.Domain.Clients;
using Bill.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bill.Infrastructure.Contexts;

public interface IClientContext
{
    IMongoCollection<Client> Clients { get; }
}

public class ClientContext : IClientContext
{
    private readonly IMongoDatabase db;

    public ClientContext(IOptions<ClientConfiguration> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        db = client.GetDatabase(options.Value.Database);
    }

    public IMongoCollection<Client> Clients => db.GetCollection<Client>("Client");
}