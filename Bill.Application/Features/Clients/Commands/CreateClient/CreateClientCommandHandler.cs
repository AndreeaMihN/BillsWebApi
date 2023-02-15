using AutoMapper;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using MediatR;

namespace Bill.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, bool>
    {
        private readonly IBillUnitOfWork _billUnitOfWork;
        private readonly IMapper _mapper;

        public CreateClientHandler(IBillUnitOfWork billUnitOfWork, IMapper mapper)
        {
            _billUnitOfWork = billUnitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            Client entity = _mapper.Map<Client>(command.createClientDto);
            entity.IsActive = false;

            try
            {
                await _billUnitOfWork.ClientCommandRepository.CreateAsync(entity);
            }
            catch
            {
                throw new Exception("Failed to add a new client");
            }

            return true;
        }
    }
}