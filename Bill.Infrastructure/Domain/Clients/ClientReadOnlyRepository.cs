using Bill.Domain.Clients;
using Bill.Infrastructure.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bill.Infrastructure.Domain.Clients
{
    public class ClientReadOnlyRepository : IClientReadOnlyRepository
    {
        private readonly IClientContext _context;

        public ClientReadOnlyRepository(IClientContext context)
        {
            _context = context;

        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients.Find(new BsonDocument()).ToListAsync();
        }

        //public async Task<Client> GetAsync(string id)
        //{
        //    return await _context.Clients.Find(client => client.Id == id).FirstOrDefaultAsync();
        //}
    }
}
