using Bill.Domain.Clients;
using Bill.Infrastructure.Contexts;

namespace Bill.Infrastructure.Domain.Clients
{
    public class ClientCommandRepository : IClientCommandRepository
    {
        private readonly IClientContext _context;

        public ClientCommandRepository(IClientContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Client client)
        {
            try
            {
                if (client == null) throw new ArgumentNullException("client");
                await _context.Clients.InsertOneAsync(client);
            }
            catch (Exception ex)
            {
                var exception = ex;
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
}