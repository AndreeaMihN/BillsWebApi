using AutoMapper;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bill.Application.Features.Clients.Commands.CreateClient;

public class CreateClientHandler : IRequestHandler<CreateClientCommand, bool>
{
    private readonly IBillUnitOfWork _billUnitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateClientHandler> _logger;

    public CreateClientHandler(IBillUnitOfWork billUnitOfWork, IMapper mapper, ILogger<CreateClientHandler> logger)
    {
        _billUnitOfWork = billUnitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        Client entity = _mapper.Map<Client>(command.createClientDto);
        entity.IsActive = false;

        try
        {
            await _billUnitOfWork.ClientCommandRepository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create client failed, {@exception}", ex);
            throw new Exception("Failed to add a new client");
        }

        return true;
    }
}