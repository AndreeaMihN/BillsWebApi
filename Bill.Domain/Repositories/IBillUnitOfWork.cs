using Bill.Domain.Clients;

namespace Bill.Domain.Repositories;

public interface IBillUnitOfWork
{
    IClientReadOnlyRepository ClientReadOnlyRepository { get; }
    IClientCommandRepository ClientCommandRepository { get; }
}