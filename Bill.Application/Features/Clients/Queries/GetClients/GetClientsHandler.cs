using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public class GetClientsHandler : IRequestHandler<GetClientsQuery, List<Client>>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        //private readonly IMapper _mapper;

        public GetClientsHandler(IBillUnitOfWork billUnitOfWork)
        {
            _billUnitOfWork = billUnitOfWork;
            //_mapper = mapper;
        }

        public async Task<List<Client>> Handle(GetClientsQuery request,
            CancellationToken cancellationToken)
        {
            List<Client> clients = new List<Client>();
            try
            {
                clients = await _billUnitOfWork.ClientReadOnlyRepository.GetAllClients();
            }
            catch
            {
                throw new Exception("Failed to get list with all clients");
            }

            return clients;
        }

    }
}