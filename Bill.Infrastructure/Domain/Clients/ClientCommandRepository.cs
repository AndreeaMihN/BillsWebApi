using Bill.Domain.Clients;
using Bill.Domain.Clients.Responses;
using Bill.Domain.CommonModels;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;

namespace Bill.Infrastructure.Domain.Clients;

public class ClientCommandRepository : IClientCommandRepository
{
    private readonly IClientContext context;
    private readonly ILogger<ClientCommandRepository> logger;

    public ClientCommandRepository(IClientContext context, ILogger<ClientCommandRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<ClientResponse> CreateAsync(Client client)
    {
        try
        {
            if (client == null) throw new ArgumentNullException("client");
            await context.Clients.InsertOneAsync(client);

            logger.LogInformation("Client was created in DB with id = {@clientId}", client.Id);
        }
        catch (Exception ex)
        {
            logger.LogError("Client was not created in db: {@ex}}", ex);
        }

        return CreateSuccessfulClientResponse(client);
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

    private static ClientResponse CreateSuccessfulClientResponse(Client client) => new()
    {
        ResponseCode = ResponseCodes.Success,
        DetailedResponseCode = DetailedResponseCodes.SuccessCodes.Success,
        Client = client
    };
}