namespace Bill.Domain.Clients
{
    public interface IClientCommandRepository
    {
        Task CreateAsync(Client client);

        //Task UpdateAsync(Client client);

        //Task RemoveAsync(string id);
    }
}