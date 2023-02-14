using Bill.Domain.Clients;
using Bill.Domain.Clients.Responses;
using Bill.Domain.CommonModels;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bill.Infrastructure.Domain.Clients
{
    public class ClientReadOnlyRepository : IClientReadOnlyRepository
    {
        private readonly IClientContext context;
        private readonly ILogger<ClientReadOnlyRepository> logger;

        public ClientReadOnlyRepository(IClientContext context, ILogger<ClientReadOnlyRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<SearchClientResponse> GetAllClients()
        {
            logger.LogInformation("Searching for clients in the DB started");

            var clients = await context.Clients.Find(new BsonDocument()).ToListAsync();
            var totalCount = clients.Count();

            logger.LogInformation("Clients search completed successfully with total count - {@totalCount}", totalCount);

            return new SearchClientResponse
            {
                ResponseCode = ResponseCodes.Success,
                DetailedResponseCode = DetailedResponseCodes.SuccessCodes.Success,
                Clients = clients,
                TotalCount = totalCount
            };
        }

        //public async Task<Client> GetAsync(string id)
        //{
        //    return await _context.Clients.Find(client => client.Id == id).FirstOrDefaultAsync();
        //}
    }
}