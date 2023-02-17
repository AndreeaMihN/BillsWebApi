namespace Bill.Domain.Clients;

public interface IClientReadOnlyRepository
{
    Task<List<Client>> GetAllClients();

    //Task<Client> GetClientById(string id);
}