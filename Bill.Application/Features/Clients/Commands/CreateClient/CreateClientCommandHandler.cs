using AutoMapper;
using Bill.Domain.Clients;
using Bill.Domain.Clients.Responses;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientResponse>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        private readonly IMapper _mapper;

        public CreateClientHandler(IBillUnitOfWork billUnitOfWork, IMapper mapper)
        {
            _billUnitOfWork = billUnitOfWork;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            Client entity = _mapper.Map<Client>(command.clientRequest);
            entity.IsActive = false;

            return await _billUnitOfWork.ClientCommandRepository.CreateAsync(entity);
        }
    }
}