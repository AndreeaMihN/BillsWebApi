using Bill.Domain.Clients;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;

namespace Bill.Infrastructure.Domain.DomainNew;

public class ClientCommandRepository : IClientCommandRepository
{
    private readonly IClientContext _context;
    private readonly ILogger<ClientCommandRepository> _logger;

    public ClientCommandRepository(IClientContext context, ILogger<ClientCommandRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(Client client)
    {
        if (client == null) throw new ArgumentNullException("client");

        try
        {
            await _context.Clients.InsertOneAsync(client);
        }
        catch (Exception ex)
        {
            _logger.LogError("CreateAsync failed, {@exception}", ex);
            throw new Exception("Failed to insert in DB a new client {@ex}", ex);
        }
    }

    //public async Task RemoveAsync(string id)
    //{
    //    await _context.Clients.DeleteOneAsync(client => client.Id == id);
    //    return;
    //}

    //public async Task UpdateAsync(Client updatedClient)
    //{
    //    await _context.Clients.ReplaceOneAsync(client => client.Id == updatedClient.Id, updatedClient);
    //    return;
    //}
}