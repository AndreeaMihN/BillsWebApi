using Bill.Domain.Clients.Responses;

namespace Bill.Domain.Clients;

public interface IClientCommandRepository
{
    public Task<ClientResponse> CreateAsync(Client client);

    //Task UpdateAsync(Client client);

    //Task RemoveAsync(string id);
}