using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public class GetClientsHandler : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        //private readonly IMapper _mapper;

        public GetClientsHandler(IBillUnitOfWork billUnitOfWork)
        {
            _billUnitOfWork = billUnitOfWork;
            //_mapper = mapper;
        }

        public async Task<IEnumerable<Client>> Handle(GetClientsQuery request,
            CancellationToken cancellationToken) => await _billUnitOfWork.ClientReadOnlyRepository.GetAllClients();
    }
}