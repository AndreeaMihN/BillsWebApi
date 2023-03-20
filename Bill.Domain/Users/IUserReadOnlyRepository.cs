namespace Bill.Domain.Users;

public interface IUserReadOnlyRepository
{
    Task<List<User>> GetAllUsers();

    //Task<User> GetUserById(string id);
}