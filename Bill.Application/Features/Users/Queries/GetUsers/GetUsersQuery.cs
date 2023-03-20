using Bill.Domain.Users;
using MediatR;

namespace Bill.Application.Features.Users.Queries.GetUsers;
public record GetUsersQuery : IRequest<List<User>>;