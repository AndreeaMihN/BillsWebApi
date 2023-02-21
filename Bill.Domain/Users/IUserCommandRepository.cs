namespace Bill.Domain.Users;

public interface IUserCommandRepository
{
    Task CreateAsync(User client);

    //Task UpdateAsync(User client);

    //Task RemoveAsync(string id);
}