using Bill.Domain.Clients.Responses;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public class GetClientsHandler : IRequestHandler<GetClientsQuery, SearchClientResponse>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        //private readonly IMapper _mapper;

        public GetClientsHandler(IBillUnitOfWork billUnitOfWork)
        {
            _billUnitOfWork = billUnitOfWork;
            //_mapper = mapper;
        }

        public async Task<SearchClientResponse> Handle(GetClientsQuery request,
            CancellationToken cancellationToken) => await _billUnitOfWork.ClientReadOnlyRepository.GetAllClients();
    }
}