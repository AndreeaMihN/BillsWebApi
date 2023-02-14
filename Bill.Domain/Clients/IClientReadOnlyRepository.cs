using Bill.Domain.Clients.Responses;

namespace Bill.Domain.Clients
{
    public interface IClientReadOnlyRepository
    {
        public Task<SearchClientResponse> GetAllClients();

        //Task<Client> GetClientById(string id);
    }
}