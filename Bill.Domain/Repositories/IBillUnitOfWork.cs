using Bill.Domain.Users;

namespace Bill.Domain.Repositories;

public interface IBillUnitOfWork
{
    IUserReadOnlyRepository UserReadOnlyRepository { get; }
    IUserCommandRepository UserCommandRepository { get; }
}