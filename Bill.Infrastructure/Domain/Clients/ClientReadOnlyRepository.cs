using Bill.Domain.Clients;
using Bill.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bill.Infrastructure.Domain.Clients
{
    public class ClientReadOnlyRepository : IClientReadOnlyRepository
    {
        private readonly IClientContext _context;
        private readonly ILogger<ClientReadOnlyRepository> _logger;

        public ClientReadOnlyRepository(IClientContext context)
        {
            _context = context;

        }
        public async Task<List<Client>> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            try
            {
                return await _context.Clients.Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllClients failed, {@exception}", ex);
                throw new Exception("Failed to get list with all clients from DB {@ex}", ex);
            }

        }

        //public async Task<Client> GetAsync(string id)
        //{
        //    return await _context.Clients.Find(client => client.Id == id).FirstOrDefaultAsync();
        //}
    }
}
