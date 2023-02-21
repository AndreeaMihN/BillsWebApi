using Bill.Domain.Repositories;
using Bill.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bill.Application.Features.Users.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly IBillUnitOfWork _billUnitOfWork;
    private readonly ILogger<GetUsersHandler> _logger;

    public GetUsersHandler(IBillUnitOfWork billUnitOfWork, ILogger<GetUsersHandler> logger)
    {
        _billUnitOfWork = billUnitOfWork;
        _logger = logger;
    }

    public async Task<List<User>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        List<User> users = new List<User>();
        try
        {
            users = await _billUnitOfWork.UserReadOnlyRepository.GetAllUsers();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get users failed, {@exception}", ex);
            throw new Exception("Failed to get list with all users");
        }

        return users;
    }
}