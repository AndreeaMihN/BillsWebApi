using Bill.Domain.Users;
using Bill.Domain.Repositories;

namespace Bill.Infrastructure.Repositories;

public class BillUnitOfWork : IBillUnitOfWork
{
    public BillUnitOfWork(IUserReadOnlyRepository clientReadOnlyRepository, IUserCommandRepository clientCommandRepository)
    {
        UserReadOnlyRepository = clientReadOnlyRepository;
        UserCommandRepository = clientCommandRepository;
    }

    public IUserReadOnlyRepository UserReadOnlyRepository { get; }

    public IUserCommandRepository UserCommandRepository { get; }
}