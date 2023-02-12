using Bill.Domain.Clients;
using Bill.Domain.Repositories;

namespace Bill.Infrastructure.Repositories
{
    public class BillUnitOfWork : IBillUnitOfWork
    {
        public BillUnitOfWork(IClientReadOnlyRepository clientReadOnlyRepository, IClientCommandRepository clientCommandRepository)
        {
            ClientReadOnlyRepository = clientReadOnlyRepository;
            ClientCommandRepository = clientCommandRepository;
        }

        public IClientReadOnlyRepository ClientReadOnlyRepository { get; }

        public IClientCommandRepository ClientCommandRepository { get; }
    }
}