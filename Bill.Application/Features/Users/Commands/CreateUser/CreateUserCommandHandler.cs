using AutoMapper;
using Bill.Domain.Repositories;
using Bill.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bill.Application.Features.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IBillUnitOfWork _billUnitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IBillUnitOfWork billUnitOfWork, IMapper mapper, ILogger<CreateUserHandler> logger)
    {
        _billUnitOfWork = billUnitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        User entity = _mapper.Map<User>(command.createUserDto);
        entity.IsActive = false;

        try
        {
            await _billUnitOfWork.UserCommandRepository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create client failed, {@exception}", ex);
            throw new Exception("Failed to add a new client");
        }

        return true;
    }
}