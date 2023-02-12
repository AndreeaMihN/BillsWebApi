using AutoMapper;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Queries.GetClients
{
    public record GetClientsHandler : IRequestHandler<GetClientsQuery, List<Client>>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        private readonly IMapper _mapper;

        public GetClientsHandler(IBillUnitOfWork billUnitOfWork, IMapper mapper)
        {
            _billUnitOfWork = billUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Client>> Handle(GetClientsQuery request,
            CancellationToken cancellationToken) => await _billUnitOfWork.ClientReadOnlyRepository.GetAllClients();
    }
}
